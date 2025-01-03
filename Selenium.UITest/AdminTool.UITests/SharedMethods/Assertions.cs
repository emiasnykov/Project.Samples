using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminTool.UITests.SharedMethods
{
    internal class Assertions
    {
        //Verify new resource created
        internal static void VerifyNewResourceCreated(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = driver.FindElements(By.TagName("h4")).Where(c => c.Text == "qa.test.api");
            Assert.IsTrue(result.LastOrDefault().Displayed);
        }

        //Verify new resource in list
        internal static void VerifyNewResourceInList(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            var result = driver.FindElements(By.XPath("/html/body/div/table/tbody/tr/td[1]"));
            Assert.That(result.Any(s => s.Text == "qa.test.api"));
        }

        //Empty name error message
        internal static void VerifyEmptyNameTextField(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var nameError = Shared.FindElement(driver, By.Id("Name-error"), 30);
            Assert.IsTrue(nameError.Text == "The Name field is required.");
        }

        //403 - Forbidden
        internal static void TextForbidden(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var h1 = Shared.FindElement(driver, By.TagName("h1"), 30);
            Assert.IsTrue(h1.Text == "Forbidden");
        }

        //Empty display name error message
        internal static void VerifyEmptyDisplayNameTextField(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var nameError = Shared.FindElement(driver, By.Id("DisplayName-error"), 30);
            Assert.IsTrue(nameError.Text == "The DisplayName field is required.");
        }

        //Verify new client created
        internal static void VerifyNewClientIsCreated(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = driver.FindElements(By.TagName("td")).Where(c => c.Text == "qa.test");
            Assert.IsTrue(result.LastOrDefault().Displayed);
        }

        //Verify h2 is displayd
        internal static void ValidateAdminToolText(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = Shared.FindElement(driver, By.TagName("h2"), 30);
            Assert.IsTrue(result.Text == "Gluwa AdminTool");
        }

        //Verify scope details displayed
        internal static void VerifyScopeDetailesDisplayed(IWebDriver driver, string name)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var header = Shared.FindElement(driver, By.XPath("//h2[contains(text(),'Scope Details')]"), 10);
            Assert.AreEqual("Scope Details", header.Text);
            var obj = Shared.FindElement(driver, By.XPath("(.//*[@class='col-md-5']//dd)[1]"), 10);
            Assert.AreEqual(name, obj.Text);
        }

        internal static void ClientTextValid(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = Shared.FindElement(driver, By.TagName("h2"), 30);
            Assert.IsTrue(result.Text == "Clients");
        }

        //Verify new identity resource created
        internal static void VerifyNewIdentityResourceCreated(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = driver.FindElements(By.TagName("h4")).Where(c => c.Text == Shared.IdentityName);
            Assert.IsTrue(result.LastOrDefault().Displayed);
        }

        //Verify error messages when missing required fields 
        internal static void VerifyRequiredFieldErrMsg(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var clientIDError = Shared.FindElement(driver, By.Id("ClientID-error"), 30);
            var clientNameError = Shared.FindElement(driver, By.Id("ClientName-error"), 30);
            Assert.IsTrue(clientIDError.Text == "The ClientID field is required.");
            Assert.IsTrue(clientNameError.Text == "The ClientName field is required.");
        }

        //Verify List of Api Resources is displayed
        internal static void ApiText(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = Shared.FindElement(driver, By.TagName("h2"), 30);
            Assert.IsTrue(result.Text.Contains("List of Api Resources"));
        }

        //Verify client details is displayed
        internal static void VerifyClientDetailsIsAt(IWebDriver driver, string clientID)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = Shared.FindElement(driver, By.XPath("(.//*[@class='dl-horizontal']//dd)[1]"), 30).Text;
            Assert.IsTrue(result == clientID);
        }

        internal static void Logout(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = Shared.FindElement(driver, By.CssSelector("#loginHeader > div"), 30);
            Assert.IsTrue(result.Text == "Pick an account");
        }

        internal static void IdentityResourcesText(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = Shared.FindElement(driver, By.TagName("h2"), 30);
            Assert.IsTrue(result.Text.Contains("List of Identity Resources"));
        }

        //Verify new identity resource in list
        internal static void VerifyNewIdentityResourceInList(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = driver.FindElements(By.TagName("h4")).Where(c => c.Text == Shared.IdentityName);
            result = driver.FindElements(By.XPath("/html/body/div/table/tbody/tr[7]/td[1]")).Where(c => c.Text == Shared.IdentityName);
            Assert.IsTrue(result.LastOrDefault().Displayed);
        }

        //Verify client edited name
        internal static void VerifyClientEdited(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = driver.FindElements(By.TagName("td")).Where(c => c.Text == "testing");
            Assert.IsTrue(result.LastOrDefault().Text == "testing");
        }

        //Verify new user in list
        internal static void VerifyNewUserIsDisplayedInGrid(IWebDriver driver, string user)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            List<IWebElement> entries = driver.FindElements(By.TagName("td")).ToList();
            List<IWebElement> result = entries.FindAll(c => c.Text == user);
            Assert.That(result.Count > 0, Is.True);
        }

        //Verify user removed
        internal static void VerifyNewUserRemoved(IWebDriver driver, string user)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            List<IWebElement> entries = driver.FindElements(By.TagName("td")).ToList();
            List<IWebElement> result = entries.FindAll(c => c.Text == user);
            Assert.That(result.Count > 0, Is.False);
        }

        //Verify resource deleted
        internal static void VerifyResourceIsDeleted(IWebDriver driver, string name)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            List<IWebElement> entries = driver.FindElements(By.TagName("td")).ToList();
            List<IWebElement> result = entries.FindAll(c => c.Text == name);
            Assert.IsFalse(result.Any());
        }

        //Invalid user error message
        internal static void VerifyInvalidUserErrMsg(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = Shared.FindElement(driver, By.XPath(".//*[@class='form-group']//span"), 30);
            Assert.IsTrue(result.Text == "Unable to find User.");
        }

        //Verify resource edited 
        internal static void VerifyResourceIsEdited(IWebDriver driver, string name)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var obj = Shared.FindElement(driver, By.XPath("(.//*[@class='col-md-5']//dd)[2]"), 30);
            Assert.AreEqual(name + ".edited", obj.Text);
        }

        //Duplicated names error message
        internal static void VerifyDuplicatedNames(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var nameError = Shared.FindElement(driver, By.XPath("(.//*[@class='form-group']//span)[1]"), 30);
            Assert.IsTrue(nameError.Text == "Duplicated name");
        }

        //Verify details displayed
        internal static void VerifyDetailesDisplayed(IWebDriver driver, string name)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var header = Shared.FindElement(driver, By.XPath("//h2[contains(text(),'Details')]"), 30);
            Assert.AreEqual("Details", header.Text);
            var obj = Shared.FindElement(driver, By.XPath("(.//*[@class='col-md-5']//dd)[1]"), 30);
            Assert.AreEqual(name, obj.Text);
        }

        //User already exist error message
        internal static void VerifyExistUserErrMsg(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var result = Shared.FindElement(driver, By.XPath(".//*[@class='form-group']//span"), 30);
            Assert.IsTrue(result.Text == "Already test user.");
        }
    }
}
