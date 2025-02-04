using Gluwa.SDK_dotnet.Clients;
using Gluwa.SDK_dotnet.Models.Exchange;
using Gluwa.SDK_dotnet.Tests;
using Gluwa.SDK_dotnet.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;
using TestContext = NUnit.Framework.TestContext;

namespace ExchangeClientTests
{
    [TestFixture("Sandbox")]
    [TestFixture("Production")]
    class ExchangeRequestsTests
    {
        private readonly string useEnvironment;

        /// <summary>
        /// Parsed exchange ID 
        /// </summary>
        private Guid GuidID { get; set; }

        /// <summary>
        /// From TestFixture
        /// </summary>
        /// <param name="platform"></param>
        public ExchangeRequestsTests(string useEnvironment)
        {
            this.useEnvironment = useEnvironment;
        }

        [SetUp]
        public void Setup()
        {
            // Set environment
            SharedMethods.SetEnvironmentVariables(useEnvironment);
        }


        [TestMethod]
        [Category("Gluwacoin")]
        [Description("TestCaseId: C1831")]
        public async Task ExchangeRequests_Gluwacoin_Pos(EConversion conversion, string sourceAddress,
                                                         string sourcePrivate)
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // EConversion to string
            string converse = conversion.ToString();

            // Get exchange request
            var exchangeRequest = SharedMethods.GetExchangeRequest(converse);
            var exchangeID = exchangeRequest.ID;
            var destAddress = exchangeRequest.DestinationAddress;
            var executor = exchangeRequest.Executor;
            var exblockNo = exchangeRequest.ExpiryBlockNumber;

            // String to Guid 
            GuidID = Guid.Parse(exchangeID);

            // Add options
            AcceptExchangeRequest quoteRequest = new AcceptExchangeRequest()
            {
                ID = GuidID,                                                        // required ID
                Conversion = conversion,                                            // required conversion
                DestinationAddress = destAddress,                                   // required destination address
                Executor = executor,                                                // optional, included only when the source currency is a Gluwacoin currency.
                ExpiryBlockNumber = BigInteger.Parse(exblockNo),                    // optional, included only when the source currency is a Gluwacoin currency.
                SourceAmount = "1000",                                              // the amount in source currency
                Fee = "0"                                                           // the fee amount
            };

            // Call
            var result = await exchangeClient.AcceptExchangeRequestAsync(
                SharedMethods.API_KEY,                                              // required user api key
                SharedMethods.API_SECRET,                                           // required user api secret
                sourceAddress,                                                      // required source address 
                sourcePrivate,                                                      // required source address private key
                quoteRequest                                                        // order request      
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }


        [TestMethod]
        [Category("Btc")]
        [Description("TestCaseId: C1832")]
        public async Task ExchangeRequests_Btc_Pos(EConversion conversion, string sourceAddress,
                                                                           string sourcePrivate)
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);
            string converse = conversion.ToString();

            // Get exchange request
            var exchangeRequest = SharedMethods.GetExchangeRequest(converse);
            var exchangeID = exchangeRequest.ID;
            var destAddress = exchangeRequest.DestinationAddress;
            var fundsAddress = exchangeRequest.ReservedFundsAddress;
            var script = exchangeRequest.ReservedFundsRedeemScript;

            // String to Guid 
            GuidID = Guid.Parse(exchangeID);

            // Add options
            AcceptExchangeRequest quoteRequest = new AcceptExchangeRequest()
            {
                ID = GuidID,                                                     // required ID
                Conversion = conversion,                                         // required conversion
                DestinationAddress = destAddress,                                // required destination address
                ReservedFundsAddress = fundsAddress,                             // optional, included only when use Btc currency.
                ReservedFundsRedeemScript = script,                              // optional, included only when use Btc currency.
                SourceAmount = "0.0001",                                         // the amount in source currency
                Fee = "0"                                                        // the fee amount
            };

