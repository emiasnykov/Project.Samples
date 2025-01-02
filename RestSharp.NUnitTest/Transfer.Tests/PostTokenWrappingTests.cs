using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Numerics;

namespace Transfer.Tests
{
    [TestFixture("Test")]
    //[TestFixture("Staging")]
    //[TestFixture("Production")]
    [Category("TokenWrapping"), Category("Post")]
    public class PostTokenWrappingTests : TransferFunctions
    {
        private string environment;

        public PostTokenWrappingTests(string environment)
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
        [Category("Usdcg"), Category("Positive")]
        [Description("TestCaseId:C3993")]
        public void PostTokenWrapping_Pos()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem,
                                                                                 environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3994")]
        public void PostTokenWrapping_ZeroAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "0";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Amount must be larger than 0.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3995")]
        public void PostTokenWrapping_NegativeAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.Amount = "-1";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Amount must be larger than 0.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3996")]
        public void PostTokenWrapping_OverSixDecimalsAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.Amount = "5.0000001";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Amount has invalid number of decimal places.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3997")]
        public void PostTokenWrapping_InvalidAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.Amount = "1,001";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Amount is not a valid numeric value.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3998")]
        public void PostTokenWrapping_MissingAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.Amount = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The Amount field is required.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3999")]
        public void PostTokenWrapping_UnsupportedSourceCurrency_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.SourceToken = ECurrency.Usdcg.ToString();

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Token wrapping between USDCG to USDCG is not a supported.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C4000")]
        public void PostTokenWrapping_UnsupportedTargetCurrency_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.TargetToken = "USDC";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Token wrapping between USDC to USDC is not a supported.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C4001")]
        public void PostTokenWrapping_MissingTargetCurrency_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.TargetToken = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The TargetToken field is required.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C4002")]
        public void PostTokenWrapping_InvalidAddress_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.Address = QAKeyVault.GetGluwaAddress("ExecuteFailUsd").Address;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerCode("InvalidRawTransactionSignature", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C4003")]
        public void PostTokenWrapping_MissingAddress_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.Address = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerCode("InvalidAddress", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C4004")]
        public void PostTokenWrapping_InvalidSignature_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.ApproveTxnSignature = "0x" + body.ApproveTxnSignature;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerCode("InvalidRawTransactionSignature", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C4005")]
        public void PostTokenWrapping_MissingSignature_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.ApproveTxnSignature = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerCode("InvalidRawTransactionSignature", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C4006")]
        public void PostTokenWrapping_InvalidMintSignature_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.MintTxnSignature = "0x" + body.MintTxnSignature;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerCode("InvalidRawTransactionSignature", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C4007")]
        public void PostTokenWrapping_MissingMintSignature_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.MintTxnSignature = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerCode("InvalidRawTransactionSignature", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C4008")]
        public void PostTokenWrapping_InvalidIdem_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.IdempotentKey = "0x";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerCode("InvalidValue", response);
        }


        [Test]
        [Category("Usdcg"), Category("Positive")]
        [Description("TestCaseId:C4009")]
        public void PostTokenWrapping_MissingOptionalIdem_Pos()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";

            // Create body
            PostTokenWrappingBody body = RequestTestBody.CreateTokenWrappingBody("USDC",
                                                                                 ECurrency.Usdcg,
                                                                                 senderAddress,
                                                                                 amount,
                                                                                 idem, environment);
            body.IdempotentKey = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenWrapping/Request"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
        }
    }
}
