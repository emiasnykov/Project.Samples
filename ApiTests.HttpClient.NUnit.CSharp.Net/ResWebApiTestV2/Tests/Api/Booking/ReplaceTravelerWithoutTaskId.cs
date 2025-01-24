using System.Net;
using System.Net.Http;
using NUnit.Framework;
using ResWebApiTest.TestEngine;

namespace ResWebApiTest.Tests.Api.Booking
{
    [TestFixture]
    class ReplaceTravelerWithoutTaskId
    {
        [SetUp]
        public void Init()
        {
            // Main api path
            WebApiTest.ApiUriLogin = "Api/Login/ByLP";

            // Login credentials
            var Credentials = new
            {
                Login = "ian",
                Password = "ian"
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Login, Credentials);

            // Assertions
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Login, "StatusCode"));
            Assert.AreEqual("ian", WebApiTest.Status(WebApiTest.ApiUri.Login, "login"));
            Assert.AreEqual("bearer", WebApiTest.Status(WebApiTest.ApiUri.Login, "token_type"));
            Assert.IsNotEmpty(WebApiTest.Status(WebApiTest.ApiUri.Login, "access_token"));
        }


        [Test]
        // Replace traveler, event closed, task id 872 required
        public void ReplaceTravelerInBookingWhenEventClose_ReturnBadRequest()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "api/Booking/ReplaceTraveler";

            string s = "{\"SalutationId\":2942,\"Title\":\"test_title\",\"Forename\":\"Sam\",\"MiddleName\":\"jr.\",\"Surname\":\"Smith\",\"Alias\":\"test_alias\",\"MailingName\":\"test_mailint@rest\",\"EnvelopeName\":\"Long str. 28, West Count\",\"Street\":\"Starodworcowa 3BI\",\"City\":\"Gdynia\",\"PostalCode\":\"81-575\",\"Phone1\":\"111222333\",\"Phone2\":\"222333444\",\"Phone3\":\"333444555\",\"Email\":\"smith123@test.com1234\",\"Gender\":\"M\",\"FolioType\":\"V\",\"AgeGroupUId\":6,\"EnquiryId\":2991,\"PrivacyFlex01\":true,\"PrivacyFlex02\":true,\"PrivacyFlex03\":true,\"PrivacyFlex04\":true,\"Flex01\":\"123456\",\"Flex02\":\"test_ coments2\",\"Flex03\":\"Viktor\",\"Flex04\":\"test_coments4\",\"FolioId\":0,\"OldFolioId\":917,\"BookingId\":\"344\"}";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, s, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Nothing returned in response
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));
            Assert.Pass();
        }


        [Test]
        // Replace traveler where rebCanModifyBooking  = 0, rebCode =   21190 and task 1699 required
        public void ReplaceTravelerInBookingRebCanModifyBookingFalse_ReturnBadRequest()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "api/Booking/ReplaceTraveler";

            string s = "{\"Surname\":\"test\",\"FolioId\":0,\"OldFolioId\":16074,\"BookingId\":\"21190\"}";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, s, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Nothing returned in response
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));
            Assert.Pass();
        }


        [Test]
        // New folio cannot have flag 'block booking' assigned, folSurname = Abarcar
        public void ReplaceTravelerInBookingFolioWithBlockBooking_ReturnBadRequest()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "api/Booking/ReplaceTraveler";

            string s = "{\"FolioId\":7123,\"NationalityUId\":68,\"LanguageUId\":5,\"AgeGroupUId\":6,\"FlagId\":-22,\"SalutationUId\":0,\"Forename\":\"Ruth\",\"Midname\":\"\",\"Surname\":\"Abarcar\",\"FolioType\":\"E\",\"RepeaterNo\":\"\",\"Title\":\"\",\"Alias\":\"\",\"Gender\":\"F\",\"BirthDate\":\"1984-09-10T00:00:00\",\"Phone1\":\"Ruth.Abarcar@ats-pacific.com.a\",\"Phone2\":\"02 92682111\",\"Phone3\":\"\",\"Email\":\"\",\"Street\":\"level 10\r\n130 Elizabeth st\",\"City\":\"Sydney\",\"ProvinceUId\":2947,\"PostalCode\":\"2000\",\"CountryId\":68,\"Flex01\":\"\",\"Flex02\":\"\",\"Flex03\":\"\",\"Flex04\":\"\",\"PrivacyFlex01\":0,\"PrivacyFlex02\":0,\"PrivacyFlex03\":1,\"PrivacyFlex04\":0,\"PrivacyFlex05\":0,\"PrivacyFlex06\":0,\"PrivacyFlex07\":0,\"PrivacyFlex08\":0,\"Primary\":0,\"CategoryUId\":0,\"UnitUId\":0,\"CategoryCode\":null,\"UnitNo\":null,\"AgeGroupId\":\"A\",\"AgeGroupName\":null,\"FlagValue\":0,\"RefUId\":0,\"EnquiryId\":0,\"MailingName\":\"\",\"EnvelopeName\":\"\",\"OldFolioId\":16074,\"BookingId\":\"21190\"}";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, s, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Nothing returned in response
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));
            Assert.Pass();
        }


        [Test]
        // Age group is different than old folio's, then task is needed 847
        public void ReplaceTravelerInBookingFolioWithAnotherAgeGroup_ReturnBadRequest()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "api/Booking/ReplaceTraveler";

            string s = "{\"FolioId\":25954,\"NationalityUId\":274,\"LanguageUId\":5,\"AgeGroupUId\":2971,\"FlagId\":-21,\"SalutationUId\":0,\"Forename\":\"Chris\",\"Midname\":\"\",\"Surname\":\"Ankcorn\",\"FolioType\":\"P\",\"RepeaterNo\":\"\",\"Title\":\"\",\"Alias\":\"\",\"Gender\":\"M\",\"BirthDate\":\"2017-09-08T00:00:00\",\"Phone1\":\"+44 7443332556\",\"Phone2\":\"\",\"Phone3\":\"\",\"Email\":\"\",\"Street\":\"TBC\",\"City\":\"TBC\",\"ProvinceUId\":0,\"PostalCode\":\"TBC\",\"CountryId\":274,\"Flex01\":\"\",\"Flex02\":\"\",\"Flex03\":\"\",\"Flex04\":\"\",\"PrivacyFlex01\":0,\"PrivacyFlex02\":1,\"PrivacyFlex03\":1,\"PrivacyFlex04\":0,\"PrivacyFlex05\":0,\"PrivacyFlex06\":0,\"PrivacyFlex07\":0,\"PrivacyFlex08\":0,\"Primary\":0,\"CategoryUId\":0,\"UnitUId\":0,\"CategoryCode\":null,\"UnitNo\":null,\"AgeGroupId\":\"C\",\"AgeGroupName\":null,\"FlagValue\":-1,\"RefUId\":0,\"EnquiryId\":0,\"MailingName\":\"\",\"EnvelopeName\":\"\",\"OldFolioId\":16074,\"BookingId\":\"21190\"}";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, s, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Nothing returned in response
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));
            Assert.Pass();
        }


        [Test]
        // Replace existing folio
        public void ReplaceTravelerInBookingFolioExist_ReturnBadRequest()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "api/Booking/ReplaceTraveler";

            string s = "{\"FolioId\":16074,\"NationalityUId\":275,\"LanguageUId\":5,\"AgeGroupUId\":6,\"FlagId\":1720,\"SalutationUId\":0,\"Forename\":\"Carol\",\"Midname\":\"\",\"Surname\":\"Alexander\",\"FolioType\":\"P\",\"RepeaterNo\":\"\",\"Title\":\"\",\"Alias\":\"\",\"Gender\":\"F\",\"BirthDate\":\"1984-09-10T00:00:00\",\"Phone1\":\"251 621 1237\",\"Phone2\":\"\",\"Phone3\":\"\",\"Email\":\"carolalexander@aol.com\",\"Street\":\"8851 Carpenter Lane\",\"City\":\"Bay Minette\",\"ProvinceUId\":1360,\"PostalCode\":\"36507\",\"CountryId\":275,\"Flex01\":\"\",\"Flex02\":\"\",\"Flex03\":\"\",\"Flex04\":\"\",\"PrivacyFlex01\":0,\"PrivacyFlex02\":1,\"PrivacyFlex03\":1,\"PrivacyFlex04\":0,\"PrivacyFlex05\":0,\"PrivacyFlex06\":0,\"PrivacyFlex07\":0,\"PrivacyFlex08\":0,\"Primary\":0,\"CategoryUId\":0,\"UnitUId\":0,\"CategoryCode\":null,\"UnitNo\":null,\"AgeGroupId\":\"A\",\"AgeGroupName\":null,\"FlagValue\":65535,\"RefUId\":0,\"EnquiryId\":0,\"MailingName\":\"\",\"EnvelopeName\":\"\",\"OldFolioId\":16074,\"BookingId\":\"21190\"}";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, s, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Nothing returned in response
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));
            Assert.Pass();
        }


        [TearDown]
        public void CleanUp()
        {
            WebApiTest.ClearAll();
        }
    }
}
