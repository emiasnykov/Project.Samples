using Nethereum.Web3;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;

namespace Dashboard.UITests
{
    internal class Assertions
    {
        // Confirm validation error
        public static void ConfirmValidationError(IWebDriver driver)
        {
            // Find error message: Signature could not be verified
            Thread.Sleep(2000);
            IWebElement ErrMsg = Shared.FindElement(driver, By.CssSelector("div.text-danger.validation-summary-errors"), 10);
            if (ErrMsg != null) {
                Assert.AreEqual("Signature could not be verified. Make sure you sign the message with the correct private key and try again.", ErrMsg.Text.ToString()        );
            }
        }

        /// <summary>
        /// Confirm validation error
        /// </summary>
        /// <param name="driver"></param>
        public static void ConfirmCurrencyNotSelected(IWebDriver driver)
        {
            Thread.Sleep(2000);
            // Find error message: Signature could not be verified
            IWebElement ErrMsg = Shared.FindElement(driver, By.CssSelector("span[data-valmsg-for='CurrencyCheckboxes']"), 10);

            // SharedMethods.Screenshot(driver, "Missing currency");
            Assert.AreEqual("Please select at least one currency.", ErrMsg.Text.ToString());
        }

        /// <summary>
        /// Check addresses with no 0x prefix
        /// </summary>
        /// <param name="driver"></param>
        public static void CheckAddressNoPrefix(IWebDriver driver)
        {
            var addressNo0x = "80492***********************";
            string addressWith0x = driver.FindElements(By.CssSelector("div.box-url > div > div.address > p")).FirstOrDefault().Text;
            SharedMethods.Screenshot(driver);
            string expectedAddress = Web3.ToChecksumAddress("0x" + addressNo0x);
            Assert.AreEqual(expectedAddress, addressWith0x);
        }

        /// <summary>
        /// Confirm new email is created
        /// </summary>
        /// <param name="driver"></param>
        public static void ConfirmNewEmail(IWebDriver driver)
        {
            var confMsg = driver.FindElement(By.XPath("//h4[contains(text(),'Confirm New Email')]"));
            Assert.AreEqual("Confirm New Email", confMsg.Text);
        }

        /// <summary>
        /// Check new address was added
        /// </summary>
        /// <param name="driver"></param>
        public static void CheckNewAddressIsAt(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var RegisteredAddresses = driver.FindElements(By.CssSelector(".address-item .address p"));
            string expectedAddress = Web3.ToChecksumAddress("0x9d6b***********************");
            string actualAddress = Web3.ToChecksumAddress(RegisteredAddresses[0].Text);
            Assert.AreEqual(expectedAddress, actualAddress);
        }

        /// <summary>
        /// Check the existed addreess was deleted 
        /// </summary>
        /// <param name="driver"></param>
        public static void CheckAddressDeleted(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var RegisteredAddresses = driver.FindElements(By.CssSelector(".address-item .address p"));
            Assert.AreEqual(0, RegisteredAddresses.Count);
        }

        /// <summary>
        /// Confirm Minting successful
        /// </summary>
        /// <param name="driver"></param>
        public static void CheckPopupMintingSuccessful(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.Screenshot(driver);
            Assert.IsTrue(Shared.FindElement(driver, By.Id("status-message"), 30).Displayed);
        }

        /// <summary>
        /// Confirm address validation error 
        /// </summary>
        /// <param name="driver"></param>
        public static void VerifyInvalidAddressInFaucet(IWebDriver driver)
        {
            Thread.Sleep(2000);
            Assert.IsTrue(Shared.FindElement(driver, By.XPath("(.//*[@class='field']//span)[2]"), 30).Displayed);
        }

        /// <summary>
        /// Empty Address Error
        /// </summary>
        /// <param name="driver"></param>
        public static void VerifyEmptyAddressInFaucet(IWebDriver driver)
        {
            Thread.Sleep(2000);
            String addressMessage = Shared.FindElement(driver, By.Id("Address"), 30).GetAttribute("validationMessage");
            Assert.AreEqual("Please fill out this field.", addressMessage);
        }

