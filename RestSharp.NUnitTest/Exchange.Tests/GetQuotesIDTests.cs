using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Models.ResponseBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;
using System.Net;

namespace Exchange.Tests
{
    [TestFixture("Test")]
    [TestFixture("Sandbox")]
    [Category("Exchange"), Category("Quote"), Category("Get")]
    public class GetQuotesTests : ExchangeFunctions
    {
        private EConversion conversion;
        private string environment;

        public GetQuotesTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user, conversion and address
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            conversion = TestSettingsUtil.Conversion;
        }

        [Test]
        [Category("Positive"), Category("BtcsUsdcg")]
        [Description("TestCaseId:C4083")]
        public void Get_QuotesID_BtcsUsdcg_ID_Pos()
        {
            // Arrange address
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("TakerSender", "TakerReceiver", environment);

            // Execute Quotes endpoint
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Quotes/{GetQuotesID(conversion)}"),
                                     Api.SendRequest(Method.GET)
                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(taker.Sender.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            GetQuoteByIDResponse quote = JsonConvert.DeserializeObject<GetQuoteByIDResponse>(response.Content);
            Assertions.HandleConversionAssertions(conversion, quote);
        }


        [Test]
        [Category("Positive"), Category("sUsdcgBtc")]
        [Description("TestCaseId:C182")]
        public void Get_QuotesID_sUsdcgBtc_Pos()
        {
            // Arrange address
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment);

            // Execute Quotes endpoint
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Quotes/{GetQuotesID(conversion)}"),
                                     Api.SendRequest(Method.GET)
                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(taker.Sender.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            GetQuoteByIDResponse quote = JsonConvert.DeserializeObject<GetQuoteByIDResponse>(response.Content);
            Assertions.HandleConversionAssertions(conversion, quote);
        }
        

        [Test]
        [Category("Negative")]
        [Description("TestCaseId:C4086")]
        public void Get_QuotesID_InvalidUrlParameters_Neg()
        {
            // Arrange address
            var taker = QAKeyVault.GetGluwaExchangeAddress("TakerSender", "TakerReceiver");

            // Execute Quotes endpoint with ID = foobar
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quotes/foobar"),
                                     Api.SendRequest(Method.GET)
                                        .AddQueryParameter("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(taker.Sender.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("foobar", "ID"), response);
        }


        [Test]
        [Category("Negative")]
        [Description("TestCaseId:C4087")]
        public void Get_QuotesID_SignatureMissing_Neg()
        {
            // Execute Quotes endpoint without X-REQUEST-SIGNATURE
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Quotes/{GetQuotesID(conversion)}"), 
                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.missingSignature, response);
        }


        [Test]
        [Category("Negative")]
        [Description("TestCaseId:C4085")]
        public void Get_QuotesID_InvalidSignature_Neg()
        {
            // Execute Quotes endpoint with invalid signature
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Quotes/{GetQuotesID(conversion)}"),
                                     Api.SendRequest(Method.GET)
                                        .AddHeader("X-REQUEST-SIGNATURE", "foobar"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.invalidBase64Format, response);
        }


        [Test]
        [Category("Negative"), Category("sUsdcgBtc")]
        [Description("TestCaseId:C4084")]
        public void Get_QuotesID_sUsdcgBtc_Forbidden_Neg()
        {
            // Arrange address
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment);

            // Get quotes ID
            string quoteID = GetQuotesID(conversion);
            string privateKey = taker.Receiver.PrivateKey;

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Quotes/{quoteID}"),
                                     Api.SendRequest(Method.GET)
                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(privateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.forbiddenSignature, response);
        }


        [Test]
        [Category("Negative"), Category("BtcsUsdcg")]
        [Description("TestCaseId:C4082")]
        public void Get_QuotesID_BtcsUsdcg_Forbidden_Neg()
        {
            // Arrange address
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("TakerSender", "TakerReceiver", environment);

            // Get Quote ID with the Sender address
            string quoteID = GetQuotesID(conversion);

            // Get the receiver private key
            string privateKey = taker.Receiver.PrivateKey;

            // Execute Quotes endpoint
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Quotes/{quoteID}"),
                                     Api.SendRequest(Method.GET)
                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(privateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.forbiddenSignature, response);
        }
    }
}