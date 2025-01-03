using CSTool.UITests.Enum;
using CSTool.UITests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace CSTool.UITests
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
        public void Login_AsValidUser_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user

                //Assert
                Assert.IsTrue((SharedMethods.FindElement(driver, By.XPath("//h2[contains(text(),'Tools')]"), 10).Displayed));    
            }
        }


        [Test, Retry(2)]
        public void Login_AsUnknownUser_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.UnknownUser);   //Login as unknown user

                //Assert
                Assert.AreEqual("Invalid login attempt.", LoginPage.ErrMsg(driver).Text);
            }
        }


        [Test, Retry(2)]
        public void Login_NoEmail_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.NoEmail);   //Empty email 

                //Assert
                Assert.AreEqual("The Email field is required.", LoginPage.ErrMsg(driver).Text);
            }
        }


        [Test, Retry(2)]
        public void Login_NoPassword_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.LoginAs(driver, LoginUserEnum.NoPassword);   //Empty password 

                //Assert
                Assert.AreEqual("The Password field is required.", LoginPage.ErrMsg(driver).Text);
            }
        }


        [Test, Retry(2)]
        public void Login_ForgotPassword_ValidEmail_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.ForgotPasswordBtn(driver).Click();
                LoginPage.EnterYourEmail(driver).SendKeys("qa.automation@gluwa.com"); //Email valid and exists
                LoginPage.SubmitBtn(driver).Click();

                //Assert
                Assert.AreEqual("Reset password link has been sent to qa.automation@gluwa.com.", LoginPage.ConfirmtMsg(driver).Text);
            }
        }


        [Test, Retry(2)]
        public void Login_ForgotPassword_EmailNotExists_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.ForgotPasswordBtn(driver).Click();
                LoginPage.EnterYourEmail(driver).SendKeys("selenium@gluwa.com"); //Email valid not exists
                LoginPage.SubmitBtn(driver).Click();

                //Assert
                Assert.AreEqual("This email does not exist in our system.", LoginPage.AlertMsg(driver).Text);
            }
        }


        [Test, Retry(2)]
        public void Login_ForgotPassword_InvalidEmail_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver);
                LoginPage.ForgotPasswordBtn(driver).Click();
                LoginPage.EnterYourEmail(driver).SendKeys("selenium@");  //Invalid Email
                LoginPage.SubmitBtn(driver).Click();

                //Assert
                Assert.AreEqual("The Email field is not a valid e-mail address.", LoginPage.AlertMsg(driver).Text);
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
