using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Linq;
using System.Net;

namespace KnowYourCustomer.Tests
{
    [TestFixture("Test")]   
    [TestFixture("Staging")]
    [TestFixture("Production")]
    //[TestFixture("Sandbox")]
    [Category("GetAccounts"), Category("Get")]
    public class KnowYourCustomerTests : TransferFunctions
    {
        private string environment;

        public KnowYourCustomerTests(string environment)
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
        [Category("Positive")]
        [Category("TestnetOnly")]
        public void Post_KYC_Details_Pos()
        {
            // Create body
            PostKYCDetailsBody body = RequestTestBody.CreateKYCDetailsBody("QA", "Assertible", "123 Main Street", "Vancouver",
                                                                           "BC", "H0H0H0", "CA", "1900-01-01T00:00:00Z", "CA",
                                                                           "CA", "1234567890", "Computer and Mathematical",
                                                                           "Salary", "EmployerIdentificationNumber", "1234567"
                                                                           );
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v2/KnowYourCustomers/Detail"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive")]
        [Category("TestnetOnly")]
        public void Post_KYC_AgreementDocuments_Pos()
        {
            // Create body
            var body = "{\"documentIds\":[\"7F55E097-B6E3-49F7-BA64-F8113C3AEED9\"]}";

            // Execute Request
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/Agree"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
        }


