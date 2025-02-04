using Gluwa.SDK_dotnet.Clients;
using Gluwa.SDK_dotnet.Models.Exchange;
using Gluwa.SDK_dotnet.Tests;
using Gluwa.SDK_dotnet.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;
using TestContext = NUnit.Framework.TestContext;

namespace ExchangeClientTests
{
    [TestFixture("Sandbox")]
    [TestFixture("Production")]
    class GetPendingQuoteTests
    {
        private readonly string useEnvironment;

        /// <summary>
        /// From TestFixture
        /// </summary>
        /// <param name="platform"></param>
        public GetPendingQuoteTests(string useEnvironment)
        {
            this.useEnvironment = useEnvironment;
        }

        [SetUp]
        public void Setup()
        {
            SharedMethods.SetEnvironmentVariables(useEnvironment);
        }

        [TestMethod]
        [Description("TestCaseId: C1676")]
        public async Task GetPendingQuote_Pos(EConversion conversion, string srcAdr, string trgAdr,
                                                                      string srcKey, string trgKey)
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(true);
            GetPendingQuoteRequest quoteRequest = new GetPendingQuoteRequest()
            {
                Amount = "1000",
                Conversion = conversion,
                SendingAddress = srcAdr,                                                         // requied sender address
                ReceivingAddress = trgAdr                                                        // requied reciever address
            };

            // Call
            var result = await exchangeClient.GetPendingQuoteAsync(
                srcKey,                                                                         // requied sender private key
                trgKey,                                                                         // requied reciever private key
                quoteRequest
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }


        [Test]
        [Category("KRWG/USDG")]
        public async Task GetPendingQuote_KrwgUsdg_Pos()
        {
            await GetPendingQuote_Pos((EConversion)Conversion.KrwgUsdg, SharedMethods.SRC_ADR_KRWG, SharedMethods.TRG_ADR_USDG,
                                      SharedMethods.SRC_PRIVATE_KRWG, SharedMethods.TRG_PRIVATE_USDG);
        }


        [Test]
        [Category("BTC/KRWG")]
        public async Task GetPendingQuote_BtcKrwg_Pos()
        {
            await GetPendingQuote_Pos((EConversion)Conversion.BtcKrwg, SharedMethods.SRC_ADR_BTC, SharedMethods.TRG_ADR_KRWG,
                                      SharedMethods.SRC_PRIVATE_BTC, SharedMethods.TRG_PRIVATE_KRWG);
        }


        [Test]
        [Category("BTC/sUSDCG")]
        public async Task GetPendingQuote_BtcsUsdcg_Pos()
        {
            await GetPendingQuote_Pos(EConversion.BtcsUsdcg, SharedMethods.SRC_ADR_BTC, SharedMethods.TRG_ADR_sUSDCG,
                                      SharedMethods.SRC_PRIVATE_BTC, SharedMethods.TRG_PRIVATE_sUSDCG);
        }


        [Test]
        [Category("BTC/USDG")]
        public async Task GetPendingQuote_BtcUsdg_Pos()
        {
            await GetPendingQuote_Pos((EConversion)Conversion.BtcUsdg, SharedMethods.SRC_ADR_BTC, SharedMethods.TRG_ADR_USDG,
                                      SharedMethods.SRC_PRIVATE_BTC, SharedMethods.TRG_PRIVATE_USDG);
        }


        [Test]
        [Category("KRWG/BTC")]
        public async Task GetPendingQuote_KrwgBtc_Pos()
        {
            await GetPendingQuote_Pos((EConversion)Conversion.KrwgBtc, SharedMethods.SRC_ADR_KRWG, SharedMethods.TRG_ADR_BTC,
                                      SharedMethods.SRC_PRIVATE_KRWG, SharedMethods.TRG_PRIVATE_BTC);
        }


        [Test]
        [Category("sUSDCG/BTC")]
        public async Task GetPendingQuote_sUsdcgBtc_Pos()
        {
            await GetPendingQuote_Pos(EConversion.sUsdcgBtc, SharedMethods.SRC_ADR_sUSDCG, SharedMethods.TRG_ADR_BTC,
                                      SharedMethods.SRC_PRIVATE_sUSDCG, SharedMethods.TRG_PRIVATE_BTC);
        }


        [Test]
        [Category("USDG/BTC")]
        public async Task GetPendingQuote_UsdgBtc_Pos()
        {
            await GetPendingQuote_Pos((EConversion)Conversion.UsdgBtc, SharedMethods.SRC_ADR_USDG, SharedMethods.TRG_ADR_BTC,
                                      SharedMethods.SRC_PRIVATE_USDG, SharedMethods.TRG_PRIVATE_BTC);
        }


        [Test]
        [Category("USDG/KRWG")]
        public async Task GetPendingQuote_UsdgKrwg_Pos()
        {
            await GetPendingQuote_Pos((EConversion)Conversion.UsdgKrwg, SharedMethods.SRC_ADR_USDG, SharedMethods.TRG_ADR_KRWG,
                                      SharedMethods.SRC_PRIVATE_USDG, SharedMethods.TRG_PRIVATE_KRWG);
        }


        [Test]
        [Description("TestCaseId: C1677")]
        public async Task GetPendingQuote_InvalidSenderKey_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(true);
            GetPendingQuoteRequest quoteRequest = new GetPendingQuoteRequest()
            {
                Amount = "1000",
                Conversion = (EConversion)Conversion.KrwgUsdg,
                SendingAddress = SharedMethods.SRC_ADR_KRWG,
                ReceivingAddress = SharedMethods.TRG_ADR_USDG
            };

