using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Exchange.Tests
{
    [TestFixture("Test")] 
    [TestFixture("Sandbox")]
    [Category("Exchange"), Category("Address"), Category("Get")]
    public class GetAddressesTests : ExchangeFunctions
    {

        private EConversion conversion;
        private string environment;

        public GetAddressesTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set User
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            conversion = TestSettingsUtil.Conversion;

            // Write test name
            string testName = TestContext.CurrentContext.Test.Name;
            TestContext.Out.WriteLine($"Running Test: {testName}");
        }

        [Test]
        [Category("Quote"), Category("Positive")]
        public void Get_AddressesQuote_BtcsUsdcg_Pos()
        {
            // Arrange
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("TakerSender", "TakerReceiver", environment);
            string currency = conversion.ToSourceCurrency().ToString();

            // Execute GET Address endpoint to get quotes based on the currency
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{taker.Sender.Address}/Quotes"),
                                     Api.SendRequest(Method.GET).AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(taker.Sender.PrivateKey))
                                    .AddQueryParameter("Status", "Processed"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyQuotesInlist(response);
        }


        [Test]
        [Category("Negative")]
        public void Get_AddressesQuote_MissingSignature_Neg()
        {
            // Arrange
            var taker = QAKeyVault.GetGluwaExchangeAddress("TakerSender", "TakerReceiver");
            string currency = conversion.ToSourceCurrency().ToString();

            // Execute GET Address endpoint to get quotes based on the currency but signature is missing
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{taker.Sender.Address}/Quotes"),
                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.missingSignature, response);
        }


        [Test]
        [Category("Negative")]
        public void Get_AddressesQuote_InvalidUrlParameters_Neg()
        {
            // Arrange
            var taker = QAKeyVault.GetGluwaExchangeAddress("TakerSender", "TakerReceiver");
            string currency = conversion.ToSourceCurrency().ToString();

            // Execute GET Address endpoint to get quotes based on the currency but with status = foobar
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{taker.Sender.Address}/Quotes"),
                                     Api.SendRequest(Method.GET)
                                    .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(taker.Sender.PrivateKey))
                                    .AddQueryParameter("Status", "foobar"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("foobar", "status"), response);
        }


        [Test]
        [Category("Negative")]
        public void Get_AddressesQuote_InvalidSignature_Neg()
        {
            // Arrange
            var taker = QAKeyVault.GetGluwaExchangeAddress("TakerSender", "TakerReceiver");
            string currency = conversion.ToSourceCurrency().ToString();

            // Execute GET Address endpoint to get quotes based on the currency but signature = foobar
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{taker.Sender.Address}/Quotes"),
                                     Api.SendRequest(Method.GET)
                                    .AddHeader("X-REQUEST-SIGNATURE", "foobar"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.invalidBase64Format, response);
        }


        [Test]
        [Category("Get"), Category("Negative")]
        public void Get_AddressesQuote_ExpiredSignature_Neg()
        {
            // Arrange
            var taker = QAKeyVault.GetGluwaExchangeAddress("TakerSender", "TakerReceiver");
            string currency = conversion.ToSourceCurrency().ToString();

            // Execute GET Address endpoint to get quotes based on the currency but signature is expired
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{taker.Sender.Address}/Quotes"),
                                     Api.SendRequest(Method.GET)
                                    .AddHeader("X-REQUEST-SIGNATURE", conversion.ToSourceCurrency().ToExpiredXRequestSignature()));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.expiredSignature, response);
        }
    }
}