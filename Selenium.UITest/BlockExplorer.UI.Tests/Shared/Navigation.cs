using OpenQA.Selenium;
using Shared.SharedMethods;
using System;
using System.Threading;

namespace BlockExplorer.UI.Tests
{
    class Navigation
    {

        //Navigate to Transactions page
        public static void GotoTransactionsPage(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@href='/transactions']"), 30).Click();
            Thread.Sleep(2000);
        }

        internal static void GotoHomePage(IWebDriver driver)
        {
            SharedMethods.FindElement(driver, By.XPath(".//*[@href='/']"), 30).Click();
            Thread.Sleep(2000);
        }

        internal static void GotoBlocksPage(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@href='/blocks']"), 30).Click();
            Thread.Sleep(2000);
        }

        internal static void GotoRichListPage(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@href='/rich-list']"), 30).Click();
            Thread.Sleep(2000);
        }

        internal static void GotoVestingTokenPage(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath("(//a[contains(text(),'Vesting Token')])"), 30).Click();
            Thread.Sleep(2000);
        }

        internal static void GotoCreditCoinPage(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath("(//a[contains(text(),'Creditcoin Foundation')])"), 30).Click();
            Thread.Sleep(2000);
        }
    }
}