            // Call
            var result = await exchangeClient.GetPendingQuoteAsync(
                SharedMethods.SRC_PRIVATE_BTC,                                                       // invalid sender private key
                SharedMethods.TRG_PRIVATE_USDG,
                quoteRequest
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("ValidationError", result.Error.Code);
            Assert.AreEqual("There are one or more validation errors. See InnerErrors for more details", result.Error.Message);
            Assert.AreEqual("AddressSignatureInvalid", result.Error.InnerErrors[0].Code);
            Assert.AreEqual("SendingAddressSignature is not valid for the provided SendingAddress.", result.Error.InnerErrors[0].Message);
        }


        [Test]
        [Description("TestCaseId: C1678")]
        public async Task GetPendingQuote_InvalidReceiverKey_Neg()
        {
            //Arrange
            ExchangeClient exchangeClient = new ExchangeClient(true);
            GetPendingQuoteRequest quoteRequest = new GetPendingQuoteRequest()
            {
                Amount = "1000",
                Conversion = (EConversion)Conversion.KrwgUsdg,
                SendingAddress = SharedMethods.SRC_ADR_KRWG,
                ReceivingAddress = SharedMethods.TRG_ADR_USDG
            };

            // Call
            var result = await exchangeClient.GetPendingQuoteAsync(
                SharedMethods.SRC_PRIVATE_KRWG,
                SharedMethods.TRG_PRIVATE_BTC,                                                         // invalid receiver private key
                quoteRequest
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("ValidationError", result.Error.Code);
            Assert.AreEqual("There are one or more validation errors. See InnerErrors for more details", result.Error.Message);
            Assert.AreEqual("AddressSignatureInvalid", result.Error.InnerErrors[0].Code);
            Assert.AreEqual("ReceivingAddressSignature is not valid for the provided ReceivingAddress.", result.Error.InnerErrors[0].Message);
        }


        [Test]
        [Description("TestCaseId: C1679")]
        public async Task GetPendingQuote_InvalidSenderAddress_Neg()
        {
            //Arrange
            ExchangeClient exchangeClient = new ExchangeClient(true);
            GetPendingQuoteRequest quoteRequest = new GetPendingQuoteRequest()
            {
                Amount = "1000",
                Conversion = (EConversion)Conversion.KrwgUsdg,
                SendingAddress = SharedMethods.INVALID_PRIVATE_KEY,                            // invalid sender address
                ReceivingAddress = SharedMethods.TRG_ADR_USDG
            };

            // Call
            var result = await exchangeClient.GetPendingQuoteAsync(
                SharedMethods.SRC_PRIVATE_KRWG,
                SharedMethods.TRG_PRIVATE_USDG,
                quoteRequest
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("ValidationError", result.Error.Code);
            Assert.AreEqual("There are one or more validation errors. See InnerErrors for more details", result.Error.Message);
            Assert.AreEqual("AddressSignatureInvalid", result.Error.InnerErrors[0].Code);
            Assert.AreEqual("SendingAddressSignature is not valid for the provided SendingAddress.", result.Error.InnerErrors[0].Message);
        }


        [Test]
        [Description("TestCaseId: C1680")]
        public async Task GetPendingQuote_InvalidRecieverAddress_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(true);
            GetPendingQuoteRequest quoteRequest = new GetPendingQuoteRequest()
            {
                Amount = "1000",
                Conversion = (EConversion)Conversion.KrwgUsdg,
                SendingAddress = SharedMethods.SRC_ADR_KRWG,
                ReceivingAddress = SharedMethods.INVALID_ADDRESS                                 // invalid reciever address
            };

            // Call
            var result = await exchangeClient.GetPendingQuoteAsync(
                SharedMethods.SRC_PRIVATE_KRWG,
                SharedMethods.TRG_PRIVATE_USDG,
                quoteRequest
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("ValidationError", result.Error.Code);
            Assert.AreEqual("There are one or more validation errors. See InnerErrors for more details", result.Error.Message);
            Assert.AreEqual("AddressSignatureInvalid", result.Error.InnerErrors[0].Code);
            Assert.AreEqual("ReceivingAddressSignature is not valid for the provided ReceivingAddress.", result.Error.InnerErrors[0].Message);
        }


        [Test]
        [Description("TestCaseId: C1682")]
        public async Task GetPendingQuote_AmountTooSmall_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(true);
            GetPendingQuoteRequest quoteRequest = new GetPendingQuoteRequest()
            {
                Amount = "1",                                                                           // too small amount
                Conversion = (EConversion)Conversion.KrwgUsdg,
                SendingAddress = SharedMethods.SRC_ADR_KRWG,
                ReceivingAddress = SharedMethods.TRG_ADR_USDG
            };

            // Call
            var result = await exchangeClient.GetPendingQuoteAsync(
                SharedMethods.SRC_PRIVATE_KRWG,
                SharedMethods.TRG_PRIVATE_USDG,
                quoteRequest
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("ValidationError", result.Error.Code);
            Assert.AreEqual("There are one or more validation errors. See InnerErrors for more details", result.Error.Message);
            Assert.AreEqual("AmountTooSmall", result.Error.InnerErrors[0].Code);
            Assert.AreEqual("Amount must be greater than or equal to 1000 KRWG.", result.Error.InnerErrors[0].Message);
        }
    }
}
