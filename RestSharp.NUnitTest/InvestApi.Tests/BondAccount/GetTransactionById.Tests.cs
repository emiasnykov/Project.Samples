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
    [Category("GetTransactionById"), Category("Get")]
    public class GetTransactionByIdTests : TransferFunctions
    {
        private string environment;

        public GetTransactionByIdTests(string environment)
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
        [Description("Transaction details for a given account, TestCaseId:C4068"), Category("Positive")]
        public void GetTransactionById_Pos()
        {
            // Arrange 
            string Id = GetBondAccountId(environment);
            string TxId = getTransactionById(Id, environment);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{Id}/Transactions/{TxId}"), Api.SendRequest(Method.GET)
                                .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Pretty JSON
            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            //// Assert
            Assert.AreEqual(TxId, (string)data.SelectToken("Id"));
            Assert.That(data.SelectToken("AccountType"), Is.Not.Null);
            Assert.That(data.SelectToken("CreatedDateTime"), Is.Not.Null);
            Assert.That(data.SelectToken("ModifiedDateTime"), Is.Not.Null);
            Assert.That(data.SelectToken("Type"), Is.Not.Null);
            Assert.That(data.SelectToken("Amount"), Is.Not.Null);
            Assert.That(data.SelectToken("TransactionStatus"), Is.Not.Null);
            Assert.That(data.SelectToken("Amount"), Is.Not.Null);
            Assert.That(data.SelectToken("SourceAddress"), Is.Not.Null);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Description("Not found, TestCaseId:4157"), Category("Negative")]
        public void GetTransactionById_NotFound_Neg()
        {
            // Arrange
            string Id = GetBondAccountId(environment);
            string TxId = getTransactionById(Id, environment);
            string modifiedTxId = TxId.Remove(TxId.Length - 1, 1) + "1";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{Id}/Transactions/{modifiedTxId}"), Api.SendRequest(Method.GET)
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
        [Description("Invalid Id, TestCaseId:C4069"), Category("Negative")]
        public void GetTransactionById_InvalidId_Neg()
        {
            //Get Fixed-Term Interest account Id 
            string Id = GetBondAccountId(environment);
            string TxId = getTransactionById(Id, environment);
            string modifiedTxId = TxId.Remove(TxId.Length - 3);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{Id}/Transactions/{modifiedTxId}"), Api.SendRequest(Method.GET)
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
        [Description("Account id of no auth user,TestCaseId:4158"), Category("Negative")]
        public void GetTransactionById_NotAuthUser_Neg()
        {
            // Existed account Id of non authorized user
            string accountId = GetBondAccountId(environment);
            string TxId = getTransactionById(accountId, environment);
            string Id = "E5B6A647-8D17-43DA-84CE-F4EFCDE619D9";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{Id}/Transactions/{TxId}"), Api.SendRequest(Method.GET)
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
