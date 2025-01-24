using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;

namespace FtaAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    public class GetFtaPoolTransactionsByIdTests : TransferFunctions
    {
        private string environment;

        public GetFtaPoolTransactionsByIdTests(string environment)
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
        public void Get_PoolTransaction_ById_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");
            string txId = GetFTATransactionId(investor, poolId, environment);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions/{txId}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialization
            JToken jsonObj = JToken.Parse(response.Content);
            TestContext.WriteLine("Response: " + jsonObj);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That((string)jsonObj.SelectToken("ID"), Is.Not.Null, message: "ID");
                Assert.That((string)jsonObj.SelectToken("ID"), Is.EqualTo(txId.ToLower()), message: "ID");
                Assert.That((string)jsonObj.SelectToken("PoolName"), Is.Not.Null, message: "PoolName");
                Assert.That((string)jsonObj.SelectToken("Amount"), Is.Not.Null, message: "Amount");
                Assert.That((string)jsonObj.SelectToken("GasUsed"), Is.Not.Null, message: "GasUsed");
                Assert.That((string)jsonObj.SelectToken("Currency"), Is.Not.Null, message: "Currency");
                Assert.That((string)jsonObj.SelectToken("SourceAddress"), Is.Not.Null, message: "SourceAddress");
                Assert.That((string)jsonObj.SelectToken("Status"), Is.Not.Null, message: "Status");
                Assert.That((string)jsonObj.SelectToken("CreatedDateTime"), Is.Not.Null, message: "CreatedDateTime");
                Assert.That((string)jsonObj.SelectToken("ModifiedDateTime"), Is.Not.Null, message: "ModifiedDateTime");
                Assert.That((string)jsonObj.SelectToken("TransactionType"), Is.Not.Null, message: "TransactionType");
                Assert.That((string)jsonObj.SelectToken("PoolLogoUrl"), Is.Not.Null, message: "PoolLogoUrl");
                Assert.That((string)jsonObj.SelectToken("TxnHash"), Is.Not.Null, message: "TxnHash");
            });
        }


        [Test]
        public void Get_PoolTransaction_ById_InvalidPoolId_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");
            string txId = GetFTATransactionId(investor, poolId, environment);            
            poolId = poolId[0..^3];    // Set poolId as invalid

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions/{txId}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{poolId}' is not valid.", response);
        }


        [Test]
        public void Get_PoolTransaction_ById_NotFoundPoolId_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = Shared.NOT_FOUND_ID.ToLower();
            string txId = Shared.NOT_FOUND_TXNID; // Dummy txId

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions/{txId}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleAssertionMessage($"Could not find PoolID {poolId} in Gluwa", response);
        }


        [Test]
        public void Get_PoolTransaction_ById_InvalidAddres_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");
            string txId = GetFTATransactionId(investor, poolId, environment);           
            investor.Address = Shared.INVALID_ID;     // Set address as invalid

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions/{txId}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        public void Get_PoolTransaction_ById_NoAccountAddress_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            var investor2 = QAKeyVault.GetGluwaAddress("MainnetSender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");
            string txId = GetFTATransactionId(investor, poolId, environment);
            investor.Address = investor2.Address;                            

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions/{txId}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        public void Get_PoolTransaction_ById_InvalidTxnId_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");
            string txId = GetFTATransactionId(investor, poolId, environment)[0..^3];   // Invalid txnId

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions/{txId}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{txId}' is not valid.", response);
        }


        [Test]
        public void Get_PoolTransaction_ById_NotFoundTxnId_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");
            string txId = Shared.NOT_FOUND_TXNID.ToLower();

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions/{txId}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleAssertionMessage($"Could not find TransactionId {txId} in Gluwa", response);
        }


        [Test]
        public void Get_PoolTransaction_ById_NoAuth_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");
            string txId = GetFTATransactionId(investor, poolId, environment);

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions/{txId}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
                                              // No authentification
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage($"Client does not have permission to access this service.", response);
        }


        [Test]
        public void Get_PoolTransaction_ById_Unverified_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");
            string txId = GetFTATransactionId(investor, poolId, environment);

            //Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions/{txId}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
                                              // No user verification
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage($"No request signature header was provided.", response);
        }
    }
}
