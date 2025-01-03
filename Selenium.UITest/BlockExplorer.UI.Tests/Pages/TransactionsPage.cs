using OpenQA.Selenium;
using Shared.SharedMethods;
using System.Collections.Generic;
using System.Threading;

namespace BlockExplorer.UI.Tests.Pages
{
    class TransactionsPage
    {
        /// <summary>
        /// Get transaction Id
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string GetTransactionId(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            string TxId = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='hash']//a)[2]"), 50).Text;
            return TxId;
        }

        /// <summary>
        /// Get address Id
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string GetAddress(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            string address = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipsis']//a)[2]"), 50).Text;
            return address;
        }

        /// <summary>
        /// Get transaction info elements
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static List<string> GetTransactionInfo(IWebDriver driver)
        {
            List<string> transaction = new List<string>();
            SharedMethods.WaitUntilPreloadGone(driver);
            string header = SharedMethods.FindElement(driver, By.XPath(".//*[@class='heading']//h2"), 50).Text;
            string txHashValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipseMe'])[1]"), 50).Text;
            string status = SharedMethods.FindElement(driver, By.XPath(".//*[@class='status confirmed']"), 50).Text;
            string size = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx-content']//strong)[1]"), 50).Text;
            string sizeValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx-content']//div)[1]"), 50).Text;
            string createdBy = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx-content']//strong)[3]"), 50).Text;
            string createdByValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipseMe'])[2]"), 50).Text;
            string minedTime = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx-content']//strong)[2]"), 50).Text;
            string minedTimeValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx-content']//div)[2]"), 50).Text;
            string includedInBlock = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx-content']//strong)[4]"), 50).Text;
            string includedInBlockValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx-content']//a)[2]"), 50).Text;
            string payload = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx-content']//strong)[5]"), 50).Text;
            string payloadValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[5]"), 50).Text;
            string from = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx-content']//strong)[6]"), 50).Text;
            string fromValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx-content']//a)[3]"), 50).Text;
            transaction.Add(header);
            transaction.Add(txHashValue);
            transaction.Add(status);
            transaction.Add(size);
            transaction.Add(sizeValue);
            transaction.Add(createdBy);
            transaction.Add(createdByValue);
            transaction.Add(minedTime);
            transaction.Add(minedTimeValue);
            transaction.Add(includedInBlock);
            transaction.Add(includedInBlockValue);
            transaction.Add(payload);
            transaction.Add(payloadValue);
            transaction.Add(from);
            transaction.Add(fromValue);

            return transaction;
        }

        /// <summary>
        /// Open single address view
        /// </summary>
        /// <param name="driver"></param>
        public static void ViewSingleAddress(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipsis']//a)[2]"), 50).Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Open single transction view 
        /// </summary>
        /// <param name="driver"></param>
        public static void ViewSingleTransaction(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            var element = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='hash']//a)[1]"), 50);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", element);
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Get transaction history view elements
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static List<string> CheckTransactionHistory(IWebDriver driver)
        {
            List<string> transaction = new List<string>();
            SharedMethods.WaitUntilPreloadGone(driver);
            string header = SharedMethods.FindElement(driver, By.XPath(".//*[@class='heading']//h2"), 50).Text;
            string hash = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='table-list']//strong)[1]"), 50).Text;
            string hashValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipsis']//a)[1]"), 50).Text;
            string age = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='table-list']//strong)[2]"), 50).Text;
            string ageValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipsis']//a)[2]"), 50).Text;
            string createdBy = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='table-list']//strong)[3]"), 50).Text;
            string createdByValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipsis']//a)[3]"), 50).Text;
            string payload = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='table-list']//strong)[4]"), 50).Text;
            string payloadValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipsis']//a)[4]"), 50).Text;
            transaction.Add(header);
            transaction.Add(hash);
            transaction.Add(hashValue);
            transaction.Add(age);
            transaction.Add(ageValue);
            transaction.Add(createdBy);
            transaction.Add(createdByValue);
            transaction.Add(payload);
            transaction.Add(payloadValue);
            return transaction;
        }
    }
}
