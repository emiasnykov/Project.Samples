using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using NUnit.Framework;

namespace Dashboard.UITests
{
    internal class SharedMethods
    {
        public static string dashboardURL = "https://gdashboard--test.azurewebsites.net";
        public static string authURL = "https://auth-test.gluwa.com";
        public static string authChangeEmailURL = "https://auth-test.gluwa.com/ManageAccount/SendChangeEmailLink";
        //TODO: Is there a better way to switch between environments?
        //public static string dashboardURL = "https://gdashboard-stage.azurewebsites.net/";
        //public static string authURL = "https://gauth-stage.azurewebsites.net/";
        //public static string authChangeEmailURL = "https://gauth-stage.azurewebsites.net/ManageAccount/SendChangeEmailLink";
        public const int TotalWaitSeconds = 20;
        public const int WaitIntervalMiliseconds = 50;

        /// <summary>
        /// Make Screenshot
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="screenshotName"></param>
        /// <param name="callerName"></param>
        public static void Screenshot(IWebDriver driver, string screenshotName = "", [CallerMemberName] string callerName = "")
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString(),"screenshots");
            System.IO.Directory.CreateDirectory(path);
            if (screenshotName == "")
            {
                string filePath = Path.Combine(path, $"{callerName}.png");
                ss.SaveAsFile(filePath);
                TestContext.AddTestAttachment(filePath, callerName);
            }
            else
            {
                string filePath = Path.Combine(path, screenshotName + ".png");
                ss.SaveAsFile(filePath);
                TestContext.AddTestAttachment(filePath, screenshotName);
            }
        }

        /// <summary>
        /// Advance wait
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public static void WaitUntilPreloadGone(IWebDriver driver, string url)
        {
            if (driver.FindElements(By.CssSelector("a.nav-link")).Count > 1) {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until<bool>((d) =>
                {
                    try
                    {
                        // If the find succeeds, the element exists, and
                        // we want the element to *not* exist, so we want
                        // to return true when the find throws an exception.
                        IWebElement element = d.FindElement(By.XPath(".//*[@href='/User245']"));
                        return false;
                    }
                    catch (NoSuchElementException)
                    {
                        return true;
                    }
                });
            }
            else
            {
                driver.Navigate().GoToUrl(url);
                Thread.Sleep(10000);
            }
        }

        /// <summary>
        /// Custom wait for address page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public static void WaitUntilPreloadAddressPage(IWebDriver driver, string url)
        {
            if (driver.FindElements(By.CssSelector("a.nav-link")).Count > 1)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until<bool>((d) =>
                {
                    try
                    {
                        // If the find succeeds, the element exists, and
                        // we want the element to *not* exist, so we want
                        // to return true when the find throws an exception.
                        IWebElement element = d.FindElement(By.XPath(".//*[@href='/User245']"));
                        return false;
                    }
                    catch (NoSuchElementException)
                    {
                        return true;
                    }
                });
            }
            else
            {
                driver.Navigate().GoToUrl(url + "/Addresses");
                Thread.Sleep(10000);
            }
        }

        /// <summary>
        /// Timeout
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static WebDriverWait GetWait(IWebDriver driver)
        {
            var clock = new SystemClock();
            var totalWaitTimeout = new TimeSpan(0, 0, TotalWaitSeconds);
            var waitInterval = new TimeSpan(0, 0, 0, 0, WaitIntervalMiliseconds);
            return new WebDriverWait(clock, driver, totalWaitTimeout, waitInterval);
        }

        /// <summary>
        /// WaitFor element to appear
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        public static void WaitForElementToAppear(IWebDriver driver, By locator)
        {
            var wait = GetWait(driver);
            wait.Until(webDriver => webDriver.FindElement(locator).Displayed);
        }  
    }
}
