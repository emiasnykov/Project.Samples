using OpenQA.Selenium;
using System;
using System.Threading;

namespace CSTool.UITests.Pages
{
    class HomePage
    {

        //Header H2
        public static IWebElement Header(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            var s = SharedMethods.FindElement(driver, By.XPath(".//*[@class='navbar-brand nav-item nav-link']"), 30);
            return s;
        }

        //LogOut
        public static IWebElement LogOut(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            var s = SharedMethods.FindElement(driver, By.LinkText("Logout"), 30);
            return s;
        }

        //Operation dropdown
        public static IWebElement OperationDropdown(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            var s = SharedMethods.FindElement(driver, By.XPath("(.//*[@href='#'])[1]"), 30);
            return s;
        }

        //External Tools dropdown
        public static IWebElement ExternalToolsDropdown(IWebDriver driver)
        {
            SharedMethods.WaitUntilPreloadGone(driver);
            var s = SharedMethods.FindElement(driver, By.XPath("(.//*[@href='#'])[3]"), 30);
            return s;
        }

    }
}
