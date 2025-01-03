using OpenQA.Selenium;
using System.Threading;

namespace CSTool.UITests.Shared
{
    class Navigation
    {
        //Navigate to Info page
        public static  void GotoUserInfoPage(IWebDriver driver)
        {

            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@href='/User']"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to MarketMaking Active Status page
        public static void GotoMarketMakingPage(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@href='/MarketMakingActiveStatus']"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to Messaging Records page
        public static void GotoMessagingRecordsPage(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@href='/MessagingRecords']"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to IP Encryption page
        public static void GotoIPEncryptionPage(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@href='/IPEncrypt']"), 30).Click();
            Thread.Sleep(2000);
        }

        //Navigate to IP Decryption page
        public static void GotoIPDecryptionPage(IWebDriver driver)
        {           
            SharedMethods.WaitUntilPreloadGone(driver);
            SharedMethods.FindElement(driver, By.XPath(".//*[@href='/IPDecrypt']"), 30).Click();
            Thread.Sleep(2000);
        }

    }
}
