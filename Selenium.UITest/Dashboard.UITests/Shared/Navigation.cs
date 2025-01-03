using OpenQA.Selenium;
using System.Threading;

namespace Dashboard.UITests
{
    class Navigation
    {
        //Navigate to address page
        public static void NavigateToAddresses(IWebDriver driver, string url)
        {
            SharedMethods.WaitUntilPreloadGone(driver, url);
            Shared.FindElement(driver, By.CssSelector("a.nav-link[href='/Addresses']"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to transactions page
        public static void NavigateToTransactions(IWebDriver driver, string url)
        {
            SharedMethods.WaitUntilPreloadGone(driver, url);
            Shared.FindElement(driver, By.CssSelector("a.nav-link[href='/Transactions']"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to fausets page
        public static void NavigateToFaucets(IWebDriver driver, string url)
        {
            SharedMethods.WaitUntilPreloadGone(driver, url);
            Shared.FindElement(driver, By.CssSelector("a.nav-link[href='/Faucet']"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to webhooks page
        public static void NavigateToWebhooks(IWebDriver driver, string url)
        {
            SharedMethods.WaitUntilPreloadGone(driver, url);
            Shared.FindElement(driver, By.CssSelector("a.nav-link[href='/Webhook']"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to webhooks page
        public static void NavigateToHomePage(IWebDriver driver, string url)
        {
            SharedMethods.WaitUntilPreloadGone(driver, url);
            Shared.FindElement(driver, By.CssSelector(SharedMethods.dashboardURL + "/"), 30).Click();
            Thread.Sleep(2000);
        }
    }
}
