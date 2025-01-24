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
    public class CompleteOrderTests : ExchangeFunctions
    {
        private static EConversion conversion;
        private static EConversion makerConversion;
        private string environment;
        private decimal remainingAmount;

        public CompleteOrderTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up tests and conversion
            SetUpCreateExchangeTests(EUserType.QAExchangeMaker, environment);
            conversion = TestSettingsUtil.Conversion;
            makerConversion = conversion.ToReverseConversion();
        }

        [Test]
        [Category("Positive"), Category("sUsdcgBtc"), Category("OrderComplete")]
        public void CompleteOrder_sUsdcgBtc_Pos()
        {
            // Get most recent exchange request
            string currentExchangeID = GetExchangeRequest().ID;

            // Set maker and taker address
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // sUsdcg in maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Get quote and source amounts
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal sourceAmount, decimal price) = AmountUtils.calculateCompleteOrderSourceAmount(conversion, quoteAmount);

            // Create an Exchange
            var quote = CreateExchange(conversion, maker, taker, quoteAmount, sourceAmount, price, environment);

            // Arrange
            var exchangeRequest = GetMostRecentExchangeRequest(currentExchangeID);
            var body = RequestTestBody.CreatePatchExchangeRequestBody(makerConversion,exchangeRequest, maker.Sender, environment);

            // Execute PATCH ExchangeRequests
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/{exchangeRequest.ID}"), Api.SendRequest(Method.PATCH, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
            Assertions.HandleAssertionOrderComplete(quote, remainingAmount);
        }


        [Test]
        [Category("Positive"), Category("BtcsUsdcg"), Category("OrderComplete")]
        public void CompleteOrder_BtcsUsdcg_Pos()
        {
            // Get most recent exchange request
            string currentExchangeID = GetExchangeRequest().ID;

            // Set maker and taker address
            var maker = QAKeyVault.GetGluwacoinBtcExchangeAddress("Sender", "Receiver", environment); // Btc in maker
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("TakerSender", "TakerReceiver", environment); // sUsdcg in taker

            // Get quote and source amounts
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal sourceAmount, decimal price) = AmountUtils.calculateCompleteOrderSourceAmount(conversion, quoteAmount);

            // Create an Exchange
            var quote = CreateExchange(conversion, maker, taker, quoteAmount, sourceAmount, price, environment);

            // Arrange
            GetExchangeRequestResponse exchangeRequest = GetMostRecentExchangeRequest(currentExchangeID);
            PatchExchangeRequestBody body = RequestTestBody.CreatePatchExchangeRequestBody(makerConversion, exchangeRequest, maker.Sender, environment);

            // Execute PATCH ExchangeRequests
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/ExchangeRequests/{exchangeRequest.ID}"), Api.SendRequest(Method.PATCH, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
            Assertions.HandleAssertionOrderComplete(quote, remainingAmount);
        }
    }
}
