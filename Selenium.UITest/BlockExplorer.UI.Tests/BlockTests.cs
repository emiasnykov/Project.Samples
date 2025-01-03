using BlockExplorer.UI.Tests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using Shared.Driver;
using Shared.SharedMethods;

namespace BlockExplorer.UI.Tests
{
    class BlockTests
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
        public void Blocks_CheckBlockHistoryList_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps
                Navigation.GotoBlocksPage(driver);
                var elementsList = BlocksPage.GetBlockHistoryList(driver);

                //Assert
                Assertions.VerifyBlocksListHistory(elementsList);
            }
        }


        [Test]
        public void Blocks_CheckSingleBlockInfo_Pos()
        {
            using (var init = new TestScope(browser))
            {
                // Setup
                driver = init.Instance;

                // Arrange
                SharedMethods.GoToUrl(driver);

                // Steps
                Navigation.GotoBlocksPage(driver);
                BlocksPage.ViewSingleBlock(driver);
                var blockInfo = BlocksPage.GetBlockInfo(driver);

                //Assert
                Assertions.BlocksVerifySingleBlockInfo(blockInfo);
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
