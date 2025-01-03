using NUnit.Framework;
using OpenQA.Selenium;
using Shared.SharedMethods;
using System.Collections.Generic;
using System.Threading;
namespace BlockExplorer.UI.Tests.Pages
{
    class HomePage
    {
        /// <summary>
        /// Get transactions list
        /// </summary>
        /// <param name="driver"></param>
        public static void ViewAllTransactions(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@href='/transactions']//em"), 50).Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Get block list
        /// </summary>
        /// <param name="driver"></param>
        public static void ViewAllBlocks(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@href='/transactions']//em[2]"), 50).Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Search by transaction Id
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="TxId"></param>
        public static void SearchByTransactionId(IWebDriver driver, string TxId)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@id='search']//input"), 50).SendKeys(TxId);
            Thread.Sleep(2000);
            var input = SharedMethods.FindElement(driver, By.XPath(".//*[@id='search']//input"), 50);
            input.SendKeys(Keys.Return);
        }

        /// <summary>
        /// Search by address
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="address"></param>
        public static void SearchByAddress(IWebDriver driver, string address)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@id='search']//input"), 50).SendKeys(address);
            Thread.Sleep(2000);
            var input = SharedMethods.FindElement(driver, By.XPath(".//*[@id='search']//input"), 50);
            input.SendKeys(Keys.Return);
        }

        /// <summary>
        /// Search by block Id
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="blockId"></param>
        public static void SearchByBlockId(IWebDriver driver, string blockId)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@id='search']//input"), 50).SendKeys(blockId);
            Thread.Sleep(2000);
            var input = SharedMethods.FindElement(driver, By.XPath(".//*[@id='search']//input"), 50);
            input.SendKeys(Keys.Return);
        }

        /// <summary>
        /// Get blockchain info elements
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static List<string> GetBlockchainInfo(IWebDriver driver)
        {
            List<string> elementsList = new List<string>();
            SharedMethods.WaitUntilPreloadGone(driver);
            string maxSupply = SharedMethods.FindElement(driver, By.XPath("//*[@class='erc']//..//*[@class='supply-info']/dt"), 50).Text;
            string maxSupplyValue = SharedMethods.FindElement(driver, By.XPath("//*[@class='erc']//..//*[@class='supply-info']/dd"), 50).Text;
            string mainSupply = SharedMethods.FindElement(driver, By.XPath("//*[@class='mined']//..//*[@class='supply-info']/dt"), 50).Text;
            string mainSupplyValue = SharedMethods.FindElement(driver, By.XPath("//*[@class='mined']//..//*[@class='supply-info']/dd"), 50).Text;
            string minedSupply = SharedMethods.FindElement(driver, By.XPath("//*[@class='mined']//..//*[@class='donut-chart']//dl/dt"), 50).Text;
            string minedSupplyValue = SharedMethods.FindElement(driver, By.XPath("//*[@class='mined']//..//*[@class='donut-chart']//dl/dd/em"), 50).Text;
            string circulationSupply = SharedMethods.FindElement(driver, By.XPath("//*[@class='erc']//..//*[@class='donut-chart']//dl/dt"), 50).Text;
            string circulationSupplyValue = SharedMethods.FindElement(driver, By.XPath("//*[@class='erc']//..//*[@class='donut-chart']//dl/dd/em"), 50).Text;
            string difficulty = SharedMethods.FindElement(driver, By.XPath("//*[@class='dashboard box']//div//div[2]//dl//dt"), 50).Text;
            string difficultyValue = SharedMethods.FindElement(driver, By.XPath("//*[@class='dashboard box']//div//div[2]//dl//dd"), 50).Text;
            string blockReward = SharedMethods.FindElement(driver, By.XPath("//*[@class='dashboard box']//div[1]//div[2]//dl[2]/dt"), 50).Text;
            string blockRewardValue = SharedMethods.FindElement(driver, By.XPath("//*[@class='dashboard box']//div[1]//div[2]//dl[2]/dd"), 50).Text;
            string blockHeight = SharedMethods.FindElement(driver, By.XPath("//*[@class='block-height']/../h4"), 50).Text;
            string blockHeightValue = SharedMethods.FindElement(driver, By.XPath("//*[@class='block-height']"), 50).Text;
            elementsList.Add(maxSupply);
            elementsList.Add(maxSupplyValue);
            elementsList.Add(mainSupply);
            elementsList.Add(mainSupplyValue);
            elementsList.Add(minedSupply);
            elementsList.Add(minedSupplyValue);
            elementsList.Add(circulationSupply);
            elementsList.Add(circulationSupplyValue);
            elementsList.Add(difficulty);
            elementsList.Add(difficultyValue);
            elementsList.Add(blockReward);
            elementsList.Add(blockRewardValue);
            elementsList.Add(blockHeight);
            elementsList.Add(blockHeightValue);
            return elementsList;
        }

        /// <summary>
        /// Get transaction list elements
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static List<string> GetTransactionList(IWebDriver driver)
        {
            List<string> transactionList = new List<string>();
            SharedMethods.WaitUntilPreloadGone(driver);
            var Txs = driver.FindElements(By.ClassName("type-transaction"));
            Assert.AreEqual(5, Txs.Count);
            string txNo = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='float'])[1]"), 50).Text;
            string txNoValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipseMe'])[1]"), 50).Text;
            string payload = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='float'])[2]"), 50).Text;
            string payloadValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipseMe'])[2]"), 50).Text;
            string time = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='time']//span)[1]"), 50).Text;
            transactionList.Add(txNo);
            transactionList.Add(txNoValue);
            transactionList.Add(payload);
            transactionList.Add(payloadValue);
            transactionList.Add(time);
            return transactionList;
        }

        /// <summary>
        /// Get block list elements
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static List<string> GetBlockList(IWebDriver driver)
        {
            List<string> blockList = new List<string>();
            SharedMethods.WaitUntilPreloadGone(driver);
            var Txs = driver.FindElements(By.ClassName("non-line-mobile"));
            Assert.AreEqual(5, Txs.Count);
            string block = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='block']//em)[1]"), 50).Text;
            string blockValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='block-info']//em)[2]"), 50).Text;
            string minedBy = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='info']//span)[1]"), 50).Text;
            string minedByValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='paragraph ellipsis mined']//a)[1]"), 50).Text;
            string transactions = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='info']//strong)[1]"), 50).Text;
            string transactionsValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='info']//span)[2]"), 50).Text;
            string size = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='info']//strong)[2]"), 50).Text;
            string sizeValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='info']//span)[3]"), 50).Text;
            blockList.Add(block);
            blockList.Add(blockValue);
            blockList.Add(minedBy);
            blockList.Add(minedByValue);
            blockList.Add(transactions);
            blockList.Add(transactionsValue);
            blockList.Add(size);
            blockList.Add(sizeValue);
            return blockList;
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
        /// Open single block view
        /// </summary>
        /// <param name="driver"></param>
        public static void ViewSingleBlock(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath("(.//*[@class='block']//em)[2]"), 50).Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Open single transaction view
        /// </summary>
        /// <param name="driver"></param>
        public static void ViewSingleTransaction(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath("(.//*[@class='inner']//a)[1]"), 50).Click();
            Thread.Sleep(2000);
        }
    }
}
