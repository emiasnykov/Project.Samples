using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace LotteryAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    public class PrizeLinkedAccountTests : TransferFunctions
    {
        private string environment;

        public PrizeLinkedAccountTests(string environment)
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
        public void Post_ConfirmNotification_Prize_Pos()
        {
            // Create body
            string body = Shared.GetDrawId(environment); 

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/PrizeLinked/ConfirmNotification"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        public void Get_DrawSummary_Prize_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/PrizeLinked/Summary"), 
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assert.Multiple(() =>
            {
                Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
                Assert.That(objects.SelectToken("DrawId").ToString(), Is.Not.Null);
                Assert.That(objects.SelectToken("DrawTimeStamp").ToString(), Is.Not.Null);
                Assert.That(objects.SelectToken("PrizeAmount").ToString(), Is.Not.Null);
                Assert.That(objects.SelectToken("TotalParticipants").ToString(), Is.Not.Null);
                Assert.That(objects.SelectToken("PrizePool").ToString(), Is.Not.Null);
                Assert.That(objects.SelectToken("Apy").ToString(), Is.Not.Null);
                Assert.That(objects.SelectToken("PreviousDraw.DrawId").ToString(), Is.Not.Empty);
                Assert.That(objects.SelectToken("PreviousDraw.DrawTimeStamp").ToString(), Is.Not.Empty);
                Assert.That(objects.SelectToken("PreviousDraw.Status").ToString(), Is.Not.Empty);
                Assert.That(objects.SelectToken("RecentPrizes[0].DrawId").ToString(), Is.Not.Empty);
                Assert.That(objects.SelectToken("RecentPrizes[0].PrizeAmount").ToString(), Is.Not.Empty);
                Assert.That(objects.SelectToken("RecentPrizes[0].DrawTimeStamp").ToString(), Is.Not.Empty);
                Assert.That(objects.SelectToken("RecentPrizes[0].Status").ToString(), Is.Not.Empty);
            });     
        }


        [Test]
        public void Get_LifetimeEarnings_Prize_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/PrizeLinked/LifetimeEarnings"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects.SelectToken("LifeTimeEarnings").ToString(), Is.Not.Null);
        }


        [Test]
        public void Get_AccountBalance_Prize_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("V1/PrizeLinked/AvailableBalance"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects.SelectToken("Balance").ToString(), Is.Not.Null);
        }


        [Test]
        public void Get_AccountDrawNotifications_Prize_Pos()
        {

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/PrizeLinked/DrawNotifications"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Deserialization
            JArray objects = JArray.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects[0].SelectToken("DrawId").ToString(), Is.Not.Null);
            Assert.That(objects[0].SelectToken("DrawTimeStamp").ToString(), Is.Not.Null);
            Assert.That(objects[0].SelectToken("DrawHadNoWinner").ToString(), Is.Not.Null);
            Assert.That(objects[0].SelectToken("UserWasWinner").ToString(), Is.Not.Null);
            Assert.That(objects[0].SelectToken("UserHasAcknowledged").ToString(), Is.Not.Null);
            Assert.That(objects[0].SelectToken("DrawPrizeAmount").ToString(), Is.Not.Null);
            Assert.That(objects[0].SelectToken("NextDrawPrizeAmount").ToString(), Is.Not.Null);
        }


        [Test]
        public void Get_UpperLimitRoom_Prize_Pos()
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/PrizeLinked/UpperLimitRoom"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects.SelectToken("UpperLimitRoom").ToString(), Is.Not.Null);
        }


        [Test]
        public void Post_ConfirmNotification_NotFoundDrawId_Neg()
        {
            // Create body
            string body = Shared.DRAW_NOT_FOUND;

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/PrizeLinked/ConfirmNotification"),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assert.That(objects.SelectToken("Message").ToString(), Does.Contain("Invalid DrawId"));
        }
    }
}
