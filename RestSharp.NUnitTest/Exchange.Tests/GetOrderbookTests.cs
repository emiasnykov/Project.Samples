using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Exchange.Tests
{
    [TestFixture("Test")]
    [TestFixture("Sandbox")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    public class GetOrderbookTests
    {
        private EConversion conversion;
        private string environment;

        public GetOrderbookTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            conversion = TestSettingsUtil.Conversion;
        }

        [Test]
        [Category("Get"), Category("Positive")]
        [Category("Smoke")]
        [Description("TestCaseId:C161")]
        public void Get_Orderbook_BtcsUsdcg_Limit1_Pos()
        {
            // Set query params with limit = 1
            Dictionary<string, string> queryLimit = new Dictionary<string, string>()
            {
                { "limit", "1" }
            };

            // Execute Orderbook endpoint
            IRestResponse response = ExchangeFunctions.GetOrderBook(conversion.ToString(), queryLimit);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);

            // Ensure only one result is returned
            List<dynamic> responseBody = JsonConvert.DeserializeObject<List<dynamic>>(response.Content);
            Assert.IsTrue(responseBody.Count() <= 1);
        }


        [Test]
        [Category("Get"), Category("Positive")]
        [Description("TestCaseId:C160")]
        public void Get_Orderbook_BtcsUsdcg_Generic_Pos()
        {
            // Execute Orderbook endpoint 
            IRestResponse response = ExchangeFunctions.GetOrderBook(conversion.ToString(), null);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Positive")]
        [Category("Smoke")]
        [Description("TestCaseId:C4080")]
        public void Get_Orderbook_sUsdcgBtc_Generic_Pos()
        {
            //Execute Orderbook endpoint 
            IRestResponse response = ExchangeFunctions.GetOrderBook(conversion.ToString(), null);

            //Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        [Category("Negative")]
        [Description("TestCaseId:C4079")]
        public void Get_Orderbook_Limit_InvalidUrlParameters_Neg()
        {
            // Get limit with invalid parameters
            Dictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "limit", "a" }
            };

            // Call orderbook endpoint
            IRestResponse response = ExchangeFunctions.GetOrderBook(conversion.ToString(), queryParams);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidQueryParameterError("a", "limit"), response);
        }
    }
}
