using Dashboard.UITests.Enum;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;

namespace Dashboard.UITests.Pages
{
    class AddressesPage
    {
        //Remove all addresses
        public static void RemoveAnyAddresses(IWebDriver driver, bool removeAddresses)
        {
            if (removeAddresses == true)
            {
                foreach (var address in driver.FindElements(By.CssSelector(".close .icon-close")))
                {
                    Thread.Sleep(2000);
                    //Click Remove Account Address button
                    driver.FindElements(By.CssSelector("div.box-url > div > span.close")).FirstOrDefault().Click();
                    Thread.Sleep(2000);
                    Shared.FindElement(driver, By.Id("buttonModalRemove"), 10).Click();
                    Thread.Sleep(2000);
                }
            }
        }

        //Fill in addresses form
        public static void AddressesForm(IWebDriver driver, string url, AddressFormEnum addressFormEnum)
        {

            //Switch to proper test case
            switch (addressFormEnum)
            {
                //Valid values
                case AddressFormEnum.ValidValues:
                    Thread.Sleep(5000);
                    SharedMethods.WaitUntilPreloadAddressPage(driver, url);
                    Shared.FindElement(driver, By.Id("buttonRegisterNewAddress"), 30).Click();
                    SharedMethods.WaitForElementToAppear(driver, By.Id("Address"));
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("0x80492a16eb70f7b4db8f89ed02bd88e38ecce636");
                    Shared.FindElement(driver, By.Id("Message"), 10).SendKeys("Gluwa");
                    Shared.FindElement(driver, By.Id("Signature"), 10).SendKeys("0xe3c0fdc4e06c8dd385b10c3dd0e40c5919c3985e51ebfe32b0e76fede29bc2ac50e5031b8a95338f3102b56f242bc921d2323c6044dc94bf018bc62e1b18413c1b");
                    Thread.Sleep(2000);
                    break;


                //Valid values for sNGNG and sKRWCG
                case AddressFormEnum.ValidValuesAltAdr:
                    Thread.Sleep(2000);
                    SharedMethods.WaitUntilPreloadAddressPage(driver, url);
                    Shared.FindElement(driver, By.Id("buttonRegisterNewAddress"), 30).Click();
                    Thread.Sleep(2000);
                    SharedMethods.WaitForElementToAppear(driver, By.Id("Address"));
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("0xf04349b4a760f5aed02131e0daa9bb99a1d1d1e5");
                    Shared.FindElement(driver, By.Id("Message"), 10).SendKeys("gluwa");
                    Shared.FindElement(driver, By.Id("Signature"), 10).SendKeys("0x304b76f7ae4cc6d76a81ea41b07b9e6d62a2fb04d104d52e3c6ae3bc27b67bb55567e4b53f692799027bb89f0cf8414d71b447c0c612026e7d963bc37ca1d9971c");
                    Thread.Sleep(2000);
                    break;


                //Valid values with no 0x prefix address
                case AddressFormEnum.ValidAddressNoPrefix:
                    Thread.Sleep(2000);
                    SharedMethods.WaitUntilPreloadAddressPage(driver, url);
                    Shared.FindElement(driver, By.Id("buttonRegisterNewAddress"), 50).Click();
                    Thread.Sleep(2000);
                    SharedMethods.WaitForElementToAppear(driver, By.Id("Address"));
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("80492a16eb70f7b4db8f89ed02bd88e38ecce636");
                    Shared.FindElement(driver, By.Id("Message"), 10).SendKeys("Gluwa");
                    Shared.FindElement(driver, By.Id("Signature"), 10).SendKeys("0xe3c0fdc4e06c8dd385b10c3dd0e40c5919c3985e51ebfe32b0e76fede29bc2ac50e5031b8a95338f3102b56f242bc921d2323c6044dc94bf018bc62e1b18413c1b");
                    Thread.Sleep(2000);
                    break;

                //Invalid signature
                case AddressFormEnum.InvalidSign:
                    Thread.Sleep(2000);
                    SharedMethods.WaitUntilPreloadAddressPage(driver, url);
                    Shared.FindElement(driver, By.Id("buttonRegisterNewAddress"), 50).Click();
                    Thread.Sleep(2000);
                    SharedMethods.WaitForElementToAppear(driver, By.Id("Address"));
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("0x80492a16eb70f7b4db8f89ed02bd88e38ecce637");
                    Shared.FindElement(driver, By.Id("Message"), 10).SendKeys("Gluwa");
                    Shared.FindElement(driver, By.Id("Signature"), 10).SendKeys("H5NVNUl1TfEWNC3Olk2PVaTEld4ORF1dZeFtVvAbra0hKxSXRfaUc6gi1UoYgYQo5YWYnftt1+BlLGFw81B/lac=");
                    Thread.Sleep(2000);
                    break;

                //Invalid address
                case AddressFormEnum.InvalidAddress:
                    Thread.Sleep(2000);
                    SharedMethods.WaitUntilPreloadGone(driver, url);
                    Shared.FindElement(driver, By.Id("buttonRegisterNewAddress"), 50).Click();
                    Thread.Sleep(2000);
                    SharedMethods.WaitForElementToAppear(driver, By.Id("Address"));
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("0x80492a16eb70f7b4db8f89ed02bd88e38ecce637");
                    Shared.FindElement(driver, By.Id("Message"), 10).SendKeys("Gluwa");
                    Shared.FindElement(driver, By.Id("Signature"), 10).SendKeys("0xe3c0fdc4e06c8dd385b10c3dd0e40c5919c3985e51ebfe32b0e76fede29bc2ac50e5031b8a95338f3102b56f242bc921d2323c6044dc94bf018bc62e1b18413c1b");
                    Thread.Sleep(2000);
                    break;

                //Invalid message
                case AddressFormEnum.InvalidMsg:
                    Thread.Sleep(2000);
                    SharedMethods.WaitUntilPreloadGone(driver, url);
                    Shared.FindElement(driver, By.Id("buttonRegisterNewAddress"), 50).Click();
                    Thread.Sleep(2000);
                    SharedMethods.WaitForElementToAppear(driver, By.Id("Address"));
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("0x80492a16eb70f7b4db8f89ed02bd88e38ecce636");
                    Shared.FindElement(driver, By.Id("Message"), 10).SendKeys("gluwa");
                    Shared.FindElement(driver, By.Id("Signature"), 10).SendKeys("0xe3c0fdc4e06c8dd385b10c3dd0e40c5919c3985e51ebfe32b0e76fede29bc2ac50e5031b8a95338f3102b56f242bc921d2323c6044dc94bf018bc62e1b18413c1b");
                    Thread.Sleep(2000);
                    break;
            }

        }

        //Select currency
        public static void SelectCurrency(IWebDriver driver, string currencies)
        {
            //Click Currency Checkbox
            string[] currenciesArray = currencies.Split('/');
            foreach (string currency in currenciesArray)
            {
                Shared.FindElement(driver, By.Id(currency), 10).SendKeys(Keys.Space);
            }
        }

        //Add new address by submitting form
        public static void RegisterAddress(IWebDriver driver)
        {
           Shared.FindElement(driver, By.Id("buttonRegister"), 10).Click();
           Thread.Sleep(5000);
        }

    }
}
