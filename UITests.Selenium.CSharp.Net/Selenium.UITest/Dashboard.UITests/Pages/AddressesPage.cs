using Dashboard.UITests.Enum;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;

namespace Dashboard.UITests.Pages
{
    internal class AddressesPage
    {
        /// <summary>
        /// Remove all addresses
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="removeAddresses"></param>
        public static void RemoveAnyAddresses(IWebDriver driver, bool removeAddresses)
        {
            if (removeAddresses == true)
            {
                foreach (var address in driver.FindElements(By.CssSelector(".close .icon-close")))
                {
                    Thread.Sleep(1000);
                    driver.FindElements(By.CssSelector("div.box-url > div > span.close")).FirstOrDefault().Click();
                    Thread.Sleep(1000);
                    Shared.FindElement(driver, By.Id("buttonModalRemove"), 10).Click();
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// Fill in addresses form
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        /// <param name="addressFormEnum"></param>
        public static void AddressesForm(IWebDriver driver, string url, AddressFormEnum addressFormEnum)
        {
            // Switch to proper test case
            switch (addressFormEnum)
            {
                // Valid values
                case AddressFormEnum.ValidValues:
                    Thread.Sleep(1000);
                    SharedMethods.WaitUntilPreloadAddressPage(driver, url);
                    Shared.FindElement(driver, By.Id("buttonRegisterNewAddress"), 30).Click();
                    SharedMethods.WaitForElementToAppear(driver, By.Id("Address"));
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("0x80492**************************");
                    Shared.FindElement(driver, By.Id("Message"), 10).SendKeys("Gluwa");
                    Shared.FindElement(driver, By.Id("Signature"), 10).SendKeys("0xe3c0fdc**********************");
                    Thread.Sleep(1000);
                    break;


                // Valid values for sNGNG and sKRWCG
                case AddressFormEnum.ValidValuesAltAdr:
                    Thread.Sleep(1000);
                    SharedMethods.WaitUntilPreloadAddressPage(driver, url);
                    Shared.FindElement(driver, By.Id("buttonRegisterNewAddress"), 30).Click();
                    Thread.Sleep(1000);
                    SharedMethods.WaitForElementToAppear(driver, By.Id("Address"));
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("0xf04349***************************");
                    Shared.FindElement(driver, By.Id("Message"), 10).SendKeys("gluwa");
                    Shared.FindElement(driver, By.Id("Signature"), 10).SendKeys("0x304b***************************");
                    Thread.Sleep(1000);
                    break;


                // Valid values with no 0x prefix address
                case AddressFormEnum.ValidAddressNoPrefix:
                    Thread.Sleep(1000);
                    SharedMethods.WaitUntilPreloadAddressPage(driver, url);
                    Shared.FindElement(driver, By.Id("buttonRegisterNewAddress"), 50).Click();
                    Thread.Sleep(1000);
                    SharedMethods.WaitForElementToAppear(driver, By.Id("Address"));
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("80492a1*****************************");
                    Shared.FindElement(driver, By.Id("Message"), 10).SendKeys("Gluwa");
                    Shared.FindElement(driver, By.Id("Signature"), 10).SendKeys("0xe3c0fdc4************************");
                    Thread.Sleep(1000);
                    break;

                // Invalid signature
                case AddressFormEnum.InvalidSign:
                    Thread.Sleep(1000);
                    SharedMethods.WaitUntilPreloadAddressPage(driver, url);
                    Shared.FindElement(driver, By.Id("buttonRegisterNewAddress"), 50).Click();
                    Thread.Sleep(1000);
                    SharedMethods.WaitForElementToAppear(driver, By.Id("Address"));
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("0x80492a*****************************");
                    Shared.FindElement(driver, By.Id("Message"), 10).SendKeys("Gluwa");
                    Shared.FindElement(driver, By.Id("Signature"), 10).SendKeys("H5NVNUl1T***************************");
                    Thread.Sleep(1000);
                    break;

                // Invalid address
                case AddressFormEnum.InvalidAddress:
                    Thread.Sleep(1000);
                    SharedMethods.WaitUntilPreloadGone(driver, url);
                    Shared.FindElement(driver, By.Id("buttonRegisterNewAddress"), 50).Click();
                    Thread.Sleep(1000);
                    SharedMethods.WaitForElementToAppear(driver, By.Id("Address"));
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("0x80492*******************************");
                    Shared.FindElement(driver, By.Id("Message"), 10).SendKeys("Gluwa");
                    Shared.FindElement(driver, By.Id("Signature"), 10).SendKeys("0xe3c0fdc****************************");
                    Thread.Sleep(1000);
                    break;

                // Invalid message
                case AddressFormEnum.InvalidMsg:
                    Thread.Sleep(1000);
                    SharedMethods.WaitUntilPreloadGone(driver, url);
                    Shared.FindElement(driver, By.Id("buttonRegisterNewAddress"), 50).Click();
                    Thread.Sleep(1000);
                    SharedMethods.WaitForElementToAppear(driver, By.Id("Address"));
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("0x80492a1********************************");
                    Shared.FindElement(driver, By.Id("Message"), 10).SendKeys("gluwa");
                    Shared.FindElement(driver, By.Id("Signature"), 10).SendKeys("0xe3c0fdc4******************************");
                    Thread.Sleep(1000);
                    break;
            }

        }

        /// <summary>
        /// Select currencies
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="currencies"></param>
        public static void SelectCurrency(IWebDriver driver, string currencies)
        {
            // Click Currency Checkbox
            string[] currenciesArray = currencies.Split('/');
            foreach (string currency in currenciesArray)
            {
                Shared.FindElement(driver, By.Id(currency), 10).SendKeys(Keys.Space);
            }
        }

        /// <summary>
        /// Submitting form
        /// </summary>
        /// <param name="driver"></param>
        public static void RegisterAddress(IWebDriver driver)
        {
           Shared.FindElement(driver, By.Id("buttonRegister"), 10).Click();
           Thread.Sleep(2000);
        }
    }
}
