using AdminTool.UITests.Pages;
using AdminTool.UITests.SharedMethods;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace AdminTool.UITests
{
    [TestFixture]
    internal class LoginTests
    {
        public IWebDriver driver;
        public string browser;

        [SetUp]
        public void Setup()
        {
            browser = TestContext.Parameters["browser"];
            browser = browser == "Firefox" ? "Firefox" : "Chrome";
        }

        [Test]
        public void Login_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Assert
            Assert.IsTrue(driver.FindElement(By.TagName("h2")).Text == "Gluwa AdminTool");
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
