using Dashboard.UITests.Enum;
using Dashboard.UITests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using Shared.Driver;
using System;

namespace Dashboard.UITests
{
    [TestFixture("Test")]
    [TestFixture("Stage")]
    internal class LoginTests
    {
        public IWebDriver driver;
        public string url;
        public readonly string useEnvironment;
        public string browser;

        /// <summary>
        /// From TestFixture
        /// </summary>
        /// /// <param name="useEnvironment"></param>
        public LoginTests(string useEnvironment)
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
                browser = "Chrome";     // Default to Chrome
            }
        }

        [Test]
        public void LoginAs_ValidUser_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Act
                // Ensure the main elements are shown 
                IWebElement Home = Shared.FindElement(driver, By.CssSelector("div.main-caption"), 30);
                IWebElement Menu = Shared.FindElement(driver, By.ClassName("sidebar"), 30);

                // Assert            
                Assert.IsTrue(Home.Displayed);
                Assert.IsTrue(Menu.Displayed);

                // Logout
                LoginPage.Logout(driver);
            }
        }


        [Test]
        public void Login_InvalidEmail_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.InvalidEmail);

                // Act
                // Ensure err message is shown
                IWebElement loginError = Shared.FindElement(driver, By.CssSelector(".validation-summary-errors > ul > li"), 30);

                // Assert
                Assert.IsTrue(loginError.Displayed);
                Assert.IsTrue(loginError.Text == "Credentials you entered don't match our records or your email requires confirmation.");
            }
        }


        [Test]
        public void Login_UnknownUser_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.UnknownUser);

                // Act
                // Ensure err message is shown
                IWebElement loginError = Shared.FindElement(driver, By.CssSelector(".validation-summary-errors > ul > li"), 30);

                // Assert
                Assert.IsTrue(loginError.Displayed);
                Assert.IsTrue(loginError.Text == "Credentials you entered don't match our records.");
            }
        }


        [Test]
        public void SignUp_WithValidCredentials_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);

                // Act
                LoginPage.SignUpBtn(driver);
                LoginPage.SignUpWith(driver, SignUpEnum.ValidCredentials);
                IWebElement ConfirmEmail = Shared.FindElement(driver, By.XPath("//h4[contains(text(),'Confirm Email')]"), 30);

                // Assert
                Assert.IsTrue(ConfirmEmail.Displayed);
            }
        }


        [Test]
        public void SignUp_InvalidUserName_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);

                // Act
                LoginPage.SignUpBtn(driver);
                LoginPage.SignUpWith(driver, SignUpEnum.InvalidUserName);
                IWebElement signUpError = Shared.FindElement(driver, By.XPath("(.//*[@class='field']//span)[1]"), 30);
                
                // Assert
                Assert.IsTrue(signUpError.Displayed);
                Assert.IsTrue(signUpError.Text == "Invalid Username");
            }
        }


        [Test]
        public void SignUp_InvalidEmail_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);

                // Act
                LoginPage.SignUpBtn(driver);
                LoginPage.SignUpWith(driver, SignUpEnum.InvalidEmail);
                IWebElement signUpError = Shared.FindElement(driver, By.Id("Email-error"), 30);

                // Assert
                Assert.IsTrue(signUpError.Displayed);
                Assert.IsTrue(signUpError.Text == "The Email field is not a valid e-mail address.");
            }
        }


        [Test]
        public void SignUp_NoPassword_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);

                // Act
                LoginPage.SignUpBtn(driver);
                LoginPage.SignUpWith(driver, SignUpEnum.NoPassword);
                IWebElement signUpError = Shared.FindElement(driver, By.Id("Password-error"), 30);

                // Assert
                Assert.IsTrue(signUpError.Displayed);
                Assert.IsTrue(signUpError.Text == "Password is required.");
            }
        }


        [Test]
        public void SignUp_PasswordTooShort_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);

                // Act
                LoginPage.SignUpBtn(driver);
                LoginPage.SignUpWith(driver, SignUpEnum.PasswordTooShort);
                IWebElement signUpError = Shared.FindElement(driver, By.CssSelector(":nth-child(3) > .text-danger"), 30);

                // Assert
                Assert.IsTrue(signUpError.Displayed);
                Assert.IsTrue(signUpError.Text == "Password too short");
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

            // SetUp
            public TestScope(string browser, string useEnvironment)
            {
                Driver initialize = new Driver();
                Instance = initialize.StartBrowser(browser);
                Shared setup = new Shared();
                Env = setup.SetEnvironmentVariables(useEnvironment);
            }

            // TearDown
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

