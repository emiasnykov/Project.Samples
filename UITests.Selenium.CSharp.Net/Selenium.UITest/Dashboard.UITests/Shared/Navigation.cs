using OpenQA.Selenium;
using System.Threading;

namespace Dashboard.UITests
{
    internal class Navigation
    {
        /// <summary>
        /// Navigate to address page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public static void NavigateToAddresses(IWebDriver driver, string url)
        {
            SharedMethods.WaitUntilPreloadGone(driver, url);
            Shared.FindElement(driver, By.CssSelector("a.nav-link[href='/Addresses']"), 30).Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Navigate to transactions page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public static void NavigateToTransactions(IWebDriver driver, string url)
        {
            SharedMethods.WaitUntilPreloadGone(driver, url);
            Shared.FindElement(driver, By.CssSelector("a.nav-link[href='/Transactions']"), 30).Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Navigate to fausets page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public static void NavigateToFaucets(IWebDriver driver, string url)
        {
            SharedMethods.WaitUntilPreloadGone(driver, url);
            Shared.FindElement(driver, By.CssSelector("a.nav-link[href='/Faucet']"), 30).Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Navigate to webhooks page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public static void NavigateToWebhooks(IWebDriver driver, string url)
        {
            SharedMethods.WaitUntilPreloadGone(driver, url);
            Shared.FindElement(driver, By.CssSelector("a.nav-link[href='/Webhook']"), 30).Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Navigate to webhooks page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public static void NavigateToHomePage(IWebDriver driver, string url)
        {
            SharedMethods.WaitUntilPreloadGone(driver, url);
            Shared.FindElement(driver, By.CssSelector(SharedMethods.dashboardURL + "/"), 30).Click();
            Thread.Sleep(2000);
        }
    }
}
