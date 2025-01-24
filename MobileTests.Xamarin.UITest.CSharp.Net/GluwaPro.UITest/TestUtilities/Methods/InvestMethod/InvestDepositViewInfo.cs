using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Models.BondAccountViewModels;
using GluwaPro.UITest.TestUtilities.Models.InvestViewModels;
using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace GluwaPro.UITest.TestUtilities.Methods.InvestMethod
{
    class InvestDepositViewInfo
    {
        /// <summary>
        /// Get initial portfolio dash view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static AvailableToInvestViewItem GetInitialPortfolioDashboardViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext: (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string viewName = AutomationID.viewPortfolioDashboard;
            string TextAvailableNow;
            string TextInvestDescription;
            string TextTransfer;
            app.WaitForElement(viewName);
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextAvailableNow = app.Query(AutomationID.labelPortfolioDashboard).LastOrDefault().Text;
                    TextInvestDescription = app.Query(AutomationID.labelPortfolioDashboardAccount).LastOrDefault().Text;
                    TextTransfer = app.Query(AutomationID.labelTransfer).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextAvailableNow = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelPortfolioDashboard).Descendant(0))[0]);
                    TextInvestDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelPortfolioDashboardAccount).Descendant(0))[0]);
                    TextTransfer = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTransfer).Descendant(0))[0]);
                }
                AvailableToInvestViewItem result = new AvailableToInvestViewItem(
                    TextAvailableNow,
                    TextInvestDescription,
                    TextTransfer
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "PortfolioDashboard",
                    "InvestDescription1",
                    "Transfer",
                    };
                    string[] listOfTextsOnScreen = {
                    TextAvailableNow,
                    TextInvestDescription,
                    TextTransfer
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get send deposit view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static DepositViewItem GetSendingDepositViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext: (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string viewName = AutomationID.viewDeposit;
            string TextDeposit;
            string TextMax;
            string TextNext;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextDeposit = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    TextMax = app.Query(AutomationID.labelMax).LastOrDefault().Text;
                    TextNext = app.Query(AutomationID.buttonNextDisabled).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextDeposit = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    TextMax = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelMax).Descendant(0))[0]);
                    TextNext = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonNextDisabled).Descendant(0))[0]);
                }

                DepositViewItem result = new DepositViewItem(
                    TextDeposit,
                    TextMax,
                    TextNext
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "Deposit",
                    "Max",
                    "Next",
                    };
                    string[] listOfTextsOnScreen = {
                    TextDeposit,
                    TextMax,
                    TextNext
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get docu linked view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PleaseDocumentsLinkedViewItem GetPleaseDocumentsLinkedViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext: (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string viewName = AutomationID.viewPleaseDocumentsLinked;
            string TextPleaseDocumentsLinked;
            string TextInvestDescription;
            string TextInvestDescription2;
            string TextIAgree;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextPleaseDocumentsLinked = app.Query(AutomationID.labelPleaseDocumentsLinked).LastOrDefault().Text;
                    TextInvestDescription = app.Query(AutomationID.labelPleaseDocumentsLinkedParagraph1).LastOrDefault().Text;
                    TextInvestDescription2 = app.Query(AutomationID.labelPleaseDocumentsLinkedParagraph8).LastOrDefault().Text;
                    TextIAgree = app.Query(AutomationID.buttonIAgree).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextPleaseDocumentsLinked = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelPleaseDocumentsLinked).Descendant(0))[0]);
                    TextInvestDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelPleaseDocumentsLinkedParagraph1).Descendant(0))[0]);
                    TextInvestDescription2 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelPleaseDocumentsLinkedParagraph8).Descendant(0))[0]);
                    TextIAgree = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonIAgree).Descendant(0))[0]);
                }

                PleaseDocumentsLinkedViewItem result = new PleaseDocumentsLinkedViewItem(
                    TextPleaseDocumentsLinked,
                    TextInvestDescription,
                    TextInvestDescription2,
                    TextIAgree
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "PleaseDocumentsLinked",
                    "PleaseDocumentsLinkedParagraph1",
                    "PleaseDocumentsLinkedParagraph8",
                    "IAgree",
                };
                    string[] listOfTextsOnScreen = {
                    TextPleaseDocumentsLinked,
                    TextInvestDescription,
                    TextInvestDescription2,
                    TextIAgree
                };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get deposit transaction preview
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static DepositTransactionPreviewViewItem GetDepositTransactionPreviewViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext: (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string viewName = AutomationID.viewPleaseDocumentsLinked;
            string TextPleaseDocumentsLinked;
            string TextInvestDescription;
            string TextInvestDescription2;
            string TextContinue;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextPleaseDocumentsLinked = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    TextInvestDescription = app.Query(AutomationID.labelTotalsUSDCG).LastOrDefault().Text;
                    TextInvestDescription2 = app.Query(AutomationID.labelTransferFromOnPreview).LastOrDefault().Text;
                    TextContinue = app.Query(AutomationID.buttonContinue).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextPleaseDocumentsLinked = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    TextInvestDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTotalsUSDCG).Descendant(0))[0]);
                    TextInvestDescription2 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTransferFromOnPreview).Descendant(0))[0]);
                    TextContinue = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonContinue).Descendant(0))[0]);
                }
                DepositTransactionPreviewViewItem result = new DepositTransactionPreviewViewItem(
                    TextPleaseDocumentsLinked,
                    TextInvestDescription,
                    TextInvestDescription2,
                    TextContinue
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "PleaseDocumentsLinked",
                    "PleaseDocumentsLinkedParagraph1",
                    "PleaseDocumentsLinkedParagraph8",
                    "Continue"
                };
                    string[] listOfTextsOnScreen = {
                    TextPleaseDocumentsLinked,
                    TextInvestDescription,
                    TextInvestDescription2,
                    TextContinue
                };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get transaction submitted view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static TransactionSubmittedViewItem GetTransactionSubmittedViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext: (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string viewName = AutomationID.viewTransactionStatus;
            string textTransactionSubmitted;
            string textAmount;
            string textPending;
            string textTransaction;
            string textDone;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTransactionSubmitted = app.Query(AutomationID.labelTransactionStatus).LastOrDefault().Text;
                    textAmount = app.Query(AutomationID.labelAmountsUSDCG).LastOrDefault().Text;
                    textPending = app.Query(AutomationID.labelPending).LastOrDefault().Text;
                    textTransaction = app.Query(AutomationID.buttonTransactions).LastOrDefault().Text;
                    textDone = app.Query(AutomationID.buttonDone2).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTransactionSubmitted = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTransactionStatus).Descendant(0))[0]);
                    textAmount = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAmountsUSDCG).Descendant(0))[0]);
                    textPending = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelPending).Descendant(0))[0]);
                    textTransaction = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonTransactions).Descendant(0))[0]);
                    textDone = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonDone2).Descendant(0))[0]);
                };

                TransactionSubmittedViewItem result = new TransactionSubmittedViewItem(
                    textTransactionSubmitted,
                    textAmount,
                    textPending,
                    textTransaction,
                    textDone
                    );

                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "TransactionSubmitted",
                    "Amount",
                    "Pending",
                    "Transaction",
                    "Done"
                };
                    string[] listOfTextsOnScreen = {
                    textTransactionSubmitted,
                    textAmount,
                    textPending,
                    textTransaction,
                    textDone
                };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get bond account view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static BondAccountViewItem GetBondAccountViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext: (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string viewName = AutomationID.viewBondAccount;
            string textBondAccount;
            string textTotalBanance;
            string textAvailableToDrawdown;
            string textInterestAccrued;
            string textEffectiveAPY;
            string textRecentTransactions;
            string textDrawdown;
            string textDeposit;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textBondAccount = app.Query(AutomationID.labelAccount).LastOrDefault().Text;
                    textTotalBanance = app.Query(AutomationID.labelTotalBalance).LastOrDefault().Text;
                    textAvailableToDrawdown = app.Query(AutomationID.labelAvailableToDrawdown).LastOrDefault().Text;
                    textInterestAccrued = app.Query(AutomationID.labelInterestAccrued).LastOrDefault().Text;
                    textEffectiveAPY = app.Query(AutomationID.labelEffectiveAPY).LastOrDefault().Text;
                    textRecentTransactions = app.Query(AutomationID.labelRecentTransactions).LastOrDefault().Text;
                    textDrawdown = app.Query(AutomationID.buttonDrawdown).LastOrDefault().Text;
                    textDeposit = app.Query(AutomationID.buttonDeposit).LastOrDefault().Text;
                }
                else // iOS
                {
                    textBondAccount = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAccount).Descendant(0))[0]);
                    textTotalBanance = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTotalBalance).Descendant(0))[0]);
                    textAvailableToDrawdown = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAvailableToDrawdown).Descendant(0))[0]);
                    textInterestAccrued = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelInterestAccrued).Descendant(0))[0]);
                    textEffectiveAPY = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelEffectiveAPY).Descendant(0))[0]);
                    textRecentTransactions = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelRecentTransactions).Descendant(0))[0]);
                    textDrawdown = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonDrawdown).Descendant(0))[0]);
                    textDeposit = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonDeposit).Descendant(0))[0]);
                }

                BondAccountViewItem result = new BondAccountViewItem(
                    textBondAccount,
                    textTotalBanance,
                    textAvailableToDrawdown,
                    textInterestAccrued,
                    textEffectiveAPY,
                    textRecentTransactions,
                    textDrawdown,
                    textDeposit
                    );

                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "BondAccount",
                    "TotalBanance",
                    "AvailableToDrawdown",
                    "InterestAccrued",
                    "EffectiveAPY",
                    "RecentTransactions",
                    "Drawdown",
                    "Deposit"
                    };
                    string[] listOfTextsOnScreen = {
                    textBondAccount,
                    textTotalBanance,
                    textAvailableToDrawdown,
                    textInterestAccrued,
                    textEffectiveAPY,
                    textRecentTransactions,
                    textDrawdown,
                    textDeposit
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get transaction history view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static NoTransactionHistoryViewItem GetTransactionHistoryViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext: (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string viewName = AutomationID.viewTransactionsHistory;
            string textTransactions;
            string textDeposit;
            string textDrawdowns;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTransactions = app.Query(AutomationID.labelTransactionsHistory).LastOrDefault().Text;
                    textDeposit = app.Query(AutomationID.buttonDeposit).LastOrDefault().Text;
                    textDrawdowns = app.Query(AutomationID.buttonDrawdown).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTransactions = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTransactionsHistory).Descendant(0))[0]);
                    textDeposit = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonDeposit).Descendant(0))[0]);
                    textDrawdowns = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonDrawdown).Descendant(0))[0]);
                }

                NoTransactionHistoryViewItem result = new NoTransactionHistoryViewItem(
                    textTransactions,
                    textDeposit,
                    textDrawdowns
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "Transactions",
                    "Deposit",
                    "Drawdowns",
                    };
                    string[] listOfTextsOnScreen = {
                    textTransactions,
                    textDeposit,
                    textDrawdowns
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }
    }
}
