using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

namespace CSTool.UITests.Pages
{
    class MessagingRecordsPage
    {
        //Submit button
        public static IWebElement SubmitBtn(IWebDriver driver)
        {
            //var s = driver.FindElement(By.XPath(".//*[@type='submit']"));
            var s = driver.FindElement(By.Id("FilterSubmitButton"));
            return s;
        }

        //Message filter
        internal static void MessageFilter(IWebDriver driver, string filter)
        {
            var s = SharedMethods.FindElement(driver, By.Id("MessageFilter"), 10);
            var element = new SelectElement(s);
            element.SelectByText(filter);
        }

        //Media filter
        internal static void MediaFilter(IWebDriver driver, string filter)
        {
            var s = SharedMethods.FindElement(driver, By.Id("MediaFilter"), 10);
            var element = new SelectElement(s);
            element.SelectByText(filter);
        }

        //Sender filter
        internal static void SenderFilter(IWebDriver driver, string filter)
        {
            var s = SharedMethods.FindElement(driver, By.Id("SenderFilter"), 10);
            var element = new SelectElement(s);
            element.SelectByText(filter);
        }

        //Get record details
        internal static IWebElement SeeDetails(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var s = SharedMethods.FindElement(driver, By.LinkText("Details"), 10);
            return s;
        }

        //Back to list
        internal static IWebElement BackToList(IWebDriver driver)
        {
            var s = SharedMethods.FindElement(driver, By.XPath("//a[contains(text(),'Back to List')]"), 10);
            return s;       
        }

        //Count records in list
        public static int CountRecords(IWebDriver driver, string name)
        {
            Thread.Sleep(2000);
            var userAccountId = driver.FindElements(By.TagName("tr")).Where(c => c.Text.Contains(name));
            var count = driver.FindElements(By.TagName("tr")).Except(userAccountId).Count();
            int result = count;
            while (count != 0)
            {
                driver.FindElement(By.XPath("/html/body/div/a[2]")).Click(); //click next
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                userAccountId = driver.FindElements(By.TagName("tr")).Where(c => c.Text.Contains(name));
                count = driver.FindElements(By.TagName("tr")).Except(userAccountId).Count();
                result += count;
            }
            return result;
        }

        //Select filter
        public static void SelectFilter(IWebDriver driver, string filter, string type)
        {
            var messageFilter = SharedMethods.FindElement(driver, By.Id(type), 10);
            var element = new SelectElement(messageFilter);
            element.SelectByText(filter);
            SharedMethods.FindElement(driver, By.CssSelector("body > div > form > input"), 10).Click();
        }

        //Back to Full list
        internal static void BackToFullList(IWebDriver driver)
        {
            SharedMethods.FindElement(driver, By.CssSelector("body > div > div > a"), 10).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
    }
}