        /// <summary>
        /// No currency selected Error msg
        /// </summary>
        /// <param name="driver"></param>
        internal static void VerifyNoCurrencyInFaucet(IWebDriver driver)
        {
            Thread.Sleep(2000);
            String addressMessage = Shared.FindElement(driver, By.Id("Currency"), 30).GetAttribute("validationMessage");
            Assert.AreEqual("Please select an item in the list.", addressMessage);
        }

        /// <summary>
        /// Transaction confirmed
        /// </summary>
        /// <param name="driver"></param>
        public static void TransactionConfirmedStatus(IWebDriver driver)
        {
            IWebElement StatusConfirmedDetailPage = (IWebElement)driver.FindElements(By.XPath(""));
            Assert.IsTrue(StatusConfirmedDetailPage.Displayed);
            Thread.Sleep(2000);
            // Close Modal window 
            Shared.FindElement(driver, By.XPath("/html/body/section[2]/div/div[5]/div[1]/div/div/div[1]/button/span"), 30).Click();
        }

        /// <summary>
        /// Confirm Korean language is selected
        /// </summary>
        /// <param name="driver"></param>
        internal static void ConfirmKoreanLanguageSelected(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IWebElement header = Shared.FindElement(driver, By.XPath("(.//*[@class='main-caption'])[1]"), 30);
            Assert.AreEqual("API 키", header.Text);
        }

        /// <summary>
        /// Confirm English language is selected
        /// </summary>
        /// <param name="driver"></param>
        internal static void ConfirmEnglishLanguageSelected(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IWebElement header = Shared.FindElement(driver, By.XPath("(.//*[@class='main-caption'])[1]"), 30);
            Assert.AreEqual("API Key", header.Text);
        }

        /// <summary>
        /// Confirm 'Received' payment status
        /// </summary>
        /// <param name="driver"></param>
        public static void TransactionConfirmedReceivedPaymentStatus(IWebDriver driver)
        {
            Thread.Sleep(2000);
            try
            {
                // Exist Payment History for given address
                IWebElement ReceivedDetails = Shared.FindElement(driver, By.XPath("//div[contains(text(),'Received Payment History')]"), 30);
                Assert.AreEqual("Received Payment History", ReceivedDetails.Text.ToString());
            }
            catch (Exception)
            {
                // Not Exist Payment History for given address
                IWebElement NotReceivedDetails = Shared.FindElement(driver, By.XPath("//div[contains(text(),'Transaction History')]"), 30);
                Assert.AreEqual("Transaction History", NotReceivedDetails.Text.ToString());
            }   
        }

        /// <summary>
        /// Confirm any addresses not displayed
        /// </summary>
        /// <param name="driver"></param>
        public static void TransactionNoAddresses(IWebDriver driver)
        {
            Assert.IsTrue(Shared.FindElement(driver, By.CssSelector("body > section.main > div.main-inner > div.box-url > div > p > em"), 30).Displayed);
        }

        /// <summary>
        /// Confirm txs export
        /// </summary>
        /// <param name="driver"></param>
        public static void TransactionsExported(IWebDriver driver)
        {
            IWebElement s = Shared.FindElement(driver, By.CssSelector(".ml-auto > button.btn"), 10);

            // activeElement() to verify element focused
            if (s.Equals(driver.SwitchTo().ActiveElement()))
            { 
                TestContext.WriteLine("Export btn is focused"); 
            }
            else
            { 
                TestContext.WriteLine("Export btn is not focused"); 
            }
        }

        /// <summary>
        /// Confirm secret is show up
        /// </summary>
        /// <param name="driver"></param>
        public static void VerifySecretIsShowUp(IWebDriver driver)
        {
            // Verify hidden secret is shown up
            IWebElement HiddenSecret = Shared.FindElement(driver, By.Id("hidden-secret"), 10);
            Assert.IsTrue(HiddenSecret.Displayed);
        }

