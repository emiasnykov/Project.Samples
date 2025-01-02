using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using NUnit.Framework;
using RestSharp;
using System.Linq;
using System.Net;

namespace Transfer.Tests
{
    [TestFixture("Test")]
    //[TestFixture("Staging")]
    //[TestFixture("Production")]
    [Category("Transactions"), Category("Post")]
    public class PostTransactionReplaceDataTests : TransferFunctions
    {
        private string environment;
        private ECurrency currency;

        public PostTransactionReplaceDataTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            currency = TestSettingsUtil.Currency;

            // Ignore positive tests on Mainnet
            if (TestContext.CurrentContext.Test.Properties["Category"].Contains("Manual"))
            {
                Assert.Ignore($"Cannot run this test on environment: {environment}");
            }
        }

        [Test]
        [Category("Positive"), Category("Ctc")]
        public void Post_Transactions_ReplaceDataByTxnId_Gcre_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            string TxnId = GetEthTransactionDetails(address, currency).Value<string>("ID");

            // Create body
            PostTransactionReplaceDataBody body = RequestTestBody.CreateTransactionReplaceDataBody(TxnId,
                                                                                                   null,
                                                                                                   null,
                                                                                                   null,
                                                                                                   "0",
                                                                                                   currency);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Ethereum/CreateReplaceData"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Post_Transactions_ReplaceDataByTxnId_Usdc_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            string TxnId = GetEthTransactionDetails(address, currency).Value<string>("ID");

            // Create body
            PostTransactionReplaceDataBody body = RequestTestBody.CreateTransactionReplaceDataBody(TxnId,
                                                                                                   null,
                                                                                                   null,
                                                                                                   null,
                                                                                                   "1",
                                                                                                   currency);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Ethereum/CreateReplaceData"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Post_Transactions_ReplaceDataByTxnId_Usdt_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            string TxnId = GetEthTransactionDetails(address, currency).Value<string>("ID");

            // Create body
            PostTransactionReplaceDataBody body = RequestTestBody.CreateTransactionReplaceDataBody(TxnId,
                                                                                                   null,
                                                                                                   null,
                                                                                                   null,
                                                                                                   "0",
                                                                                                   currency);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Ethereum/CreateReplaceData"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Post_Transactions_ReplaceDataByTxnId_Eth_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            string TxnId = GetEthTransactionDetails(address, currency).Value<string>("ID");

            // Create body
            PostTransactionReplaceDataBody body = RequestTestBody.CreateTransactionReplaceDataBody(TxnId,
                                                                                                   null,
                                                                                                   null,
                                                                                                   null,
                                                                                                   "0",
                                                                                                   currency);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Ethereum/CreateReplaceData"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Ctc")]
        public void Post_Transactions_ReplaceDataByTxHash_Gcre_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            string TxnHash = GetEthTransactionDetails(address, currency).Value<string>("TxnHash");

            // Create body
            PostTransactionReplaceDataBody body = RequestTestBody.CreateTransactionReplaceDataBody(null,
                                                                                                   null,
                                                                                                   TxnHash,
                                                                                                   null,
                                                                                                   "1",
                                                                                                   currency);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Ethereum/CreateReplaceData"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Ctc")]
        public void Post_Transactions_ReplaceDataByIdem_Gcre_Pos()
        {
            // Create body
            PostTransactionReplaceDataBody body = RequestTestBody.CreateTransactionReplaceDataBody(null,
                                                                                                   null,
                                                                                                   null,
                                                                                                   Shared.VALID_IDEM,
                                                                                                   "1",
                                                                                                   currency);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Ethereum/CreateReplaceData"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive"), Category("Ctc")]
        public void Post_Transactions_ReplaceDataBySignature_Gcre_Pos()
        {
            // Create body
            PostTransactionReplaceDataBody body = RequestTestBody.CreateTransactionReplaceDataBody(null,
                                                                                                   Shared.VALID_SIGNATURE,
                                                                                                   null,
                                                                                                   null,
                                                                                                   "1",
                                                                                                   currency);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Ethereum/CreateReplaceData"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        // Use manually. Replacing of the given transaction is available once only.
        [Test]
        [Category("Positive"), Category("Manual")]
        public void Put_Transactions_ReplaceData_Eth_Pos()
        {
            // Pending transaction
            var txnId = "";

            // Set up testing address and currency
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");
            decimal amount = decimal.Parse("0.000001");
            decimal fee = decimal.Parse("0");

            // Create body
            PutTransactionReplaceDataBody body = RequestTestBody.CreatePutTransactionReplaceDataBody(txnId,
                                                                                   senderAddress,
                                                                                   receiverAddress.Address,
                                                                                   currency,
                                                                                   amount,
                                                                                   fee,
                                                                                   environment);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Ethereum/TransferReplace"),
                                     Api.SendRequest(Method.PUT, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Negative")]
        public void Post_TransactionReplaceData_UnsupportedCurrency_Btc_Pos()
        {
            // Create body
            PostTransactionReplaceDataBody body = RequestTestBody.CreateTransactionReplaceDataBody(null,
                                                                                                   null,
                                                                                                   Shared.VALID_TXNID,
                                                                                                   null,
                                                                                                   "2",
                                                                                                   currency);
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Transactions/Ethereum/CreateReplaceData"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
        }
    }
}
