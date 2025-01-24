using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;
using RestSharp;
using System.Net;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Utils;
using GluwaAPI.TestEngine.Models;

namespace FtaAccount.Tests
{
    [TestFixture("Test")] 
    //[TestFixture("Staging")]
    public class PostFtaWithdrawTests : TransferFunctions
    {
        private string environment;
        private ECurrency currency;

        public PostFtaWithdrawTests(string environment)
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
        public void Post_FtaWithdraw_Usdc_Pos()
        {
            // Arrange
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolIdForWithdraw(environment, poolState: "Matured", currency: currency);

            // Create body
            PostFtaWithdrawBody body = RequestTestBody.CreatePostFtaWithdrawBody(investor,
                                                                                 poolId,
                                                                                 currency,
                                                                                 environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Drawdown"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            if (response.Content.Contains($"User has no withdrawable balance"))
            {
                Assert.Ignore($"User has no withdrawable balance");
            }
            else { Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response); }
        }


        [Test]
        public void Post_FtaWithdraw_PoolNotMatured_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");

            // Create body
            PostFtaWithdrawBody body = RequestTestBody.CreatePostFtaWithdrawBodyNegativeTests(senderAddress,
                                                                                              poolId,
                                                                                              currency,
                                                                                              environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Drawdown"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage("Pool is either locked or not matured", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaWithdraw_MissingAddress_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Create body
            PostFtaWithdrawBody body = RequestTestBody.CreatePostFtaWithdrawBodyNegativeTests(senderAddress,
                                                                                              poolId,
                                                                                              currency,
                                                                                              environment);
            // Change address to null
            body.OwnerAddress = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Drawdown"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage("The OwnerAddress field is required.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaWithdraw_InvalidPoolID_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");

            // Create body
            PostFtaWithdrawBody body = RequestTestBody.CreatePostFtaWithdrawBodyNegativeTests(senderAddress,
                                                                                              poolId,
                                                                                              currency,
                                                                                              environment);
            // Change PoolID to invalid format
            body.PoolID = poolId[0..^4] + "1nva**lid";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Drawdown"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage($" \"{body.PoolID}\" is not a valid PoolID.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaWithdraw_MissingPoolID_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = "";

            // Create body
            PostFtaWithdrawBody body = RequestTestBody.CreatePostFtaWithdrawBodyNegativeTests(senderAddress,
                                                                                              poolId,
                                                                                              currency,
                                                                                              environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Drawdown"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage("The PoolID field is required.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaWithdraw_MismatchSignaturePayload_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Create body
            PostFtaWithdrawBody body = RequestTestBody.CreatePostFtaWithdrawBody(senderAddress,
                                                                                 poolId,
                                                                                 currency,
                                                                                 environment);
            // Change payload to create a mismatch with signature.
            body.OwnerAddress = senderAddress.Address + "1";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Drawdown"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaWithdraw_InvalidCurrency_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Create body
            PostFtaWithdrawBody body = RequestTestBody.CreatePostFtaWithdrawBodyNegativeTests(senderAddress,
                                                                                              poolId,
                                                                                              currency,
                                                                                              environment);
            // Change currency to unsupported.
            body.Currency = Shared.INVALID_CURRENCY;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Drawdown"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage($"Unsupported currency", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaWithdraw_UnsupportedCurrency_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured");

            // Create body
            PostFtaWithdrawBody body = RequestTestBody.CreatePostFtaWithdrawBodyNegativeTests(senderAddress,
                                                                                              poolId,
                                                                                              currency,
                                                                                              environment);
            // Change currency to unsupported.
            body.Currency = "GCRE";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Drawdown"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage($"Currency CTC is not supported", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaWithdraw_NoAuth_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured"); ;

            // Create body
            PostFtaWithdrawBody body = RequestTestBody.CreatePostFtaWithdrawBody(senderAddress,
                                                                                 poolId,
                                                                                 currency,
                                                                                 environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Drawdown"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaWithdraw_UnVerifiedAddress_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Matured"); ;

            // Create body
            PostFtaWithdrawBody body = RequestTestBody.CreatePostFtaWithdrawBody(senderAddress,
                                                                                 poolId,
                                                                                 currency,
                                                                                 environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Drawdown"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response);
            Assertions.HandleAssertionMessage("No request signature header was provided.", response);
        }
    }
}
