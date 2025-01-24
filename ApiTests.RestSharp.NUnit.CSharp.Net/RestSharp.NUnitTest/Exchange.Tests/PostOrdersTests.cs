using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Exchange.Tests
{
    [TestFixture("Test")]
    [TestFixture("Sandbox")]
    [Category("Exchange"), Category("Orders"), Category("Post")]
    public class PostOrdersTests : ExchangeFunctions
    {
        private string mOrderID { get; set; }
        private string environment;
        private EConversion conversion;
        private const decimal DEFAULT_PRICE = 1m;
        private decimal sourceAmountFromCurrency;

        public PostOrdersTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            conversion = TestSettingsUtil.Conversion;
            sourceAmountFromCurrency = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
        }

        [Test]
        [Category("Positive"), Category("BtcsUsdcg")]
        [Category("Smoke")]
        [Description("TestCaseId:C1307")]
        public void Post_Orders_BtcsUsdcg_Pos()
        {
            // Arrange 
            var makerAddress = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment);          
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();

            // Create body
            var body = RequestTestBody.CreatePostOrderBody(conversion, quoteAmount, DEFAULT_PRICE, makerAddress);
            
            // Execute POST Orders POST Orders
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Created, response, environment);

            // Clean up
            if (response.StatusCode == HttpStatusCode.Created && response.StatusCode == HttpStatusCode.OK)
            {
                JObject responseBody = JObject.Parse(response.Content);
                mOrderID = responseBody.SelectToken("ID").ToString();
                TearDownOrder(mOrderID);
            }
        }


        [Test]
        [Category("Positive"), Category("sUsdcgBtc")]
        [Category("Smoke")]
        [Description("TestCaseId:C1306")]
        public void Post_Orders_sUsdcgBtc_Pos()
        {
            // Arrange 
            var makerAddress = QAKeyVault.GetGluwacoinBtcExchangeAddress("Sender", "Receiver", environment);
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();

            // Create body
            var body = RequestTestBody.CreatePostOrderBody(conversion, quoteAmount, DEFAULT_PRICE, makerAddress);
            
            // Execute POST Orders POST Orders
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Created, response, environment);

            // Clean up
            if (response.StatusCode == HttpStatusCode.Created && response.StatusCode == HttpStatusCode.OK)
            {
                JObject responseBody = JObject.Parse(response.Content);
                mOrderID = responseBody.SelectToken("ID").ToString();
                TearDownOrder(mOrderID);
            }
        }


        [Test]
        [Category("Negative"), Category("BtcsUsdcg")]
        [Category("Smoke")]
        [Description("TestCaseId:C188")]
        public void Post_Orders_BtcsUsdcg_AmountTooSmall_Neg()
        {
            // Arrange 
            var makerAddress = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment);
            decimal smallAmount = conversion.ToSourceCurrency().ToAmountTooSmallCurrencyAmount();

            // Create body
            var body = RequestTestBody.CreatePostOrderBody(conversion, smallAmount, DEFAULT_PRICE, makerAddress);

            // Execute POST Orders POST Orders with amount < 0.0001 BTC
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.SourceAmountTooSmallBTC, response);

            // Clean up
            if (response.StatusCode == HttpStatusCode.Created && response.StatusCode == HttpStatusCode.OK)
            {
                JObject responseBody = JObject.Parse(response.Content);
                mOrderID = responseBody.SelectToken("ID").ToString();
                TearDownOrder(mOrderID);
            }
        }


        [Test]
        [Category("Negative"), Category("MissingBody")]
        [Description("C4091")]
        public void Post_Orders_MissingBody_Neg()
        {
            // Execute POST Orders without a body
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"), Api.SendRequest(Method.POST, null));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.missingBody, response);

            // Clean up
            if (response.StatusCode == HttpStatusCode.Created && response.StatusCode == HttpStatusCode.OK)
            {
                JObject responseBody = JObject.Parse(response.Content);
                mOrderID = responseBody.SelectToken("ID").ToString();
                TearDownOrder(mOrderID);
            }
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("C164")]
        public void Post_Orders_InvalidBody_Neg()
        {
            // Arrange 
            var makerAddress = QAKeyVault.GetGluwaExchangeAddress("Sender", "Receiver");
            var body = RequestTestBody.CreatePostOrderBody(conversion, sourceAmountFromCurrency, DEFAULT_PRICE, makerAddress);

            // Invalid conversion
            body.Conversion = "foobar";

            // Execute POST Orders with conversion = foobar
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidFieldError("foobar", "Conversion"), response);

            // Clean up
            if (response.StatusCode == HttpStatusCode.Created && response.StatusCode == HttpStatusCode.OK)
            {
                JObject responseBody = JObject.Parse(response.Content);
                mOrderID = responseBody.SelectToken("ID").ToString();
                TearDownOrder(mOrderID);
            }
        }
    }
}