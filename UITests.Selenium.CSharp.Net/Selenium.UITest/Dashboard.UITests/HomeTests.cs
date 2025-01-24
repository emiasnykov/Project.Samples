using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;
using Shared.Driver;
using Dashboard.UITests.Pages;
using NUnit.Framework;
using Dashboard.UITests.Enum;
using OpenQA.Selenium;
using System;

namespace Dashboard.UITests
{
    [TestFixture("Test")]
    [TestFixture("Stage")]
    internal class HomeTests
    {
        public IWebDriver driver;
        public string url;
        public readonly string useEnvironment;
        public string browser;

        /// <summary>
        /// From TestFixture
        /// </summary>
        /// /// <param name="useEnvironment"></param>
        public HomeTests(string useEnvironment)
        {
            this.useEnvironment = useEnvironment;
        }

        [SetUp]
        public void Setup()
        {
            browser = TestContext.Parameters["browser"];
            if (browser == "Firefox")
            {
                browser = "Firefox";
            }
            else 
            {
                browser = "Chrome";   // Default to Chrome
            }
        }

        [Test]
        [Description("TestCaseId:T1164")]
        public void Home_ChangeEmail_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);                                     
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);                  

                // Steps
                HomePage.UserDropDownMenu(driver).Click();                         
                HomePage.ChangeEmail(driver).Click();                             
                HomePage.NewEmailAddress(driver).SendKeys("qa+selenium@gluwa.com");    // Add new valid email
                HomePage.SubmitBtn(driver).Click();                                 

                // Assert
                Assertions.ConfirmNewEmail(driver);                                    // Assert confirmation message is displayed
            }
        }


        [Test]
        [Description("TestCaseId:T1165")]
        public void Home_ChangePassword_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);                                   
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);               

                // Steps
                HomePage.UserDropDownMenu(driver).Click();                        
                HomePage.ChangePassword(driver).Click();

                // Assertions
                Assertions.VerifyChangePasswordDisplayed(driver);                      // Confirm 'Change Password' page is opened
            }
        }


        [Test]
        [Description("TestCaseId:T1167")]
        public void Home_ViewSecret_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);                                   
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);  

                // Steps
                HomePage.ViewSecretKey(driver).Click();                  // Show Secret Key

                // Assertions
                Assertions.VerifyHiddenSecretDisplayed(driver);          // Confirm Secret Key is not hidden
            }
        }


        [Test]
        [Description("TestCaseId:T1168")]
        public void Home_ResetSecretkey_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                HomePage.ViewSecretKey(driver).Click();
                string oldKey = HomePage.GetSecretKey(driver);         // Get old secret key
                HomePage.ResetSecretKey(driver).Click();               // Reset key
                HomePage.ConfirmResetSecretKey(driver).Click();
                HomePage.ViewSecretKey(driver).Click();
                string newKey = HomePage.GetSecretKey(driver);         // Get new secret key

                // Assert
                Assert.AreNotEqual(oldKey, newKey);                    // Confirm reset of secret key
            }
        }


        [Test]
        [Description("TestCaseId:T1162")]
        public void Home_SwitchLanguage_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);                                   
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);            

                // Steps
                HomePage.UserDropDownMenu(driver).Click();
                HomePage.SwitchLanguage(driver).Click();
                HomePage.SelectKoreanLanguage(driver).Click();          // Switch to Korean language

                // Assert point one
                Assertions.ConfirmKoreanLanguageSelected(driver);       // Confirm Korean

                // Steps
                HomePage.UserDropDownMenu(driver).Click();
                HomePage.SwitchLanguage(driver).Click();
                HomePage.SelectEnglishLanguage(driver).Click();         // Switch back to English language

                // Assert point two
                Assertions.ConfirmEnglishLanguageSelected(driver);      // Confirm English
            }
        }


        [Test]
        [Description("TestCaseId:T1172")]
        public void Home_AddValidIpAddress()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);                                   
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);               

                // Steps
                HomePage.ClearWhitelistedIPaddresses(driver);                    
                HomePage.AddWhiteListIpAddressBtn(driver).Click();
                HomePage.AddWhiteListIpAddressValue(driver).Clear();
                HomePage.AddWhiteListIpAddressValue(driver).SendKeys("100.10.10.1");    // Add valid whitelisted IP address
                HomePage.SubmitNewWhitelistIPAddress(driver).Click();             

                // Assert point one
                Assertions.WhitelistedIpDisplayed(driver);                              // Confirm new Ip is displayed

                // CleanUP
                HomePage.ClearWhitelistedIPaddresses(driver);
            }
        }


        [Test]
        [Description("TestCaseId:T1175")]
        public void Home_AddInvalidIpAddress_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);                                   
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);               

                // Steps
                HomePage.ClearWhitelistedIPaddresses(driver);                     
                HomePage.AddWhiteListIpAddressBtn(driver).Click();
                HomePage.AddWhiteListIpAddressValue(driver).Clear();
                HomePage.AddWhiteListIpAddressValue(driver).SendKeys("100.10.1");   // Add invalid whitelisted IP address

                // Assertions
                Assertions.VerifyInvalidWhitelistedIpAddress(driver);               // Error msg is displayed
            }
        }


        /// <summary>
        /// Initialization
        /// Set environment
        /// TearDown
        /// </summary>
        private sealed class TestScope : IDisposable
        {
            public IWebDriver Instance { get; }
            public string Env { get; }

            //SetUp
            public TestScope(string browser, string useEnvironment)
            {
                Driver initialize = new Driver();
                Instance = initialize.StartBrowser(browser);
                Shared setup = new Shared();
                Env = setup.SetEnvironmentVariables(useEnvironment);
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
    }
}
