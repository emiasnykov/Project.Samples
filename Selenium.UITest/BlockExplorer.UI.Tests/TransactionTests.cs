using BlockExplorer.UI.Tests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using Shared.Driver;
using Shared.SharedMethods;

namespace BlockExplorer.UI.Tests
{
    class TransactionTests
    {
        public IWebDriver driver;
        public string browser;

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


        [Test]
        public void Transactions_CheckTransactionHistory_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps
                Navigation.GotoTransactionsPage(driver);
                var elementsList = TransactionsPage.CheckTransactionHistory(driver);

                //Assert
                Assertions.VerifyransactionHistoryInfo(elementsList);
            }
        }


        [Test]
        public void Transaction_CheckSingleTransactionInfo_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps 
                Navigation.GotoTransactionsPage(driver);
                TransactionsPage.ViewSingleTransaction(driver);
                var transactionInfo = TransactionsPage.GetTransactionInfo(driver);

                //Assert
                Assertions.VerifySingleTransactionInfo(transactionInfo);
            }
        }

        //Enable after fix
        //[Test]
        //public void Transaction_CheckSingleAddressInfo_Pos()
        //{
        //    using (var init = new TestScope(browser))
        //    {
        //        // Setup
        //        driver = init.Instance;

        //        // Arrange
        //        SharedMethods.GoToUrl(driver);

        //        // Steps 
        //        Navigation.GotoTransactionsPage(driver);
        //        TransactionsPage.ViewSingleAddress(driver);
        //        var addressInfo = HomePage.GetAddressInfo(driver);

        //        //Assert
        //        Assertions.VerifySingleAddressInfo(addressInfo);
        //    }
        //}


        private sealed class TestScope : IDisposable
        {
            public IWebDriver Instance { get; }

            //SetUp
            public TestScope(string browser)
            {
                Driver initialize = new Driver();
                Instance = initialize.StartBrowser(browser);
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

        [TearDown]
        public void CleanUp()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
