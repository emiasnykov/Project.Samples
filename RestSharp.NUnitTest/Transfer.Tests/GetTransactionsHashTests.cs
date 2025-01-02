using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Transfer.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    [Category("Transactions"), Category("Get")]
    public class GetTransactionsHashTests : TransferFunctions
    {

        private string environment;
        private ECurrency currency;
        private string keyName;

        public GetTransactionsHashTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            //Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            keyName = GluwaTestApi.SetUpGluwaKeyName(environment);
            currency = TestSettingsUtil.Currency;
        }

        [Test]
        [Category("Positive"), Category("Btc")]
        public void Get_TransactionsHash_Btc_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetBtcAddress("Sender", environment);
            (string signature, string txnHash) = generateSignatureAndHash(address, currency);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_TransactionsHash_Eth_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string txnHash = GetEthTransactionDetails(address, currency).Value<string>("TxnHash");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Parse response
            JObject objects = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(objects["TxnHash"].ToString(), Is.EqualTo($"{txnHash}"), message: "TxnHash:");
                Assert.That(objects["Currency"].ToString(), Is.EqualTo($"{currency.ToString().ToUpper()}"), message: "Currency:");
                Assert.That(objects["GasUsed"], Is.Not.Null, message: "GasUsed:");
            });
        }


        [Test]
        [Category("Positive"), Category("Gcre")]
        public void Get_TransactionsHash_Gcre_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string txnHash = GetEthTransactionDetails(address, currency).Value<string>("TxnHash");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Parse response
            JObject objects = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(objects["TxnHash"].ToString(), Is.EqualTo($"{txnHash}"), message: "TxnHash:");
                Assert.That(objects["Currency"].ToString(), Is.EqualTo($"{currency.ToString().ToUpper()}"), message: "Currency:");
                Assert.That(objects["GasUsed"], Is.Not.Null, message: "GasUsed:");
            });
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_TransactionsHash_Usdc_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            (string signature, string txnHash) = generateEthSignatureAndHash(address, currency);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Parse response
            JObject objects = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(objects["TxnHash"].ToString(), Is.EqualTo($"{txnHash}"), message: "TxnHash:");
                Assert.That(objects["Currency"].ToString(), Is.EqualTo($"{currency.ToString().ToUpper()}"), message: "Currency:");
                Assert.That(objects["GasUsed"], Is.Not.Null, message: "GasUsed:");
            });
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_TransactionsHash_Gate_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            (string signature, string txnHash) = generateEthSignatureAndHash(address, currency);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Parse response
            JObject objects = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
            Assert.Multiple(() =>
            {
                Assert.That(objects["TxnHash"].ToString(), Is.EqualTo($"{txnHash}"), message: "TxnHash:");
                Assert.That(objects["Currency"].ToString(), Is.EqualTo($"{currency.ToString().ToUpper()}"), message: "Currency:");
                Assert.That(objects["GasUsed"], Is.Not.Null, message: "GasUsed:");
            });
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_TransactionsHash_Usdt_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            (string signature, string txnHash) = generateEthSignatureAndHash(address, currency);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Parse response
            JObject objects = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(objects["TxnHash"].ToString(), Is.EqualTo($"{txnHash}"), message: "TxnHash:");
                Assert.That(objects["Currency"].ToString(), Is.EqualTo($"{currency.ToString().ToUpper()}"), message: "Currency:");
                Assert.That(objects["GasUsed"], Is.Not.Null, message: "GasUsed:");
            });
        }


        [Test]
        [Category("Positive"), Category("sUsdcg")]
        public void Get_TransactionsHash_sUsdcg_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            (string signature, string txnHash) = generateSignatureAndHash(address, currency);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Usdcg")]
        public void Get_TransactionsHash_Usdcg_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            (string signature, string txnHash) = generateSignatureAndHash(address, currency);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("NgNg")]
        public void Get_TransactionsHash_NgNg_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            (string signature, string txnHash) = generateSignatureAndHash(address, currency);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("sNgNg")]
        public void Get_TransactionsHash_sNgNg_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            (string signature, string txnHash) = generateSignatureAndHash(address, currency);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }

        //// Service temporarily unavailable
        //[Test]
        //[Category("Positive"), Category("sSgDg")]
        //public void Get_TransactionsHash_sSgDg_Pos()
        //{
        //    // Arrange
        //    var address = QAKeyVault.GetGluwaAddress("Sender");
        //    (string signature, string txnHash) = generateSignatureAndHash(address, currency);

        //    // Execute
        //    var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
        //                                   Api.SendRequest(Method.GET)
        //                                      .AddHeader("X-REQUEST-SIGNATURE", signature));
        //    // Assert
        //    Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
        //}


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_InvalidCurrency_Eth_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string txnHash = GetEthTransactionDetails(address, currency).Value<string>("TxnHash");
            string invalidCurrency = "Ethemereumnem";

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{invalidCurrency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{invalidCurrency}' is not valid.", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_UnsupportedCurrency_Eth_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string txnHash = GetEthTransactionDetails(address, currency).Value<string>("TxnHash");

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{ECurrency.sUsdcg}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("currency is not a supported currency", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_InvalidHash_Eth_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            (string signature, string txnHash) = generateEthSignatureAndHash(address, currency);
            txnHash = txnHash + "99999999invalid";

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleAssertionMessage("Transaction not found.", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_MissingCurrency_Eth_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            (string signature, string txnHash) = generateEthSignatureAndHash(address, currency);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{null}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_MissingTxnHash_Eth_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            (string signature, _) = generateEthSignatureAndHash(address, currency);

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{null}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_NonEthTxnHash_Eth_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            (string _, string txnHash) = generateSignatureAndHash(address, ECurrency.sUsdcg);

            // Execute with nonEth TxnHash
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_NotFoundTxnHash_Eth_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string txnHash = GetEthTransactionDetails(address, currency).Value<string>("TxnHash");
            txnHash = txnHash[0..^2] + "22";

            // Execute with not found txnHash
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleAssertionMessage("Transaction not found.", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_MissingSignature_Eth_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string txnHash = GetEthTransactionDetails(address, currency).Value<string>("TxnHash");

            // Execute without X-REQUEST-SIGNATURE Header
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("No request signature header was provided.", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_InvalidSignature_Eth_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string txnHash = GetEthTransactionDetails(address, currency).Value<string>("TxnHash");
            string signature = "invalidSignature";

            // Execute with invalid X-REQUEST-SIGNATURE Header
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("Address signature does not have a valid format.", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_ExpiredSignature_Eth_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string txnHash = GetEthTransactionDetails(address, currency).Value<string>("TxnHash");
            string signature = ECurrency.Eth.ToExpiredXRequestSignature();

            // Execute with expired X-REQUEST-SIGNATURE
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("Signature is expired.", response);
        }


        [Test]
        [Category("Negative")]
        public void Get_TransactionsHash_InvalidCurrency_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            (string signature, string txnHash) = generateSignatureAndHash(address, currency);

            // Execute with invalid currency
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/foobar/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("foobar", "currency"), response);
        }


        [Test]
        [Category("Negative")]
        public void Get_TransactionsHash_InvalidTxnHash_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string signature = SignaturesUtil.GetXRequestSignature(address.PrivateKey); // X-Request-Signature

            // Execute with invalid txnHash
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/1111"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.invalidTxnHashQuery, response);
        }


        [Test]
        [Category("Negative")]
        public void Get_TransactionsHash_SignatureMissing_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string txnHash = generateSignatureAndHash(address, currency).Item2;

            // Get TxnHash without Signature
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
        }


        [Test]
        [Category("Negative")]
        public void Get_TransactionsHash_InvalidSignature_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string txnHash = generateSignatureAndHash(address, currency).Item2;

            // Execute with invalid signature
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", "invalidSignature"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.invalidAddressSignatureFormat, response);
        }


        [Test]
        [Category("Negative"), Category("Gcre")]
        public void Get_TransactionsHash_ExpiredSignature_Gcre_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string txnHash = GetEthTransactionDetails(address, currency).Value<string>("TxnHash");

            // Get expired signature
            string expiredSignature = currency.ToExpiredXRequestSignature();

            // Execute with expired X-REQUEST-SIGNATURE
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", expiredSignature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.expiredSignature, response);
        }


        [Test]
        [Category("Negative"), Category("NgNg")]
        public void Get_TransactionsHash_ExpiredSignature_NgNg_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            string txnHash = generateSignatureAndHash(address, currency).Item2;

            // Get expired signature
            string expiredSignature = currency.ToExpiredXRequestSignature();

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", expiredSignature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.expiredSignature, response);
        }


        [Test]
        [Category("Negative"), Category("sNgNg")]
        public void Get_TransactionsHash_ExpiredSignature_sNgNg_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            string txnHash = generateSignatureAndHash(address, currency).Item2;

            // Get expired signature
            string expiredSignature = currency.ToExpiredXRequestSignature();

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", expiredSignature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.expiredSignature, response);
        }


        [Test]
        [Category("Negative"), Category("sUsdcg")]
        public void Get_TransactionsHash_ExpiredSignature_sUsdcg_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            string txnHash = generateSignatureAndHash(address, currency).Item2;

            // Get expired signature
            string expiredSignature = currency.ToExpiredXRequestSignature();

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", expiredSignature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.expiredSignature, response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_ExpiredSignature_Usdc_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            (_, string txnHash) = generateEthSignatureAndHash(address, currency);
            string signature = currency.ToExpiredXRequestSignature();

            // Execute with expired X-REQUEST-SIGNATURE
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("Signature is expired.", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_ExpiredSignature_Gate_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            (_, string txnHash) = generateEthSignatureAndHash(address, currency);
            string signature = currency.ToExpiredXRequestSignature();

            // Execute with expired X-REQUEST-SIGNATURE
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response);
            Assertions.HandleAssertionMessage("Signature is expired.", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_TransactionsHash_ExpiredSignature_Usdt_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            (_, string txnHash) = generateEthSignatureAndHash(address, currency);
            string signature = currency.ToExpiredXRequestSignature();

            // Execute with expired X-REQUEST-SIGNATURE
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Transfer/{currency}/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("Signature is expired.", response);
        }


        [Test]
        [Category("Negative"), Category("Usdcg")]
        public void Get_TransactionsHash_ExpiredSignature_Usdcg_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress(keyName);
            string txnHash = generateSignatureAndHash(address, currency).Item2;

            // Get expired signature
            string expiredSignature = currency.ToExpiredXRequestSignature();

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", expiredSignature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.expiredSignature, response);
        }


        [Test]
        [Category("Negative"), Category("Btc")]
        public void Get_TransactionsHash_ExpiredSignature_Btc_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetBtcAddress("Sender", environment);
            string txnHash = generateSignatureAndHash(address, currency).Item2;

            // Get expired signature
            string expiredSignature = currency.ToExpiredXRequestSignature(); // Expired Signature

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", expiredSignature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.expiredSignature, response);
        }


        [Test]
        [Category("Negative"), Category("Btc"), Category("NotFound")]
        public void Get_TransactionsHash_NotFound_Btc_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetBtcAddress("Sender", environment);
            string signature = generateSignatureAndHash(address, currency).Item1;
            string txnHash = NotFoundTxnHash(environment); // Txn Hash can't be found in db

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Transactions/{txnHash}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.getErrorMsg(HttpStatusCode.NotFound, environment), response);
        }
    }
}
