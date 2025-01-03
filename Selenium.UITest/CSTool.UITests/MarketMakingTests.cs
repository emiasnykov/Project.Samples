using CSTool.UITests.Enum;
using CSTool.UITests.Pages;
using CSTool.UITests.Shared;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace CSTool.UITests
{
    [TestFixture]
    public class MarketMakingTests
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
        public void MarketMaking_VerifyMarketMaking_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MarketMakingActiveStatus");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);       

                //Assert
                Assert.IsTrue(MarketMakingPage.MarketMakingStatusHeader(driver).Displayed);
                Assert.IsTrue(MarketMakingPage.MarketMakingStatusHeader(driver).Text.Contains("All Active Status is"));
                if (MarketMakingPage.MarketMakingStatusHeader(driver).Text.Contains("Running"))
                {
                    Assert.IsTrue(MarketMakingPage.MarketMakingStatusBtn(driver).Text == "Shutdown");
                }
                else
                {
                    Assert.IsTrue(MarketMakingPage.MarketMakingStatusBtn(driver).Text == "Restart");
                }
            }
        }


        [Test, Retry(2)]
        public void MarketMaking_VerifyCharmbit_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MarketMakingActiveStatus");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);       

                //assert
                Assert.IsTrue(MarketMakingPage.CharmbitStatusHeader(driver).Displayed);
                Assert.IsTrue(MarketMakingPage.CharmbitStatusHeader(driver).Text.Contains(" "));
                if (MarketMakingPage.CharmbitStatusHeader(driver).Text.Contains("Running"))
                {
                    Assert.IsTrue(MarketMakingPage.CharmbitStatusBtn(driver).Text == "Shutdown");
                }
                else
                {
                    Assert.IsTrue(MarketMakingPage.CharmbitStatusBtn(driver).Text == "Restart");
                }
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
