using Gluwa.SDK_dotnet.Clients;
using Gluwa.SDK_dotnet.Models;
using Gluwa.SDK_dotnet.Tests;
using Gluwa.SDK_dotnet.Tests.Shared;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;
using TestContext = NUnit.Framework.TestContext;

namespace GluwaClientTests
{
    [TestFixture("Sandbox")]
    [TestFixture("Production")]
    class GetTransactionsTests
    {
        private readonly string useEnvironment;

        /// <summary>
        /// From TestFixture
        /// </summary>
        /// <param name="platform"></param>
        public GetTransactionsTests(string useEnvironment)
        {
            this.useEnvironment = useEnvironment;
        }

        [SetUp]
        public void Setup()
        {
            SharedMethods.SetEnvironmentVariables(useEnvironment);
        }

        [Test]
        [Description("TestCaseId: C1105")]
        public async Task GetTransactions_USDG_Pos()
        {
            // Arrange
            GluwaClient gluwaClient = new GluwaClient(SharedMethods.BASE_ENV);

            // Call
            var result = await gluwaClient.GetTransactionListAsync(
                (ECurrency)Currency.USDG,                                         // requied currency: USDG
                SharedMethods.SRC_ADR_USDG,                                       // requied source address
                SharedMethods.SRC_PRIVATE_USDG,                                   // requied private key
                3,                                                                // optional limit, default = 100
                ETransactionStatusFilter.Confirmed,                               // optional status, default = Confirmed
                0                                                                 // optional, default = 0
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(3, result.Data.Count);
            Assert.AreEqual((ECurrency)Currency.USDG, result.Data[0].Currency);
            Assert.Contains(SharedMethods.SRC_ADR_USDG, new[] { result.Data[0].Sources[0], result.Data[0].Targets[0] },
                $"Expected {SharedMethods.SRC_ADR_USDG} but found source {result.Data[0].Sources[0]} and target {result.Data[0].Targets[0]}");       // SRC_ADR_USDG can be either source or target
        }


        [Test]
        [Description("TestCaseId: C1105")]
        public async Task GetTransactions_sUSDCG_Pos()
        {
            // Arrange
            GluwaClient gluwaClient = new GluwaClient(SharedMethods.BASE_ENV);

            // Call
            var result = await gluwaClient.GetTransactionListAsync(
                ECurrency.sUSDCG,                                                     // requied currency: sUSDCG
                SharedMethods.SRC_ADR_sUSDCG,                                         // requied source address
                SharedMethods.SRC_PRIVATE_sUSDCG,                                     // requied private key
                100,                                                                  // optional limit, default = 100
                ETransactionStatusFilter.Confirmed,                                   // optional status, default = Confirmed
                10                                                                    // optional offset, default = 0
            );

           // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(ECurrency.sUSDCG, result.Data[0].Currency);
            Assert.AreEqual(SharedMethods.SRC_ADR_sUSDCG.ToLower(), result.Data[0].Sources[0].ToLower());
        }


        [Test]
        [Description("TestCaseId: C1105")]
        public async Task GetTransactions_sNGNG_Pos()
        {
            // Arrange
            GluwaClient gluwaClient = new GluwaClient(SharedMethods.BASE_ENV);

            // Call
            var result = await gluwaClient.GetTransactionListAsync(
                ECurrency.sNGNG,                                                      // requied currency: sNGNG
                SharedMethods.SRC_ADR_sNGNG,                                          // requied source address
                SharedMethods.SRC_PRIVATE_sNGNG,                                      // requied private key
                100,                                                                  // optional limit, default = 100
                ETransactionStatusFilter.Confirmed,                                   // optional status, default = Confirmed
                10                                                                    // optional offset, default = 0
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(ECurrency.sNGNG, result.Data[0].Currency);           
            Assert.Contains(SharedMethods.SRC_ADR_sNGNG.ToLower(), new[] { result.Data[0].Sources[0].ToLower(), result.Data[0].Targets[0].ToLower() },      // SRC_ADR_sNGNG can be either source or target
                $"Expected {SharedMethods.SRC_ADR_sNGNG} but found source {result.Data[0].Sources[0]} and target {result.Data[0].Targets[0]}");
        }


        [Test]
        [Description("TestCaseId: C1667")]
        public async Task GetTransactions_InvalidAddress_Neg()
        {
            // Arrange
            GluwaClient gluwaClient = new GluwaClient(SharedMethods.BASE_ENV);

            // Call
            var result = await gluwaClient.GetTransactionListAsync(
                ECurrency.sUSDCG,
                SharedMethods.INVALID_ADDRESS,                                        // invalid source address
                SharedMethods.SRC_PRIVATE_sUSDCG,
                10,
                ETransactionStatusFilter.Confirmed,
                0
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
        [Description("TestCaseId: C1668")]
        public async Task GetTransactions_InvalidKey_Neg()
        {
            // Arrange
            GluwaClient gluwaClient = new GluwaClient(SharedMethods.BASE_ENV);

            // Call
            var result = await gluwaClient.GetTransactionListAsync(
                ECurrency.sUSDCG,
                SharedMethods.SRC_ADR_sUSDCG,
                SharedMethods.INVALID_PRIVATE_KEY,                                 // invalid private key
                10,
                ETransactionStatusFilter.Confirmed,
                0
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
        [Description("TestCaseId: C1106")]
        public async Task GetTxDetails_USDG_Pos()
        {
            // Arrange
            GluwaClient gluwaClient = new GluwaClient(SharedMethods.BASE_ENV);

            // Call
            var result = await gluwaClient.GetTransactionDetailsAsync(
            (ECurrency)Currency.USDG,                                        // requied currency:USDG
            SharedMethods.SRC_PRIVATE_USDG,                                  // requied private key
            SharedMethods.TxID_USDG                                          // requied transaction Id
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(SharedMethods.TxID_USDG, result.Data.TxnHash);
            Assert.AreEqual(SharedMethods.SRC_ADR_USDG, result.Data.Sources[0]);
            Assert.AreEqual((ECurrency)Currency.USDG, result.Data.Currency);
        }


        [Test]
        [Description("TestCaseId: C1106")]
        public async Task GetTxDetails_sUSDCG_Pos()
        {
            // Arrange
            GluwaClient gluwaClient = new GluwaClient(SharedMethods.BASE_ENV);

            // Call
            var result = await gluwaClient.GetTransactionDetailsAsync(
            ECurrency.sUSDCG,                                                 // requied currency:sUSDCG
            SharedMethods.SRC_PRIVATE_sUSDCG,                                 // requied private key
            SharedMethods.TxID_sUSDCG                                         // requied transaction Id
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(SharedMethods.TxID_sUSDCG, result.Data.TxnHash);
            Assert.AreEqual(SharedMethods.SRC_ADR_sUSDCG.ToLower(), result.Data.Sources[0]);
            Assert.AreEqual(ECurrency.sUSDCG, result.Data.Currency);
        }


        [Test]
        [Description("TestCaseId: C1106")]
        public async Task GetTxDetails_sNGNG_Pos()
        {
            // Arrange
            GluwaClient gluwaClient = new GluwaClient(SharedMethods.BASE_ENV);

            // Call
            var result = await gluwaClient.GetTransactionDetailsAsync(
            ECurrency.sNGNG,                                               // requied currency:sNGNG
            SharedMethods.SRC_PRIVATE_sNGNG,                               // requied private key
            SharedMethods.TxID_sNGNG                                       // requied transaction Id
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.AreEqual(SharedMethods.TxID_sNGNG, result.Data.TxnHash);
            Assert.AreEqual(SharedMethods.SRC_ADR_sNGNG.ToLower(), result.Data.Sources[0]);
            Assert.AreEqual(ECurrency.sNGNG, result.Data.Currency);
        }


        [Test]
        [Description("TestCaseId: C1669")]
        public async Task GetTxDetails_InvalidKey_Neg()
        {
            // Arrange
            GluwaClient gluwaClient = new GluwaClient(SharedMethods.BASE_ENV);

            // Call
            var result = await gluwaClient.GetTransactionDetailsAsync(
            ECurrency.sUSDCG,
            SharedMethods.INVALID_PRIVATE_KEY,                         // invalid private key
            SharedMethods.TxID_sUSDCG
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
        [Description("TestCaseId: C1670")]
        public async Task GetTxDetails_InvalidTxId_Neg()
        {
            // Arrange
            GluwaClient gluwaClient = new GluwaClient(SharedMethods.BASE_ENV);

            // Call
            var result = await gluwaClient.GetTransactionDetailsAsync(
            ECurrency.sUSDCG,
            SharedMethods.SRC_PRIVATE_sUSDCG,
            SharedMethods.invalidTxID                            // invalid TxId
            );

            // Trace result
            TestContext.WriteLine($"Testing against: {useEnvironment}");
            TestContext.WriteLine(JsonConvert.SerializeObject(result));

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("NotFound", result.Error.Code);
            Assert.AreEqual("Transaction not found.", result.Error.Message);
        }
    }
}