        [Test]
        [Category("Positive")]
        [Category("TestnetOnly")]
        public void Get_KYC_Details_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/KnowYourCustomers/Detail"), 
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Pretty JSON
            JToken obj = JToken.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual("QA", (string)obj.SelectToken("FirstName"));
            Assert.AreEqual("Assertible", (string)obj.SelectToken("LastName"));
            Assert.AreEqual("123 Main Street", (string)obj.SelectToken("AddressLine1"));
            Assert.AreEqual("Vancouver", (string)obj.SelectToken("City"));
            Assert.AreEqual("H0H0H0", (string)obj.SelectToken("PostalCode"));
            Assert.AreEqual("CA", (string)obj.SelectToken("CountryIsoCode"));
            Assert.AreEqual("CA", (string)obj.SelectToken("BirthPlace"));
            Assert.AreEqual("CA", (string)obj.SelectToken("Nationality"));
            Assert.AreEqual("1234567890", (string)obj.SelectToken("IdCardNo"));
            Assert.AreEqual("Computer and Mathematical", (string)obj.SelectToken("Occupation"));
            Assert.AreEqual("Salary", (string)obj.SelectToken("SourceOfFunds"));
            Assert.AreEqual("EmployerIdentificationNumber", (string)obj.SelectToken("TaxReferenceNumberType"));
            Assert.AreEqual("1234567", (string)obj.SelectToken("TaxReferenceNumber"));
        }


        [Test]
        [Category("Positive")]
        public void Get_KYC_AgreementDocuments_TypePreinvest_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/preinvest/1"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Gone, response, environment);
            Assertions.HandleAssertionMessage("This endpoint is no longer supported", response);
        }


        [Test]
        [Category("Positive")]
        public void Get_KYC_AgreementDocuments_ByTypeOnboarding_Prize_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/onboarding/Prize"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Gone, response, environment);
            Assertions.HandleAssertionMessage("This endpoint is no longer supported", response);
        }


        [Test]
        [Category("Positive")]
        public void Get_KYC_AgreementDocuments_ByTypePreinvest_Prize_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/KnowYourCustomers/AgreementDocuments/preinvest/Prize/1"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Gone, response, environment);
            Assertions.HandleAssertionMessage("This endpoint is no longer supported", response);
        }


        [Test]
        [Category("Positive")]
        public void Get_KYC_AgreementDocuments_ByTypePreinvest_Bond_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/KnowYourCustomers/AgreementDocuments/preinvest/Bond/1"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Gone, response, environment);
            Assertions.HandleAssertionMessage("This endpoint is no longer supported", response);
        }


        [Test]
        [Category("Positive")]
        public void Get_KYC_AgreementDocuments_ByTypeOnboarding_Bond_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/KnowYourCustomers/AgreementDocuments/onboarding/Bond"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Gone, response, environment);
            Assertions.HandleAssertionMessage("This endpoint is no longer supported", response);
        }


        [Test]
        [Category("Positive")]
        [Description("TestCaseID: C4422")]
        public void Get_KYC_Status_V1_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"/V1/KnowYourCustomers/Status/IdentityVerification"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            
            // Pretty JSON
            JObject content = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(content.ContainsKey("VerificationState"), message: "VerificationState");
                Assert.IsTrue(content.ContainsKey("WitnessSignatureReceived"), message: "WitnessSignatureReceived");
                Assert.IsTrue(content.ContainsKey("PreAgreementFormSubmitted"), message: "PreAgreementFormSubmitted");
                Assert.IsTrue(content.ContainsKey("SignedAgreementDocuments"), message: "SignedAgreementDocuments");
                Assert.That(content["SignedAgreementDocuments"].SelectToken("Lpa"), Is.Not.Null, message: "SignedAgreementDocuments[Lpa]");
                Assert.That(content["SignedAgreementDocuments"].SelectToken("SideLetter"), Is.Not.Null, message: "SignedAgreementDocuments[SideLetter]");
                Assert.IsTrue(content.ContainsKey("AddressDocumentStatus"), message: "AddressDocumentStatus");
                Assert.That(content["AddressDocumentStatus"].SelectToken("Status"), Is.Not.Null, message: "AddressDocumentStatus[Status]");
            }); 
        }


        [Test]
        [Category("Positive")]
        public void Get_KYC_Status_V2_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"/V2/KnowYourCustomers/Status/IdentityVerification"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Pretty JSON
            JObject content = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(content.ContainsKey("VerificationState"), message: "VerificationState");
                Assert.IsTrue(content.ContainsKey("WitnessSignatureReceived"), message: "WitnessSignatureReceived");
                Assert.IsTrue(content.ContainsKey("PreAgreementFormSubmitted"), message: "PreAgreementFormSubmitted");
                Assert.IsTrue(content.ContainsKey("SignedAgreementDocuments"), message: "SignedAgreementDocuments");
                Assert.That(content["SignedAgreementDocuments"].SelectToken("Lpa"), Is.Not.Null, message: "SignedAgreementDocuments[Lpa]");
                Assert.That(content["SignedAgreementDocuments"].SelectToken("SideLetter"), Is.Not.Null, message: "SignedAgreementDocuments[SideLetter]");
                Assert.That(content["SignedAgreementDocuments"].SelectToken("PlsaSideLetter"), Is.Not.Null, message: "SignedAgreementDocuments[PlsaSideLetter]");
                Assert.IsTrue(content.ContainsKey("AddressDocumentStatus"), message: "AddressDocumentStatus");
                Assert.That(content["AddressDocumentStatus"].SelectToken("Status"), Is.Not.Null, message: "AddressDocumentStatus[Status]");
            });
        }


        [Test]
        [Category("Negative")]
        public void Post_KYC_Details_MissingTaxNumber_Neg()
        {
            // Create body
            PostKYCDetailsBody body = RequestTestBody.CreateKYCDetailsBody("QA", "Assertible", "123 Main Street", "Vancouver",
                                                                           "BC", "H0H0H0", "CA", "1900-01-01T00:00:00Z", "US",
                                                                           "CA", "1234567890", "Computer and Mathematical",
                                                                           "Salary", "EmployerIdentificationNumber", null
                                                                           );
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v2/KnowYourCustomers/Detail"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Parse response content
            JObject content = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assert.That((string)content.SelectToken("Code"), Is.EqualTo("InvalidBody"));
            Assert.That((string)content.SelectToken("InnerErrors[0].Code"), Is.EqualTo("Required"));
            Assert.That((string)content.SelectToken("InnerErrors[0].Message"), Is.EqualTo("The TaxReferenceNumber field is required."));
        }


        [Test]
        [Category("Negative")]
        public void Post_KYC_Details_MissingTaxDocumentType_Neg()
        {
            // Create body
            PostKYCDetailsBody body = RequestTestBody.CreateKYCDetailsBody("QA", "Assertible", "123 Main Street", "Vancouver",
                                                                           "BC", "H0H0H0", "CA", "1900-01-01T00:00:00Z", "US",
                                                                           "CA", "1234567890", "Computer and Mathematical",
                                                                           "Salary", null, "1234567"
                                                                           );
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v2/KnowYourCustomers/Detail"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Parse response content
            JObject content = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assert.That((string)content.SelectToken("Code"), Is.EqualTo("InvalidBody"));
            Assert.That((string)content.SelectToken("InnerErrors[0].Code"), Is.EqualTo("Required"));
            Assert.That((string)content.SelectToken("InnerErrors[0].Message"), Is.EqualTo("The TaxReferenceNumberType field is required."));
        }


        [Test]
        [Category("Negative")]
        public void Post_KYC_Details_InvalidTaxDocumentType_Neg()
        {
            // Create body
            PostKYCDetailsBody body = RequestTestBody.CreateKYCDetailsBody("QA", "Assertible", "123 Main Street", "Vancouver",
                                                                           "BC", "H0H0H0", "CA", "1900-01-01T00:00:00Z", "US",
                                                                           "CA", "1234567890", "Computer and Mathematical",
                                                                           "Salary", "foobar", "1234567"
                                                                           );
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v2/KnowYourCustomers/Detail"), 
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("Value is not a valid TaxReferenceNumberType.", response);
            Assertions.HandleAssertionInnerCode("InvalidFormat", response);
        }


        [Test]
        [Category("Negative")]
        public void Get_KYC_AgreementDocuments_ZeroAmount_Neg()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/preinvest/0"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Gone, response, environment);
            Assertions.HandleAssertionMessage("This endpoint is no longer supported", response);
        }


        [Test]
        [Category("Negative")]
        public void Get_KYC_AgreementDocuments_InvalidAmount_Neg()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/preinvest/a"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Gone, response, environment);
            Assertions.HandleAssertionMessage("This endpoint is no longer supported", response);
        }


        [Test]
        [Category("Negative")]
        public void Get_KYC_AgreementDocuments_NegativeAmount_Neg()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/preinvest/-1"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Gone, response, environment);
            Assertions.HandleAssertionMessage("This endpoint is no longer supported", response);
        }


        [Test]
        [Category("Negative")]
        public void Get_KYC_AgreementDocuments_MissingAmount_Neg()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/preinvest/"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Gone, response, environment);
            Assertions.HandleAssertionMessage("This endpoint is no longer supported", response);
        }


        [Test]
        [Category("Negative")]
        public void Post_KYC_AgreementDocuments_InvalidDocumentIds_Neg()
        {
            // Create body
            var body = "{\"documentIds\":[\"6694dd3f-dc47-4461-b596-71bb21ce89012\",\"16dd6cfd-1d22-4ceb-8570-c847a8064d742\"]}";

            // Execute Request
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/Agree"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("An invalid DocumentId was provided", response);
        }


        [Test]
        [Category("Negative")]
        public void Post_KYC_AgreementDocuments_NotFound_Neg()
        {
            // Create body
            var body = "{\"documentIds\":[\"E06485EA-DD48-46E4-92BE-D64035833333\",\"2CAEB62C-1B30-441C-B784-85BFC6233333\"]}";

            // Execute Request
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/Agree"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
        }


        [Test]
        [Category("Negative")]
        [Category("TestnetOnly")]
        public void Post_KYC_AgreementDocuments_Forbidden_Neg()
        {
            // Create body with document IDs that belong to different user
            var body = "{\"documentIds\":[\"9B7C76FE-853E-4198-88F1-1A264A48931B\",\"A2865117-3FB8-42FC-A9F0-2E841B5C7FCA\"]}";

            // Execute Request
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/Agree"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionCode("Unauthorized", response);
        }


        [Test]
        [Category("Negative")]
        public void Post_KYC_AgreementDocuments_MissingDocumentId_Neg()
        {
            // Create body
            var body = "{\"documentIds\":[\"\",\"17034B15-056D-4E09-8DF5-7437B717F02C\"]}";

            // Execute Request
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/KnowYourCustomers/AgreementDocuments/Agree"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("An invalid DocumentId was provided", response);
        }
    }
}
