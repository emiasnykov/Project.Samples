using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;
using RestSharp;
using System.Net;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json.Linq;
using System;
using GluwaAPI.TestEngine.Models;

namespace FtaAccount.Tests
{
    [TestFixture("Test")]
    //[TestFixture("Staging")]
    public class PostGenerateRSVTests : TransferFunctions
    {
        private string environment;
        private ECurrency currency;

        public PostGenerateRSVTests(string environment)
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
        [Category("TestnetOnly")]
        public void Post_GenerateRSV_Fta_Usdc_Pos()
        {
            // Arrange
            string poolId, amount;
            string investorName = "Sender";          
            string nonce = AmountUtils.GetNonce();
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress(investorName);

            // Get poolId and amount 
            (poolId, amount) = GetFtaPoolIdAndAmount(environment, isActiveApprovedAmount: true, investorName: investorName);
            
            // Create body
            PostFtaAuthorizationBody body = RequestTestBody.CreatePostFtaAuthorizationBody(senderAddress,
                                                                                           poolId,
                                                                                           amount,
                                                                                           nonce,
                                                                                           currency);
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/GenerateRSV?investoraddress={senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
            if ((bool)content.SelectToken("IsCreateAccount"))     // Create account
            {                 
                Assert.That(content.SelectToken("IsCreateAccount").ToString(), Is.Not.Empty);
                Assert.That(content.SelectToken("PoolHash").ToString(), Is.Not.Empty);
                Assert.That(content.SelectToken("V").ToString(), Is.Not.Empty);
                Assert.That(content.SelectToken("R").ToString(), Is.Not.Empty);
                Assert.That(content.SelectToken("S").ToString(), Is.Not.Empty);
                Assert.That(content.SelectToken("IdentityHash").ToString(), Is.Not.Empty);
            }
            else    // Create balance
            {
                Assert.That(content.SelectToken("IsCreateAccount").ToString(), Is.Not.Empty);
                Assert.That(content.SelectToken("PoolHash").ToString(), Is.Not.Empty);
                Assert.That(content.SelectToken("V").ToString(), Is.Not.Empty);
                Assert.That(content.SelectToken("R").ToString(), Is.Not.Empty);
                Assert.That(content.SelectToken("S").ToString(), Is.Not.Empty);
            }
        }


        [Test]
        public void Post_GenerateRSV_MaturedPool_Fta_Usdc_Neg()
        {
            // Arrange
            string investorName = "Test";           
            string amount = "2";
            string nonce = AmountUtils.GetNonce();
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress(investorName);        

            // Get poolId 
            (string poolId, string name) = GetFtaPoolIdAndName(environment, poolState: "Matured", investorName: investorName); // Matured pool

            // Create body
            PostFtaAuthorizationBody body = RequestTestBody.CreatePostFtaAuthorizationBody(senderAddress,
                                                                                           poolId,
                                                                                           amount,
                                                                                           nonce,
                                                                                           currency);
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/GenerateRSV?investoraddress={senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment, userName: "qa.assertible.t"))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage($"Pool '{name}' is not accepting any more deposits", response);
        }


        [Test]
        public void Post_GenerateRSV_InvalidPoolId_Fta_Usdc_Neg()
        {
            // Arrange
            string investorName = "Test";           
            string amount = "4";
            string nonce = AmountUtils.GetNonce();
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress(investorName);

            // Get poolId
            string poolId = GetFtaPoolId(environment, poolState: "Open")[0..^3]; // Invalid pool Id format

            // Create body
            PostFtaAuthorizationBody body = RequestTestBody.CreatePostFtaAuthorizationBody(senderAddress,
                                                                                           poolId,
                                                                                           amount,
                                                                                           nonce,
                                                                                           currency);
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/GenerateRSV?investoraddress={senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment, userName: "qa.assertible.t"))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($" \"{poolId}\" is not a valid PoolId.", response);
        }


