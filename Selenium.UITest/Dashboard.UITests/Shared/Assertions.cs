using Nethereum.Web3;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;

namespace Dashboard.UITests
{
    class Assertions
    {
        //Confirm validation error
        public static void ConfirmValidationError(IWebDriver driver)
        {
            //Find error message: Signature could not be verified
            Thread.Sleep(2000);
            //SharedMethods.WaitForElementToAppear(driver, By.CssSelector("div.text-danger.validation-summary-errors"));
            IWebElement ErrMsg = Shared.FindElement(driver, By.CssSelector("div.text-danger.validation-summary-errors"), 10);
            if (ErrMsg != null) {
                Assert.AreEqual("Signature could not be verified. Make sure you sign the message with the correct private key and try again.", ErrMsg.Text.ToString()        );
            }
        }


        //Confirm validation error
        public static void ConfirmCurrencyNotSelected(IWebDriver driver)
        {
            Thread.Sleep(2000);
            //Find error message: Signature could not be verified
            IWebElement ErrMsg = Shared.FindElement(driver, By.CssSelector("span[data-valmsg-for='CurrencyCheckboxes']"), 10);
            //SharedMethods.Screenshot(driver, "Missing currency");
            Assert.AreEqual("Please select at least one currency.", ErrMsg.Text.ToString());
        }


        ////Check active addresses
        //public static void CheckActiveAddresses(IWebDriver driver)
        //{
        //    IWebElement active = driver.FindElement(By.TagName(".currency-active"));
        //    if (active != null)
        //    {
        //        Assert.IsTrue(driver.FindElement(By.TagName(".currency-active")).Displayed);
        //    }
        //    else if (active == null)
        //    {
        //        String elementColor = driver.FindElement(By.TagName(".currency")).GetCssValue("font-color");
        //        Assert.AreEqual("#CACCCF", elementColor);
        //    }
        //}

        //Check addresses with no 0x prefix
        public static void CheckAddressNoPrefix(IWebDriver driver)
        {
            var addressNo0x = "80492a16eb70f7b4db8f89ed02bd88e38ecce636";
            string addressWith0x = driver.FindElements(By.CssSelector("div.box-url > div > div.address > p")).FirstOrDefault().Text;
            SharedMethods.Screenshot(driver);
            string expectedAddress = Web3.ToChecksumAddress("0x" + addressNo0x);
            Assert.AreEqual(expectedAddress, addressWith0x);
        }

        //Confirm new email is created
        public static void ConfirmNewEmail(IWebDriver driver)
        {
            var confMsg = driver.FindElement(By.XPath("//h4[contains(text(),'Confirm New Email')]"));
            Assert.AreEqual("Confirm New Email", confMsg.Text);
        }

        //Check new address was added
        public static void CheckNewAddressIsAt(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var RegisteredAddresses = driver.FindElements(By.CssSelector(".address-item .address p"));
            string expectedAddress = Nethereum.Web3.Web3.ToChecksumAddress("0x9d6bC9763008AD1F7619a3498EfFE9Ec671b276D");
            string actualAddress = Web3.ToChecksumAddress(RegisteredAddresses[0].Text);
            Assert.AreEqual(expectedAddress, actualAddress);
        }

        //Check the existed addreess was deleted 
        public static void CheckAddressDeleted(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var RegisteredAddresses = driver.FindElements(By.CssSelector(".address-item .address p"));
            Assert.AreEqual(0, RegisteredAddresses.Count);
        }

        //Confirm Minting successful
        public static void CheckPopupMintingSuccessful(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Shared.Screenshot(driver);
            Assert.IsTrue(Shared.FindElement(driver, By.Id("status-message"), 30).Displayed);
        }

        //Confirm address validation error 
        public static void VerifyInvalidAddressInFaucet(IWebDriver driver)
        {
            Thread.Sleep(2000);
            Assert.IsTrue(Shared.FindElement(driver, By.XPath("(.//*[@class='field']//span)[2]"), 30).Displayed);
        }

        //Empty Address Error
        public static void VerifyEmptyAddressInFaucet(IWebDriver driver)
        {
            Thread.Sleep(2000);
            String addressMessage = Shared.FindElement(driver, By.Id("Address"), 30).GetAttribute("validationMessage");
            Assert.AreEqual("Please fill out this field.", addressMessage);
        }

        //No currency selected Error msg
        internal static void VerifyNoCurrencyInFaucet(IWebDriver driver)
        {
            Thread.Sleep(2000);
            String addressMessage = Shared.FindElement(driver, By.Id("Currency"), 30).GetAttribute("validationMessage");
            Assert.AreEqual("Please select an item in the list.", addressMessage);
        }

        //Transaction confirmed 
        public static void TransactionConfirmedStatus(IWebDriver driver)
        {
            //  IWebElement StatusConfirmedDetailPage = driver.FindElements(By.XPath("/html/body/section[2]/div/div[5]/a/div/div[3]/div"));
            Thread.Sleep(2000);
            //     Assert.IsTrue(StatusConfirmedDetailPage.Displayed);
            Thread.Sleep(2000);
            //Close Modal window 
            Shared.FindElement(driver, By.XPath("/html/body/section[2]/div/div[5]/div[1]/div/div/div[1]/button/span"), 30).Click();
            // Shared.WaitUntilPreloadGone(Driver.Instance);
        }

        //Confirm Korean language is selected
        internal static void ConfirmKoreanLanguageSelected(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IWebElement header = Shared.FindElement(driver, By.XPath("(.//*[@class='main-caption'])[1]"), 30);
            Assert.AreEqual("API 키", header.Text);
        }

