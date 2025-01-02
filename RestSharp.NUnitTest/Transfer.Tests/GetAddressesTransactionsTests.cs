using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Models.ResponseBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Transfer.Tests
{
    [TestFixture("Test")]
    //[TestFixture("Staging")]
    //[TestFixture("Production")]
    [Category("Transactions"), Category("Get")]
    public class GetAddressesTransactionsTests
    {
        private string environment;
        private ECurrency currency;
        private string keyName;

        public GetAddressesTransactionsTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            keyName = GluwaTestApi.SetUpGluwaKeyName(environment);
            currency = TestSettingsUtil.Currency;
        }

        [Test]
        [Category("Positive"), Category("sUsdcg")]
        public void Get_AddressesTransactions_sUsdcg_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Usdcg")]
        public void Get_AddressesTransactions_Usdcg_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("NgNg")]
        public void Get_AddressesTransactions_NgNg_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("sNgNg")]
        public void Get_AddressesTransactions_sNgNg_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }

        //// Service temporarily unavailable
        //[Test]
        //[Category("Positive"), Category("sSgDg")]
        //public void Get_AddressesTransactions_sSgDg_Pos()
        //{
        //    // Arrange 
        //    var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
        //    string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
        //    string address = senderAddress.Address; // Address

        [Test]
        [Category("Positive"), Category("sSgDg")]
        public void Get_AddressesTransactions_sSgDg_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Btc")]
        public void Get_AddressesTransactions_Btc_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetBtcAddress("Sender", environment); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Eth")]
        public void Get_AddressesTransactions_Eth_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey);
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Deserialization
            JArray objects = JArray.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects[0].Select(m => (string)m.SelectToken("GasUsed")), Is.Not.Null, message: "GasUsed:");
        }


        [Test]
        [Category("Positive")]
        public void Get_AddressesTransactions_Limit1_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute with limit = 1
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature)
                                              .AddQueryParameter("limit", "1"));

            // Assert status code and verify the returned list
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            var addressTxnItemList = JsonConvert.DeserializeObject<List<GetTransactionsResponse>>(response.Content);
            Assert.IsTrue(addressTxnItemList.Count == 1);
        }


        [Test]
        [Category("Positive")]
        public void Get_AddressesTransactions_Limit99_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Call endpoint with limit = 99
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{ECurrency.sUsdcg}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature)
                                              .AddQueryParameter("limit", "99"));
            // Assert status code
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            var addressTxnItemList = JsonConvert.DeserializeObject<List<GetTransactionsResponse>>(response.Content);

            // Assert the list count to 99
            Assert.IsTrue(addressTxnItemList.Count == 99);
        }


        [Test]
        [Category("Positive")]
        public void Get_AddressesTransactions_Limit101_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Call endpoint with limit = 101
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{ECurrency.sUsdcg}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature)
                                              .AddQueryParameter("limit", "101"));

            // Assert status code
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            var addressTxnItemList = JsonConvert.DeserializeObject<List<GetTransactionsResponse>>(response.Content);

            // Assert that the list count doesn't go over 100
            Assert.IsTrue(addressTxnItemList.Count == 100);
        }


        [Test]
        [Category("Positive")]
        public void Get_AddressesTransactions_Limit0_Pos()
        {
            TestContext.WriteLine("There is a known bug for this test case -- See https://gluwa.atlassian.net/browse/GLA-704");

            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Call endpoint with limit = 0
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{ECurrency.sUsdcg}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature)
                                              .AddQueryParameter("limit", "0"));
            // Assert status code
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_AddressesTransactions_WithParameters_Eth_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey);
            string address = senderAddress.Address;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}?limit=1&offset=1"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_AddressesTransactions_Limit0_Eth_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey);
            string address = senderAddress.Address;

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}?limit=0"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Deserialization
            JArray objects = JArray.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects[0].Select(m => (string)m.SelectToken("GasUsed")), Is.Not.Null, message: "GasUsed:");
        }


        [Test]
        [Category("Positive"), Category("Gcre")]
        public void Get_AddressesTransactions_Gcre_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey);
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Deserialization
            JArray objects = JArray.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects[0].Select(m => (string)m.SelectToken("GasUsed")), Is.Not.Null, message: "GasUsed:");
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_AddressesTransactions_Usdc_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey);
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));

            // Deserialization
            JArray objects = JArray.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects[0].Select(m => (string)m.SelectToken("GasUsed")), Is.Not.Null, message: "GasUsed:");
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_AddressesTransactions_Gate_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey);
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));

            // Deserialization
            JArray objects = JArray.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
            Assert.That(objects[0].Select(m => (string)m.SelectToken("GasUsed")), Is.Not.Null, message: "GasUsed:");
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_AddressesTransactions_Usdt_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey);
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Deserialization
            JArray objects = JArray.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects[0].Select(m => (string)m.SelectToken("GasUsed")), Is.Not.Null, message: "GasUsed:");
        }


        [Test]
        [Category("Negative"), Category("Ethereum")]
        public void Get_AddressesTransactions_InvalidCurrency_sUsdcg_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey);
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("currency is not a supported currency", response);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_AddressesTransactions_ExpiredSignature_Eth_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string address = senderAddress.Address; // Address
            string signature = currency.ToExpiredXRequestSignature(); // Expires signature

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("Signature is expired.", response);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_AddressesTransactions_ExpiredSignature_Usdc_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string address = senderAddress.Address; // Address
            string signature = currency.ToExpiredXRequestSignature(); // Expires signature

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("Signature is expired.", response);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_AddressesTransactions_ExpiredSignature_Gate_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string address = senderAddress.Address; // Address
            string signature = currency.ToExpiredXRequestSignature(); // Expires signature

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response);
            Assertions.HandleAssertionMessage("Signature is expired.", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum"), Category("Missing")]
        public void Get_AddressesTransactions_MissingSignature_Eth_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}"),
                                           Api.SendRequest(Method.GET));
            // .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("No request signature header was provided.", response);
        }


        [Test]
        [Category("Negative"), Category("Ethereum"), Category("Invalid")]
        public void Get_AddressesTransactions_InvalidSignature_Eth_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey);
            string address = senderAddress.Address;

            // Change signature to invalid format
            signature = signature + "111";

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/{currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("Address signature is not a valid base64-encoded value.", response);
        }


        [Test]
        [Category("Negative"), Category("Btc")]
        public void Get_AddressesTransactions_ExpiredSignature_Btc_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetBtcAddress("Sender", environment); // AddressItem to contain address for transactions
            string address = senderAddress.Address; // Address
            string signature = currency.ToExpiredXRequestSignature(); // Expires signature

            // Execute request
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.expiredSignature, response);
        }


        [Test]
        [Category("Negative"), Category("sUsdcg")]
        public void Get_AddressesTransactions_ExpiredSignature_sUsdcg_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string address = senderAddress.Address; // Address
            string signature = currency.ToExpiredXRequestSignature(); // Expires signature

            // Execute request
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.expiredSignature, response);
        }


        [Test]
        [Category("Negative"), Category("Usdcg")]
        public void Get_AddressesTransactions_InvalidCurrencyWithHyphen_Usdcg_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request_Signature
            string address = senderAddress.Address; // Address

            // Execute endpoint with format USD-G
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/USDC-G/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("USDC-G", "currency"), response);
        }


        [Test]
        [Category("Negative"), Category("Btc")]
        public void Get_AddressesTransactions_AddressMismatch_Btc_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetBtcAddress("Sender", environment); // This would be a BTC Address
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute with USDCG not BTC
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/USDCG/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.forbiddenSignature, response);
        }


        [Test]
        [Category("Negative"), Category("sUsdcg")]
        public void Get_AddressesTransactions_AddressMismatch_sUsdcg_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // This would be a BTC Address
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute with sUSDCG not BTC
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/BTC/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.forbiddenSignature, response);
        }


        [Test]
        [Category("Negative")]
        public void Get_AddressesTransactions_MissingSignature_Neg()
        {
            // Address
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain the address to get txns
            string address = senderAddress.Address; // Address

            // Execute with no Signature
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.missingSignature, response);
        }


        [Test]
        [Category("Negative")]
        public void Get_AddressesTransactions_InvalidSignature_Neg()
        {
            // Address
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain the address to get txns
            string address = senderAddress.Address; // Address

            // Execute with invalid signature
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", "invalidSignature"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.invalidAddressSignatureFormat, response);
        }


        [Test]
        [Category("Negative")]
        public void Get_AddressesTransactions_LimitDecimalInvalidUrlParameters_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute with decimal limit
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature)
                                              .AddQueryParameter("limit", "0.01"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("0.01", "limit"), response);
        }


        [Test]
        [Category("Negative")]
        public void Get_AddressesTransactions_LimitNegativeInvalidUrlParameters_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute with -1
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature)
                                              .AddQueryParameter("limit", "-1"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("-1", "limit"), response);
        }


        [Test]
        [Category("Negative")]
        public void Get_AddressesTransactions_LimitCharacterInvalidUrlParameters_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute limit = a
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature)
                                              .AddQueryParameter("limit", "a"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("a", "limit"), response);
        }


        [Test]
        [Category("Negative")]
        public void Get_AddressesTransactions_InvalidCurrency_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute with invalid currency
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/foobar/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("foobar", "currency"), response);
        }


        [Test]
        [Category("Negative"), Category("Unsupported currency")]
        public void Get_AddressesTransactions_UnsupportedCurrency_Usdg_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/USDG/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.ServiceUnavailable, response, environment);
            Assertions.HandleAssertionMessage("Unsupported currency", response);
        }


        [Test]
        [Category("Negative"), Category("Unsupported currency")]
        public void Get_AddressesTransactions_Krwg_UnsupportedCurrency_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/KRWG/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.ServiceUnavailable, response, environment);
            Assertions.HandleAssertionMessage("Unsupported currency", response);
        }


        [Test]
        [Category("Negative"), Category("Bnb")]
        public void Get_AddressesTransactions_BNB_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem to contain address for transactions
            string signature = SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey); // X-Request-Signature
            string address = senderAddress.Address; // Address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Transactions/Ethereum/Addresses/{address}/BNB"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.ServiceUnavailable, response);
            Assertions.HandleAssertionMessage("Unsupported currency", response);
        }
    }
}
