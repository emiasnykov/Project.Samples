using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Models.ResponseBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Exchange.Tests
{
    [TestFixture("Test")]
    [TestFixture("Sandbox")]
    [Category("Exchange"), Category("Orders"), Category("Get")]
    public class GetOrdersTests
    {
        private EConversion conversion;
        private string environment;

        public GetOrdersTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            conversion = TestSettingsUtil.Conversion;
        }

        [Test]
        [Category("Positive")]
        [Category("Smoke")]
        [Description("TestCaseId:C172")]
        public void Get_Orders_Active_Pos()
        {
            // Execute GET Orders endpoint with Status = Active
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddQueryParameter("status", "active"));
            // Deserialization
            List<GetOrdersResponse> responseBody = JsonConvert.DeserializeObject<List<GetOrdersResponse>>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assertions.CheckOrderList(responseBody, "Active");
        }


        [Test]
        [Category("Positive")]
        [Category("Smoke")]
        [Description("TestCaseId:C173")]
        public void Get_Orders_Canceled_Pos()
        {
            // Execute GET Orders endpoint with Status = Canceled
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddQueryParameter("status", "Canceled"));
            // Deserialization
            List<GetOrdersResponse> responseBody = JsonConvert.DeserializeObject<List<GetOrdersResponse>>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual("application/json; charset=utf-8", response.ContentType);
            Assertions.CheckOrderList(responseBody, "Canceled");
        }

        
        [Test]
        [Category("Positive")]
        [Category("Smoke")]
        [Description("TestCaseId:C175")]
        public void Get_Orders_Limit1_Pos()
        {
            // Execute GET Orders endpoint
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddQueryParameter("limit", "1"));
            // Deserialization
            List<GetOrdersResponse> responseBody = JsonConvert.DeserializeObject<List<GetOrdersResponse>>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual("application/json; charset=utf-8", response.ContentType);

            // Ensure there's only one response
            Assert.IsTrue(responseBody.Count() <= 1);
        }


        [Test]
        [Category("Positive")]
        [Category("Smoke")]
        [Description("TestCaseId:C174")]
        public void Get_Orders_Completed_Pos()
        {
            // Execute GET Orders endpoint with Status = Completed
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddQueryParameter("status", "Complete"));
            // Deserialization
            List<GetOrdersResponse> responseBody = JsonConvert.DeserializeObject<List<GetOrdersResponse>>(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual("application/json; charset=utf-8", response.ContentType);
            Assertions.CheckOrderList(responseBody, "Completed");
        }


        [Test]
        [Category("Positive")]
        [Category("Smoke")]
        [Description("TestCaseId:C168")]
        public void Get_OrdersID_Pos()
        {
            // Arrange 
            var maker = QAKeyVault.GetGluwacoinBtcExchangeAddress("Sender", "Receiver", environment);

            // Get default quote amount
            decimal quoteAmount = EConversion.sUsdcgBtc.ToSourceCurrency().ToDefaultCurrencyAmount();

            // Execute
            string orderID = ExchangeFunctions.TryToCreateOrder(EConversion.sUsdcgBtc, quoteAmount, 1m, maker);

            // Get Order By ID
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Orders/{orderID}"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.AreEqual("application/json; charset=utf-8", response.ContentType);

            // Clean up
            if (response.StatusCode == HttpStatusCode.Created && response.StatusCode == HttpStatusCode.OK)
            {
                JObject responseBody = JObject.Parse(response.Content);
                string mOrderID = responseBody.SelectToken("ID").ToString();
                ExchangeFunctions.TearDownOrder(mOrderID);
            }
        }


        [Test]
        [Category("Negative")]
        [Description("TestCaseId:C162")]
        public void Get_OrdersID_NotFound_Neg()
        {
            // Get an Order by not found ID
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/Orders/{Shared.ORDER_NOT_FOUND}"),
                                                     Api.SendRequest(Method.GET));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedOrderNotFoundError(Shared.ORDER_NOT_FOUND), response);
        }


        [Test]
        [Category("Negative")]
        [Description("TestCaseId:C176")]
        public void Get_Orders_Status_InvalidUrlParameters_Neg()
        {
            // Execute GET Orders endpoint with Status = foobar
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth("v1/Orders"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddQueryParameter("status", "foobar"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("foobar", "status"), response);
        }
    }
}