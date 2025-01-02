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
    [Category("Exchange"), Category("Quote"), Category("Post")]
    public class PostQuoteTests : ExchangeFunctions
    {
        private string environment;
        private EConversion conversion;
        private EConversion makerConversion;

        public PostQuoteTests(string environment)
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
        [Description("TestCaseId:C4089")]
        public void Post_Quote_BtcsUsdcg_Pos()
        {
            // Set maker and taker address
            var maker = QAKeyVault.GetGluwacoinBtcExchangeAddress("Sender", "Receiver", environment); // Btc in maker
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Set Up quote amounts, order amount and order price
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal orderAmount, decimal orderPrice) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, quoteAmount);

            // Create Order
            string orderId = TryToCreateOrder(makerConversion, orderAmount, orderPrice, maker);

            // Create body
            var body = RequestTestBody.CreatePostQuoteBody(conversion, quoteAmount, taker.Sender, taker.Receiver); 

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.POST, body));
            PostQuoteResponse responseBody = JsonConvert.DeserializeObject<PostQuoteResponse>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyQuoteResponseAmounts(body.Amount, responseBody);

            // Clean up
            TearDownOrder(orderId);
        }


        [Test]
        [Category("Positive"), Category("sUsdcgBtc")]
        [Description("TestCaseId:C183")]
        public void Post_Quote_sUsdcgBtc_Pos()
        {
            //Set maker and taker address
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("Sender", "Receiver", environment); // Btc in maker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Set Up quote amounts, order amount and order price
            decimal quoteAmount = conversion.ToSourceCurrency().ToDefaultCurrencyAmount();
            (decimal orderAmount, decimal orderPrice) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, quoteAmount);

            string orderId = TryToCreateOrder(makerConversion, orderAmount, orderPrice, maker); //Create Order
            var body = RequestTestBody.CreatePostQuoteBody(conversion, quoteAmount, taker.Sender, taker.Receiver); // Create body

            //Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.POST, body));
            PostQuoteResponse responseBody = JsonConvert.DeserializeObject<PostQuoteResponse>(response.Content);

            //Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyQuoteResponseAmounts(body.Amount, responseBody);

            //Clean up
            TearDownOrder(orderId);
        }


        [Test]
        [Category("Negative"), Category("BtcsUsdcg"), Category("NotFound")]
        [Description("TestCaseId:C186")]
        public void Post_Quote_BtcsUsdcg_NotFound_Neg()
        {
            // Set maker and taker address
            var maker = QAKeyVault.GetGluwacoinBtcExchangeAddress("Sender", "Receiver", environment); // Btc in maker
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Get matched Order amount and price
            (decimal orderAmount, decimal orderPrice) = AmountUtils.GetMatchedOrderAmountAndPrice(conversion, 1m);

            // Create Order
            string orderId = TryToCreateOrder(makerConversion, orderAmount, orderPrice, maker);

            // Create body
            var body = RequestTestBody.CreatePostQuoteBody(conversion, 100000000m, taker.Sender, taker.Receiver); 

            // Act
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.quoteOrderNotFound, response);

            // Clean up
            TearDownOrder(orderId);
        }


        [Test]
        [Category("Negative"), Category("BtcsUsdcg"), Category("AmountTooSmall")]
        [Description("TestCaseId:C185")]
        public void Post_Quote_BtcsUsdcg_AmountTooSmall_Neg()
        {
            // Set maker and taker address
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("TakerSender", "TakerReceiver", environment); // Btc in taker

            // Get amount
            decimal smallQuoteAmount = conversion.ToSourceCurrency().ToAmountTooSmallCurrencyAmount();

            // Create body
            var body = RequestTestBody.CreatePostQuoteBody(conversion, smallQuoteAmount, taker.Sender, taker.Receiver); 

            // Execute POST Quote
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.AmountTooSmallBTC, response);
        }


        [Test]
        [Category("Negative"), Category("MissingBody")]
        [Description("TestCaseId:C685")]
        public void Post_Quote_MissingBody_Neg()
        {
            // Execute POST Quote
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Quote"), Api.SendRequest(Method.POST, null));
            
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.missingBody, response);
        }
    }
}