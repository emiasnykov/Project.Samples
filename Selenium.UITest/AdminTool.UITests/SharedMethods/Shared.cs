using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace AdminTool.UITests.SharedMethods
{
    internal class Shared
    {
        //Test data
        public static string Name = "qa.test.api";
        public static string IdentityName = "qa.test.identity";
        public static string User = "qa.test3";
        public static string InvalidUser = "qa.test4";

        //Advance wait
        public static void WaitUntilPreloadGone(IWebDriver driver)
        {
            if (driver.FindElements(By.CssSelector(".nav > li > a")).Count > 1)
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            else
            {
                driver.Navigate().GoToUrl("https://gadmintool-test.azurewebsites.net/");
                Thread.Sleep(10000);
            }
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

        //Make a screeshot
        public static void Screenshot(IWebDriver driver, string screenshotName = "", [CallerMemberName] string callerName = "")
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();

            if (screenshotName == "")
            {
                ss.SaveAsFile(Path.Combine(path, $"{callerName}.png"));
            }
            else
            {
                ss.SaveAsFile(Path.Combine(path, screenshotName + ".png"));
            }
        }

    }
}