            // Call
            var result = await exchangeClient.AcceptExchangeRequestAsync(
                SharedMethods.API_KEY,                                          // required user api key
                SharedMethods.API_SECRET,                                       // required user api secret
                sourceAddress,                                                  // required source address 
                sourcePrivate,                                                  // required source address private key
                quoteRequest                                                    // order request      
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }


        [Test]
        [Category("KRWG/USDG")]
        public async Task ExchangeRequests_KrwgUsdg_Pos()
        {
            // Call
            await ExchangeRequests_Gluwacoin_Pos((EConversion)Conversion.KrwgUsdg, SharedMethods.SRC_ADR_KRWG,
                                                 SharedMethods.SRC_PRIVATE_KRWG);
        }


        [Test]
        [Category("USDG/KRWG")]
        public async Task ExchangeRequests_UsdgKrwg_Pos()
        {
            // Call
            await ExchangeRequests_Gluwacoin_Pos((EConversion)Conversion.UsdgKrwg, SharedMethods.SRC_ADR_USDG,
                                                 SharedMethods.SRC_PRIVATE_USDG);
        }


        [Test]
        [Category("BTC/KRWG")]
        public async Task ExchangeRequests_BtcKrwg_Pos()
        {
            // Call
            await ExchangeRequests_Btc_Pos((EConversion)Conversion.BtcKrwg, SharedMethods.SRC_ADR_BTC,
                                           SharedMethods.SRC_PRIVATE_BTC);
        }


        [Test]
        [Category("BTC/sUSDCG")]
        public async Task ExchangeRequests_BtcsUsdcg_Pos()
        {
            // Call
            await ExchangeRequests_Btc_Pos(EConversion.BtcsUsdcg, SharedMethods.SRC_ADR_BTC,
                                           SharedMethods.SRC_PRIVATE_BTC);
        }


        [Test]
        [Category("BTC/USG")]
        public async Task ExchangeRequests_BtcsUsdg_Pos()
        {
            // Call
            await ExchangeRequests_Btc_Pos((EConversion)Conversion.BtcUsdg, SharedMethods.SRC_ADR_BTC,
                                           SharedMethods.SRC_PRIVATE_BTC);
        }


        [Test]
        [Category("KRWG/BTC")]
        public async Task ExchangeRequests_KrwgBtc_Pos()
        {
            // Call
            await ExchangeRequests_Gluwacoin_Pos((EConversion)Conversion.KrwgBtc, SharedMethods.SRC_ADR_KRWG,
                                                 SharedMethods.SRC_PRIVATE_KRWG);
        }


        [Test]
        [Category("sUSDCG/BTC")]
        public async Task ExchangeRequests_sUsdcgBtc_Pos()
        {
            // Call
            await ExchangeRequests_Gluwacoin_Pos(EConversion.sUsdcgBtc, SharedMethods.SRC_ADR_sUSDCG,
                                                 SharedMethods.SRC_PRIVATE_sUSDCG);
        }


        [Test]
        [Category("USDG/BTC")]
        public async Task ExchangeRequests_UsdgBtc_Pos()
        {
            // Call
            await ExchangeRequests_Gluwacoin_Pos((EConversion)Conversion.UsdgBtc, SharedMethods.SRC_ADR_USDG,
                                                 SharedMethods.SRC_PRIVATE_USDG);
        }


