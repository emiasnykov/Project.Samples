using OpenQA.Selenium;
using System.Threading;

namespace CSTool.UITests.Pages
{
    class IPDecryptPage
    {

        //Encrypted IP field
        public static IWebElement EnterEncryptedIP(IWebDriver driver)
        {
            var s = driver.FindElement(By.Id("EncryptedIP"));
            return s;
        }

        //Decrypt button
        public static IWebElement DecryptBtn(IWebDriver driver)
        {
            Thread.Sleep(2000); 
            var s = driver.FindElement(By.Id("DecryptSubmitButton"));
            return s;
        }

    }
}
