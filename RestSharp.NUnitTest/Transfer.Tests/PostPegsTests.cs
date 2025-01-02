using System.Net;
using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using NUnit.Framework;
using RestSharp;

namespace Transfer.Tests
{
    [TestFixture("Test")]
    //[TestFixture("Staging")]
    //[TestFixture("Production")]
    [Category("Peg"), Category("Post")]
    public class PostPegsTests : TransferFunctions
    {
        private string environment;
        private ECurrency currency;   
        private const ECurrency DEFAULT_PEG_CURRENCY = ECurrency.Usdcg;

        public PostPegsTests(string environment)
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
        [Category("Usdcg"), Category("Positive")]
        [Description("TestCaseId:C3959")]
        public void Post_Pegs_Usdcg_Pos()
        {
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            //Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
        }


        [Test]
        [Category("Negative"), Category("Forbidden")]
        [Description("TestCaseId:C1812")]
        public void Post_Pegs_NoAuth_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, DEFAULT_PEG_CURRENCY.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }


        [Test]
        [Category("sUsdcg"), Category("Negative")]
        [Description("TestCaseId:C1857")]
        public void Post_Pegs_sUsdcg_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, DEFAULT_PEG_CURRENCY.ToString());
            var amount = currency.ToDefaultCurrencyAmount().ToString();
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency, amount);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                amount,
                                                                fee,
                                                                idem,
                                                                environment);
            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
        }


        [Test]
        [Category("Btc"), Category("Negative")]
        [Description("TestCaseId:C3960")]
        public void Post_Pegs_UnsupportedCurrency_Usdcg_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            body.Currency = ECurrency.Btc.ToString();

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative"), Category("Conflict")]
        [Description("TestCaseId:C3961")]
        public void Post_Pegs_Usdcg_Conflict_Usdcg_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, DEFAULT_PEG_CURRENCY.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            // Execute
            Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body).AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Execute again with the same parameters
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Conflict, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.transactionPegConflict, response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3962")]
        public void Post_Pegs_Usdcg_ZeroAmount_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                "0",
                                                                fee,
                                                                idem,
                                                                environment);
            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount '0' is less than the minimum amount '" + Shared.MIN_AMOUNT + "'.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3963")]
        public void Post_Pegs_Usdcg_NegativeAmount_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);
            decimal amount = currency.ToDefaultCurrencyAmount(); // Change amount to negative

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                amount.ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            body.Amount = Shared.NEGATIVE_AMOUNT;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount '" + Shared.NEGATIVE_AMOUNT + "' is less than the minimum amount '" + Shared.MIN_AMOUNT + "'.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3964")]
        public void Post_Pegs_Usdcg_OverSixDecimalsAmount_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);
            decimal amount = currency.ToDefaultCurrencyAmount(); // Change amount to negative

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                amount.ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            body.Amount = Shared.OVER_SIX_DECIMALS_AMOUNT;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount has more than 6 decimal places.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3965")]
        public void Post_Pegs_Usdcg_TooSmallAmount_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            body.Amount = Shared.TOO_SMALL_AMOUNT;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount '" + Shared.TOO_SMALL_AMOUNT + "' is less than the minimum amount '" + Shared.MIN_AMOUNT + "'.", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3966")]
        public void Post_Pegs_Usdcg_ZeroFee_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency, currency.ToDefaultCurrencyAmount().ToString());

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                "0",
                                                                idem,
                                                                environment);
            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Fee amount is less than the minimum amount " + fee + ".", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3968")]
        public void Post_Pegs_Usdcg_NegativeFee_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string amount = currency.ToDefaultCurrencyAmount().ToString();
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency, amount);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                amount,
                                                                fee,
                                                                idem,
                                                                environment);
            body.Fee = Shared.NEGATIVE_FEE;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Fee amount is less than the minimum amount " + fee + ".", response);
        }


        [Test]
        [Category("Usdcg"), Category("Negative")]
        [Description("TestCaseId:C3969")]
        public void Post_Pegs_Usdcg_OverSixDecimalsFee_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);
            decimal amount = currency.ToDefaultCurrencyAmount(); // Change amount to negative

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                amount.ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            body.Fee = Shared.OVER_SIX_DECIMALS_FEE;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Fee has more than 6 decimal places.", response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C3970")]
        public void Post_Pegs_Usdcg_InvalidAddress_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);

            body.Source = QAKeyVault.GetGluwaAddress("ExecuteFailUsd").Address;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Signature is invalid", response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C3973")]
        public void Post_Pegs_Usdcg_InvalidSignature_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);

            body.Signature = "Ox" + body.Signature;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Signature is invalid", response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C3975")]
        public void Post_Pegs_Usdcg_InvalidIdem_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);

            body.Idem = Shared.INVALID_IDEM;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C3977")]
        public void Post_Pegs_Usdcg_InvalidNonce_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);

            body.Nonce = Shared.INVALID_NONCE;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C3980")]
        public void Post_Pegs_Usdcg_InvalidAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);

            body.Amount = Shared.INVALID_FORMAT_AMOUNT;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount value '" + Shared.INVALID_FORMAT_AMOUNT + "' is not a valid number.", response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C3983")]
        public void Post_Pegs_Usdcg_InvalidFee_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, currency.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(currency,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);

            body.Fee = Shared.INVALID_FORMAT_FEE;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Fee value '" + Shared.INVALID_FORMAT_FEE + "' is not a valid number.", response);
        }


        [Test]
        [Category("Negative"), Category("MissingBody")]
        [Description("TestCaseId:C1807")]
        public void Post_Pegs_MissingBody_Neg()
        {
            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, null)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("body is missing.", response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1816")]
        public void Post_Pegs_MissingCurrency_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, DEFAULT_PEG_CURRENCY.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(DEFAULT_PEG_CURRENCY,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            body.Currency = Shared.NULL_OR_EMPTY;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Currency"), response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1813")]
        public void Post_Pegs_MissingAddress_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, DEFAULT_PEG_CURRENCY.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(DEFAULT_PEG_CURRENCY,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            body.Source = Shared.NULL_OR_EMPTY;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Source"), response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1814")]
        public void Post_Pegs_MissingAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, DEFAULT_PEG_CURRENCY.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(DEFAULT_PEG_CURRENCY,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            body.Amount = Shared.NULL_OR_EMPTY;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Amount"), response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1815")]
        public void Post_Pegs_MissingFee_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, DEFAULT_PEG_CURRENCY.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(DEFAULT_PEG_CURRENCY,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            body.Fee = Shared.NULL_OR_EMPTY;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Fee"), response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1818")]
        public void Post_Pegs_MissingIdem_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, DEFAULT_PEG_CURRENCY.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(DEFAULT_PEG_CURRENCY,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            body.Idem = Shared.NULL_OR_EMPTY;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Idem"), response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1817")]
        public void Post_Pegs_MissingSignature_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, DEFAULT_PEG_CURRENCY.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(DEFAULT_PEG_CURRENCY,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            body.Signature = Shared.NULL_OR_EMPTY;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Signature"), response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        [Description("TestCaseId:C1819")]
        public void Post_Pegs_MissingNonce_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = GetGatewayContract(environment, DEFAULT_PEG_CURRENCY.ToString());
            string idem = GenerateIdem();
            string fee = AmountUtils.GetFee(currency);

            // Create body
            PostPegsBody body = RequestTestBody.CreatePegsBody(DEFAULT_PEG_CURRENCY,
                                                                senderAddress,
                                                                receiverAddress,
                                                                currency.ToDefaultCurrencyAmount().ToString(),
                                                                fee,
                                                                idem,
                                                                environment);
            body.Nonce = Shared.NULL_OR_EMPTY;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Pegs"), Api.SendRequest(Method.POST, body)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Nonce"), response);
        }
    }
}
