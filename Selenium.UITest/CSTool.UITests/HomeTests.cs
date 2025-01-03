using CSTool.UITests.Enum;
using CSTool.UITests.Pages;
using CSTool.UITests.Shared;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace CSTool.UITests
{
    [TestFixture]
    public class HomeTests
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
        public void Home_ClickTools_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   
                HomePage.Header(driver).Click();

                //Assert
                Assertions.ConfirmPageIsOpened(driver, "Tools");
            }
        }


        [Test, Retry(2)]
        public void Home_ClickGluwaUserInfo_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   
                Navigation.GotoUserInfoPage(driver);

                //Assert
                Assertions.ConfirmPageIsOpened(driver, "GluwaUserSearch");
            }
        }


        [Test, Retry(2)]
        public void Home_Logout_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   
                HomePage.LogOut(driver).Click();

                //Assert
                Assertions.ConfirmLogOut(driver);
            }
        }


        [Test, Retry(2)]
        public void Home_ClickMarketMaking_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   
                HomePage.OperationDropdown(driver).Click();
                Navigation.GotoMarketMakingPage(driver);

                //Assert
                Assertions.ConfirmPageIsOpened(driver, "MarketMaking Active Status");
            }
        }


        [Test, Retry(2)]
        public void Home_ClickMessagingRecords_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   
                HomePage.OperationDropdown(driver).Click();
                Navigation.GotoMessagingRecordsPage(driver);

                //Assert
                Assertions.ConfirmPageIsOpened(driver, "Messaging Record List");
            }
        }


        [Test, Retry(2)]
        public void Home_ClickIPEncryption()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);
                HomePage.ExternalToolsDropdown(driver).Click();
                Navigation.GotoIPEncryptionPage(driver);

                //Assert
                Assertions.ConfirmPageIsOpened(driver, "IP Encrypt");
            }
        }


        [Test, Retry(2)]
        public void Home_ClickIPDecryption()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   
                HomePage.ExternalToolsDropdown(driver).Click();
                Navigation.GotoIPDecryptionPage(driver);

                //Assert
                Assertions.ConfirmPageIsOpened(driver, "IP Decrypt");
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
