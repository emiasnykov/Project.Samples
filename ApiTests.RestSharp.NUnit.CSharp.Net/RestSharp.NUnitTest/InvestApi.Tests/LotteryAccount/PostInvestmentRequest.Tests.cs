using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Models.RequestBody;
using NUnit.Framework;
using RestSharp;
using GluwaAPI.TestEngine.Utils;
using System.Net;
using System.Linq;

namespace LotteryAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    //[TestFixture("Production")]
    //[TestFixture("Sandbox")]
    [Category("PostInvestmentRequestPrize"), Category("Post")]
    public class PostInvestmentRequestTests : TransferFunctions
    {
        private string environment;
        private string keyName;

        public PostInvestmentRequestTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            //Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            keyName = GluwaTestApi.SetUpGluwaKeyName(environment);

            // Ignore positive tests on Mainnet
            if (environment != "Test" && TestContext.CurrentContext.Test.Properties["Category"].Contains("TestnetOnly"))
            {
                Assert.Ignore($"Cannot run this test on environment: {environment}");
            }
        }

        [Test]
        [Category("TestnetOnly")]
        [Description("Deposit to Lottery account")]
        public void Post_InvestmentRequest_Deposit_Prize_Pos()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Prize",
                                                                                            environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
        }


        [Test]
        [Category("TestnetOnly")]
        [Description("Withdraw from Lottery account")]
        public void PostInvestmentRequest_Withdraw_Prize_Pos()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            var amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentWithdrawBody(senderAddress,
                                                                                              amount,
                                                                                             "Withdraw",
                                                                                             "Prize");
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_NotAssociatedAddress_Prize_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            var amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentWithdrawBody(senderAddress,
                                                                                              amount,
                                                                                             "Withdraw",
                                                                                             "Prize");
            // AddressId valid and existed but not associated with user
            body.Address = Shared.NONACCOUNT_ADDRESS;
                

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("User does not have an active Lottery Account", response);
        }


        [Test]
        [Description("Invalid amount"), Category("Negative")]
        public void PostInvestmentRequest_OverSixDecimals_Prize_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Prize",
                                                                                            environment);
            body.Amount = "1.01234567";
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount is invalid", response);
        }


        [Test]
        [Description("Invalid amount"), Category("Negative")]
        public void PostInvestmentRequest_ZeroAmount_Prize_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string amount = "0";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Prize",
                                                                                            environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount must be greater than 0", response);
        }


        [Test]
        [Description("Invalid amount"), Category("Negative")]
        public void PostInvestmentRequest_NegativeAmount_Prize_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Prize",
                                                                                            environment);
            body.Amount = "-1";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Amount value can not be a negative number", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_InvalidAddress_Prize_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress(keyName);
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Prize",
                                                                                            environment);
            body.Address = senderAddress.Address.Remove(senderAddress.Address.Length - 3);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("Address is invalid", response);
        }
    }
}

