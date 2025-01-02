using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Setup;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace KnowYourCustomer.Tests
{
    [TestFixture("Test")]
    public class KnowYourCustomersReferralTests : PostKYCReferralBody
    {
        private string environment;

        public KnowYourCustomersReferralTests(string environment)
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
        public void Post_KYC_Referral_Pos()
        {
            // Create body
            PostKYCReferralBody body = new PostKYCReferralBody()
            {
                ReferralCode = "R37HYE"
            };

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/KnowYourCustomers/Referral"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            if (response.Content.Contains($"User is already referred"))
            {
                Assert.Ignore("User is already referred");
            }
            else
            {
                Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            }
        }


        [Test]
        public void Post_KYC_InvalidReferralCode_Neg()
        {
            // Create body
            PostKYCReferralBody body = new PostKYCReferralBody()
            {
                ReferralCode = "R37HYEF"
            };

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/KnowYourCustomers/Referral"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage("ReferralCode must be 6 alphanumeric characters", response);
        }


        [Test]
        public void Post_KYC_NotFoundReferralCode_Neg()
        {
            // Create body
            PostKYCReferralBody body = new PostKYCReferralBody()
            {
                ReferralCode = "R37HYF"
            };

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/KnowYourCustomers/Referral"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Pretty JSON
            JToken content = JToken.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assert.AreEqual("ReferralCode is not valid", content.ToString());
        }


        [Test]
        public void Get_KYC_ReferralCode_Pos()
        {
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/KnowYourCustomers/Referral"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Pretty JSON
            JToken content = JToken.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual("72CF52", content.SelectToken("ReferralCode").ToString());
        }


        [Test]
        public void Get_KYC_ReferralCode_NoAuth_Neg()
        {
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/KnowYourCustomers/Referral"),
                                           Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage("Client does not have permission to access this service.", response);
        }
    }
}
