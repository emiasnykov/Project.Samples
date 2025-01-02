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
    [TestFixture("Staging")]
    [TestFixture("Production")]
    [Category("Transactions"), Category("Post")]
    public class PostTransactionsTests
    {
        private string environment;
        private ECurrency currency;

        public PostTransactionsTests(string environment)
        {
            this.environment = environment;
        }

        private decimal calculateMinAmount(decimal fee, ECurrency? currency)
        {
            if (currency == ECurrency.Btc)
            {
                return decimal.Parse("0.00001") + fee; // Min BTC amount + fee
            }
            return 1m + fee;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            currency = TestSettingsUtil.Currency;
        }

        [Test]
        [Category("Positive")]
        public void Post_Transactions_Btc_Pos()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetBtcAddress("Sender", environment);
            var receiverAddress = QAKeyVault.GetBtcAddress("Receiver", environment);
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   calculateMinAmount(fee, currency),
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Positive"), Category("NgNg")]
        public void Post_Transactions_NgNg_Pos()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   calculateMinAmount(fee, currency),
                                                                                   fee,
                                                                                   environment);
            //Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                                     Api.SendRequest(Method.POST, body));
            //Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
        }


        [Test]
        [Category("Positive"), Category("sNgNg")]
        public void Post_Transactions_sNgNg_Pos()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency));
            var signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey);

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   calculateMinAmount(fee, currency),
                                                                                   fee,
                                                                                   environment);
            //Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                     Api.SendRequest(Method.POST, body)
                                     .AddHeader("X-REQUEST-SIGNATURE", signature));
            //Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Positive"), Category("sUsdcg")]
        public void Post_Transactions_sUsdcg_Pos()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = currency.ToDefaultCurrencyAmount();
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency, amount.ToString()));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Positive"), Category("Usdcg")]
        public void Post_Transactions_Usdcg_Pos()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   calculateMinAmount(fee, currency),
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Post_Transactions_Eth_Pos()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("0.0001");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Positive"), Category("Gcre")]
        public void Post_Transactions_Gcre_Pos()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("5");
            decimal fee = decimal.Parse("1");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,//.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Post_Transactions_Usdc_Pos()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("1");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Post_Transactions_Gate_Pos()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("1");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Post_Transactions_Usdt_Pos()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("1");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Neg"), Category("Ethereum")]
        public void Post_Transactions_InsufficientFund_Eth_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("100");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
            Assertions.HandleAssertionInnerCode("InsufficientFund", response);
        }


        [Test]
        [Category("Negative"), Category("InvalidBody")]
        public void Post_Transactions_InvalidBody_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   calculateMinAmount(fee, currency),
                                                                                   fee,
                                                                                   environment);
            body.Currency = "foobar";   // Invalid currency

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, senderAddress);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidFieldError("foobar", "Currency"), response);
        }


        [Test]
        [Category("Negative")]
        public void Post_Transactions_MissingBody_Neg()
        {
            // Execute without body
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                     Api.SendRequest(Method.POST, null));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.missingBody, response);
        }


        [Test]
        [Category("Negative")]
        public void Post_Transactions_MissingCurrency_Neg()
        {
            // Set up testing address and currency
            var currency = TestSettingsUtil.Currency;
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   calculateMinAmount(fee, currency),
                                                                                   fee,
                                                                                   environment);
            body.Currency = "";    // Missing currency

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, senderAddress);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedMissingFieldError("Currency"), response);
        }


        [Test]
        [Category("Neg"), Category("Ethereum")]
        public void Post_Transactions_MissingCurrency_Eth_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("100");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            body.Currency = "";    // Missing currency

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
            Assertions.HandleAssertionInnerMessage("The Currency field is required.", response);
        }


        [Test]
        [Category("Negative")]
        public void Post_Transactions_InvalidSignature_Neg()
        {
            // Set up testing address and currency
            var currency = TestSettingsUtil.Currency;
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   calculateMinAmount(fee, currency),
                                                                                   fee,
                                                                                   environment);
            // Switch source and target addresses in the final body
            body.Source = receiverAddress.Address;
            body.Target = senderAddress.Address;

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, senderAddress);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.invalidSignatureBody, response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Post_Transactions_InvalidSignature_Usdc_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("2");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Switch source and target addresses in the final body
            body.Source = receiverAddress.Address;
            body.Target = senderAddress.Address;

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assertions.HandleAssertionInnerCode("SignatureNotMatch", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Post_Transactions_InvalidSignature_Gcre_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("2");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Switch source and target addresses in the final body
            body.Source = receiverAddress.Address;
            body.Target = senderAddress.Address;

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assertions.HandleAssertionInnerCode("SignatureNotMatch", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Post_Transactions_InvalidSignature_Gate_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("2");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Switch source and target addresses in the final body
            body.Source = receiverAddress.Address;
            body.Target = senderAddress.Address;

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assertions.HandleAssertionInnerCode("SignatureNotMatch", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Post_Transactions_Conflict_Eth_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Sender");
            decimal amount = decimal.Parse("2");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage("Source and Target address are the same.", response);
        }


        [Test]
        [Category("Negative")]
        public void Post_Transactions_Conflict_Neg()
        {
            // Set up testing address and currency
            var currency = TestSettingsUtil.Currency;
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   calculateMinAmount(fee, currency),
                                                                                   fee,
                                                                                   environment);
            // Execute once
            Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"), Api.SendRequest(Method.POST, body));

            // Execute again
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Conflict, response, senderAddress);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.transactionConflict, response);
        }


        [Test]
        [Category("Negative")]
        public void Post_Transactions_Conflict_Gate_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = 0m;
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   calculateMinAmount(fee, currency),
                                                                                   fee,
                                                                                   environment);
            // Execute once
            Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"), Api.SendRequest(Method.POST, body));

            // Execute again
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Conflict, response, senderAddress);
            Assertions.HandleAssertionMessage($"A transaction with the same idempotent key '{body.Idem}' already exists.", response);
        }


        [Test]
        [Category("Negative"), Category("NgNg")]
        public void Post_Transactions_InvalidAmount_NgNg_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   0,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, senderAddress);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.InvalidTxnAmount, response);
        }


        [Test]
        [Category("Negative"), Category("sNgNg")]
        public void Post_Transactions_InvalidAmount_sNgNg_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   0,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, senderAddress);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.InvalidTxnAmount, response);
        }


        [Test]
        [Category("Negative"), Category("Btc")]
        public void Post_Transactions_InvalidAmount_Btc_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetBtcAddress("Sender", environment);
            var receiverAddress = QAKeyVault.GetBtcAddress("Receiver", environment);
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   0,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, senderAddress);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.InvalidTxnAmount, response);
        }


        [Test]
        [Category("Negative"), Category("sUsdcg")]
        public void Post_Transactions_InvalidAmount_sUsdcg_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = currency.ToDefaultCurrencyAmount();
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency, amount.ToString()));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   0,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, senderAddress);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.InvalidTxnAmount, response);
        }


        [Test]
        [Category("Negative"), Category("Gcre")]
        public void Post_Transactions_InvalidAmount_Gcre_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("-0.5");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage("Amount must not be less than 0.", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Post_Transactions_InvalidAmount_Usdc_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("-0.5");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage("Amount must not be less than 0.", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Post_Transactions_InvalidAmount_Gate_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("-0.5");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage("Amount must not be less than 0.", response);
        }


        [Test]
        [Category("Negative"), Category("Usdcg")]
        public void Post_Transactions_InvalidAmount_Usdcg_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = decimal.Parse(AmountUtils.GetFee(currency));

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   0,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, senderAddress);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.InvalidTxnAmount, response);
        }


        [Test]
        [Category("Negative"), Category("Unsupported currency")]
        public void Post_Transactions_UnsupportedCurrency_Usdg_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = 100m;

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   calculateMinAmount(fee, currency),
                                                                                   fee,
                                                                                   environment);
            body.Currency = "USDG"; // Unsupported currency

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage("Unsupported currency", response);
        }


        [Test]
        [Category("Negative"), Category("Unsupported currency")]
        public void Post_Transactions_UnsupportedCurrency_Krwg_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal fee = 100m;

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   calculateMinAmount(fee, currency),
                                                                                   fee,
                                                                                   environment);
            body.Currency = "KRWG"; // Unsupported currency

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionInnerMessage("Unsupported currency", response);
        }


        [Test]
        [Category("Neg"), Category("Ethereum")]
        public void Post_Transactions_UnsupportedCurrency_sUsdcg_Neg()
        {
            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("100");
            decimal fee = decimal.Parse("0");

            // Create body
            PostTransactionsBody body = RequestTestBody.CreatePostTransactionsBody(senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Transfer/Ethereum"),
                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(response, body, senderAddress, environment);
            Assertions.HandleAssertionInnerMessage("currency is not a supported currency", response);
        }
    }
}