        [Test]
        public void Post_GenerateRSV_NotFoundPoolId_Fta_Usdc_Neg()
        {
            // Arrange
            string investorName = "Test";           
            string amount = "4";
            string nonce = AmountUtils.GetNonce();
            string poolId = Shared.NOT_FOUND_ID.ToLower();   // Non exist poolId
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress(investorName);

            // Create body
            PostFtaAuthorizationBody body = RequestTestBody.CreatePostFtaAuthorizationBody(senderAddress,
                                                                                           poolId,
                                                                                           amount,
                                                                                           nonce,
                                                                                           currency);
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/GenerateRSV?investoraddress={senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment, userName: "qa.assertible.t"))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assert.That(response.Content, Does.Contain($"The pool with ID '{poolId}' does not exist"));
        }


        [Test]
        public void Post_GenerateRSV_InvalidAddress_Fta_Usdc_Neg()
        {
            // Arrange
            string amount = "4";       
            string nonce = AmountUtils.GetNonce();
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");

            // Get poolId
            string poolId = GetFtaPoolId(environment, poolState: "Open");

            // Create body
            PostFtaAuthorizationBody body = RequestTestBody.CreatePostFtaAuthorizationBody(senderAddress,
                                                                                           poolId,
                                                                                           amount,
                                                                                           nonce,
                                                                                           currency);
            // Invalid address
            body.OwnerAddress = body.OwnerAddress[0..^4];

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/GenerateRSV"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment, userName: "qa.assertible.t"))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage($"Address '{body.OwnerAddress}' is invalid", response);
        }


        [Test]
        public void Post_GenerateRSV_NoAccountForAddress_Fta_Usdc_Neg()
        {
            // Arrange              
            string amount = "4";
            string nonce = AmountUtils.GetNonce();
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("MainnetSender");

            // Get poolId
            string poolId = GetFtaPoolId(environment, poolState: "Open");

            // Create body
            PostFtaAuthorizationBody body = RequestTestBody.CreatePostFtaAuthorizationBody(senderAddress,
                                                                                           poolId,
                                                                                           amount,
                                                                                           nonce,
                                                                                           currency);
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/GenerateRSV"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment, userName: "qa.assertible.t"))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage($"No FtaApprovals could be found for this generate RSV request", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_GenerateRSV_InvalidAmount_Fta_Usdc_Neg()
        {
            // Arrange
            string amount = "-10";
            string nonce = AmountUtils.GetNonce();
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");

            // Get poolId
            string poolId = GetFtaPoolId(environment, poolState: "Open");

            // Create body
            PostFtaAuthorizationBody body = RequestTestBody.CreatePostFtaAuthorizationBody(senderAddress,
                                                                                           poolId,
                                                                                           amount,
                                                                                           nonce,
                                                                                           currency);
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/GenerateRSV"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment, userName: "qa.assertible.t"))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage($"Amount must not be less than 0.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_GenerateRSV_UnsupportedCurrency_Fta_sUsdcg_Neg()
        {
            // Arrange
            string amount = "2";
            string nonce = AmountUtils.GetNonce();
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");

            // Get poolId
            string poolId = GetFtaPoolId(environment, poolState: "Open");

            // Create body
            PostFtaAuthorizationBody body = RequestTestBody.CreatePostFtaAuthorizationBody(senderAddress,
                                                                                           poolId,
                                                                                           amount,
                                                                                           nonce,
                                                                                           currency);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/GenerateRSV"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage($"Currency 'sUsdcg' is not supported", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_GenerateRSV_InvalidCurrency_Fta_Doge_Neg()
        {
            // Arrange
            string amount = "2";
            string nonce = AmountUtils.GetNonce();
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");

            // Get poolId
            string poolId = GetFtaPoolId(environment, poolState: "Open");

            // Create body
            PostFtaAuthorizationBody body = RequestTestBody.CreatePostFtaAuthorizationBody(senderAddress,
                                                                                           poolId,
                                                                                           amount,
                                                                                           nonce,
                                                                                           currency);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/GenerateRSV"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($" \"{currency}\" is not a valid Currency.", response);
        }
    }
}
