using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;

namespace LotteryAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    //[TestFixture("Sandbox")]
    [Category("GetAccounts"), Category("Get")]
    public class GetAccountsTests
    {
        private string environment;
        private string keyName;

        public GetAccountsTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            //Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            keyName = GluwaTestApi.SetUpGluwaKeyName(environment);
        }

        [Test]
        public void Get_Accounts_Prize_Pos()
        {
            // Arrange 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v2/Accounts"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Deserialization
            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That((string)data.SelectToken("TotalValue"), Is.Not.Null, message: "TotalValue");
                Assert.That((string)data.SelectToken("TotalDeposits"), Is.Not.Null, message: "TotalDeposits");
                Assert.That((string)data.SelectToken("InterestAccrued"), Is.Not.Null, message: "InterestAccrued");
                Assert.That((string)data.SelectToken("EffectiveApy"), Is.Not.Null, message: "EffectiveApy");
                Assert.That(data.SelectToken("Accounts"), Is.Not.Empty, message: "Accounts are empty");
                Assert.That((string)data.SelectToken("Accounts")[0].Type, Is.Not.Null, message: "Accounts.Type");
                Assert.That((string)data.SelectToken("Accounts")[0].Type, Is.Not.Null, message: "Accounts.Type");
                Assert.That((string)data.SelectToken("Accounts")[0].Balance, Is.Not.Null, message: "Accounts.Balance");
                Assert.That((string)data.SelectToken("Accounts")[0].TotalValue, Is.Not.Null, message: "Accounts.TotalValue");
                Assert.That((string)data.SelectToken("Accounts")[0].ChangeAmount, Is.Not.Null, message: "Accounts.ChangeAmount");
                Assert.That((string)data.SelectToken("Accounts")[0].ChangePercent, Is.Not.Null, message: "Accounts.ChangePercent");
            });
        }


        [Test]
        public void Get_AccountsByAddress_Prize_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Prize/{senderAddress.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(objects.SelectToken("Type").ToString(), Is.Not.Null, message: "Type");
                Assert.That(objects.SelectToken("Balance").ToString(), Is.Not.Null, message: "Balance");
                Assert.That(objects.SelectToken("WithdrawableBalance").ToString(), Is.Not.Null, message: "WithdrawableBalance");
                Assert.That(objects.SelectToken("MaturedBalance").ToString(), Is.Not.Null, message: "MaturedBalance");
                Assert.That(objects.SelectToken("InterestAccrued").ToString(), Is.Not.Null, message: "InterestAccrued");
                Assert.That(objects.SelectToken("MyDrawWinnings").ToString(), Is.Not.Null, message: "MyDrawWinnings");
                //Assert.That(objects.SelectToken("MyOdds").ToString(), Is.Not.Null, message: "MyOdds");
                Assert.That(objects.SelectToken("EffectiveApy").ToString(), Is.Not.Null, message: "EffectiveApy");
            });
        }


        [Test]
        public void Get_AccountsByAddress_Invalid_Prize_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Prize/{senderAddress.Address + "aa"}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Invalid wallet address provided.", response);
        }


        [Test]
        public void Get_AccountsByAddress_NotFound_Prize_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("TakerSender");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Prize/{senderAddress.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Deserialization
            JObject obj = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(obj.SelectToken("WithdrawableBalance").ToString(),
                        Is.EqualTo("0"),
                        message: $"ENV: {environment}\n" + 
                                  "PropertyName: WithdrawableBalance");
        }


        [Test]
        public void Get_AccountsByAddress_MissingAddress_Prize_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            senderAddress.Address = null; // Change address to null

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Prize/{senderAddress.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
        }
    }
}
