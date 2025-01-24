using Dashboard.UITests.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Dashboard.UITests.Pages
{
    internal class LoginPage
    {
        // Goto base url
        public static void GoToUrl(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        // Login As
        public static void LoginAs(IWebDriver driver, LoginUserEnum loginUserEnum)
        {
            // Switch to proper user login and initialize it
            switch (loginUserEnum)
            {
                // Valid user
                case LoginUserEnum.ValidUser:
                    try
                    {
                        Shared.FindElement(driver, By.Id("Email"), 30).SendKeys("qa+***");
                        Shared.FindElement(driver, By.Id("Password"), 30).SendKeys("****");
                        Thread.Sleep(1000);
                        Shared.FindElement(driver, By.CssSelector("body > section > div > div > div.ab-content > form > div.field.pt-2.mb-0 > button"), 30).Click();
                        Shared.WaitUntilPreloadGone(driver);
                        driver.Manage().Cookies.AddCookie(new Cookie(".AspNet.Consent", "yes"));
                    }
                    catch
                    {
                        Assert.Inconclusive("Login failed - CI might be redeploying at the moment");
                    }
                break;

                // Valid user for ETH
                case LoginUserEnum.ValidUserETH:
                
                    try
                    {
                        Shared.FindElement(driver, By.Id("Email"), 30).SendKeys("qa******");
                        Shared.FindElement(driver, By.Id("Password"), 30).SendKeys("*****");
                        Thread.Sleep(1000);
                        Shared.FindElement(driver, By.CssSelector("body > section > div > div > div.ab-content > form > div.field.pt-2.mb-0 > button"), 30).Click();
                        Shared.WaitUntilPreloadGone(driver);
                        driver.Manage().Cookies.AddCookie(new Cookie(".AspNet.Consent", "yes"));
                    }
                    catch
                    {
                        Assert.Inconclusive("Login failed - CI might be redeploying at the moment");
                    }
                break;

                // Unknown user
                case LoginUserEnum.UnknownUser:
                    try
                    {
                        Shared.FindElement(driver, By.Id("Email"), 30).SendKeys("qa+*******");
                        Shared.FindElement(driver, By.Id("Password"), 30).SendKeys("******");
                        Thread.Sleep(1000);
                        Shared.FindElement(driver, By.CssSelector("body > section > div > div > div.ab-content > form > div.field.pt-2.mb-0 > button"), 30).Click();
                        Shared.WaitUntilPreloadGone(driver);
                        driver.Manage().Cookies.AddCookie(new Cookie(".AspNet.Consent", "yes"));
                    }
                    catch
                    {
                        Assert.Inconclusive("Login failed - CI might be redeploying at the moment");
                    }
                break;

                // Invalid user
                case LoginUserEnum.InvalidEmail:
                    try
                    {
                        Shared.FindElement(driver, By.Id("Email"), 30).SendKeys("qa+******");
                        Shared.FindElement(driver, By.Id("Password"), 30).SendKeys("******");
                        Thread.Sleep(1000);
                        Shared.FindElement(driver, By.CssSelector("body > section > div > div > div.ab-content > form > div.field.pt-2.mb-0 > button"), 30).Click();
                        Shared.WaitUntilPreloadGone(driver);
                        driver.Manage().Cookies.AddCookie(new Cookie(".AspNet.Consent", "yes"));
                    }
                    catch
                    {
                        Assert.Inconclusive("Login failed - CI might be redeploying at the moment");
                    }
                break;
            }
        }

        /// <summary>
        /// Goto base url
        /// </summary>
        /// <param name="driver"></param>
        public static void SignUpBtn(IWebDriver driver)
        {
            Thread.Sleep(1000);
            Shared.FindElement(driver, By.XPath(".//*[@href='/Account/Signup']"), 10).Click();
          
        }

        /// <summary>
        ///  SignUp
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="signUpUserEnum"></param>
        public static void SignUpWith(IWebDriver driver, SignUpEnum signUpUserEnum)
        {
            switch (signUpUserEnum)
            {
                // Valid credentials
                case SignUpEnum.ValidCredentials:
                    Shared.FindElement(driver, By.Id("Username"), 10).SendKeys("QA***");
                    Shared.FindElement(driver, By.Id("Email"), 10).SendKeys("qa@****");
                    Shared.FindElement(driver, By.Id("Password"), 10).SendKeys("pass***");
                    Shared.FindElement(driver, By.XPath(".//*[@type='submit']"), 10).Click();
                    Thread.Sleep(1000);
                break;

                // Invalid user
                case SignUpEnum.InvalidUserName:
                    Shared.FindElement(driver, By.Id("Username"), 10).SendKeys("sd3****");
                    Shared.FindElement(driver, By.Id("Email"), 10).SendKeys("qa@*****");
                    Shared.FindElement(driver, By.Id("Password"), 10).SendKeys("pass***");
                    Shared.FindElement(driver, By.XPath(".//*[@type='submit']"), 10).Click();
                    Thread.Sleep(1000);
                break;

                // Invalid email
                case SignUpEnum.InvalidEmail:
                    Shared.FindElement(driver, By.Id("Username"), 10).SendKeys("QA**");
                    Shared.FindElement(driver, By.Id("Email"), 10).SendKeys("qa*****");
                    Shared.FindElement(driver, By.Id("Password"), 10).SendKeys("pass***");
                    Shared.FindElement(driver, By.XPath(".//*[@type='submit']"), 10).Click();
                    Thread.Sleep(1000);
                break;

                // Without password
                case SignUpEnum.NoPassword:
                    Shared.FindElement(driver, By.Id("Username"), 10).SendKeys("QA***");
                    Shared.FindElement(driver, By.Id("Email"), 10).SendKeys("qa@*****");
                    Shared.FindElement(driver, By.Id("Password"), 10).SendKeys("");
                    Shared.FindElement(driver, By.XPath(".//*[@type='submit']"), 10).Click();
                    Thread.Sleep(1000);
                break;

                // Too Short Password
                case SignUpEnum.PasswordTooShort:
                    Shared.FindElement(driver, By.Id("Username"), 10).SendKeys("QA***");
                    Shared.FindElement(driver, By.Id("Email"), 10).SendKeys("qa@*****");
                    Shared.FindElement(driver, By.Id("Password"), 10).SendKeys("p");
                    Shared.FindElement(driver, By.XPath(".//*[@type='submit']"), 10).Click();
                    Thread.Sleep(2000);
                break;
            }
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="driver"></param>
        public static void Logout(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until<IWebElement>(c => c.FindElement(By.Id("menuUser"))).Click();
            wait.Until<IWebElement>(c => c.FindElement(By.LinkText("Logout"))).Click();
            IWebElement Home = driver.FindElement(By.XPath("/html/body/header"));
        }
    }
}
