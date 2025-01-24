using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using GluwaAPI.TestEngine.Utils;
using System.Linq;
using System.Collections.Generic;

namespace FtaAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    public class GetFtaAccountsTests
    {
        private string environment;
        private string keyName;

        public GetFtaAccountsTests(string environment)
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
        [Category("Positive")]
        public void Get_FtaAccounts_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Summary/IncludeFta/{investor.Address}"), // V2 route to Get all investment accounts
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialization
            JArray accounts = (JArray)JObject.Parse(response.Content)["Accounts"]; // Get all invest accounts
            TestContext.WriteLine("Response: " + response.Content);
            JToken ftaAccountDetails = accounts.Where(x => x.Value<string>("Type") == "Fta").First(); // Get FTA account from list

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That((string)ftaAccountDetails.SelectToken("ID"), Is.Not.Null, message: "ID");
                Assert.That((string)ftaAccountDetails.SelectToken("Type"), Is.Not.Null, message: "Type");
                Assert.That((string)ftaAccountDetails.SelectToken("Balance"), Is.Not.Null, message: "Balance");
                Assert.That((string)ftaAccountDetails.SelectToken("TotalValue"), Is.Not.Null, message: "TotalValue");
                Assert.That((string)ftaAccountDetails.SelectToken("ChangeAmount"), Is.Not.Null, message: "ChangeAmount");
                Assert.That((string)ftaAccountDetails.SelectToken("ChangePercent"), Is.Not.Null, message: "ChangePercent");
                Assert.That((string)ftaAccountDetails.SelectToken("WithdrawableBalance"), Is.Not.Null, message: "WithdrawableBalance");
            });
        }


        [Test]
        public void Get_FtaAccounts_InvalidAddress_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Summary/IncludeFta/{investor.Address[0..^5]}"), // Invalid address
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        public void Get_FtaAccounts_NotFoundInvestor_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("MainnetSender"); // No Fta account for address

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Summary/IncludeFta/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialaze
            JArray accounts = (JArray)JObject.Parse(response.Content)["Accounts"]; // Get all invest accounts
            IList<JToken> accountTypes = accounts.SelectTokens("Type").ToList();
            TestContext.WriteLine("Response: " + response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(accountTypes.ToString(), Has.No.Member("FTA"));
        }


        [Test]
        public void Get_FtaAccounts_NotFoundAccount_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Summary/IncludeFta/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment, "qa.test")) // User without Fta account
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialaze
            JArray accounts = (JArray)JObject.Parse(response.Content)["Accounts"]; // Get all invest accounts
            IList<JToken> accountTypes = accounts.SelectTokens("Type").ToList();
            TestContext.WriteLine("Response: " + response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(accountTypes.ToString(), Has.No.Member("FTA"));
        }


        [Test]
        public void Get_FtaAccounts_NoAuth_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Summary/IncludeFta/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
                                              // No Authorization
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }


        [Test]
        public void Get_FtaAccounts_UnverifiedAddress_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Summary/IncludeFta/{investor.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
                                              // No Address verification
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("No request signature header was provided.", response);
        }


        [Test]
        [Category("Positive")]
        public void Get_FtaAccounts_NotAuthorizedUser_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            var investor2 = QAKeyVault.GetGluwaAddress("MainnetSender");

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Summary/IncludeFta/{investor.Address}"),
                           Api.SendRequest(Method.GET)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor2.PrivateKey))); // Investor not an address owner
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }
    }
}
