using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Models.ResponseBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace Exchange.Tests
{
    [TestFixture("Test")]
    [TestFixture("Sandbox")]
    [Category("Exchange"), Category("Orders"), Category("Patch")]
    public class PatchOrdersTests
    {     
        private EConversion conversion { get; set; }
        private string environment;

        public PatchOrdersTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set user and conversion
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            conversion = TestSettingsUtil.Conversion;
        }

        [Test]
        [Category("Positive")]
        [Category("Smoke")]
        [Description("TestCaseId:C169")]
        public void Patch_Orders_Pos()
        {
            // Set maker address
            var maker = QAKeyVault.GetGluwacoinBtcExchangeAddress("Sender", "Receiver", environment);

            // Create Order to get Id
            string orderID = ExchangeFunctions.TryToCreateOrder(conversion, 1.003m, 0.0001m, maker);

            // Arrange
            IRestResponse response = ExchangeFunctions.CancelOrder(orderID);
            string content = response.Content;

            // Check of the status is canceled
            GetOrderByIDResponse order = ExchangeFunctions.GetOrderByID(orderID);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual("Canceled", order.Status);
            Assert.IsTrue(string.IsNullOrEmpty(content));
        }


        [Test]
        [Category("Negative")]
        [Category("Smoke")]
        [Description("TestCaseId:C4089")]
        public void Patch_Orders_NotFound_Neg()
        {
            // Arrange
            IRestResponse response = ExchangeFunctions.CancelOrder(Shared.ORDER_NOT_FOUND);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedOrderNotFoundError(Shared.ORDER_NOT_FOUND), response);
        }


        [Test]
        [Category("Negative")]
        [Category("Smoke")]
        [Description("TestCaseId:C4090")]
        public void Patch_Orders_InvalidBody_Neg()
        {
            // Get Active Orders
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"),
                                                    Api.SendRequest(Method.GET)
                                                    .AddQueryParameter("Status", "Active"));
            // Deserialization
            List<GetOrdersResponse> responseBody = JsonConvert.DeserializeObject<List<GetOrdersResponse>>(response.Content);

            // Get Order Id
            string ID;
            if (responseBody.Count == 0)
            {
                // Case for when there's no active orders 
                var maker = QAKeyVault.GetGluwacoinBtcExchangeAddress("Sender", "Receiver", environment);
                ID = ExchangeFunctions.TryToCreateOrder(conversion, 1.003m, 0.0001m, maker);
            }
            else
            {
                ID = responseBody[0].ID;
            }

            // Arrange
            IRestResponse cancelResponse = ExchangeFunctions.CancelOrder(ID, "Foobar");

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, cancelResponse, environment);
        }
    }
}