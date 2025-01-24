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
    [Category("GetTransactions"), Category("Get")]
    public class GetTransactionsTests : TransferFunctions
    {
        private string environment;

        public GetTransactionsTests(string environment)
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
        [Description("Get transaction history for a given account, TestCaseId:C4070"), Category("Positive")]
        public void GetTransactions_Pos()
        {
            // Arrange 
            string Id = GetBondAccountId(environment);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{Id}/Transactions"), 
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Pretty JSON
            JArray jsonArray = JArray.Parse(response.Content);
            dynamic data = JObject.Parse(jsonArray[0].ToString());
            TestContext.WriteLine("Response: " + data);

            // Assert
            Assert.That(data.SelectToken("Id"), Is.Not.Null);
            Assert.That(data.SelectToken("AccountType"), Is.Not.Null);
            Assert.That(data.SelectToken("CreatedDateTime"), Is.Not.Null);
            Assert.That(data.SelectToken("Type"), Is.Not.Null);
            Assert.That(data.SelectToken("Amount"), Is.Not.Null);
            Assert.That(data.SelectToken("TransactionStatus"), Is.Not.Null);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Description("Not found account id, TestCaseId:C4159"), Category("Negative")]
        public void GetTransactions_NotFound_Neg()
        {
            // Arrange
            string Id = GetBondAccountId(environment);
            string modifiedId = Id.Remove(Id.Length - 1, 1) + "1";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{modifiedId}/Transactions"), Api.SendRequest(Method.GET)
                                .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Pretty JSON
            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            //Assertions.HandleAssertionCode("NotFound", response);
        }


        [Test]
        [Description("Invalid account id, TestCaseId:C4071"), Category("Negative")]
        public void GetTransactions_InvalidId_Neg()
        {
            // Arrange 
            string Id = GetBondAccountId(environment);
            string modifiedId = Id.Remove(Id.Length - 3);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{modifiedId}/Transactions"), 
                                           Api.SendRequest(Method.GET)
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
        [Description("Account id of no auth user, TestCaseId:4160"), Category("Negative")]
        public void GetTransactions_NotAuthUser_Neg()
        {
            // Arrange
            // Existed account Id of non authorized user
            string Id = "E5B6A647-8D17-43DA-84CE-F4EFCDE619D9";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{Id}/Transactions"), Api.SendRequest(Method.GET)
                                .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Pretty JSON
            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            //Assertions.HandleAssertionCode("NotFound", response);
        }
    }
}