        //Confirm English language is selected
        internal static void ConfirmEnglishLanguageSelected(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IWebElement header = Shared.FindElement(driver, By.XPath("(.//*[@class='main-caption'])[1]"), 30);
            Assert.AreEqual("API Key", header.Text);
        }

        //Confirm 'Received' payment status
        public static void TransactionConfirmedReceivedPaymentStatus(IWebDriver driver)
        {
            Thread.Sleep(2000);
            try
            {
                //Exist Payment History for given address
                IWebElement ReceivedDetails = Shared.FindElement(driver, By.XPath("//div[contains(text(),'Received Payment History')]"), 30);
                Assert.AreEqual("Received Payment History", ReceivedDetails.Text.ToString());
            }
            catch (Exception)
            {
                //Not Exist Payment History for given address
                IWebElement NotReceivedDetails = Shared.FindElement(driver, By.XPath("//div[contains(text(),'Transaction History')]"), 30);
                Assert.AreEqual("Transaction History", NotReceivedDetails.Text.ToString());
            }   
        } 

        //Confirm any addresses not displayed
        public static void TransactionNoAddresses(IWebDriver driver)
        {
            Assert.IsTrue(Shared.FindElement(driver, By.CssSelector("body > section.main > div.main-inner > div.box-url > div > p > em"), 30).Displayed);
        }

        //Confirm txs export
        public static void TransactionsExported(IWebDriver driver)
        {
            IWebElement l = Shared.FindElement(driver, By.CssSelector(".ml-auto > button.btn"), 10);
            //activeElement() to verify element focused
            if (l.Equals(driver.SwitchTo().ActiveElement()))
            { TestContext.WriteLine("Export btn is focused"); }
            else
            { TestContext.WriteLine("Export btn is not focused"); }
        }

        //Confirm secret is show up
        public static void VerifySecretIsShowUp(IWebDriver driver)
        {
            //Verify hidden secret is shown up
            IWebElement HiddenSecret = Shared.FindElement(driver, By.Id("hidden-secret"), 10);
            Assert.IsTrue(HiddenSecret.Displayed);
        }

        //Invalid webhook - err msg
        public static void VerifyInvalidWebhookUrl(IWebDriver driver)
        {
            //Verify warning message appears
            Thread.Sleep(2000);
            string NegativeUrl = Shared.FindElement(driver, By.XPath("//*[@id='register-url-box']/div/span"), 30).Text;
            Assert.AreEqual("Invalid URL entered.", NegativeUrl);
        }

        //Invalid Url - err msg
        public static void VerifyInvalidExReqUrl(IWebDriver driver)
        {
            //Verify warning message appears
            Thread.Sleep(2000);
            string NegativeUrl = Shared.FindElement(driver, By.XPath("(.//*[@class='invalid-feedback'])[2]"), 30).Text;
            Assert.AreEqual("Invalid URL entered.", NegativeUrl);
        }

        //Confirm ex. request webhook is added
        public static void VerufyAddExRequestWebHookUrl(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IWebElement ExchangeUrl = Shared.FindElement(driver, By.CssSelector("div.exchange-url"), 30);
            Assert.IsTrue(ExchangeUrl.Displayed);
            Thread.Sleep(2000);
        }

        //Confirm ex. request webhook is NOT added
        public static void VerufyNoExRequestWebHookUrl(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = Shared.FindElement(driver, By.XPath("//h3[contains(text(),'Register Exchange Webhook URL')]"), 30);
            Assert.IsFalse(s.Displayed);
            Thread.Sleep(2000);
        }

        //Confirm Register webhook is added
        public static void VerufyRegisterWebHookUrl(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = driver.FindElements(By.CssSelector("div.url")).Any();
            Assert.That(s, Is.Not.Null);
            Thread.Sleep(2000);          
        }

        //Verify 'Change Password' is displayed
        public static void VerifyChangePasswordDisplayed(IWebDriver driver)
        {
            IWebElement ChangePassword = Shared.FindElement(driver, By.XPath(".//*[@class='ab-mess max300']//h4"), 30);
            Assert.AreEqual("Change Password", ChangePassword.Text);
        }

        //Verify hidden secret is displayed
        public static void VerifyHiddenSecretDisplayed(IWebDriver driver)
        {
            IWebElement HiddenSecret = Shared.FindElement(driver, By.Id("hide-show-btn"), 30);
            SharedMethods.Screenshot(driver);
            Assert.IsTrue(HiddenSecret.Displayed);
        }

        //Whitelist ip is displayed
        public static void WhitelistedIpDisplayed(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IWebElement whitelistedIp = Shared.FindElement(driver, By.Id("whitelist-p"), 30);
            Assert.IsTrue(whitelistedIp.Displayed);
            Thread.Sleep(2000);
          }

        //Whitelist Ips are cleared
        internal static void WhitelistedIpCleared(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = driver.FindElements(By.Id("whitelist-p"));
            Assert.AreEqual(0, s.Count());
        }

        //Invalid Ip - err msg
        public static void VerifyInvalidWhitelistedIpAddress(IWebDriver driver)
        {
            Thread.Sleep(2000);
            IWebElement invalidIp = Shared.FindElement(driver, By.XPath(".//*[@id='ip-textearea-feedback']"), 30);
            Assert.AreEqual("There is one or more invalid IP addresses.", invalidIp.Text);
        }
    }
}
