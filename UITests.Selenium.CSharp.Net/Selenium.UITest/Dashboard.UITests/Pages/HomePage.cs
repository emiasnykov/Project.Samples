using OpenQA.Selenium;
using System.Linq;
using System.Threading;

namespace Dashboard.UITests.Pages
{
    internal class HomePage
    {
        /// <summary>
        /// Remove whitelist IP adresses
        /// </summary>
        /// <param name="driver"></param>
        public static void ClearWhitelistedIPaddresses(IWebDriver driver)
        {
            // If there's any existing whitelisted IP addresses, remove them
            if (driver.FindElements(By.Id("whitelist-p")).Any())
            {
                Thread.Sleep(1000);
                driver.FindElements(By.Id("whitelist-clear-btn")).LastOrDefault().Click();
                driver.FindElements(By.Id("clear-ip-btn")).Where(c => c.Displayed == true).FirstOrDefault().Click();
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// User DropDown Menu
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static IWebElement UserDropDownMenu(IWebDriver driver)
        {
            Thread.Sleep(1000);
            var s  = Shared.FindElement(driver, By.CssSelector(".user-dropdown"), 10);
            return s;
        }

        /// <summary>
        /// Change Email link
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static IWebElement ChangeEmail(IWebDriver driver)
        {
            Thread.Sleep(1000);
            var s = Shared.FindElement(driver, By.XPath("//a[contains(text(),'Change Email')]"), 10);
            return s;
        }

        /// <summary>
        /// Enter new email text field
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static IWebElement NewEmailAddress(IWebDriver driver)
        {
            Thread.Sleep(1000);
            var s = Shared.FindElement(driver, By.Id("NewEmail"), 10);
            return s;
        }

        /// <summary>
        /// Submit button
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static IWebElement SubmitBtn(IWebDriver driver)
        {
            Thread.Sleep(1000);
            var s = Shared.FindElement(driver, By.XPath(".//*[@class='db-btn d-block']"), 10);
            return s;
        }

        /// <summary>
        /// Change password link
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static IWebElement ChangePassword(IWebDriver driver)
        {
            Thread.Sleep(1000);
            var s = Shared.FindElement(driver, By.XPath("//a[contains(text(),'Change Password')]"), 10);
            return s;
        }

        /// <summary>
        /// Show secret key button
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static IWebElement ViewSecretKey(IWebDriver driver)
        {
            Thread.Sleep(1000);
            var s = Shared.FindElement(driver, By.Id("buttonApiSecretShow"), 10);
            return s;
         }
 
        /// <summary>
        /// Switch language button
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        internal static IWebElement SwitchLanguage(IWebDriver driver)
        {
            var s = Shared.FindElement(driver, By.Id("language-selector"), 10);
            Thread.Sleep(1000);
            return s;
        }

        /// <summary>
        /// Set Korean language
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        internal static IWebElement SelectKoreanLanguage(IWebDriver driver)
        {
            var s = driver.FindElement(By.CssSelector("#language-popout > form:nth-child(3)"));
            Thread.Sleep(1000);
            return s;
        }

        /// <summary>
        /// Set English language
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        internal static IWebElement SelectEnglishLanguage(IWebDriver driver)
        {
            var s = Shared.FindElement(driver, By.CssSelector("#language-popout > form:nth-child(2)"), 10);
            Thread.Sleep(1000);
            return s;
        }

        /// <summary>
        /// Submit new whitelist address
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        internal static IWebElement SubmitNewWhitelistIPAddress(IWebDriver driver)
        {
            Thread.Sleep(1000);
            var s = Shared.FindElement(driver, By.Id("submit-ip-button"), 10);
            return s;
        }

        /// <summary>
        /// Get Secret key text
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        internal static string GetSecretKey(IWebDriver driver)
        {
            Thread.Sleep(1000);
            var s  = Shared.FindElement(driver, By.Id("hidden-secret"), 10).Text;
            return s;
        }

        /// <summary>
        /// Reset secret key button
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        internal static IWebElement ResetSecretKey(IWebDriver driver)
        {
            Thread.Sleep(1000);
            var s = Shared.FindElement(driver, By.XPath("//button[contains(text(),'Reset')]"), 10);
            return s;
        }

        /// <summary>
        /// Reset secret key modal button
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        internal static IWebElement ConfirmResetSecretKey(IWebDriver driver)
        {
            Thread.Sleep(1000);
            var s = Shared.FindElement(driver, By.Id("buttonModalReset"), 10);
            return s;
        }

        /// <summary>
        /// Add new whitelist Ip address button
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        internal static IWebElement AddWhiteListIpAddressBtn(IWebDriver driver)
        {
            Thread.Sleep(1000);
            var s = Shared.FindElement(driver, By.Id("whitelist-add-edit-btn"), 10);
            return s;
        }

        /// <summary>
        /// Add new whitelist Ip text field
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        internal static IWebElement AddWhiteListIpAddressValue(IWebDriver driver)
        {
            Thread.Sleep(1000);
            var s = Shared.FindElement(driver, By.Id("whitelist-textarea"), 10);
            return s;
        }
    }
}
