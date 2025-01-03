using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CSTool.UITests.Pages
{
    class UserInfoPage
    {
        //Search by UserName or Email text field
        internal static void SearchByUserNameOrEmail(IWebDriver driver, string value)
        {
            SharedMethods.FindElement(driver, By.Id("NameOrEmail"), 10).SendKeys(value);
        }

        //Search by UserName or Email button
        internal static IWebElement SearchBtn(IWebDriver driver)
        {
            var s = driver.FindElement(By.Id("NameOrEmailSubmitButton"));
            return s;
        }

        //Search by Phone No. button
        internal static IWebElement SearchByPhoneBtn(IWebDriver driver)
        {
            var s = driver.FindElement(By.Id("PhoneNumberSubmitButton"));
            return s;
        }

        //Search by Phone text field
        internal static void SearchByPhoneNo(IWebDriver driver, string phoneNo, string areaCode)
        {
            var country = driver.FindElement(By.Id("Country"));
            var selectElement = new SelectElement(country);
            selectElement.SelectByValue(areaCode);
            driver.FindElement(By.Id("PhoneNumber")).SendKeys(phoneNo);
        }

        //Search by Wallet Address text field
        internal static void InvestmentAccountWalletAddress(IWebDriver driver, string walletNo)
        {
             driver.FindElement(By.Id("WalletAddress")).SendKeys(walletNo);

        }
        //Search by Wallet Address button
        internal static IWebElement SearchByWalletAddress(IWebDriver driver)
        {
            var s = driver.FindElement(By.Id("WalletAddressSubmitButton"));
            return s;
        }
    }
}
