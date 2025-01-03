using AdminTool.UITests.SharedMethods;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AdminTool.UITests.Pages
{
    internal class ApiResourcesPage
    {
        //Create New Api Resource
        public static void CreateNewApiResource(IWebDriver driver, string name, ApiResourceEnum resourceEnum)
        {

            //Switch to proper user login and initialize it
            switch (resourceEnum)
            {
                //Valid resource
                case ApiResourceEnum.ValidResource:
                    Shared.FindElement(driver, By.Id("Name"), 30).SendKeys(name);
                    Shared.FindElement(driver, By.Id("DisplayName"), 30).SendKeys("qa.test.api");
                    Shared.FindElement(driver, By.Id("add-userclaim-button"), 30).Click();
                    Thread.Sleep(2000);
                    Shared.FindElement(driver, By.Id("UserClaims_0_"), 30).SendKeys("qa.test.api");
                    break;

                //Empty Name
                case ApiResourceEnum.NoName:
                    Shared.FindElement(driver, By.Id("Name"), 30).SendKeys("");
                    Shared.FindElement(driver, By.Id("DisplayName"), 30).SendKeys("qa.test.api");
                    Shared.FindElement(driver, By.Id("add-userclaim-button"), 30).Click();
                    Thread.Sleep(2000);
                    Shared.FindElement(driver, By.Id("UserClaims_0_"), 30).SendKeys("qa.test.api");
                    break;

                //Empty DisplayName
                case ApiResourceEnum.NoDisplayName:
                    Shared.FindElement(driver, By.Id("Name"), 30).SendKeys("qa.test.api");
                    Shared.FindElement(driver, By.Id("DisplayName"), 30).SendKeys("");
                    Shared.FindElement(driver, By.Id("add-userclaim-button"), 30).Click();
                    Thread.Sleep(2000);
                    Shared.FindElement(driver, By.Id("UserClaims_0_"), 30).SendKeys("qa.test.api");
                    break;
            }
        }

        //Create New button
        internal static IWebElement CreateNewBtn(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var s = Shared.FindElement(driver, By.LinkText("Create New"), 30);
            return s;
        }

        //Save button
        internal static IWebElement SaveBtn(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var s = Shared.FindElement(driver, By.XPath("/html/body/div/div[1]/div/form/div[2]/div/div/input"), 30);
            return s;
        }

        //Remove resource by name
        internal static void RemoveExistResources(IWebDriver driver, string name)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                List<IWebElement> entries = driver.FindElements(By.TagName("tr")).ToList();
                var index = entries.FindIndex(c => c.Text.Contains(name));
                List<IWebElement> links = driver.FindElements(By.LinkText("Delete")).ToList();
                links[index - 1].Click();
                Thread.Sleep(2000);
                Shared.FindElement(driver, By.XPath("/html/body/div/div/form/input[1]"), 30).Click(); //click delete
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            }
            catch
            {
                // Existing resource not found, moving on
            }
        }

        //Back to list
        internal static IWebElement BackToListBtn(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var s = Shared.FindElement(driver, By.LinkText("Back to List"), 30);
            return s;
        }

        //Get scope details
        internal static void GetScopeDetails(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.XPath("(.//*[@class='table']//a)[1]"), 30).Click();  // click details
            Thread.Sleep(2000);
        }

        //Delete scope
        internal static void DeleteScope(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.XPath("(//a[contains(text(),'Delete')])[1]"), 10).Click();  // click delete scope
            Thread.Sleep(2000);
            Shared.FindElement(driver, By.XPath(".//*[@class='btn btn-danger']"), 30).Click(); //click confirm delete
            Thread.Sleep(2000);
        }

        //Edit scope
        internal static void EditScope(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.XPath("(//a[contains(text(),'Edit')])[1]"), 10).Click();  // click delete scope
            Thread.Sleep(2000);
            Shared.FindElement(driver, By.Id("DisplayName"), 30).Clear();
            Shared.FindElement(driver, By.Id("DisplayName"), 30).SendKeys("qa.test.api.edited");
            SaveBtn(driver).Click();
            Thread.Sleep(2000);
        }
    }
}
