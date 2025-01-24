using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace KnowYourCustomer.Tests
{
    [TestFixture("Test")]
    public class WitnessVerificationTests : VerifyOtpRequest
    {
        private string environment;

        public WitnessVerificationTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
        }

        [Test]
        public void Post_EndUserOtpRequest_Pos()
        {
            // Arrange
            PostEndUserOtpRequestBody body = CreateEndUserOtpRequesBody();

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("V1/OneTimePassword/Enduser"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            if (response.Content.Contains($"User has already verified OTP"))
            {
                TestContext.WriteLine(response);
                Assert.Ignore($"User has already verified OTP with another phone number");
            }
            else 
            {
                Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment); 
            }        
        }


        [Test]
        public void Post_WitnessOtpRequest_Pos()
        {
            // Arrange
            WitnessOtpRequestBody body = CreateWitnessOtpRequesBody();

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("V1/OneTimePassword"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            if (response.Content.Contains($"User has already verified OTP"))
            {
                TestContext.WriteLine(response);
                Assert.Ignore($"User has already verified Witness OTP");
            }
            else
            {
                Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            }
        }


        [Test]
        public void Post_VerifyEndUserOtpCode_Pos()
        {
            // Arrange
            VerifyOtpRequestBody body = CreateVerifyOtpRequestBody();

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("V1/OneTimePassword/Enduser/Verify"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            if (response.Content.Contains($"User has already verified OTP"))
            {
                TestContext.WriteLine(response);
                Assert.Ignore($"User has already verified OTP with another phone number");
            }
            else
            {
                Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            };
        }


        [Test]
        public void Post_VerifyWitnessOtpCode_Pos()
        {
            // Arrange
            VerifyOtpRequestBody body = CreateVerifyOtpRequestBody();

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("V1/OneTimePassword/Verify"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            if (response.Content.Contains($"User has already verified OTP"))
            {
                TestContext.WriteLine(response);
                Assert.Ignore($"User has already verified Witness OTP");
            }
            else
            {
                Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            }
        }


        [Test]
        public void Post_ValidateWitnessName_Neg()
        {
            // Arrange
            WitnessOtpRequestBody body = CreateWitnessOtpRequesBody();

            // Set Withness name = EndUser name
            body.WitnessFirstName = "QA";
            body.WitnessLastName = "Assertible";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("V1/OneTimePassword"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionMessage("You cannot be a witness to your own agreement.", response);
        }


        [Test]
        public void Post_OtpRequest_WithoutRequiredFields_Neg()
        {
            // Arrange
            WitnessOtpRequestBody body = CreateWitnessOtpRequesBody();

            body.PhoneNumber = null;
            body.WitnessFirstName = null;
            body.WitnessLastName = null;
            body.WitnessStreetAddress1 = null;
            body.WitnessCity = null;
            body.WitnessProvince = null;
            body.WitnessPostalCode = null;
            body.WitnessCountry = null;
            body.WitnessNationality = null;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("V1/OneTimePassword"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The PhoneNumber field is required.", response, 0);
            Assertions.HandleAssertionInnerMessage("The WitnessCity field is required.", response, 1);
            Assertions.HandleAssertionInnerMessage("The WitnessCountry field is required.", response, 2);
            Assertions.HandleAssertionInnerMessage("The WitnessLastName field is required.", response, 3);
            Assertions.HandleAssertionInnerMessage("The WitnessProvince field is required.", response, 4);
            Assertions.HandleAssertionInnerMessage("The WitnessFirstName field is required.", response, 5);
            Assertions.HandleAssertionInnerMessage("The WitnessPostalCode field is required.", response, 6);
            Assertions.HandleAssertionInnerMessage("The WitnessNationality field is required.", response, 7);
            Assertions.HandleAssertionInnerMessage("The WitnessStreetAddress1 field is required.", response, 8);
        }


        [Test]
        public void Post_VerifyOtp_WithoutRequiredFields_Neg()
        {
            // Arrange
            VerifyOtpRequestBody body = CreateVerifyOtpRequestBody();

            // Send request with empty 'PhoneNumber' field
            body.PhoneNumber = "";
            body.Code = "";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("V1/OneTimePassword/Enduser/Verify"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("The Code field is required.", response, 0);
            Assertions.HandleAssertionInnerMessage("The PhoneNumber field is required.", response, 1);
            
        }


        [Test]
        public void Post_OtpRequest_NoAuth_Neg()
        {
            // Arrange
            WitnessOtpRequestBody body = CreateWitnessOtpRequesBody();

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("V1/OneTimePassword"),
                                           Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
        }


        /// <summary>
        /// POST V1/OneTimePassword/Enduser body
        /// </summary>
        /// <returns></returns>
        private static PostEndUserOtpRequestBody CreateEndUserOtpRequesBody()
        {

            return new PostEndUserOtpRequestBody()
            {
                PhoneNumber = "+17788885544",             // Phone placeholder E164 Format: +1xxxxxxxxxx
                EndUserFirstName = "QA",
                EndUserLastName = "Assertible",
                EndUserBirthDate = "1982-02-10"
            };
        }


        /// <summary>
        /// POST V1/OneTimePassword body
        /// </summary>
        /// <returns></returns>
        private static WitnessOtpRequestBody CreateWitnessOtpRequesBody()
        {

            return new WitnessOtpRequestBody()
            {
                PhoneNumber = "+17788885544",             // Phone placeholder E164 Format: +1xxxxxxxxxx
                WitnessFirstName = "QA1",
                WitnessLastName = "Assertible1",
                WitnessStreetAddress1 = "123 Main",
                WitnessStreetAddress2 = "321 Broadway",
                WitnessCity = "Burnaby",
                WitnessProvince = "BC",
                WitnessPostalCode = "V3R0H2",
                WitnessCountry = "CA",
                WitnessNationality = "CA",
            };
        }


        /// <summary>
        /// POST V1/OneTimePassword/Verify body
        /// </summary>
        /// <returns></returns>
        private static VerifyOtpRequestBody CreateVerifyOtpRequestBody()
        {
            return new VerifyOtpRequestBody()
            {
                PhoneNumber = "+17788885544",  // Phone placeholder E164 Format: +1xxxxxxxxxx
                Code = "1234"                  // Code placeholder
            };
        }
    }
}
