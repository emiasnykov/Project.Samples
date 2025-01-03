using Microsoft.VisualStudio.TestTools.UnitTesting;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;
using TestContext = NUnit.Framework.TestContext;
using Shared.Driver;
using Dashboard.UITests.Pages;
using NUnit.Framework;
using Dashboard.UITests.Enum;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Dashboard.UITests
{
    [TestFixture("Test")]
    internal class FaucetsTests
    {
        public IWebDriver driver;
        public string url;
        public readonly string useEnvironment;
        public string browser;

        /// <summary>
        /// From TestFixture
        /// </summary>
        /// /// <param name="useEnvironment"></param>
        public FaucetsTests(string useEnvironment)
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
            else // Default to Chrome
            {
                browser = "Chrome";
            }
        }

        [TestMethod]
        [Description("TestMethod for positive testcases with valid values")]
        public void AddFaucetTests(string currency)
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                //Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);                          //Set environment URL
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);     //Log in as valid user

                //Steps
                Navigation.NavigateToFaucets(driver, url);                     //Navigate to 'Fausets'
                FaucetsPage.FaucetsForm(driver, FaucetsFormEnum.ValidAddress); //Fill in form with valid values
                FaucetsPage.SelectCurrency(driver, currency);                  //Select currency dropdown 
                FaucetsPage.SubmitFaucet(driver);                              //Click Submit Faucet

                // Assert
                Assertions.CheckPopupMintingSuccessful(driver);                //Verify popup appears
            }
        }


        //[Test]
        //[Description("TestCaseId:C534")]
        //public void Faucets_USDG_ValidAddress_Pos()
        //{
        //    AddFaucetTests("USDG");
        //}


        //[Test]
        //[Description("TestCaseId:C536")]
        //public void Faucets_KRWG_ValidAddress_Pos()
        //{
        //    AddFaucetTests("KRWG");
        //}


        [Test]
        [Description("TestCaseId:C1579")]
        public void Faucets_NGNG_ValidAddress_Pos()
        {
            Thread.Sleep(10000);  //delay to prevent error: 'Please wait at least 30 seconds before minting to this address again'
            AddFaucetTests("NGNG");
        }


        [Test]
        [Description("TestCaseId:C1578")]
        public void Faucets_sNGNG_ValidAddress_Pos()
        {
            Thread.Sleep(10000);  //delay to prevent error: 'Please wait at least 30 seconds before minting to this address again'
            AddFaucetTests("sNGNG");
        }

       // Disabling these tests as faucet no longer works for these currencies
        //[Test]
        //[Description("TestCaseId:C538")]
        //public void Faucets_sUSDCG_ValidAddress_Pos()
        //{
        //    Thread.Sleep(10000);  //delay to prevent error: 'Please wait at least 30 seconds before minting to this address again'
        //    AddFaucetTests("sUSDCG");
        //}

        //[Test]
        //[Description("TestCaseId:C1978")]
        //public void Faucets_sKRWCG_ValidAddress_Pos()
        //{
        //    Thread.Sleep(10000);  //delay to prevent error: 'Please wait at least 30 seconds before minting to this address again'
        //    AddFaucetTests("sKRWCG");
        //}

        //[Test]
        //[Description("TestCaseId:C539")]
        //public void Faucets_sUSDCG_InvalidAddress_Neg()
        //{
        //    using (var init = new TestScope(browser, useEnvironment))
        //    {
        //        //Setup
        //        driver = init.Instance;
        //        url = init.Env;
        //        // Arrange
        //        LoginPage.GoToUrl(driver, url);                                  //Set environment URL
        //        LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);              //Log in as valid user

        //        // Steps
        //        Navigation.NavigateToFaucets(driver, url);                       //Navigate to 'Fausets'
        //        FaucetsPage.FaucetsForm(driver, FaucetsFormEnum.InvalidAddress); //Fill in form with invalid values
        //        FaucetsPage.SelectCurrency(driver, "sUSDCG");                      //Select currency dropdown 
        //        FaucetsPage.SubmitFaucet(driver);

        //        //Assertion  
        //        Assertions.VerifyInvalidAddressInFaucet(driver);                 //Validate Error message appears
        //    }
        //}


        //[Test]
        //[Description("TestCaseId:C537")]
        //public void Faucets_sKRWCG_InvalidAddress_Neg()
        //{
        //    using (var init = new TestScope(browser, useEnvironment))
        //    {
        //        //Setup
        //        driver = init.Instance;
        //        url = init.Env;
        //        // Arrange
        //        LoginPage.GoToUrl(driver, url);                                  //Set environment URL
        //        LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);              //Log in as valid user

        //        // Steps
        //        Navigation.NavigateToFaucets(driver, url);                       //Navigate to 'Fausets'
        //        FaucetsPage.FaucetsForm(driver, FaucetsFormEnum.InvalidAddress); //Fill in form with invalid values
        //        FaucetsPage.SelectCurrency(driver, "sKRWCG");                      //Select currency dropdown 
        //        FaucetsPage.SubmitFaucet(driver);                                //Submit Faucet  

        //        //Assertion
        //        Assertions.VerifyInvalidAddressInFaucet(driver);                 //Verify Validation Error appears
        //    }
        //}


        [Test]
        [Description("TestCaseId:C542")]
        public void Faucets_EmptyAddress_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                //Setup
                driver = init.Instance;
                url = init.Env;
                // Arrange
                LoginPage.GoToUrl(driver, url);                                //Set environment URL
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);            //Log in as valid user

                // Steps
                Navigation.NavigateToFaucets(driver, url);                     //Navigate to 'Fausets'
                FaucetsPage.FaucetsForm(driver, FaucetsFormEnum.EmptyAddress); //Fill in form with invalid values
                FaucetsPage.SelectCurrency(driver, "Currency");                    //Select currency dropdown 
                FaucetsPage.SubmitFaucet(driver);                              //Submit Faucet  

                //Assertion
                Assertions.VerifyEmptyAddressInFaucet(driver);                //Verify validation message appears
            }
        }


        [Test]
        [Description("TestCaseId:C542")]
        public void Faucets_NoCurrency_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                //Setup
                driver = init.Instance;
                url = init.Env;
                // Arrange
                LoginPage.GoToUrl(driver, url);                                //Set environment URL
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);            //Log in as valid user

                // Steps
                Navigation.NavigateToFaucets(driver, url);                     //Navigate to 'Fausets'
                FaucetsPage.FaucetsForm(driver, FaucetsFormEnum.ValidAddress); //Fill in form with invalid values
                //Currency not selected
                FaucetsPage.SubmitFaucet(driver);                              //Submit Faucet  

                //Assertion
                Assertions.VerifyNoCurrencyInFaucet(driver);                   //Verify validation message appears
            }
        }

        private sealed class TestScope : IDisposable
        {
            public IWebDriver Instance { get; }
            public string Env { get; }

            //SetUp
            public TestScope(string browser, string useEnvironment)
            {
                Driver initialize = new Driver();
                Instance = initialize.StartBrowser(browser);
                Shared setup = new Shared();
                Env = setup.SetEnvironmentVariables(useEnvironment);
            }

            //TearDown
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