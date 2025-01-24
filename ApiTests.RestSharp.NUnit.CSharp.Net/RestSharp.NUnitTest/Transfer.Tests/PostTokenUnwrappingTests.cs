using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Transfer.Tests
{
    [TestFixture("Test")]
    //[TestFixture("Staging")]
    //[TestFixture("Production")]
    [Category("TokenUnwrapping"), Category("Post")]
    public class PostTokenUnwrappingTests : TransferFunctions
    {
        private string environment;
        private ECurrency currency;

        public PostTokenUnwrappingTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            currency = ECurrency.Usdcg;
        }

        [Test]
        [Category("Usdc"), Category("Positive")]
        [Description("TestCaseId:C4010")]
        public void PostTokenUnwrapping_Pos()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = "1";          // Amount must be >= than fee 
            string fee = ECurrency.Usdcg.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(ECurrency.Usdcg))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4011")]
        public void PostTokenUnwrapping_ZeroAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Amount = "0"; // Zero amount

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Amount must be larger than 0.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4012")]
        public void PostTokenUnwrapping_NegativeAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Amount = "-1";  // Negative amount

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Amount must be larger than 0.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4013")]
        public void PostTokenUnwrapping_OverSixDecimalsAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Amount = "1.00000001"; // Over 6 decimal points amount

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Amount has invalid number of decimal places.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4014")]
        public void PostTokenUnwrapping_ZeroFee_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Fee = "0";  // Zero fee

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Fee is too low.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4015")]
        public void PostTokenUnwrapping_NegativeFee_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Fee = "-1";  // Negative fee

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Fee is too low.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4016")]
        public void PostTokenUnwrapping_OverSixDecimalsFee_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Fee = "300.00000001"; // Over 6 decimal points fee

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Fee has invalid number of decimal places.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4017")]
        public void PostTokenUnwrapping_InvalidAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Amount = "1,55";  // Invalid amount

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Amount is not a valid numeric value.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4018")]
        public void PostTokenUnwrapping_InvalidFee_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Fee = "100,55";   // Invalid fee

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Fee is not a valid numeric value.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4019")]
        public void PostTokenUnwrapping_InvalidSourceToken_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.SourceToken = ECurrency.sUsdcg.ToString(); // Invalid source currency

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Token unwrapping between sUSDCG to USDC is not a supported.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4020")]
        public void PostTokenUnwrapping_InvalidTargetToken_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.TargetToken = ECurrency.sUsdcg.ToString();  // Invalid target currency

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Token unwrapping between USDCG to sUSDCG is not a supported.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4021")]
        public void PostTokenUnwrapping_InvalidAddress_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Address = QAKeyVault.GetGluwaAddress("Receiver").Address;  // Invalid address

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4022")]
        public void PostTokenUnwrapping_InvalidSignature_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.BurnSignature = "Ox" + body.BurnSignature;  // Invalid signature

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4023")]
        public void PostTokenUnwrapping_MissingNonce_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Nonce = null;  // Missing nonce

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4024")]
        public void PostTokenUnwrapping_MissingSourceToken_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.SourceToken = null; // Missing source currency

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The SourceToken field is required.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4025")]
        public void PostTokenUnwrapping_MissingTargetToken_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.TargetToken = null;  // Missing target currency

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The TargetToken field is required.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4026")]
        public void PostTokenUnwrapping_MissingAddress_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Address = null;  // Missing address

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Address is not a valid Ethereum address.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4027")]
        public void PostTokenUnwrapping_MissingAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Amount = null;  // Missing aount

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The Amount field is required.", response);
        }


        [Test]
        [Category("Usdc"), Category("Negative")]
        [Description("TestCaseId:C4028")]
        public void PostTokenUnwrapping_MissingSignature_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.BurnSignature = null;  // Missing signature

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The BurnSignature field is required.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C4029")]
        public void PostTokenUnwrapping_MissingFee_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.Fee = null;  // Missing fee

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The Fee field is required.", response);
        }


        [Test]
        [Category("Usdc"), Category("Positive"), Category("Optional field")]
        [Description("TestCaseId:C4030")]
        public void PostTokenUnwrapping_MissingIdem_Pos()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string idem = GenerateIdem();
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = currency.ToDefaultCurrencyFee(decimal.Parse(AmountUtils.GetFee(currency))).ToString();

            // Create body
            PostTokenUnwrappingBody body = RequestTestBody.CreateTokenUnWrappingBody(ECurrency.Usdcg,
                                                                                     "USDC",
                                                                                     senderAddress,
                                                                                     amount,
                                                                                     fee,
                                                                                     idem,
                                                                                     environment);
            body.IdempotentKey = null;  // Missing optional idem. Positive case

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/TokenUnwrapping/Request"), 
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
        }
    }
}

