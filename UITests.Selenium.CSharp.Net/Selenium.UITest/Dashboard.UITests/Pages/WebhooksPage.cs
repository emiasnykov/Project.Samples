using Dashboard.UITests.Enum;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;

namespace Dashboard.UITests.Pages
{
    /// <summary>
    /// Webhooks Page functions
    /// </summary>
    internal class WebhooksPage
    {
        /// <summary>
        /// Show Secret Btn
        /// </summary>
        /// <param name="driver"></param>
        public static void ClickSecretShowBtn(IWebDriver driver)
        {
            Shared.FindElement(driver, By.Id("buttonWebhookSecretShow"), 10).Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Get secret key
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string GetHiddenSecret(IWebDriver driver)
        {
            string secret = Shared.FindElement(driver, By.Id("hidden-secret"), 10).Text.ToString();
            Thread.Sleep(2000);
            return secret;
        }

        /// <summary>
        /// Reset webhook 
        /// </summary>
        /// <param name="driver"></param>
        public static void WebhooksReset(IWebDriver driver)
        {
            Shared.FindElement(driver, By.Id("buttonReset"), 10).Click();
            Shared.FindElement(driver, By.Id("btn-reset-secret"), 10).Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Add webhook url
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="WebhookUrlEnum"></param>
        public static void AddWebhookUrl(IWebDriver driver, RegWebhookUrlEnum WebhookUrlEnum)
        {
            // Switch to proper test case
            switch (WebhookUrlEnum)
            {
                // Valid Url
                case RegWebhookUrlEnum.ValidUrl:
                    Shared.FindElement(driver, By.Id("btn-register-webhook-url"), 10).Click();
                    Shared.FindElement(driver, By.Id("webhook-url-input"), 10).SendKeys("https://www.gluwa.com");
                    Thread.Sleep(2000);
                break;

                // Invalid Url
                case RegWebhookUrlEnum.InvalidUrl:
                    Shared.FindElement(driver, By.Id("btn-register-webhook-url"), 10).Click();
                    Shared.FindElement(driver, By.Id("webhook-url-input"), 10).SendKeys("gluwa");
                    Thread.Sleep(2000);
                break;
            }
        }

        /// <summary>
        /// Add exchange webhook url
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="WebhookUrlEnum"></param>
        public static void AddExRequestWebhookUrl(IWebDriver driver, RegWebhookUrlEnum WebhookUrlEnum)
        {
            // Switch to proper test case
            switch (WebhookUrlEnum)
            {
                // Valid Url
                case RegWebhookUrlEnum.ValidUrl:
                    Shared.FindElement(driver, By.Id("btn-register-exchange-url"), 10).Click();
                    Thread.Sleep(2000);
                    Shared.FindElement(driver, By.Id("exchange-url-input"), 10).SendKeys("https://wwww.gluwa.com");
                    Thread.Sleep(2000);
                break;

                // Invalid Url
                case RegWebhookUrlEnum.InvalidUrl:
                    Shared.FindElement(driver, By.Id("btn-register-exchange-url"), 10).Click();
                    Thread.Sleep(2000);
                    Shared.FindElement(driver, By.Id("exchange-url-input"), 10).SendKeys("gluwa");
                    Thread.Sleep(2000);
                break;
            }
        }

        /// <summary>
        /// Add webhook URLs
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        /// <param name="i"></param>
        public static void AddWebhooksUrls (IWebDriver driver, string url, int i)
        {
            // driver.Navigate().GoToUrl(SharedMethods.dashboardURL + "/Webhook/");
            // Hack because Firefox is weird about this button being near the footer
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("return document.getElementsByClassName('aut-footer')[0].remove();");
            SharedMethods.WaitUntilPreloadGone(driver, url);
            Shared.FindElement(driver, By.Id("btn-register-webhook-url"), 10).Click();
            SharedMethods.WaitUntilPreloadGone(driver, url);
            Shared.FindElement(driver, By.Id("webhook-url-input"), 10).SendKeys("https://www.gluwa-" + i + ".com");
            Shared.FindElement(driver, By.Id("buttonRegister"), 10).Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Register webhook URL
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public static void RegisterWebhookUrl(IWebDriver driver, string url)
        {
            // Click register button
            driver.FindElements(By.CssSelector("#buttonRegister")).Where(c => c.Displayed == true).FirstOrDefault().Click();
            SharedMethods.WaitUntilPreloadGone(driver, url);
        }

        /// <summary>
        /// Add URL button
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public static void AddUrl(IWebDriver driver, string url)
        {
            // Click add url button
            Shared.FindElement(driver, By.CssSelector(".exchange-url"), 10).Click();
            SharedMethods.WaitUntilPreloadGone(driver, url);
        }

        /// <summary>
        /// Remove exchange Url
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public static void RemoveExchangeUrl(IWebDriver driver, string url)
        {
            // If there's any existing webhooks, remove them
            if (driver.FindElements(By.CssSelector(".close .icon-close")).Any())
            {
                driver.FindElements(By.CssSelector(".close .icon-close")).LastOrDefault().Click();
                SharedMethods.WaitUntilPreloadGone(driver, url);
                driver.FindElements(By.CssSelector("div.mc-box > div.mc-control > button")).Where(c => c.Displayed == true).FirstOrDefault().Click();
                SharedMethods.WaitUntilPreloadGone(driver, url);
            }
        }

        /// <summary>
        /// Remove webhook URLs
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public static void RemoveWebhookUrls(IWebDriver driver, string url)
        {
            // If there's any existing webhooks, remove them
            foreach (var webhook in driver.FindElements(By.CssSelector(".close .icon-close")))
            {
                RemoveWebhookURL(driver, url);
            }
        }

        /// <summary>
        /// Remove WebHook URL
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="url"></param>
        public static void RemoveWebhookURL(IWebDriver driver, string url)
        {
            SharedMethods.WaitUntilPreloadGone(driver, url);
            // Click "x" on the right from registered Url
            driver.FindElements(By.CssSelector("div.url-item > span.close")).FirstOrDefault().Click();
            Thread.Sleep(2000);
            driver.FindElements(By.CssSelector("div.mc-box > div.mc-control > button.remove-url-btn")).FirstOrDefault().Click();
            Navigation.NavigateToWebhooks(driver, url);   // TODO: Remove this line once https://www.notion.so/gluwa/modal-backdrop-doesn-t-disappear-after-a-webhook-or-exchange-URL-is-deleted-738fc766fb60434dae13c09aa3ba9acf is resolved
        }

        /// <summary>
        /// Cancel X button
        /// </summary>
        /// <param name="driver"></param>
        public static void CancelAddUrl(IWebDriver driver) 
        {
            // Click X button 
            Shared.FindElement(driver, By.CssSelector("#register-exchange-url-modal > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1)"), 10).Click();
            Thread.Sleep(2000);
        }
    }
}
