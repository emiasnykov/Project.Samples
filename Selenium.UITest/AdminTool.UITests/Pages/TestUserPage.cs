using AdminTool.UITests.SharedMethods;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AdminTool.UITests.Pages
{
    internal class TestUserPage
    {
        //Add new user button
        internal static void AddNewTestUserBtn(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.XPath(".//*[@class='btn btn-primary']"), 30).Click();
            Thread.Sleep(2000);
        }

        //Add new user textarea
        internal static void AddNewTestUserTextField(IWebDriver driver, string name)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.Id("Username"), 30).SendKeys(name);
            Thread.Sleep(2000);
        }

        //Search button
        internal static void SearchBtn(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.XPath(".//*[@class='btn btn-primary']"), 30).Click();
            Thread.Sleep(2000);
        }

        //Remove user by name
        internal static void RemoveTestUser(IWebDriver driver, string name)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                List<IWebElement> entries = driver.FindElements(By.TagName("tr")).ToList();
                var index = entries.FindIndex(c => c.Text.Contains(name));
                List<IWebElement> links = driver.FindElements(By.XPath(".//*[@type='submit']")).ToList();
                links[index - 1].Click();
                Thread.Sleep(2000);
            }
            catch
            {
                // Existing resource not found, moving on
            }
        }

        //Confirm button
        internal static void ClickConfirmBtn(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.XPath(".//*[@type='submit']"), 30).Click();
            Thread.Sleep(2000);
        }
    }
}
