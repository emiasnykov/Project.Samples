using AdminTool.UITests.SharedMethods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace AdminTool.UITests.Pages
{
    internal class LoginPage
    {
        //Goto base url
        public static void GoToUrl(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://gadmintool-test.azurewebsites.net/");
        }

        //Login As
        public static void LoginAs(IWebDriver driver, LoginUserEnum loginUserEnum)
        {

            //Switch to proper user login and initialize it
            switch (loginUserEnum)
            {
                //Valid user
                case LoginUserEnum.ValidUser:
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
                    Thread.Sleep(5000);
                    wait.Until<IWebElement>(c => c.FindElement(By.Id("i0116"))).SendKeys("qa.automation@gluwa.com");
                    wait.Until<IWebElement>(c => c.FindElement(By.CssSelector("#idSIButton9"))).Click();
                    Thread.Sleep(5000);
                    wait.Until<IWebElement>(c => c.FindElement(By.Id("i0118"))).SendKeys("u9LW9SucZC9WyVVNH%vG98gyzf2Wfc9ET2BgyKFKY^&YGfmkKU");
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                    //sign in
                    _ = wait.Until<IWebElement>(c => c.FindElement(By.CssSelector("#idSIButton9")));
                    driver.FindElement(By.CssSelector("#idSIButton9")).Click();
                    //click yes
                    wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
                    _ = wait.Until<IWebElement>(c => c.FindElement(By.Id("idSIButton9")));
                    driver.FindElement(By.Id("idSIButton9")).Click();
                    break;

                //Unknown user
                case LoginUserEnum.UnknownUser:
                    driver.FindElement(By.Id("Email")).SendKeys("automation@gluwa.com");
                    driver.FindElement(By.Id("Password")).SendKeys("T3st0rder!");
                    driver.FindElement(By.Name("button")).Click();
                    break;

                //Empty email
                case LoginUserEnum.NoEmail:
                    driver.FindElement(By.Id("Email")).SendKeys("");
                    driver.FindElement(By.Id("Password")).SendKeys("T3st0rder!");
                    driver.FindElement(By.Name("button")).Click();
                    break;

                //Empty password
                case LoginUserEnum.NoPassword:
                    driver.FindElement(By.Id("Email")).SendKeys("qa.automation@gluwa.com");
                    driver.FindElement(By.Id("Password")).SendKeys("");
                    driver.FindElement(By.Name("button")).Click();
                    break;
            }

        }
    }
}
