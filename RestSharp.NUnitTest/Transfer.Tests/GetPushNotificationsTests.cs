using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Linq;
using System.Net;

namespace Transfer.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    [Category("PushNotifications"), Category("Get")]
    public class GetPushNotificationsTests
    {
        private string environment;
        private ECurrency currency;
        public GetPushNotificationsTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            currency = TestSettingsUtil.Currency;
        }

        [Test]
        [Category("Positive"), Category("Btc")]
        public void Get_PushNotifications_Btc_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetBtcAddress("Sender", environment); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive"), Category("NgNg")]
        public void Get_PushNotifications_NgNg_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive"), Category("sNgNg")]
        public void Get_PushNotifications_sNgNg_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive"), Category("sUsdcg")]
        public void Get_PushNotifications_sUsdcg_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive"), Category("Usdcg")]
        public void Get_PushNotifications_Usdcg_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive"), Category("Gtd")]
        public void Get_PushNotifications_Gtd_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive"), Category("Gate")]
        public void Get_PushNotifications_Gate_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive"), Category("sSgDg")]
        public void Get_PushNotifications_sSgDg_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Receiver"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_PushNotifications_Eth_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Receiver"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive"), Category("Gcre")]
        public void Get_PushNotifications_Gcre_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_PushNotifications_Usdt_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Receiver"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_PushNotifications_Usdc_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive"), Category("Ethereum")]
        public void Get_PushNotifications_BnbUsdc_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{"BNBUSDC"}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
        }


        [Test]
        [Category("Positive")]
        public void Get_PushNotifications_NoAuth_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Create client with no auth
            var client = Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications");
            client.Authenticator = null;

            // Execute
            IRestResponse response = Api.GetResponse(client, Api.SendRequest(Method.GET)
                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive")]
        public void Get_PushNotifications_AllCurrencies_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive")]
        public void Get_PushNotifications_AllCurrencies_WithParams_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address.Address}/PushNotifications?limit=10&offset=10"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Deserialization 
            var objects = JArray.Parse(JObject.Parse(response.Content).GetValue("Items").ToString());

            // Assert
            Assert.AreEqual(10, objects.Count);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive")]
        public void Get_PushNotifications_AllCurrencies_CheckOffset_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address.Address}/PushNotifications?limit=10"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));

            // Get Id of 10th element in the notifications list
            var lastId = JArray.Parse(JObject.Parse(response.Content).GetValue("Items").ToString()).Last().SelectToken("ID").ToString();

            // Execute
            IRestResponse responseNext = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address.Address}/PushNotifications?offset=9"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));

            // Get Id of 1st element in the notifications offset list
            var firstId = JArray.Parse(JObject.Parse(responseNext.Content).GetValue("Items").ToString()).First().SelectToken("ID").ToString();
            // Assert
            Assert.AreEqual(lastId, firstId);
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Positive")]
        public void Get_PushNotifications_AllCurrencies_ZeroLimitAndOffset_Pos()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address.Address}/PushNotifications?limit=0&offset=0"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response,environment);
        }


        [Test]
        [Category("Negative"), Category("InvalidValue")]
        public void Get_PushNotifications_InvalidCurrency_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute Push Notifications endpoint with mismatched address and private key
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/foobar/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response,environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("foobar", "currency"), response);
        }


        [Test]
        [Category("Negative"), Category("Usdcg"), Category("InvalidValue")]
        public void Get_PushNotifications_InvalidCurrencyWithHyphen_Usdcg_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Call Push Notifications endpoint with mismatched address and private key
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/USDC-G/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response,environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("USDC-G", "currency"), response);
        }


        [Test]
        [Category("Negative"), Category("NgNg"), Category("Forbidden")]
        public void Get_PushNotifications_Forbidden_NgNg_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");

            // Call Push Notifications endpoint with mismatched address and private key
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(receiverAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response,environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.forbiddenSignature, response);
        }


        [Test]
        [Category("Negative"), Category("sNgNg"), Category("Forbidden")]
        public void Get_PushNotifications_Forbidden_sNgNg_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver"); // AddressItem Receiver for forbidden push notifications

            // Call Push Notifications endpoint with mismatched address and private key
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(receiverAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response,environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.forbiddenSignature, response);
        }


        [Test]
        [Category("Negative"), Category("Forbidden")]
        public void Get_PushNotifications_Forbidden_Btc_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetBtcAddress("Sender", environment); // AddressItem for push notifications
            var receiverAddress = QAKeyVault.GetBtcAddress("Receiver", environment);

            // Call Push Notifications endpoint with mismatched address and private key
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(receiverAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response,environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.forbiddenSignature, response);
        }


        [Test]
        [Category("Negative"), Category("Forbidden")]
        public void Get_PushNotifications_Forbidden_sUsdcg_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications
            var receiverAddress = QAKeyVault.GetGluwaAddress("Receiver");

            // Call Push Notifications endpoint with mismatched address and private key
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(receiverAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response,environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.forbiddenSignature, response);
        }


        [Test]
        [Category("Negative"), Category("Forbidden")]
        public void Get_PushNotifications_Forbidden_Usdcg_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetBtcAddress("Sender", environment); // AddressItem for push notifications
            var receiverAddress = QAKeyVault.GetBtcAddress("Receiver", environment);

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(receiverAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response,environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.forbiddenSignature, response);
        }


        [Test]
        [Category("Negative"), Category("SignatureMissing")]
        public void Get_PushNotifications_MissingSignature_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute without Signature
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response,environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.missingSignature, response);
        }


        [Test]
        [Category("Negative")]
        public void Get_PushNotifications_AllCurrencies_InvalidLimit_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address.Address}/PushNotifications?limit=-1"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response,environment);
            Assertions.HandleAssertionInnerMessage("The value '-1' is not valid.", response);
        }


        [Test]
        [Category("Negative")]
        public void Get_PushNotifications_AllCurrencies_InvalidOffset_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender"); // AddressItem for push notifications

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address.Address}/PushNotifications?offset=-1"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response,environment);
            Assertions.HandleAssertionInnerMessage("The value '-1' is not valid.", response);
        }


        [Test]
        [Category("Negative")]
        public void Get_PushNotifications_AllCurrencies_NoAuthorizedUser_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");
            var invalidAddress = QAKeyVault.GetGluwaAddress("MainnetSender");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(invalidAddress.PrivateKey)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response,environment);
        }


        [Test]
        [Category("Negative")]
        public void Get_PushNotifications_AllCurrencies_NoSignature_Neg()
        {
            // Arrange
            var address = QAKeyVault.GetGluwaAddress("Sender");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Addresses/{address.Address}/PushNotifications"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Forbidden, response,environment);
        }
    }
}
