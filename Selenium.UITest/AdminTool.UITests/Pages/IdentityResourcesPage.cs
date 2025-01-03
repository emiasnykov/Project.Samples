using AdminTool.UITests.SharedMethods;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AdminTool.UITests.Pages
{
    internal class IdentityResourcesPage
    {
        //Remove existing identity resource
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
                driver.FindElement(By.XPath("/html/body/div/div/form/input[1]")).Click(); //click delete
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            }
            catch
            {
                // Existing resource not found, moving on
            }
        }

        //Create new button
        internal static IWebElement CreateNewBtn(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var s = Shared.FindElement(driver, By.LinkText("Create New"), 30);
            return s;
        }

        //Create New Identity Resource
        public static void CreateNewIdentityResource(IWebDriver driver, string name, IdentityResourceEnum resourceEnum)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //Switch to proper user login and initialize it
            switch (resourceEnum)
            {
                //Valid resource
                case IdentityResourceEnum.ValidResource:
                    Shared.FindElement(driver, By.Id("Name"), 30).SendKeys(name);
                    Shared.FindElement(driver, By.Id("DisplayName"), 30).SendKeys(name);
                    Shared.FindElement(driver, By.Id("Description"), 30).SendKeys("This is a testing account");
                    Shared.FindElement(driver, By.Id("add-userclaim-button"), 30).Click();
                    Shared.FindElement(driver, By.Id("UserClaims_0_"), 30).SendKeys(name);
                    break;

                //Empty Name
                case IdentityResourceEnum.NoName:
                    Shared.FindElement(driver, By.Id("Name"), 30).SendKeys("");
                    Shared.FindElement(driver, By.Id("DisplayName"), 30).SendKeys(name);
                    Shared.FindElement(driver, By.Id("Description"), 30).SendKeys("This is a testing account");
                    Shared.FindElement(driver, By.Id("add-userclaim-button"), 30).Click();
                    Shared.FindElement(driver, By.Id("UserClaims_0_"), 30).SendKeys(name);
                    break;

                //Empty DisplayName
                case IdentityResourceEnum.NoDisplayName:
                    Shared.FindElement(driver, By.Id("Name"), 30).SendKeys(name);
                    Shared.FindElement(driver, By.Id("DisplayName"), 30).SendKeys("");
                    Shared.FindElement(driver, By.Id("Description"), 30).SendKeys("This is a testing account");
                    Shared.FindElement(driver, By.Id("add-userclaim-button"), 30).Click();
                    Shared.FindElement(driver, By.Id("UserClaims_0_"), 30).SendKeys(name);
                    break;
            }
        }

        //Back to identity resources list
        internal static IWebElement BackToIdentityListBtn(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var s = Shared.FindElement(driver, By.LinkText("Back to List"), 30);
            return s;
        }

        //Save button
        internal static IWebElement SaveBtn(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var s = Shared.FindElement(driver, By.XPath("/html/body/div/div[1]/div/form/div[2]/div/div/input"), 30);
            return s;
        }

        //Get resource details
        internal static void GetResourceDetails(IWebDriver driver, string name)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            try
            {
                List<IWebElement> entries = driver.FindElements(By.TagName("tr")).ToList();
                var index = entries.FindIndex(c => c.Text.Contains(name));
                List<IWebElement> links = driver.FindElements(By.LinkText("Details")).ToList();
                links[index - 1].Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            }
            catch
            {
                // Existing resource not found, moving on
            }
        }

        //Delete resource
        internal static void DeleteResource(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.XPath("(//a[contains(text(),'Delete')])[1]"), 30).Click();  // click delete scope
            Thread.Sleep(2000);
            Shared.FindElement(driver, By.XPath(".//*[@class='btn btn-danger']"), 30).Click(); //click confirm delete
        }

        //Edit resource
        internal static void EditResource(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.XPath("(//a[contains(text(),'Edit')])[1]"), 30).Click();  // click delete scope
            Thread.Sleep(2000);
            Shared.FindElement(driver, By.Id("DisplayName"), 30).Clear();
            Shared.FindElement(driver, By.Id("DisplayName"), 30).SendKeys("qa.test.identity.edited");
            SaveBtn(driver).Click();
            Thread.Sleep(2000);
        }
    }
}
