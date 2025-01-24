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
    public class GetFtaAccountsByIdTests
    {
        private string environment;
        private string keyName;

        public GetFtaAccountsByIdTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            keyName = GluwaTestApi.SetUpGluwaKeyName(environment);
        }

        [Test]
        public void Get_FtaAccount_ById_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/Fta/Summary/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialization
            JObject jsonObj = JObject.Parse(response.Content);
            TestContext.WriteLine("Response: " + jsonObj.ToString());

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That((string)jsonObj.SelectToken("Id"), Is.Not.Null, message: "Id");
                Assert.That((string)jsonObj.SelectToken("Balance"), Is.Not.Null, message: "Balance");
                Assert.That((string)jsonObj.SelectToken("WithdrawableBalance"), Is.Not.Null, message: "WithdrawableBalance");
                Assert.That((string)jsonObj.SelectToken("Status"), Is.Not.Null, message: "Status");
                Assert.That((string)jsonObj.SelectToken("Currency"), Is.Not.Null, message: "Currency");
                Assert.That((string)jsonObj.SelectToken("TotalExpectedInterest"), Is.Not.Null, message: "TotalExpectedInterest");
                Assert.That((string)jsonObj.SelectToken("AverageYield"), Is.Not.Null, message: "AverageYield");
            });
        }


        [Test]
        public void Get_FtaAccount_InvalidAddress_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/Fta/Summary/{investor.Address[0..^5]}"), // Invalid address
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        public void Get_FtaAccount_NotFoundInvestor_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("MainnetSender"); // No Fta account for address

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/Fta/Summary/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assert.IsTrue(response.Content.Contains($"No FtaAccount exists for user with address"));
        }


        [Test]
        public void Get_FtaAccount_NoAuth_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/Fta/Summary/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
                                              // No Authorization
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }


        [Test]
        public void Get_FtaAccount_UnVerifiedAddress_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/Fta/Summary/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
                                              // No Address verification
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("No request signature header was provided.", response);
        }


        [Test]
        [Category("Positive")]
        public void Get_FtaAccount_NotAuthorizedUser_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            var investor2 = QAKeyVault.GetGluwaAddress("MainnetSender");

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/Fta/Summary/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor2.PrivateKey))); // Investor not an address owner
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }
    }
}

