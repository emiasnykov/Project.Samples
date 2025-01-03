using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Dashboard.UITests
{
    public class Shared
    {
        public static string BASE_URI;
        public static string DATE;
        public static string DATE_sUSDCG;
        public static string DATE_sNGNG;

        //Environments
        public static string TEST_URL = "https://gdashboard--test.azurewebsites.net";
        public static string STAGE_URL = "https://gdashboard-stage.azurewebsites.net";
        public static string DATE_TEST = "2020 - 05 - 05";
        public static string DATE_STAG = "2022 - 06 - 07";
        public static string DATE_sUSDCG_TEST = "2022 - 06 - 10";
        public static string DATE_sUSDCG_STAG = "2020 - 07 - 29";
        public static string DATE_sNGNG_TEST = "2022 - 06 - 07";
        public static string DATE_sNGNG_STAG = "2022 - 06 - 07";
        public static string DATE_sKRWCG_TEST = "2022 - 04 - 22";
        public static string DATE_NGNG = "2020 - 07 - 29";
        public static string DATE_NGNG_TEST = "2022 - 09 - 13";
        public static string DATE_NGNG_STAG = "2022 - 09 - 21";


        //Shared Methods
        public static void WaitUntilPreloadGone(IWebDriver driver)
        {
            if (driver.FindElements(By.ClassName("page-preload")).Count > 1)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until<bool>((d) =>
                {
                    try
                    {
                        // If the find succeeds, the element exists, and
                        // we want the element to *not* exist, so we want
                        // to return true when the find throws an exception.
                        IWebElement element = d.FindElement(By.ClassName("page-preload"));
                        return false;
                    }
                    catch (NoSuchElementException)
                    {
                        return true;
                    }
                });
            }
            Thread.Sleep(5000);
        }

        public static void Screenshot(IWebDriver driver, string screenshotName = "", [CallerMemberName] string callerName = "")
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString(), "screenshots");
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

        //Set environment variables
        public string SetEnvironmentVariables(string environmment = "Test")
        {
            switch (environmment)
            {
                case "Stag":
                    BASE_URI = STAGE_URL;
                    break;

                default:
                    BASE_URI = TEST_URL;
                    break;

            }
            return BASE_URI;
        }

        //Set date variables
        public string SetDateVariables(string environmment = "Test")
        {
            switch (environmment)
            {
                case "Stag":
                    DATE = DATE_STAG;
                    break;

                default:
                    DATE = DATE_TEST;
                    break;
            }
            return DATE;
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
    }
}
