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
    public class PostFtaDepositTests : TransferFunctions
    {
        private string environment;
        private ECurrency currency;

        public PostFtaDepositTests(string environment)
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
        public void Post_FtaDeposit_Usdc_Pos()
        {
            // Arrange
            AddressItem investor = QAKeyVault.GetGluwaAddress("Sender");
            (string poolId, string amount) = GetFtaPoolIdAndAmount(environment, isActiveApprovedAmount: true);

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBody(investor,
                                                                               poolId,
                                                                               amount,
                                                                               currency,
                                                                               environment);            
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{investor.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_MismatchBodySignature_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Change address to invalid format
            body.OwnerAddress = senderAddress.Address[0..^5] + "invalid";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_MissingAddress_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Change address to null
            body.OwnerAddress = "";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage("The OwnerAddress field is required.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_InvalidPoolID_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = Shared.INVALID_ID;
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage($" \"{ Shared.INVALID_ID}\" is not a valid PoolId.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_MissingPoolID_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = null;
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage("The PoolId field is required.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_InvalidAmount_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Change amount to invalid amount
            body.Amount = Shared.INVALID_AMOUNT;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage($"Amount '{Shared.INVALID_AMOUNT}' is invalid", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_NegativeAmount_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Change amount to invalid amount
            body.Amount = Shared.NEGATIVE_AMOUNT;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage($"Amount '{Shared.NEGATIVE_AMOUNT}' is invalid", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_AmountMissmatch_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            (string poolId, string amount) = GetFtaPoolIdAndAmount(environment, isActiveApprovedAmount: true);

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);

            // Change amount to different amount
            body.Amount = "128396712983712893712983";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage("Deposit amount mismatch", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_MissingAmount_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Change amount to different amount
            body.Amount = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage("The Amount field is required.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_UnsupportedCurrency_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Change currency to unsupported
            body.Currency = "USDT";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage("Currency is not valid", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_InvalidCurrency_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Change currency to invalid
            body.Currency = Shared.INVALID_CURRENCY;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage($"Unsupported currency", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_MissingCurrency_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Change currency to null value
            body.Currency = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage("The Currency field is required.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_InvalidTxnSignature_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Change currency to null value
            body.DepositTxnSignature = body.DepositTxnSignature[0..^5] + "1nValid";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage($"String '{body.DepositTxnSignature}' could not be converted to byte array (not hex?).", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_PoolNotAvailable_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Closed");
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage("Pool is not available", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_UnVerifiedAddress_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response);
            Assertions.HandleAssertionMessage("No request signature header was provided.", response);
        }


        [Test]
        [Category("TestnetOnly")]
        public void Post_FtaDeposit_NoAuth_Usdc_Neg()
        {
            // Arrange
            AddressItem senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string poolId = GetFtaPoolId(environment, poolState: "Open");
            string amount = "2";

            // Create body
            PostFtaDepositBody body = RequestTestBody.CreatePostFtaDepositBodyNegativeTests(senderAddress,
                                                                                            poolId,
                                                                                            amount,
                                                                                            currency,
                                                                                            environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/Deposits?{senderAddress.Address}"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }      
    }
}
