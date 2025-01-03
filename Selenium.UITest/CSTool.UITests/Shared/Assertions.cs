using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

namespace CSTool.UITests.Shared
{
    class Assertions
    {
        //Verify list filtered by message type
        public static void VerifyMessageFilterByType(IWebDriver driver, string msgType) 
        {
            if (driver.FindElements(By.XPath("(.//*[@class='table']//td)")).Any())
            {
                Assert.AreEqual(msgType, driver.FindElement(By.XPath("(.//*[@class='table']//td)[2]")).Text);
            }
            else
            {
                TestContext.WriteLine("Message type: " + msgType + " - records Not Found");
            }
        }

        //Verify list filtered by media type
        public static void VerifyMediaFilterByType(IWebDriver driver, string mediaType)
        {
            Thread.Sleep(5000);
            if (driver.FindElements(By.XPath("(.//*[@class='table']//td)")).Any())
            {
                Assert.AreEqual(mediaType, driver.FindElement(By.XPath("(.//*[@class='table']//td)[3]")).Text);
            }
            else
            {
                TestContext.WriteLine("Media type: " + mediaType + " - records Not Found");
            }
        }

        //Confirm page is opened
        internal static void ConfirmPageIsOpened(IWebDriver driver, string pageName)
        {
            IWebElement h2 = SharedMethods.FindElement(driver, By.TagName("h2"), 30);
            Assert.IsTrue(h2.Displayed);
            Assert.IsTrue(h2.Text == pageName);  //Confirm page is opened/ header is displayed 
        }

        //Verify list filtered by sender type
        internal static void VerifySenderFilterByType(IWebDriver driver, string senderType)
        {
            if (driver.FindElements(By.XPath("(.//*[@class='table']//td)")).Any())
            {
                Assert.AreEqual(senderType, driver.FindElement(By.XPath("(.//*[@class='table']//td)[4]")).Text);
            }
            else
            {
                TestContext.WriteLine("Sender type: " + senderType + " - records Not Found");
            }
        }

        //Confirm log out
        internal static void ConfirmLogOut(IWebDriver driver)
        {
            IWebElement loginPage = SharedMethods.FindElement(driver, By.ClassName("login-page"), 30);
            Assert.IsTrue(loginPage.Displayed);   //Confirm page is opened/ header is displayed
        }

        //Confirm details page amd elements are displayed
        internal static void VerifyDetailsPageIsAt(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var header = driver.FindElements(By.TagName("h2")).Where(c => c.Text == "Details Page");
            Assert.IsTrue(header.LastOrDefault().Displayed);

            var Id = driver.FindElements(By.XPath("//dt[contains(text(),'UserAccountID')]"));
            Assert.IsTrue(Id.LastOrDefault().Displayed);

            var msgType = driver.FindElements(By.XPath("//dt[contains(text(),'MessageType')]"));
            Assert.IsTrue(msgType.LastOrDefault().Displayed);

            var mediaType = driver.FindElements(By.XPath("//dt[contains(text(),'MediaType')]"));
            Assert.IsTrue(mediaType.LastOrDefault().Displayed);

            var msgSender = driver.FindElements(By.XPath("//dt[contains(text(),'MessageSender')]"));
            Assert.IsTrue(msgSender.LastOrDefault().Displayed);

            var maskEmail = driver.FindElements(By.XPath("//dt[contains(text(),'MaskedEmail')]"));
            Assert.IsTrue(maskEmail.LastOrDefault().Displayed);

            var maskPhoneNo = driver.FindElements(By.XPath("//dt[contains(text(),'MaskedPhoneNumber')]"));
            Assert.IsTrue(maskPhoneNo.LastOrDefault().Displayed);

            var refId = driver.FindElements(By.XPath("//dt[contains(text(),'ReferenceID')]"));
            Assert.IsTrue(refId.LastOrDefault().Displayed);

            var createdDT = driver.FindElements(By.XPath("//dt[contains(text(),'CreatedDateTime')]"));
            Assert.IsTrue(createdDT.LastOrDefault().Displayed);
        }

        //Confirm user details are displayed
        internal static void VerifyUserDetailsIsAt(IWebDriver driver)
        {
            Thread.Sleep(3000);
            if (driver.FindElements(By.Id("tableUserInfo")).Any())
            {
                var result = SharedMethods.FindElement(driver, By.TagName("h2"), 30);
                Assert.IsTrue(result.Displayed);
            }
            else
            {
                TestContext.WriteLine("Details Not Found");
            }
        }

        //Confirm messaging records page is opened
        internal static void MessagingRecordsPageIsAt(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var pageHeader = driver.FindElements(By.XPath("//h2[contains(text(),'Messaging Record List')]"));
            Assert.IsTrue(pageHeader.LastOrDefault().Displayed);           
        }

