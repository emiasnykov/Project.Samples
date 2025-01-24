using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Models;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Models.ResponseBody;
using GluwaAPI.TestEngine.Setup;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;

namespace GluwaAPI.TestEngine.Utils
{
    /// <summary>
    /// Methods and variables used throughout the Exchange API tests
    /// </summary>
    public class ExchangeFunctions
    {
        /// <summary>
        /// Get ID for Exchange Requests tests
        /// </summary>
        /// <param name="errorType"></param>
        /// <returns></returns>
        public static string GetNegativeID(EErrorType errorType)
        {
            return errorType switch
            {
                EErrorType.InvalidUrlParameters => "foobar",
                EErrorType.NotFound => "029bc549-98e1-43aa-a0c8-ff1c32830588",
                EErrorType.MissingBody => "dummyID",
                _ => GetExchangeRequest().ID,
            };
        }

        /// <summary>
        /// Sets up tests for Market Maker and Exchange Requests
        ///     Cancels all active orders made by qa.assertible and qa.exchangemaker
        ///     For Exchange Requests tests
        ///         -Checks market maker status is off
        ///     For Market Maker
        ///         -Checks that market maker status is on
        ///         -Checks that we're not in staging env
        /// </summary>
        /// <param name="bTestingMarketMaker"></param>
        public static void SetUpCreateExchangeTests(EUserType qaUser, string environment)
        {
            //Set environment
            GluwaTestApi.InitializeGluwaApi = environment;

            //Cancel orders
            GluwaTestApi.QAGluwaUser = EUserType.QAAssertible;
            TryToCancelActiveOrders();

            //Cancel all orders in QA Exchange Maker
            GluwaTestApi.QAGluwaUser = EUserType.QAExchangeMaker;
            TryToCancelActiveOrders();

            //The final qa user to run the test
            GluwaTestApi.QAGluwaUser = qaUser;
        }

        /// <summary>
        /// Deletes the order made in test
        /// </summary>
        /// <param name="orderID"></param>
        public static void TearDownOrder(string orderID)
        {
            //Cancels orders
            IRestResponse response = CancelOrder(orderID);

            //Verify that the right status code has been recieved
            Assertions.HandleSetupAssertions((int)HttpStatusCode.OK, response);

            GetOrderByIDResponse order = GetOrderByID(orderID);

            try
            {
                Assert.AreEqual("Canceled", order.Status);
            }
            catch (AssertionException e)
            {
                throw new Exception($"OrderID {orderID} wasn't canceled", e);
            }
        }

        /// <summary>
        /// Cancels the given order
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IRestResponse CancelOrder(string ID, string action = "Cancel")
        {
            //Arrange
            var body = new { Action = action };

            //Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Orders/{ID}"), Api.SendRequest(Method.PATCH, body));

            return response;
        }

        /// <summary>
        /// Creates Exchange for the exchange request
        /// Order > Quote
        /// </summary>
        /// <returns></returns>
        public static PostQuoteResponse CreateExchange(EConversion takerConversion, 
                                                        ExchangeAddressItem maker, 
                                                        ExchangeAddressItem taker, 
                                                        decimal quoteAmount, 
                                                        decimal orderSourceAmount, 
                                                        decimal price, 
                                                        string environment)
        {

            // Create Order
            TryToCreateOrder(takerConversion.ToReverseConversion(), orderSourceAmount, price, maker);

            // Change to taker
            GluwaTestApi.QAGluwaUser = EUserType.QAAssertible;

            // Match quote with order from above
            PostQuoteResponse quote = TryToCreateQuote(takerConversion, quoteAmount, taker);
            TestContext.WriteLine($"OrderID {quote.MatchedOrders[0].OrderID}");

            // Accept quote
            TryToAcceptQuote(takerConversion, quote, taker.Sender, environment);

            // Switch user to maker
            GluwaTestApi.QAGluwaUser = EUserType.QAExchangeMaker;

            return quote;
        }

