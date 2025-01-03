using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Shared.Driver;
using Dashboard.UITests.Pages;
using Dashboard.UITests.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;
using System.Threading;
using TestContext = NUnit.Framework.TestContext;
using Assert = NUnit.Framework.Assert;

namespace Dashboard.UITests
{
    [TestFixture("Test")]
    [TestFixture("Stag")]
    internal class TransactionsTests
    {
        public IWebDriver driver;
        public string url;
        public string date;
        public readonly string useEnvironment;
        public string browser;

        /// <summary>
        /// From TestFixture
        /// </summary>
        /// /// <param name="useEnvironment"></param>
        public TransactionsTests(string useEnvironment)
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
        [Description("TestCases: C546-C548")]
        public void CheckTransactions(string currency)
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                //Setup                
                driver = init.Instance;
                url = init.Env;
                date = init.Date;

                // Arrange
                LoginPage.GoToUrl(driver, url);                            //Set environment URL
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);        //Log in as valid user

                //Steps
                Navigation.NavigateToAddresses(driver, url);               //Navigate to 'Addresses'
                AddressesPage.RemoveAnyAddresses(driver, true);            //Remove existed addresses
                AddressesPage.AddressesForm(driver, url, AddressFormEnum.ValidValues); //Fill in form with valid values
                AddressesPage.SelectCurrency(driver, currency);            //Select Currency
                AddressesPage.RegisterAddress(driver);                     //Register address
                Thread.Sleep(3000);
                Navigation.NavigateToTransactions(driver, url);            //Navigate to transactions page    
                TransactionsPage.SearchTransactionsByDate(driver, Shared.DATE_NGNG);//Search transactions by date 
                TransactionsPage.TransactionDetailsPopup(driver);          //Navigate to transaction detail page  
                TransactionsPage.TransactionsModalDetailsGluwa(driver);    //Verify transaction details     

                //Assert
                Assertions.TransactionConfirmedStatus(driver);

