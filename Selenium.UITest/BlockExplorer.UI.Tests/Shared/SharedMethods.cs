using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using NUnit.Framework;

namespace Shared.SharedMethods
{
    class SharedMethods
    {

        //Goto base url
        public static void GoToUrl(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://ccblockexplorer.azurewebsites.net/");           
        }

        //Find element with timeout
        public static IWebElement FindElement(IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }

        //Advance wait
        public static void WaitUntilPreloadGone(IWebDriver driver)
        {
            if (driver.FindElements(By.CssSelector(".header-inner > nav  > a")).Count > 4)
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            else
            {
                driver.Navigate().GoToUrl("https://ccblockexplorer.azurewebsites.net/");
                Thread.Sleep(10000);
            }
        }

    }
}
