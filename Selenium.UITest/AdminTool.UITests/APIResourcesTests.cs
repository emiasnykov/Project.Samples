using AdminTool.UITests.Pages;
using AdminTool.UITests.SharedMethods;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace AdminTool.UITests
{
    [TestFixture]
    public class APIResourcesTests
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
        public void APIResources_CreateNew_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoApiResourcesPage(driver);
            ApiResourcesPage.RemoveExistResources(driver, Shared.Name);
            ApiResourcesPage.CreateNewBtn(driver).Click();
            ApiResourcesPage.CreateNewApiResource(driver, Shared.Name, ApiResourceEnum.ValidResource); //Valid values
            ApiResourcesPage.SaveBtn(driver).Click();

            //Assert
            Assertions.VerifyNewResourceCreated(driver);         //Assert point one
            ApiResourcesPage.BackToListBtn(driver).Click();
            Assertions.VerifyNewResourceInList(driver);          //Assert point two
        }


        [Test]
        public void APIResources_EmptyName_Neg()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoApiResourcesPage(driver);
            ApiResourcesPage.RemoveExistResources(driver, Shared.Name);
            ApiResourcesPage.CreateNewBtn(driver).Click();
            ApiResourcesPage.CreateNewApiResource(driver, Shared.Name, ApiResourceEnum.NoName); //Without required value 'Name' 
            ApiResourcesPage.SaveBtn(driver).Click();

            //Assert
            Assertions.VerifyEmptyNameTextField(driver);
        }


        [Test]
        public void APIResources_EmptyDisplayName_Neg()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoApiResourcesPage(driver);
            ApiResourcesPage.RemoveExistResources(driver, Shared.Name);
            ApiResourcesPage.CreateNewBtn(driver).Click();
            ApiResourcesPage.CreateNewApiResource(driver, Shared.Name, ApiResourceEnum.NoDisplayName); //Without required value 'DisplayName' 
            ApiResourcesPage.SaveBtn(driver).Click();

            //Assert
            Assertions.VerifyEmptyDisplayNameTextField(driver);
        }


        [Test]
        public void APIResources_GetScopeDetails_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoApiResourcesPage(driver);
            ApiResourcesPage.RemoveExistResources(driver, Shared.Name);
            ApiResourcesPage.CreateNewBtn(driver).Click();
            ApiResourcesPage.CreateNewApiResource(driver, Shared.Name, ApiResourceEnum.ValidResource); //Valid values
            ApiResourcesPage.SaveBtn(driver).Click();
            ApiResourcesPage.GetScopeDetails(driver);

            //Assert
            Assertions.VerifyScopeDetailesDisplayed(driver, Shared.Name);
        }


        [Test]
        public void APIResources_DeleteScope_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoApiResourcesPage(driver);
            ApiResourcesPage.RemoveExistResources(driver, Shared.Name);
            ApiResourcesPage.CreateNewBtn(driver).Click();
            ApiResourcesPage.CreateNewApiResource(driver, Shared.Name, ApiResourceEnum.ValidResource); //Valid values
            ApiResourcesPage.SaveBtn(driver).Click();
            ApiResourcesPage.DeleteScope(driver);

            //Assert
            Assertions.VerifyResourceIsDeleted(driver, Shared.Name);
        }


        [Test]
        public void APIResources_EditScope_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoApiResourcesPage(driver);
            ApiResourcesPage.RemoveExistResources(driver, Shared.Name);
            ApiResourcesPage.CreateNewBtn(driver).Click();
            ApiResourcesPage.CreateNewApiResource(driver, Shared.Name, ApiResourceEnum.ValidResource); //Valid values
            ApiResourcesPage.SaveBtn(driver).Click();
            ApiResourcesPage.EditScope(driver);

            //Assert
            Assertions.VerifyResourceIsEdited(driver, Shared.Name);
        }


        [Test]
        public void APIResources_DuplicatedScope_Neg()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoApiResourcesPage(driver);
            ApiResourcesPage.RemoveExistResources(driver, Shared.Name);
            ApiResourcesPage.CreateNewBtn(driver).Click();
            ApiResourcesPage.CreateNewApiResource(driver, Shared.Name, ApiResourceEnum.ValidResource); //Valid values
            ApiResourcesPage.SaveBtn(driver).Click();
            ApiResourcesPage.BackToListBtn(driver).Click();
            ApiResourcesPage.CreateNewBtn(driver).Click();
            ApiResourcesPage.CreateNewApiResource(driver, Shared.Name, ApiResourceEnum.ValidResource); //Duplicated name
            ApiResourcesPage.SaveBtn(driver).Click();

            //Assert
            Assertions.VerifyDuplicatedNames(driver);
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