                //CleanUp
                Navigation.NavigateToAddresses(driver, url);                //Navigate to 'Addresses'
                AddressesPage.RemoveAnyAddresses(driver, true);             //Remove existed addresses
            }
        }

        // Disabled because NGNG is no longer supported
        //[Test]
        //[Description("TestCaseId:C555")]
        //public void Transactions_NGNG_Pos()
        //{
        //    CheckTransactions("NGNG");
        //}

        [Test]
        [Description("TestCaseId:C557")]
        public void Transactions_sUSDCG_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                //Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                //Steps
                Navigation.NavigateToAddresses(driver, url);
                AddressesPage.RemoveAnyAddresses(driver, true);
                AddressesPage.AddressesForm(driver, url, AddressFormEnum.ValidValuesAltAdr);
                AddressesPage.SelectCurrency(driver, "sUSDCG");
                AddressesPage.RegisterAddress(driver);
                Thread.Sleep(3000);
                Navigation.NavigateToTransactions(driver, url);
                //Check Balance is not 0
                if (driver.FindElements(By.ClassName("transaction-item")).Count > 0)
                {
                    TransactionsPage.SearchTransactionsByDate(driver, Shared.DATE_sUSDCG_TEST);
                    TransactionsPage.TransactionDetailsPopup(driver);
                    TransactionsPage.TransactionsModalDetailsLuniverse(driver);

                    //Assert
                    Assertions.TransactionConfirmedStatus(driver);
                }
                else
                {
                    Thread.Sleep(10000);                                    //Synhronize test execution
                }


                //CleanUp
                Navigation.NavigateToAddresses(driver, url);                //Navigate to 'Addresses'
                AddressesPage.RemoveAnyAddresses(driver, true);             //Remove existed addresses
            }
        }


        [Test]
        public void Transaction_NGNG_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                //Setup
                driver = init.Instance;
                url = init.Env;

                //Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUserETH);

                //steps
                Navigation.NavigateToAddresses(driver, url);
                AddressesPage.RemoveAnyAddresses(driver, true);
                AddressesPage.AddressesForm(driver, url, AddressFormEnum.ValidValuesAltAdr);
                AddressesPage.SelectCurrency(driver, "NGNG");
                AddressesPage.RegisterAddress(driver);
                Thread.Sleep(3000);
                Navigation.NavigateToTransactions(driver, url);
                //Check Balance is not 0
                if (driver.FindElements(By.ClassName("transaction-item")).Count > 0)
                {
                    TransactionsPage.SearchTransactionsByDate(driver, Shared.DATE_NGNG_TEST);
                    TransactionsPage.TransactionDetailsPopup(driver);
                    TransactionsPage.TransactionsModalDetailsGoerli(driver);


                    //Assert
                    Assertions.TransactionConfirmedStatus(driver);


                }
                else
                { Thread.Sleep(10000); }

                //CleanUp
                Navigation.NavigateToAddresses(driver, url);                //Navigate to 'Addresses'
                AddressesPage.RemoveAnyAddresses(driver, true);             //Remove existed addresses
            }
        }

        [Test]
        [Description("TestCaseId:C1582")]
        public void Transactions_sNGNG_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                //Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);                           
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);      

                //Steps
                Navigation.NavigateToAddresses(driver, url);
                AddressesPage.RemoveAnyAddresses(driver, true);
                AddressesPage.AddressesForm(driver, url, AddressFormEnum.ValidValuesAltAdr);
                AddressesPage.SelectCurrency(driver, "sNGNG");
                AddressesPage.RegisterAddress(driver);
                Thread.Sleep(3000);
                Navigation.NavigateToTransactions(driver, url);
                TransactionsPage.SearchTransactionsByDate(driver, Shared.DATE_sNGNG_TEST);
                TransactionsPage.TransactionDetailsPopup(driver);
                TransactionsPage.TransactionsModalDetailsLuniverse(driver);

                //Assert
                Assertions.TransactionConfirmedStatus(driver);
            }
        }

        // Disabled because sKRWCG isn't working/supported
        //[Test]
        //[Description("TestCaseId:C1980")]
        //public void Transactions_sKRWCG_Pos()
        //{
        //    using (var init = new TestScope(browser, useEnvironment))
        //    {
        //        //Setup
        //        driver = init.Instance;
        //        url = init.Env;

        //        if (useEnvironment == "Stag")  // Undo when currency be delivered to Stage
        //        {
        //            Assert.Inconclusive($"Cannot run this test on environment: {useEnvironment}");
        //        }
        //        else
        //        {
        //            // Arrange
        //            LoginPage.GoToUrl(driver, url);
        //            LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

        //            //Steps
        //            Navigation.NavigateToAddresses(driver, url);
        //            AddressesPage.RemoveAnyAddresses(driver, true);
        //            AddressesPage.AddressesForm(driver, url, AddressFormEnum.ValidValuesAltAdr);
        //            AddressesPage.SelectCurrency(driver, "sKRWCG");
        //            AddressesPage.RegisterAddress(driver);
        //            Thread.Sleep(3000);
        //            Navigation.NavigateToTransactions(driver, url);
        //            TransactionsPage.SearchTransactionsByDate(driver, Shared.DATE_sKRWCG_TEST);
        //            TransactionsPage.TransactionDetailsPopup(driver);
        //            TransactionsPage.TransactionsModalDetailsLuniverse(driver);

        //            //Assert
        //            Assertions.TransactionConfirmedStatus(driver);
        //        }
        //    }
        //}


        [Test]
        [Description("TestCaseId:C572")]
        public void TransactionsExportToFile_Pos()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                //Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                //Steps
                Navigation.NavigateToAddresses(driver, url);
                AddressesPage.RemoveAnyAddresses(driver, true);
                AddressesPage.AddressesForm(driver, url, AddressFormEnum.ValidValues);
                AddressesPage.SelectCurrency(driver, "NGNG");
                AddressesPage.RegisterAddress(driver);
                Thread.Sleep(3000);
                Navigation.NavigateToTransactions(driver, url);
                TransactionsPage.TransactionClickExportButton(driver);

                //Assert
                Assertions.TransactionsExported(driver);

                //CleanUp
                Navigation.NavigateToAddresses(driver, url);                //Navigate to 'Addresses'
                AddressesPage.RemoveAnyAddresses(driver, true);             //Remove existed addresses
            }
        }


        [Test]
        [Description("TestCaseId:C564")]
        public void TransactionsNoAddress_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                //Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                //Steps
                Navigation.NavigateToAddresses(driver, url);                    //Navigate to 'Addresses'
                AddressesPage.RemoveAnyAddresses(driver, true);                 //Remove existed addresses
                Thread.Sleep(3000);
                Navigation.NavigateToTransactions(driver, url);                 //Navigate to transactions page
                TransactionsPage.NoAddress(driver);                             //No address selected

                //Assert
                Assertions.TransactionNoAddresses(driver);
            }
        }

        private sealed class TestScope : IDisposable
        {
            public IWebDriver Instance { get; }
            public string Env { get; }
            public string Date { get; }

            //SetUp
            public TestScope(string browser, string useEnvironment)
            {
                NUnit.Framework.TestContext.WriteLine($"Browser: {browser}");
                NUnit.Framework.TestContext.WriteLine($"Environment: {useEnvironment}");

                Driver initialize = new Driver();
                Instance = initialize.StartBrowser(browser);
                Shared setup = new Shared();
                Env = setup.SetEnvironmentVariables(useEnvironment);
                Date = setup.SetDateVariables(useEnvironment);
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
