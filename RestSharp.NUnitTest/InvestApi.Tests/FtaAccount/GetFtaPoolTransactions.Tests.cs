using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using GluwaAPI.TestEngine.Utils;

namespace FtaAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    public class GetFtaPoolTransactionsTests : TransferFunctions
    {
        private string environment;

        public GetFtaPoolTransactionsTests(string environment)
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
        public void Get_FtaPoolTransactions_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialization
            JArray data = (JArray)JObject.Parse(response.Content)["Items"];

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That((string)data[0].SelectToken("ID"), Is.Not.Null, message: "ID");
                Assert.That((string)data[0].SelectToken("PoolName"), Is.Not.Null, message: "PoolName");
                Assert.That((string)data[0].SelectToken("Amount"), Is.Not.Null, message: "Amount");
                Assert.That((string)data[0].SelectToken("GasUsed"), Is.Not.Null, message: "GasUsed");
                Assert.That((string)data[0].SelectToken("Currency"), Is.Not.Null, message: "Currency");
                Assert.That((string)data[0].SelectToken("SourceAddress"), Is.Not.Null, message: "SourceAddress");
                Assert.That((string)data[0].SelectToken("Status"), Is.Not.Null, message: "Status");
                Assert.That((string)data[0].SelectToken("CreatedDateTime"), Is.Not.Null, message: "CreatedDateTime");
                Assert.That((string)data[0].SelectToken("ModifiedDateTime"), Is.Not.Null, message: "ModifiedDateTime");
                Assert.That((string)data[0].SelectToken("TransactionType"), Is.Not.Null, message: "TransactionType");
                Assert.That((string)data[0].SelectToken("PoolLogoUrl"), Is.Not.Null, message: "PoolLogoUrl");
                Assert.That((string)data[0].SelectToken("TxnHash"), Is.Not.Null, message: "TxnHash");
            });
        }


        [Test]
        public void Get_FtaPoolTransactions_LimitParam_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");
            string limit = "2";

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions?limit={limit}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialization
            JArray data = (JArray)JObject.Parse(response.Content)["Items"];

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(data.Count, Is.LessThanOrEqualTo(int.Parse(limit)), message: "Total Count");
            Assert.Multiple(() =>
            {
                Assert.That((string)data[0].SelectToken("ID"), Is.Not.Null, message: "ID");
                Assert.That((string)data[0].SelectToken("PoolName"), Is.Not.Null, message: "PoolName");
                Assert.That((string)data[0].SelectToken("Amount"), Is.Not.Null, message: "Amount");
                Assert.That((string)data[0].SelectToken("GasUsed"), Is.Not.Null, message: "GasUsed");
                Assert.That((string)data[0].SelectToken("Currency"), Is.Not.Null, message: "Currency");
                Assert.That((string)data[0].SelectToken("SourceAddress"), Is.Not.Null, message: "SourceAddress");
                Assert.That((string)data[0].SelectToken("Status"), Is.Not.Null, message: "Status");
                Assert.That((string)data[0].SelectToken("CreatedDateTime"), Is.Not.Null, message: "CreatedDateTime");
                Assert.That((string)data[0].SelectToken("ModifiedDateTime"), Is.Not.Null, message: "ModifiedDateTime");
                Assert.That((string)data[0].SelectToken("TransactionType"), Is.Not.Null, message: "TransactionType");
                Assert.That((string)data[0].SelectToken("PoolLogoUrl"), Is.Not.Null, message: "PoolLogoUrl");
                Assert.That((string)data[0].SelectToken("TxnHash"), Is.Not.Null, message: "TxnHash");
            });
        }


        [Test]
        public void Get_FtaPoolTransactions_LimitOffsetParam_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");
            string limit = "2";
            string offset = "2";

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            
            // Deserialize to extract offset txnId
            JArray data = (JArray)JObject.Parse(response.Content)["Items"];
            string offsetId = data[int.Parse(offset)].SelectToken("ID").ToString();

            // Execute with parameters
            var responseParam = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions?limit={limit}&offset={offset}"),
                                                Api.SendRequest(Method.GET)
                                                   .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                                   .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            //Deserialization
            JArray data2 = (JArray)JObject.Parse(responseParam.Content)["Items"];

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(data2.Count, Is.LessThanOrEqualTo(int.Parse(limit)), message: "Total Count");
            Assert.That((string)data2[0].SelectToken("ID").ToString(), Is.EqualTo(offsetId), message: "ID");
        }


        [Test]
        public void Get_FtaPoolTransactions_ZeroLimitOffsetParam_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");
            string limit = "0";
            string offset = "0";

            // Execute with parameters
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions?limit={limit}&offset={offset}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        public void Get_FtaPoolTransactions_NoAccountAddress_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("MainnetSender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            //Decerialize 
            JObject data = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(data.SelectToken("TotalCount").ToString, Is.EqualTo("0"), message: "TotalCount");
        }


        [Test]
        public void Get_FtaPoolTransactions_InvalidPoolId_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = Shared.INVALID_ID; // Invalid poolID

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{poolId}' is not valid.", response);
        }


        [Test]
        public void Get_FtaPoolTransactions_NotFoundPoolId_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = Shared.NOT_FOUND_ID.ToLower(); // Not Found poolId

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleAssertionMessage($"Could not find PoolID {poolId} in Gluwa", response);
        }


        [Test]
        public void Get_FtaPoolTransactions_InvalidAddres_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");
            investor.Address = investor.Address[0..^4];   // Set address as invalid 

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        public void Get_FtaPoolTransactions_NoAuth_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
                                              // No authentification
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage($"Client does not have permission to access this service.", response);
        }


        [Test]
        public void Get_FtaPoolTransactions_Unverified_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            //Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
                                              // No authorization
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage($"No request signature header was provided.", response);
        }
    }
}
