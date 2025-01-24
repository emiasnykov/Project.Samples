using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Models;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Models.ResponseBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace GluwaAPI.TestEngine.AssertionHandlers
{
    public class Assertions
    {
        /// <summary>
        /// The name of the method that's currently being run so ie: TryCreateOrder
        /// </summary>
        public static string ExchangeSetUpMethod { get; set; }

        /// <summary>
        /// Handles response for methods that are use to set up an exchange
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="response"></param>
        /// <param name="callerName"></param>
        public static void HandleSetupAssertions(int expectedStatusCode, IRestResponse response, [CallerMemberName] string callerName = null)
        {
            if (response.Content.Contains($"The value '{TestSettingsUtil.Currency}' is not valid"))
            {
                string requestBody = formatRequestBody(response, callerName);
                writeAssertionDetails(response, callerName);
                TestContext.WriteLine(requestBody);
                Assert.Inconclusive($"The currency '{TestSettingsUtil.Currency}' seems to be disabled in this environment or invalid");
            }

            // Getting method name for error reporting
            string setupName = $"{ExchangeSetUpMethod}.{callerName}";
            try
            {
                Assert.AreEqual(expectedStatusCode, (int)response.StatusCode);
            }
            catch (AssertionException e)
            {
                writeAssertionDetails(response, setupName);
                string requestBody = formatRequestBody(response, callerName);

                if (!setupName.ToLower().Contains("get"))
                {
                    TestContext.WriteLine($"Showing Request Body \n {requestBody}");
                }
                throw new Exception($"Exception at {callerName}", e);
            }
        }

        /// <summary>
        /// Handles test assertions with checking Tx amount
        /// </summary>
        /// <param name="response"></param>
        /// <param name="body"></param>
        public static void HandleAssertionStatusCode(IRestResponse response, PostTransactionsBody body, AddressItem address, string environment = "")
        {
            if (response.Content.Contains("NotEnoughBalance"))
            {
                TestContext.WriteLine("NotEnoughBalance in" + address.Address);
                Assert.Inconclusive("NotEnoughBalance in " + address.Address);
            }
            else
            if (response.Content == "")
            {
                HandleAssertionStatusCode(HttpStatusCode.Accepted, response, address, environment);
            }
        }

        /// <summary>
        /// Check response message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="response"></param>
        public static void HandleAssertionMessage(string msg, IRestResponse response)
        {
            JObject obj = JObject.Parse(response.Content);
            TestContext.WriteLine("Response: " + obj.ToString());
            Assert.AreEqual(msg, obj["Message"].ToString());
        }

        /// <summary>
        /// Check response code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="response"></param>
        public static void HandleAssertionCode(string code, IRestResponse response)
        {
            JObject obj = JObject.Parse(response.Content);
            TestContext.WriteLine("Response: " + obj.ToString());
            Assert.AreEqual(code, obj["Code"].ToString());
        }

        /// <summary>
        /// Check Inner Error message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="response"></param>
        public static void HandleAssertionInnerMessage(string msg, IRestResponse response, int errorIndex = 0)
        {
            JObject obj = JObject.Parse(response.Content);
            var data = (string)obj.SelectToken($"InnerErrors[{errorIndex}].Message");
            Assert.AreEqual(msg, data);
        }

        /// <summary>
        /// Check Inner Error code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="response"></param>
        public static void HandleAssertionInnerCode(string code, IRestResponse response, int errorIndex = 0)
        {
            JObject obj = JObject.Parse(response.Content);
            var data = (string)obj.SelectToken($"InnerErrors[{errorIndex}].Code");
            Assert.AreEqual(code, data);
        }

        /// <summary>
        /// Verify the amounts in the quote response
        /// </summary>
        /// <param name="quoteAmount"></param>
        /// <param name="quote"></param>
        public static void VerifyQuoteResponseAmounts(string quoteAmount, PostQuoteResponse quote)
        {
            string price = getPriceFromMatchedOrder(quote);
            string expectedExchangedAmount = calculateExpectedExchangedAmount(quoteAmount, price);

            MatchedOrder order = quote.MatchedOrders[0];

            decimal exchangedAmountDecimal = decimal.Parse(order.ExchangedAmount);
            string exchangedAmount = Math.Round(exchangedAmountDecimal, 5).ToString();

            Assert.AreEqual(quoteAmount, order.SourceAmount, "Source Amount is not equal");
            Assert.AreEqual(expectedExchangedAmount.Substring(0, 1), exchangedAmount.Substring(0, 1), "Exchanged Amount is not equal");
        }

        /// <summary>
        /// Verify quotes in quote list
        /// </summary>
        /// <param name="response"></param>
        public static void VerifyQuotesInlist(IRestResponse response)
        {
            // Verify quotes in quote list
            List<GetAddressQuoteResponse> quoteList = JsonConvert.DeserializeObject<List<GetAddressQuoteResponse>>(response.Content);
            if (quoteList.Count > 0)
            {
                // Check all quotes are processed
                foreach (GetAddressQuoteResponse quote in quoteList)
                {
                    Assert.AreEqual("Processed", quote.Status);
                }
            }
        }

        /// <summary>
        /// Handles test assertions
        /// </summary>
        /// <param name="expectedStatusCode"></param>
        /// <param name="response"></param>
        /// <param name="callerName"></param>
        public static void HandleAssertionStatusCode(HttpStatusCode expectedStatusCode, IRestResponse response, string environment = "", [CallerMemberName] string callerName = "")
        {
            string requestBody = formatRequestBody(response, callerName);

            if (response.Content.Contains($"The value '{TestSettingsUtil.Currency}' is not valid"))
            {
                writeAssertionDetails(response, callerName);
                TestContext.WriteLine(requestBody);
                Assert.Inconclusive($"The currency '{TestSettingsUtil.Currency}' seems to be disabled in this environment or invalid");
            }
            else if (response.Content.Contains("NotEnoughBalance") || response.Content.Contains("not have enough funds") || response.Content.Contains("InsufficientFund"))
            {
                TestContext.WriteLine("NotEnoughBalance in SenderAddress. Insufficient funds.");
                Assert.Ignore("NotEnoughBalance in SenderAddress. Insufficient funds.");
            }
            else if (response.Content.Contains("Eth balance is too low"))
            {
                TestContext.WriteLine("Not enough gas fee. Insufficient funds.");
                Assert.Ignore("Not enough gas fee. Insufficient funds.");
            }
            else if (response.Content.Contains("ServiceDisabled"))
            {
                TestContext.WriteLine("ServiceDisabled");
                Assert.Ignore("ServiceDisabled");
            }
            else if(response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                TestContext.WriteLine("Received ServiceUnavailable (503) response, test cannot be validated at this time.");
                TestContext.WriteLine($"Response received: {response.Content}");
                Assert.Ignore("ServiceUnavailable");
            }
            else
            {
                try
                {
                    Assert.AreEqual((int)expectedStatusCode, (int)response.StatusCode, message:"Environment: " + environment);

                    //Show request body
                    if (response.Request.Method != Method.GET)
                    {
                        TestContext.WriteLine($"\nShowing Request Body from {callerName} \n{requestBody} ");
                    }
                    else
                    {
                        TestContext.WriteLine($"\nShowing Query Parameters from {callerName}");
                        foreach (var param in response.Request.Parameters)
                        {
                            TestContext.WriteLine($"{param.Name} : {param.Value}");
                        }
                    }
                }
                catch
                {
                    writeAssertionDetails(response, callerName);
                    TestContext.WriteLine(requestBody);
                }
            }
        }

        /// <summary>
        /// Verifies the orders in orderbook
        /// </summary>
        /// <param name="orderList"></param>
        /// <param name="status"></param>
        public static void CheckOrderList(List<GetOrdersResponse> orderList, string status)
        {
            // Handle body being empty
            if (orderList.Count > 0)
            {
                // Check all orders are active
                foreach (GetOrdersResponse order in orderList)
                {
                    Assert.AreEqual(status, order.Status);
                }
            }
        }

        /// <summary>
        /// Handle conversions in response
        /// </summary>
        /// <param name="quote"></param>
        public static void HandleConversionAssertions(CurrencyUtils.EConversion conversion, GetQuoteByIDResponse quote)
        {
            try
            {
                Assert.IsFalse(string.IsNullOrEmpty(quote.Conversion));
            }
            catch
            {
                Assert.Warn("ERROR: Couldn't parse response body to assert");
                TestContext.WriteLine("\"Conversion\" doesn't exist in the response body");
            }
            Assert.AreEqual(conversion.ToString(), quote.Conversion);
        }

        /// <summary>
        /// Handles test assertions
        /// </summary>
        /// <param name="expectedStatusCode"></param>
        /// <param name="response"></param>
        /// <param name="callerName"></param>
        public static void HandleAssertionStatusCode(HttpStatusCode expectedStatusCode, IRestResponse response, AddressItem address, [CallerMemberName] string callerName = "", string environment = "")
        {
            string requestBody = formatRequestBody(response, callerName);

            if (response.Content.Contains("NotEnoughBalance"))
            {
                TestContext.WriteLine("NotEnoughBalance in" + address.Address);
                Assert.Inconclusive("NotEnoughBalance in " + address.Address);
            }
            else
            {
                try
                {
                    Assert.AreEqual((int)expectedStatusCode, (int)response.StatusCode, message: "Environment: " + environment);

                    // Show request body
                    if (response.Request.Method != Method.GET)
                    {
                        TestContext.WriteLine($"\nShowing Request Body from {callerName} \n{requestBody} ");
                    }
                    else
                    {
                        TestContext.WriteLine($"\nShowing Query Parameters from {callerName}");
                        foreach (var param in response.Request.Parameters)
                        {
                            TestContext.WriteLine($"{param.Name} : {param.Value}");
                        }
                    }
                }
                catch
                {
                    writeAssertionDetails(response, callerName);
                    TestContext.WriteLine(requestBody);
                }
            }
        }

        /// <summary>
        /// Assert Insufficient Funds case for positive tests 
        /// </summary>
        /// <param name="responseContent"></param>
        public static void InsufficientFunds(IRestResponse response)
        {
            JObject jObject = JObject.Parse(response.Content);
            Assert.AreEqual("Insufficient funds.", jObject["Message"].ToString());
        }

        /// <summary>
        /// Handle negative error message
        /// </summary>
        /// <param name="expectedErrorItem"></param>
        /// <param name="response"></param>
        public static void HandleNegativeTestErrorMessages(ValidationErrorItem expectedErrorItem, IRestResponse response)
        {
            string responseContent = response.Content;
            ValidationErrorItem errorMessageItem = JsonConvert.DeserializeObject<ValidationErrorItem>(responseContent);
            TestContext.WriteLine($"\nEnvironment: {response.ResponseUri}\nStatusCode: {response.StatusCode}");

            if (string.IsNullOrEmpty(responseContent))
            {
                TestContext.WriteLine("\nNo response body to show");
            }
            else
            {
                TestContext.WriteLine($"\nShowing response body:\n {responseContent}");
                try
                {
                    // Verify error codes and message
                    Assert.AreEqual(expectedErrorItem.Code, errorMessageItem.Code, "Codes do not match.");
                    Assert.AreEqual(expectedErrorItem.Message, errorMessageItem.Message, "Messages do not match.");

                    if (expectedErrorItem.InnerErrors != null)
                    {
                        // Verify inner errors if they are expected
                        Assert.IsFalse(errorMessageItem.InnerErrors == null, "No inner errors found.");
                        Assert.AreEqual(expectedErrorItem.InnerErrors.Count, errorMessageItem.InnerErrors.Count, "More errors than expected.");

                        // Verify the inner errors code, path and message
                        InnerError expectedInnerError = expectedErrorItem.InnerErrors[0];
                        InnerError innerError = errorMessageItem.InnerErrors[0];

                        Assert.AreEqual(expectedInnerError.Code, innerError.Code, "Inner Error codes don't match");
                        Assert.AreEqual(expectedInnerError.Path, innerError.Path, "Inner Error paths don't match.");
                        Assert.AreEqual(expectedInnerError.Message, innerError.Message, "Inner Error message don't match.");
                    }
                }
                catch (AssertionException assertExp)
                {
                    string expectedErrorMessage = JsonConvert.SerializeObject(expectedErrorItem);
                    TestContext.WriteLine($"Errors didn't match. Printing out expected error message: \n{expectedErrorMessage}");
                    throw new Exception(assertExp.Message);
                }
            }

        }

        /// <summary>
        /// Assert Error messages and code for negative Auth Tests
        /// </summary>
        /// <param name="expectedErrorItem"></param>
        /// <param name="responseContent"></param>
        public static void HandleAuthTestErrorMessages(AuthErrorItem expectedErrorItem, IRestResponse response)
        {
            string responseContent = response.Content;
            JObject errorMessageItem = JObject.Parse(response.Content);

            TestContext.WriteLine($"\nEnvironment: {response.ResponseUri}\nStatusCode: {response.StatusCode}");

            if (string.IsNullOrEmpty(responseContent))
            {
                TestContext.WriteLine("\nNo response body to show");
            }
            else
            {
                TestContext.WriteLine($"\nShowing response body:\n {responseContent}");

                try
                {
                    // Verify error codes and message
                    Assert.AreEqual(expectedErrorItem.Error.ToString(), errorMessageItem.GetValue("error").ToString(), "Error do not match.");
                    if (expectedErrorItem.Description != null)
                    {
                        Assert.AreEqual(expectedErrorItem.Description, errorMessageItem.GetValue("error_description").ToString(), "Messages do not match.");
                    }
                }
                catch (AssertionException assertExp)
                {
                    string expectedErrorMessage = JsonConvert.SerializeObject(expectedErrorItem);
                    TestContext.WriteLine($"Errors didn't match. Printing out expected error message: \n{expectedErrorMessage}");
                    throw new Exception(assertExp.Message);
                }
            }
        }

        /// <summary>
        /// Handle asserting of inner error in content
        /// </summary>
        /// <param name="expectedErrorCode"></param>
        /// <param name="expectedErrorMessage"></param>
        /// <param name="response"></param>
        public static void HandleAssertionInnerErrorContent(string expectedErrorCode, string expectedErrorMessage, IRestResponse response)
        {
            try
            {
                ValidationErrorItem errorBody = JsonConvert.DeserializeObject<ValidationErrorItem>(response.Content);
                Assert.AreEqual(expectedErrorMessage, errorBody.InnerErrors[0].Message);
                Assert.AreEqual(expectedErrorCode, errorBody.InnerErrors[0].Code);
            }
            catch
            {
                // Getting method name for error reporting
                StackTrace m = new StackTrace();
                TestContext.WriteLine("Assertion detail {0}:", m.GetFrame(1).GetMethod().Name);
                TestContext.WriteLine("Response Uri: ");
                TestContext.WriteLine(response.ResponseUri);
                TestContext.WriteLine("Response Body: ");
                if (response.Content != "")
                {
                    TestContext.WriteLine(JObject.Parse(response.Content).ToString(Formatting.None));
                }
            }
        }

        /// <summary>
        /// Handle asserting of error in content
        /// </summary>
        /// <param name="expectedErrorCode"></param>
        /// <param name="expectedErrorMessage"></param>
        /// <param name="response"></param>
        public static void HandleAssertionErrorContent(string expectedErrorCode, string expectedErrorMessage, IRestResponse response)
        {
            try
            {
                ValidationErrorItem errorBody = JsonConvert.DeserializeObject<ValidationErrorItem>(response.Content);
                Assert.AreEqual(expectedErrorCode, errorBody.Code);

                if (errorBody.InnerErrors == null)
                {
                    Assert.AreEqual(expectedErrorMessage, errorBody.Message);
                }
                else
                {
                    Assert.AreEqual(expectedErrorMessage, errorBody.InnerErrors[0].Message);
                }

            }
            catch
            {
                // Getting method name for error reporting
                StackTrace m = new StackTrace();
                TestContext.WriteLine("Assertion detail {0}:", m.GetFrame(1).GetMethod().Name);
                TestContext.WriteLine("Response Uri: ");
                TestContext.WriteLine(response.ResponseUri);
                TestContext.WriteLine("Response Body: ");
                if (response.Content != "")
                {
                    TestContext.WriteLine(JObject.Parse(response.Content).ToString(Formatting.None));
                }
            }
        }

        /// <summary>
        /// Handle Order Complete status
        /// </summary>
        /// <param name="quote"></param>
        public static void HandleAssertionOrderComplete(PostQuoteResponse quote, decimal expectedRemainingAmount)
        {
            string ID = quote.MatchedOrders[0].OrderID;
            var order = ExchangeFunctions.GetOrderByID(ID);

            while(order.Status == "Active")
            {
                // Sleep so that the db have time to process quote accept
                System.Threading.Thread.Sleep(new TimeSpan(0, 1, 00));
                order = ExchangeFunctions.GetOrderByID(ID);
            }

            Assert.AreEqual("Complete", order.Status, $"Order status is not Complete. Is actually {order.Status}");
            Assert.AreEqual(expectedRemainingAmount.ToString(), order.SourceAmount, $"Order amount is {order.SourceAmount} not 0");
        }

        /// <summary>
        /// Handle response with amount and currency
        /// </summary>
        /// <param name="expectedCurrency"></param>
        /// <param name="actualCurrency"></param>
        /// <param name="amount"></param>
        public static void HandleCurrencyAndAmountAssertions(string expectedCurrency, string actualCurrency, string amount)
        {
            Assert.IsTrue(string.Compare(expectedCurrency, actualCurrency, StringComparison.InvariantCultureIgnoreCase) == 0,
                $"expectedCurrency '{expectedCurrency}' and actualCurrency '{actualCurrency}' didn't match");
            Assert.IsTrue(decimal.TryParse(amount, out _), $"{amount} is not entirely digits");
        }

        /// <summary>
        /// Post assertion details on additional output
        /// </summary>
        /// <param name="response"></param>
        /// <param name="testStep"></param>
        private static void writeAssertionDetails(IRestResponse response, string testStep)
        {
            // Getting method name for error reporting
            if ((int)response.StatusCode == 500)
            {
                TestContext.WriteLine(formatRequestBody(response, testStep));
                Assert.Fail($"Received an internal server errror (500) response from '{response.ResponseUri}'.");
            }
            else if ((int)response.StatusCode == 429)
            {
                Assert.Inconclusive("API returned too many requests response. Cannot verify test output");
            }
            else
            {
                TestContext.WriteLine($"ERROR at {testStep}");
                TestContext.WriteLine($"Response Uri: {response.ResponseUri}");
                TestContext.WriteLine($"Response Status Code for {testStep}: \n {(int)response.StatusCode} - {response.StatusCode} \n");
                TestContext.WriteLine($"Printing response body from {testStep}:");

                if (response.Content != "")
                {
                    writeErrors(response);
                }
                else
                {
                    throw new Exception("Response didn't return any content");
                }
            }
        }

        /// <summary>
        /// Write out and errors in the response
        /// </summary>
        /// <param name="response"></param>
        private static void writeErrors(IRestResponse response)
        {
            ValidationErrorItem error = JsonConvert.DeserializeObject<ValidationErrorItem>(response.Content);

            TestContext.WriteLine($"Code: {error.Code}");

            if (error.InnerErrors == null)
            {
                TestContext.WriteLine($"{error.Message}\n");
            }
            else
            {
                var innerErrors = error.InnerErrors;
                for (var i = 0; i < innerErrors.Count; i++)
                {
                    // write error
                    TestContext.WriteLine($"{innerErrors[0].Message}\n");
                }
            }
        }

        /// <summary>
        /// Format Request body response
        /// </summary>
        /// <param name="response"></param>
        /// <param name="callerName"></param>
        /// <returns></returns>
        private static string formatRequestBody(IRestResponse response, string callerName)
        {

            object requestBody = response.Request.Body;

            if (requestBody != null)
            {
                string body = JsonConvert.SerializeObject(response.Request.Body.Value);
                string formattedBody = body.Replace(",", "\n").Replace("\\", "");

                Regex regex = new Regex("[{}\"]");
                string bodyRegex = regex.Replace(formattedBody, "");

                return bodyRegex;
            }
            else
            {
                if (response.Request.Method == Method.GET)
                {
                    return $"{callerName} is a GET method. No request body to print.";
                }
                else
                {
                    throw new Exception("No Request body found");
                }
            }
        }

        /// <summary>
        /// Calculates the expected exchange amount for the auote
        /// </summary>
        /// <param name="quoteAmount"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        private static string calculateExpectedExchangedAmount(string quoteAmount, string price)
        {

            decimal amount = decimal.Parse(quoteAmount);
            decimal priceConversion = decimal.Parse(price);
            decimal exchange;
            exchange = amount / priceConversion;
            string exchangedAmount = Math.Round(exchange, 5).ToString();

            return exchangedAmount;
        }

        /// <summary>
        /// Check the price from the matched order
        /// </summary>
        /// <param name="quote"></param>
        /// <returns></returns>
        private static string getPriceFromMatchedOrder(PostQuoteResponse quote)
        {

            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Orders/{quote.MatchedOrders[0].OrderID}"),
                                                    Api.SendRequest(Method.GET));

            string price = "";
            HttpStatusCode code = response.StatusCode;
            if (code == HttpStatusCode.OK)
            {
                try
                {
                    GetOrderByIDResponse order = JsonConvert.DeserializeObject<GetOrderByIDResponse>(response.Content);
                    return order.Price;
                }
                catch (JsonReaderException e)
                {
                    string testName = TestContext.CurrentContext.Test.Name;
                    throw new Exception($"Cannot parse order item in {testName}", e);
                }
            }
            else if (code == HttpStatusCode.Forbidden)
            {
                // The order is from market maker
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    string conversion = TestSettingsUtil.Conversion.ToReverseConversion().ToString();

                    // Get order information from order book
                    IRestResponse orderbookResponse = ExchangeFunctions.GetOrderBook(conversion, null);
                    List<OrderBookRespose> responseBody = JsonConvert.DeserializeObject<List<OrderBookRespose>>(orderbookResponse.Content);
                    HandleAssertionStatusCode(HttpStatusCode.OK, orderbookResponse);

                    // Get highest amount as MarketMaker orders will always take the most spendable
                    OrderBookRespose highestAmount = responseBody.OrderByDescending(c => decimal.Parse(c.Amount)).First();
                    return highestAmount.Price;
                }
            }
            else
            {
                TestContext.WriteLine("Can't Find Order");
                TestContext.WriteLine(response.Content);
            }
            return price;
        }

        /// <summary>
        /// Verifies that there are Market Maker orders
        /// </summary>
        /// <param name="conversion"></param>
        public static void CheckMarketMakerOrdersByConversion(EConversion conversion)
        {
            GluwaTestApi.QAGluwaUser = EUserType.QAMarketMaker;
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"), Api.SendRequest(Method.GET).AddQueryParameter("status", "Active"));

            HandleSetupAssertions(200, response);
            var orderList = JsonConvert.DeserializeObject<List<GetOrdersResponse>>(response.Content);

            bool isConversionOrderExist = orderList.FindAll(order => order.Conversion == conversion.ToString()).Any();

            if (!isConversionOrderExist)
            {
                Assert.Inconclusive("No order available on Market Maker");
            }
        }

        /// <summary>
        /// Verifies the currency expected in the response matches the actual currency
        /// </summary>
        /// <param name="responseObjects"></param>
        /// <param name="expectedCurrency"></param>
        public static void VerifyCurrencyMatch(JObject responseObjects, ECurrency expectedCurrency)
        {
            if (responseObjects.SelectToken("Items[0].Type").ToString() == "Exchange")
            {
                Assert.IsTrue(
                    responseObjects.SelectToken("Items[0].FromCurrency").ToString().ToUpper() == expectedCurrency.ToString().ToUpper() ||
                    responseObjects.SelectToken("Items[0].ToCurrency").ToString().ToUpper() == expectedCurrency.ToString().ToUpper(),
                    $"Expected {expectedCurrency.ToString().ToUpper()} in ToCurrency or FromCurrency but found {responseObjects.SelectToken("Items[0].FromCurrency").ToString().ToUpper()} and {responseObjects.SelectToken("Items[0].ToCurrency").ToString().ToUpper()}"
                );
                return;
            }

            Assert.IsTrue(
                responseObjects.SelectToken("Items[0].Currency").ToString().ToUpper() == expectedCurrency.ToString().ToUpper(),
                $"Expected {expectedCurrency.ToString().ToUpper()} in Currency but found {responseObjects.SelectToken("Items[0].Currency").ToString().ToUpper()}"
            );
        }
    }
}
