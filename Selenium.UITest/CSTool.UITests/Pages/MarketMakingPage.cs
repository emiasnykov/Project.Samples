using OpenQA.Selenium;

namespace CSTool.UITests.Pages
{
    class MarketMakingPage
    {
        //Market Making Status Button
        public static IWebElement MarketMakingStatusBtn(IWebDriver driver)
        {
            var s  = driver.FindElement(By.CssSelector("#buttonMarketMakingStatus"));
            return s;
        }

        //Market Making Status header
        public static IWebElement MarketMakingStatusHeader(IWebDriver driver)
        {
            var s = driver.FindElement(By.CssSelector("#headingMarketMakingStatus"));
            return s;
        }

        //Charmbit Status Button
        public static IWebElement CharmbitStatusBtn(IWebDriver driver)
        {
            var s = driver.FindElement(By.CssSelector("#buttonMarketMakingStatus"));
            return s;
        }

        //Charmbit Status header
        public static IWebElement CharmbitStatusHeader(IWebDriver driver)
        {
            var s = driver.FindElement(By.CssSelector("#headingMarketMakingStatus"));
            return s;
        }

    }
}