        /// <summary>
        /// Creates an order and asserts if order has been made
        /// </summary>
        /// <param name="sender">Sending Address information</param>
        /// <param name="receiver">Receiver Address information</param>
        /// <returns></returns>
        public static string TryToCreateOrder(EConversion conversion, decimal source, decimal price, ExchangeAddressItem maker)
        {
            // Get Exchange SetUp Method 
            Assertions.ExchangeSetUpMethod = MethodBase.GetCurrentMethod().Name;
            PostOrderBody body = RequestTestBody.CreatePostOrderBody(conversion, source, price, maker);

            IRestResponse orderResponse = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"), Api.SendRequest(Method.POST, body));

            if (orderResponse.Content.Contains("NotEnoughBalance"))
            {
                Assert.Inconclusive("NotEnoughBalance in " + maker.Sender.Address);
            }
            TestContext.WriteLine($"order response {orderResponse.Content}");
            Assertions.HandleSetupAssertions(201, orderResponse);
            return JObject.Parse(orderResponse.Content).SelectToken("ID").ToString();
        }

        /// <summary>
        /// Creates quote and asserts if quote has been made
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public static PostQuoteResponse TryToCreateQuote(EConversion conversion, decimal quoteAmount, ExchangeAddressItem taker)
        {
            // Get Exchange SetUp Method 
            Assertions.ExchangeSetUpMethod = MethodBase.GetCurrentMethod().Name;
            PostQuoteBody body = RequestTestBody.CreatePostQuoteBody(conversion, quoteAmount, taker.Sender, taker.Receiver);
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.POST, body));
            Assertions.HandleSetupAssertions(200, response);

