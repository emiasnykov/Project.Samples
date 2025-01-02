using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
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
    [Category("Gas"), Category("Get")]

    public class GetGasPriceAndLimitTests
    {
        private string environment;
        private ECurrency currency;

        public GetGasPriceAndLimitTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types and currency
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            currency = TestSettingsUtil.Currency;
        }


        [Test]
        [Category("Positive"), Category("Eth")]
        public void Get_GasPriceAndLimit_Eth_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Positive"), Category("sSgDg")]
        public void Get_GasPriceAndLimit_sSgDg_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"V1/{currency}/GasPriceAndLimit"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Positive"), Category("Usdc")]
        public void Get_GasPriceAndLimit_Usdc_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Positive"), Category("Usdt")]
        public void Get_GasPriceAndLimit_Usdt_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Positive"), Category("Gcre")]
        public void Get_GasPriceAndLimit_Gcre_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Positive"), Category("Gate")]
        public void Get_GasPriceAndLimit_Gate_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Positive"), Category("Eth")]
        public void Get_GasPriceAndLimit_Eth_TransferType_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?transactionType=Transfer"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Positive"), Category("Eth")]
        public void Get_GasPriceAndLimit_Eth_ExecuteType_Pos()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?transactionType=Execute"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Positive"), Category("Gcre")]
        public void Get_GasPriceAndLimit_Gcre_Erc20ApproveType_Pos()
        {
            // Set Transaction type
            string transactionType = "Erc20Approve";

            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?TransactionType={transactionType}"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Positive"), Category("Gtd")]
        public void Get_GasPriceAndLimit_Gtd_MintToStakeType_Pos()
        {
            // Set Transaction type
            string transactionType = "GtdMintToStake";

            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?TransactionType={transactionType}"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Positive"), Category("Gtd")]
        public void Get_GasPriceAndLimit_Gtd_UnstakeType_Pos()
        {
            // Set Transaction type
            string transactionType = "GtdUnstake";

            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?TransactionType={transactionType}"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Positive"), Category("Gtd")]
        public void Get_GasPriceAndLimit_Gtd_BurnType_Pos()
        {
            // Set Transaction type
            string transactionType = "GtdBurn";

            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?TransactionType={transactionType}"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Positive"), Category("Gate")]
        public void Get_GasPriceAndLimit_Gate_ClaimType_Pos()
        {
            // Set Transaction type
            string transactionType = "GateMintAllReward";

            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?TransactionType={transactionType}"),
                                                     Api.SendRequest(Method.GET));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That(content.SelectToken("GasPrice").ToString(), Is.Not.Empty, message: "GasPrice is empty");
                Assert.That(content.SelectToken("GasLimit").ToString(), Is.Not.Empty, message: "GasLimit is empty");
            });
        }


        [Test]
        [Category("Negative"), Category("Eth")]
        public void Get_GasPriceAndLimit_Eth_ReclaimType_Neg()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?transactionType=Reclaim"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.ServiceUnavailable, response, environment);
        }


        [Test]
        [Category("Negative"), Category("Eth")]
        public void Get_GasPriceAndLimit_Eth_GenericGluwacoinFunctionType_Neg()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?transactionType=GenericGluwacoinFunction"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.ServiceUnavailable, response, environment);
        }


        [Test]
        [Category("Negative"), Category("Eth"), Category("Invalid")]
        public void Get_GasPriceAndLimit_Eth_TypeInvalid_Neg()
        {
            // Arrange
            string transactionType = "Invalid";

            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?transactionType={transactionType}"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{transactionType}' is not valid.", response);
        }


        [Test]
        [Category("Negative"), Category("Eth"), Category("Missing")]
        public void Get_GasPriceAndLimit_Eth_TypeMissing_Neg()
        {
            // Arrange
            string transactionType = null;

            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?transactionType={transactionType}"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{transactionType}' is invalid.", response);
        }


        [Test]
        [Category("Negative"), Category("Invalid")]
        public void Get_GasPriceAndLimit_InvalidCurrency_Neg()
        {
            // Arrange
            string currency = "Ethemerium";  // Non-existent currency

            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{currency}' is not valid.", response);
        }


        [Test]
        [Category("Negative"), Category("Missing")]
        public void Get_GasPriceAndLimit_MissingCurrency_Neg()
        {
            // Arrange
            string currency = null;

            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
        }


        [Test]
        [Category("Negative"), Category("Eth")]
        public void Get_GasPriceAndLimit_Eth_ReserveType_Neg()
        {
            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?transactionType=Reserve"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.ServiceUnavailable, response, environment);
        }


        [Test]
        [Category("Negative")]
        public void Get_GasPriceAndLimit_Gate_UnsupportedType_Neg()
        {
            // Set Transaction type
            string transactionType = "GtdBurn";

            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?TransactionType={transactionType}"),
                                                     Api.SendRequest(Method.GET));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.ServiceUnavailable, response, environment);
            Assertions.HandleAssertionMessage("Unsupported type of transaction for currency GATE", response);
        }


        [Test]
        [Category("Negative")]
        public void Get_GasPriceAndLimit_Gtd_UnsupportedType_Pos()
        {
            // Set Transaction type
            string transactionType = "GateMintAllReward";

            // Execute client
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/{currency}/GasPriceAndLimit?TransactionType={transactionType}"),
                                                     Api.SendRequest(Method.GET));
            // Assert 
            Assertions.HandleAssertionMessage("Unsupported type of transaction for currency GTD", response);
        }
    }
}