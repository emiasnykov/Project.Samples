using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace CSTool.UITests
{
    public class SharedMethods
    {
        //Shared test data 
        public static string ValidEncryptedIP = "MDAwMDAwMDAwMDAwMDAwMDtKmR0snZIpXHw6G_8x0Di0O2OKRGf9TfxUDYeYJOBF";
        public static string InvalidEncryptedIP = "MDAwMDAwMDAwMDAwMDAwMDtKmR0snZIpXHw6G_8x0Di0O2OKRGf9TfxUDYeYJOB";
        public static string InvalidEncryptedIP_ErrMsg = "Exception from IP decryptions task: The input data is not a complete block. -";
        public static string TooShortEncryptedIP_ErrMsg = "Exception from IP decryptions task: Arithmetic operation resulted in an overflow. -";
        public static string InvalidIpAddress_ErrMsg = "The field IP Address must be a string or array type with a minimum length of '7'.";
        public static string EmptyEncryptedIP_ErrMsg = "The Encrypted IP field is required.";
        public static string EmptyIpAddress_ErrMsg = "The IP Address field is required.";
        public static string TooShortEncryptedIP = "MDA";
        public static string ValidIpAddress = "192.168.1.1";
        public static string InvalidIpAddress = "1.4445";

        //Shared test methods
        public static string IP(IWebDriver driver)
        {
            var element = FindElement(driver, By.XPath("(.//*[@class='col-sm-9'])[2]"), 30).Text;
            return element;
        }

        //Alert
        public static string Alert(IWebDriver driver)
        {
            var element = FindElement(driver, By.XPath("(.//*[@class='form-group']//span)[2]"), 30).Text;
            return element;
        }

        //Error message
        public static string ErrorMessage(IWebDriver driver)
        {
            var element = FindElement(driver, By.XPath(".//*[@class='row']//td"), 30).Text;
            return element;
        }
       
        //Make screenshot
        public static void Screenshot(IWebDriver driver, string screenshotName = "", [CallerMemberName] string callerName = "")
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString();

            if (screenshotName == "")
            {
                ss.SaveAsFile(Path.Combine(path, $"{callerName}.png"));
            }
            else
            {
                ss.SaveAsFile(Path.Combine(path, screenshotName + ".png"));
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


        //Advance wait
        public static void WaitUntilPreloadGone(IWebDriver driver)
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
                        //IWebElement element = d.FindElement(By.XPath("(//a[contains(text(),'Tools')])[1]"));
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
                driver.Navigate().GoToUrl("https://gtool-test.azurewebsites.net/");
                Thread.Sleep(10000);
            }
        }

    }
}
