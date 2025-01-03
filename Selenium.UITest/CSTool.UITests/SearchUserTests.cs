using CSTool.UITests.Enum;
using CSTool.UITests.Pages;
using CSTool.UITests.Shared;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace CSTool.UITests
{
    [TestFixture]
    public class SearchUserTests
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
        public void UserInfo_ValidUsername_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);                 //Login as valid user
                UserInfoPage.SearchByUserNameOrEmail(driver, "qa.test1");           //Valid UserName
                UserInfoPage.SearchBtn(driver).Click();

                //Assert
                Assertions.VerifyUserDetailsIsAt(driver);
            }
        }


        [Test, Retry(2)]
        public void UserInfo_ValidEmail_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);                      //Login as valid user
                UserInfoPage.SearchByUserNameOrEmail(driver, "qa.assertible@gluwa.com"); //Valid Email
                UserInfoPage.SearchBtn(driver).Click();

                //Assert
                Assertions.VerifyUserDetailsIsAt(driver);
            }
        }


        [Test, Retry(2)]
        public void UserInfo_ValidPhoneNo_US_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);         //Login as valid user
                UserInfoPage.SearchByPhoneNo(driver, "5555550002", "1");    //Valid Phone No. /US
                UserInfoPage.SearchByPhoneBtn(driver).Click();

                //Assert
                Assertions.VerifyUserDetailsIsAt(driver);
            }
        }


        [Test, Retry(2)]
        public void UserInfo_ValidPhoneNo_KR_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);         //Login as valid user
                UserInfoPage.SearchByPhoneNo(driver, "5555550001", "82");   //Valid Phone No. /KR
                UserInfoPage.SearchByPhoneBtn(driver).Click();

                //Assert
                Assertions.VerifyUserDetailsIsAt(driver);
            }
        }
        [Test, Retry(2)]
        public void UserInfo_ValidInvestmentAccountWallet_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);         //Login as valid user
                UserInfoPage.InvestmentAccountWalletAddress(driver, "0xf04349b4a760f5aed02131e0daa9bb99a1d1d1e5");   //Valid Investment Account Wallet Address
                UserInfoPage.SearchByWalletAddress(driver).Click();

                //Assert
                Assertions.VerifyUserDetailsIsAt(driver);
            }
        }

        [Test, Retry(2)]
        public void UserInfo_InValidInvestmentAccountWallet_Pos()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);         //Login as valid user
                UserInfoPage.InvestmentAccountWalletAddress(driver, "0xf04349b4a760f5aed02131e0daa9bb99a1d1d000");   //Invalid Investment Account Wallet Address
                UserInfoPage.SearchByWalletAddress(driver).Click();

                //Assert
                Assertions.VerifyNotExistWallet(driver);
            }
        }

        [Test, Retry(2)]
        public void UserInfo_EmailOrUserNameNotExist_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);                      //Login as valid user
                UserInfoPage.SearchByUserNameOrEmail(driver, "qa.test1.wrong");          //Not exist Email or UserName
                UserInfoPage.SearchBtn(driver).Click();

                //Assert
                Assertions.VerifyNotExistEmailorUserName(driver, "qa.test1.wrong");
            }
        }


        [Test, Retry(2)]
        public void UserInfo_InvalidEmailOrUserName_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);        //Login as valid user
                UserInfoPage.SearchByUserNameOrEmail(driver, " ");          //Invalid Email or UserName
                UserInfoPage.SearchBtn(driver).Click();

                //Assert
                Assertions.VerifyInvalidEmailOrUserName(driver);
            }
        }


        [Test, Retry(2)]
        public void UserInfo_EmptyEmailOrUserName_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);        //Login as valid user
                UserInfoPage.SearchByUserNameOrEmail(driver, "");          //Empty Email or UserName
                UserInfoPage.SearchBtn(driver).Click();

                //Assert
                Assertions.VerifyEmptyEmailOrUserName(driver);
            }
        }


        [Test, Retry(2)]
        public void UserInfo_PhoneNoNotExist_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);         //Login as valid user
                UserInfoPage.SearchByPhoneNo(driver, "7775551112", "1");    //Not exist Phone No. /US
                UserInfoPage.SearchByPhoneBtn(driver).Click();

                //Assert
                Assertions.VerifyNotExistPhoneNo(driver);
            }
        }


        [Test, Retry(2)]
        public void UserInfo_EmptyPhoneNo_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user
                UserInfoPage.SearchByPhoneNo(driver, "", "1");        //Empty Phone No. /US
                UserInfoPage.SearchByPhoneBtn(driver).Click();

                //Assert
                Assertions.VerifyEmptyPhoneNo(driver);
            }
        }


        [Test, Retry(2)]
        public void UserInfo_InvalidPhoneNo_Neg()
        {
            using (var init = new TestScope(browser))
            {
                //Setup
                driver = init.Instance;

                //Steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);   //Login as valid user
                UserInfoPage.SearchByPhoneNo(driver, "  ", "1");        //Invalid Phone No. /US
                UserInfoPage.SearchByPhoneBtn(driver).Click();

                //Assert
                Assertions.VerifyInvalidPhoneNo(driver);
            }
        }

        [Test, Retry(2)]
        public void UserDetails_ValidateCustomerDetailsID()
        {
            using (var init = new TestScope(browser)){

                //setup
                driver = init.Instance;

                //steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);
                UserInfoPage.SearchByUserNameOrEmail(driver, "qa.assertible"); //Search by valid username
                UserInfoPage.SearchBtn(driver).Click();


                //Assert
                Assertions.VerifyCustomerDetailsUserId(driver, "309d1527-167f-48b9-91e5-a6e376dd26d0");


            }
        }

        [Test, Retry(2)]
        public void UserDetails_ValidateCustomerDetails()
        {
            using (var init = new TestScope(browser))
            {

                //setup
                driver = init.Instance;

                //steps
                LoginPage.GoToUrl(driver, "User");
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);
                UserInfoPage.SearchByUserNameOrEmail(driver, "qa.assertible"); //Search by valid username
                UserInfoPage.SearchBtn(driver).Click();


                //Assert
                Assertions.VerifyCustomerDetails(driver);


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
