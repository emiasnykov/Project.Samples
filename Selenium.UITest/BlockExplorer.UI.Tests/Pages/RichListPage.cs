using OpenQA.Selenium;
using Shared.SharedMethods;
using System.Collections.Generic;
using System.Threading;

namespace BlockExplorer.UI.Tests.Pages
{
    class RichListPage
    {
        public static string GetAddress(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            string address = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/header/div/div[2]/nav/a[3]"), 50).Text;
            return address;
        }

        /// <summary>
        /// Open single address view
        /// </summary>
        /// <param name="driver"></param>
        public static void ViewSingleAddress(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath("(.//*[@class='info']//a)[2]"), 50).Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Get single address view elements
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static List<string> GetAddressInfo(IWebDriver driver)
        {
            List<string> address = new List<string>();
            SharedMethods.WaitUntilPreloadGone(driver);
            string header = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='heading']//h2)[1]"), 50).Text;
            string addressId = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipseMe'])[1]"), 50).Text;
            string balance = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//span)[1]"), 50).Text;
            string h2 = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='heading']//h2)[2]"), 50).Text;
            string tx = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx-content']//strong)[1]"), 50).Text;
            string txValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipseMe'])[2]"), 50).Text;
            string payload = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx-content']//strong)[2]"), 50).Text;
            string payloadValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[2]"), 50).Text;
            string from = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[3]"), 50).Text;
            string fromValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipseMe'])[3]"), 50).Text;
            address.Add(header);
            address.Add(addressId);
            address.Add(balance);
            address.Add(h2);
            address.Add(tx);
            address.Add(txValue);
            address.Add(payload);
            address.Add(payloadValue);
            address.Add(from);
            address.Add(fromValue);
            return address;
        }

        /// <summary>
        /// Opens addresses history view
        /// </summary>
        /// <param name="driver"></param>
        public static List<string> GetAddressesHistory(IWebDriver driver)
        {
            List<string> address = new List<string>();
            SharedMethods.WaitUntilPreloadGone(driver);
            string header = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section/div[1]/h2"), 50).Text;
            string rank = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section/div[2]/div[1]/div/div/div/div[1]/strong"), 50).Text;
            string rankValue = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section/div[2]/div[1]/div/ol/li[1]/div[1]"), 50).Text;
            string header2 = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section/div[2]/div[1]/div/div/div/div[2]/strong"), 50).Text;
            string addressValue = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section/div[2]/div[1]/div/ol/li[1]/div[2]/span/a"), 50).Text;
            string balance = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section/div[2]/div[1]/div/div/div/div[3]/strong"), 50).Text;
            string balanceValue = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section/div[2]/div[1]/div/ol/li[1]/div[3]"), 50).Text;
            address.Add(header);
            address.Add(rank);
            address.Add(rankValue);
            address.Add(header2);
            address.Add(addressValue);
            address.Add(balance);
            address.Add(balanceValue);
            return address;
        }
    }
}
