using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using NUnit.Framework;
using System;
using System.Threading;

namespace Exchange.Tests
{
    [TestFixture("Test"), TestFixture("Sandbox")]
    public class TestOrdersCleanup
    {

        private string environment;

        public TestOrdersCleanup(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void WaitForExchange()
        {
            //Wait for the exchanges to get processed
            Thread.Sleep(new TimeSpan(0, 1, 0));
        }

        [Test]
        public void CleanUpActiveTests()
        {
            //Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            ExchangeFunctions.TryToCancelActiveOrders();
        }
    }
}

