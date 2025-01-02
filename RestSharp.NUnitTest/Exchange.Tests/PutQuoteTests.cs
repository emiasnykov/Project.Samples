using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Models.ResponseBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;
using System;
using System.Net;
using System.Threading;

namespace Exchange.Tests
{
    [TestFixture("Test")]
    [TestFixture("Sandbox")]
    [Category("Exchange"), Category("Quote")]
    public class PutQuoteTests : ExchangeFunctions
    {
        private string environment;
        private EConversion conversion;
        private EConversion makerConversion;

        public PutQuoteTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            SetUpCreateExchangeTests(EUserType.QAAssertible, environment);
            conversion = TestSettingsUtil.Conversion;
            makerConversion = conversion.ToReverseConversion();
        }

        [Test]
        [Category("Positive"), Category("BtcsUsdcg")]
        [Description("TestCaseId:C4093")]
        public void Put_Quote_BtcsUsdcg_Pos()
        {
            // Set maker and taker address
            var maker = QAKeyVault.GetGluwacoinBtcExchangeAddress("Sender", "Receiver", environment); // Gluwacoin to Btc as maker
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc taker

            // Create quote
            (PostQuoteResponse quote, string orderId) = CreateOrderAndQuote(maker, taker, conversion, makerConversion);

            // Create accept quote body
            PutQuoteBody body = RequestTestBody.CreatePutQuoteBody(conversion, quote, taker.Sender, environment);

            // Action
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.PUT, body));
            AmountUtils.CheckUnspentOutputs(response.Content, taker.Sender.Address);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);

            // Clean up
            TearDownOrder(orderId);
        }


        [Test]
        [Category("Positive"), Category("sUsdcgBtc")]
        [Description("TestCaseId:C184")]
        public void Put_Quote_sUsdcgBtc_Pos()
        {
            // Set maker and taker address
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Gluwacoin taker

            // Create quote
            (PostQuoteResponse quote, string orderId) = CreateOrderAndQuote(maker, taker, conversion, makerConversion);

            // Create quote
            PutQuoteBody body = RequestTestBody.CreatePutQuoteBody(conversion, quote, taker.Sender, environment);

            // Action
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.PUT, body));
            AmountUtils.CheckUnspentOutputs(response.Content, taker.Sender.Address);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);

            // Clean up
            TearDownOrder(orderId);
        }


        [Test]
        [Category("Negative"), Category("BtcsUsdcg"), Category("Conflict")]
        [Description("TestCaseId:C680")]
        public void Put_Quote_BtcsUsdcg_Conflict_Neg()
        {
            // Set maker and taker address
            var maker = QAKeyVault.GetGluwacoinBtcExchangeAddress("Sender", "Receiver", environment); // Gluwacoin to Btc as maker
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc to Gluwacoin as taker

            // Create quote
            (PostQuoteResponse quote, string orderId) = CreateOrderAndQuote(maker, taker, conversion, makerConversion);

            // Create quote
            PutQuoteBody body = RequestTestBody.CreatePutQuoteBody(conversion, quote, taker.Sender, environment);

            // Accept Quote twice
            Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.PUT, body));
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.PUT, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Conflict, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.acceptQuoteConflict, response);

            // Clean up
            TearDownOrder(orderId);
        }


        [Test]
        [Category("Negative"), Category("sUsdcgBtc"), Category("Conflict")]
        [Description("TestCaseId:C676")]
        public void Put_Quote_sUsdcgBtc_Conflict_Neg()
        {
            // Set maker and taker address
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Gluwacoin taker

            // Create quote
            (PostQuoteResponse quote, string orderId) = CreateOrderAndQuote(maker, taker, conversion, makerConversion);

            // Create quote
            PutQuoteBody body = RequestTestBody.CreatePutQuoteBody(conversion, quote, taker.Sender, environment);

            // Accept Quote twice
            Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.PUT, body));
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.PUT, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Conflict, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.acceptQuoteConflict, response);

            // Clean up
            TearDownOrder(orderId);
        }


        [Test]
        [Category("Negative"), Category("sUsdcgBtc"), Category("MissingBody")]
        [Description("TestCaseId:C4094")]
        public void Put_Quote_sUsdcgBtc_MissingBody_Neg()
        {
            // Execute without body
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.PUT, null));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.missingBody, response);
        }


        [Test]
        [Category("Negative"), Category("sUsdcgBtc"), Category("Forbidden")]
        [Description("TestCaseId:C4095")]
        public void Put_Quote_sUsdcgBtc_InvalidChecksum_Neg()
        {
            // Set maker and taker address
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Gluwacoin taker

            // Create quote
            (PostQuoteResponse quote, string orderId) = CreateOrderAndQuote(maker, taker, conversion, makerConversion);

            // Arrange
            PutQuoteBody body = RequestTestBody.CreatePutQuoteBody(conversion, quote, taker.Sender, environment);
            body.Checksum = "foobar";

            // Action
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.PUT, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.invalidChecksum, response);

            // Clean up
            TearDownOrder(orderId);
        }


        [Test]
        [Category("Negative"), Category("sUsdcgBtc"), Category("Expired")]
        [Description("TestCaseId:C675")]
        public void Put_Quote_sUsdcgBtc_ExpiredQuote_Neg()
        {
            // Set maker and taker address
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Gluwacoin taker

            // Create quote
            (PostQuoteResponse quote, string orderId) = CreateOrderAndQuote(maker, taker, conversion, makerConversion);

            // Arrange
            PutQuoteBody body = RequestTestBody.CreatePutQuoteBody(conversion, quote, taker.Sender, environment);

            // Wait
            int.TryParse(quote.TimeToLive, out int expirySeconds);
            Thread.Sleep(new TimeSpan(0, 0, expirySeconds + 10));

            // Action
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.PUT, body));
            AmountUtils.CheckUnspentOutputs(response.Content, taker.Sender.Address);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.invalidChecksum, response);

            // Clean up
            TearDownOrder(orderId);
        }


        [Test]
        [Category("Negative"), Category("sUsdcgBtc"), Category("NotFound")]
        [Description("TestCaseId:C187")]
        public void Put_Quote_sUsdcgBtc_CancelOrder_Neg()
        {
            // Set maker and taker address
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Gluwacoin taker

            // Create quote
            (PostQuoteResponse quote, string orderId) = CreateOrderAndQuote(maker, taker, conversion, makerConversion);

            // Cancel order
            CancelOrder(orderId);

            // Arrange
            PutQuoteBody body = RequestTestBody.CreatePutQuoteBody(conversion, quote, taker.Sender, environment);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.PUT, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.orderNotAvailable, response);

            // Clean up
            TearDownOrder(orderId);
        }
    }
}