using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using ResWebApiTest.TestEngine;

namespace ResWebApiTest.Tests.Api.Visit
{
    [TestFixture]
    public class VisitTests
    {
        //***************************************************************************************************************************************************
        // Note:
        // This login is used for RegisterVisitAgent used for common api methods (as helpers in the tests) that need authorizarion i.e.: SearchAgencies,
        // otherwise we would get HttpStatusCode.Unauthorized.

        public void LoginByLP()
        {
            // Main api path
            WebApiTest.ApiUriLogin = "Api/Login/ByLP";

            // Login credentials
            var Credentials = new
            {
                Login = "********",
                Password = "**********"
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Login, Credentials);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Login, "StatusCode"));
            Assert.AreEqual("janine", WebApiTest.Status(WebApiTest.ApiUri.Login, "login"));
            Assert.AreEqual("bearer", WebApiTest.Status(WebApiTest.ApiUri.Login, "token_type"));
            Assert.IsNotEmpty(WebApiTest.Status(WebApiTest.ApiUri.Login, "access_token"));
        }


        //***************************************************************************************************************************************************
        // Note:
        // This is login to Visit using different emails. Password is the same for each visitor agents ('visagent').
        // Login with password
        public void WebVisitLogin(string _LoginEmail)
        {
            // Main api path
            WebApiTest.ApiUriLogin = "Api/Login/WebVisitLogin";

            // Login credentials.
            var credentialsObject = new
            {
                Login = _LoginEmail,
                Password = "**************"
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Login, credentialsObject);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Login, "StatusCode"));
            Assert.AreEqual("bearer", WebApiTest.Status(WebApiTest.ApiUri.Login, "token_type"));
            Assert.IsNotEmpty(WebApiTest.Status(WebApiTest.ApiUri.Login, "access_token"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));
        }


        // Login without password
        public void WebVisitLogin_NoPassword(string _LoginEmail)
        {
            // Main api path
            WebApiTest.ApiUriLogin = "Api/Login/WebVisitLogin";

            // Login credentials.
            var credentialsObject = new
            {
                Login = _LoginEmail,
                Password = ""
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Login, credentialsObject, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Login, "StatusCode"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));
        }

        //***************************************************************************************************************************************************
        // Add Visit test cases
        //***************************************************************************************************************************************************

        [Test]
        public void AddVisit_OneContractorAllCorrectDataPayload_ReturnHttpStatusOK()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Visitor booking data
            int AddVisEventId = 1702;
            int AddVisAgentId = 37214;
            DateTime AddVisVisitDate = new DateTime(2022,04,05);

            // Visitor contractor data
            var AddVisVisitorData = new VisitHelpers.VisitorData
            {
                Id = 1,
                FolioId = 0,
                Forename = $"AddVisForename{random}",
                Surname = $"AddVisSurname{random}",
                BirthDate = new DateTime(1972, 05, 06),
                Gender = "M",
                Address = $"AddVisAddress{random}",
                City = $"AddVisCity{random}",
                Email = $"AddVisEmail{random}@gmail.xxx",
                Phone = "+491736402322",
                DocumentType = "P",
                DocumentNumber = $"VisDocNo{random}",
                DocumentForename = $"AddVisDocForename{random}",
                DocumentSurname = $"AddVisDocSurname{random}",
                DocumentAddress = $"AddVisDocAddress{random}",
                DocumentValidDate = new DateTime(2025, 05, 06),
            };

            // Add to list
            var AddVisVisitorList = new List<VisitHelpers.VisitorData>();
            AddVisVisitorList.Add(AddVisVisitorData);

            // Login
            WebVisitLogin("apivisitagent@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            var AddVisitPayload = new
            {
                EventId = AddVisEventId,
                AgentId = AddVisAgentId,
                VisitDate = AddVisVisitDate,
                Visitors = AddVisVisitorList
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void AddVisit_OneContractorOnlyMandatoryCorrectDataWithEmptyNonMandatoryPayload_ReturnHttpStatusOK()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Visitor booking data
            int AddVisEventId = 1702;
            int AddVisAgentId = 37214;
            DateTime AddVisVisitDate = new DateTime(2022, 04, 05);

            // Visitor contractor data
            var AddVisVisitorData = new VisitHelpers.VisitorData
            {
                Id = 1,
                FolioId = 0,
                Forename = $"AddVisForename{random}",
                Surname = $"AddVisSurname{random}",
                BirthDate = new DateTime(1972, 05, 06),
                Gender = "",
                Address = "",
                City = "",
                Email = $"AddVisEmail{random}@gmail.xxx",
                Phone = "",
                DocumentType = "P",
                DocumentNumber = $"VisDocNo{random}",
                DocumentForename = $"AddVisDocForename{random}",
                DocumentSurname = $"AddVisDocSurname{random}",
                DocumentAddress = "",
                DocumentValidDate = new DateTime(2025, 05, 06),
            };

            // Add to list
            var AddVisVisitorList = new List<VisitHelpers.VisitorData>();
            AddVisVisitorList.Add(AddVisVisitorData);

            // Login
            WebVisitLogin("apivisitagent@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            var AddVisitPayload = new
            {
                EventId = AddVisEventId,
                AgentId = AddVisAgentId,
                VisitDate = AddVisVisitDate,
                Visitors = AddVisVisitorList
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void AddVisit_OneContractorOnlyMandatoryDataPayload_ReturnHttpStatusOK()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Visitor booking data
            int AddVisEventId = 1702;
            int AddVisAgentId = 37214;
            DateTime AddVisVisitDate = new DateTime(2022, 04, 05);

            // Visitor contractor data - only mandatory fields
            // Any other fields (non-mandatory) are not being added
            var AddVisVisitorData = new VisitHelpers.VisitorData
            {
                Id = 1,
                FolioId = 0,
                Forename = $"AddVisForename{random}",
                Surname = $"AddVisSurname{random}",
                BirthDate = new DateTime(1972, 05, 06),
                Email = $"AddVisEmail{random}@gmail.xxx",
                DocumentType = "P",
                DocumentNumber = $"VisDocNo{random}",
                DocumentForename = $"AddVisDocForename{random}",
                DocumentSurname = $"AddVisDocSurname{random}",
                DocumentValidDate = new DateTime(2025, 05, 06),
            };

            // Add to list
            var AddVisVisitorList = new List<VisitHelpers.VisitorData>();
            AddVisVisitorList.Add(AddVisVisitorData);

            // Login
            WebVisitLogin("apivisitagent@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            var AddVisitPayload = new
            {
                EventId = AddVisEventId,
                AgentId = AddVisAgentId,
                VisitDate = AddVisVisitDate,
                Visitors = AddVisVisitorList
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void AddVisit_ThreeContractorsAllCorrectDataPayload_ReturnHttpStatusOK()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Init variables data 
            var AddVisVisitorList = new List<VisitHelpers.VisitorData>();
            string random;

            // Visitor booking data
            int AddVisEventId = 1702;
            int AddVisAgentId = 37214;
            DateTime AddVisVisitDate = new DateTime(2022, 04, 05);

            // Create three visits
            for (int idx = 0; idx < 3; idx++)
            {
                // Create random data
                random = WebApiTest.QA_GenerateRandomFixedData();

                // Visitor contractor data
                var AddVisVisitorData = new VisitHelpers.VisitorData
                {
                    Id = idx + 1,
                    FolioId = 0,
                    Forename = $"AddVisForename{random}",
                    Surname = $"AddVisSurname{random}",
                    BirthDate = new DateTime(1972, 05, 06),
                    Gender = "M",
                    Address = $"AddVisAddress{random}",
                    City = $"AddVisCity{random}",
                    Email = $"AddVisEmail{random}@gmail.xxx",
                    Phone = "+**************",
                    DocumentType = "P",
                    DocumentNumber = $"VisDocNo{random}",
                    DocumentForename = $"AddVisDocForename{random}",
                    DocumentSurname = $"AddVisDocSurname{random}",
                    DocumentAddress = $"AddVisDocAddress{random}",
                    DocumentValidDate = new DateTime(2025, 05, 06),
                };

                // Add to list
                AddVisVisitorList.Add(AddVisVisitorData);
            }

            // Login
            WebVisitLogin("apivisitagent@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            var AddVisitPayload = new
            {
                EventId = AddVisEventId,
                AgentId = AddVisAgentId,
                VisitDate = AddVisVisitDate,
                Visitors = AddVisVisitorList
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void AddVisit_OneContractorAllCorrectDataOnlyWrongGenderCodePayload_ReturnHttpStatusOK()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Visitor booking data
            int AddVisEventId = 1702;
            int AddVisAgentId = 37214;
            DateTime AddVisVisitDate = new DateTime(2022, 04, 05);

            // Improper Gender value
            // Note:
            // The improper value will be set to N due to api changes
            string AddVisGender = "y"; // Proper values are: F-Female, M-Male, N-Undefined.

            // Visitor contractor data
            var AddVisVisitorData = new VisitHelpers.VisitorData
            {
                Id = 1,
                FolioId = 0,
                Forename = $"AddVisForename{random}",
                Surname = $"AddVisSurname{random}",
                BirthDate = new DateTime(1972, 05, 06),
                Gender = AddVisGender,
                Address = $"AddVisAddress{random}",
                City = $"AddVisCity{random}",
                Email = $"AddVisEmail{random}@gmail.xxx",
                Phone = "+491736402322",
                DocumentType = "P",
                DocumentNumber = $"VisDocNo{random}",
                DocumentForename = $"AddVisDocForename{random}",
                DocumentSurname = $"AddVisDocSurname{random}",
                DocumentAddress = $"AddVisDocAddress{random}",
                DocumentValidDate = new DateTime(2025, 05, 06),
            };

            // Add to list
            var AddVisVisitorList = new List<VisitHelpers.VisitorData>();
            AddVisVisitorList.Add(AddVisVisitorData);

            // Login
            WebVisitLogin("apivisitagent@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            var AddVisitPayload = new
            {
                EventId = AddVisEventId,
                AgentId = AddVisAgentId,
                VisitDate = AddVisVisitDate,
                Visitors = AddVisVisitorList
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        [Test]
        [Ignore("Test passed. Due to not taking to much time on test, this one is ignored.")]
        [Description("Hardcore test to check if Api is able to add at least 20 visits in one-go. Once passed, the test will be ignored.")]
        public void AddVisit_Hardcore_TwentyContractorsAllCorrectDataPayload_ReturnHttpStatusOK()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Init variables data 
            var AddVisVisitorList = new List<VisitHelpers.VisitorData>();
            string random;

            // Visitor booking data
            int AddVisEventId = 1702;
            int AddVisAgentId = 37214;
            DateTime AddVisVisitDate = new DateTime(2022, 04, 05);

            // Create three visits
            for (int idx = 0; idx < 20; idx++)
            {
                // Create random data
                random = WebApiTest.QA_GenerateRandomFixedData();

                // Visitor contractor data
                var AddVisVisitorData = new VisitHelpers.VisitorData
                {
                    Id = idx + 1,
                    FolioId = 0,
                    Forename = $"AddVisForename{random}",
                    Surname = $"AddVisSurname{random}",
                    BirthDate = new DateTime(1972, 05, 06),
                    Gender = "M",
                    Address = $"AddVisAddress{random}",
                    City = $"AddVisCity{random}",
                    Email = $"AddVisEmail{random}@gmail.xxx",
                    Phone = "+491736402322",
                    DocumentType = "P",
                    DocumentNumber = $"VisDocNo{random}",
                    DocumentForename = $"AddVisDocForename{random}",
                    DocumentSurname = $"AddVisDocSurname{random}",
                    DocumentAddress = $"AddVisDocAddress{random}",
                    DocumentValidDate = new DateTime(2025, 05, 06),
                };

                // Add to list
                AddVisVisitorList.Add(AddVisVisitorData);
            }

            // Login
            WebVisitLogin("apivisitagent@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            var AddVisitPayload = new
            {
                EventId = AddVisEventId,
                AgentId = AddVisAgentId,
                VisitDate = AddVisVisitDate,
                Visitors = AddVisVisitorList
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void AddVisit_ValidateNullAddVisitPayload_ReturnHttpStatusBadRequestWithErrorMessage()
        {
            // Login
            WebVisitLogin("apivisitagent@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            Object AddVisitPayload = null;

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void AddVisit_ValidateContractorZeroDataForAddVisitPayload_ReturnHttpBadRequestWithErrorMessage()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Visitor contractor zero data
            int AddVisEventId = 0;
            int AddVisAgentId = 0;
            DateTime AddVisVisitDate = new DateTime();
            List<VisitHelpers.VisitorData> AddVisVisitorList = null;

            // Login
            WebVisitLogin("apivisitagent@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            var AddVisitPayload = new
            {
                EventId = AddVisEventId,
                AgentId = AddVisAgentId,
                VisitDate = AddVisVisitDate,
                Visitors = AddVisVisitorList
            };            

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void AddVisit_ValidateOnlyContractorListZeroDataForAddVisitPayload_ReturnHttpStatusBadRequestWithErrorMessage()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Visitor contractor zero data
            int AddVisEventId = 1702;
            int AddVisAgentId = 37214;
            DateTime AddVisVisitDate = new DateTime();
            List<VisitHelpers.VisitorData> AddVisVisitorList = null;

            // Login
            WebVisitLogin("apivisitagent@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            var AddVisitPayload = new
            {
                EventId = AddVisEventId,
                AgentId = AddVisAgentId,
                VisitDate = AddVisVisitDate,
                Visitors = AddVisVisitorList
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void AddVisit_OneContractorAllMandatoryDataEmptyNonMandatoryDataPopulatedPayload_ReturnHttpStatusBadRequestWithErrorMessage()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Visitor booking data
            int AddVisEventId = 1702;
            int AddVisAgentId = 37214;
            DateTime AddVisVisitDate = new DateTime(2022, 04, 05);

            // Visitor contractor data
            var AddVisVisitorData = new VisitHelpers.VisitorData
            {
                Id = 1,
                FolioId = 0,
                Forename = "",
                Surname = "",
                BirthDate = new DateTime(1972, 05, 06),
                Gender = "M",
                Address = $"AddVisAddress{random}",
                City = $"AddVisCity{random}",
                Email = "",
                Phone = "+491736402322",
                DocumentType = "",
                DocumentNumber = "",
                DocumentForename = "",
                DocumentSurname = "",
                DocumentAddress = $"AddVisDocAddress{random}",
                DocumentValidDate = new DateTime(2025, 05, 06),
            };

            // Add to list
            var AddVisVisitorList = new List<VisitHelpers.VisitorData>();
            AddVisVisitorList.Add(AddVisVisitorData);

            // Login
            WebVisitLogin("apivisitagent@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            var AddVisitPayload = new
            {
                EventId = AddVisEventId,
                AgentId = AddVisAgentId,
                VisitDate = AddVisVisitDate,
                Visitors = AddVisVisitorList
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }

        [Test]
        public void AddVisit_OneContractorSomeTooLongDataPayload_ReturnHttpStatusBadRequestWithErrorMessage()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();
            string tooLong = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

            // Visitor booking data
            int AddVisEventId = 1702;
            int AddVisAgentId = 37214;
            DateTime AddVisVisitDate = new DateTime(2022, 04, 05);

            // Visitor contractor data
            var AddVisVisitorData = new VisitHelpers.VisitorData
            {
                Id = 1,
                FolioId = 0,
                Forename = $"AddVisForename{tooLong}{random}",
                Surname = $"AddVisSurname{tooLong}{random}",
                BirthDate = new DateTime(1972, 05, 06),
                Gender = "M",
                Address = $"AddVisAddress{tooLong}{random}",
                City = $"AddVisCity{tooLong}{random}",
                Email = $"AddVisEmail{tooLong}{random}",
                Phone = $"+491736402322{tooLong}",
                DocumentType = "P",
                DocumentNumber = $"VisDocNo{tooLong}{random}",
                DocumentForename = $"AddVisDocForename{tooLong}{random}",
                DocumentSurname = $"AddVisDocSurname{tooLong}{random}",
                DocumentAddress = $"AddVisDocAddress{tooLong}{random}",
                DocumentValidDate = new DateTime(2025, 05, 06),
            };

            // Add to list
            var AddVisVisitorList = new List<VisitHelpers.VisitorData>();
            AddVisVisitorList.Add(AddVisVisitorData);

            // Login
            WebVisitLogin("apivisitagent@gmail.xxx");

            //Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            var AddVisitPayload = new
            {
                EventId = AddVisEventId,
                AgentId = AddVisAgentId,
                VisitDate = AddVisVisitDate,
                Visitors = AddVisVisitorList
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void AddVisit_OneContractorAllCorrectDataOnlyWrongDocumentTypePayload_ReturnHttpStatusBadRequestWithErrorMessage()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            //Visitor booking data
            int AddVisEventId = 1702;
            int AddVisAgentId = 37214;
            DateTime AddVisVisitDate = new DateTime(2022, 04, 05);

            // Improper Document type
            // Note:
            // The improper value will NOT be set to any value
            string AddVisDocType = "j"; //Proper values for visitors are: 'P'-Passport, 'N'-National Id

            // Visitor contractor data
            var AddVisVisitorData = new VisitHelpers.VisitorData
            {
                Id = 1,
                FolioId = 0,
                Forename = $"AddVisForename{random}",
                Surname = $"AddVisSurname{random}",
                BirthDate = new DateTime(1972, 05, 06),
                Gender = "M",
                Address = $"AddVisAddress{random}",
                City = $"AddVisCity{random}",
                Email = $"AddVisEmail{random}@gmail.xxx",
                Phone = "+491736402322",
                DocumentType = AddVisDocType,
                DocumentNumber = $"VisDocNo{random}",
                DocumentForename = $"AddVisDocForename{random}",
                DocumentSurname = $"AddVisDocSurname{random}",
                DocumentAddress = $"AddVisDocAddress{random}",
                DocumentValidDate = new DateTime(2025, 05, 06),
            };

            // Add to list
            var AddVisVisitorList = new List<VisitHelpers.VisitorData>();
            AddVisVisitorList.Add(AddVisVisitorData);

            // Login
            WebVisitLogin("apivisitagent@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            var AddVisitPayload = new
            {
                EventId = AddVisEventId,
                AgentId = AddVisAgentId,
                VisitDate = AddVisVisitDate,
                Visitors = AddVisVisitorList
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));
            Assert.AreEqual($"Something went wrong...\r\nMissing document type for Traveler ({ AddVisVisitorData.Forename} { AddVisVisitorData.Surname})", WebApiTest.Node("0"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void AddVisit_OneContractorAllCorrectDataPayloadForUnapprovedVisitorAgent_ReturnHttpStatusUnauthorized()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Visitor booking data
            int AddVisEventId = 1702;
            DateTime AddVisVisitDate = new DateTime(2022, 04, 05);

            // Check add visit on unapproved visitor agent
            // ApiVisitNonAprovedAgentName, ApiVisitNonAprovedAgentSurname, apivisitnonnprovedagent@gmail.xxx (fake password: visagent)
            // (foluid = 37224)
            int AddVisAgentId = 37224;

            // Visitor contractor data
            var AddVisVisitorData = new VisitHelpers.VisitorData
            {
                Id = 1,
                FolioId = 0,
                Forename = $"AddVisForename{random}",
                Surname = $"AddVisSurname{random}",
                BirthDate = new DateTime(1972, 05, 06),
                Gender = "M",
                Address = $"AddVisAddress{random}",
                City = $"AddVisCity{random}",
                Email = $"AddVisEmail{random}@gmail.xxx",
                Phone = "+491736402322",
                DocumentType = "P",
                DocumentNumber = $"VisDocNo{random}",
                DocumentForename = $"AddVisDocForename{random}",
                DocumentSurname = $"AddVisDocSurname{random}",
                DocumentAddress = $"AddVisDocAddress{random}",
                DocumentValidDate = new DateTime(2025, 05, 06),
            };

            // Add to list
            var AddVisVisitorList = new List<VisitHelpers.VisitorData>();
            AddVisVisitorList.Add(AddVisVisitorData);

            // Login to unapproved agent without password
            WebVisitLogin_NoPassword("apivisitnonnprovedagent@gmail.xxx");

            // Note:
            // Eventhough the login status is BadRequest we try to force to add new visit

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/AddVisit";

            // Add visit payload
            var AddVisitPayload = new
            {
                EventId = AddVisEventId,
                AgentId = AddVisAgentId,
                VisitDate = AddVisVisitDate,
                Visitors = AddVisVisitorList
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, AddVisitPayload, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));
            Assert.AreEqual("Authorization has been denied for this request.", WebApiTest.Node("Message"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        //***************************************************************************************************************************************************
        // Visit/GetDocumentTypeList Test Cases
        //***************************************************************************************************************************************************

        [Test]
        public void GetDocumentTypeList_ReturnAllExpectedData()
        {
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetDocumentTypeList";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Asserts
            Assert.AreEqual("P", WebApiTest.Node(0, 0, "Key"));
            Assert.AreEqual("Passport", WebApiTest.Node(0, 0, "Value"));
            Assert.AreEqual("N", WebApiTest.Node(0, 1, "Key"));
            Assert.AreEqual("National Id", WebApiTest.Node(0, 1, "Value"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        //***************************************************************************************************************************************************
        // Visit/GetGenderList Test Cases
        //***************************************************************************************************************************************************

        [Test]
        public void GetGenderList_ReturnAllExpectedData()
        {
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetGenderList";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetGenderList_ReturnAllExpectedData

            // Asserts
            Assert.AreEqual("N", WebApiTest.Node(0, 0, "Key"));
            Assert.AreEqual("Undefined", WebApiTest.Node(0, 0, "Value"));
            Assert.AreEqual("M", WebApiTest.Node(0, 1, "Key"));
            Assert.AreEqual("Male", WebApiTest.Node(0, 1, "Value"));
            Assert.AreEqual("F", WebApiTest.Node(0, 2, "Key"));
            Assert.AreEqual("Female", WebApiTest.Node(0, 2, "Value"));

            #endregion Asserts for GetGenderList_ReturnAllExpectedData

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        //***************************************************************************************************************************************************
        // Visit/GetShipsList Test Cases
        //***************************************************************************************************************************************************

        [Test]
        public void GetShipsList_ReturnAllExpectedData()
        {
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetShipsList";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetShipsList_ReturnAllExpectedData

            // Asserts
            Assert.AreEqual(14, WebApiTest.Node<int>(0, 0, "Id"));
            Assert.AreEqual("MBE", WebApiTest.Node(0, 0, "Code"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 0, "Name"));
            Assert.IsNotEmpty(WebApiTest.Node<string>(0, 0, "ImageBase64"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Description"));

            #endregion Asserts for GetShipsList_ReturnAllExpectedData

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        //***************************************************************************************************************************************************
        // Visit/GetShipsPlacesList Test Cases
        //***************************************************************************************************************************************************

        [Test]
        public void GetShipsPlacesList_MissingURIParam_ReturnHttpStatusNotFound()
        {
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetShipsPlacesList";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request, _ThrowException: WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.NotFound, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void GetShipsPlacesList_EmptyURIParam_ReturnHttpStatusBadRequestWithInformation()
        {
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetShipsPlacesList?facilityId=";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request, _ThrowException: WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetShipsPlacesList_EmptyURIParam_ReturnHttpStatusBadRequestWithInformation

            // Asserts
            Assert.AreEqual("The request is invalid.", WebApiTest.Node("Message"));

            #endregion Asserts for GetShipsPlacesList_EmptyURIParam_ReturnHttpStatusBadRequestWithInformation

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }

        [Test]
        public void GetShipsPlacesList_WrongValueURIParam_ReturnHttpStatusBadRequestWithInformation()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create post random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetShipsPlacesList?facilityId={random}";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request, _ThrowException: WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetShipsPlacesList_WrongValueURIParam_ReturnHttpStatusBadRequestWithInformation

            // Asserts
            Assert.AreEqual("The request is invalid.", WebApiTest.Node("Message"));

            #endregion Asserts for GetShipsPlacesList_WrongValueURIParam_ReturnHttpStatusBadRequestWithInformation

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void GetShipsPlacesList_WrongFacilityIdURIParam_ReturnHttpStatusOKWithoutAnyData()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Non-existing facility
            string FacilityId = "444";

            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetShipsPlacesList?facilityId={FacilityId}";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request, _ThrowException: WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetShipsPlacesList_WrongFacilityIdURIParam_ReturnHttpStatusOKWithoutAnyData

            // Asserts
            Assert.IsEmpty(WebApiTest.Node()); //JSON result array is empty

            #endregion Asserts for GetShipsPlacesList_WrongFacilityIdURIParam_ReturnHttpStatusOKWithoutAnyData

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void GetShipsPlacesList_ProperFacilityIdURIParam_ReturnHttpStatusOKAndAllExpectedData()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Existing facility
            string FacilityId = "14";

            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetShipsPlacesList?facilityId={FacilityId}";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetShipsPlacesList_ProperFacilityIdURIParam_ReturnHttpStatusOKAndAllExpectedData

            // Asserts
            Assert.AreEqual(14, WebApiTest.Node<int>(0, 0, "ShipId"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 0, "ShipName"));
            Assert.AreEqual(18, WebApiTest.Node<int>(0, 0, "PlaceId"));
            Assert.AreEqual("CHECKCC", WebApiTest.Node(0, 0, "PlaceCode"));
            Assert.AreEqual("Check In -", WebApiTest.Node(0, 0, "PlaceName"));
            Assert.AreEqual(new DateTime(2022, 4, 5), WebApiTest.Node<DateTime>(0, 0, "Date"));
            Assert.AreEqual(1702, WebApiTest.Node<int>(0, 0, "EventId"));
            Assert.AreEqual(14, WebApiTest.Node<int>(0, 1, "ShipId"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 1, "ShipName"));
            Assert.AreEqual(19, WebApiTest.Node<int>(0, 1, "PlaceId"));
            Assert.AreEqual("RIBBONCHCB", WebApiTest.Node(0, 1, "PlaceCode"));
            Assert.AreEqual("Ribbon Reef 9", WebApiTest.Node(0, 1, "PlaceName"));
            Assert.AreEqual(new DateTime(2022, 4, 5), WebApiTest.Node<DateTime>(0, 1, "Date"));
            Assert.AreEqual(1702, WebApiTest.Node<int>(0, 1, "EventId"));
            Assert.AreEqual(14, WebApiTest.Node<int>(0, 2, "ShipId"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 2, "ShipName"));
            Assert.AreEqual(29, WebApiTest.Node<int>(0, 2, "PlaceId"));
            Assert.AreEqual("TRIN", WebApiTest.Node(0, 2, "PlaceCode"));
            Assert.AreEqual("8:00am disembark at Trinity Wharf", WebApiTest.Node(0, 2, "PlaceName"));
            Assert.AreEqual(new DateTime(2022, 4, 8), WebApiTest.Node<DateTime>(0, 2, "Date"));
            Assert.AreEqual(1702, WebApiTest.Node<int>(0, 2, "EventId"));
            Assert.AreEqual(14, WebApiTest.Node<int>(0, 3, "ShipId"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 3, "ShipName"));
            Assert.AreEqual(34, WebApiTest.Node<int>(0, 3, "PlaceId"));
            Assert.AreEqual("RIBBONLBPR", WebApiTest.Node(0, 3, "PlaceCode"));
            Assert.AreEqual("Ribbon Reefs 4 & 5", WebApiTest.Node(0, 3, "PlaceName"));
            Assert.AreEqual(new DateTime(2022, 4, 6), WebApiTest.Node<DateTime>(0, 3, "Date"));
            Assert.AreEqual(1702, WebApiTest.Node<int>(0, 3, "EventId"));
            Assert.AreEqual(14, WebApiTest.Node<int>(0, 4, "ShipId"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 4, "ShipName"));
            Assert.AreEqual(35, WebApiTest.Node<int>(0, 4, "PlaceId"));
            Assert.AreEqual("RIBBONSBFP", WebApiTest.Node(0, 4, "PlaceCode"));
            Assert.AreEqual("Ribbon Reef 3", WebApiTest.Node(0, 4, "PlaceName"));
            Assert.AreEqual(new DateTime(2022, 4, 7), WebApiTest.Node<DateTime>(0, 4, "Date"));
            Assert.AreEqual(1702, WebApiTest.Node<int>(0, 4, "EventId"));
            Assert.AreEqual(14, WebApiTest.Node<int>(0, 5, "ShipId"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 5, "ShipName"));
            Assert.AreEqual(36, WebApiTest.Node<int>(0, 5, "PlaceId"));
            Assert.AreEqual("FLT/C-L", WebApiTest.Node(0, 5, "PlaceCode"));
            Assert.AreEqual("Chartered flight from Cairns to Lizard Island.", WebApiTest.Node(0, 5, "PlaceName"));
            Assert.AreEqual(new DateTime(2022, 4, 5), WebApiTest.Node<DateTime>(0, 5, "Date"));
            Assert.AreEqual(1702, WebApiTest.Node<int>(0, 5, "EventId"));
            Assert.AreEqual(14, WebApiTest.Node<int>(0, 6, "ShipId"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 6, "ShipName"));
            Assert.AreEqual(36, WebApiTest.Node<int>(0, 6, "PlaceId"));
            Assert.AreEqual("FLT/C-L", WebApiTest.Node(0, 6, "PlaceCode"));
            Assert.AreEqual("Chartered flight from Cairns to Lizard Island.", WebApiTest.Node(0, 6, "PlaceName"));
            Assert.AreEqual(new DateTime(2022, 4, 16), WebApiTest.Node<DateTime>(0, 6, "Date"));
            Assert.AreEqual(1702, WebApiTest.Node<int>(0, 6, "EventId"));
            Assert.AreEqual(14, WebApiTest.Node<int>(0, 7, "ShipId"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 7, "ShipName"));
            Assert.AreEqual(36, WebApiTest.Node<int>(0, 7, "PlaceId"));
            Assert.AreEqual("FLT/C-L", WebApiTest.Node(0, 7, "PlaceCode"));
            Assert.AreEqual("Chartered flight from Cairns to Lizard Island.", WebApiTest.Node(0, 7, "PlaceName"));
            Assert.AreEqual(new DateTime(2023, 2, 3), WebApiTest.Node<DateTime>(0, 7, "Date"));
            Assert.AreEqual(1702, WebApiTest.Node<int>(0, 7, "EventId"));
            Assert.AreEqual(14, WebApiTest.Node<int>(0, 8, "ShipId"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 8, "ShipName"));
            Assert.AreEqual(56, WebApiTest.Node<int>(0, 8, "PlaceId"));
            Assert.AreEqual("DEPPL", WebApiTest.Node(0, 8, "PlaceCode"));
            Assert.AreEqual("Depart Port Lincoln", WebApiTest.Node(0, 8, "PlaceName"));
            Assert.AreEqual(new DateTime(2022, 6, 5), WebApiTest.Node<DateTime>(0, 8, "Date"));
            Assert.AreEqual(1702, WebApiTest.Node<int>(0, 8, "EventId"));

            #endregion Asserts for GetShipsPlacesList_ProperFacilityIdURIParam_ReturnHttpStatusOKAndAllExpectedData

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        //***************************************************************************************************************************************************
        // Visit/GetVisitList Test Cases
        //***************************************************************************************************************************************************

        [Test]
        public void GetVisitList_MissingURIParam_ReturnHttpStatusNotFound()
        {
            // Login
            WebVisitLogin("Visagentemail.wijy3407BT@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetVisitList";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request, _ThrowException: WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.NotFound, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            //Test passed
            Assert.Pass();
        }


        [Test]
        public void GetVisitList_EmptyURIParam_ReturnHttpStatusBadRequestWithInformation()
        {
            // Login
            WebVisitLogin("Visagentemail.wijy3407BT@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetVisitList?folioId=";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request, _ThrowException: WebApiTest.ThrowAnyException.No_AsItIsExpected);
             
            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetVisitList_EmptyURIParam_ReturnHttpStatusBadRequestWithInformation

            // Asserts
            Assert.AreEqual("The request is invalid.", WebApiTest.Node("Message"));

            #endregion Asserts for GetVisitList_EmptyURIParam_ReturnHttpStatusBadRequestWithInformation

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void GetVisitList_WrongValueURIParam_ReturnHttpStatusBadRequestWithInformation()
        {
            // Prerequisites
            // -------------------------------------------------------------------------------------------------------------
            // Create post random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Login
            WebVisitLogin("Visagentemail.wijy3407BT@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetVisitList?folioId={random}";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request, _ThrowException: WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetVisitList_WrongValueURIParam_ReturnHttpStatusBadRequestWithInformation

            // Asserts
            Assert.AreEqual("The request is invalid.", WebApiTest.Node("Message"));

            #endregion Asserts for GetVisitList_WrongValueURIParam_ReturnHttpStatusBadRequestWithInformation

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void GetVisitList_WrongFolioIdURIParam_ReturnHttpStatusOKWithoutAnyData()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Non-existing visitor agent folio
            string VisitorAgentFolioId = "444999";

            // Login
            WebVisitLogin("Visagentemail.wijy3407BT@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetVisitList?folioId={VisitorAgentFolioId}";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request, _ThrowException: WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetVisitList_WrongFolioIdURIParam_ReturnHttpStatusOKWithoutAnyData

            // Asserts
            Assert.IsEmpty(WebApiTest.Node()); //JSON result array is empty

            #endregion Asserts for GetVisitList_WrongFolioIdURIParam_ReturnHttpStatusOKWithoutAnyData

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void GetVisitList_ExistedVisitorAgentFolioIdURIParamWithNoVisits_ReturnHttpStatusOKWithoutAnyData()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Existing visitor agent folio without any visits
            string VisitorAgentFolioId = "37216";

            // Login
            WebVisitLogin("apivisitemptyagent@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetVisitList?folioId={VisitorAgentFolioId}";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request, _ThrowException: WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetVisitList_ExistedVisitorAgentFolioIdURIParamWithNoVisits_ReturnHttpStatusOKWithoutAnyData

            // Asserts
            Assert.IsEmpty(WebApiTest.Node()); //JSON result array is empty

            #endregion Asserts for GetVisitList_ExistedVisitorAgentFolioIdURIParamWithNoVisits_ReturnHttpStatusOKWithoutAnyData

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void GetVisitList_ProperFolioIdURIParam_ReturnHttpStatusOKAndAllExpectedData()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Existing visitor agent folio
            string VisitorAgentFolioId = "37178";

            // Login
            WebVisitLogin("Visagentemail.wijy3407BT@gmail.xxx");

            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Visit/GetVisitList?folioId={VisitorAgentFolioId}";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetVisitList_ProperFolioIdURIParam_ReturnHttpStatusOKAndAllExpectedData

            // Asserts
            Assert.AreEqual(21512, WebApiTest.Node<int>(0, 0, "Id"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 0, "ShipName"));
            Assert.AreEqual(new DateTime(2022, 4, 5), WebApiTest.Node<DateTime>(0, 0, "Date"));
            Assert.AreEqual("Check In -", WebApiTest.Node(0, 0, "Port"));
            Assert.AreEqual("Confirm", WebApiTest.Node(0, 0, "Status"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "Visitors", 0, "Id"));
            Assert.AreEqual(37209, WebApiTest.Node<int>(0, 0, "Visitors", 0, "FolioId"));
            Assert.AreEqual("ContrahentName1", WebApiTest.Node(0, 0, "Visitors", 0, "Forename"));
            Assert.AreEqual("ContrahentSurname1", WebApiTest.Node(0, 0, "Visitors", 0, "Surname"));
            Assert.AreEqual(new DateTime(2013, 9, 2), WebApiTest.Node<DateTime>(0, 0, "Visitors", 0, "BirthDate"));
            Assert.AreEqual("Male", WebApiTest.Node(0, 0, "Visitors", 0, "Gender"));
            Assert.AreEqual("Borstendorfer Str. 61 o", WebApiTest.Node(0, 0, "Visitors", 0, "Address"));
            Assert.AreEqual("Eppendorf", WebApiTest.Node(0, 0, "Visitors", 0, "City"));
            Assert.AreEqual("cntrhnt1@gmail.xxx", WebApiTest.Node(0, 0, "Visitors", 0, "Email"));
            Assert.AreEqual(491736402327, WebApiTest.Node<double>(0, 0, "Visitors", 0, "Phone"));
            Assert.AreEqual("Passport", WebApiTest.Node(0, 0, "Visitors", 0, "DocumentType"));
            Assert.AreEqual(4574564647, WebApiTest.Node<double>(0, 0, "Visitors", 0, "DocumentNumber"));
            Assert.AreEqual("ContrahentName1", WebApiTest.Node(0, 0, "Visitors", 0, "DocumentForename"));
            Assert.AreEqual("ContrahentSurname1", WebApiTest.Node(0, 0, "Visitors", 0, "DocumentSurname"));
            Assert.AreEqual("4203 Tule Cv", WebApiTest.Node(0, 0, "Visitors", 0, "DocumentAddress"));
            Assert.AreEqual(new DateTime(2024, 3, 1), WebApiTest.Node<DateTime>(0, 0, "Visitors", 0, "DocumentValidDate"));
            Assert.AreEqual(21513, WebApiTest.Node<int>(0, 1, "Id"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 1, "ShipName"));
            Assert.AreEqual(new DateTime(2022, 4, 5), WebApiTest.Node<DateTime>(0, 1, "Date"));
            Assert.AreEqual("Check In -", WebApiTest.Node(0, 1, "Port"));
            Assert.AreEqual("Confirm", WebApiTest.Node(0, 1, "Status"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 1, "Visitors", 0, "Id"));
            Assert.AreEqual(37210, WebApiTest.Node<int>(0, 1, "Visitors", 0, "FolioId"));
            Assert.AreEqual("ContrahentName2", WebApiTest.Node(0, 1, "Visitors", 0, "Forename"));
            Assert.AreEqual("ContrahentSurname2", WebApiTest.Node(0, 1, "Visitors", 0, "Surname"));
            Assert.AreEqual(new DateTime(2017, 9, 4), WebApiTest.Node<DateTime>(0, 1, "Visitors", 0, "BirthDate"));
            Assert.AreEqual("Undefined", WebApiTest.Node(0, 1, "Visitors", 0, "Gender"));
            Assert.AreEqual("", WebApiTest.Node(0, 1, "Visitors", 0, "Address"));
            Assert.AreEqual("", WebApiTest.Node(0, 1, "Visitors", 0, "City"));
            Assert.AreEqual("cntrhnt2@gamil.xxx", WebApiTest.Node(0, 1, "Visitors", 0, "Email"));
            Assert.AreEqual("", WebApiTest.Node(0, 1, "Visitors", 0, "Phone"));
            Assert.AreEqual("Passport", WebApiTest.Node(0, 1, "Visitors", 0, "DocumentType"));
            Assert.AreEqual(62357423574, WebApiTest.Node<double>(0, 1, "Visitors", 0, "DocumentNumber"));
            Assert.AreEqual("ContrahentName2", WebApiTest.Node(0, 1, "Visitors", 0, "DocumentForename"));
            Assert.AreEqual("ContrahentSurname2", WebApiTest.Node(0, 1, "Visitors", 0, "DocumentSurname"));
            Assert.AreEqual("", WebApiTest.Node(0, 1, "Visitors", 0, "DocumentAddress"));
            Assert.AreEqual(new DateTime(2023, 3, 1), WebApiTest.Node<DateTime>(0, 1, "Visitors", 0, "DocumentValidDate"));
            Assert.AreEqual(21514, WebApiTest.Node<int>(0, 2, "Id"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 2, "ShipName"));
            Assert.AreEqual(new DateTime(2022, 4, 5), WebApiTest.Node<DateTime>(0, 2, "Date"));
            Assert.AreEqual("Check In -", WebApiTest.Node(0, 2, "Port"));
            Assert.AreEqual("Confirm", WebApiTest.Node(0, 2, "Status"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 2, "Visitors", 0, "Id"));
            Assert.AreEqual(37211, WebApiTest.Node<int>(0, 2, "Visitors", 0, "FolioId"));
            Assert.AreEqual("ContrahentName3", WebApiTest.Node(0, 2, "Visitors", 0, "Forename"));
            Assert.AreEqual("ContrahentSurname3", WebApiTest.Node(0, 2, "Visitors", 0, "Surname"));
            Assert.AreEqual(new DateTime(2017, 9, 4), WebApiTest.Node<DateTime>(0, 2, "Visitors", 0, "BirthDate"));
            Assert.AreEqual("Undefined", WebApiTest.Node(0, 2, "Visitors", 0, "Gender"));
            Assert.AreEqual("", WebApiTest.Node(0, 2, "Visitors", 0, "Address"));
            Assert.AreEqual("", WebApiTest.Node(0, 2, "Visitors", 0, "City"));
            Assert.AreEqual("cntrhnt3@gamil.xxx", WebApiTest.Node(0, 2, "Visitors", 0, "Email"));
            Assert.AreEqual("", WebApiTest.Node(0, 2, "Visitors", 0, "Phone"));
            Assert.AreEqual("Passport", WebApiTest.Node(0, 2, "Visitors", 0, "DocumentType"));
            Assert.AreEqual(346234623463, WebApiTest.Node<double>(0, 2, "Visitors", 0, "DocumentNumber"));
            Assert.AreEqual("ContrahentName3", WebApiTest.Node(0, 2, "Visitors", 0, "DocumentForename"));
            Assert.AreEqual("ContrahentSurname3", WebApiTest.Node(0, 2, "Visitors", 0, "DocumentSurname"));
            Assert.AreEqual("", WebApiTest.Node(0, 2, "Visitors", 0, "DocumentAddress"));
            Assert.AreEqual(new DateTime(2024, 6, 1), WebApiTest.Node<DateTime>(0, 2, "Visitors", 0, "DocumentValidDate"));
            Assert.AreEqual(21515, WebApiTest.Node<int>(0, 3, "Id"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 3, "ShipName"));
            Assert.AreEqual(new DateTime(2022, 4, 5), WebApiTest.Node<DateTime>(0, 3, "Date"));
            Assert.AreEqual("Check In -", WebApiTest.Node(0, 3, "Port"));
            Assert.AreEqual("Waiting for Approval", WebApiTest.Node(0, 3, "Status"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 3, "Visitors", 0, "Id"));
            Assert.AreEqual(37212, WebApiTest.Node<int>(0, 3, "Visitors", 0, "FolioId"));
            Assert.AreEqual("ContrahentName4", WebApiTest.Node(0, 3, "Visitors", 0, "Forename"));
            Assert.AreEqual("ContrahentSurname4", WebApiTest.Node(0, 3, "Visitors", 0, "Surname"));
            Assert.AreEqual(new DateTime(2018, 9, 3), WebApiTest.Node<DateTime>(0, 3, "Visitors", 0, "BirthDate"));
            Assert.AreEqual("Undefined", WebApiTest.Node(0, 3, "Visitors", 0, "Gender"));
            Assert.AreEqual("", WebApiTest.Node(0, 3, "Visitors", 0, "Address"));
            Assert.AreEqual("", WebApiTest.Node(0, 3, "Visitors", 0, "City"));
            Assert.AreEqual("cntrhnt4@gamil.xxx", WebApiTest.Node(0, 3, "Visitors", 0, "Email"));
            Assert.AreEqual("", WebApiTest.Node(0, 3, "Visitors", 0, "Phone"));
            Assert.AreEqual("Passport", WebApiTest.Node(0, 3, "Visitors", 0, "DocumentType"));
            Assert.AreEqual(36234632, WebApiTest.Node<int>(0, 3, "Visitors", 0, "DocumentNumber"));
            Assert.AreEqual("ContrahentName4", WebApiTest.Node(0, 3, "Visitors", 0, "DocumentForename"));
            Assert.AreEqual("ContrahentSurname4", WebApiTest.Node(0, 3, "Visitors", 0, "DocumentSurname"));
            Assert.AreEqual("", WebApiTest.Node(0, 3, "Visitors", 0, "DocumentAddress"));
            Assert.AreEqual(new DateTime(2023, 6, 1), WebApiTest.Node<DateTime>(0, 3, "Visitors", 0, "DocumentValidDate"));

            #endregion Asserts for GetVisitList_ProperFolioIdURIParam_ReturnHttpStatusOKAndAllExpectedData

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        //***************************************************************************************************************************************************
        // Visit/RegisterVisitAgent Test Cases
        //***************************************************************************************************************************************************

        [Test]
        public void RegisterVisitAgent_WithoutAdvisor_ReturnAllExpectedData()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create post random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Visitor Company preliminaries
            string VisCompanyName = $"VisCompany{random}";

            // Visitor Agent preliminaries
            string VisAgentPassword = "//||//old//"; //this is treated as empty
            string VisAgentForename = $"VisAgentName{random}";
            string VisAgentSurname = $"VisAgentSurname{random}";
            string VisAgentEmail = $"Visagentemail.{random}@gmail.xxx";
            string VisAgentPhone = "48876957674";
            string VisAgentType = "E";
            bool VisAgentEnabled = false;

            // Advisor Agent - none
            int AdvisorFolioId = 0;

            // Login
            LoginByLP();

            // Step: 1
            //-------------------------------------------------------------------------------------------------------------
            // Api:
            //  /QuickSearch/SearchAgencies
            // Info:
            //  Confirm the visitor company doesn't exist yet (in this step we need to be logged-in)
            //-------------------------------------------------------------------------------------------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/QuickSearch/SearchAgencies?searchTerm={VisCompanyName}";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request, _ThrowException: WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check visitor company if exists
            #region Asserts for QuickSearchVisitorCompanyBeforeRegistration

            // Asserts
            Assert.IsEmpty(WebApiTest.Node()); //JSON result array is empty

            #endregion Asserts for QuickSearchVisitorCompanyBeforeRegistration

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Step: 2
            //-------------------------------------------------------------------------------------------------------------
            // Api:
            //  /QuickSearch/SearchFolios
            // Info:
            //  Confirm the visitor folio doesn't exist yet (in this step we need to be logged-in)
            //-------------------------------------------------------------------------------------------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/QuickSearch/SearchFolios?searchTerm={VisAgentSurname}&onlyRecordsCount=1";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check visitor folio if exists
            #region Asserts for QuickSearchVisitorFolioBeforeRegistration

            // Asserts
            Assert.IsEmpty(WebApiTest.Node("Data", 0)); //JSON result array is empty
            Assert.AreEqual(0, WebApiTest.Node<int>("Count"));

            #endregion Asserts for QuickSearchVisitorFolioBeforeRegistration            

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Step: 3
            //-------------------------------------------------------------------------------------------------------------
            // Api:
            //  /QuickSearch/RegisterVisitAgent
            // Info:
            //  Register new visitor agent  
            //-------------------------------------------------------------------------------------------------------------
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/RegisterVisitAgent";

            // Register visit agent payload
            var visitAgentPayload = new
            {
                CompanyName = VisCompanyName,
                CompanyCity = "VisCity",
                CompanyAddress = "VisAddress",
                CompanyPostal = "VisPostal",
                AgentForename = VisAgentForename,
                AgentSurname = VisAgentSurname,
                AgentEmail = VisAgentEmail,
                AgentPhone = VisAgentPhone
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, visitAgentPayload);

            // Assertions
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Step: 4
            //-------------------------------------------------------------------------------------------------------------
            // Api:
            //  /QuickSearch/SearchFolios
            // Info:
            //  Confirm the visitor folio is created (in this step we need to be logged-in)
            //-------------------------------------------------------------------------------------------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/QuickSearch/SearchFolios?searchTerm={VisAgentSurname}&onlyRecordsCount=1";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Grab folUId for visitor folio
            int VisAgentFolioId = WebApiTest.Node<int>("Data", 0, "Id");

            // Step: 5
            //-------------------------------------------------------------------------------------------------------------
            // Api:
            //  /Folio/GetFolio
            // Info:
            //  Confirm the most important visitor folio data (in this step we need to be logged-in)
            //  - Type 'E'
            //  - No Password
            //  - Disabled
            //  - without Advisor Id - none
            //-------------------------------------------------------------------------------------------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Folio/GetFolio?folioId={VisAgentFolioId}";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check crucial Asserts

            #region Asserts for GetFolio_ReturnCorrectVisitorAgentFolio

            // Asserts
            Assert.AreEqual(VisAgentFolioId, WebApiTest.Node<int>("FolioId"));
            Assert.AreEqual(VisAgentPassword, WebApiTest.Node("Details", "Password"));
            Assert.AreEqual(VisAgentType, WebApiTest.Node("Details", "Type"));
            Assert.AreEqual(275, WebApiTest.Node<int>("Details", "NationalityId"));
            Assert.AreEqual(6, WebApiTest.Node<int>("Details", "AgeGroupId"));
            Assert.AreEqual(VisAgentForename, WebApiTest.Node("Details", "Forename"));
            Assert.AreEqual(VisAgentSurname, WebApiTest.Node("Details", "Surname"));
            Assert.AreEqual(VisAgentPhone, WebApiTest.Node("Details", "Phone1"));
            Assert.AreEqual(VisAgentEmail, WebApiTest.Node("Details", "Email1"));
            Assert.AreEqual(275, WebApiTest.Node<int>("Details", "CountryId"));
            Assert.AreEqual("N", WebApiTest.Node("Details", "Gender"));
            Assert.AreEqual(AdvisorFolioId, WebApiTest.Node<int>("AdditionalDetails", "AdvisorId"));
            Assert.AreEqual(5, WebApiTest.Node<int>("AdditionalDetails", "LanguageId"));
            Assert.AreEqual(300, WebApiTest.Node<int>("AdditionalDetails", "CurrencyId"));
            Assert.AreEqual(false, WebApiTest.Node<bool>("AdditionalDetails", "CanMerge"));
            Assert.AreEqual(VisAgentEnabled, WebApiTest.Node<bool>("AdditionalDetails", "Enabled"));

            #endregion Asserts for GetFolio_ReturnCorrectVisitorAgentFolio

            // Check some count of asserts - full check not needed
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Step: 6
            //-------------------------------------------------------------------------------------------------------------
            // Api:
            //  /Note/GetFolioNotes
            // Info:
            //  Check created new note 'VR' for visitor folio 
            //-------------------------------------------------------------------------------------------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Note/GetFolioNotes?folioId={VisAgentFolioId}";

            // Send GET request authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            //Check crucial Asserts
            #region Asserts for CreatedNoteForVisitorFolio

            //Asserts

            Assert.GreaterOrEqual(WebApiTest.Node<DateTime>(0, 0, "CreatedDate"), new DateTime(2021, 10, 1, 11, 50, 23, 623));
            Assert.AreEqual("RA", WebApiTest.Node(0, 0, "Type"));
            Assert.AreEqual("A", WebApiTest.Node(0, 0, "VoidType"));
            Assert.AreEqual("VisRegAcc", WebApiTest.Node(0, 0, "TemplateCode"));
            Assert.AreEqual("WebDirect", WebApiTest.Node(0, 0, "UserName"));
            Assert.AreEqual("Visit Register Agent Account", WebApiTest.Node(0, 0, "TemplateName"));

            #endregion Asserts for CreatedNoteForVisitorFolio

            // Check some count of asserts - full check not needed
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            //Test passed
            Assert.Pass();
        }


        [Test]
        public void RegisterVisitAgent_AllVisitAgentCorrectData_ReturnAllCorrectData()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create post random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Visitor Company preliminaries
            string VisCompanyName = $"VisCompany{random}";

            // Visitor Agent preliminaries
            string VisAgentPassword = "//||//old//"; //this is treated as empty
            string VisAgentForename = $"VisAgentName{random}";
            string VisAgentSurname = $"VisAgentSurname{random}";
            string VisAgentEmail = $"Visagentemail{random}@gmail.xxx";
            string VisAgentPhone = "48876957674";
            string VisAgentType = "E";
            bool VisAgentEnabled = false;

            // Advisor Agent (Lucas Janine) preliminaries
            int AdvisorFolioId = 41;

            // Login
            LoginByLP();

            // Step: 1
            //-------------------------------------------------------------------------------------------------------------
            // Api:
            //  /QuickSearch/SearchAgencies
            // Info:
            //  Confirm the visitor company doesn't exist yet (in this step we need to be logged-in)
            //-------------------------------------------------------------------------------------------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/QuickSearch/SearchAgencies?searchTerm={VisCompanyName}";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request, _ThrowException:WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check visitor company if exists
            #region Asserts for QuickSearchVisitorCompanyBeforeRegistration

            //Asserts
            Assert.IsEmpty(WebApiTest.Node()); //JSON result array is empty

            #endregion Asserts for QuickSearchVisitorCompanyBeforeRegistration

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Step: 2
            //-------------------------------------------------------------------------------------------------------------
            // Api:
            //  /QuickSearch/SearchFolios
            // Info:
            //  Confirm the visitor folio doesn't exist yet (in this step we need to be logged-in)
            //-------------------------------------------------------------------------------------------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/QuickSearch/SearchFolios?searchTerm={VisAgentSurname}&onlyRecordsCount=1";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check visitor folio if exists
            #region Asserts for QuickSearchVisitorFolioBeforeRegistration

            //Asserts
            Assert.IsEmpty(WebApiTest.Node("Data", 0)); //JSON result array is empty
            Assert.AreEqual(0, WebApiTest.Node<int>("Count"));

            #endregion Asserts for QuickSearchVisitorFolioBeforeRegistration            

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Step: 3
            //-------------------------------------------------------------------------------------------------------------
            // Api:
            //  /QuickSearch/RegisterVisitAgent
            // Info:
            //  Register new visitor agent  
            //-------------------------------------------------------------------------------------------------------------
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/RegisterVisitAgent";

            // Register visit agent payload
            var visitAgentPayload = new
            {
                CompanyName = VisCompanyName,
                CompanyCity = "VisCity",
                CompanyAddress = "VisAddress",
                CompanyPostal = "VisPostal",
                AgentForename = VisAgentForename,
                AgentSurname = VisAgentSurname,
                AgentEmail = VisAgentEmail,
                AgentPhone = VisAgentPhone,
                CruiselineForename = "Janine",
                CruiselineSurname = "Lucas",
                CruiselineEmail = "lucasj@gmail.xxx",
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, visitAgentPayload);

            // Assertions
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Step: 4
            //-------------------------------------------------------------------------------------------------------------
            // Api:
            //  /QuickSearch/SearchFolios
            // Info:
            //  Confirm the visitor folio is created (in this step we need to be logged-in)
            //-------------------------------------------------------------------------------------------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/QuickSearch/SearchFolios?searchTerm={VisAgentSurname}&onlyRecordsCount=1";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Grab folUId for visitor folio
            int VisAgentFolioId = WebApiTest.Node<int>("Data", 0, "Id");

            // Step: 5
            //-------------------------------------------------------------------------------------------------------------
            // Api:
            //  /Folio/GetFolio
            // Info:
            //  Confirm the most important visitor folio data (in this step we need to be logged-in)
            //  - Type 'E'
            //  - No Password
            //  - Disabled
            //  - with Advisor Id
            //-------------------------------------------------------------------------------------------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Folio/GetFolio?folioId={VisAgentFolioId}";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check crucial Asserts
            #region Asserts for GetFolio_ReturnCorrectVisitorAgentFolio

            // Asserts
            Assert.AreEqual(VisAgentFolioId, WebApiTest.Node<int>("FolioId"));
            Assert.AreEqual(VisAgentPassword, WebApiTest.Node("Details", "Password"));
            Assert.AreEqual(VisAgentType, WebApiTest.Node("Details", "Type"));
            Assert.AreEqual(275, WebApiTest.Node<int>("Details", "NationalityId"));
            Assert.AreEqual(6, WebApiTest.Node<int>("Details", "AgeGroupId"));
            Assert.AreEqual(VisAgentForename, WebApiTest.Node("Details", "Forename"));
            Assert.AreEqual(VisAgentSurname, WebApiTest.Node("Details", "Surname"));
            Assert.AreEqual(VisAgentPhone, WebApiTest.Node("Details", "Phone1"));
            Assert.AreEqual(VisAgentEmail, WebApiTest.Node("Details", "Email1"));
            Assert.AreEqual(275, WebApiTest.Node<int>("Details", "CountryId"));
            Assert.AreEqual("N", WebApiTest.Node("Details", "Gender"));
            Assert.AreEqual(AdvisorFolioId, WebApiTest.Node<int>("AdditionalDetails", "AdvisorId"));
            Assert.AreEqual(5, WebApiTest.Node<int>("AdditionalDetails", "LanguageId"));
            Assert.AreEqual(300, WebApiTest.Node<int>("AdditionalDetails", "CurrencyId"));
            Assert.AreEqual(false, WebApiTest.Node<bool>("AdditionalDetails", "CanMerge"));
            Assert.AreEqual(VisAgentEnabled, WebApiTest.Node<bool>("AdditionalDetails", "Enabled"));

            #endregion Asserts for GetFolio_ReturnCorrectVisitorAgentFolio

            // Check some count of asserts - full check not needed
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Step: 7
            //-------------------------------------------------------------------------------------------------------------
            // Api:
            //  /Note/GetFolioNotes
            // Info:
            //  Check created new note 'VR' for visitor folio 
            //-------------------------------------------------------------------------------------------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Note/GetFolioNotes?folioId={VisAgentFolioId}";

            // Send GET request authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check crucial Asserts
            #region Asserts for CreatedNoteForVisitorFolio

            // Asserts
            Assert.GreaterOrEqual(WebApiTest.Node<DateTime>(0, 0, "CreatedDate"), new DateTime(2021, 10, 1, 11, 50, 23, 623));
            Assert.AreEqual("RA", WebApiTest.Node(0, 0, "Type"));
            Assert.AreEqual("A", WebApiTest.Node(0, 0, "VoidType"));
            Assert.AreEqual("VisRegAcc", WebApiTest.Node(0, 0, "TemplateCode"));
            Assert.AreEqual("WebDirect", WebApiTest.Node(0, 0, "UserName"));
            Assert.AreEqual("Visit Register Agent Account", WebApiTest.Node(0, 0, "TemplateName"));

            #endregion Asserts for CreatedNoteForVisitorFolio

            // Check some count of asserts - full check not needed
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void RegisterVisitAgent_ValidateNullVisitPayload_ReturnHttpStatusBadRequestWithErrorMessage()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/RegisterVisitAgent";

            // Register visit agent payload
            Object visitAgentPayload = null;

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, visitAgentPayload, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Assertions
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Assert errors
            #region Asserts for RegisterVisitAgent_ValidateNullVisitPayload_ReturnHttpStatusBadRequestWithErrorMessage

            // Asserts
            Assert.AreEqual("Given parameter is null", WebApiTest.Node("0"));

            #endregion Asserts for RegisterVisitAgent_ValidateNullVisitPayload_ReturnHttpStatusBadRequestWithErrorMessage

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void RegisterVisitAgent_NullVisitorDataInPayload_ReturnHttpStausBadRequestWithErrorMessages()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create post random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Visitor Company preliminaries
            string VisCompanyName = null;

            // Visitor Agent preliminaries
            string VisAgentForename = null;
            string VisAgentSurname = null;
            string VisAgentEmail = null;

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/RegisterVisitAgent";

            // Register visit agent payload
            var visitAgentPayload = new
            {
                CompanyName = VisCompanyName,
                CompanyCity = $"VisCity{random}",
                CompanyAddress = $"VisAddress{random}",
                CompanyPostal = "VisPostal",
                AgentForename = VisAgentForename,
                AgentSurname = VisAgentSurname,
                AgentEmail = VisAgentEmail,
                AgentPhone = "48876957674",
                CruiselineForename = "Janine",
                CruiselineSurname = "Lucas",
                CruiselineEmail = "lucasj@gmail.xxx"
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, visitAgentPayload, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Asserts
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));
            Assert.AreEqual("Given Agent Forename is wrong\r\nGiven Agent Surname is wrong\r\nGiven Agent Email is wrong\r\nGiven Agent Company Name is wrong", WebApiTest.Node("0"));

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void RegisterVisitAgent_WrongAdvisorDataInPayload_ReturnHttpStatusOK()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create post random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Visitor Company preliminaries
            string VisCompanyName = $"VisCompany{random}";

            // Visitor Agent preliminaries
            string VisAgentForename = $"VisAgentName{random}";
            string VisAgentSurname = $"VisAgentSurname{random}";
            string VisAgentEmail = $"Visagentemail.{random}@gmail.xxx";

            // Advisor Agent incorrect preliminaries
            string AdvForename = "JanineXXX";
            string AdvSurname = "LucasXXX";
            string AdvEmail = "lucasjXXX@gmail.xxx";

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/RegisterVisitAgent";

            // Register visit agent payload
            var visitAgentPayload = new
            {
                CompanyName = VisCompanyName,
                CompanyCity = "VisCity",
                CompanyAddress = "VisAddress",
                CompanyPostal = "VisPostal",
                AgentForename = VisAgentForename,
                AgentSurname = VisAgentSurname,
                AgentEmail = VisAgentEmail,
                AgentPhone = "48876957674",
                CruiselineForename = AdvForename,
                CruiselineSurname = AdvSurname,
                CruiselineEmail = AdvEmail
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, visitAgentPayload, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Confirm no messages in return (QA_GenerateAsserts)
            #region Asserts for RegisterVisitAgent_WrongAdvisorDataInPayload_ReturnHttpStatusOK

            // Asserts

            #endregion Asserts for RegisterVisitAgent_WrongAdvisorDataInPayload_ReturnHttpStatusOK

            // Test passed
            Assert.Pass();
        }


        [Test]
        [Ignore("Obsolete. TemplateCode not needed anymore in the structure.")]
        [Description("Needs to be checked if NteTemplateCode is needed anymore. If confirmed not needed the test is passed.")]
        public void RegisterVisitAgent_IncorrectOnlyNoteTemplate_ReturnHttpStatusOK()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Create post random data 
            string random = WebApiTest.QA_GenerateRandomFixedData();

            // Visitor Company preliminaries
            string VisCompanyName = $"VisCompany{random}";

            // Visitor Agent preliminaries
            string VisAgentForename = $"VisAgentName{random}";
            string VisAgentSurname = $"VisAgentSurname{random}";
            string VisAgentEmail = $"Visagentemail.{random}@gmail.xxx";

            // Advisor Agent preliminaries
            string AdvForename = "Janine";
            string AdvSurname = "Lucas";
            string AdvEmail = "lucasj@gmail.xxx";

            // Note template incorrect preliminaries
            string NteTemplateCode = "XXXYYY";

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/RegisterVisitAgent";

            // Register visit agent payload
            var visitAgentPayload = new
            {
                CompanyName = VisCompanyName,
                CompanyCity = "VisCity",
                CompanyAddress = "VisAddress",
                CompanyPostal = "VisPostal",
                AgentForename = VisAgentForename,
                AgentSurname = VisAgentSurname,
                AgentEmail = VisAgentEmail,
                AgentPhone = "48876957674",
                CruiselineForename = AdvForename,
                CruiselineSurname = AdvSurname,
                CruiselineEmail = AdvEmail,
                TemplateCode = NteTemplateCode
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, visitAgentPayload, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Asserts
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Confirm no messages in return (QA_GenerateAsserts)
            #region Asserts for RegisterVisitAgent_IncorrectOnlyNoteTemplate_ReturnHttpStatusOK

            //Asserts

            #endregion Asserts for RegisterVisitAgent_IncorrectOnlyNoteTemplate_ReturnHttpStatusOK

            // Test passed
            Assert.Pass();
        }


        //***************************************************************************************************************************************************
        // Visit/VisitExists Test Cases
        //***************************************************************************************************************************************************

        [Test]
        public void VisitExists_ProperVisitorData_ResultTrue_ReturnHttpStatusOK()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Visitor booking data
            int VisEventId = 1702;
            int VisAgentId = 37178;
            DateTime VisVisitDate = new DateTime(2022, 04, 05);

            // Visitor contractor data
            var VisVisitorData = new VisitHelpers.VisitorData
            {
                Id = 1,
                FolioId = 37209,
                Forename = "ContrahentName1",
                Surname = "ContrahentSurname1",
                BirthDate = new DateTime(2013, 09, 02),
                Gender = "M",
                Address = "Borstendorfer Str. 61 o",
                City = "Eppendorf",
                Email = "cntrhnt1@gmail.xxx",
                Phone = "491736402327",
                DocumentType = "P",
                DocumentNumber = "4574564647",
                DocumentForename = "ContrahentName1",
                DocumentSurname = "ContrahentSurname1",
                DocumentAddress = "4203 Tule Cv",
                DocumentValidDate = new DateTime(2024, 03, 01),
            };

            // Add to list
            var VisVisitorList = new List<VisitHelpers.VisitorData>();
            VisVisitorList.Add(VisVisitorData);

            // Login
            WebVisitLogin("Visagentemail.wijy3407BT@gmail.xxx");

            // Add visit payload
            var VisitPayload = new
            {
                EventId = VisEventId,
                AgentId = VisAgentId,
                VisitDate = VisVisitDate,
                Visitors = VisVisitorList
            };

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/VisitExists";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, VisitPayload);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for VisitExists_ProperVisitorData_ResultTrue_ReturnHttpStatusOK

            // Asserts
            Assert.AreEqual(true, WebApiTest.Node<bool>("0"));

            #endregion Asserts for VisitExists_ProperVisitorData_ResultTrue_ReturnHttpStatusOK

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void VisitExists_WrongVisitorSurname_ResultFalse_ReturnHttpStatusOK()
        {
            // Prerequisites
            //-------------------------------------------------------------------------------------------------------------
            // Visitor booking data
            int VisEventId = 1702;
            int VisAgentId = 37178;
            DateTime VisVisitDate = new DateTime(2022, 04, 05);

            // Visitor contractor data
            var VisVisitorData = new VisitHelpers.VisitorData
            {
                Id = 1,
                FolioId = 37209,
                Forename = "ContrahentName1",
                Surname = "WRONGSurname1",
                BirthDate = new DateTime(2013, 09, 02),
                Gender = "M",
                Address = "Borstendorfer Str. 61 o",
                City = "Eppendorf",
                Email = "cntrhnt1@gmail.xxx",
                Phone = "491736402327",
                DocumentType = "P",
                DocumentNumber = "4574564647",
                DocumentForename = "ContrahentName1",
                DocumentSurname = "ContrahentSurname1",
                DocumentAddress = "4203 Tule Cv",
                DocumentValidDate = new DateTime(2024, 03, 01),
            };

            // Add to list
            var VisVisitorList = new List<VisitHelpers.VisitorData>();
            VisVisitorList.Add(VisVisitorData);

            // Login
            WebVisitLogin("Visagentemail.wijy3407BT@gmail.xxx");

            // Add visit payload
            var VisitPayload = new
            {
                EventId = VisEventId,
                AgentId = VisAgentId,
                VisitDate = VisVisitDate,
                Visitors = VisVisitorList
            };

            // Main api path
            WebApiTest.ApiUriRequest = "Api/Visit/VisitExists";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, VisitPayload);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for VisitExists_WrongVisitorSurname_ResultFalse_ReturnHttpStatusOK

            // Asserts
            Assert.AreEqual(false, WebApiTest.Node<bool>("0"));

            #endregion Asserts for VisitExists_WrongVisitorSurname_ResultFalse_ReturnHttpStatusOK

            // Check full count of asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect());

            //Test passed
            Assert.Pass();
        }


        [TearDown]
        public void CleanUp()
        {
            WebApiTest.ClearAll();
        }
    }
}