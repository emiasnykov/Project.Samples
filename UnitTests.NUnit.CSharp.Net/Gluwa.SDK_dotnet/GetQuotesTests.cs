using Gluwa.SDK_dotnet.Clients;
using Gluwa.SDK_dotnet.Models;
using Gluwa.SDK_dotnet.Models.Exchange;
using Gluwa.SDK_dotnet.Tests;
using Gluwa.SDK_dotnet.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;
using TestContext = NUnit.Framework.TestContext;

namespace ExchangeClientTests
{
    [TestFixture("Production")]
    [TestFixture("Sandbox")]
    public class GetQuotesTests
    {
        private readonly string useEnvironment;

        /// <summary>
        /// From TestFixture
        /// </summary>
        /// <param name="platform"></param>
        public GetQuotesTests(string useEnvironment)
        {
            this.useEnvironment = useEnvironment;
        }

        [SetUp]
        public void Setup()
        {
            SharedMethods.SetEnvironmentVariables(useEnvironment);
        }

        [TestMethod]
        [Description("TestCaseId: C1671")]
        public async Task GetQuotes_Pos(ECurrency currency, string address, string privateKey)
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // Call
            var result = await exchangeClient.GetQuotesAsync(currency, address, privateKey);

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }


        [Test]
        [Category("USDG")]
        public async Task GetQuotes_Usdg_Pos()
        {
            await GetQuotes_Pos((ECurrency)Currency.USDG, SharedMethods.SRC_ADR_USDG, SharedMethods.SRC_PRIVATE_USDG);
        }


        [Test]
        [Category("sUSDCG")]
        public async Task GetQuotes_sUSDCG_Pos()
        {
            await GetQuotes_Pos(ECurrency.sUSDCG, SharedMethods.SRC_ADR_sUSDCG, SharedMethods.SRC_PRIVATE_sUSDCG);
        }


        [Test]
        [Category("KRWG")]
        public async Task GetQuotes_Krwg_Pos()
        {
            await GetQuotes_Pos((ECurrency)Currency.KRWG, SharedMethods.SRC_ADR_KRWG, SharedMethods.SRC_PRIVATE_KRWG);
        }


        [Test]
        [Category("BTC")]
        public async Task GetQuotes_Btc_Pos()
        {
            await GetQuotes_Pos(ECurrency.BTC, SharedMethods.SRC_ADR_BTC, SharedMethods.SRC_PRIVATE_BTC);
        }


