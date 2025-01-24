using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;
using RestSharp;
using System.Net;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Utils;
using System;
using GluwaAPI.TestEngine.Models;

namespace FtaAccount.Tests
{
    [TestFixture("Test")]
    //[TestFixture("Staging")]
    public class PostFtaPoolApproveTests : TransferFunctions
    {
        private string environment;
        private ECurrency currency;

        public PostFtaPoolApproveTests(string environment)
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
        public void Post_ApproveFtaTransaction_Usdc_Pos()
        {
            // Arrange
            string amount = "1";
            string poolId = GetFtaPoolId(environment, poolState: "Open", currency: currency);
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve?investoraddress={investor.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_ApproveFtaTransaction_NegativeAmount_Usdc_Neg()
        {
            // Arrange
            string amount = "1";
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);
            // Change body
            body.Amount = Shared.NEGATIVE_AMOUNT;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Amount can not be negative", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_ApproveFtaTransaction_InvalidAmount_Usdc_Neg()
        {
            // Arrange
            string amount = "1";
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);
            // Change amount to invalid amount
            body.Amount = Shared.INVALID_AMOUNT;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Amount is not a valid number", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_ApproveFtaTransaction_MissingAmount_Usdc_Neg()
        {
            // Arrange
            string amount = "1";
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);
            // Change amount to null
            body.Amount = "";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The Amount field is required.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_ApproveFtaTransaction_InvaliAddress_Usdc_Neg()
        {
            // Arrange
            string amount = "1";
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);
            // Change address to non account address
            body.OwnerAddress = Shared.NONACCOUNT_ADDRESS;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_ApproveFtaTransaction_MissingAddress_Usdc_Neg()
        {
            // Arrange
            string amount = "1";
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);
            // Change address to null
            body.OwnerAddress = "";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The OwnerAddress field is required.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_ApproveFtaTransaction_NotFoundPoolId_Usdc_Neg()
        {
            // Arrange
            string amount = "1";
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);
            // Change PoolID to non existing PoolID
            body.PoolID = Shared.NOT_FOUND_ID;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleAssertionMessage(msg: $"Could not find PoolID {Shared.NOT_FOUND_ID.ToLowerInvariant()} in Gluwa", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_ApproveFtaTransaction_InvalidPoolId_Usdc_Neg()
        {
            // Arrange
            string amount = "1";
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);
            // Change PoolID to non existing PoolID
            body.PoolID = Shared.INVALID_ID;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($" \"{body.PoolID}\" is not a valid PoolID.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_ApproveFtaTransaction_MissingPoolId_Usdc_Neg()
        {
            // Arrange
            string amount = "1";
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);
            // Change PoolID to null
            body.PoolID = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The PoolID field is required.", response);        
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_ApproveFtaTransaction_ClosedPool_Usdc_Neg()
        {
            // Arrange
            string amount = "1";
            (string poolId, string poolName) = GetFtaPoolIdAndName(environment, poolState: "Closed");
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage($"Pool '{poolName}' is not in open state for approval", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_ApproveFtaTransaction_MaturedPool_Usdc_Neg()
        {
            // Arrange
            string amount = "1";         
            (string poolId, string poolName) = GetFtaPoolIdAndName(environment, poolState: "Matured");
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage($"Pool '{poolName}' is not in open state for approval", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_ApproveFtaTransaction_NoAuth_Usdc_Neg()
        {
            // Arrange
            string amount = "1";
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_ApproveFtaTransaction_UnVerifiedAddress_Usdc_Neg()
        {
            // Arrange
            string amount = "1";
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostFtaApproveBody body = RequestTestBody.CreatePostApproveTransactionBody(amount,
                                                                                       investor,
                                                                                       poolId,
                                                                                       currency,
                                                                                       environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/Approve"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("No request signature header was provided.", response);
        }
    }
}
