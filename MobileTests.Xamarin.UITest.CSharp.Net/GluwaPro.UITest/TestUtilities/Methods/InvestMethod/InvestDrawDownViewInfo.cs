using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Models.BondAccountViewModels;
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
    class InvestDrawDownViewInfo
    {
        /// <summary>
        /// Get bond account
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

            string TextBondAccount;
            string TextTotalBanance;
            string TextAvailableToDrawdown;
            string TextInterestAccrued;
            string TextEffectiveAPY;
            string TextRecentTransactions;
            string TextDrawdown;
            string TextDeposit;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextBondAccount = app.Query(AutomationID.labelAccount).LastOrDefault().Text;
                    TextTotalBanance = app.Query(AutomationID.labelTotalBalance).LastOrDefault().Text;
                    TextAvailableToDrawdown = app.Query(AutomationID.labelAvailableToDrawdown).LastOrDefault().Text;
                    TextInterestAccrued = app.Query(AutomationID.labelInterestAccrued).LastOrDefault().Text;
                    TextEffectiveAPY = app.Query(AutomationID.labelEffectiveAPY).LastOrDefault().Text;
                    TextRecentTransactions = app.Query(AutomationID.labelRecentTransactions).LastOrDefault().Text;
                    TextDrawdown = app.Query(AutomationID.buttonDrawdown).LastOrDefault().Text;
                    TextDeposit = app.Query(AutomationID.buttonDeposit).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextBondAccount = extractTextContext(app.Query(c => c.Id(AutomationID.labelAccount).Descendant(0))[0]);
                    TextTotalBanance = extractTextContext(app.Query(c => c.Id(AutomationID.labelTotalBalance).Descendant(0))[0]);
                    TextAvailableToDrawdown = extractTextContext(app.Query(c => c.Id(AutomationID.labelAvailableToDrawdown).Descendant(0))[0]);
                    TextInterestAccrued = extractTextContext(app.Query(c => c.Id(AutomationID.labelInterestAccrued).Descendant(0))[0]);
                    TextEffectiveAPY = extractTextContext(app.Query(c => c.Id(AutomationID.labelEffectiveAPY).Descendant(0))[0]);
                    TextRecentTransactions = extractTextContext(app.Query(c => c.Id(AutomationID.labelRecentTransactions).Descendant(0))[0]);
                    TextDrawdown = extractTextContext(app.Query(c => c.Id(AutomationID.buttonDrawdown).Descendant(0))[0]);
                    TextDeposit = extractTextContext(app.Query(c => c.Id(AutomationID.buttonDeposit).Descendant(0))[0]);
                }

                BondAccountViewItem result = new BondAccountViewItem(
                    TextBondAccount,
                    TextTotalBanance,
                    TextAvailableToDrawdown,
                    TextInterestAccrued,
                    TextEffectiveAPY,
                    TextRecentTransactions,
                    TextDrawdown,
                    TextDeposit
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
                    "Deposit",
                    };
                    string[] listOfTextsOnScreen = {
                    TextBondAccount,
                    TextTotalBanance,
                    TextAvailableToDrawdown,
                    TextInterestAccrued,
                    TextEffectiveAPY,
                    TextRecentTransactions,
                    TextDrawdown,
                    TextDeposit
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
        /// Get mature bond account balance
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static MatureBondAccountBalanceViewItem GetMatureBondAccountBalanceViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textMatureBondAccountBalance;
            string textDescription;
            string textAvailableAmount;
            string textUnavailableLockedAmount;
            string textDrawDown;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textMatureBondAccountBalance = app.Query(AutomationID.labelYouHaveAMatureBondAccountBalance).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelDrawdownDescriptionParagraph1).LastOrDefault().Text;
                    textAvailableAmount = app.Query(AutomationID.labelAvailableAmount).LastOrDefault().Text;
                    textUnavailableLockedAmount = app.Query(AutomationID.labelUnavailableLockedAmount).LastOrDefault().Text;
                    textDrawDown = app.Query(AutomationID.buttonDrawdown).LastOrDefault().Text;
                }
                else // iOS
                {
                    textMatureBondAccountBalance = extractTextContext(app.Query(c => c.Id(AutomationID.labelYouHaveAMatureBondAccountBalance).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelDrawdownDescriptionParagraph1).Descendant(0))[0]);
                    textAvailableAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelAvailableAmount).Descendant(0))[0]);
                    textUnavailableLockedAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelUnavailableLockedAmount).Descendant(0))[0]);
                    textDrawDown = extractTextContext(app.Query(c => c.Id(AutomationID.buttonDrawdown).Descendant(0))[0]);
                }

                MatureBondAccountBalanceViewItem result = new MatureBondAccountBalanceViewItem(
                    textMatureBondAccountBalance,
                    textDescription,
                    textAvailableAmount,
                    textUnavailableLockedAmount,
                    textDrawDown
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "YouHaveAMatureBondAccountBalance",
                    "DrawdownDescriptionParagraph1",
                    "AvailableAmount",
                    "UnavailableLockedAmount",
                    "Drawdown"
                    };
                    string[] listOfTextsOnScreen = {
                    textMatureBondAccountBalance,
                    textDescription,
                    textAvailableAmount,
                    textUnavailableLockedAmount,
                    textDrawDown
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
        /// Preview drawdown transaction
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static DrawdownTransactionPreviewViewItem GetDrawdownTransactionPreviewViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textTransactionPreview;
            string textTotalWithCurrency;
            string textTransferTo;
            string textDrawDown;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTransactionPreview = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    textTotalWithCurrency = app.Query(AutomationID.labelTotalsUSDCG).LastOrDefault().Text;
                    textTransferTo = app.Query(AutomationID.labelDrawdownTransferTo).LastOrDefault().Text;
                    textDrawDown = app.Query(AutomationID.buttonDrawdown).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTransactionPreview = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textTotalWithCurrency = extractTextContext(app.Query(c => c.Id(AutomationID.labelTotalsUSDCG).Descendant(0))[0]);
                    textTransferTo = extractTextContext(app.Query(c => c.Id(AutomationID.labelDrawdownTransferTo).Descendant(0))[0]);
                    textDrawDown = extractTextContext(app.Query(c => c.Id(AutomationID.buttonDrawdown).Descendant(0))[0]);
                }
                DrawdownTransactionPreviewViewItem result = new DrawdownTransactionPreviewViewItem(
                    textTransactionPreview,
                    textTotalWithCurrency,
                    textTransferTo,
                    textDrawDown
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "DrawdownTransactionPreview",
                    "TotalsUSDCG",
                    "DrawdownTransferTo",
                    "Drawdown"
                    };
                    string[] listOfTextsOnScreen = {
                    textTransactionPreview,
                    textTotalWithCurrency,
                    textTransferTo,
                    textDrawDown
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
