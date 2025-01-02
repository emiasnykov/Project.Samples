using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace VerifyVASP.Tests
{
    [TestFixture("Test")]
    public class PostWalletTests : TransferFunctions
    {
        private string environment;
        internal ECurrency currency;

        public PostWalletTests(string environment)
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
        public void Post_Wallet_Pos()
        {
            // Arrange address and create body
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            var body = RequestTestBody.CreatePostWalletBody(address.Address, "EVM");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            if (response.Content.Contains($"{address.Address} already exists"))
            {
                Assert.Ignore("Address is already registered");
            }
            else { Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response); }
        }


        [Test]
        public void Post_Wallet_InvalidBtcAddress_Neg()
        {
            // Arrange 
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            var body = RequestTestBody.CreatePostWalletBody(address.Address, "BTC");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage($"Address {address.Address} is not valid", response);
        }


        [Test]
        public void Post_Wallet_InvalidEvmAddress_Neg()
        {
            // Arrange 
            var address = QAKeyVault.GetBtcAddress("Sender", environment);

            // Create body
            var body = RequestTestBody.CreatePostWalletBody(address.Address, "EVM");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage($"Address {address.Address} is not valid", response);
        }


        [Test]
        public void Post_Wallet_InvalidChainType_Neg()
        {
            // Arrange 
            var address = QAKeyVault.GetBtcAddress("Sender", environment);

            // Create body
            var body = RequestTestBody.CreatePostWalletBody(address.Address, "EVN");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage(" \"EVN\" is not a valid ChainTypeId.", response);
        }


        [Test]
        public void Post_Wallet_NoAuth_Neg()
        {
            // Arrange address and create body
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            var body = RequestTestBody.CreatePostWalletBody(address.Address, "EVM");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet"),
                                           Api.SendRequest(Method.POST, body)
                                              //.AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }


        [Test]
        public void Post_Wallet_UnverifiedAddress_Neg()
        {
            // Arrange address and create body
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            var body = RequestTestBody.CreatePostWalletBody(address.Address, "EVM");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/Wallet"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response);
            Assertions.HandleAssertionMessage("No request signature header was provided.", response);
        }
    }
}
