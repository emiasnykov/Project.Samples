using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
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
    [Category("Unpeg"), Category("Post")]
    public class PostUnpegTests : TransferFunctions
    {
        private string environment;
        private ECurrency currency;
        private const ECurrency DEFAULT_UNPEG_CURRENCY = ECurrency.sUsdcg;

        public PostUnpegTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            //Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            currency = TestSettingsUtil.Currency;
        }


        [Test]
        [Category("sUsdcg"), Category("Positive")]
        [Description("TestCaseId:C1803")]
        public void Post_Unpegs_sUsdcg_Pos()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";
            string fee = AmountUtils.GetFee(currency, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment); ;
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C1824")]
        public void Post_Unpegs_NoAuth_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(currency);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C3984")]
        public void Post_Unpegs_UnsupportedCurrency_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(currency);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Currency = ECurrency.sNgNg.ToString();

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative"), Category("Conflict")]
        [Description("TestCaseId:C1821")]
        public void Post_Unpegs_sUsdcg_Conflict_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // Address
            decimal amount = currency.ToDefaultCurrencyAmount();
            string fee = AmountUtils.GetFee(currency, amount.ToString());
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount.ToString(),
                                                                    fee,
                                                                    environment);
            // Execute 
            Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
               .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Execute again
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Conflict, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.transactionUnpegConflict, response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3985")]
        public void Post_Unpegs_Usdcg_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // Address
            decimal amount = currency.ToDefaultCurrencyAmount();
            string fee = AmountUtils.GetFee(currency);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount.ToString(),
                                                                    fee, environment);
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C3986")]
        public void Post_Unpegs_sUsdcg_ZeroFee_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    "0",
                                                                    environment);
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Invalid Fee amount.", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C3987")]
        public void Post_Unpegs_sUsdcg_OverSixDecimalsFee_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(currency, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Fee = Shared.OVER_SIX_DECIMALS_FEE;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Fee has more than 6 decimal places.", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C3988")]
        public void Post_Unpegs_sUsdcg_NegativeFee_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(currency, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Fee = Shared.NEGATIVE_FEE;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Invalid Fee amount.", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C1863")]
        public void Post_Unpegs_sUsdcg_NegativeAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(currency, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Amount = Shared.NEGATIVE_AMOUNT;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount '" + Shared.NEGATIVE_AMOUNT + "' is less than the minimum amount '" + Shared.MIN_AMOUNT + "'.", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C3989")]
        public void Post_Unpegs_sUsdcg_OverSixDecimalsAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(currency, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Amount = Shared.OVER_SIX_DECIMALS_AMOUNT;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount has more than 6 decimal places.", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C3990")]
        public void Post_Unpegs_sUsdcg_TooSmallAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(currency, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Amount = Shared.TOO_SMALL_AMOUNT;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount '" + Shared.TOO_SMALL_AMOUNT + "' is less than the minimum amount '" + Shared.MIN_AMOUNT + "'.", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C1864")]
        public void Post_Unpegs_sUsdcg_ZeroAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string fee = AmountUtils.GetFee(currency, "1");
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    "0",
                                                                    fee,
                                                                    environment);
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount '0' is less than the minimum amount '" + Shared.MIN_AMOUNT + "'.", response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1861")]
        public void Post_Unpegs_sUsdcg_InvalidAddress_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(currency, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);

            body.Source = QAKeyVault.GetGluwaAddress("ExecuteFailUsd").Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("AllowanceSignature is invalid", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C1822")]
        public void Post_Unpegs_sUsdcg_InvalidSignature_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(currency, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);

            body.AllowanceSignature = "Ox" + body.AllowanceSignature;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("AllowanceSignature is invalid", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C1823")]
        public void Post_Unpegs_sUsdcg_InvalidIdem_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("TakerReceiver");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(currency, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Idem = Shared.INVALID_IDEM;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C3991")]
        public void Post_Unpegs_sUsdcg_InvalidAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("TakerReceiver");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(currency, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Amount = Shared.INVALID_FORMAT_AMOUNT;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount value '" + Shared.INVALID_FORMAT_AMOUNT + "' is not a valid number.", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C3992")]
        public void Post_Unpegs_sUsdcg_InvalidFee_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("TakerReceiver");
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(currency, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(currency,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Fee = Shared.INVALID_FORMAT_FEE;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Fee value '" + Shared.INVALID_FORMAT_FEE + "' is not a valid number.", response);
        }


        [Test]
        [Category("Negative"), Category("MissingBody")]
        [Description("TestCaseId:C1820")]
        public void Post_Unpegs_MissingBody_Neg()
        {
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, null)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.missingBody, response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1829")]
        public void Post_Unpegs_MissingCurrency_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // Address
            string amount = DEFAULT_UNPEG_CURRENCY.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(DEFAULT_UNPEG_CURRENCY, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(DEFAULT_UNPEG_CURRENCY,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Currency = Shared.NULL_OR_EMPTY;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Currency"), response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1826")]
        public void Post_Unpegs_MissingAddress_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // Address
            string amount = DEFAULT_UNPEG_CURRENCY.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(DEFAULT_UNPEG_CURRENCY, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(DEFAULT_UNPEG_CURRENCY,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Source = Shared.NULL_OR_EMPTY;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Source"), response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1827")]
        public void Post_Unpegs_MissingAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // Address
            string amount = DEFAULT_UNPEG_CURRENCY.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(DEFAULT_UNPEG_CURRENCY, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(DEFAULT_UNPEG_CURRENCY,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Amount = Shared.NULL_OR_EMPTY;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Amount"), response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1828")]
        public void Post_Unpegs_MissingFee_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // Address
            string amount = DEFAULT_UNPEG_CURRENCY.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(DEFAULT_UNPEG_CURRENCY, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(DEFAULT_UNPEG_CURRENCY,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Fee = Shared.NULL_OR_EMPTY;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Fee"), response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1825")]
        public void Post_Unpegs_MissingSignature_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // Address
            string amount = DEFAULT_UNPEG_CURRENCY.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(DEFAULT_UNPEG_CURRENCY, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(DEFAULT_UNPEG_CURRENCY,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.AllowanceSignature = Shared.NULL_OR_EMPTY;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("AllowanceSignature"), response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1830")]
        public void Post_Unpegs_MissingIdem_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // Address
            string amount = DEFAULT_UNPEG_CURRENCY.ToDefaultCurrencyAmount().ToString();
            string fee = AmountUtils.GetFee(DEFAULT_UNPEG_CURRENCY, amount);
            string contractAbiBlob = GetContractAbi("Usd", "Sidechain");

            // Create body
            PostUnpegsBody body = RequestTestBody.CreateUnpegsBody(DEFAULT_UNPEG_CURRENCY,
                                                                    senderAddress,
                                                                    contractAbiBlob,
                                                                    amount,
                                                                    fee,
                                                                    environment);
            body.Idem = Shared.NULL_OR_EMPTY;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Unpegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Idem"), response);
        }
    }
}
