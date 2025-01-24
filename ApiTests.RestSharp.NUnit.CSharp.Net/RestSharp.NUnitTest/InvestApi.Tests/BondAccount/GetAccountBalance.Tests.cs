using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace BondAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    [Category("GetAvailableBalance"), Category("Get")]
    public class GetAccountBalanceTests : TransferFunctions
    {
        private string environment;

        public GetAccountBalanceTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            //Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
        }


        [Test]
        [Description("Return amount remaining in Fixed-Term Interest Account,TestCaseId:C4062"), Category("Positive")]

        public void GetAvailableBalance_Pos()
        {

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/BondAccount/AvailableBalance"), Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Pretty JSON
            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            //// Assert
            Assert.That(data.SelectToken("Balance"), Is.Not.Null);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }
    }
}
