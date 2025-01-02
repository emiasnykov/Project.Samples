using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Models;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Exchange.Tests
{
    [TestFixture("Test")]
    [TestFixture("Sandbox")]
    [Category("Exchanges")]
    public class ExecuteFailTests : ExchangeFunctions
    {

        private EConversion conversion;
        private EConversion makerConversion;

        private string environment;
        public ExecuteFailTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up tests and conversion
            SetUpCreateExchangeTests(EUserType.QAExchangeMaker, environment);
            conversion = TestSettingsUtil.Conversion;
        }

        [Test]
        [Category("Negative"), Category("BtcsUsdcg"), Category("ExecuteFail")]
        public void Create_Exchange_BtcsUsdcg_ExecuteFail_Neg()
        {
            // Get most recent exchange request
            string currentExchangeID = GetExchangeRequest().ID;

            // Set up taker and maker
            var maker = QAKeyVault.GetGluwacoinBtcExchangeAddress("Sender", "Receiver", environment);
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("ExecuteFail", "TakerReceiver", environment);

            // Get amounts
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal sourceAmount, decimal price) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, quoteAmount);

            // Create an Exchange
            CreateExchange(conversion, maker, taker, quoteAmount, sourceAmount, price, environment);

            // Arrange with address that automatically fails upon execute step
            GetExchangeRequestResponse exchangeRequest = GetMostRecentExchangeRequest(currentExchangeID);
            PatchExchangeRequestBody body = RequestTestBody.CreatePatchExchangeRequestBody(makerConversion, exchangeRequest, maker.Sender, environment);

            // Execute PATCH ExchangeRequests
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/{exchangeRequest.ID}"), Api.SendRequest(Method.PATCH, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
            TestContext.WriteLine($"No Response Body to show. Case fails at execute step");
        }


        [Test]
        [Category("Negative"), Category("sUsdcgBtc"), Category("ExecuteFail")]
        public void Create_Exchange_sUsdcgBtc_ExecuteFail_Neg()
        {
            // Get most recent exchange request
            string currentExchangeID = GetExchangeRequest().ID;

            // Set up taker and maker
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment);
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("ExecuteFailUsd", "TakerReceiver", environment);

            // Get amounts
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal sourceAmount, decimal price) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, quoteAmount);

            // Create an Exchange
            CreateExchange(conversion, maker, taker, quoteAmount, sourceAmount, price, environment);

            // Arrange with address that automatically fails upon execute step
            GetExchangeRequestResponse exchangeRequest = GetMostRecentExchangeRequest(currentExchangeID);
            PatchExchangeRequestBody body = RequestTestBody.CreatePatchExchangeRequestBody(makerConversion, exchangeRequest, maker.Sender, environment);

            // Execute PATCH ExchangeRequests
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/{exchangeRequest.ID}"), Api.SendRequest(Method.PATCH, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
            TestContext.WriteLine($"No Response Body to show. Case fails at execute step");
        }
    }
}
