using AdminTool.UITests.Pages;
using AdminTool.UITests.SharedMethods;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace AdminTool.UITests
{
    [TestFixture]
    public class IdentityResourcesTests
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
        public void IdentityResources_CreateNew_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);           //Login as valid user

            //Steps
            Navigation.GotoIdentityResourcesPage(driver);
            IdentityResourcesPage.RemoveExistResources(driver, Shared.IdentityName);
            IdentityResourcesPage.CreateNewBtn(driver).Click();
            IdentityResourcesPage.CreateNewIdentityResource(driver, Shared.IdentityName, IdentityResourceEnum.ValidResource); //Valid identity resource
            IdentityResourcesPage.SaveBtn(driver).Click();

            //Assert
            Assertions.VerifyNewIdentityResourceCreated(driver);          //Assert point one
            IdentityResourcesPage.BackToIdentityListBtn(driver).Click();  //Back to list
            Assertions.VerifyNewIdentityResourceInList(driver);           //Assert point two
        }


        [Test]
        public void IdentityResources_EmptyName_Neg()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);      //Login as valid user

            //Steps
            Navigation.GotoIdentityResourcesPage(driver);
            IdentityResourcesPage.RemoveExistResources(driver, Shared.IdentityName);
            IdentityResourcesPage.CreateNewBtn(driver).Click();
            IdentityResourcesPage.CreateNewIdentityResource(driver, "", IdentityResourceEnum.NoName); //Without required Name
            IdentityResourcesPage.SaveBtn(driver).Click();

            //Assert
            Assertions.VerifyEmptyNameTextField(driver);
        }


        [Test]
        public void IdentityResources_EmptyDisplayName_Neg()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);      //Login as valid user

            //Steps
            Navigation.GotoIdentityResourcesPage(driver);
            IdentityResourcesPage.RemoveExistResources(driver, Shared.IdentityName);
            IdentityResourcesPage.CreateNewBtn(driver).Click();
            IdentityResourcesPage.CreateNewIdentityResource(driver, "", IdentityResourceEnum.NoDisplayName); //Without required DisplayName
            IdentityResourcesPage.SaveBtn(driver).Click();

            //Assert
            Assertions.VerifyEmptyDisplayNameTextField(driver);
        }


        [Test]
        public void IdentityResources_GetDetails_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);      //Login as valid user

            //Steps
            Navigation.GotoIdentityResourcesPage(driver);
            IdentityResourcesPage.RemoveExistResources(driver, Shared.IdentityName);
            IdentityResourcesPage.CreateNewBtn(driver).Click();
            IdentityResourcesPage.CreateNewIdentityResource(driver, Shared.IdentityName, IdentityResourceEnum.ValidResource);
            IdentityResourcesPage.SaveBtn(driver).Click();
            IdentityResourcesPage.GetResourceDetails(driver, Shared.IdentityName);

            //Assert
            Assertions.VerifyDetailesDisplayed(driver, Shared.IdentityName);
        }


        [Test]
        public void IdentityResources_DeleteResource_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);      //Login as valid user

            //Steps
            Navigation.GotoIdentityResourcesPage(driver);
            IdentityResourcesPage.RemoveExistResources(driver, Shared.IdentityName);
            IdentityResourcesPage.CreateNewBtn(driver).Click();
            IdentityResourcesPage.CreateNewIdentityResource(driver, Shared.IdentityName, IdentityResourceEnum.ValidResource);
            IdentityResourcesPage.SaveBtn(driver).Click();
            IdentityResourcesPage.DeleteResource(driver);

            //Assert
            Assertions.VerifyResourceIsDeleted(driver, Shared.IdentityName);
        }


        [Test]
        public void IdentityResources_EditResource_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);      //Login as valid user

            //Steps
            Navigation.GotoIdentityResourcesPage(driver);
            IdentityResourcesPage.RemoveExistResources(driver, Shared.IdentityName);
            IdentityResourcesPage.CreateNewBtn(driver).Click();
            IdentityResourcesPage.CreateNewIdentityResource(driver, Shared.IdentityName, IdentityResourceEnum.ValidResource);
            IdentityResourcesPage.SaveBtn(driver).Click();
            IdentityResourcesPage.EditResource(driver);

            //Assert
            Assertions.VerifyResourceIsEdited(driver, Shared.IdentityName);
        }


        [Test]
        public void IdentityResources_DuplicatedResource_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);      //Login as valid user

            //Steps
            Navigation.GotoIdentityResourcesPage(driver);
            IdentityResourcesPage.RemoveExistResources(driver, Shared.IdentityName);
            IdentityResourcesPage.CreateNewBtn(driver).Click();
            IdentityResourcesPage.CreateNewIdentityResource(driver, Shared.IdentityName, IdentityResourceEnum.ValidResource);
            IdentityResourcesPage.SaveBtn(driver).Click();
            IdentityResourcesPage.BackToIdentityListBtn(driver).Click();
            IdentityResourcesPage.CreateNewBtn(driver).Click();
            IdentityResourcesPage.CreateNewIdentityResource(driver, Shared.IdentityName, IdentityResourceEnum.ValidResource);
            IdentityResourcesPage.SaveBtn(driver).Click();

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
