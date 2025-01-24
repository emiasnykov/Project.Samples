using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Shared.Driver
{
    public class Driver
    {
        private IWebDriver Instance { get; set; }
        private string BASE_URI { get; set; }

        // Environments
        public static string Dashboard_TEST_URL = "https://gdashboard--test.azurewebsites.net";
        public static string Dashboard_STAGE_URL = "https://gdashboard-stage.azurewebsites.net/";

        /// <summary>
        /// Initialize driver
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public IWebDriver StartBrowser(string browser)
        {
            bool devOpsBuild = Environment.GetEnvironmentVariable("TF_BUILD") == "True" ? true : false;

            // Switch to proper browser
            switch (browser)
            {
                case "Chrome":
                    ChromeOptions options = new ChromeOptions();
                    if (devOpsBuild == true) { options.AddArguments("headless"); }
                    options.AddArguments("start-maximized");
                    Instance = new ChromeDriver(options);
                    Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                break;

                case "Firefox":
                    FirefoxOptions ffOptions = new FirefoxOptions();
                    if (devOpsBuild == true) { ffOptions.AddArguments("--headless"); }
                    Instance = new FirefoxDriver(ffOptions);
                    Instance.Manage().Window.Maximize();
                    Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                break;
            }
            return Instance;
        }

        /// <summary>
        /// Set environment
        /// </summary>
        /// <param name="environmment"></param>
        /// <returns></returns>
        public string SetEnvironmentVariables(string environmment = "Test")
        {
            switch (environmment)
            {
                case "Stage":
                    BASE_URI = Dashboard_STAGE_URL;
                break;

                default:
                    BASE_URI = Dashboard_TEST_URL;   // Default = Test
                break;
            }
            return BASE_URI;
        }
    }
}