        [Test]
        [Description("TestCaseId: C1675")]
        public async Task GetQuotes_WithOptions_Pos()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);
            GetQuotesOptions options = new GetQuotesOptions()
            {
                StartDateTime = new DateTime(2021, 01, 01),
                EndDateTime = new DateTime(2021, 12, 01),
                Status = EQuoteStatus.Pending,
                Offset = 0,
                Limit = 100
            };

            // Call
            var result = await exchangeClient.GetQuotesAsync(
                (ECurrency)Currency.USDG,
                SharedMethods.SRC_ADR_USDG,
                SharedMethods.SRC_PRIVATE_USDG,
                options
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }


        [Test]
        [Description("TestCaseId: C1672")]
        public async Task GetQuotes_InvalidAddress_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // Call
            var result = await exchangeClient.GetQuotesAsync(
                (ECurrency)Currency.USDG,
                SharedMethods.INVALID_ADDRESS,                                  // invalid address
                SharedMethods.SRC_PRIVATE_USDG
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("InvalidSignature", result.Error.Code);
            Assert.AreEqual("The supplied signature is not authorized for this resource.", result.Error.Message);
        }


        [Test]
        [Description("TestCaseId: C1673")]
        public async Task GetQuotes_InvalidPrivateKey_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // Call
            var result = await exchangeClient.GetQuotesAsync(
                (ECurrency)Currency.USDG,
                SharedMethods.SRC_ADR_USDG,
                SharedMethods.SRC_PRIVATE_BTC                                // invalid private key
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("InvalidSignature", result.Error.Code);
            Assert.AreEqual("The supplied signature is not authorized for this resource.", result.Error.Message);
        }


        [TestMethod]
        [Description("TestCaseId: C1683")]
        public async Task GetQuote_ByQuoteId_Pos(ECurrency currency, string privateKey, string quoteId)
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // String to Guid
            var quoteIdGuid = Guid.Parse(quoteId);

            // Call
            var result = await exchangeClient.GetQuoteAsync(
                currency,                                                     // requied carrency
                privateKey,                                                   // requied private key
                quoteIdGuid                                                   // requied quoteId
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }


        [Test]
        [Category("KRWG")]
        public async Task GetQuote_ByQuoteId_Krwg_Pos()
        {
            // Get QuoteId for currency and address
            string quoteId = SharedMethods.GetQuoteByAddress("Krwg", SharedMethods.TRG_ADR_KRWG, SharedMethods.TRG_PRIVATE_KRWG);

            // Call
            await GetQuote_ByQuoteId_Pos((ECurrency)Currency.KRWG, SharedMethods.TRG_PRIVATE_KRWG, quoteId);
        }


        [Test]
        [Category("USDG")]
        public async Task GetQuote_ByQuoteId_Usdg_Pos()
        {
            // Get QuoteId for currency and address
            string quoteId = SharedMethods.GetQuoteByAddress("Usdg", SharedMethods.SRC_ADR_USDG, SharedMethods.SRC_PRIVATE_USDG);

            // Call
            await GetQuote_ByQuoteId_Pos((ECurrency)Currency.USDG, SharedMethods.SRC_PRIVATE_USDG, quoteId);
        }


        [Test]
        [Category("sUSDCG")]
        public async Task GetQuote_ByQuoteId_sUsdcg_Pos()
        {
            // Get QuoteId for currency and address
            string quoteId = SharedMethods.GetQuoteByAddress("sUsdcg", SharedMethods.SRC_ADR_sUSDCG, SharedMethods.SRC_PRIVATE_sUSDCG);

            // Call
            await GetQuote_ByQuoteId_Pos(ECurrency.sUSDCG, SharedMethods.SRC_PRIVATE_sUSDCG, quoteId);
        }


        [Test]
        [Category("BTC")]
        public async Task GetQuote_ByQuoteId_Btc_Pos()
        {
            string quoteId;

            // Get QuoteId for currency and address
            if (SharedMethods.BASE_ENV)
                {
                    quoteId = SharedMethods.GetQuoteByAddress("btc", SharedMethods.SRC_ADR_BTC, SharedMethods.SRC_PRIVATE_BTC);
                }
            else
                    quoteId = SharedMethods.BTC_QUOTE_ID;

            // Call
            await GetQuote_ByQuoteId_Pos(ECurrency.BTC, SharedMethods.SRC_PRIVATE_BTC, quoteId);
        }


        [Test]
        [Description("TestCaseId: C1805")]
        public async Task GetQuote_ByQuoteId_InvalidKey_Neg()
        { 
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // Get QuoteId for currency and address
            string quoteId = SharedMethods.GetQuoteByAddress("Usdg", SharedMethods.SRC_ADR_USDG, SharedMethods.SRC_PRIVATE_USDG);

            // String to Guid
            var quoteIdGuid = Guid.Parse(quoteId);

            // Call
            var result = await exchangeClient.GetQuoteAsync(
                (ECurrency)Currency.USDG,
                SharedMethods.SRC_PRIVATE_KRWG,                                             // invalid private key
                quoteIdGuid
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
        }


        [Test]
        [Description("TestCaseId: C1806")]
        public async Task GetQuote_ByQuoteId_InvalidQuoteId_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // String to Guid
            var quoteIdGuid = Guid.Parse(SharedMethods.INVALID_QUOTEID);

            // Call
            var result = await exchangeClient.GetQuoteAsync(
                (ECurrency)Currency.USDG,
                SharedMethods.SRC_PRIVATE_USDG,
                quoteIdGuid                                                                 // invalid quoteId
            );
        
            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
        }
    }
}
