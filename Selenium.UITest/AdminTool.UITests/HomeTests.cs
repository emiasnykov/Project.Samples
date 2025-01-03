using AdminTool.UITests.Pages;
using AdminTool.UITests.SharedMethods;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace AdminTool.UITests
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
            browser = browser == "Firefox" ? "Firefox" : "Chrome";
        }

        [Test]
        public void Home_TestWhiteList()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            try
            {
                //Arrange
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user
            }
            catch
            {
                //Assertion
                Assertions.TextForbidden(driver);
            }
        }


        [Test]
        public void Home_GluwaAdminTool()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Act
            Navigation.AdminTool(driver);

            //Assertion
            Assertions.ValidateAdminToolText(driver);
        }


        [Test]
        public void Home_ClientsLink_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Act
            Navigation.ClientText(driver);

            //Assertions
            Assertions.ClientTextValid(driver);
        }


        [Test]
        public void Home_APIResourcesLink_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Act
            Navigation.ApiResourcesText(driver);

            //Assertion
            Assertions.ApiText(driver);
        }


        [Test]
        public void Home_IdentityResourcesLink_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Act
            Navigation.GotoIdentityResourcesText(driver);

            //Assertion
            Assertions.IdentityResourcesText(driver);
        }


        [Test]
        public void Home_LogoutLink_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Act
            Navigation.Logout(driver);

            //Assertiion
            Assertions.Logout(driver);
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
