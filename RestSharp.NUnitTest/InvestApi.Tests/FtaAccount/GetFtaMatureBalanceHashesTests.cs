using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using GluwaAPI.TestEngine.Utils;
using GluwaAPI.TestEngine.CurrencyUtils;
using System;

namespace FtaAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    public class GetFtaMatureBalanceHashesTests : TransferFunctions
    {
        private string environment;
        private ECurrency currency;

        public GetFtaMatureBalanceHashesTests(string environment)
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
        public void Get_FtaMatureBalanceHashes_Usdc_Pos()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");  // Set pool with matured balance

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/MatureBalanceHashes/{poolId}/{investor.Address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));

            // Assert
            if (response.Content.Contains("No matured balance to withdraw") || response.Content.Contains("Sorry, we are processing the payment"))
            {
                Assert.Ignore("No matured balance to withdraw or the payment is processing");
            }
            else
            {
                JObject responseContents = JObject.Parse(response.Content);
                JArray hashes = JArray.Parse(responseContents.SelectToken("Hashes").ToString());
                Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
                Assert.That(hashes.ToString(), Is.Not.Null);
            }
        }


        [Test]
        [Category("TestnetOnly")]
        public void Get_FtaMatureBalanceHashes_NotMaturedPool_Usdc_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            (string poolId, string name) = GetFtaPoolIdAndName(environment, poolState: "Open");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/MatureBalanceHashes/{poolId}/{investor.Address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage($"Pool '{name}' has not matured yet", response);
        }


        [Test]
        public void Get_FtaMatureBalanceHashes_InvalidPoolId_Usdc_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured")[0..^3];

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/MatureBalanceHashes/{poolId}/{investor.Address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{poolId}' is not valid.", response);
        }


        [Test]
        public void Get_FtaMatureBalanceHashes_NotFoundPoolId_Usdc_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = Shared.NOT_FOUND_ID.ToLower();

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/MatureBalanceHashes/{poolId}/{investor.Address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage($"Invalid Pool ID '{poolId}'", response);
        }


        [Test]
        public void Get_FtaMatureBalanceHashes_InvalidInvestorAddress_Usdc_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/MatureBalanceHashes/{poolId}/{investor.Address[0..^4]}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            if (response.Content.Contains("No matured balance to withdraw") || response.Content.Contains("Sorry, we are processing the payment"))
            {
                Assert.Ignore("No matured balance to withdraw or the payment is processing");
            }
            else
            {
                Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
                Assertions.HandleAssertionMessage($"investorAddress is not a valid Ethereum address", response);
            }       
        }


        [Test]
        public void Get_FtaMatureBalanceHashes_NotFoundInvestor_Usdc_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("MainnetSender");   // No Fta account linked to investor address
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/MatureBalanceHashes/{poolId}/{investor.Address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            if (response.Content.Contains("No matured balance to withdraw") || response.Content.Contains("Sorry, we are processing the payment"))
            {
                Assert.Ignore("No matured balance to withdraw or the payment is processing");
            }
            else
            {
                Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
                Assertions.HandleAssertionMessage($"No account linked to address '{investor.Address}' or account is not active", response);
            }
        }


        [Test]
        public void Get_FtaMatureBalanceHashes_UnsupportedCurrency_Usdcg_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/MatureBalanceHashes/{poolId}/{investor.Address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Currency is not valid", response);
        }


        [Test]
        public void Get_FtaMatureBalanceHashes_NoAuth_Usdc_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");  // Set pool with matured balance

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/MatureBalanceHashes/{poolId}/{investor.Address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
                                              // No Authorization
            // Assert
            if (response.Content.Contains("No matured balance to withdraw") || response.Content.Contains("Sorry, we are processing the payment"))
            {
                Assert.Ignore("No matured balance to withdraw or the payment is processing");
            }
            else
            {
                Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
                Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
            }
        }


        [Test]
        public void Get_FtaMatureBalanceHashes_UnVerifiedUser_Usdc_Neg()
        {
            // Arrange
            var investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");  // Set pool with matured balance

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/MatureBalanceHashes/{poolId}/{investor.Address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
                                              // No user verification
            // Assert
            if (response.Content.Contains("No matured balance to withdraw") || response.Content.Contains("Sorry, we are processing the payment"))
            {
                Assert.Ignore("No matured balance to withdraw or the payment is processing");
            }
            else
            {
                Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
                Assertions.HandleAssertionMessage("No request signature header was provided.", response);
            }
        }
    }
}
