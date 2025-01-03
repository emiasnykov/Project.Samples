using OpenQA.Selenium;
using System.Threading;

namespace CSTool.UITests.Pages
{
    class IPEncryptPage
    {

        //IP Address field
        public static IWebElement EnterIPAddress(IWebDriver driver)
        {
            var s = driver.FindElement(By.Id("IP"));
            return s;
        }

        //Encrypt button
        public static IWebElement EncryptBtn(IWebDriver driver)
        {
            Thread.Sleep(2000);
            var s = driver.FindElement(By.Id("EncryptSubmitButton"));
            return s;
        }

    }
}
