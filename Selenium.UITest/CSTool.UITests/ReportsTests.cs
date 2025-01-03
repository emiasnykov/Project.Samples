using CSTool.UITests.Enum;
using CSTool.UITests.Pages;
using CSTool.UITests.Shared;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace CSTool.UITests
{
    [TestFixture]
    internal class ReportsTests
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
        public void AgreementDocuments_Download()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);         //Login as valid user
                ReportsPage.Reports(driver).Click();     //Reports
                ReportsPage.KYC(driver).Click();     //KYC page
                ReportsPage.Download(driver).Click();

                //Assert
                Assertions.VerifyreportDownloaded(driver);
            }
        }

        [Test, Retry(2)]
        public void Reports_Download()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);         //Login as valid user
                ReportsPage.Reports(driver).Click();     //Reports
                ReportsPage.KYC(driver).Click();     //KYC page
                ReportsPage.Report(driver).Click();

                //Assert
                Assertions.VerifyreportDownloaded(driver);
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
