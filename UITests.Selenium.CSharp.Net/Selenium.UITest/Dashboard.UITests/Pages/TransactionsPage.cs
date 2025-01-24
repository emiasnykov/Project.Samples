using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using Assert = NUnit.Framework.Assert;
using System.Linq;

namespace Dashboard.UITests.Pages
{
    /// <summary>
    ///  Transactions Page functions
    /// </summary>
    internal class TransactionsPage
    {
        /// <summary>
        /// Search transactions by date
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="date"></param>
        public static void SearchTransactionsByDate(IWebDriver driver, string date)
        {
            // Open Address dropdown
            SharedMethods.WaitForElementToAppear(driver, By.ClassName("main-caption"));
            Shared.FindElement(driver, By.Id("addressDropdown"), 30).SendKeys(Keys.Space);
            Thread.Sleep(1000);           

            // Click Filter by date button
            Shared.FindElement(driver, By.CssSelector(".fa-filter"), 30).Click();
            Shared.FindElement(driver, By.Id("dateFilter"), 30).SendKeys(date);
            Shared.FindElement(driver, By.Id("search"), 30).Click();
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Open popup with transaction details
        /// </summary>
        /// <param name="driver"></param>
        public static void TransactionDetailsPopup(IWebDriver driver)
        {
            SharedMethods.WaitForElementToAppear(driver, By.ClassName("main-caption"));
            Shared.FindElement(driver, By.CssSelector(":nth-child(1) > .transaction-item"), 30).Click();
            SharedMethods.WaitForElementToAppear(driver, By.ClassName("modal-content"));
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Get transactions with received payments
        /// </summary>
        /// <param name="driver"></param>
        public static void TransactionReceivedPayments(IWebDriver driver)
        {
            Shared.FindElement(driver, By.CssSelector(".fa-filter"), 30).Click();
            Shared.FindElement(driver, By.Id("type-filter"), 30).Click();
            Shared.FindElement(driver, By.Id("search"), 30).Click();
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Verify transaction modal details
        /// Rinkeby info page
        /// </summary>
        /// <param name="driver"></param>
        public static void TransactionsModalDetailsGluwa(IWebDriver driver)
        {
            // Get text for TX hash and make sure that the URL contains the hash
            string textTxHash = driver.FindElement(By.CssSelector("div.transaction-detail-body > a")).Text;
            string urlTx = driver.FindElement(By.CssSelector("div.transaction-detail-body > a")).GetAttribute("href");
            bool txHashMatch = urlTx.Contains(textTxHash);

            // Get text for sender address and make sure that the URL contains the same sender address
            string textSenderAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(2) > div.transaction-detail-body > a")).Text;
            string urlSenderAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(2) > div.transaction-detail-body > a")).GetAttribute("href");
            bool senderAddressMatch = urlSenderAddress.Contains(textSenderAddress);

            // Get text for receiver address and make sure that the URL contains the same receiver address
            string textReceiverAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(3) > div.transaction-detail-body > a")).Text;
            string urlReceiverAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(3) > div.transaction-detail-body > a")).GetAttribute("href");
            bool receiverAddressMatch = urlReceiverAddress.Contains(textReceiverAddress);

            // Get details on Rinkeby page
            driver.FindElement(By.CssSelector("div.transaction-detail-body > a")).Click();   // click the Rinkeby transaction link
            var popup = driver.WindowHandles[1];   // handler for the new tab
            driver.SwitchTo().Window(popup);   // switch to tab
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
            wait.Until<IWebElement>(c => c.FindElement(By.CssSelector("#spanTxHash")));
            string rinkebyUrl = driver.SwitchTo().Window(popup).Url; // get tab URL
            string rinkebyTxHash = driver.FindElement(By.CssSelector("#spanTxHash")).Text; // get the tx hash on rinkeby
            bool rbTxHashMatch = rinkebyTxHash.Contains(textTxHash);

            // Assert
            Assert.IsTrue(txHashMatch, $"Expected URL to contain {textTxHash}, but actual URL was {urlTx}");
            Assert.IsTrue(txHashMatch, $"Expected URL to contain {textSenderAddress}, but actual URL was {urlSenderAddress}");
            Assert.IsTrue(txHashMatch, $"Expected URL to contain {textReceiverAddress}, but actual URL was {urlReceiverAddress}");
            Assert.AreEqual(urlTx, rinkebyUrl); // url is OK  
            Assert.IsTrue(rbTxHashMatch, $"Expected Luniverse has to contain {textTxHash}, but it was {rinkebyTxHash}");

            // Clear
            driver.SwitchTo().Window(driver.WindowHandles[1]).Close();   // close the tab
            driver.SwitchTo().Window(driver.WindowHandles[0]);   // get back to the main window
        }

        /// <summary>
        /// Verify not confirmed transaction details
        /// Rinkeby info page
        /// </summary>
        /// <param name="driver"></param>
        public static void TransactionsNotConfirmedGluwa(IWebDriver driver)
        {
            // Get text for TX hash and make sure that the URL contains the hash
            string textTxHash = driver.FindElement(By.CssSelector("div.transaction-detail-body > a")).Text;
            string urlTx = driver.FindElement(By.CssSelector("div.transaction-detail-body > a")).GetAttribute("href");
            bool txHashMatch = urlTx.Contains(textTxHash);

            // Get text for sender address and make sure that the URL contains the same sender address
            string textSenderAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(2) > div.transaction-detail-body > a")).Text;
            string urlSenderAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(2) > div.transaction-detail-body > a")).GetAttribute("href");
            bool senderAddressMatch = urlSenderAddress.Contains(textSenderAddress);

            // Get text for receiver address and make sure that the URL contains the same receiver address
            string textReceiverAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(3) > div.transaction-detail-body > a")).Text;
            string urlReceiverAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(3) > div.transaction-detail-body > a")).GetAttribute("href");
            bool receiverAddressMatch = urlReceiverAddress.Contains(textReceiverAddress);

            // Get details on Rinkeby page
            driver.FindElement(By.XPath("(.//*[@class='transaction-detail-body']//a)[2]")).Click(); // click the Rinkeby transaction link
            var popup = driver.WindowHandles[1]; // handler for the new tab
            driver.SwitchTo().Window(popup); // switch to tab
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
            wait.Until<IWebElement>(c => c.FindElement(By.XPath("//span[contains(text(),'Rinkeby Testnet Network')]")));
            string rinkebyUrl = driver.SwitchTo().Window(popup).Url; // get tab URL
            string matchAddress = driver.FindElement(By.XPath(".//*[@id='mainaddress']")).Text;

            // Assert
                // Verify sender address only
            Assert.AreEqual(matchAddress, textSenderAddress);
            Assert.AreEqual(urlSenderAddress, rinkebyUrl); // url is OK  

            // Clear
            driver.SwitchTo().Window(driver.WindowHandles[1]).Close(); // close the tab
            driver.SwitchTo().Window(driver.WindowHandles[0]); // get back to the main window
        }


        /// <summary>
        /// Verify modal transaction details
        /// Luniverse info page
        /// </summary>
        /// <param name="driver"></param>
        public static void TransactionsModalDetailsLuniverse(IWebDriver driver)
        {
            // Get text for TX hash and make sure that the URL contains the hash
            string textTxHash = driver.FindElement(By.CssSelector("div.transaction-detail-body > a")).Text;
            string urlTx = driver.FindElement(By.CssSelector("div.transaction-detail-body > a")).GetAttribute("href");
            bool txHashMatch = urlTx.Contains(textTxHash);

            // Get text for sender address and make sure that the URL contains the same sender address
            string textSenderAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(2) > div.transaction-detail-body > a")).Text;
            string urlSenderAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(2) > div.transaction-detail-body > a")).GetAttribute("href");
            bool senderAddressMatch = urlSenderAddress.Contains(textSenderAddress);

            // Get text for receiver address and make sure that the URL contains the same receiver address
            string textReceiverAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(3) > div.transaction-detail-body > a")).Text;
            string urlReceiverAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(3) > div.transaction-detail-body > a")).GetAttribute("href");
            bool receiverAddressMatch = urlReceiverAddress.Contains(textReceiverAddress);

            // Verify details on Luniverse page
            driver.FindElement(By.CssSelector("div.transaction-detail-body > a")).Click();  // click the Luniverse transaction link 
            Thread.Sleep(5000);
            var popup = driver.WindowHandles[1];  // handler for the new tab
            driver.SwitchTo().Window(popup);  // switch to tab
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
            Shared.Screenshot(driver, "Luniverse - Before wait");
            wait.Until<IWebElement>(c => c.FindElement(By.CssSelector("#app > article > main > section > div:nth-child(1) > p")));
            Shared.Screenshot(driver, "Luniverse - After wait");
            string luniverseUrl = driver.SwitchTo().Window(popup).Url; // get tab URL
            string luniverseTxHash = driver.FindElement(By.CssSelector("#app > article > main > section > div:nth-child(1) > p")).Text; // get the tx hash on luniverse
            bool lvTxHashMatch = luniverseTxHash.Contains(textTxHash);

            // Assert
            Assert.IsTrue(txHashMatch, $"Expected URL to contain {textTxHash}, but actual URL was {urlTx}");
            Assert.IsTrue(txHashMatch, $"Expected URL to contain {textSenderAddress}, but actual URL was {urlSenderAddress}");
            Assert.IsTrue(txHashMatch, $"Expected URL to contain {textReceiverAddress}, but actual URL was {urlReceiverAddress}");
            Assert.AreEqual(urlTx, luniverseUrl); // url is OK  
            Assert.IsTrue(lvTxHashMatch, $"Expected Luniverse has to contain {textTxHash}, but it was {luniverseTxHash}");

            // Clear
            driver.SwitchTo().Window(driver.WindowHandles[1]).Close();  // close the tab
            driver.SwitchTo().Window(driver.WindowHandles[0]);  // get back to the main window
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Verify modal transaction details
        /// Goerli info page
        /// </summary>
        /// <param name="driver"></param>
        public static void TransactionsModalDetailsGoerli(IWebDriver driver)
        {
            // Get text for TX hash and make sure that the URL contains the hash
            string textTxHash = driver.FindElement(By.CssSelector("div.transaction-detail-body > a")).Text;
            string urlTx = driver.FindElement(By.CssSelector("div.transaction-detail-body > a")).GetAttribute("href");
            bool txHashMatch = urlTx.Contains(textTxHash);

            // Get text for sender address and make sure that the URL contains the same sender address
            string textSenderAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(2) > div.transaction-detail-body > a")).Text;
            string urlSenderAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(2) > div.transaction-detail-body > a")).GetAttribute("href");
            bool senderAddressMatch = urlSenderAddress.Contains(textSenderAddress);

            // Get text for receiver address and make sure that the URL contains the same receiver address
            string textReceiverAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(3) > div.transaction-detail-body > a")).Text;
            string urlReceiverAddress = driver.FindElement(By.CssSelector("div.modal-body > div:nth-child(3) > div.transaction-detail-body > a")).GetAttribute("href");
            bool receiverAddressMatch = urlReceiverAddress.Contains(textReceiverAddress);

            // Verify transaction details on Goerli page
            driver.FindElement(By.CssSelector("div.transaction-detail-body > a")).Click(); // click the goerli transaction link
            Thread.Sleep(5000);
            var popup = driver.WindowHandles[1]; // handler for the new tab
            driver.SwitchTo().Window(popup); // switch to tab
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
            string goerliUrl = driver.SwitchTo().Window(popup).Url;   //get tab Url

            // Assert
            if (driver.FindElements(By.Id("ContentPlaceHolder1_maintable")).Any())   // positive scenario
            {
                Shared.Screenshot(driver, "Goerli - Before wait");
                wait.Until<IWebElement>(c => c.FindElement(By.ClassName("col-md-9")));
                Shared.Screenshot(driver, "Goerli - After wait");
                string goerliTxHash = driver.FindElement(By.ClassName("col-md-9")).Text; // get the tx hash on goerli
                bool glTxHashMatch = goerliTxHash.Contains(textTxHash);

                Assert.IsTrue(txHashMatch, $"Expected URL to contain {textTxHash}, but actual URL was {urlTx}");
                Assert.IsTrue(txHashMatch, $"Expected URL to contain {textSenderAddress}, but actual URL was {urlSenderAddress}");
                Assert.IsTrue(txHashMatch, $"Expected URL to contain {textReceiverAddress}, but actual URL was {urlReceiverAddress}");
                Assert.AreEqual(urlTx, goerliUrl); // url is OK
                Assert.IsTrue(glTxHashMatch, $"Expected Goerli has to contain {textTxHash}, but it was {goerliTxHash}");

                driver.SwitchTo().Window(driver.WindowHandles[1]).Close();   // close the tab
                driver.SwitchTo().Window(driver.WindowHandles[0]);   // get back to the main window
                Thread.Sleep(2000);
            }
            else if(driver.FindElements(By.CssSelector("div.space-2.text-center > p")).Any())   // if unable to locate TxnHash
            {
                Assert.IsTrue(txHashMatch, $"Expected URL to contain {textTxHash}, but actual URL was {urlTx}");
                Assert.IsTrue(txHashMatch, $"Expected URL to contain {textSenderAddress}, but actual URL was {urlSenderAddress}");
                Assert.IsTrue(txHashMatch, $"Expected URL to contain {textReceiverAddress}, but actual URL was {urlReceiverAddress}");
                Assert.AreEqual(urlTx, goerliUrl);   // url is OK

                driver.SwitchTo().Window(driver.WindowHandles[1]).Close();   // close the tab
                driver.SwitchTo().Window(driver.WindowHandles[0]);   // get back to the main window
                Thread.Sleep(2000);
            }
            else
            {
                // Clear
                driver.SwitchTo().Window(driver.WindowHandles[1]).Close(); // close the tab
                driver.SwitchTo().Window(driver.WindowHandles[0]); // get back to the main window
                Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// No addreess link
        /// </summary>
        /// <param name="driver"></param>
        public static void NoAddress(IWebDriver driver)
        {
            Shared.FindElement(driver, By.XPath(".//*[@class='color-second']//a"), 30).Click(); ;
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Export button
        /// </summary>
        /// <param name="driver"></param>
        public static void TransactionClickExportButton(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("button.btn:nth-child(9)")));
            Shared.FindElement(driver, By.CssSelector("button.btn:nth-child(9)"), 30).Click();
        }
    }
}