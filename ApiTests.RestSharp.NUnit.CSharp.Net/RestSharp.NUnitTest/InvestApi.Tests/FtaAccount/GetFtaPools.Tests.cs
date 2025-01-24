using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using GluwaAPI.TestEngine.Utils;
using GluwaAPI.TestEngine.CurrencyUtils;

namespace FtaAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    public class GetFtaPoolsTests : TransferFunctions
    {
        private string environment;
        private string chainId;
        private ECurrency currency;

        public GetFtaPoolsTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            currency = TestSettingsUtil.Currency;
            chainId = Shared.GetChainId(environment);
        }

        [Test]
        public void Get_FtaPools_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialization
            JArray jsonObj = JArray.Parse(response.Content);
            TestContext.WriteLine("Response: " + jsonObj.ToString());

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That((string)jsonObj[0].SelectToken("ID"), Is.Not.Null, message: "ID");
                Assert.That((string)jsonObj[0].SelectToken("PoolName"), Is.Not.Null, message: "PoolName");
                Assert.That((string)jsonObj[0].SelectToken("Tenor"), Is.Not.Null, message: "Tenor");
                Assert.That((string)jsonObj[0].SelectToken("OpeningDate"), Is.Not.Null, message: "OpeningDate");
                Assert.That((string)jsonObj[0].SelectToken("ClosingDate"), Is.Not.Null, message: "ClosingDate");
                Assert.That((string)jsonObj[0].SelectToken("MaturingDate"), Is.Not.Null, message: "MaturingDate");
                Assert.That((string)jsonObj[0].SelectToken("StartingDate"), Is.Not.Null, message: "StartingDate");
                Assert.That((string)jsonObj[0].SelectToken("RepaymentDate"), Is.Not.Null, message: "RepaymentDate");
                Assert.That((string)jsonObj[0].SelectToken("MinimumRaise"), Is.Not.Null, message: "MinimumRaise");
                Assert.That((string)jsonObj[0].SelectToken("MaximumRaise"), Is.Not.Null, message: "MaximumRaise");
                Assert.That((string)jsonObj[0].SelectToken("CurrentRaise"), Is.Not.Null, message: "CurrentRaise");
                Assert.That((string)jsonObj[0].SelectToken("RemainingCapacity"), Is.Not.Null, message: "RemainingCapacity");
                Assert.That((string)jsonObj[0].SelectToken("LogoUrl"), Is.Not.Null, message: "LogoUrl");
                Assert.That((string)jsonObj[0].SelectToken("Description"), Is.Not.Null, message: "Description");
                Assert.That((string)jsonObj[0].SelectToken("APY"), Is.Not.Null, message: "APY");
                Assert.That((string)jsonObj[0].SelectToken("Balance"), Is.Not.Null, message: "Balance");
                Assert.That((string)jsonObj[0].SelectToken("TotalWithdrawnAmount"), Is.Not.Null, message: "TotalWithdrawnAmount");
                Assert.That((string)jsonObj[0].SelectToken("MaturityYield"), Is.Not.Null, message: "MaturityYield");
                Assert.That((string)jsonObj[0].SelectToken("CurrentYield"), Is.Not.Null, message: "CurrentYield");
                Assert.That((string)jsonObj[0].SelectToken("ActiveApprovedAmount"), Is.Not.Null, message: "ActiveApprovedAmount");
                Assert.That((string)jsonObj[0].SelectToken("IsCanceled"), Is.Not.Null, message: "IsCanceled");
                Assert.That((string)jsonObj[0].SelectToken("IsLocked"), Is.Not.Null, message: "IsLocked");
                Assert.That((string)jsonObj[0].SelectToken("IsRejected"), Is.Not.Null, message: "IsRejected");
                Assert.That((string)jsonObj[0].SelectToken("ChainID"), Is.Not.Null, message: "ChainID");
                Assert.That((string)jsonObj[0].SelectToken("Currency"), Is.Not.Null, message: "Currency");
                Assert.That((string)jsonObj[0].SelectToken("IsApprovalTransactionPending"), Is.Not.Null, message: "IsApprovalTransactionPending");
                Assert.That((string)jsonObj[0].SelectToken("IsDepositTransactionPending"), Is.Not.Null, message: "IsDepositTransactionPending");
                Assert.That((string)jsonObj[0].SelectToken("IsDrawdownTransactionPending"), Is.Not.Null, message: "IsDrawdownTransactionPending");
                Assert.That((string)jsonObj[0].SelectToken("ExpectedApprovedAmounInterestAtMaturity"), Is.Not.Null, message: "ExpectedApprovedAmounInterestAtMaturity");
                Assert.That((string)jsonObj[0].SelectToken("PoolState"), Is.Not.Null, message: "PoolState");
            });
        }


        [Test]
        public void Get_FtaPools_With_ValidChainId_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{investor.Address}?ChainID={Shared.CHAIN_ID_GOERLI_TEST}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialization
            JArray jsonObj = JArray.Parse(response.Content);
            dynamic data = JToken.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            // Assert         
            Assert.Multiple(() =>
            {
                Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
                Assert.That((string)data[0].SelectToken("ID"), Is.Not.Null, message: "ID");
                Assert.AreEqual((string)data[0].SelectToken("ChainID"), Shared.CHAIN_ID_GOERLI_TEST, message: "ChainID");
            });
        }


        [Test]
        public void Get_FtaPools_NoAccountAddress_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("MainnetSender");     // No Fta account linked to investor address

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialization
            JArray jsonObj = JArray.Parse(response.Content);
            TestContext.WriteLine("Response: " + jsonObj.ToString());

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        public void Get_FtaPools_InvalidChainId_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{investor.Address}?ChainID={Shared.INVALID_IDEM}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage($"chainId is not a valid", response);
        }


        [Test]
        public void Get_FtaPools_With_NonExistChainId_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{investor.Address}?ChainID=8"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage($"chainId is not supported by Gluwa", response);
        }


        [Test]
        public void Get_FtaPools_NoAuth_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }


        [Test]
        public void Get_FtaPools_UnverifiedUser_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("No request signature header was provided.", response);
        }
    }
}
