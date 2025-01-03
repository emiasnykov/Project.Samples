using AdminTool.UITests.Pages;
using AdminTool.UITests.SharedMethods;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace AdminTool.UITests
{
    [TestFixture]
    internal class TestUserTests
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
        public void TestUser_AddValidUser_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoTestUserPage(driver);
            TestUserPage.RemoveTestUser(driver, Shared.User);
            TestUserPage.AddNewTestUserBtn(driver);
            TestUserPage.AddNewTestUserTextField(driver, Shared.User);
            TestUserPage.SearchBtn(driver);
            TestUserPage.ClickConfirmBtn(driver);

            //Assert
            Assertions.VerifyNewUserIsDisplayedInGrid(driver, Shared.User);
        }


        [Test]
        public void TestUser_RemoveUser_Pos()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

            //Steps
            Navigation.GotoTestUserPage(driver);
            TestUserPage.RemoveTestUser(driver, Shared.User);
            TestUserPage.AddNewTestUserBtn(driver);
            TestUserPage.AddNewTestUserTextField(driver, Shared.User);
            TestUserPage.SearchBtn(driver);
            TestUserPage.ClickConfirmBtn(driver);

            //Assert
            Assertions.VerifyNewUserIsDisplayedInGrid(driver, Shared.User); //Verify new user created
            TestUserPage.RemoveTestUser(driver, Shared.User);               //Remove new user
            Assertions.VerifyNewUserRemoved(driver, Shared.User);           //Verify new user removed
        }


        [Test]
        public void TestUser_InvalidUser_Neg()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoTestUserPage(driver);
            TestUserPage.RemoveTestUser(driver, Shared.User);
            TestUserPage.AddNewTestUserBtn(driver);
            TestUserPage.AddNewTestUserTextField(driver, Shared.InvalidUser); //Invalid user
            TestUserPage.SearchBtn(driver);

            //Assert
            Assertions.VerifyInvalidUserErrMsg(driver);
        }


        [Test]
        public void TestUser_ExistUser_Neg()
        {
            using var init = new TestScope(browser);
            //Setup
            driver = init.Instance;

            //Arrange
            LoginPage.GoToUrl(driver);
            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

            //Steps
            Navigation.GotoTestUserPage(driver);
            TestUserPage.RemoveTestUser(driver, Shared.User);
            TestUserPage.AddNewTestUserBtn(driver);
            TestUserPage.AddNewTestUserTextField(driver, Shared.User); //Add valid user
            TestUserPage.SearchBtn(driver);
            TestUserPage.ClickConfirmBtn(driver);
            TestUserPage.AddNewTestUserBtn(driver);
            TestUserPage.AddNewTestUserTextField(driver, Shared.User); //Atempt to add the same user
            TestUserPage.SearchBtn(driver);

            //Assert
            Assertions.VerifyExistUserErrMsg(driver);
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
