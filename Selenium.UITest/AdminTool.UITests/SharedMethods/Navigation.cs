using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace AdminTool.UITests.SharedMethods
{
    internal class Navigation
    {
        //Navigate to ApiResources page
        public static void GotoApiResourcesPage(IWebDriver driver)
        {
            Shared.WaitUntilPreloadGone(driver);
            Shared.FindElement(driver, By.XPath(".//*[@href='/ApiResources']"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to ApiIdentityResources page
        internal static void GotoIdentityResourcesPage(IWebDriver driver)
        {
            Shared.WaitUntilPreloadGone(driver);
            Shared.FindElement(driver, By.LinkText("Identity Resources"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to TestUser page
        internal static void GotoTestUserPage(IWebDriver driver)
        {
            Shared.WaitUntilPreloadGone(driver);
            Shared.FindElement(driver, By.LinkText("Test User"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to Clients page
        internal static void GotoClientsPage(IWebDriver driver)
        {
            Shared.WaitUntilPreloadGone(driver);
            Shared.FindElement(driver, By.LinkText("Clients"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to Home page
        internal static void AdminTool(IWebDriver driver)
        {
            Shared.WaitUntilPreloadGone(driver);
            Shared.FindElement(driver, By.LinkText("Gluwa AdminTool"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to Clients page
        internal static void ClientText(IWebDriver driver)
        {
            Shared.WaitUntilPreloadGone(driver);
            driver.FindElement(By.LinkText("Clients")).Click();
            Thread.Sleep(2000);
        }

        //Navigate to API Resources page
        internal static void ApiResourcesText(IWebDriver driver)
        {
            Shared.WaitUntilPreloadGone(driver);
            driver.FindElement(By.LinkText("API Resources")).Click();
            Thread.Sleep(2000);
        }

        //Navigate to IdentityResources page
        internal static void GotoIdentityResourcesText(IWebDriver driver)
        {
            Shared.WaitUntilPreloadGone(driver);
            driver.FindElement(By.LinkText("Identity Resources")).Click();
            Thread.Sleep(2000);
        }

        //Logout
        internal static void Logout(IWebDriver driver)
        {
            Shared.WaitUntilPreloadGone(driver);
            Shared.FindElement(driver, By.LinkText("Logout"), 30).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            _ = wait.Until(c => c.FindElement(By.CssSelector("#loginHeader > div")));
            Thread.Sleep(2000);
        }
    }
}
