using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Transfer.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    [Category("Transactions"), Category("Get")]
    public class GetTransactionsByTypeTests
    {
        private string environment;
        private ECurrency currency;
        private string keyName;

        public GetTransactionsByTypeTests(string environment)
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
        public void Get_TransactionsByType_Default_sUsdcg_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE",
                                                          SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> Items = objects["Items"].Select(m => (string)m.SelectToken("ID")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyCurrencyMatch(objects, currency);
            Assert.That(objects["Items"].Count, Is.LessThanOrEqualTo(25), message: "Transactions count:");
            Assert.That(objects.SelectToken("TotalCount").ToString(), Is.Not.Null, message: "TotalCount property presence:");
        }


        [Test]
        public void Get_TransactionsByType_Usdcg_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyCurrencyMatch(objects, currency);
        }


        [Test]
        public void Get_TransactionsByType_NgNg_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyCurrencyMatch(objects, currency);
        }


        [Test]
        public void Get_TransactionsByType_sNgNg_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyCurrencyMatch(objects, currency);
        }

        //// Service temporarily unavailable
        //[Test]
        //public void Get_TransactionsByType_sSgDg_Pos()
        //{
        //    // Arrange 
        //    var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
        //    string address = senderAddress.Address;

        //    // Execute 
        //    var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?currency={currency}"),
        //                                   Api.SendRequest(Method.GET)
        //                                      .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
        //    // Deserialization
        //    JObject objects = JObject.Parse(response.Content);

            // Assert 
        //    Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        //    Assertions.VerifyCurrencyMatch(objects, currency);
        //}


        [Test]
        public void Get_TransactionsByType_GatewayDAO_Gtd_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;
            string transactionsType = "GatewayDao";

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type={transactionsType}&currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyCurrencyMatch(objects, currency);
        }


        [Test]
        public void Get_TransactionsByType_GatewayDAO_Gate_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;
            string transactionsType = "GatewayDao";

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type={transactionsType}&currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> transactionsTypeList = objects["Items"].Select(m => (string)m.SelectToken("Type")).ToList();
            IList<string> transactionsCurrencyList = objects["Items"].Select(m => (string)m.SelectToken("Currency")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(transactionsTypeList, Has.All.EqualTo($"{transactionsType}"), message: "Transactions type:");
            Assert.That(transactionsCurrencyList, Has.All.EqualTo($"{currency.ToString().ToUpper()}"), message: "Currency:");
        }


        [Test]
        public void Get_TransactionsByType_GatewayDAO_Gcre_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;
            string transactionsType = "GatewayDao";

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type={transactionsType}&currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> transactionsTypeList = objects["Items"].Select(m => (string)m.SelectToken("Type")).ToList();
            IList<string> transactionsCurrencyList = objects["Items"].Select(m => (string)m.SelectToken("Currency")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(transactionsTypeList, Has.All.EqualTo($"{transactionsType}"), message: "Transactions type:");
            Assert.That(transactionsCurrencyList, Has.All.EqualTo($"{currency.ToString().ToUpper()}"), message: "Currency:");
        }


        [Test]
        public void Get_TransactionsByType_Btc_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetBtcAddress("Sender", environment);
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyCurrencyMatch(objects, currency);
        }


        [Test]
        public void Get_TransactionsByType_Default_Eth_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));

            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> transactionsCurrency = objects["Items"].Select(m => (string)m.SelectToken("Currency")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyCurrencyMatch(objects, currency);
            Assert.That(objects["Items"].Count, Is.LessThanOrEqualTo(25), message: "Transactions count:");
            Assert.That(objects.SelectToken("TotalCount").ToString(), Is.Not.Null, message: "TotalCount property presence:");
            Assert.That(objects["Items"][0].Select(m => (string)m.SelectToken("GasUsed")), Is.Not.Null, message: "GasUsed:");
        }


        [Test]
        public void Get_TransactionsByType_Gcre_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?Currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> transactionsCurrency = objects["Items"].Select(m => (string)m.SelectToken("Currency")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyCurrencyMatch(objects, currency);
        }


        [Test]
        public void Get_TransactionsByType_Usdc_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?Currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> transactionsCurrency = objects["Items"].Select(m => (string)m.SelectToken("Currency")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyCurrencyMatch(objects, currency);
            Assert.That(objects["Items"][0].Select(m => (string)m.SelectToken("GasUsed")), Is.Not.Null, message: "GasUsed:");
        }


        [Test]
        public void Get_TransactionsByType_Gate_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?Currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> transactionsCurrency = objects["Items"].Select(m => (string)m.SelectToken("Currency")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
            Assertions.VerifyCurrencyMatch(objects, currency);
            Assert.That(objects["Items"][0].Select(m => (string)m.SelectToken("GasUsed")), Is.Not.Null, message: "GasUsed:");
        }


        [Test]
        public void Get_TransactionsByType_Usdt_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?Currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> transactionsCurrency = objects["Items"].Select(m => (string)m.SelectToken("Currency")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.VerifyCurrencyMatch(objects, currency);
            Assert.That(objects["Items"][0].Select(m => (string)m.SelectToken("GasUsed")), Is.Not.Null, message: "GasUsed:");
        }


        [Test]
        public void Get_TransactionsByType_Pay_Eth_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string address = senderAddress.Address;
            string transactionsType = "Pay";

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?type={transactionsType}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> transactionsTypeList = objects["Items"].Select(m => (string)m.SelectToken("Type")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(transactionsTypeList, Has.All.EqualTo($"{transactionsType}"), message: "Transactions type:");
        }


        [Test]
        public void Get_TransactionsByType_Pay_Gcre_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string address = senderAddress.Address;
            string transactionsType = "Pay";

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?Currency={currency}&type={transactionsType}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> transactionsTypeList = objects["Items"].Select(m => (string)m.SelectToken("Type")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(transactionsTypeList, Has.All.EqualTo($"{transactionsType}"), message: "Transactions type:");
        }


        [Test]
        public void Get_TransactionsByType_Pay_Gate_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;
            string transactionsType = "Pay";

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?type={transactionsType}&currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> transactionsTypeList = objects["Items"].Select(m => (string)m.SelectToken("Type")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
            Assert.That(transactionsTypeList, Has.All.EqualTo($"{transactionsType}"), message: "Transactions type:");
        }


        [Test]
        public void Get_TransactionsByType_Pay_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;
            string transactionsType = "Pay";

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type={transactionsType}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> transactionsTypeList = objects["Items"].Select(m => (string)m.SelectToken("Type")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(transactionsTypeList, Has.All.EqualTo($"{transactionsType}"), message: "Transactions type:");
        }


        [Test]
        public void Get_TransactionsByType_Invest_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type=invest"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual("INVEST", objects.SelectToken("Items[0].Type").ToString().ToUpper(), message: "Transactions type:");
        }


        [Test]
        public void Get_TransactionsByType_Invest_Usdc_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?currency={currency}&type=invest"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
            Assert.AreEqual("INVEST", objects.SelectToken("Items[0].Type").ToString().ToUpper(), message: "Transactions type:");
            Assert.That(objects["Items"][0].Select(m => (string)m.SelectToken("FtaTxnType")), Is.Not.Null, message: "Fta transaction type:");
            Assert.That(objects["Items"][0].Select(m => (string)m.SelectToken("PoolName")), Is.Not.Null, message: "Pool name:");
        }


        [Test]
        public void Get_TransactionsByType_Fta_Usdc_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type=invest&currency={currency}&accounttype=Fta"),
                                           Api.SendRequest(Method.GET)
                                           .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));

            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            dynamic data = JToken.Parse(objects.SelectToken("Items").ToString());

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
            Assert.Multiple(() =>
            {
                Assert.That((string)data[0].SelectToken("ID"), Is.Not.Null, message: "ID");
                Assert.That((string)data[0].SelectToken("Currency"), Is.Not.Null, message: "Currency");
                Assert.That((string)data[0].SelectToken("FtaTxnType"), Is.Not.Null, message: "FtaTxnType");
                Assert.That((string)data[0].SelectToken("FromAddress"), Is.Not.Null, message: "FromAddress");
                Assert.That((string)data[0].SelectToken("Amount"), Is.Not.Null, message: "Amount");
                Assert.That((string)data[0].SelectToken("PoolName"), Is.Not.Null, message: "PoolName");
                Assert.That((string)data[0].SelectToken("PoolID"), Is.Not.Null, message: "PoolID");
                Assert.That((string)data[0].SelectToken("PoolLogoUrl"), Is.Not.Null, message: "PoolLogoUrl");
                Assert.That((string)data[0].SelectToken("CreatedDateTime"), Is.Not.Null, message: "CreatedDateTime");
                Assert.That((string)data[0].SelectToken("ModifiedDateTime"), Is.Not.Null, message: "ModifiedDateTime");
                Assert.That((string)data[0].SelectToken("Type"), Is.Not.Null, message: "Type");
                Assert.That((string)data[0].SelectToken("TxnHash"), Is.Not.Null, message: "TxnHash");
                Assert.That((string)data[0].SelectToken("Status"), Is.Not.Null, message: "Status");
                Assert.That((string)data[0].SelectToken("GasUsed"), Is.Not.Null, message: "GasUsed");
            });
        }


        [Test]
        public void Get_TransactionsByType_OffsetLimit_Fta_Usdc_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type=invest&currency={currency}&accounttype=Fta&offset=0&limit=5"),
                                           Api.SendRequest(Method.GET)
                                           .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));

            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            dynamic data = JToken.Parse(objects.SelectToken("Items").ToString());

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
            Assert.Multiple(() =>
            {
                Assert.That((string)data[0].SelectToken("ID"), Is.Not.Null, message: "ID");
                Assert.That((string)data[0].SelectToken("Currency"), Is.Not.Null, message: "Currency");
                Assert.That((string)data[0].SelectToken("FtaTxnType"), Is.Not.Null, message: "FtaTxnType");
                Assert.That((string)data[0].SelectToken("FromAddress"), Is.Not.Null, message: "FromAddress");
                Assert.That((string)data[0].SelectToken("Amount"), Is.Not.Null, message: "Amount");
                Assert.That((string)data[0].SelectToken("PoolName"), Is.Not.Null, message: "PoolName");
                Assert.That((string)data[0].SelectToken("PoolID"), Is.Not.Null, message: "PoolID");
                Assert.That((string)data[0].SelectToken("PoolLogoUrl"), Is.Not.Null, message: "PoolLogoUrl");
                Assert.That((string)data[0].SelectToken("CreatedDateTime"), Is.Not.Null, message: "CreatedDateTime");
                Assert.That((string)data[0].SelectToken("ModifiedDateTime"), Is.Not.Null, message: "ModifiedDateTime");
                Assert.That((string)data[0].SelectToken("Type"), Is.Not.Null, message: "Type");
                Assert.That((string)data[0].SelectToken("TxnHash"), Is.Not.Null, message: "TxnHash");
                Assert.That((string)data[0].SelectToken("Status"), Is.Not.Null, message: "Status");
                Assert.That((string)data[0].SelectToken("GasUsed"), Is.Not.Null, message: "GasUsed");
            });
        }


        [Test]
        public void Get_TransactionsByType_NoSign_Fta_Usdc_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type=invest&currency={currency}&accounttype=Fta"),
                                           Api.SendRequest(Method.GET));

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response);
        }


        [Test]
        public void Get_TransactionsByType_InvalidCurrency_Fta_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type=invest&currency={"DOGE"}&accounttype=Fta"),
                                           Api.SendRequest(Method.GET)
                                           .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage("one of more Url parameters are invalid.", response);
            Assertions.HandleAssertionInnerMessage($"The value {"'DOGE'"} is not valid.", response);
        }


        [Test]
        public void Get_TransactionsByType_MissingCurrency_Fta_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type=invest&currency={""}&accounttype=Fta"),
                                           Api.SendRequest(Method.GET)
                                           .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage("one of more Url parameters are invalid.", response);
            Assertions.HandleAssertionInnerMessage($"The value {"''"} is invalid.", response);
        }


        [Test]
        public void Get_TransactionsByType_AccountType_Invest_Fta_Usdc_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type=invest&currency={currency}&accounttype=invest"),
                                           Api.SendRequest(Method.GET)
                                           .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            dynamic data = JToken.Parse(objects.SelectToken("InnerErrors").ToString());

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage("one of more Url parameters are invalid.", response);
            Assert.That((string)data[0].SelectToken("Path"), Is.EqualTo("accountType"));
            Assertions.HandleAssertionInnerMessage($"The value {"'invest'"} is not valid.", response);
        }


        [Test]
        public void Get_TransactionsByType_TypeLottery_Fta_Usdc_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type=lottery&currency={currency}&accounttype=Fta"),
                                           Api.SendRequest(Method.GET)
                                           .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            dynamic data = JToken.Parse(objects.SelectToken("InnerErrors").ToString());

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage("one of more Url parameters are invalid.", response);
            Assert.That((string)data[0].SelectToken("Path"), Is.EqualTo("type"));
            Assertions.HandleAssertionInnerMessage($"The value {"'lottery'"} is not valid.", response);
        }


        [Test]
        public void Get_TransactionsByType_Exchange_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetBtcAddress("Sender", environment);
            var currency = ECurrency.Btc;
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?type=exchange&currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            if (environment == "Staging")
            {
                Assert.Ignore("Cannot run exchange tests in Staging environment");
            }
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual("EXCHANGE", objects.SelectToken("Items[0].Type").ToString().ToUpper(), message: "Transactions type:");
        }


        [Test]
        public void Get_TransactionsByType_ValidLimitAndOffset_Eth_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string address = senderAddress.Address;
            string offset = "1";
            string limit = "1";

            // Execute with no parameters to extract transaction on offset position 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));

            // Parse and extract transaction with index = offset
            JObject transactions = JObject.Parse(response.Content);
            var transactionId = transactions["Items"][int.Parse(offset)].SelectToken("ID").ToString();

            // Execute with offset parameter
            var responseLimitOffset = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?offset={offset}&limit={limit}"),
                                                      Api.SendRequest(Method.GET)
                                                         .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Parse response
            JObject objects = JObject.Parse(responseLimitOffset.Content);
            IList<string> itemIDs = objects["Items"].Select(m => (string)m.SelectToken("ID")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, responseLimitOffset, environment);
            Assert.That(itemIDs[0], Is.EqualTo(transactionId), message: "Offset transactionID:");
            Assert.That(itemIDs.Count, Is.EqualTo(int.Parse(limit)), message: "Transaction Count:");
        }


        [Test]
        public void Get_TransactionsByType_ZeroLimitAndOffset_Eth_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string address = senderAddress.Address;
            string offset = "0";
            string limit = "0";

            // Execute with no parameters to extract transaction on offset position 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));

            // Parse and extract transaction with index = offset
            JObject transactions = JObject.Parse(response.Content);
            var transactionId = transactions["Items"][int.Parse(offset)].SelectToken("ID").ToString();

            // Execute with offset parameter
            var responseOffset = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?offset={offset}&limit={limit}"),
                                                 Api.SendRequest(Method.GET)
                                                    .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Parse second response
            JObject objects = JObject.Parse(responseOffset.Content);
            IList<string> itemIDs = objects["Items"].Select(m => (string)m.SelectToken("ID")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, responseOffset, environment);
            Assert.That(itemIDs[0], Is.EqualTo(transactionId), message: "Offset transactionID:");
            Assert.That(itemIDs.Count, Is.LessThanOrEqualTo(25), message: "Transaction Count:");
        }


        [Test]
        public void Get_TransactionsByType_ValidLimitAndOffset_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?limit=4&offset=2"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> Items = objects["Items"].Select(m => (string)m.SelectToken("ID")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual(4, Items.Count, message: "Transactions count:");
        }


        [Test]
        public void Get_TransactionsByType_ZeroLimitAndOffset_Pos()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?limit=0&offset=0"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            IList<string> Items = objects["Items"].Select(m => (string)m.SelectToken("ID")).ToList();

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(Items.Count, Is.LessThanOrEqualTo(25), message: "Transactions count:");
        }


        [Test]
        public void Get_TransactionsByType_InvalidCurrency_Eth_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;
            string currency = "Ethemereumnem"; // Invalid currency

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?Currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{currency}' is not valid.", response);
        }


        [Test]
        public void Get_TransactionsByType_UnsupportedCurrency_Eth_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;
            string currency = "BTC"; // Currency not supported by the endpoint

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?Currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        public void Get_TransactionsByType_NegativeLimit_Eth_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;
            string invalidLimit = "-5";

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?limit={invalidLimit}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{invalidLimit}' is not valid.", response);
        }


        [Test]
        public void Get_TransactionsByType_NegativeOffset_Eth_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;
            string invalidOffset = "-2";

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?offset={invalidOffset}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{invalidOffset}' is not valid.", response);
        }


        [Test]
        public void Get_TransactionsByType_NegativeLimit_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{senderAddress}/Transactions?limit=-1"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE",
                                                          SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("one of more Url parameters are invalid.", response);
        }


        [Test]
        public void Get_TransactionsByType_NegativeOffset_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{senderAddress}/Transactions?offset=-1"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE",
                                                          SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("one of more Url parameters are invalid.", response);
        }


        [Test]
        public void Get_TransactionsByType_InvalidType_Eth_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;
            string transactionType = "Invest"; // Invalid transaction type

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum?Type={transactionType}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("type is not a supported transaction type", response);
        }


        [Test]
        public void Get_TransactionsByType_AddressMismatch_Eth_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = "0x3B5149E01c39518Afe0943462a7c3bF62E3c1F1a"; //Unassociated address

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        public void Get_TransactionsByType_InvalidAddress_Eth_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address + "1232224"; // Invalid address

            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("The supplied signature is not authorized for this resource.", response);
        }


        [Test]
        public void Get_TransactionsByType_ExpiredSignature_Eth_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;
            string signature = ECurrency.Eth.ToExpiredXRequestSignature();

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions/Ethereum"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
            Assertions.HandleAssertionMessage("Signature is expired.", response);
        }


        [Test]
        public void Get_TransactionsByType_NoAuthorizedUser_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("MainnetSender");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{senderAddress}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE",
                                                          SignaturesUtil.GetXRequestSignature(receiverAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
        }


        [Test]
        public void Get_TransactionsByType_NoSignature_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions"),
                                           Api.SendRequest(Method.GET));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
        }


        [Test]
        public void Get_TransactionsByType_InvalidAddress_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string invalidAddress = senderAddress.Address.Remove(0, 3);

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{invalidAddress}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response, environment);
        }


        [Test]
        public void Get_TransactionsByType_UnsupportedCurrency_Neg()
        {
            // Arrange 
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string address = senderAddress.Address;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address}/Transactions?currency=Krwg"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.ServiceUnavailable, response, environment);
            Assertions.HandleAssertionMessage("Unsupported currency", response);
        }
    }
}
