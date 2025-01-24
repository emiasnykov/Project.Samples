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
    [Category("GetAllTransactions"), Category("Get")]
    public class GetAllTransactionsTests : TransferFunctions
    {

        private string environment;

        public GetAllTransactionsTests(string environment)
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
        [Description("Get transaction details for all accounts, TestCaseId:C4067"), Category("Positive")]
        public void GetAllTransactions_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Accounts/Transactions"), Api.SendRequest(Method.GET)
                                .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Pretty JSON
            JArray jsonArray = JArray.Parse(response.Content);
            dynamic data = JObject.Parse(jsonArray[0].ToString());
            TestContext.WriteLine("Response: " + data);

            // Assert
            Assert.That(data.SelectToken("Id"), Is.Not.Null);
            Assert.AreEqual("Bond", (string)data.SelectToken("AccountType"));
            Assert.That(data.SelectToken("CreatedDateTime"), Is.Not.Null);
            Assert.That(data.SelectToken("Type"), Is.Not.Null);
            Assert.That(data.SelectToken("Amount"), Is.Not.Null);
            Assert.That(data.SelectToken("TransactionStatus"), Is.Not.Null);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }
    }
}
