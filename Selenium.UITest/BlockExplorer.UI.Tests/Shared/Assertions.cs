using NUnit.Framework;
using OpenQA.Selenium;
using Shared.SharedMethods;
using System.Collections.Generic;
using System.Threading;

namespace BlockExplorer.UI.Tests
{
    class Assertions
    {
        /// <summary>
        /// Check transactions page opened
        /// </summary>
        /// <param name="driver"></param>
        public static void TransactionsPageIsAt(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var actualResult = SharedMethods.FindElement(driver, By.CssSelector(".header-inner > nav > .active"), 30);
            var actualHeader = SharedMethods.FindElement(driver, By.XPath(".//*[@class='heading']//h2"), 30);
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Transactions", actualResult.Text);
                Assert.AreEqual("Transaction History", actualHeader.Text);
            });
        }

        /// <summary>
        /// Check blocks page opened
        /// </summary>
        /// <param name="driver"></param>
        public static void BlocksPageIsAt(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var actualResult = SharedMethods.FindElement(driver, By.CssSelector(".header-inner > nav > .active"), 30);
            var actualHeader = SharedMethods.FindElement(driver, By.XPath(".//*[@class='heading']//h2"), 30);
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Blocks", actualResult.Text);
                Assert.AreEqual("Block History", actualHeader.Text);
            });
        }

        /// <summary>
        /// Check rich list page opened
        /// </summary>
        /// <param name="driver"></param>
        public static void RichListPageIsAt(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var actualResult = SharedMethods.FindElement(driver, By.CssSelector(".header-inner > nav > .active"), 30);
            var actualHeader = SharedMethods.FindElement(driver, By.XPath(".//*[@class='heading']//h2"), 30);
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Rich List", actualResult.Text);
                Assert.AreEqual("Rich List", actualHeader.Text);
            });
        }

        /// <summary>
        /// Verify transaction history view elements
        /// </summary>
        /// <param name="elementsList"></param>
        public static void VerifyransactionHistoryInfo(List<string> elementsList)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Transaction History", elementsList[0]);
                Assert.AreEqual("Hash", elementsList[1]);
                Assert.That(elementsList[2], Is.Not.Null);
                Assert.AreEqual("Age", elementsList[3]);
                Assert.That(elementsList[4], Is.Not.Null);
                Assert.AreEqual("Created By", elementsList[5]);
                Assert.That(elementsList[6], Is.Not.Null);
                Assert.AreEqual("Payload", elementsList[7]);
                Assert.That(elementsList[8], Is.Not.Null);
            });
        }

        /// <summary>
        /// Check vesting token page opened
        /// </summary>
        /// <param name="driver"></param>
        public static void VestingTokenPageIsAt(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var header = SharedMethods.FindElement(driver, By.CssSelector(".media-body > .small"), 30);
            Assert.AreEqual("Gluwa Creditcoin Vesting Token", header.Text);
        }

        /// <summary>
        /// Check creditcoin foundation page opened
        /// </summary>
        /// <param name="driver"></param>
        public static void CreditCoinPageIsAt(IWebDriver driver)
        {
            Thread.Sleep(2000);
            SharedMethods.FindElement(driver, By.CssSelector("#header > div.header-inner > h1 > a.logo"), 30); 
        }

        /// <summary>
        /// Verify transaction ID found
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="expectedtxId"></param>
        public static void VerifyTransactionIsFound(IWebDriver driver, string expectedtxId)
        {
            Thread.Sleep(2000);
            string h2 = SharedMethods.FindElement(driver, By.XPath(".//*[@class='heading']//h2"), 30).Text;
            string actualTxId = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipseMe'])[1]"), 30).GetAttribute("data-original");
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Transaction", h2);
                Assert.AreEqual(expectedtxId, actualTxId);
            });
        }

        /// <summary>
        /// Verify address Id found
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="address"></param>
        public static void VerifyAddressIsFound(IWebDriver driver, string address)
        {
            Thread.Sleep(2000);
            string h2 = SharedMethods.FindElement(driver, By.XPath(".//*[@class='heading']//h2"), 30).Text;
            string actualAddress = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipseMe'])[1]"), 30).GetAttribute("data-original");
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Address", h2);
                Assert.AreEqual(address, actualAddress);
            });
        }

        /// <summary>
        /// Verify block Id found
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="blockId"></param>
        public static void VerifyBlockIsFound(IWebDriver driver, string blockId)
        {
            Thread.Sleep(2000);
            string h2 = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='heading']//h2)[1]"), 30).Text;
            string blockNumber = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[5]"), 30).Text;
            string actualBlockId = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipseMe'])[1]"), 30).GetAttribute("data-original");
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Block #" + (blockNumber), h2);
                Assert.AreEqual(blockId, actualBlockId);
            });
        }

        /// <summary>
        /// Verify blockchane info view
        /// </summary>
        /// <param name="elementsList"></param>
        public static void VerifyBlockchainInfo(List<string> elementsList)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual("ERC-20 Maximum Supply", elementsList[0]);
                Assert.That(elementsList[1], Is.Not.Null);
                Assert.AreEqual("Mainnet Maximum Supply", elementsList[2]);
                Assert.That(elementsList[3], Is.Not.Null);
                Assert.AreEqual("Mined Supply", elementsList[4]);
                Assert.That(elementsList[5], Is.Not.Null);
                Assert.AreEqual("Circulation Supply", elementsList[6]);
                Assert.That(elementsList[7], Is.Not.Null);
                Assert.AreEqual("Difficulty", elementsList[8]);
                Assert.That(elementsList[9], Is.Not.Null);
                Assert.AreEqual("Block Reward", elementsList[10]);
                Assert.That(elementsList[11], Is.Not.Null);
                Assert.AreEqual("Block Height", elementsList[12]);
                Assert.That(elementsList[13], Is.Not.Null);
            });

        }

        /// <summary>
        /// Verify transaction list elements
        /// </summary>
        /// <param name="transactionList"></param>
        public static void VerifyTransactionList(List<string> transactionList)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual("TX#", transactionList[0]);
                Assert.That(transactionList[1], Is.Not.Null);
                Assert.AreEqual("Payload", transactionList[2]);
                Assert.That(transactionList[3], Is.Not.Null);
                StringAssert.Contains("ago", transactionList[4]);
            });
        }

        /// <summary>
        /// Verify blocks list elements
        /// </summary>
        /// <param name="blockList"></param>
        public static void VerifyBlockList(List<string> blockList)
        {
            Assert.Multiple(() =>
            {
                Assert.That(blockList[0].Contains("Block"));
                Assert.That(blockList[1], Is.Not.Null);
                Assert.AreEqual("Mined by", blockList[2]);
                Assert.That(blockList[3], Is.Not.Null);
                Assert.AreEqual("Transactions", blockList[4]);
                Assert.That(blockList[5], Is.Not.Null);
                Assert.AreEqual("Size", blockList[6]);
                Assert.That(blockList[7], Is.Not.Null);
            });
        }

        /// <summary>
        /// Verify single transaction view
        /// </summary>
        /// <param name="transaction"></param>
        public static void VerifySingleTransactionInfo(List<string> transaction)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Transaction", transaction[0]);
                Assert.That(transaction[1], Is.Not.Null);
                Assert.AreEqual("Confirmed", transaction[2]);
                Assert.AreEqual("Size", transaction[3]);
                Assert.That(transaction[4], Is.Not.Null);
                Assert.AreEqual("Created by", transaction[5]);
                Assert.That(transaction[6], Is.Not.Null);
                Assert.AreEqual("Mined Time", transaction[7]);
                Assert.That(transaction[8], Is.Not.Null);
                Assert.AreEqual("Included in Block", transaction[9]);
                Assert.That(transaction[10], Is.Not.Null);
                Assert.AreEqual("Payload", transaction[11]);
                Assert.That(transaction[12], Is.Not.Null);
                Assert.AreEqual("From", transaction[13]);
                Assert.That(transaction[14], Is.Not.Null);
            });
        }

        /// <summary>
        /// Verify single block view
        /// </summary>
        /// <param name="blockInfo"></param>
        public static void VerifySingleBlockInfo(List<string> blockInfo)
        {
            Assert.Multiple(() =>
            {
                Assert.That(blockInfo[0], Is.Not.Null);
                Assert.That(blockInfo[1], Is.Not.Null);
                Assert.AreEqual("Number of Transactions", blockInfo[2]);
                Assert.That(blockInfo[3], Is.Not.Null);
                Assert.AreEqual("Previous Block", blockInfo[4]);
                Assert.That(blockInfo[5], Is.Not.Null);
                Assert.AreEqual("Timestamp", blockInfo[6]);
                Assert.That(blockInfo[7], Is.Not.Null);
                Assert.AreEqual("Difficulty", blockInfo[8]);
                Assert.That(blockInfo[9], Is.Not.Null);
                Assert.AreEqual("Height", blockInfo[10]);
                Assert.That(blockInfo[11], Is.Not.Null);
                Assert.AreEqual("Size", blockInfo[12]);
                Assert.That(blockInfo[13], Is.Not.Null);
                Assert.AreEqual("Block Reward", blockInfo[14]);
                Assert.That(blockInfo[15], Is.Not.Null);
                Assert.AreEqual("Version", blockInfo[16]);
                Assert.That(blockInfo[17], Is.Not.Null);
                Assert.AreEqual("Miner", blockInfo[18]);
                Assert.That(blockInfo[19], Is.Not.Null);
                Assert.AreEqual("Nonce", blockInfo[20]);
                Assert.That(blockInfo[21], Is.Not.Null);
                Assert.AreEqual("Transactions", blockInfo[22]);
                Assert.AreEqual("TX#", blockInfo[23]);
                Assert.That(blockInfo[24], Is.Not.Null);
                Assert.AreEqual("Payload", blockInfo[25]);
                Assert.That(blockInfo[26], Is.Not.Null);
                Assert.AreEqual("From", blockInfo[27]);
                Assert.That(blockInfo[28], Is.Not.Null);
            });
        }

        /// <summary>
        /// Verify blocks list elements in Blocks list
        /// </summary>
        /// <param name="blockList"></param>
        public static void VerifyRichListHistory(List<string> blockList)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Rich List", blockList[0]);
                Assert.AreEqual("Rank", blockList[1]);
                Assert.That(blockList[2], Is.Not.Null);
                Assert.AreEqual("Address", blockList[3]);
                Assert.That(blockList[4], Is.Not.Null);
                Assert.AreEqual("Balance", blockList[5]);
                Assert.That(blockList[6], Is.Not.Null);
            });
        }


        /// <summary>
        /// Verify single address view
        /// </summary>
        /// <param name="addressInfo"></param>
        public static void VerifySingleAddressInfo(List<string> addressInfo)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Address", addressInfo[0]);
                Assert.That(addressInfo[1], Is.Not.Null);
                Assert.That(addressInfo[2], Is.Not.Null);
                Assert.AreEqual("Transactions", addressInfo[3]);
                Assert.AreEqual("TX#", addressInfo[4]);
                Assert.That(addressInfo[5], Is.Not.Null);
                Assert.AreEqual("Payload", addressInfo[6]);
                Assert.That(addressInfo[7], Is.Not.Null);
            });
        }

        /// <summary>
        /// Verify blocks list elements in Blocks list
        /// </summary>
        /// <param name="blockList"></param>
        public static void VerifyBlocksListHistory(List<string> blockList)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Block History", blockList[0]);
                Assert.AreEqual("Height", blockList[1]);
                Assert.That(blockList[2], Is.Not.Null);
                Assert.AreEqual("Timestamp", blockList[3]);
                Assert.That(blockList[4], Is.Not.Null);
                Assert.AreEqual("Transactions", blockList[5]);
                Assert.That(blockList[6], Is.Not.Null);
                Assert.AreEqual("Mined by", blockList[7]);
                Assert.That(blockList[8], Is.Not.Null);
            });
        }

        /// <summary>
        /// Verify single block view in Block list
        /// </summary>
        /// <param name="addressInfo"></param>
        public static void BlocksVerifySingleBlockInfo(List<string> blockInfo)
        {
            Assert.Multiple(() =>
            {
                Assert.That(blockInfo[0], Is.Not.Null);
                Assert.That(blockInfo[1], Is.Not.Null);
                Assert.AreEqual("Number of Transactions", blockInfo[2]);
                Assert.That(blockInfo[3], Is.Not.Null);
                Assert.AreEqual("Previous Block", blockInfo[4]);
                Assert.That(blockInfo[5], Is.Not.Null);
                Assert.AreEqual("Timestamp", blockInfo[6]);
                Assert.That(blockInfo[7], Is.Not.Null);
                Assert.AreEqual("Difficulty", blockInfo[8]);
                Assert.That(blockInfo[9], Is.Not.Null);
                Assert.AreEqual("Height", blockInfo[10]);
                Assert.That(blockInfo[11], Is.Not.Null);
                Assert.AreEqual("Size", blockInfo[12]);
                Assert.That(blockInfo[13], Is.Not.Null);
                Assert.AreEqual("Block Reward", blockInfo[14]);
                Assert.That(blockInfo[15], Is.Not.Null);
                Assert.AreEqual("Version", blockInfo[16]);
                Assert.That(blockInfo[17], Is.Not.Null);
                Assert.AreEqual("Miner", blockInfo[18]);
                Assert.That(blockInfo[19], Is.Not.Null);
                Assert.AreEqual("Nonce", blockInfo[20]);
                Assert.That(blockInfo[21], Is.Not.Null);
                Assert.AreEqual("Transactions", blockInfo[22]);
                Assert.AreEqual("TX#", blockInfo[23]);
                Assert.That(blockInfo[24], Is.Not.Null);
                Assert.AreEqual("Payload", blockInfo[25]);
                Assert.That(blockInfo[26], Is.Not.Null);
                Assert.AreEqual("From", blockInfo[27]);
                Assert.That(blockInfo[28], Is.Not.Null);
            });
        }
    }
}
