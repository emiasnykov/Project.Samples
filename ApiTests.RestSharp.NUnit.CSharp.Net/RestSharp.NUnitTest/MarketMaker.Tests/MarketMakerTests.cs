using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Models.ResponseBody;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace MarketMaker.Tests
{
    [TestFixture("Test")]
    [Category("MarketMaker")]
    public class MarketMakerTests : ExchangeFunctions
    {
        private static readonly TimeSpan ORDER_INTERVAL = new TimeSpan(0, 3, 0);
        private static readonly TimeSpan ORDER_INTERVAL_BEFORE = new TimeSpan(0, 0, 10);
        private static readonly TimeSpan ORDER_INTERVAL_AFTER = ORDER_INTERVAL + ORDER_INTERVAL_BEFORE; //Waiting +30 seconds
        private static EConversion conversion;
        private static EConversion takerConversion;
        private string environment;

        public MarketMakerTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up MarketMaker tests
            VerifyMarketMakerStatus(environment, true);
            SetUpCreateExchangeTests(EUserType.QAAssertible, environment);
            conversion = TestSettingsUtil.Conversion;
            takerConversion = conversion.ToReverseConversion();
            Assertions.CheckMarketMakerOrdersByConversion(conversion);
        }

        [Test]
        [Category("Postive"), Category("sUsdcgBtc")]
        public void MarketMaker_sUsdcgBtc_Pos()
        {
            // Set up taker
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("TakerSender", "TakerReceiver", environment);

            // Execute
            IRestResponse response = createQuoteForMarketMaker(taker, takerConversion, environment);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Postive"), Category("BtcsUsdcg")]
        public void MarketMaker_BtcsUsdcg_Pos()
        {
            // Set up taker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("TakerSender", "TakerReceiver", environment);

            // Execute
            IRestResponse response = createQuoteForMarketMaker(taker, takerConversion, environment);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Positive"), Category("sUsdcgBtc"), Category("IntervalOrders")]
        public void MarketMaker_Z_sUsdcgBtc_IntervalOrders_Pos()
        {
            // Set up user
            GluwaTestApi.QAGluwaUser = EUserType.QAMarketMaker;

            // Get active orders by conversion
            List<GetOrdersResponse> marketMakerOrdersList = GetOrdersByStatus("Active");
            GetOrdersResponse order = marketMakerOrdersList.Where(c => c.Conversion == "sUsdcgBtc")
                            .LastOrDefault();

            // Wait
            Thread.Sleep(ORDER_INTERVAL_BEFORE);

            // Verify MarketMaker order is  still active now.
            List<GetOrdersResponse> newMarketMakerList = GetOrdersByStatus("Active");
            List<string> orderListItems = newMarketMakerList.Select(c => c.ID).ToList();
            Assert.IsTrue(orderListItems.Contains(order.ID));

            // Wait
            Thread.Sleep(ORDER_INTERVAL_AFTER);

            // Check that it's cancelled after
            marketMakerOrdersList = GetOrdersByStatus("Canceled");
            order = marketMakerOrdersList.Where(c => c.Conversion == "sUsdcgBtc")
                            .LastOrDefault();

            // Verify that the Market Maker Order created is the correct status
            verifyMarketMakerOrderStatus(order, "Canceled", "Active");
        }


        [Test]
        [Category("Positive"), Category("BtcsUsdcg"), Category("IntervalOrders")]
        public void MarketMaker_Z_BtcsUsdcg_IntervalOrders_Pos()
        {
            // Set up user
            GluwaTestApi.QAGluwaUser = EUserType.QAMarketMaker;

            // Get active orders by conversion
            List<GetOrdersResponse> marketMakerOrdersList = GetOrdersByStatus("Active");
            GetOrdersResponse order = marketMakerOrdersList.Where(c => c.Conversion == "BtcsUsdcg")
                            .LastOrDefault();

            // Wait
            Thread.Sleep(ORDER_INTERVAL_BEFORE);
            //Verify MarketMaker order is  still active now

            // Verify MarketMaker order is  still active now.
            List<GetOrdersResponse> newMarketMakerList = GetOrdersByStatus("Active");
            List<string> orderListItems = newMarketMakerList.Select(c => c.ID).ToList();
            Assert.IsTrue(orderListItems.Contains(order.ID));

            // Wait
            Thread.Sleep(ORDER_INTERVAL_AFTER);

            // Check that it's cancelled after
            marketMakerOrdersList = GetOrdersByStatus("Canceled");
            order = marketMakerOrdersList.Where(c => c.Conversion == "BtcsUsdcg")
                            .LastOrDefault();

            // Verify that the Market Maker Order created is the correct status
            verifyMarketMakerOrderStatus(order, "Canceled", "Active");
        }


        [Test]
        [Category("Positive"), Category("sUsdcgBtc")]
        public void MarketMaker_Z_sUsdcgBtc_CreateOrderNotCanceled_Pos()
        {
            // Set User to Market maker to be able to create orders in Market Maker db
            GluwaTestApi.QAGluwaUser = EUserType.QAMarketMaker;

            // Set maker address
            var maker = QAKeyVault.GetGluwacoinBtcExchangeAddress("MarketMaker", "MarketMaker", environment);
            string orderID = TryToCreateOrder(conversion, 1m, 1m, maker);

            // Verify order has been made
            List<GetOrdersResponse> activeOrders = GetOrdersByStatus("Active");
            bool bActiveOrder = activeOrders.FindAll(c => c.ID == orderID).Any();
            Assert.IsTrue(bActiveOrder);

            // Wait
            Thread.Sleep(ORDER_INTERVAL);

            // Verify the order hasn't been cancelled
            GetOrderByIDResponse orderCreated = GetOrderByID(orderID);
            Assert.AreEqual("Active", orderCreated.Status);

            // Cancel order
            CancelOrder(orderID);
        }


        [Test]
        [Category("Positive"), Category("BtcsUsdcg")]
        public void MarketMaker_Z_BtcsUsdcg_CreateOrderNotCanceled_Pos()
        {
            // Set User to Market maker to be able to create orders in Market Maker db
            GluwaTestApi.QAGluwaUser = EUserType.QAMarketMaker;

            // Set maker address
            var maker = QAKeyVault.GetBtcGluwacoinExchangeAddress("MarketMaker", "MarketMaker", environment);
            string orderID = TryToCreateOrder(conversion, 0.0001m, 0.0001m, maker);

            // Verify order has been made
            List<GetOrdersResponse> activeOrders = GetOrdersByStatus("Active");
            bool bActiveOrder = activeOrders.FindAll(c => c.ID == orderID).Any();
            Assert.IsTrue(bActiveOrder);

            // Wait
            Thread.Sleep(ORDER_INTERVAL);

            // Verify the order hasn't been cancelled
            GetOrderByIDResponse orderCreated = GetOrderByID(orderID);
            Assert.AreEqual("Active", orderCreated.Status);

            // Cancel order
            CancelOrder(orderID);
        }


        [Test]
        [Category("Negative"), Category("sUsdcgBtc")]
        public void MarketMaker_sUsdcgBtc_ExecuteFail_Neg()
        {
            // Set up taker
            var taker = QAKeyVault.GetBtcGluwacoinExchangeAddress("ExecuteFail", "TakerReceiver", environment);

            // Execute
            IRestResponse response = createQuoteForMarketMaker(taker, takerConversion, environment);
            
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);
        }


        [Test]
        [Category("Negative"), Category("BtcsUsdcg"), Category("ExecuteFail")]
        public void MarketMaker_BtcsUsdcg_ExecuteFail_Neg()
        {
            // Set up taker
            var taker = QAKeyVault.GetGluwacoinBtcExchangeAddress("ExecuteFailUsd", "TakerReceiver", environment);

            // Execute
            IRestResponse response = createQuoteForMarketMaker(taker, takerConversion, environment);
            
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Accepted, response);

            // Write Response Details
            TestContext.WriteLine($"No Response Body to show. Case fails at execute step");
        }
    }
}
