using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Transfer.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    [Category("Address"), Category("Get")]
    public class GetBalancesTests
    {
        private string environment;

        public GetBalancesTests(string environment)
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
        [Category("Get All balances"), Category("Positive")]
        public void Get_AddressBalances_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Balances/{address.Address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance items
            JArray jsonArray = JArray.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual(14, jsonArray.Count);                               // check return 4 pairs 
            Assert.That(JObject.Parse(jsonArray[2].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("NGNG", (string)JObject.Parse(jsonArray[2].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[3].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("sUSDCG", (string)JObject.Parse(jsonArray[3].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[4].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("sNGNG", (string)JObject.Parse(jsonArray[4].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[5].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("sKRWCG", (string)JObject.Parse(jsonArray[5].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[6].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("GCRE", (string)JObject.Parse(jsonArray[6].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[7].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("USDCG", (string)JObject.Parse(jsonArray[7].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[8].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("USDC", (string)JObject.Parse(jsonArray[8].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[9].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("ETH", (string)JObject.Parse(jsonArray[9].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[10].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("USDT", (string)JObject.Parse(jsonArray[10].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[11].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("sSGDG", (string)JObject.Parse(jsonArray[11].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[12].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("GTD", (string)JObject.Parse(jsonArray[12].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[13].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("GATE", (string)JObject.Parse(jsonArray[13].ToString()).SelectToken("Currency"));
        }


        [Test]
        [Category("Positive"), Category("gatewayDao")]
        public void Get_AddressesBalance_GatewayDao_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/balances/{address.Address}/gatewaydao"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance items
            JArray jsonArray = JArray.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(JObject.Parse(jsonArray[0].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("GTD", (string)JObject.Parse(jsonArray[0].ToString()).SelectToken("Currency"));
            Assert.AreEqual("Unstaked", (string)JObject.Parse(jsonArray[0].ToString()).SelectToken("State"));
            Assert.That(JObject.Parse(jsonArray[1].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("GATE", (string)JObject.Parse(jsonArray[1].ToString()).SelectToken("Currency"));
            Assert.AreEqual("Claimed", (string)JObject.Parse(jsonArray[1].ToString()).SelectToken("State"));
            Assert.That(JObject.Parse(jsonArray[2].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("GTD", (string)JObject.Parse(jsonArray[2].ToString()).SelectToken("Currency"));
            Assert.AreEqual("Staked", (string)JObject.Parse(jsonArray[2].ToString()).SelectToken("State"));
            Assert.That(JObject.Parse(jsonArray[3].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("GATE", (string)JObject.Parse(jsonArray[3].ToString()).SelectToken("Currency"));
            Assert.AreEqual("Unclaimed", (string)JObject.Parse(jsonArray[3].ToString()).SelectToken("State"));
        }


        [Test]
        [Category("Positive"), Category("gatewayDao")]
        public void Get_AddressesBalance_ZeroBalanceGatewayDao_Pos()
        {
            // Arrange address
            var address = Shared.ZERO_BALANCES_ADDRESS;

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/balances/{address}/gatewaydao"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance items
            JArray jsonArray = JArray.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual("0", (string)JObject.Parse(jsonArray[0].ToString()).SelectToken("Balance"));
            Assert.AreEqual("0", (string)JObject.Parse(jsonArray[1].ToString()).SelectToken("Balance"));
            Assert.AreEqual("0", (string)JObject.Parse(jsonArray[2].ToString()).SelectToken("Balance"));
            Assert.AreEqual("0", (string)JObject.Parse(jsonArray[3].ToString()).SelectToken("Balance"));
        }


        [Test]
        [Category("Get zero balances"), Category("Positive")]
        public void Get_AddressBalances_ZeroBalances_Pos()
        {
            // Arrange address
            var address = Shared.ZERO_BALANCES_ADDRESS;

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Balances/{address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance items
            JArray jsonArray = JArray.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual(14, jsonArray.Count, environment);   // check return 14 pairs 
            Assert.That(JObject.Parse(jsonArray[2].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("NGNG", (string)JObject.Parse(jsonArray[2].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[3].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("sUSDCG", (string)JObject.Parse(jsonArray[3].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[4].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("sNGNG", (string)JObject.Parse(jsonArray[4].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[5].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("sKRWCG", (string)JObject.Parse(jsonArray[5].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[6].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("GCRE", (string)JObject.Parse(jsonArray[6].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[7].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("USDCG", (string)JObject.Parse(jsonArray[7].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[8].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("USDC", (string)JObject.Parse(jsonArray[8].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[9].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("ETH", (string)JObject.Parse(jsonArray[9].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[10].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("USDT", (string)JObject.Parse(jsonArray[10].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[11].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("sSGDG", (string)JObject.Parse(jsonArray[11].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[12].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("GTD", (string)JObject.Parse(jsonArray[12].ToString()).SelectToken("Currency"));
            Assert.That(JObject.Parse(jsonArray[13].ToString()).SelectToken("Balance"), Is.Not.Null);
            Assert.AreEqual("GATE", (string)JObject.Parse(jsonArray[13].ToString()).SelectToken("Currency"));
        }
    }
}