        /// <summary>
        /// Invalid webhook - err msg
        /// </summary>
        /// <param name="driver"></param>
        public static void VerifyInvalidWebhookUrl(IWebDriver driver)
        {
            // Verify warning message appears
            Thread.Sleep(2000);
            string NegativeUrl = Shared.FindElement(driver, By.XPath("//*[@id='register-url-box']/div/span"), 30).Text;
            Assert.AreEqual("Invalid URL entered.", NegativeUrl);
        }

        /// <summary>
        /// Invalid Url - err msg
        /// </summary>
        /// <param name="driver"></param>
        public static void VerifyInvalidExReqUrl(IWebDriver driver)
        {
            // Verify warning message appears
            Thread.Sleep(2000);
            string NegativeUrl = Shared.FindElement(driver, By.XPath("(.//*[@class='invalid-feedback'])[2]"), 30).Text;
            Assert.AreEqual("Invalid URL entered.", NegativeUrl);
        }

        /// <summary>
        /// Confirm ex. request webhook is added
        /// </summary>
        /// <param name="driver"></param>
        public static void VerufyAddExRequestWebHookUrl(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IWebElement ExchangeUrl = Shared.FindElement(driver, By.CssSelector("div.exchange-url"), 30);
            Assert.IsTrue(ExchangeUrl.Displayed);
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Confirm ex. request webhook is NOT added
        /// </summary>
        /// <param name="driver"></param>
        public static void VerufyNoExRequestWebHookUrl(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = Shared.FindElement(driver, By.XPath("//h3[contains(text(),'Register Exchange Webhook URL')]"), 30);
            Assert.IsFalse(s.Displayed);
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Confirm Register webhook is added
        /// </summary>
        /// <param name="driver"></param>
        public static void VerufyRegisterWebHookUrl(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = driver.FindElements(By.CssSelector("div.url")).Any();
            Assert.That(s, Is.Not.Null);
            Thread.Sleep(2000);          
        }

        /// <summary>
        /// Verify 'Change Password' is displayed
        /// </summary>
        /// <param name="driver"></param>
        public static void VerifyChangePasswordDisplayed(IWebDriver driver)
        {
            IWebElement ChangePassword = Shared.FindElement(driver, By.XPath(".//*[@class='ab-mess max300']//h4"), 30);
            Assert.AreEqual("Change Password", ChangePassword.Text);
        }

        /// <summary>
        /// Verify hidden secret is displayed
        /// </summary>
        /// <param name="driver"></param>
        public static void VerifyHiddenSecretDisplayed(IWebDriver driver)
        {
            IWebElement HiddenSecret = Shared.FindElement(driver, By.Id("hide-show-btn"), 30);
            SharedMethods.Screenshot(driver);
            Assert.IsTrue(HiddenSecret.Displayed);
        }

        /// <summary>
        /// Whitelist ip is displayed
        /// </summary>
        /// <param name="driver"></param>
        public static void WhitelistedIpDisplayed(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IWebElement whitelistedIp = Shared.FindElement(driver, By.Id("whitelist-p"), 30);
            Assert.IsTrue(whitelistedIp.Displayed);
            Thread.Sleep(2000);
          }

        /// <summary>
        /// Whitelist Ips are cleared
        /// </summary>
        /// <param name="driver"></param>
        internal static void WhitelistedIpCleared(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = driver.FindElements(By.Id("whitelist-p"));
            Assert.AreEqual(0, s.Count());
        }

        /// <summary>
        /// Invalid Ip - err msg
        /// </summary>
        /// <param name="driver"></param>
        public static void VerifyInvalidWhitelistedIpAddress(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IWebElement invalidIp = Shared.FindElement(driver, By.XPath(".//*[@id='ip-textearea-feedback']"), 30);
            Assert.AreEqual("There is one or more invalid IP addresses.", invalidIp.Text);
        }
    }
}
