using AdminTool.UITests.SharedMethods;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AdminTool.UITests.Pages
{
    internal class ClientPage
    {
        //Client form negative inputs
        internal static void ClientFormNeg(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.XPath("/html/body/div/p/a"), 30).Click();
            Thread.Sleep(2000);
            _ = driver.FindElements(By.TagName("h2")).Where(c => c.Text == "Clients");
            Shared.FindElement(driver, By.XPath("/html/body/div/form/div/div[13]/div/input"), 30).Click();
            Thread.Sleep(2000);
        }

        //Get client details
        internal static string GetClientDetails(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var clientID = Shared.FindElement(driver, By.XPath("(.//*[@class='table']//td)[1]"), 30).Text;
            Shared.FindElement(driver, By.XPath("(//a[contains(text(),'Details')])[1]"), 30).Click(); // click details
            Thread.Sleep(2000);
            return clientID;
        }

        //Remove test clients if exist
        internal static void RemoveExistingTestClient(IWebDriver driver)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                List<IWebElement> entries = driver.FindElements(By.TagName("tr")).ToList();
                var index = entries.FindIndex(c => c.Text.Contains("qa.test"));
                List<IWebElement> links = driver.FindElements(By.LinkText("Delete")).ToList();
                links[index - 1].Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath(".//*[@type='submit']")).Click(); //click delete  
                Thread.Sleep(2000);
            }
            catch
            {
                // Existing resource not found, moving on
            }
        }

        //Edit client form
        internal static void EditClientForm(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.Id("ClientName"), 30).Clear();
            Shared.FindElement(driver, By.Id("ClientName"), 30).SendKeys("testing");
            Shared.FindElement(driver, By.XPath("/html/body/div/form/div/div[13]/div/input"), 30).Click();
            Thread.Sleep(2000);
        }

        //Edit test client button
        internal static void EditClientBtn(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            List<IWebElement> entries = driver.FindElements(By.TagName("tr")).ToList();
            var index = entries.FindIndex(c => c.Text.Contains("qa.test"));
            List<IWebElement> links = driver.FindElements(By.LinkText("Edit")).ToList();
            links[index - 1].Click();
            Thread.Sleep(2000);
        }

        //Submit button
        internal static void SubmitButton(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.XPath("/html/body/div/form/div/div[13]/div/input"), 30).Click();
            Thread.Sleep(2000);
        }

        //Create new client form positive inputs
        internal static void CreateNewClientForm(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.Id("ClientID"), 30).SendKeys("qa.test");
            Shared.FindElement(driver, By.Id("ClientName"), 30).SendKeys("qa.test");
            var radioButton = driver.FindElements(By.XPath("//*[@id='AllowedGrantType']"));
            radioButton.FirstOrDefault().Click();
            Thread.Sleep(1000);
            Shared.FindElement(driver, By.Id("allowedScopes_0__IsChecked"), 30).Click();
            Thread.Sleep(1000);

            //adding claims
            Shared.FindElement(driver, By.XPath("//*[@id='claims-field']/div/div[2]/button"), 30).Click();
            Thread.Sleep(1000);
            Shared.FindElement(driver, By.Name("ClientClaims[0].Type"), 30).SendKeys("test");
            Shared.FindElement(driver, By.Name("ClientClaims[0].Value"), 30).SendKeys("test");

            //adding origins
            Shared.FindElement(driver, By.CssSelector("#allowed-origins-field > div > div.new-text-field > button"), 30).Click();
            Thread.Sleep(1000);
            Shared.FindElement(driver, By.Name("AllowedOrigins"), 30).SendKeys("test");

            //adding redirecturis
            Shared.FindElement(driver, By.XPath("//*[@id='redirect-uris-field']/div/div[2]/button"), 30).Click();
            Thread.Sleep(1000);
            Shared.FindElement(driver, By.Name("RedirectUris"), 30).SendKeys("test");

            //adding postlogout redirecturis
            Shared.FindElement(driver, By.XPath("//*[@id='post-logout-redirect-uris-field']/div/div[2]/button"), 30).Click();
            Thread.Sleep(1000);
            Shared.FindElement(driver, By.Name("PostLogoutRedirectUris"), 30).SendKeys("test");
        }

        //Create new client button
        internal static void CreateNewButton(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.FindElement(driver, By.XPath(".//*[@class='btn btn-primary']"), 30).Click();
            Thread.Sleep(2000);
            _ = driver.FindElements(By.TagName("h2")).Where(c => c.Text == "Clients");
        }
    }
}
