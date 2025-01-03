using BlockExplorer.UI.Tests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using Shared.Driver;
using Shared.SharedMethods;

namespace BlockExplorer.UI.Tests
{
    [TestFixture]
    class HomeTests
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
        public void Home_CheckNavigationLinks_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps
                // Verify elements of navigation bar
                Navigation.GotoTransactionsPage(driver);
                Assertions.TransactionsPageIsAt(driver);                // Check Transactions page opened

                Navigation.GotoHomePage(driver);
                Navigation.GotoBlocksPage(driver);
                Assertions.BlocksPageIsAt(driver);                       // Check Blocks page opened

                Navigation.GotoHomePage(driver);
                Navigation.GotoRichListPage(driver);
                Assertions.RichListPageIsAt(driver);                     // Check Rich List page opened

                Navigation.GotoHomePage(driver);
                Navigation.GotoVestingTokenPage(driver);
                driver.SwitchTo().Window(driver.WindowHandles[1]);
                Assertions.VestingTokenPageIsAt(driver);                   // Check Vesting Token page opened 
                driver.SwitchTo().Window(driver.WindowHandles[1]).Close();
                driver.SwitchTo().Window(driver.WindowHandles[0]);

                Navigation.GotoHomePage(driver);
                Navigation.GotoCreditCoinPage(driver);
                driver.SwitchTo().Window(driver.WindowHandles[1]);
                Assertions.CreditCoinPageIsAt(driver);                     // Check Credit Coin page opened             
                driver.SwitchTo().Window(driver.WindowHandles[1]).Close();
            }
        }


        [Test]
        public void Home_SearchByTransactionId_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps
                HomePage.ViewAllTransactions(driver);
                string TxId = TransactionsPage.GetTransactionId(driver);
                Navigation.GotoHomePage(driver);
                HomePage.SearchByTransactionId(driver, TxId);

                //Assert
                Assertions.VerifyTransactionIsFound(driver, TxId);
            }
        }


        [Test]
        public void Home_SearchByAddress_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps
                HomePage.ViewAllTransactions(driver);
                string address = TransactionsPage.GetAddress(driver);
                Navigation.GotoHomePage(driver);
                HomePage.SearchByAddress(driver, address);

                //Assert
                Assertions.VerifyAddressIsFound(driver, address);
            }
        }


       [Test]
        public void Home_SearchByBlockId_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps
                HomePage.ViewSingleBlock(driver);
                string blockId = BlocksPage.GetBlockid(driver);
                Navigation.GotoHomePage(driver);
                HomePage.SearchByBlockId(driver, blockId);

                //Assert
                Assertions.VerifyBlockIsFound(driver, blockId);
            }
        }


        [Test]
        public void Home_CheckBlockchainInfo_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps              
                var elementsList = HomePage.GetBlockchainInfo(driver);

                //Assert
                Assertions.VerifyBlockchainInfo(elementsList);
            }
        }


        [Test]
        public void Home_CheckLatestTransactions_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps              
                var transactionList = HomePage.GetTransactionList(driver);

                //Assert
                Assertions.VerifyTransactionList(transactionList);
            }
        }


        [Test]
        public void Home_CheckLatestBlocks_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps              
                var blockList = HomePage.GetBlockList(driver);

                //Assert
                Assertions.VerifyBlockList(blockList);
            }
        }


        [Test]
        public void Home_CheckSingleTransactionInfo_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps 
                HomePage.ViewSingleTransaction(driver);
                var transactionInfo = TransactionsPage.GetTransactionInfo(driver);

                //Assert
                Assertions.VerifySingleTransactionInfo(transactionInfo);
            }
        }


        [Test]
        public void Home_CheckSingleBlockInfo_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps 
                HomePage.ViewSingleBlock(driver);
                var blockInfo = BlocksPage.GetBlockInfo(driver);

                //Assert
                Assertions.VerifySingleBlockInfo(blockInfo);
            }
        }


        [Test]
        public void Home_CheckSingleAddressInfo_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps 
                HomePage.ViewSingleAddress(driver);
                var addressInfo = HomePage.GetAddressInfo(driver);

                //Assert
                Assertions.VerifySingleAddressInfo(addressInfo);
            }
        }


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
