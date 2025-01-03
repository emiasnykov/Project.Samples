using CSTool.UITests.Enum;
using CSTool.UITests.Pages;
using CSTool.UITests.Shared;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace CSTool.UITests
{
    [TestFixture]
    internal class IPDecryptTests
    {
        public IWebDriver driver;
        public string browser;

        [SetUp]
        public void Setup()
        {
            browser = TestContext.Parameters["browser"];
            if (browser == "Firefox")
            {
                browser = "Firefox";
            }
            else // Default to Chrome
            {
                browser = "Chrome";
            }
        }

        [Test, Retry(2)]
        public void IPDecrypt_ValidIp_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "IPDecrypt");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);         //Login as valid user
                IPDecryptPage.EnterEncryptedIP(driver)
                             .SendKeys(SharedMethods.ValidEncryptedIP);     //Valid Encrypted IP
                IPDecryptPage.DecryptBtn(driver).Click();

                //Assert
                Assert.AreEqual(SharedMethods.ValidIpAddress, SharedMethods.IP(driver));
            }
        }


        [Test, Retry(2)]
        public void IPDecrypt_InvalidIp_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "IPDecrypt");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);         //Login as valid user
                IPDecryptPage.EnterEncryptedIP(driver)
                             .SendKeys(SharedMethods.InvalidEncryptedIP);   //Invalid Encrypted IP
                IPDecryptPage.DecryptBtn(driver).Click();

                //Assert
                Assert.AreEqual(SharedMethods.InvalidEncryptedIP_ErrMsg, SharedMethods.ErrorMessage(driver));
            }
        }


        [Test, Retry(2)]
        public void IPDecrypt_TooShortIP_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "IPDecrypt");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);         //Login as valid user
                IPDecryptPage.EnterEncryptedIP(driver).SendKeys("MDA");     //Too short Encrypted IP
                IPDecryptPage.DecryptBtn(driver).Click();

                //Assert
                Assert.AreEqual(SharedMethods.TooShortEncryptedIP_ErrMsg, SharedMethods.ErrorMessage(driver));
            }
        }


        [Test, Retry(2)]
        public void IPDecrypt_EmptyIP_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "IPDecrypt");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);       //Login as valid user
                IPDecryptPage.EnterEncryptedIP(driver).SendKeys("");      //Empty Encrypted IP
                IPDecryptPage.DecryptBtn(driver).Click();

                //Assert
                Assert.AreEqual(SharedMethods.EmptyEncryptedIP_ErrMsg, SharedMethods.Alert(driver));
            }
        }

        private sealed class TestScope : IDisposable
        {
            public IWebDriver Instance { get; }

            //SetUp
            public TestScope(string browser)
            {
                Driver initialize = new Driver();
                Instance = initialize.StartBrowser(browser);
            }

            //TearDown
            public void Dispose()
            {
                if (Instance != null)
                {
                    Instance.Quit();
                }
            }

        }

        [TearDown]
        public void CleanUp()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }

    }
}
