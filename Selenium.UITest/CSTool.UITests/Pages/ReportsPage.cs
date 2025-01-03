using OpenQA.Selenium;
using System.Threading;

namespace CSTool.UITests.Pages
{
    class ReportsPage
    {
        //Reports
        public static IWebElement Reports(IWebDriver driver)
        {
            var s = driver.FindElement(By.XPath("/html/body/nav/div/div/ul/li[3]/a"));
            return s;
        }

        //Know Your Customer
        public static IWebElement KYC(IWebDriver driver)
        { 
            var s = driver.FindElement(By.XPath("/html/body/nav/div/div/ul/li[3]/ul/li/a"));
            return s;
        }

        //Download button
        public static IWebElement Download(IWebDriver driver)
        {
            var s = driver.FindElement(By.Id("DownloadButton"));
            return s;
        }

        //Decrypt button
        public static IWebElement Report(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = driver.FindElement(By.Id("ReportButton"));
            return s;
        }

    }
}
