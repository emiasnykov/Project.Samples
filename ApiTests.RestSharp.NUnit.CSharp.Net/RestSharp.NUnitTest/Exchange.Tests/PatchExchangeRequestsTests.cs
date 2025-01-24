using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Models;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;

namespace Exchange.Tests
{
    [TestFixture("Test")]
    [TestFixture("Sandbox")]
    [Category("Exchange"), Category("ExchangeRequests"), Category("Patch")]
    public class PatchExchangeRequestTests : ExchangeFunctions
    {
        /// <summary>
        /// Current Exchange ID in the db
        /// </summary>
        private string mCurrentExchangeID { get; set; }

        private string environment;
        private EConversion conversion;
        private EConversion makerConversion;

        public PatchExchangeRequestTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up exchange requests tests
            conversion = TestSettingsUtil.Conversion;
            makerConversion = conversion.ToReverseConversion();
            SetUpCreateExchangeTests(EUserType.QAExchangeMaker, environment);
            string testName = TestContext.CurrentContext.Test.Name;
            TestContext.Out.WriteLine($"Running Test: {testName} \n");
            mCurrentExchangeID = GetExchangeRequest().ID;

            // Change to maker
            GluwaTestApi.QAGluwaUser = EUserType.QAExchangeMaker;
        }

        [Test]
        [Category("Positive"), Category("BtcsUsdcg")]
        public void Patch_ExchangeRequests_BtcsUsdcg_Pos()
        {
            // Create an exchange first
            var maker = QAKeyVault.GetGluwacoinBtcExchangeAddress("Sender", "Receiver", environment); // Btc in maker
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Get Amounts
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal sourceAmount, decimal price) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, quoteAmount);

            // Execute exchange request
            CreateExchange(conversion, maker, taker, quoteAmount, sourceAmount, price, environment); // Create exchange

            // Arrange
            GetExchangeRequestResponse exchangeRequest = GetMostRecentExchangeRequest(mCurrentExchangeID);

            // Create body
            PatchExchangeRequestBody body = RequestTestBody.CreatePatchExchangeRequestBody(makerConversion, exchangeRequest, maker.Sender,environment);

            // Execute PATCH ExchangeRequests
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/{exchangeRequest.ID}"), 
                                                     Api.SendRequest(Method.PATCH, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
        }


        [Test]
        [Category("Positive"), Category("sUsdcgBtc")]
        public void Patch_ExchangeRequests_sUsdcgBtc_Pos()
        {
            // Create an exchange first
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc in maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Get Amounts
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal sourceAmount, decimal price) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, quoteAmount);

            // Execute exchange request
            CreateExchange(conversion, maker, taker, quoteAmount, sourceAmount, price, environment); // Create exchange

            // Arrange
            GetExchangeRequestResponse exchangeRequest = GetMostRecentExchangeRequest(mCurrentExchangeID);

            // Create body
            PatchExchangeRequestBody body = RequestTestBody.CreatePatchExchangeRequestBody(makerConversion, exchangeRequest, maker.Sender,environment);

            // Execute PATCH ExchangeRequests
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/{exchangeRequest.ID}"), 
                                                     Api.SendRequest(Method.PATCH, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
        }


        [Test]
        [Category("Negative"), Category("MissingBody")]
        public void Patch_ExchangeRequests_MissingBody_Neg()
        {
            // Create an exchange first
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc in maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Get Amounts
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal sourceAmount, decimal price) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, quoteAmount);

            // Execute exchange request
            var quoteResponse = CreateExchange(conversion, maker, taker, quoteAmount, sourceAmount, price, environment); // Create exchange

            // Arrange
            var exchangeRequest = GetMostRecentExchangeRequest(mCurrentExchangeID);

            // Execute PATCH ExchangeRequests PATCH ExchangeRequests with body = null
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/{exchangeRequest.ID}"), 
                                           Api.SendRequest(Method.PATCH, null));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.missingBody, response);

            // TearDown
            var orderId = quoteResponse.MatchedOrders[0].OrderID;
            if (response.StatusCode != HttpStatusCode.Accepted && !string.IsNullOrEmpty(orderId))
            {
                // Cancel only when an order has been created and a negative exchange requests test
                GluwaTestApi.QAGluwaUser = EUserType.QAExchangeMaker;
                TearDownOrder(orderId);
            }
        }


        [Test]
        [Category("Negative"), Category("NotFound")]
        public void Patch_ExchangeRequests_NotFoundID_Neg()
        {
            // Create an exchange first
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc in maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Get Amounts
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal sourceAmount, decimal price) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, quoteAmount);

            // Execute exchange request
            var quoteResponse = CreateExchange(conversion, maker, taker, quoteAmount, sourceAmount, price, environment); // Create exchange

            // Arrange
            GetExchangeRequestResponse exchangeRequest = GetMostRecentExchangeRequest(mCurrentExchangeID);
            PatchExchangeRequestBody body = RequestTestBody.CreatePatchExchangeRequestBody(makerConversion, exchangeRequest, maker.Sender, environment);

            // Execute PATCH ExchangeRequests PATCH ExchangeRequests with an exchange ID that's not found
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/{GetNegativeID(EErrorType.NotFound)}"),
                                     Api.SendRequest(Method.PATCH, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.exchangeRequestNotFound, response);

            // TearDown
            var orderId = quoteResponse.MatchedOrders[0].OrderID;
            if (response.StatusCode != HttpStatusCode.Accepted && !string.IsNullOrEmpty(orderId))
            {
                // Cancel only when an order has been created and a negative exchange requests test
                GluwaTestApi.QAGluwaUser = EUserType.QAExchangeMaker;
                TearDownOrder(orderId);
            }
        }


        [Test]
        [Category("Negative"), Category("InvalidUrlParameters")]
        public void Patch_ExchangeRequests_InvalidUrlParameters_Neg()
        {
            // Create an exchange first
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc in maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Get Amounts
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal sourceAmount, decimal price) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, quoteAmount);

            // Execute exchange request
            var quoteResponse = CreateExchange(conversion, maker, taker, quoteAmount, sourceAmount, price, environment); // Create exchange

            // Arrange
            GetExchangeRequestResponse exchangeRequest = GetMostRecentExchangeRequest(mCurrentExchangeID);
            PatchExchangeRequestBody body = RequestTestBody.CreatePatchExchangeRequestBody(makerConversion, exchangeRequest, maker.Sender, environment);

            // Execute PATCH ExchangeRequests PATCH ExchangeRequests with an invalid ID
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/foobar"),
                                                     Api.SendRequest(Method.PATCH, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("foobar", "ID"), response);

            // TearDown
            var orderId = quoteResponse.MatchedOrders[0].OrderID;
            if (response.StatusCode != HttpStatusCode.Accepted && !string.IsNullOrEmpty(orderId))
            {
                // Cancel only when an order has been created and a negative exchange requests test
                GluwaTestApi.QAGluwaUser = EUserType.QAExchangeMaker;
                TearDownOrder(orderId);
            }
        }


        [Test]
        [Category("Negative"), Category("Conflict")]
        public void Patch_ExchangeRequests_Conflict_Neg()
        {
            // Create an exchange first
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc in maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Get Amounts
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal sourceAmount, decimal price) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, quoteAmount);

            // Execute exchange request
            var quoteResponse = CreateExchange(conversion, maker, taker, quoteAmount, sourceAmount, price, environment); // Create exchange
            
            // Arrange
            GetExchangeRequestResponse exchangeRequest = GetMostRecentExchangeRequest(mCurrentExchangeID);
            PatchExchangeRequestBody body = RequestTestBody.CreatePatchExchangeRequestBody(makerConversion, exchangeRequest, maker.Sender,environment);

            // Execute PATCH ExchangeRequests PATCH ExchangeRequests
            Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/{exchangeRequest.ID}"),
                            Api.SendRequest(Method.PATCH, body));

            // Execute PATCH ExchangeRequests PATCH ExchangeRequests again with the same parameters
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/{exchangeRequest.ID}"),
                                     Api.SendRequest(Method.PATCH, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Conflict, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.exchangeRequestConflict, response);

            // TearDown
            var orderId = quoteResponse.MatchedOrders[0].OrderID;
            if (response.StatusCode != HttpStatusCode.Accepted && !string.IsNullOrEmpty(orderId))
            {
                // Cancel only when an order has been created and a negative exchange requests test
                GluwaTestApi.QAGluwaUser = EUserType.QAExchangeMaker;
                TearDownOrder(orderId);
            }
        }


        [Test]
        [Category("Negative"), Category("Expired")]
        public void Patch_ExchangeRequests_Expired_Neg()
        {
            // Create an exchange first
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc in maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Get Amounts
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal sourceAmount, decimal price) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, quoteAmount);

            // Execute exchange request
            var quoteResponse = CreateExchange(conversion, maker, taker, quoteAmount, sourceAmount, price, environment); // Create exchange

            // Arrange
            GetExchangeRequestResponse exchangeRequest = GetMostRecentExchangeRequest(mCurrentExchangeID);
            PatchExchangeRequestBody body = RequestTestBody.CreatePatchExchangeRequestBody(makerConversion, exchangeRequest, maker.Sender,environment);

            // Wait to expire
            System.Threading.Thread.Sleep(new TimeSpan(0, 6, 0));

            // Execute PATCH ExchangeRequests
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/{exchangeRequest.ID}"),
                                                     Api.SendRequest(Method.PATCH, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Conflict, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.exchangeRequestConflict, response);

            // TearDown
            var orderId = quoteResponse.MatchedOrders[0].OrderID;
            if (response.StatusCode != HttpStatusCode.Accepted && !string.IsNullOrEmpty(orderId))
            {
                // Cancel only when an order has been created and a negative exchange requests test
                GluwaTestApi.QAGluwaUser = EUserType.QAExchangeMaker;
                TearDownOrder(orderId);
            }
        }


        [Test]
        [Category("Negative"), Category("Forbidden")]
        public void Patch_ExchangeRequests_Forbidden_Neg()
        {
            // Create an exchange first
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc in maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Get Amounts
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal sourceAmount, decimal price) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, quoteAmount);

            // Execute exchange request
            var quoteResponse = CreateExchange(conversion, maker, taker, quoteAmount, sourceAmount, price, environment); // Create exchange

            // Arrange
            GetExchangeRequestResponse exchangeRequest = GetMostRecentExchangeRequest(mCurrentExchangeID);
            PatchExchangeRequestBody body = RequestTestBody.CreatePatchExchangeRequestBody(makerConversion, exchangeRequest, maker.Sender,environment);

            // Change user
            GluwaTestApi.QAGluwaUser = EUserType.QAAssertible;

            // Execute PATCH ExchangeRequests
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/{exchangeRequest.ID}"), 
                                                     Api.SendRequest(Method.PATCH, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.exchangeRequestsForbidden, response);

            // TearDown
            var orderId = quoteResponse.MatchedOrders[0].OrderID;
            if (response.StatusCode != HttpStatusCode.Accepted && !string.IsNullOrEmpty(orderId))
            {
                // Cancel only when an order has been created and a negative exchange requests test
                GluwaTestApi.QAGluwaUser = EUserType.QAExchangeMaker;
                TearDownOrder(orderId);
            }
        }
    }
}