            return JsonConvert.DeserializeObject<PostQuoteResponse>(response.Content);
        }

        /// <summary>
        /// Create Order and Quote
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns>Quote and the resulting Order ID created</returns>
        public static (PostQuoteResponse, string) CreateOrderAndQuote(ExchangeAddressItem maker, ExchangeAddressItem taker, EConversion takerConversion, EConversion makerConversion)
        {
            // Set Up quote amounts, order amount and order price
            decimal quoteAmount = takerConversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal orderAmount, decimal orderPrice) = AmountUtils.GetMatchedOrderAmountAndPrice(takerConversion, quoteAmount);

            // Create Order
            string orderId = TryToCreateOrder(makerConversion, orderAmount, orderPrice, maker);

            PostQuoteResponse quote = TryToCreateQuote(takerConversion, quoteAmount, taker);
            return (quote, orderId);
        }

        /// <summary>
        /// Try to accept quote
        /// </summary>
        /// <param name="quote">Quote Response Item</param>
        /// <param name="sender">Taker Sender</param>
        /// <returns></returns>
        public static string TryToAcceptQuote(EConversion conversion, PostQuoteResponse quote, AddressItem sender, string environment)
        {
            // Get Exchange SetUp Method 
            Assertions.ExchangeSetUpMethod = MethodBase.GetCurrentMethod().Name;

            PutQuoteBody body = RequestTestBody.CreatePutQuoteBody(conversion, quote, sender, environment);
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.PUT, body));

            Assertions.HandleSetupAssertions(202, response);
            AmountUtils.CheckUnspentOutputs(response.Content, sender.Address);

            // Return parsed body 
            return JObject.Parse(response.Content).SelectToken("ID").ToString(); ;
        }

        /// <summary>
        /// Returns Order Status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static List<GetOrdersResponse> GetOrdersByStatus(string status)
        {
            Assertions.ExchangeSetUpMethod = MethodBase.GetCurrentMethod().Name;

            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"),
                                     Api.SendRequest(Method.GET)
                                    .AddQueryParameter("Status", status)
                                    .AddQueryParameter("limit", "100"));

            // Verify that the correct response is recieved
            Assertions.HandleSetupAssertions((int)HttpStatusCode.OK, response);
            List<GetOrdersResponse> ordersList = JsonConvert.DeserializeObject<List<GetOrdersResponse>>(response.Content);

            return ordersList;
        }

        /// <summary>
        /// Returns Order coressponding to ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static GetOrderByIDResponse GetOrderByID(string ID)
        {
            // Get Exchange SetUp Method for Assertion Handling
            Assertions.ExchangeSetUpMethod = MethodBase.GetCurrentMethod().Name;
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Orders/{ID}"), Api.SendRequest(Method.GET));
            Assertions.HandleSetupAssertions((int)HttpStatusCode.OK, response);

            try
            {
                return JsonConvert.DeserializeObject<GetOrderByIDResponse>(response.Content);
            }
            catch (JsonReaderException e)
            {
                string testName = TestContext.CurrentContext.Test.Name;
                throw new Exception($"Cannot parse order in GetOrderByID for Test: {testName}", e);
            }
        }

        /// <summary>
        /// Returns Quotes corresponding to addresses
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IRestResponse GetQuoteByAddress(ECurrency source, string environment = "Test")
        {
            // Get 
            AddressItem sending;
            if(source == ECurrency.Btc)
            {
                sending = QAKeyVault.GetBtcGluwacoinExchangeAddress("TakerSender", "TakerReceiver", environment).Sender;
            }
            else
            {
                sending = QAKeyVault.GetGluwaExchangeAddress("TakerSender", "TakerReceiver").Sender;
            }
            string xRequestSignature = SignaturesUtil.GetXRequestSignature(sending.PrivateKey);

            // Arrange 
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{source}/Addresses/{sending.Address}/Quotes"),
                                     Api.SendRequest(Method.GET)
                                    .AddHeader("X-REQUEST-SIGNATURE", xRequestSignature)
                                    .AddQueryParameter("status", "Processed")
                                    .AddQueryParameter("limit", "100"));
            return response;
        }

        /// <summary>
        /// Returns a quote ID based on an address
        /// </summary>
        /// <returns></returns>
        public static string GetQuotesID(EConversion conversion)
        {
            // Get quote by address
            IRestResponse response = GetQuoteByAddress(conversion.ToSourceCurrency());

            if(response.StatusCode == HttpStatusCode.InternalServerError)
            {
                Assert.Fail($"Received an internal server errror (500) response from '{response.ResponseUri}'. A bug has been created for this issue - https://gluwa.atlassian.net/browse/GLA-868");
            }

            // return first quote
            List<GetAddressQuoteResponse> responseBodyList = JsonConvert.DeserializeObject<List<GetAddressQuoteResponse>>(response.Content);

            try
            {
                // Check if quote exists with said conversion
                return responseBodyList.FirstOrDefault(c => c.Conversion == conversion.ToString()).ID;
            }
            catch(NullReferenceException)
            {
                Assert.Inconclusive($"Quote with conversion {conversion} doesn't exist ");
            }
            return null;
        }

        /// <summary>
        /// Cancels any active orders on test
        /// </summary>
        public static void TryToCancelActiveOrders()
        {
            List<GetOrdersResponse> activeOrdersList = GetOrdersByStatus("Active");
            int activeOrdersCount = activeOrdersList.Count();

            if (activeOrdersCount == 0)
            {
                TestContext.WriteLine("No Active Orders to Cancel. Continuing with test");
            }
            else
            {
                foreach (var order in activeOrdersList)
                {
                    // Cancels orders
                    IRestResponse response = CancelOrder(order.ID);

                    // Verify that the right status code has been recieved
                    Assertions.HandleSetupAssertions((int)HttpStatusCode.OK, response);
                }

                // Check if all orders have been canceled in test
                activeOrdersList = GetOrdersByStatus("Active");

                try
                {
                    Assert.AreEqual(0, activeOrdersList.Count());
                }
                catch
                {
                    TestContext.WriteLine("Active orders not canceled");
                }
            }
        }

        /// <summary>
        /// Calls orderbook
        /// </summary>
        /// <param name="conversion"></param>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        public static IRestResponse GetOrderBook(string conversion, Dictionary<string, string> queryParameters)
        {
            // Arrange
            IRestRequest request = Api.SendRequest(Method.GET);


            if (queryParameters != null)
            {
                foreach (KeyValuePair<string, string> kvp in queryParameters)
                {
                    // Extracting query parameters
                    // Using Dictionary so that we can pass in incorrect query parameter PURPOSEFULLY
                    request.AddQueryParameter(kvp.Key, kvp.Value);
                }
            }

            // act
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Orderbook/{conversion}"), request);
            return response;
        }

        /// <summary>
        /// Calls the exchange request webhook endpoint
        /// </summary>
        /// <returns>Latest exchange made</returns>
        public static GetExchangeRequestResponse GetExchangeRequest()
        {
            IRestResponse response = Api.GetResponse(Api.SetWebhookReceiver("ExchangeRequest"),
                Api.SendRequest(Method.GET)
                .AddQueryParameter("userID", GluwaTestApi.WebhookUserID));
                 Assertions.HandleSetupAssertions(200, response);
            try
            {
                List<GetExchangeRequestResponse> exchangeRequestList = JsonConvert.DeserializeObject<List<GetExchangeRequestResponse>>(response.Content);
                Assert.IsTrue(exchangeRequestList.Count >= 1);

                return exchangeRequestList.First();
            }
            catch (AssertionException e)
            {
                TestContext.WriteLine("There are no exchange requests in the webhook receiver.Please create an exchange");
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Returns the most recent exchange request from Exchange Request endpoint
        /// </summary>
        /// <param name="currentExchangeID"></param>
        /// <returns></returns>
        public static GetExchangeRequestResponse GetMostRecentExchangeRequest(string currentExchangeID)
        {
            // Wait for the database to load the most recent exchange request
            int numAttempt = 0;
            int maxAttempt = 7;
            TimeSpan waitInterval = new TimeSpan(0, 0, 30);

            // Calling the endpoint every wait interval seconds to check if the latest order is in the database       
            while (numAttempt < maxAttempt)
            {
                Thread.Sleep(waitInterval);
                GetExchangeRequestResponse exchangeRequest = GetExchangeRequest();

                //Check if the exchange request id has changed
                if (exchangeRequest.ID != currentExchangeID)
                {
                    string exchangeID = exchangeRequest.ID;
                    TestContext.WriteLine("exchangeID: {0}", exchangeID);
                    return exchangeRequest;
                }

                numAttempt++;
            }
            // Assert
            Assert.Warn("Couldn't find the latest exchangeID");
            return null;
        }
        
        /// Verifies if MarketMaker test can be run based on environment 
        /// Market Maker orders status
        /// </summary>
        public static void VerifyMarketMakerStatus(string environment, bool bMarketMakerTest)
        {
            switch (environment)
            {
                case "Test":
                    //Set environment
                    GluwaTestApi.InitializeGluwaApi = environment;
                    bool bGluwaMarketMakeStatus = getGluwaMarketMakerShutdownStatus(); // Check market maker on status

                    if (bMarketMakerTest && bGluwaMarketMakeStatus)
                    {
                        TestContext.WriteLine("MarketMaker is on. Continuing with test");
                        GluwaTestApi.QAGluwaUser = EUserType.QAAssertible;
                        break;
                    }
                    else if (bMarketMakerTest && !bGluwaMarketMakeStatus)
                    {
                        Assert.Ignore("MarketMaker is currently shut down. Please turn on MarketMaker before running test");
                        break;
                    }
                    else if (!bMarketMakerTest && bGluwaMarketMakeStatus)
                    {
                        Assert.Ignore("MarketMaker is currently on. Please turn off MarketMaker before running test");
                        break;
                    }
                    else if (!bMarketMakerTest && !bGluwaMarketMakeStatus)
                    {
                        TestContext.WriteLine("MarketMaker is off. Continuing with test");
                        GluwaTestApi.QAGluwaUser = EUserType.QAExchangeMaker;
                        break;

                    }
                    else
                    {
                        throw new Exception("Error: No market maker status matches with test type");
                    }
                case "Staging":
                case "Sandbox":
                    if (bMarketMakerTest)
                    {
                        Assert.Ignore("Market Maker test is running on sandbox/staging. Ignoring case");
                    }
                    else
                    {
                        TestContext.WriteLine("MarketMaker doesn't run on sandbox/staging. Continuing with test");
                        GluwaTestApi.QAGluwaUser = EUserType.QAExchangeMaker;
                    }
                    break;
                default:
                    throw new Exception("Error: Cannot verify market maker");
            }
        }

        /// <summary>
        /// Checks the Market Maker shutdown status from QA functions app
        /// </summary>
        /// <returns></returns>
        private static bool getGluwaMarketMakerShutdownStatus()
        {

            IRestResponse response = Api.GetResponse(Api.SetQAFunctionsExchange("/api/GetMarketMakerStatus"),
                                    Api.SendRequest(Method.GET));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assertions.HandleSetupAssertions(200, response);

                JObject obj = JObject.Parse(response.Content);

                string marketMakerStatus = obj.SelectToken("IsMarketMakingShutdown").ToString();
                string gluwaUsdgExchangeStatus = obj.SelectToken("IsGluwaUsdgExchangeShutdown").ToString();
                string gluwaKrwgExchangeStatus = obj.SelectToken("IsGluwakrwgExchangeShutdown").ToString();

                bool bMarketMaker = Convert.ToBoolean(marketMakerStatus);
                bool bGluwaUsdgExchange = Convert.ToBoolean(gluwaUsdgExchangeStatus);
                bool bGluwaKrwgExchange = Convert.ToBoolean(gluwaKrwgExchangeStatus);

                return (bMarketMaker == false && bGluwaUsdgExchange == false && bGluwaKrwgExchange == false);
            }
            else 
            {
                Assert.Inconclusive("Access is denied. Cannot retrieve Gluwa Market Maker Status");
                return false;               
            }
        }

        /// <summary>
        /// Create and accept a quote to exchange with the market maker
        /// </summary>
        /// <returns></returns>
        public static IRestResponse createQuoteForMarketMaker(ExchangeAddressItem taker, EConversion takerConversion, string environment)
        {
            // Match quote with Order
            PostQuoteResponse quote = TryToCreateQuote(takerConversion, takerConversion.ToSourceCurrency().ToDefaultCurrencyAmount(), taker);

            // Create Accept quote body
            PutQuoteBody body = RequestTestBody.CreatePutQuoteBody(takerConversion, quote, taker.Sender, environment);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.PUT, body));

            return response;
        }

        /// <summary>
        /// Verify that the Market Maker Order created is the correct status
        /// </summary>
        /// <param name="order"></param>
        /// <param name="expectedStatus"></param>
        /// <param name="notExpectedStatus"></param>
        public static void verifyMarketMakerOrderStatus(GetOrdersResponse order, string expectedStatus, string notExpectedStatus)
        {
            // Verify order is still in the correct get order db
            Assert.AreEqual(expectedStatus, order.Status);

            // Verify order is not cancelled/active
            List<GetOrdersResponse> notExpectedOrdersList = GetOrdersByStatus(notExpectedStatus);
            Assert.IsFalse(notExpectedOrdersList.Contains(order));

            // Verify get orderID reflects this status
            string orderID = order.ID;

            GetOrderByIDResponse orderCreated = GetOrderByID(orderID);
            Assert.AreNotEqual(notExpectedStatus, orderCreated.Status);
        }
    }
}
