using Gluwa.SDK_dotnet.Clients;
using Gluwa.SDK_dotnet.Models.Exchange;
using Gluwa.SDK_dotnet.Tests;
using Gluwa.SDK_dotnet.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;
using TestContext = NUnit.Framework.TestContext;

namespace ExchangeClientTests
{
    [TestFixture("Sandbox")]
    [TestFixture("Production")]
    class PostOrderTests
    {
        private readonly string useEnvironment;

        /// <summary>
        /// From TestFixture
        /// </summary>
        /// <param name="platform"></param>
        public PostOrderTests(string useEnvironment)
        {
            this.useEnvironment = useEnvironment;
        }

        [SetUp]
        public void Setup()
        {
            if (useEnvironment == "Production" && TestContext.CurrentContext.Test.Properties["Category"].Contains("SandboxOnly"))
            {
                Assert.Inconclusive($"Cannot run this test on environment: {useEnvironment}");
            }
            SharedMethods.SetEnvironmentVariables(useEnvironment);
        }

        [TestMethod]
        [Description("TestCaseId: C1846")]
        public async Task PostOrder_Pos(EConversion conversion, string sourceAddress, string targetAddress,
                                        string sourcePrivate, string targetPrivate)
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(true);

            // Add options
            CreateOrderRequest orderRequest = new CreateOrderRequest()
            {
                Conversion = conversion,
                SendingAddress = sourceAddress,
                ReceivingAddress = targetAddress,
                SourceAmount = "1000",
                Price = "0.001"
            };

            // Call
            var result = await exchangeClient.CreateOrderAsync(
                SharedMethods.API_KEY_SANDBOX,                                      // required user api key
                SharedMethods.API_SECRET_SANDBOX,                                   // required user api secret
                sourcePrivate,                                                      // required sourece address private key
                targetPrivate,                                                      // required target address private key
                orderRequest                                                        // order request      
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            if (JsonConvert.SerializeObject(result).Contains("NotEnoughBalance"))
            {
                Assert.Warn("Low account balance prevented this test from completing.");
            }
            else
            {
                Assert.IsTrue(result.IsSuccess);
            }
        }


        [Test]
        [Category("KRWG/USDG")]
        [Category("SandboxOnly")]
        public async Task PostOrder_KrwgUsdg_Pos()
        {
            // Call
            await PostOrder_Pos((EConversion)Conversion.KrwgUsdg, SharedMethods.TRG_ADR_KRWG_SANDBOX, SharedMethods.TRG_ADR_USDG_SANDBOX,
                                SharedMethods.TRG_PRIVATE_KRWG_SANDBOX, SharedMethods.TRG_PRIVATE_USDG_SANDBOX);
        }


        [Test]
        [Category("KRWG/BTC")]
        [Category("SandboxOnly")]
        public async Task PostOrder_KrwgBtc_Pos()
        {
            // Call
            await PostOrder_Pos((EConversion)Conversion.KrwgBtc, SharedMethods.SRC_ADR_KRWG_SANDBOX, SharedMethods.TRG_ADR_BTC_SANDBOX,
                                SharedMethods.SRC_PRIVATE_KRWG_SANDBOX, SharedMethods.TRG_PRIVATE_BTC_SANDBOX);
        }


        [Test]
        [Category("BTC/KRWG")]
        [Category("SandboxOnly")]
        public async Task PostOrder_BtcKrwg_Pos()
        {
            // Call
            await PostOrder_Pos((EConversion)Conversion.BtcKrwg, SharedMethods.SRC_ADR_BTC_SANDBOX, SharedMethods.TRG_ADR_KRWG_SANDBOX,
                                SharedMethods.SRC_PRIVATE_BTC_SANDBOX, SharedMethods.TRG_PRIVATE_KRWG_SANDBOX);
        }


        [Test]
        [Category("USDG/BTC")]
        [Category("SandboxOnly")]
        public async Task PostOrder_UsdgBtc_Pos()
        {
            // Call
            await PostOrder_Pos((EConversion)Conversion.UsdgBtc, SharedMethods.SRC_ADR_USDG_SANDBOX, SharedMethods.TRG_ADR_BTC_SANDBOX,
                                SharedMethods.SRC_PRIVATE_USDG_SANDBOX, SharedMethods.TRG_PRIVATE_BTC_SANDBOX);
        }


        [Test]
        [Category("USDG/KRWG")]
        [Category("SandboxOnly")]
        public async Task PostOrder_UsdgKrwg_Pos()
        {
            // Call
            await PostOrder_Pos((EConversion)Conversion.UsdgKrwg, SharedMethods.SRC_ADR_USDG_SANDBOX, SharedMethods.TRG_ADR_KRWG_SANDBOX,
                                SharedMethods.SRC_PRIVATE_USDG_SANDBOX, SharedMethods.TRG_PRIVATE_KRWG_SANDBOX);
        }