        [Test]
        [Description("TestCaseId: C1833")]
        public async Task ExchangeRequests_InvalidSourceAddress_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);
            AcceptExchangeRequest quoteRequest = new AcceptExchangeRequest()
            {
                ID = Guid.Parse(SharedMethods.EXCHANGE_ID),
                Conversion = (EConversion)Conversion.KrwgUsdg,
                DestinationAddress = SharedMethods.DEST_ADR_USDG,
                Executor = SharedMethods.EXECUTOR,
                ExpiryBlockNumber = BigInteger.Parse(SharedMethods.EXPIRYBLOCK_NO),
                SourceAmount = "1000",
                Fee = "0"
            };

            // Call
            var result = await exchangeClient.AcceptExchangeRequestAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                SharedMethods.SRC_ADR_BTC,                                    // invalid source address
                SharedMethods.SRC_PRIVATE_KRWG,
                quoteRequest
                );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }


        [Test]
        [Description("TestCaseId: C1834")]
        public async Task ExchangeRequests_InvalidPrivateKey_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // Add options
            AcceptExchangeRequest quoteRequest = new AcceptExchangeRequest()
            {
                ID = Guid.Parse(SharedMethods.EXCHANGE_ID),
                Conversion = (EConversion)Conversion.KrwgUsdg,
                DestinationAddress = SharedMethods.DEST_ADR_USDG,
                Executor = SharedMethods.EXECUTOR,
                ExpiryBlockNumber = BigInteger.Parse(SharedMethods.EXPIRYBLOCK_NO),
                SourceAmount = "1000",
                Fee = "0"
            };

            // Call
            var result = await exchangeClient.AcceptExchangeRequestAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                SharedMethods.SRC_ADR_KRWG,
                SharedMethods.SRC_PRIVATE_BTC,                                           // invalid private key
                quoteRequest
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }


        [Test]
        [Description("TestCaseId: C1835")]
        public async Task ExchangeRequests_InvalidDestAddress_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // Add options
            AcceptExchangeRequest quoteRequest = new AcceptExchangeRequest()
            {
                ID = Guid.Parse(SharedMethods.EXCHANGE_ID),
                Conversion = (EConversion)Conversion.KrwgUsdg,
                DestinationAddress = SharedMethods.TRG_ADR_BTC,                              // invalid destination address
                Executor = SharedMethods.EXECUTOR,
                ExpiryBlockNumber = BigInteger.Parse(SharedMethods.EXPIRYBLOCK_NO),
                SourceAmount = "1000",
                Fee = "0"
            };

            // Call
            var result = await exchangeClient.AcceptExchangeRequestAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                SharedMethods.SRC_ADR_KRWG,
                SharedMethods.SRC_PRIVATE_KRWG,
                quoteRequest
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }


        [Test]
        [Description("TestCaseId: C1836")]
        public async Task ExchangeRequests_InvalidExecutor_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // Add options
            AcceptExchangeRequest quoteRequest = new AcceptExchangeRequest()
            {
                ID = Guid.Parse(SharedMethods.EXCHANGE_ID),
                Conversion = (EConversion)Conversion.KrwgUsdg,
                DestinationAddress = SharedMethods.DEST_ADR_USDG,
                Executor = SharedMethods.DEST_ADR_BTC,                                       // invalid executor
                ExpiryBlockNumber = BigInteger.Parse(SharedMethods.EXPIRYBLOCK_NO),
                SourceAmount = "1000",
                Fee = "0"
            };

            // Call
            var result = await exchangeClient.AcceptExchangeRequestAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                SharedMethods.SRC_ADR_KRWG,
                SharedMethods.SRC_PRIVATE_KRWG,
                quoteRequest
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }


        [Test]
        [Description("TestCaseId: C1837")]
        public async Task ExchangeRequests_InvalidBlockNo_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // Add options
            AcceptExchangeRequest quoteRequest = new AcceptExchangeRequest()
            {
                ID = Guid.Parse(SharedMethods.EXCHANGE_ID),
                Conversion = (EConversion)Conversion.KrwgUsdg,
                DestinationAddress = SharedMethods.DEST_ADR_USDG,
                Executor = SharedMethods.EXECUTOR,
                ExpiryBlockNumber = BigInteger.Parse(SharedMethods.INVALID_BLOCK_NO),  // invalid block No
                SourceAmount = "1000",
                Fee = "0"
            };

           // Call
           var result = await exchangeClient.AcceptExchangeRequestAsync(
               SharedMethods.API_KEY,
               SharedMethods.API_SECRET,
               SharedMethods.SRC_ADR_KRWG,
               SharedMethods.SRC_PRIVATE_KRWG,
               quoteRequest
              );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }


        [Test]
        [Description("TestCaseId: C1838")]
        public async Task ExchangeRequests_InvalidAmount_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // Add options
            AcceptExchangeRequest quoteRequest = new AcceptExchangeRequest()
            {
                ID = Guid.Parse(SharedMethods.EXCHANGE_ID),
                Conversion = (EConversion)Conversion.KrwgUsdg,
                DestinationAddress = SharedMethods.DEST_ADR_USDG,
                Executor = SharedMethods.EXECUTOR,
                ExpiryBlockNumber = BigInteger.Parse(SharedMethods.EXPIRYBLOCK_NO),
                SourceAmount = "-1.33",                                                            // invalid amount
                Fee = "0"
            };

            // Call
            var result = await exchangeClient.AcceptExchangeRequestAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                SharedMethods.SRC_ADR_KRWG,
                SharedMethods.SRC_PRIVATE_KRWG,
                quoteRequest
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }


        [Test]
        [Description("TestCaseId: C1839")]
        public async Task ExchangeRequests_InvalidFee_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // Add options
            AcceptExchangeRequest quoteRequest = new AcceptExchangeRequest()
            {
                ID = Guid.Parse(SharedMethods.EXCHANGE_ID),
                Conversion = (EConversion)Conversion.KrwgUsdg,
                DestinationAddress = SharedMethods.DEST_ADR_USDG,
                Executor = SharedMethods.EXECUTOR,
                ExpiryBlockNumber = BigInteger.Parse(SharedMethods.EXPIRYBLOCK_NO),
                SourceAmount = "1000",
                Fee = "-1.33"                                                                       // invalid fee
            };

            // Call
            var result = await exchangeClient.AcceptExchangeRequestAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                SharedMethods.SRC_ADR_KRWG,
                SharedMethods.SRC_PRIVATE_KRWG,
                quoteRequest
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }


        [Test]
        [Description("TestCaseId: C1840")]
        public async Task ExchangeRequests_InvalidFundsAddress_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // Add options
            AcceptExchangeRequest quoteRequest = new AcceptExchangeRequest()
            {
                ID = Guid.Parse(SharedMethods.EXCHANGE_ID),
                Conversion = (EConversion)Conversion.BtcUsdg,
                DestinationAddress = SharedMethods.DEST_ADR_BTC,
                ReservedFundsAddress = SharedMethods.INVALID_ADDRESS,                               // invalid funds address
                ReservedFundsRedeemScript = SharedMethods.FUNDS_SCRIPT,
                SourceAmount = "1000",
                Fee = "0"
            };

            // Call
            var result = await exchangeClient.AcceptExchangeRequestAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                SharedMethods.SRC_ADR_BTC,
                SharedMethods.SRC_PRIVATE_BTC,
                quoteRequest
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }


        [Test]
        [Description("TestCaseId: C1841")]
        public async Task ExchangeRequests_InvalidFundsScript_Neg()
        {
            // Arrange
            ExchangeClient exchangeClient = new ExchangeClient(SharedMethods.BASE_ENV);

            // Add options
            AcceptExchangeRequest quoteRequest = new AcceptExchangeRequest()
            {
                ID = Guid.Parse(SharedMethods.EXCHANGE_ID),
                Conversion = (EConversion)Conversion.BtcUsdg,
                DestinationAddress = SharedMethods.DEST_ADR_BTC,
                ReservedFundsAddress = SharedMethods.FUNDS_ADR,
                ReservedFundsRedeemScript = SharedMethods.INVALID_PRIVATE_KEY,                        // invalid funds redeem script
                SourceAmount = "1000",
                Fee = "0"
            };

            // Call
            var result = await exchangeClient.AcceptExchangeRequestAsync(
                SharedMethods.API_KEY,
                SharedMethods.API_SECRET,
                SharedMethods.SRC_ADR_BTC,
                SharedMethods.SRC_PRIVATE_BTC,
                quoteRequest
               );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(null, result.Error.InnerErrors);
        }
    }
}
