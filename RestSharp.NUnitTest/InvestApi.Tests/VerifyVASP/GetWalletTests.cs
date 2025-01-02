using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace VerifyVASP.Tests
{
    [TestFixture("Test")]
    public class GetWalletTests : TransferFunctions
    {
        private string environment;
        internal ECurrency currency;

        public GetWalletTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            currency = TestSettingsUtil.Currency;
        }

        [Test]
        public void Get_Wallet_AddressExists_Usdc_Pos()
        {
            // Arrange 
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet/Address/{address.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Deserialization
            JObject obj = JObject.Parse(response.Content);

            // Assert
            Assert.AreEqual(true, (bool)obj["Exists"]);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.HandleAssertionMessage($"Address {address.Address} already exists under another user", response);
        }


        [Test]
        public void Get_Wallet_AddressNotExists_Usdc_Pos()
        {
            // Arrange 
            var address = QAKeyVault.GetGluwaAddress("Investor");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet/Address/{address.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Deserialization
            JObject obj = JObject.Parse(response.Content);

            // Assert
            Assert.AreEqual(false, (bool)obj["Exists"]);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        public void Get_Wallet_AddressExists_Btc_Pos()
        {
            // Arrange 
            var address = QAKeyVault.GetBtcAddress("Sender", environment);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet/Address/{address.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Deserialization
            JObject obj = JObject.Parse(response.Content);

            // Assert
            Assert.AreEqual(true, (bool)obj["Exists"]);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.HandleAssertionMessage($"{address.Address} already exists", response);
        }


        [Test]
        public void Get_Wallet_AddressNotExists_Btc_Pos()
        {
            // Arrange 
            var address = QAKeyVault.GetBtcAddress("TakerReceiver", environment);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet/Address/{address.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Deserialization
            JObject obj = JObject.Parse(response.Content);

            // Assert
            Assert.AreEqual(false, (bool)obj["Exists"]);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        public void Get_Wallet_InvalidAddress_Usdc_Neg()
        {
            // Arrange 
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet/Address/{address.Address[0..^4]}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        public void Get_Wallet_NoAuth_Usdc_Neg()
        {
            // Arrange 
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet/Address/{address.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));

            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }


        [Test]
        public void Get_Wallet_UnverifiedAddress_Usdc_Neg()
        {
            // Arrange 
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet/Address/{address.Address}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response);
            Assertions.HandleAssertionMessage("No request signature header was provided.", response);
        }
    }
}
