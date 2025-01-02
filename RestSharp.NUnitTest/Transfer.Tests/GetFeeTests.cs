using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Models.ResponseBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Transfer.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    [Category("Fee"), Category("Get")]
    public class GetFeeTests
    {
        private string environment;

        public GetFeeTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
        }

        [Test]
        [Category("Positive"), Category("Btc")]
        public void Get_Fee_Btc_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{TestSettingsUtil.Currency}/Fee"),
                                                     Api.SendRequest(Method.GET));
            // Get fee
            GetFeeResponse feeItem = JsonConvert.DeserializeObject<GetFeeResponse>(response.Content);

            // Assert status code and minimum fee for currency
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), feeItem.Currency, feeItem.MinimumFee);

            // Assert fee is not 0
            Assert.AreNotEqual(0m, decimal.Parse(feeItem.MinimumFee), $"Fee is 0 for {TestSettingsUtil.Currency}");
        }


        [Test]
        [Category("Positive"), Category("sUsdcg")]
        public void Get_Fee_sUsdcg_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{TestSettingsUtil.Currency}/Fee?amount=1"),
                                                     Api.SendRequest(Method.GET));
            // Get fee
            GetFeeResponse feeItem = JsonConvert.DeserializeObject<GetFeeResponse>(response.Content);

            // Assert status code and minimum fee for currency
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), feeItem.Currency, feeItem.MinimumFee);

            // Assert fee is not 0
            Assert.AreEqual(0.001m, decimal.Parse(feeItem.MinimumFee), $"Fee is 0.1% for {TestSettingsUtil.Currency}");
        }

        //// Service temporarily unavailable
        //[Test]
        //[Category("Positive"), Category("sSgDg")]
        //public void Get_Fee_sSgDg_Pos()
        //{
        //    // Execute client
        //    IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{TestSettingsUtil.Currency}/Fee"),
        //                                             Api.SendRequest(Method.GET));
        //    // Get fee
        //    GetFeeResponse feeItem = JsonConvert.DeserializeObject<GetFeeResponse>(response.Content);

        [Test]
        [Category("Positive"), Category("sSgDg")]
        public void Get_Fee_sSgDg_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{TestSettingsUtil.Currency}/Fee"),
                                                     Api.SendRequest(Method.GET));
            // Get fee
            GetFeeResponse feeItem = JsonConvert.DeserializeObject<GetFeeResponse>(response.Content);

            // Assert status code and minimum fee for currency
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), feeItem.Currency, feeItem.MinimumFee);
        }


        [Test]
        [Category("Positive"), Category("sUsdcg")]
        public void Get_Fee_sUsdcg_ZeroAmount_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{TestSettingsUtil.Currency}/Fee?amount=0"),
                                                     Api.SendRequest(Method.GET));
            // Assert status code and fee and currency
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            GetFeeResponse feeItem = JsonConvert.DeserializeObject<GetFeeResponse>(response.Content);
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), feeItem.Currency, feeItem.MinimumFee);
        }


        [Test]
        [Category("Negative"), Category("sUsdcg")]
        public void Get_Fee_sUsdcg_NegativeAmount_Neg()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{TestSettingsUtil.Currency}/Fee?amount=-1"),
                                                     Api.SendRequest(Method.GET));
            // Assert status code and fee and currency
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("amount should be a positive number", response);
        }


        [Test]
        [Category("Positive"), Category("Usdcg")]
        public void Get_Fee_Usdcg_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{TestSettingsUtil.Currency}/Fee"),
                                                     Api.SendRequest(Method.GET));
            // Get fee
            GetFeeResponse feeItem = JsonConvert.DeserializeObject<GetFeeResponse>(response.Content);

            // Assert status code and fee and currency
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), feeItem.Currency, feeItem.MinimumFee);

            // Assert fee is not 0
            Assert.AreNotEqual(0m, decimal.Parse(feeItem.MinimumFee), $"Fee is 0 for {TestSettingsUtil.Currency}");
        }


        [Test]
        [Category("Positive"), Category("Gcre")]
        public void Get_Fee_Gcre_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{TestSettingsUtil.Currency}/Fee"),
                                                     Api.SendRequest(Method.GET));
            // Get fee
            GetFeeResponse feeItem = JsonConvert.DeserializeObject<GetFeeResponse>(response.Content);

            // Assert status code and fee and currency
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), feeItem.Currency, feeItem.MinimumFee);

            // Assert fee is not 0
            Assert.AreEqual(0m, decimal.Parse(feeItem.MinimumFee), $"Fee is 0 for {TestSettingsUtil.Currency}");
        }


        [Test]
        [Category("Positive"), Category("Gate")]
        public void Get_Fee_Gate_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{TestSettingsUtil.Currency}/Fee"),
                                                     Api.SendRequest(Method.GET));
            // Get fee
            GetFeeResponse feeItem = JsonConvert.DeserializeObject<GetFeeResponse>(response.Content);

            // Assert status code and fee and currency
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), feeItem.Currency, feeItem.MinimumFee);

            // Assert fee is not 0
            Assert.AreEqual(0m, decimal.Parse(feeItem.MinimumFee), $"Fee is 0 for {TestSettingsUtil.Currency}");
        }


        [Test]
        [Category("Positive"), Category("NgNg")]
        public void Get_Fee_NgNg_Pos()
        {
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{TestSettingsUtil.Currency}/Fee"),
                                                       Api.SendRequest(Method.GET));
            // Get fee
            GetFeeResponse feeItem = JsonConvert.DeserializeObject<GetFeeResponse>(response.Content);

            // Assert Status Code and Fee currency
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), feeItem.Currency, feeItem.MinimumFee);

            // Assert fee is not 0
            Assert.AreNotEqual(0m, decimal.Parse(feeItem.MinimumFee), $"Fee is 0 for {TestSettingsUtil.Currency}");
        }


        [Test]
        [Category("Positive"), Category("sNgNg")]
        public void Get_Fee_sNgNg_Pos()
        {
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{TestSettingsUtil.Currency}/Fee"),
                                                       Api.SendRequest(Method.GET));
            // Get fee
            GetFeeResponse feeItem = JsonConvert.DeserializeObject<GetFeeResponse>(response.Content);

            // Assert Status Code and Fee currency
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), feeItem.Currency, feeItem.MinimumFee);

            // Assert
            Assert.AreEqual(0m, decimal.Parse(feeItem.MinimumFee), "Fee is 0 for sNGNG");
        }


        [Test]
        [Category("Positive"), Category("Fee Info")]
        public void Get_FeeInfo_Pos()
        {
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/GetFeeInfo"),
                                                     Api.SendRequest(Method.GET));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert status code and rate/fee amount per token
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects.SelectToken("BTC.MinimumAmount").ToString(), Is.Not.Null);
            Assert.That(objects.SelectToken("USDCG.MinimumAmount").ToString(), Is.Not.Null);
            Assert.That(objects.SelectToken("NGNG.MinimumAmount").ToString(), Is.Not.Null);
            Assert.AreEqual(objects.SelectToken("sUSDCG.FeeRate").ToString(), "0.001");
            Assert.That(objects.SelectToken("sNGNG.MinimumAmount").ToString(), Is.Not.Null);
            Assert.That(objects.SelectToken("GCRE.MinimumAmount").ToString(), Is.Not.Null);
        }


        [Test]
        [Category("Negative"), Category("InvalidValue")]
        public void Get_Fee_InvalidCurrency_Neg()
        {
            // Execute client with invalid currency
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl("v1/foobar/Fee"), Api.SendRequest(Method.GET));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("foobar", "currency"), response);
        }


        [Test]
        [Category("Negative"), Category("Unsupported currency")]
        public void Get_Fee_Usdg_UnsupportedCurrency_Neg()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/USDG/Fee"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.ServiceUnavailable, response, environment);
            Assertions.HandleAssertionMessage("Unsupported currency", response);
        }


        [Test]
        [Category("Negative"), Category("Unsupported currency")]
        public void Get_Fee_Krwg_UnsupportedCurrency_Neg()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KRWG/Fee"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.ServiceUnavailable, response, environment);
            Assertions.HandleAssertionMessage("Unsupported currency", response);
        }
    }
}
