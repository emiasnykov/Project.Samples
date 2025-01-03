using CSTool.UITests.Enum;
using CSTool.UITests.Pages;
using CSTool.UITests.Shared;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace CSTool.UITests
{
    [TestFixture]
    public class IPEncryptTests
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
        public void IPEncrypt_Valid_Address_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "IPEncrypt");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);          //Login as valid user
                IPEncryptPage.EnterIPAddress(driver)
                             .SendKeys(SharedMethods.ValidIpAddress);        //Valid Ip address
                IPEncryptPage.EncryptBtn(driver).Click();

                //Assert
                Assert.AreEqual(SharedMethods.ValidEncryptedIP, SharedMethods.IP(driver));
            }
        }


        [Test, Retry(2)]
        public void IPEncrypt_InvalidIP_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "IPEncrypt");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);        //Login as valid user
                IPEncryptPage.EnterIPAddress(driver)
                             .SendKeys(SharedMethods.InvalidIpAddress);    //Too short Ip address
                IPEncryptPage.EncryptBtn(driver).Click();

                //Assert
                Assert.AreEqual(SharedMethods.InvalidIpAddress_ErrMsg, SharedMethods.Alert(driver));
            }
        }


        [Test, Retry(2)]
        public void IPEncrypt_EmptyIP_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "IPEncrypt");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);       //Login as valid user
                IPEncryptPage.EnterIPAddress(driver).SendKeys("");        //Empty Ip address
                IPEncryptPage.EncryptBtn(driver).Click();

                //Assert
                Assert.AreEqual(SharedMethods.EmptyIpAddress_ErrMsg, SharedMethods.Alert(driver));
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
