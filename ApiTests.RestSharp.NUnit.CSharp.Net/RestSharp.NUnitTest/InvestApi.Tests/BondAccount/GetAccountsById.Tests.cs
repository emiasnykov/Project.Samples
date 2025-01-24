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
    [Category("GetAccounts"), Category("Get")]
    public class GetAccountsByIdTests : TransferFunctions
    {
        private string environment;

        public GetAccountsByIdTests(string environment)
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
        [Description("Get details about user investment accounts, TestCaseId:C4065"), Category("Positive")]
        public void GetAccountById_Pos()
        {
            //Get Fixed-Term Interest account Id 
            string Id = GetBondAccountId(environment);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{Id}"), Api.SendRequest(Method.GET)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Pretty JSON
            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            // Assert
            Assert.AreEqual(Id, (string)data.SelectToken("Id"));
            Assert.That(data.SelectToken("Type"), Is.Not.Null);
            Assert.That(data.SelectToken("Balance"), Is.Not.Null);
            Assert.That(data.SelectToken("WithdrawableBalance"), Is.Not.Null);
            Assert.That(data.SelectToken("Status"), Is.Not.Null);
            Assert.That(data.SelectToken("InterestAccrued"), Is.Not.Null);
            Assert.That(data.SelectToken("EffectiveApy"), Is.Not.Null);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Description("Not found, TestCaseId:C4155"), Category("Negative")]
        public void GetAccountById_NotFound_Neg()
        {
            //Get Fixed-Term Interest account Id 
            string Id = GetBondAccountId(environment);
            string modifiedId = Id.Remove(Id.Length - 1, 1) + "1";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{modifiedId}"), Api.SendRequest(Method.GET)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Pretty JSON
            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleAssertionCode("NotFound", response);
        }


        [Test]
        [Description("Invalid Id, TestCaseId:C4065"), Category("Negative")]
        public void GetAccountById_InvalidId_Neg()
        {
            //Get Fixed-Term Interest account Id 
            string Id = GetBondAccountId(environment);
            string modifiedId = Id.Remove(Id.Length - 3);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{modifiedId}"), Api.SendRequest(Method.GET)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Pretty JSON
            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
        }


        [Test]
        [Description("Account id of no auth user, TestCaseId:C4156"), Category("Negative")]
        public void GetAccountById_NotAuthUser_Neg()
        {
            // Existed account Id of non authorized user
            string Id = "E5B6A647-8D17-43DA-84CE-F4EFCDE619D9";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{Id}"), Api.SendRequest(Method.GET)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Pretty JSON
            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleAssertionCode("NotFound", response);
        }
    }
}
