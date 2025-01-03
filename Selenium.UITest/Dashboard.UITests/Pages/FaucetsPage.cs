using Dashboard.UITests.Enum;
using OpenQA.Selenium;
using Shared.Driver;
using System.Linq;
using System.Threading;

namespace Dashboard.UITests.Pages
{
  class FaucetsPage
    {
        //public static void FaucetsForm(IWebDriver driver, FaucetsFormEnum faucetsFormEnum)
        //{
        //    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("0xBBb8bbAF43fE8b9E5572B1860d5c94aC7ed87Bb9");
        //    Thread.Sleep(2000);
        //}

        //Select currency
        public static void SelectCurrency(IWebDriver driver, string currency)
        {
            //Click Currency Dropdown
            Shared.FindElement(driver, By.Id("Currency"), 10).Click();

            //Switch to proper test case
            switch (currency)
            {
                //Valid values
                case "KRWG":
                    Shared.FindElement(driver, By.CssSelector("#Currency > option:nth-child(3)"), 10).Click(); //Select currency
                    Thread.Sleep(2000);
                    break;

                //Valid values
                case "USDG":
                    Shared.FindElement(driver, By.CssSelector("#Currency > option:nth-child(2)"), 10).Click(); //Select currency
                    Thread.Sleep(2000);
                    break;

                //Valid values
                case "NGNG":
                    Shared.FindElement(driver, By.CssSelector("#Currency > option:nth-child(2)"), 10).Click(); //Select currency
                    Thread.Sleep(2000);
                    break;

                //Valid values
                case "sUSDCG":
                    Shared.FindElement(driver, By.CssSelector("#Currency > option:nth-child(5)"), 10).Click(); //Select currency
                    Thread.Sleep(2000);
                    break;

                //Valid values
                case "sNGNG":
                    Shared.FindElement(driver, By.CssSelector("#Currency > option:nth-child(3)"), 10).Click(); //Select currency
                    Thread.Sleep(2000);
                    break;

                //Valid values
                case "sKRWCG":
                    Shared.FindElement(driver, By.CssSelector("#Currency > option:nth-child(7)"), 10).Click(); //Select currency
                    Thread.Sleep(2000);
                    break;
            }
        }

        public static void SubmitFaucet(IWebDriver driver)
        {
            Thread.Sleep(2000);
            Shared.FindElement(driver, By.Id("buttonSubmit"), 10).Click();
            Thread.Sleep(2000);
        }

        //Fill in FaucetsForm
        public static void FaucetsForm(IWebDriver driver, FaucetsFormEnum faucetsFormEnum)
        {

            //Switch to proper test case
            switch (faucetsFormEnum)
            {
                //Valid values
                case FaucetsFormEnum.ValidAddress:
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("0xBBb8bbAF43fE8b9E5572B1860d5c94aC7ed87Bb9");
                    Thread.Sleep(2000);
                    break;

                //Valid values with invalid address
                case FaucetsFormEnum.InvalidAddress:
                    Thread.Sleep(2000);
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("1QFCM6czj89szVTbUg7ZxzEz5tSJpzhCXR");
                    Thread.Sleep(2000);
                    break;

                //Empty address
                case FaucetsFormEnum.EmptyAddress:
                    Shared.FindElement(driver, By.Id("Address"), 10).SendKeys("");
                    Thread.Sleep(2000);
                    break;
            }
        }
        //        public static void FaucetsForm()
        //        {
        //            Driver.Instance.FindElement(By.Id("Address")).SendKeys("0xBBb8bbAF43fE8b9E5572B1860d5c94aC7ed87Bb9");
        //            Thread.Sleep(2000);
        //        }

        //        //Select currency
        //        public static void SelectCurrency(string currency)
        //        {
        //            //Click Currency Dropdown
        //            Driver.Instance.FindElement(By.Id("Currency")).Click();

        //            //Switch to proper test case
        //            switch (currency)
        //            {
        //                //Valid values
        //                case "KRWG":
        //                    Driver.Instance.FindElement(By.CssSelector("#Currency > option:nth-child(3)")).Click(); //Select currency
        //                    Thread.Sleep(2000);
        //                    break;

        //                //Valid values
        //                case "USDG":
        //                    Driver.Instance.FindElement(By.CssSelector("#Currency > option:nth-child(2)")).Click(); //Select currency
        //                    Thread.Sleep(2000);
        //                    break;

        //                //Valid values
        //                case "NGNG":
        //                    Driver.Instance.FindElement(By.CssSelector("#Currency > option:nth-child(4)")).Click(); //Select currency
        //                    Thread.Sleep(2000);
        //                    break;

        //                //Valid values
        //                case "sUSDCG":
        //                    Driver.Instance.FindElement(By.CssSelector("#Currency > option:nth-child(5)")).Click(); //Select currency
        //                    Thread.Sleep(2000);
        //                    break;

        //                //Valid values
        //                case "sNGNG":
        //                    Driver.Instance.FindElement(By.CssSelector("#Currency > option:nth-child(5)")).Click(); //Select currency
        //                    Thread.Sleep(2000);
        //                    break;
        //            }
        //        }

        //        public static void SubmitFaucet(IWebDriver)
        //        {
        //            Thread.Sleep(2000);
        //            driver.FindElement(By.Id("buttonSubmit")).Click();
        //            SharedMethods.Screenshot(driver);
        //        }

        //        //Fill in FaucetsForm
        //        public static void FaucetsForm(FaucetsFormEnum faucetsFormEnum)
        //        {

        //            //Switch to proper test case
        //            switch (faucetsFormEnum)
        //            {
        //                //Valid values
        //                case FaucetsFormEnum.ValidAddress:
        //                    Driver.Instance.FindElement(By.Id("Address")).SendKeys("0xBBb8bbAF43fE8b9E5572B1860d5c94aC7ed87Bb9");
        //                    Thread.Sleep(2000);
        //                    break;

        //                //Valid values with invalid address
        //                case FaucetsFormEnum.InvalidAddress:
        //                    Thread.Sleep(2000);
        //                    Driver.Instance.FindElement(By.Id("Address")).SendKeys("1QFCM6czj89szVTbUg7ZxzEz5tSJpzhCXR");
        //                    Thread.Sleep(2000);
        //                    break;

        //                //Empty address
        //                case FaucetsFormEnum.EmptyAddress:
        //                    Driver.Instance.FindElement(By.Id("Address")).SendKeys("");
        //                    Thread.Sleep(2000);
        //                    break;
        //            }
        //        }
    }
}