using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Models.RequestBody;
using NUnit.Framework;
using RestSharp;
using GluwaAPI.TestEngine.Utils;
using System.Net;
using System.Threading;

namespace BondAccount.Tests
{
    [TestFixture("Test")]
    //[TestFixture("Staging")]
    //[TestFixture("Production")]
    //[TestFixture("Sandbox")]
    [Category("PostInvestmentRequest"), Category("Post")]
    public class PostInvestmentRequestTests : TransferFunctions
    {
        private string environment;

        public PostInvestmentRequestTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            //Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
        }

        [Test]
        [Order(1)]
        [Description("Save InvestmentRequest into the db"), Category("Positive")]
        public void PostInvestmentRequest_Deposit_Pos()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                             senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                             environment);
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response, environment);
        }


        ////Run manually to avoid zeroing your Fixed-Term Interest account balance
        //[Test]
        //[Description("Check investment workflow"), Category("Positive")]
        //public void PostInvestmentRequest_MaxTest_Pos()
        //{
        //    // Get balance before 1st deposit
        //    var accountType = "Bond";
        //    var initialBalance = Get_AccountBalance(environment, accountType);

        //    // New deposit 
        //    PostInvestmentRequestBody depositBody = RequestTestBody.CreatePostInvestmentDepositBody("10",
        //                                                                                            QAKeyVault.GetGluwaAddress("Sender"),
        //                                                                                            "Deposit",
        //                                                                                            accountType,
        //                                                                                            environment);
        //    // Execute
        //    Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
        //                    Api.SendRequest(Method.POST, depositBody)
        //                       .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

        //    Thread.Sleep(30000);

        //    // Get balance after new deposit
        //    var afterBalance = Get_AccountBalance(environment, accountType);

        //    // Ensure the balance after deposit less than initial balance  
        //    Assert.That(int.Parse(initialBalance) > int.Parse(afterBalance)); // 1st checkPoint

        //    // Withdraw all funds from Fixed-Term Interest account
        //    PostInvestmentRequestBody withdrawBody = RequestTestBody.CreatePostInvestmentWithdrawBody(QAKeyVault.GetGluwaAddress("Sender"),
        //                                                                                             "Withdraw",
        //                                                                                              accountType);
        //    // Execute
        //    var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
        //                                   Api.SendRequest(Method.POST, withdrawBody)
        //                                      .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

        //    Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        //    Thread.Sleep(100000);

        //    // Check balance after withdraw
        //    var finalBalance = Get_AccountBalance(environment, accountType);

        //    // Ensure the initial balance is restored
        //    Assert.That(int.Parse(finalBalance) >= int.Parse(afterBalance)); // 2nd checkPoint
        //}


        //Run manually to avoid zeroing your Fixed-Term Interest account balance
        //[Test]
        //[Description("Withdraw all amount from Fixed-Term Interest account"), Category("Positive")]
        //public void PostInvestmentRequest_Withdraw_Pos()
        //{
        //    // Arrange
        //    var senderAddress = QAKeyVault.GetGluwaAddress("Sender");

        //    // Create body
        //    PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentWithdrawBody(senderAddress,
        //                                                                                      "Withdraw",
        //                                                                                      "Bond");

        //    // Execute response
        //    var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"),
        //                                   Api.SendRequest(Method.POST, body)
        //                                      .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

        //    // Assert
        //    Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        //}


        [Test]
       [Description("Withdraw to not associated destination address"), Category("Negative")]
        public void PostInvestmentRequest_WithdrawNotAssociatedAddress_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentWithdrawBody(senderAddress,
                                                                                             "Withdraw",
                                                                                             "Bond");
            // AddressId valid and existed but not associated with user
            body.Address = "0xaBB451D3aAAFfDc45B00548Bce55b4c3f8A37Af3"; 

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("User does not have an active Fixed-Term Interest Account", response);
        }


        [Test]
        [Description("Invalid amount"), Category("Negative")]
        public void PostInvestmentRequest_DepositOverBalance_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            var accountType = "Bond";
            var balance = int.Parse(Get_AccountBalance(environment, accountType));
            string amount = (balance + 1).ToString();

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            accountType,
                                                                                            environment);
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("The Amount will exceed the investment cap", response);
        }


        [Test]
        [Description("Invalid amount"), Category("Negative")]
        public void PostInvestmentRequest_OverSixDecimals_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
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
        public void PostInvestmentRequest_ZeroAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "0";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
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
        public void PostInvestmentRequest_NegativeAmount_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
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
        public void PostInvestmentRequest_InvalidAction_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);
            body.Action = "Exchange";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
            Assertions.HandleAssertionInnerMessage($" \"{body.Action}\"" + " is not a valid Action.", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_MissingAction_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);
            body.Action = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
            Assertions.HandleAssertionInnerMessage("The Action field is required.", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_InvalidType_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);
            body.AccountType = "Chequing";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
            Assertions.HandleAssertionInnerMessage($" \"{body.AccountType}\"" + " is not a valid AccountType.", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_MissingType_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);
            body.AccountType = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
            Assertions.HandleAssertionInnerMessage("The AccountType field is required.", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_InvalidAddress_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
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


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_MissingAddress_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);
            body.Address = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("One or more fields are invalid in body.", response);
            Assertions.HandleAssertionInnerMessage("The Address field is required.", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_InvalidSignature_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);

            body.ApproveTxnSignature = body.ApproveTxnSignature.Remove(body.ApproveTxnSignature.Length - 3);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("ApproveTxnSignature is invalid", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_MissingSignature_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);
            body.ApproveTxnSignature = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("TxnSignature is required when Action = 'Deposit'", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_InvalidDocumentId_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);

            body.SideLetterDocumentId =  body.SideLetterDocumentId.Remove(body.SideLetterDocumentId.Length-1);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("SideLetterDocumentId is not a valid Guid", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_NotAssociatedDocumentId_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);
            // Existed and valid DocumentId but not associated with user
            body.SideLetterDocumentId = "77683E1D-929D-402D-B657-7ECE9AE16417";  

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("SideLetterDocumentId is invalid", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_MissingDocumentId_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);
            body.SideLetterDocumentId = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("SideLetterDocumentId is required when Action = 'Deposit'", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_InvalidSubDocumentId_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);

            body.SubscriptionAgreementDocumentId = body.SubscriptionAgreementDocumentId.Remove(body.SubscriptionAgreementDocumentId.Length - 1);

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("SubscriptionAgreementDocumentId is not a valid Guid", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_NotAssociatedSubDocumentId_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);
            // Existed and valid Subscription DocumentId but not associated with user
            body.SubscriptionAgreementDocumentId = "341613E8-A223-4049-AC09-98873F8FFB97";  

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("SubscriptionAgreementDocumentId is invalid", response);
        }


        [Test]
        [Description("Invalid body"), Category("Negative")]
        public void PostInvestmentRequest_MissingSubDocumentId_Neg()
        {
            // Arrange
            var senderAddress = QAKeyVault.GetGluwaAddress("Sender");
            string amount = "1";

            // Create body
            PostInvestmentRequestBody body = RequestTestBody.CreatePostInvestmentDepositBody(amount,
                                                                                            senderAddress,
                                                                                            "Deposit",
                                                                                            "Bond",
                                                                                            environment);
            body.SubscriptionAgreementDocumentId = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Investments"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("SubscriptionAgreementDocumentId is required when Action = 'Deposit'", response);
        }
    }
}
