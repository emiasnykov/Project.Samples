using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;

namespace BondAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    [Category("GetAccounts"), Category("Get")]
    public class GetAccountsTests : TransferFunctions
    {
        private string environment;

        public GetAccountsTests(string environment)
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
        [Description("Get details about user investment accounts, TestCaseId:C4063"), Category("Positive")]
        public void GetAccounts_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Accounts"), Api.SendRequest(Method.GET)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Pretty JSON
            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            // Assert
            Assert.That((string)data.SelectToken("TotalValue"), Is.Not.Null);
            Assert.That((string)data.SelectToken("TotalDeposits"), Is.Not.Null);
            Assert.That((string)data.SelectToken("InterestAccrued"), Is.Not.Null);
            Assert.That((string)data.SelectToken("EffectiveApy"), Is.Not.Null);
            Assert.That(data.SelectToken("Accounts")[0].Id, Is.Not.Null);
            Assert.That(data.SelectToken("Accounts")[0].Type, Is.Not.Null);
            Assert.That(data.SelectToken("Accounts")[0].Balance, Is.Not.Null);
            Assert.That(data.SelectToken("Accounts")[0].TotalValue, Is.Not.Null);
            Assert.That(data.SelectToken("Accounts")[0].Status, Is.Not.Null);
            Assert.That(data.SelectToken("Accounts")[0].ChangeAmount, Is.Not.Null);
            Assert.That(data.SelectToken("Accounts")[0].ChangePercent, Is.Not.Null);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Description("Get account details and check interest rate, TestCaseId:C4064"), Category("Positive")]
        public void GetAccount_CheckInterestRate_Pos()
        {
            //Get Fixed-Term Interest account Id 
            string Id = GetBondAccountId(environment);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{Id}"), Api.SendRequest(Method.GET)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Pretty JSON
            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            TestContext.WriteLine("Response: " + data);

            if (environment == "Test") 
            {
                // Calculate actual interest rate
                var balance = data.SelectToken("Balance");
                var expectedRate = (decimal)data.SelectToken("InterestAccrued");
                var actualRate = Math.Round((decimal)(((balance - expectedRate) * 0.12) / 365 * 91.25), 2);

                // Assert
                Assert.AreEqual(expectedRate, actualRate);
            }
        }
    }
}