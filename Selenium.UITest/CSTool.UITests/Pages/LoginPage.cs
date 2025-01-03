using CSTool.UITests.Enum;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace CSTool.UITests.Pages
{
    class LoginPage
    {
        //Goto base url
        public static void GoToUrl(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://gtool-test.azurewebsites.net/");
        }

        public static void GoToUrl(IWebDriver driver, string path)
        {
            driver.Navigate().GoToUrl("https://gtool-test.azurewebsites.net/" + path);
        }

        //Login As
        public static void LoginAs(IWebDriver driver, LoginUserEnum loginUserEnum)
        {

            //Switch to proper user login and initialize it
            switch (loginUserEnum)
            {
                //Valid user
                case LoginUserEnum.ValidUser:
                    SharedMethods.FindElement(driver, By.Id("Email"), 30).SendKeys("qa.automation@gluwa.com");
                    driver.FindElement(By.Id("Password")).SendKeys("T3st0rder!");
                    driver.FindElement(By.Name("button")).Click();
                    Thread.Sleep(2000);
                    break;

                //Unknown user
                case LoginUserEnum.UnknownUser:
                    SharedMethods.FindElement(driver, By.Id("Email"), 30).SendKeys("automation@gluwa.com");
                    driver.FindElement(By.Id("Password")).SendKeys("T3st0rder!");
                    driver.FindElement(By.Name("button")).Click();
                break;

                //Empty email
                case LoginUserEnum.NoEmail:
                    SharedMethods.FindElement(driver, By.Id("Email"), 30).SendKeys("");
                    driver.FindElement(By.Id("Password")).SendKeys("T3st0rder!");
                    driver.FindElement(By.Name("button")).Click();
                break;

                //Empty password
                case LoginUserEnum.NoPassword:
                    SharedMethods.FindElement(driver, By.Id("Email"), 30).SendKeys("qa.automation@gluwa.com");
                    driver.FindElement(By.Id("Password")).SendKeys("");
                    driver.FindElement(By.Name("button")).Click();
                break;
            }

        }

        //Error message
        public static IWebElement ErrMsg(IWebDriver driver)
        {
            var err = SharedMethods.FindElement(driver, By.XPath(".//*[@class='alert alert-danger']//li"), 30);
            return err;
        }

        //Alert message
        public static IWebElement AlertMsg(IWebDriver driver)
        {
            var err = SharedMethods.FindElement(driver, By.XPath(".//*[@class='col-md-10']//span"), 30);
            return err;
        }

        //Confirmation message
        public static IWebElement ConfirmtMsg(IWebDriver driver)
        {
            var msg = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='container body-content']//p)[1]"), 30);
            return msg;
        }

        //Forgot password button
        public static IWebElement ForgotPasswordBtn(IWebDriver driver)
        {
            var s = SharedMethods.FindElement(driver, By.XPath(".//*[@href='/Account/ForgotPassword']"), 30);
            return s;
        }

        //Enter your email field
        public static IWebElement EnterYourEmail(IWebDriver driver)
        {
            var s = SharedMethods.FindElement(driver, By.XPath(".//*[@type='email']"), 30);
            return s;
        }

        //Submit button
        public static IWebElement SubmitBtn(IWebDriver driver)
        {
            var s = SharedMethods.FindElement(driver, By.XPath(".//*[@type='submit']"), 30);
            return s;
        }

    }
}
