using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Exchange.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    public class GetExchangeRatesTests
    {
        private string environment;

        public GetExchangeRatesTests(string environment)
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
        [Category("Positive"), Category("Usd")]
        [Description("TestCaseId:C4077")]
        public void Get_ExchangeRates_Pos()
        {
            // Execute 
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/ExchangeRates/USD"),
                                                     Api.SendRequest(Method.GET));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects.SelectToken("RatesToTarget.NGN-G").ToString(), Is.Not.Null);
            Assert.That(objects.SelectToken("RatesToTarget.CTC").ToString(), Is.Not.Null);
            Assert.That(objects.SelectToken("RatesToTarget.BTC").ToString(), Is.Not.Null);
            Assert.That(objects.SelectToken("RatesToTarget.ETH").ToString(), Is.Not.Null);
        }


        [Test]
        [Category("Negative"), Category("Usdcg")]
        [Description("TestCaseId:C4078")]
        public void Get_ExchangeRates_NotFound_Pos()
        {
            // Execute 
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/ExchangeRates/USDСG"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
        }
    }
}
