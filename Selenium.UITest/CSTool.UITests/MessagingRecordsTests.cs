using CSTool.UITests.Enum;
using CSTool.UITests.Pages;
using CSTool.UITests.Shared;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace CSTool.UITests
{
    [TestFixture]
    public class MessagingRecordsTests
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
        public void MessagingRecords_ResetPasswordRequest_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MessagingRecords");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);                 //Login as valid user
                MessagingRecordsPage.MessageFilter(driver, "ResetPasswordRequest"); //Select ResetPasswordRequest type
                MessagingRecordsPage.SubmitBtn(driver).Click();

                //Assert
                Assertions.VerifyMessageFilterByType(driver, "ResetPasswordRequest");
            }
        }


        [Test, Retry(2)]
        public void MessagingRecords_AccountVerificationRequest_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MessagingRecords");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);                        //Login as valid user
                MessagingRecordsPage.MessageFilter(driver, "AccountVerificationRequest");  //Select AccountVerificationRequest type
                MessagingRecordsPage.SubmitBtn(driver).Click();

                //Assert
                Assertions.VerifyMessageFilterByType(driver, "AccountVerificationRequest");
            }
        }


        [Test, Retry(2)]
        public void MessagingRecords_AccountVerificationNotification_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MessagingRecords");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);                             //Login as valid user
                MessagingRecordsPage.MessageFilter(driver, "AccountVerificationNotification");  //Select AccountVerificationNotification type
                MessagingRecordsPage.SubmitBtn(driver).Click();

                //Assert
                Assertions.VerifyMessageFilterByType(driver, "AccountVerificationNotification");
            }
        }


        [Test, Retry(2)]
        public void MessagingRecords_ResetPasswordNotification_Pos()
        {
            using (var init = new TestScope(browser)) 
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MessagingRecords");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);                       //Login as valid user
                MessagingRecordsPage.MessageFilter(driver, "ResetPasswordNotification");  //Select ResetPassword Notification type
                MessagingRecordsPage.SubmitBtn(driver).Click();

                //Assert
                Assertions.VerifyMessageFilterByType(driver, "ResetPasswordNotification");
            }
        }


        [Test, Retry(2)]
        public void MessagingRecords_Email_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MessagingRecords");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);  //Login as valid user
                MessagingRecordsPage.MediaFilter(driver, "Email");   //Select Email media type
                MessagingRecordsPage.SubmitBtn(driver).Click();

                //Assert
                Assertions.VerifyMediaFilterByType(driver, "Email");
            }
        }

        [Test, Retry(2)]
        public void MessagingRecords_Sms_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MessagingRecords");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);  //Login as valid user
                MessagingRecordsPage.MediaFilter(driver, "Sms");     //Select Sms media type
                MessagingRecordsPage.SubmitBtn(driver).Click();

                //Assert
                Assertions.VerifyMediaFilterByType(driver, "Sms");
            }
        }


        [Test, Retry(2)]
        public void MessagingRecords_Push_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MessagingRecords");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user
                MessagingRecordsPage.MediaFilter(driver, "Push");     //Select Push media type
                MessagingRecordsPage.SubmitBtn(driver).Click();

                //Assert
                Assertions.VerifyMediaFilterByType(driver, "Push");
            }
        }


        [Test, Retry(2)]
        public void MessagingRecords_Auth_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MessagingRecords");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);    //Login as valid user
                MessagingRecordsPage.SenderFilter(driver, "Auth");     //Select Auth sender type
                MessagingRecordsPage.SubmitBtn(driver).Click();

                //Assert
                Assertions.VerifySenderFilterByType(driver, "Auth");
            }
        }


        [Test, Retry(2)]
        public void MessagingRecords_ToolAuth_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MessagingRecords");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);      //Login as valid user
                MessagingRecordsPage.SenderFilter(driver, "ToolAuth");   //Select ToolAuth sender type
                MessagingRecordsPage.SubmitBtn(driver).Click();

                //Assert
                Assertions.VerifySenderFilterByType(driver, "ToolAuth");
            }
        }


        [Test, Retry(2)]
        public void MessagingRecords_SeeDetails_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MessagingRecords");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);  //Login as valid user
                MessagingRecordsPage.SeeDetails(driver).Click();

                //Assert
                Assertions.VerifyDetailsPageIsAt(driver);            //Confirm details are displayed         
                MessagingRecordsPage.BackToList(driver).Click();     //Back to message list    
                Assertions.MessagingRecordsPageIsAt(driver);         //Assert point two
            }
        }


        [Test, Retry(2)]
        public void MessagingRecords_BackToFullList()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "MessagingRecords");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user
                var count = MessagingRecordsPage.CountRecords(driver, "UserAccountID");
                MessagingRecordsPage.SelectFilter(driver, "AccountVerificationRequest", "MessageFilter");
                MessagingRecordsPage.BackToFullList(driver);
                var countFullList = MessagingRecordsPage.CountRecords(driver, "UserAccountID");

                //Assert
                Assert.IsTrue(countFullList == count);
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
