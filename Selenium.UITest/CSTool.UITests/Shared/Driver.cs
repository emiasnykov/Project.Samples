using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace CSTool.UITests
{
    public class Driver
    {
        private IWebDriver Instance { get; set; }

        //Initialize driver
        public IWebDriver StartBrowser(string browser)
        {
            bool devOpsBuild = Environment.GetEnvironmentVariable("TF_BUILD") == "True" ? true : false;

            //Switch to proper browser
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

    }
}


