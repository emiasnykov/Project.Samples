using GluwaAPI.TestEngine.Models;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;

namespace GluwaAPI.TestEngine.Utils
{
    [TestFixture("Test")]
    //[TestFixture("EphEnv")]
    [TestFixture("Staging")]
    [TestFixture("Production")]
    [SetUpFixture]
    public class TestsSetupClass
    {
        public TestsSetupClass(string environment)
        {
            this.environment = environment;
        }

        public AddressItem investor { get; set; }
        public AddressItem receiverAddress { get; set; }
        public string environment;
        public string keyName;
        //public ECurrency currency;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            investor = QAKeyVault.GetGluwaAddress("Sender");
            receiverAddress = QAKeyVault.GetGluwaAddress("SsgdgMinter");
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            keyName = GluwaTestApi.SetUpGluwaKeyName(environment);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {               
        }
    }
}