        [Test]
        [Category("sUSDCG/BTC")]
        [Category("SandboxOnly")]
        public async Task PostOrder_sUsdcgBtc_Pos()
        {
            // Call
            await PostOrder_Pos(EConversion.sUsdcgBtc, SharedMethods.SRC_ADR_sUSDCG_SANDBOX, SharedMethods.TRG_ADR_BTC_SANDBOX,
                                SharedMethods.SRC_PRIVATE_sUSDCG_SANDBOX, SharedMethods.TRG_PRIVATE_BTC_SANDBOX);
        }


        [Test]
        [Category("BTC/sUSDCG")]
        [Category("SandboxOnly")]
        public async Task PostOrder_BtcsUsdcg_Pos()
        {
            // Call
            await PostOrder_Pos(EConversion.BtcsUsdcg, SharedMethods.SRC_ADR_BTC_SANDBOX, SharedMethods.TRG_ADR_sUSDCG_SANDBOX,
                                SharedMethods.SRC_PRIVATE_BTC_SANDBOX, SharedMethods.TRG_PRIVATE_sUSDCG_SANDBOX);
        }


        [Test]
        [Description("TestCaseId: C1847")]
        public async Task PostOrder_InvalidAddress_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);
            CreateOrderRequest orderRequest = new CreateOrderRequest()
            {
                Conversion = (EConversion)Conversion.KrwgUsdg,
                SendingAddress = SharedMethods.INVALID_ADDRESS,
                ReceivingAddress = SharedMethods.INVALID_ADDRESS,
                SourceAmount = "1000",
                Price = "0.001"
            };

            // Call
            var result = await exchangeClient.CreateOrderAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                SharedMethods.SRC_PRIVATE_KRWG,
                SharedMethods.SRC_PRIVATE_USDG,
                orderRequest
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("ValidationError", result.Error.Code);
            Assert.AreEqual("There are one or more validation errors. See InnerErrors for more details", result.Error.Message);
        }


        [Test]
        [Description("TestCaseId: C1848")]
        public async Task PostOrder_InvalidKey_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);
            CreateOrderRequest orderRequest = new CreateOrderRequest()
            {
                Conversion = (EConversion)Conversion.KrwgUsdg,
                SendingAddress = SharedMethods.SRC_ADR_KRWG,
                ReceivingAddress = SharedMethods.TRG_ADR_USDG,
                SourceAmount = "1000",
                Price = "0.001"
            };

            // Call
            var result = await exchangeClient.CreateOrderAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                SharedMethods.INVALID_PRIVATE_KEY,
                SharedMethods.INVALID_PRIVATE_KEY,
                orderRequest
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("ValidationError", result.Error.Code);
            Assert.AreEqual("There are one or more validation errors. See InnerErrors for more details", result.Error.Message);
        }


        [Test]
        [Description("TestCaseId: C1849")]
        public async Task PostOrder_TooSmallAmount_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);
            CreateOrderRequest orderRequest = new CreateOrderRequest()
            {
                Conversion = (EConversion)Conversion.KrwgUsdg,
                SendingAddress = SharedMethods.SRC_ADR_KRWG,
                ReceivingAddress = SharedMethods.SRC_ADR_USDG,
                SourceAmount = "1",
                Price = "0.001"
            };

            // Call
            var result = await exchangeClient.CreateOrderAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                SharedMethods.SRC_PRIVATE_KRWG,
                SharedMethods.SRC_PRIVATE_USDG,
                orderRequest
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("ValidationError", result.Error.Code);
            Assert.AreEqual("SourceAmount must be greater than or equal to 1000 KRWG.", result.Error.InnerErrors[0].Message);
        }


        [Test]
        [Description("TestCaseId: C1850")]
        public async Task PostOrder_EmptyAddress_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);
            CreateOrderRequest orderRequest = new CreateOrderRequest()
            {
                Conversion = (EConversion)Conversion.KrwgUsdg,
                SendingAddress = null,                                                             //empty address
                ReceivingAddress = SharedMethods.TRG_ADR_USDG,
                SourceAmount = "1000",
                Price = "0.001"
            };

            // Call
            var result = await exchangeClient.CreateOrderAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                SharedMethods.SRC_PRIVATE_KRWG,
                SharedMethods.SRC_PRIVATE_USDG,
                orderRequest
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("WebhookNotFound", result.Error.Code);
            Assert.AreEqual("Webhook url for exchange requests is not found", result.Error.Message);
        }


        [Test]
        [Description("TestCaseId: C1851")]
        public async Task PostOrder_EmptyPrivateKey_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);
            CreateOrderRequest orderRequest = new CreateOrderRequest()
            {
                Conversion = (EConversion)Conversion.KrwgUsdg,
                SendingAddress = SharedMethods.SRC_ADR_KRWG,
                ReceivingAddress = SharedMethods.TRG_ADR_USDG,
                SourceAmount = "1000",
                Price = "0.001"
            };

            // Call
            var result = await exchangeClient.CreateOrderAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                null,                                                                         //empty key 
                SharedMethods.SRC_PRIVATE_USDG,
                orderRequest
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("WebhookNotFound", result.Error.Code);
            Assert.AreEqual("Webhook url for exchange requests is not found", result.Error.Message);
        }
    }
}
