using AdminTool.UITests.Pages;
using AdminTool.UITests.SharedMethods;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace AdminTool.UITests
{
    [TestFixture]
    public class ClientTests
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
        public void Clients_CreateNew_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoClientsPage(driver);
            ClientPage.RemoveExistingTestClient(driver);
            ClientPage.CreateNewButton(driver);
            ClientPage.CreateNewClientForm(driver);
            ClientPage.SubmitButton(driver);

            //Assert
            Assertions.VerifyNewClientIsCreated(driver);
        }


        [Test]
        public void Clients_CreateNew_Neg()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoClientsPage(driver);
            ClientPage.ClientFormNeg(driver);

            //Assert
            Assertions.VerifyRequiredFieldErrMsg(driver);
        }


        [Test]
        public void Clients_GetClientDetails_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoClientsPage(driver);
            string details = ClientPage.GetClientDetails(driver);

            //Assert
            Assertions.VerifyClientDetailsIsAt(driver, details);
        }


        [Test]
        public void Clients_CreateNew_ZEdit()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoClientsPage(driver);
            ClientPage.RemoveExistingTestClient(driver);
            ClientPage.CreateNewButton(driver);
            ClientPage.CreateNewClientForm(driver);
            ClientPage.SubmitButton(driver);
            ClientPage.EditClientBtn(driver);
            ClientPage.EditClientForm(driver);

            //Assert
            Assertions.VerifyClientEdited(driver);
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
