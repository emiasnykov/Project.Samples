using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using GluwaAPI.TestEngine.Utils;
using GluwaAPI.TestEngine.CurrencyUtils;

namespace FtaAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    public class GetFtaFee
    {
        private string environment;
        private ECurrency currency;
        private string keyName;

        public GetFtaFee(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            keyName = GluwaTestApi.SetUpGluwaKeyName(environment);
            currency = TestSettingsUtil.Currency;
        }

        [Test]
        public void Get_FtaFee_Usdc_Pos()
        {
            // Arrange
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaInvestment/Fees?currency={currency}"),
                                           Api.SendRequest(Method.GET));
            // Deserialization
            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That((string)data.SelectToken("GasPrice"), Is.Not.Null, message: "GasPrice");
                Assert.That((string)data.SelectToken("CreateAccountGasLimit"), Is.Not.Null, message: "CreateFtaAccountLimit");
                Assert.That((string)data.SelectToken("CreateBalanceGasLimit"), Is.Not.Null, message: "CreateBalanceGasLimit");
                Assert.That((string)data.SelectToken("DrawdownGasLimit"), Is.Not.Null, message: "DrawdownGasLimit");
            });
        }


        [Test]
        public void Get_FtaFee_UnsupportedCurrency_Usdt_Neg()
        {
            // Arrange
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaInvestment/Fees?currency={currency}"),
                                           Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Unsupported FTA currency", response);
        }


        [Test]
        public void Get_FtaFee_WithoutCurrency_Usdt_Neg()
        {
            // Arrange
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaInvestment/Fees"),
                                           Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Unsupported currency", response);
        }
    }
}