        //UserName or Email not exist - error message
        internal static void VerifyNotExistEmailorUserName(IWebDriver driver, string username)
        {
            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until<IWebElement>(c => c.FindElement(By.XPath("//*[@id=\"toast-container\"]/div")));
            var result = SharedMethods.FindElement(driver, By.XPath("//*[@id=\"toast-container\"]/div"), 30);
            Assert.AreEqual("Username: " + username + " does not exist.", result.Text);
        }

        //Empty UserName or Email - error message
        internal static void VerifyEmptyEmailOrUserName(IWebDriver driver)
        {

            var result = SharedMethods.FindElement(driver, By.Id("NameOrEmail-error"), 30);
            Assert.AreEqual("The Username or E-mail field is required.", result.Text);
        }

        //invalid UserName or Email - error message
        internal static void VerifyInvalidEmailOrUserName(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until<IWebElement>(c => c.FindElement(By.XPath("//*[@id=\"toast-container\"]/div")));
            var result = SharedMethods.FindElement(driver, By.XPath("//*[@id=\"toast-container\"]/div"), 30);
            Assert.AreEqual("Error: Invalid Name or Email", result.Text);
        }

        //Phone  not exist - error message
        internal static void VerifyNotExistPhoneNo(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var result = SharedMethods.FindElement(driver, By.XPath("(.//*[@class='container body-content']//td)[1]"), 30);
            Assert.IsTrue(result.Displayed);
        }

        //Empty phone no - error message
        internal static void VerifyEmptyPhoneNo(IWebDriver driver)
        {
            Thread.Sleep(5000);
            var result = SharedMethods.FindElement(driver, By.Id("PhoneNumber-error"), 30);
            Assert.AreEqual("The Phone Number field is required.", result.Text);
        }

        //Invalid phone no. - error message
        internal static void VerifyInvalidPhoneNo(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until<IWebElement>(c => c.FindElement(By.XPath("//*[@id=\"toast-container\"]/div")));
            var result = SharedMethods.FindElement(driver, By.XPath("//*[@id=\"toast-container\"]/div"), 30);
            Assert.AreEqual("Error: Invalid Phonenumber", result.Text);
        }

        //Wallet does not exist - error message
        internal static void VerifyNotExistWallet(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var result = SharedMethods.FindElement(driver, By.CssSelector(".toast-error"), 30);
            Assert.IsTrue(result.Displayed);
        }
        //Add assertion
        //Confirm Report downloaded
        internal static void VerifyreportDownloaded(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var result = SharedMethods.FindElement(driver, By.CssSelector(".toast-success"), 30);
            Assert.IsTrue(result.Displayed);
        }

        internal static void VerifyCustomerDetailsUserId(IWebDriver driver, string senderType)
        {
            if (driver.FindElements(By.Id("tableKycDetails")).Any())
            {
                Assert.AreEqual(senderType, driver.FindElement(By.Id("tdUserIdValue")).Text);
            }
            else
            {
                TestContext.WriteLine("Sender type: " + senderType + " - records Not Found");
            }
        }

        internal static void VerifyCustomerDetails(IWebDriver driver)
        {
            var userid = driver.FindElement(By.Id("tdUserIdLabel")).Text;
            Assert.AreEqual("User Id", userid);

            var firstname = driver.FindElement(By.Id("tdFirstNameLabel")).Text;
            Assert.AreEqual("Firstname", firstname);

            var lastname = driver.FindElement(By.Id("tdLastNameLabel")).Text;
            Assert.AreEqual("Lastname", lastname);

            var IdentificationCardNumber = driver.FindElement(By.Id("tdIdCardNumberLabel")).Text;
            Assert.AreEqual("Identification Card Number", IdentificationCardNumber);

            var nationality = driver.FindElement(By.Id("tdNationalityLabel")).Text;
            Assert.AreEqual("Nationality", nationality);

            var country = driver.FindElement(By.Id("tdCountryLabel")).Text;
            Assert.AreEqual("Country", country);

            var city = driver.FindElement(By.Id("tdCityLabel")).Text;
            Assert.AreEqual("City", city);

            var province = driver.FindElement(By.Id("tdProvinceLabel")).Text;
            Assert.AreEqual("Province", province);

            var StreetAddress1 = driver.FindElement(By.Id("tdStreetAddress1Label")).Text;
            Assert.AreEqual("Street Address 1", StreetAddress1);

            var StreetAddress2 = driver.FindElement(By.Id("tdStreetAddress2Label")).Text;
            Assert.AreEqual("Street Address 2", StreetAddress2);

            var postalcode = driver.FindElement(By.Id("tdPostalCodeLabel")).Text;
            Assert.AreEqual("PostalCode", postalcode);

            var TaxReferenceNumber = driver.FindElement(By.Id("tdTaxReferenceNumberLabel")).Text;
            Assert.AreEqual("Tax Reference Number", TaxReferenceNumber);

            var TaxReferenceType = driver.FindElement(By.Id("tdTaxReferenceTypeLabel")).Text;
            Assert.AreEqual("Tax Reference Type", TaxReferenceType);

        }
    }
}
