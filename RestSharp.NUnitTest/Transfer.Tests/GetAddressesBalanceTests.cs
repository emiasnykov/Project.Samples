using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Models.ResponseBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Transfer.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    [Category("Address"), Category("Get")]
    public class GetAddressesBalanceTests
    {
        private string environment;
        private string keyName;
        private ECurrency currency;

        public GetAddressesBalanceTests(string environment)
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
        [Category("Positive"), Category("NgNg")]
        public void Get_AddressesBalance_NgNg_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance item
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status code
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), balanceItem.Currency, balanceItem.Balance); // Correct currency & balance
        }


        [Test]
        [Category("Positive"), Category("sNgNg")]
        public void Get_AddressesBalance_sNgNg_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance item
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status code
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), balanceItem.Currency, balanceItem.Balance); // Correct currency & balance
        }


        [Test]
        [Category("Positive"), Category("Btc")]
        public void Get_AddressesBalance_Btc_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetBtcAddress("Sender", environment);

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance item
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status code
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), balanceItem.Currency, balanceItem.Balance); // Correct currency & balance
        }


        [Test]
        [Category("Positive"), Category("sUsdcg")]
        public void Get_AddressesBalance_sUsdcg_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance item
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status code
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), balanceItem.Currency, balanceItem.Balance); // Correct currency & balance
        }


        [Test]
        [Category("Positive"), Category("Usdcg")]
        public void Get_AddressesBalance_Usdcg_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance item
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status code
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), balanceItem.Currency, balanceItem.Balance); // Correct currency & balance
        }


        [Test]
        [Category("Positive"), Category("sSgDg")]
        public void Get_AddressesBalance_sSgDg_Pos()
        {
            // Arrange          
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance item
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status code
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), balanceItem.Currency, balanceItem.Balance); // Correct currency & balance
        }


        [Test]
        [Category("Positive"), Category("Gcre")]
        public void Get_AddressesBalance_Gcre_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetGluwaAddress(keyName);

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance item
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status code
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), balanceItem.Currency, balanceItem.Balance); // Correct currency & balance
        }


        [Test]
        [Category("Positive"), Category("Eth")]
        public void Get_AddressesBalance_Eth_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetGluwaAddress("Receiver");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance item
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status code
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), balanceItem.Currency, balanceItem.Balance); // Correct currency & balance
        }


        [Test]
        [Category("Positive"), Category("Usdt")]
        public void Get_AddressesBalance_Usdt_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetGluwaAddress(keyName);

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance item
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status code
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), balanceItem.Currency, balanceItem.Balance); // Correct currency & balance
        }


        [Test]
        [Category("Positive"), Category("Usdc")]
        public void Get_AddressesBalance_Usdc_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetGluwaAddress(keyName);

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract balance item
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status code
            Assertions.HandleCurrencyAndAmountAssertions(TestSettingsUtil.Currency.ToString(), balanceItem.Currency, balanceItem.Balance); // Correct currency & balance
        }


        [Test]
        [Category("Positive"), Category("Btc")]
        public void Get_AddressesBalance_IncludeUnspentOutputsTrue_Btc_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetBtcAddress("Sender", environment);

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddQueryParameter("includeUnspentOutputs", "true"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status Code
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content); // Balance Item
            Assert.IsTrue(balanceItem.UnspentOutputs.Count > 0); // Check that there are unspent outputs
        }


        [Test]
        [Category("Positive"), Category("Btc")]
        public void Get_AddressesBalance_IncludeUnspentOutput_ZeroBalance_Btc_Pos()
        {
            // Arrange address
            var address = Shared.GetZeroBalanceBtcAddress(environment);

            // Execute 'true' case
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address}"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddQueryParameter("includeUnspentOutputs", "true"));
            // Execute 'false' case
            IRestResponse nextresponse = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address}"),
                                         Api.SendRequest(Method.GET)
                                            .AddQueryParameter("includeUnspentOutputs", "false"));
            // Assert 'true' case
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status Code
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content); // Balance Item
            Assert.IsTrue(balanceItem.UnspentOutputs.Count == 0);
            Assert.AreEqual("0", balanceItem.Balance.ToString());

            // Assert 'false' case
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, nextresponse, environment); // Status Code
            GetBalanceResponse nextbalanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(nextresponse.Content); // Balance Item
            Assert.AreEqual("0", nextbalanceItem.Balance.ToString());
        }


        [Test]
        [Category("Positive"), Category("Usdcg")]
        public void Get_AddressesBalance_IncludeUnspentOutputsTrue_Eth_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddQueryParameter("includeUnspentOutputs", "true"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); // Status Code
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content); // Extract Balance item
            Assert.IsTrue(balanceItem.UnspentOutputs == null); // Check that no unspent outputs appear
        }


        [Test]
        [Category("Positive"), Category("Btc")]
        public void Get_AddressesBalance_IncludeUnspentOutputsFalse_Btc_Pos()
        {
            // Arrange address
            var address = QAKeyVault.GetBtcAddress("Sender", environment);

            // Excute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddQueryParameter("includeUnspentOutputs", "false"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content); // Get BalanceItem
            Assert.IsTrue(balanceItem.UnspentOutputs == null); // Check that no unspent outputs appear
        }


        [Test]
        [Category("Negative"), Category("InvalidValue")]
        public void Get_AddressesBalance_InvalidCurrency_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/foobar/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("foobar", "currency"), response);
        }


        [Test]
        [Category("Negative"), Category("InvalidValue")]
        public void Get_AddressesBalance_AddressMismatch_Neg()
        {
            // Arrange
            string btcAddress = QAKeyVault.GetBtcAddress("Sender", environment).Address;

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Usdcg/Addresses/{btcAddress}"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.invalidAddressQuery, response);
        }


        [Test]
        [Category("Negative")]
        public void Get_AddressesBalance_IncludeUnspentOutputsInvalidValue_Btc_Neg()
        {
            // Arrange address
            var address = QAKeyVault.GetBtcAddress("Sender", environment);

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddQueryParameter("includeUnspentOutputs", "a"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("a", "includeUnspentOutputs"), response);
        }
    }
}
