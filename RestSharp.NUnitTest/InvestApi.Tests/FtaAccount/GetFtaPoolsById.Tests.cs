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
    public class GetFtaPoolsByIdTests : TransferFunctions
    {
        private string environment;

        public GetFtaPoolsByIdTests(string environment)
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
        public void Get_FtaPool_ById_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{poolId}/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialization
            JToken jsonObj = JToken.Parse(response.Content);
            TestContext.WriteLine("Response: " + jsonObj.ToString());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That((string)jsonObj.SelectToken("ID"), Is.Not.Null, message: "ID");
                Assert.That((string)jsonObj.SelectToken("PoolName"), Is.Not.Null, message: "PoolName");
                Assert.That((string)jsonObj.SelectToken("Tenor"), Is.Not.Null, message: "Tenor");
                Assert.That((string)jsonObj.SelectToken("OpeningDate"), Is.Not.Null, message: "OpeningDate");
                Assert.That((string)jsonObj.SelectToken("ClosingDate"), Is.Not.Null, message: "ClosingDate");
                Assert.That((string)jsonObj.SelectToken("MaturingDate"), Is.Not.Null, message: "MaturingDate");
                Assert.That((string)jsonObj.SelectToken("StartingDate"), Is.Not.Null, message: "StartingDate");
                Assert.That((string)jsonObj.SelectToken("RepaymentDate"), Is.Not.Null, message: "RepaymentDate");
                Assert.That((string)jsonObj.SelectToken("MinimumRaise"), Is.Not.Null, message: "MinimumRaise");
                Assert.That((string)jsonObj.SelectToken("MaximumRaise"), Is.Not.Null, message: "MaximumRaise");
                Assert.That((string)jsonObj.SelectToken("CurrentRaise"), Is.Not.Null, message: "CurrentRaise");
                Assert.That((string)jsonObj.SelectToken("RemainingCapacity"), Is.Not.Null, message: "RemainingCapacity");
                Assert.That((string)jsonObj.SelectToken("LogoUrl"), Is.Not.Null, message: "LogoUrl");
                Assert.That((string)jsonObj.SelectToken("Description"), Is.Not.Null, message: "Description");
                Assert.That((string)jsonObj.SelectToken("APY"), Is.Not.Null, message: "APY");
                Assert.That((string)jsonObj.SelectToken("Balance"), Is.Not.Null, message: "Balance");
                Assert.That((string)jsonObj.SelectToken("TotalWithdrawnAmount"), Is.Not.Null, message: "TotalWithdrawnAmount");
                Assert.That((string)jsonObj.SelectToken("MaturityYield"), Is.Not.Null, message: "MaturityYield");
                Assert.That((string)jsonObj.SelectToken("CurrentYield"), Is.Not.Null, message: "CurrentYield");
                Assert.That((string)jsonObj.SelectToken("ActiveApprovedAmount"), Is.Not.Null, message: "ActiveApprovedAmount");
                Assert.That((string)jsonObj.SelectToken("IsCanceled"), Is.Not.Null, message: "IsCanceled");
                Assert.That((string)jsonObj.SelectToken("IsLocked"), Is.Not.Null, message: "IsLocked");
                Assert.That((string)jsonObj.SelectToken("IsRejected"), Is.Not.Null, message: "IsRejected");
                Assert.That((string)jsonObj.SelectToken("ChainID"), Is.Not.Null, message: "ChainID");
                Assert.That((string)jsonObj.SelectToken("Currency"), Is.Not.Null, message: "Currency");
                Assert.That((string)jsonObj.SelectToken("IsApprovalTransactionPending"), Is.Not.Null, message: "IsApprovalTransactionPending");
                Assert.That((string)jsonObj.SelectToken("IsDepositTransactionPending"), Is.Not.Null, message: "IsDepositTransactionPending");
                Assert.That((string)jsonObj.SelectToken("IsDrawdownTransactionPending"), Is.Not.Null, message: "IsDrawdownTransactionPending");
                Assert.That((string)jsonObj.SelectToken("ExpectedApprovedAmounInterestAtMaturity"), Is.Not.Null, message: "ExpectedApprovedAmounInterestAtMaturity");
                Assert.That((string)jsonObj.SelectToken("PoolState"), Is.Not.Null, message: "PoolState");
            });
        }


        [Test]
        public void Get_FtaPool_NoAccountAddress_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("MainnetSender");     // No Fta account linked to investor address
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{poolId}/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        public void Get_FtaPool_InvalidPoolId_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Invalid poolId
            string poolId = Shared.INVALID_ID;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{poolId}/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{poolId}' is not valid.", response);
        }


        [Test]
        public void Get_FtaPool_NotFoundPoolId_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");       
            string poolId = Shared.NOT_FOUND_ID;  // Non-exist poolId

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{poolId}/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleAssertionMessage($"Could not find PoolID {poolId.ToLower()} in Gluwa", response);
        }


        [Test]
        public void Get_FtaPool_InvalidAddress_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{poolId}/{investor.Address[0..^4]}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        public void Get_FtaPool_UnverifiedAddress_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{poolId}/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("No request signature header was provided.", response);
        }


        [Test]
        public void Get_FtaPool_NoUserAuth_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{poolId}/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
                                            // No User authentification
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }


        [Test]
        public void Get_FtaPool_NotAuthorizedUser_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            var investor2 = QAKeyVault.GetGluwaAddress("MainnetSender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{poolId}/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor2.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }
    }
}
