using NUnit.Framework;
using OpenQA.Selenium;
using NUnit.Framework.Internal;
using System;
using Shared.Driver;
using Dashboard.UITests.Pages;
using Dashboard.UITests.Enum;

namespace Dashboard.UITests
{
    [TestFixture("Test")]
    [TestFixture("Stage")]
    internal class WebhooksTests
    {
        public IWebDriver driver;
        public string url;
        public readonly string useEnvironment;
        public string browser;
        public int num;

        /// <summary>
        /// From TestFixture
        /// </summary>
        /// /// <param name="useEnvironment"></param>
        public WebhooksTests(string useEnvironment)
        {
            this.useEnvironment = useEnvironment;
        }

        [SetUp]
        public void Setup()
        {
            browser = TestContext.Parameters["browser"];
            if (browser == "Firefox")
            {
                browser = "Firefox";
            }
            else    
            {
                browser = "Chrome";  // Default to Chrome
            }
        }

        [Test]
        public void Webhooks_SecretButton_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToWebhooks(driver, url);
                WebhooksPage.ClickSecretShowBtn(driver);

                // Assert
                Assertions.VerifySecretIsShowUp(driver);       // Verify secret key is visible

            }
        }


        [Test]
        public void Webhooks_ResetButton_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToWebhooks(driver, url);
                WebhooksPage.ClickSecretShowBtn(driver);
                string HiddenSecretOld = WebhooksPage.GetHiddenSecret(driver);    // Get old hidden secret key
                WebhooksPage.WebhooksReset(driver);                               // Reset 
                string HiddenSecretNew = WebhooksPage.GetHiddenSecret(driver);    // Get new hidden secret key

                // Assert
                Assert.AreNotEqual(HiddenSecretOld, HiddenSecretNew, "Webhook secret did not change!");
            }
        }


        [Test]
        public void Webhooks_RegisterUrl_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToWebhooks(driver, url);
                WebhooksPage.RemoveWebhookUrls(driver, url);
                WebhooksPage.AddWebhookUrl(driver, RegWebhookUrlEnum.ValidUrl);   // Valid Url
                WebhooksPage.RegisterWebhookUrl(driver, url);

                // Assert
                Assertions.VerufyRegisterWebHookUrl(driver);

                // CleanUp
                WebhooksPage.RemoveWebhookUrls(driver, url);
            }
        }


        [Test]
        public void Webhooks_RegisterInvalidUrl_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToWebhooks(driver, url);
                WebhooksPage.RemoveWebhookUrls(driver, url);
                WebhooksPage.AddWebhookUrl(driver, RegWebhookUrlEnum.InvalidUrl);  // Invalid Url

                // Assert
                Assertions.VerifyInvalidWebhookUrl(driver);
            }
        }


        [Test]
        public void Webhooks_RegisterListOfUrls_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToWebhooks(driver, url);
                WebhooksPage.RemoveWebhookUrls(driver, url);

                int i = 0;
                do
                {
                    if (browser == "Firefox")                           // Reduce number of Urls for 'Firefox' cause it is time-consuming  
                    {  num = 3; }
                    else { num = 10; }
                    WebhooksPage.AddWebhooksUrls(driver, url, i);       // Add {10} webhook Urls 
                    i++;

                } while (i < num);

                // Assert
                Assert.IsTrue(driver.FindElements(By.CssSelector(".close .icon-close")).Count > 1);

                // CleanUp
                WebhooksPage.RemoveWebhookUrls(driver, url);
            }
        }


        [Test]
        public void Webhooks_AddExRequestWebhookUrl_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToWebhooks(driver, url);
                WebhooksPage.RemoveExchangeUrl(driver, url);
                WebhooksPage.AddExRequestWebhookUrl(driver, RegWebhookUrlEnum.ValidUrl);
                WebhooksPage.AddUrl(driver, url);
                
                // Assert
                Assertions.VerufyAddExRequestWebHookUrl(driver);

                // CleanUp
                WebhooksPage.RemoveExchangeUrl(driver, url);
            }
        }


        [Test]
        public void Webhooks_AddInvalidExRequestUrl_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToWebhooks(driver, url);
                WebhooksPage.RemoveExchangeUrl(driver, url);
                WebhooksPage.AddExRequestWebhookUrl(driver, RegWebhookUrlEnum.InvalidUrl);  // Invalid ExReqwebhookUrl

                // Assert
                Assertions.VerifyInvalidExReqUrl(driver);
            }
        }


        [Test]
        public void CancelExchange_RequestWebhookURL()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToWebhooks(driver, url);
                WebhooksPage.RemoveExchangeUrl(driver, url);
                WebhooksPage.AddExRequestWebhookUrl(driver, RegWebhookUrlEnum.ValidUrl);
                WebhooksPage.CancelAddUrl(driver);

                // Assert
                Assertions.VerufyNoExRequestWebHookUrl(driver);
            }
        }


        /// <summary>
        /// Initialization
        /// Set environment
        /// TearDown
        /// </summary>
        private sealed class TestScope : IDisposable
        {
            public IWebDriver Instance { get; }
            public string Env { get; }

            // SetUp
            public TestScope(string browser, string useEnvironment)
            {
                Driver initialize = new Driver();
                Instance = initialize.StartBrowser(browser);
                Shared setup = new Shared();
                Env = setup.SetEnvironmentVariables(useEnvironment);
            }

            // TearDown
            public void Dispose()
            {
                if (Instance != null)
                {
                    Instance.Quit();
                }
            }
        }
    }
}