using OpenQA.Selenium;
using Shared.SharedMethods;
using System;
using System.Collections.Generic;

namespace BlockExplorer.UI.Tests.Pages
{
    class BlocksPage
    {
        internal static string GetBlockid(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            string blockId = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipseMe'])[1]"), 50).GetAttribute("data-original");
            return blockId;
        }

        internal static void ViewSingleBlock(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath("(.//*[@class='height']//a)[1]"), 50).Click();
        }

        internal static List<string> GetBlockInfo(IWebDriver driver)
        {
            List<string> block = new List<string>();
            SharedMethods.WaitUntilPreloadGone(driver);
            string header = SharedMethods.FindElement(driver, By.XPath(".//*[@class='heading']//h2"), 50).Text;
            string blockId = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='ellipseMe'])[1]"), 50).Text;
            string number = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[1]"), 50).Text;
            string numberValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[1]"), 50).Text;
            string prvBlock = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[2]"), 50).Text;
            string prvBlockValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[2]"), 50).Text;
            string timestamp = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[3]"), 50).Text;
            string timestampValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[3]"), 50).Text;
            string difficulty = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[4]"), 50).Text;
            string difficultyValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[4]"), 50).Text;
            string height = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[5]"), 50).Text;
            string heightValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[5]"), 50).Text;
            string size = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[6]"), 50).Text;
            string sizeValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[6]"), 50).Text;
            string blockRwd = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[7]"), 50).Text;
            string blockRwdValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[7]"), 50).Text;
            string version = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[8]"), 50).Text;
            string versionValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[8]"), 50).Text;
            string miner = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[9]"), 50).Text;
            string minerValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[9]"), 50).Text;
            string nonce = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[10]"), 50).Text;
            string nonceValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[10]"), 50).Text;
            string header2 = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='heading']//h2)[2]"), 50).Text;
            string tx = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[11]"), 50).Text;
            string txValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[11]"), 50).Text;
            string payload = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[12]"), 50).Text;
            string payloadValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[12]"), 50).Text;
            string from = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='bx']//strong)[13]"), 50).Text;
            string fromValue = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='detail'])[13]"), 50).Text;
            block.Add(header);
            block.Add(blockId);
            block.Add(number);
            block.Add(numberValue);
            block.Add(prvBlock);
            block.Add(prvBlockValue);
            block.Add(timestamp);
            block.Add(timestampValue);
            block.Add(difficulty);
            block.Add(difficultyValue);
            block.Add(height);
            block.Add(heightValue);
            block.Add(size);
            block.Add(sizeValue);
            block.Add(blockRwd);
            block.Add(blockRwdValue);
            block.Add(version);
            block.Add(versionValue);
            block.Add(miner);
            block.Add(minerValue);
            block.Add(nonce);
            block.Add(nonceValue);

            block.Add(header2);
            block.Add(tx);
            block.Add(txValue);
            block.Add(payload);
            block.Add(payloadValue);
            block.Add(from);
            block.Add(fromValue);
            return block;
        }

        internal static List<string> GetBlockHistoryList(IWebDriver driver)
        {
            List<string> blockList = new List<string>();
            SharedMethods.WaitUntilPreloadGone(driver);
            string header = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section[1]/div/h2"), 50).Text;
            string height = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section[2]/div[1]/div/div/div[1]/strong"), 50).Text;
            string heightValue = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section[2]/div[1]/ul/li[1]/div[1]"), 50).Text;
            string timestamp = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section[2]/div[1]/div/div/div[2]"), 50).Text;
            string timestampValue = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section[2]/div[1]/ul/li[1]/div[2]"), 50).Text;
            string tx = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section[2]/div[1]/div/div/div[3]"), 50).Text;
            string txValue = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section[2]/div[1]/ul/li[1]/div[3]"), 50).Text;
            string minedBy = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section[2]/div[1]/div/div/div[4]/strong"), 50).Text;
            string address = SharedMethods.FindElement(driver, By.XPath("/html/body/div/section/section/article/section[2]/div[1]/ul/li[1]/div[4]/span/a"), 50).Text;
            // block.Add(header1);
            blockList.Add(header);
            blockList.Add(height);
            blockList.Add(heightValue);
            blockList.Add(timestamp);
            blockList.Add(timestampValue);
            blockList.Add(tx);
            blockList.Add(txValue);
            blockList.Add(minedBy);
            blockList.Add(address);
            return blockList;
        }
    }
}
