using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace KnowYourCustomer.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    public class KnowYourCustomersFtaTests
    {
        private string environment;

        public KnowYourCustomersFtaTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            if (environment != "Test" && TestContext.CurrentContext.Test.Properties["Category"].Contains("TestnetOnly"))
            {
                Assert.Ignore($"Cannot run this test on environment: {environment}");
            }

            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
        }

        [Test]
        [Category("TestnetOnly")]
        public void Post_KYC_FtaAgreementDocuments_Pos()
        {

            // Arrange
            List<string> ids = TransferFunctions.GetFtaAgreementDocs(environment);

            var inputBody = new
            {
                documentIds = ids
            };

            // Create Body
            var body = JsonConvert.SerializeObject(inputBody);

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/Agree"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
        }


        [Test]
        public void Get_KYC_FtaAgreementDocuments_Onboarding_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/KnowYourCustomers/AgreementDocuments/Fta/onboarding"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Pretty JSON
            JToken jsonObj = JArray.Parse(response.Content);
            dynamic data = JToken.Parse(jsonObj.ToString());

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That((string)data[0].SelectToken("Id"), Is.Not.Null, message: "ID");
            Assert.That((string)data[0].SelectToken("Type"), Is.Not.Null, message: "Type");
            Assert.That((string)data[0].SelectToken("Content"), Is.Not.Null, message: "Content");
        }


        [Test]
        public void Get_KYC_FtaAgreementDocuments_Preinvest_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/KnowYourCustomers/AgreementDocuments/Fta/preinvest?investmentAmount=1"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        public void Get_KYC_FtaStatus_Verified_Pos()
        {

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/KnowYourCustomers/Status/Fta/IdentityVerification"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Pretty JSON
            JToken jsonObj = JToken.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.That((string)jsonObj.SelectToken("VerificationState"), Is.EqualTo("Approved"));
                Assert.That((string)jsonObj.SelectToken("PreAgreementFormSubmitted"), Is.EqualTo("True"));
                Assert.That((string)jsonObj.SelectToken("IsDaoAgreementSigned"), Is.EqualTo("True"));
                Assert.That((string)jsonObj.SelectToken("AddressDocumentStatus").SelectToken("Status"), Is.EqualTo("Approved"));
            });
        }


        [Test]
        public void Post_KYC_FtaAgreementDocuments_InvalidId_Neg()
        {
            // Arrange
            var inputBody = new
            {
                documentIds = new List<string> { Shared.INVALID_ID }
            };

            // Create Body
            var body = JsonConvert.SerializeObject(inputBody);

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/Agree"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response);
            Assertions.HandleAssertionMessage("An invalid DocumentId was provided", response);
        }


        [Test]
        public void Post_KYC_FtaAgreementDocuments_NoAuth_Neg()
        {
            // Arrange
            List<string> ids = TransferFunctions.GetFtaAgreementDocs(environment);

            var inputBody = new
            {
                documentIds = ids
            };

            var body = JsonConvert.SerializeObject(inputBody);

            // Execute Request
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/Agree"),
                                           Api.SendRequest(Method.POST, body));
            // Assert        
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }


        [Test]
        public void Get_KYC_FtaAgreementDocuments_InvalidStep_Neg()
        {
            // Arrange
            string step = "preinvestereum";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/KnowYourCustomers/AgreementDocuments/Fta/{step}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{step}' is not valid.", response);
        }


        [Test]
        public void Get_KYC_FtaAgreementDocuments_NoAuth_Neg()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/KnowYourCustomers/AgreementDocuments/Fta/onboarding"),
                                           Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }


        [Test]
        public void Get_KYC_FtaStatus_NoAuth_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/KnowYourCustomers/Status/Fta/IdentityVerification"),
                                           Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }
    }
}
