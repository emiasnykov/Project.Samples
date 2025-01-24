using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;
using RestSharp;
using GluwaAPI.TestEngine.Utils;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace LotteryAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    [Category("GetTransactionsByTypePrize"), Category("Get")]
    public class GetTransactionsByTypeTests : TransferFunctions
    {
        private string environment;

        public GetTransactionsByTypeTests(string environment)
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
        public void Get_TransactionsByType_Prize_Pos()
        {
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Prize/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            var items = objects["Items"];

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(objects.SelectToken("TotalCount"), Is.Not.Null, message: "TotalCount");
                Assert.That(items[0].SelectToken("Id"), Is.Not.Null, message: "Items.Id");
                Assert.That(items[0].SelectToken("AccountType"), Is.Not.Null, message: "Items.AccountType");
                Assert.That((string)items[0].SelectToken("AccountType"), Is.EqualTo("Prize"), message: "Items.AccountType");
                Assert.That(items[0].SelectToken("CreatedDateTime"), Is.Not.Null, message: "Items.CreatedDateTime");
                Assert.That(items[0].SelectToken("Type"), Is.Not.Null, message: "Items.Type");
                Assert.That(items[0].SelectToken("Amount"), Is.Not.Null, message: "Items.Amount");
                Assert.That(items[0].SelectToken("TransactionStatus"), Is.Not.Null, message: "Items.TransactionStatus");
            });
        }


        [Test]
        public void Get_TransactionsByType_WithParameters_Prize_Pos()
        {
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Prize/Transactions?limit=2&offset=1"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> Items = objects["Items"].Select(m => (string)m.SelectToken("ID")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual(2, Items.Count);
        }


        [Test]
        public void Get_TransactionsByTxnId_Prize_Pos()
        {
            // Arrange
            string TxnId = getPrizeTransaction(environment);

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V2/Accounts/Prize/Transactions/{TxnId}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(objects.SelectToken("Id"), Is.Not.Null, message: "Id");
                Assert.That(objects.SelectToken("AccountType"), Is.Not.Null, message: "AccountType");
                Assert.That((string)objects.SelectToken("AccountType"), Is.EqualTo("Prize"), message: "AccountType");
                Assert.That(objects.SelectToken("CreatedDateTime"), Is.Not.Null, message: "CreatedDateTime");
                Assert.That(objects.SelectToken("ModifiedDateTime"), Is.Not.Null, message: "ModifiedDateTime");
                Assert.That(objects.SelectToken("MaturityDate"), Is.Empty, message: "MaturityDate");
                Assert.That(objects.SelectToken("Type"), Is.Not.Null, message: "Type");
                Assert.That(objects.SelectToken("Amount"), Is.Not.Null, message: "Amount");
                Assert.That(objects.SelectToken("TransactionStatus"), Is.Not.Null, message: "TransactionStatus");
                Assert.That(objects.SelectToken("TxnHash"), Is.Not.Null, message: "TxnHash");
                Assert.That(objects.SelectToken("SourceAddress"), Is.Not.Null, message: "SourceAddress");
            });
        }


        [Test]
        public void Get_TransactionsByType_ZeroParameters_Prize_Pos()
        {
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Prize/Transactions?limit=0&offset=0"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        public void Get_TransactionsByTxnId_Invalid_Prize_Neg()
        {
            // Arrange
            string TxnId = getPrizeTransaction(environment);
            TxnId = TxnId + "11";

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V2/Accounts/Prize/Transactions/{TxnId}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{TxnId}' is not valid.", response);
        }

        
        [Test]
        public void Get_TransactionsByTxnId_NotFound_Prize_Neg()
        {
            // Arrange
            string TxnId = getPrizeTransaction(environment);
            TxnId = TxnId[0..^4] + "1111"; //Change to valid but inexistent Id

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V2/Accounts/Prize/Transactions/{TxnId}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleAssertionCode("NotFound", response);
        }


        [Test]
        public void Get_TransactionsByType_NegativeLimit_Prize_Neg()
        {
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Prize/Transactions?limit=-1"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The value '-1' is not valid.", response);
        }


        [Test]
        public void Get_TransactionsByType_NegativeOffset_Prize_Neg()
        {
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Prize/Transactions?offset=-1"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The value '-1' is not valid.", response);
        }
    }
}

