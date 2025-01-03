using OpenQA.Selenium;
using System.Linq;
using System.Threading;

namespace Dashboard.UITests.Pages
{
    class HomePage
    {
        // Remove whitelist IP adresses
        public static void ClearWhitelistedIPaddresses(IWebDriver driver)
        {
            //If there's any existing whitelisted IP addresses, remove them
                if (driver.FindElements(By.Id("whitelist-p")).Any())
            {
                Thread.Sleep(2000);
                driver.FindElements(By.Id("whitelist-clear-btn")).LastOrDefault().Click();
                Thread.Sleep(2000);
                driver.FindElements(By.Id("clear-ip-btn")).Where(c => c.Displayed == true).FirstOrDefault().Click();
                Thread.Sleep(2000);
            }
        }

        // User DropDown Menu
        public static IWebElement UserDropDownMenu(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s  = Shared.FindElement(driver, By.CssSelector(".user-dropdown"), 10);
            return s;
        }

        //Change Email link
        public static IWebElement ChangeEmail(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = Shared.FindElement(driver, By.XPath("//a[contains(text(),'Change Email')]"), 10);
            return s;
        }

        // Enter new email text field
        public static IWebElement NewEmailAddress(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = Shared.FindElement(driver, By.Id("NewEmail"), 10);
            return s;
        }

        // Submit button
        public static IWebElement SubmitBtn(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = Shared.FindElement(driver, By.XPath(".//*[@class='db-btn d-block']"), 10);
            return s;
        }

        //Change password link
        public static IWebElement ChangePassword(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = Shared.FindElement(driver, By.XPath("//a[contains(text(),'Change Password')]"), 10);
            return s;
        }

        // Show secret key button
         public static IWebElement ViewSecretKey(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = Shared.FindElement(driver, By.Id("buttonApiSecretShow"), 10);
            return s;
         }

        // Switch language button
        internal static IWebElement SwitchLanguage(IWebDriver driver)
        {
            var s = Shared.FindElement(driver, By.Id("language-selector"), 10);
            Thread.Sleep(2000);
            return s;
        }

        // Set Korean language
        internal static IWebElement SelectKoreanLanguage(IWebDriver driver)
        {
            var s = driver.FindElement(By.CssSelector("#language-popout > form:nth-child(3)"));
            Thread.Sleep(2000);
            return s;
        }

        //Set English language
        internal static IWebElement SelectEnglishLanguage(IWebDriver driver)
        {
            var s = Shared.FindElement(driver, By.CssSelector("#language-popout > form:nth-child(2)"), 10);
            Thread.Sleep(2000);
            return s;
        }

        //Submit new whitelist address
        internal static IWebElement SubmitNewWhitelistIPAddress(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = Shared.FindElement(driver, By.Id("submit-ip-button"), 10);
            return s;
        }

        // Get Secret key text
        internal static string GetSecretKey(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s  = Shared.FindElement(driver, By.Id("hidden-secret"), 10).Text;
            return s;
        }

        // Reset secret key button 
        internal static IWebElement ResetSecretKey(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = Shared.FindElement(driver, By.XPath("//button[contains(text(),'Reset')]"), 10);
            return s;
        }

        // Reset secret key modal button
        internal static IWebElement ConfirmResetSecretKey(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = Shared.FindElement(driver, By.Id("buttonModalReset"), 10);
            return s;
        }

        //Add new whitelist Ip address button
        internal static IWebElement AddWhiteListIpAddressBtn(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = Shared.FindElement(driver, By.Id("whitelist-add-edit-btn"), 10);
            return s;
        }

        //Add new whitelist Ip text field
        internal static IWebElement AddWhiteListIpAddressValue(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = Shared.FindElement(driver, By.Id("whitelist-textarea"), 10);
            return s;
        }
    }
}
