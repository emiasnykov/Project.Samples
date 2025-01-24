using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Models.HomeViewModels;
using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace GluwaPro.UITest.TestUtilities.Methods.HomeMethod
{
    /// <summary>
    /// Home page views
    /// </summary>
    class HomeViewInfo
    {
        public const string WELCOME_HOME_VIEW = "viewWelcomeHome";
        public const string PICK_A_PASSWORD_VIEW = "viewPickAPassword";
        public const string WELL_DONE_VIEW = "viewWellDone";
        public const string RESTORE_WALLET_VIEW = "viewRestoreYourWallet";
        public const string ENTER_RECOVERY_PHRASE = "viewEnterRecoveryPhrase";
        public const string PICK_A_NEW_PASSWORD_VIEW = "viewPickANewPassword";
        public const string PREPARE_TO_WRITE = "viewPrepareToWrite";
        public const string RECOVERY_PHRASE_LIST_VIEW = "viewRecoveryPhraseList";
        public const string DOUBLE_CHECK_VIEW = "viewDoubleCheck";
        public const string YOUR_WALLET_IS_BACK_UP_VIEW = "viewYourWalletIsBackUp";
        public const string OPEN_GLUWA_WALLET = "viewOpenGluwaWallet";        

        /// <summary>
        /// Get welcome view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PageWithTwoButtonsViewItem GetWelcomeViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textTitle;
            string textDescription;
            string textCreateWallet;
            string textRestoreWallet;

            app.WaitForElement(AutomationID.labelWelcomeToGluwa, "Timed out waiting for element " + AutomationID.labelWelcomeToGluwa, Shared.twoMin);
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.labelWelcomeToGluwa).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelWelcomeDescription).LastOrDefault().Text;
                    textCreateWallet = app.Query(AutomationID.buttonCreateWallet).LastOrDefault().Text;
                    textRestoreWallet = app.Query(AutomationID.buttonRestoreWallet).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.labelWelcomeToGluwa).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelWelcomeDescription).Descendant(0))[0]);
                    textCreateWallet = extractTextContext(app.Query(c => c.Id(AutomationID.buttonCreateWallet).Descendant(0))[0]);
                    textRestoreWallet = extractTextContext(app.Query(c => c.Id(AutomationID.buttonRestoreWallet).Descendant(0))[0]);
                }

                PageWithTwoButtonsViewItem result = new PageWithTwoButtonsViewItem(
                    textTitle,
                    textDescription,
                    textCreateWallet,
                    textRestoreWallet);
                if (isLocalizationTest)
                {
                    // To check localzation - Welcome page
                    string[] listOfPrimaryKeys = { "Welcome", "CreateWallet", "HaveAWalletAlready" };
                    string[] listOfTextsOnScreen = { textDescription, textCreateWallet, textRestoreWallet };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Welcome Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get restore your wallet view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PageWithOneButtonViewItem GetRestoreYourWalletViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textTitle;
            string textDescription;
            string textRestoreWallet;

            app.WaitForElement(AutomationID.labelRestoreYourWallet);
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.labelRestoreYourWallet).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelIfYouCreatedaBackup).LastOrDefault().Text;
                    textRestoreWallet = app.Query(AutomationID.buttonRestoreWallet).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.labelRestoreYourWallet).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelIfYouCreatedaBackup).Descendant(0))[0]);
                    textRestoreWallet = extractTextContext(app.Query(c => c.Id(AutomationID.buttonRestoreWallet).Descendant(0))[0]);
                }

                PageWithOneButtonViewItem result = new PageWithOneButtonViewItem(
                    textTitle,
                    textDescription,
                    textRestoreWallet);
                if (isLocalizationTest)
                {
                    // To check localzation - Restore Wallet page
                    string[] listOfPrimaryKeys = { "RestoreYourWallet", "IfYouCreatedaBackup", "RestoreWallet" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textRestoreWallet };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Restore Wallet Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get pick password view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PasswordWithDescriptionViewItem GetPickAPassword(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textTitle;
            string textDescription;
            string textCreateWallet;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.labelPickAPassword).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelSoNoOneElseButYouCan).LastOrDefault().Text;
                    textCreateWallet = app.Query(AutomationID.buttonCreateWallet).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.labelPickAPassword).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelSoNoOneElseButYouCan).Descendant(0))[0]);
                    textCreateWallet = extractTextContext(app.Query(c => c.Id(AutomationID.buttonCreateWallet).Descendant(0))[0]);
                }

                PasswordWithDescriptionViewItem result = new PasswordWithDescriptionViewItem(
                    textTitle,
                    textDescription,
                    textCreateWallet);
                if (isLocalizationTest)
                {
                    // To check localzation - Pick A Password page
                    string[] listOfPrimaryKeys = { "PickAPassword", "SoNoOneElseButYouCan", "CreateWallet" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textCreateWallet };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Pick a Password Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get password error view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="localizationPrimaryKey"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PasswordErrorViewItem GetPasswordError(IApp app, Platform platform, string localizationPrimaryKey, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string automationID;
            switch (localizationPrimaryKey)
            {
                case "PasswordValidate1":
                    app.WaitForElement(AutomationID.labelPasswordValidate1);
                    automationID = AutomationID.labelPasswordValidate1;
                    break;
                case "PasswordValidate2":
                    app.WaitForElement(AutomationID.labelPasswordValidate2);
                    automationID = AutomationID.labelPasswordValidate2;
                    break;
                case "PasswordValidate3":
                    app.WaitForElement(AutomationID.labelPasswordValidate3);
                    automationID = AutomationID.labelPasswordValidate3;
                    break;
                case "PasswordWeek":
                    app.WaitForElement(AutomationID.labelPasswordWeek);
                    automationID = AutomationID.labelPasswordWeek;
                    break;
                case "PasswordStrong":
                    app.WaitForElement(AutomationID.labelPasswordStrong);
                    automationID = AutomationID.labelPasswordStrong;
                    break;
                default:
                    app.WaitForElement(AutomationID.labelPasswordNormal);
                    automationID = AutomationID.labelPasswordNormal;
                    break;
            }

            string textPasswordValidate;
            if (platform == Platform.Android) // Android
            {
                textPasswordValidate = app.Query(automationID).LastOrDefault().Text;
            }
            else // iOS
            {
                textPasswordValidate = extractTextContext(app.Query(c => c.Id(automationID)).LastOrDefault());
            }
            try
            {
                PasswordErrorViewItem result = new PasswordErrorViewItem(
                    textPasswordValidate);
                if (isLocalizationTest)
                {
                    string[] listOfPrimaryKeys = { localizationPrimaryKey };
                    string[] listOfTextsOnScreen = { textPasswordValidate };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Password Error Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get well done view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PageWithTwoButtonsViewItem GetWellDoneViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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
            string textTitle;
            string textDescription;
            string textBackupMyWallet;
            string textSkip;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.labelWellDone).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelYourWalletIsStored).LastOrDefault().Text;
                    textBackupMyWallet = app.Query(AutomationID.buttonBackupMyWallet).LastOrDefault().Text;
                    textSkip = app.Query(AutomationID.textSkip).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.labelWellDone).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelYourWalletIsStored).Descendant(0))[0]);
                    textBackupMyWallet = extractTextContext(app.Query(c => c.Id(AutomationID.buttonBackupMyWallet).Descendant(0))[0]);
                    textSkip = extractTextContext(app.Query(c => c.Id(AutomationID.textSkip).Descendant(0))[0]);
                }
                PageWithTwoButtonsViewItem result = new PageWithTwoButtonsViewItem(
                    textTitle,
                    textDescription,
                    textBackupMyWallet,
                    textSkip);

                if (isLocalizationTest)
                {
                    // To check localzation - Well Done page
                    string[] listOfPrimaryKeys = { "WellDone", "TotalYourWalletIsStored", "BackupMyWallet", "Skip" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textBackupMyWallet, textSkip };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Well Done Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get are U sure popup view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static WarningPopupWithTwoButtonsViewItem GetAreYouSurePopupViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext = (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string textTitle;
            string textDescription;
            string textSkipBackup;
            string textCancle;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.alertTitle).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.message).LastOrDefault().Text;
                    textSkipBackup = app.Query(AutomationID.button1).LastOrDefault().Text;
                    textCancle = app.Query(AutomationID.button2).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[0]);
                    textDescription = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[1]);
                    textSkipBackup = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[2]);
                    textCancle = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[3]);
                }
                WarningPopupWithTwoButtonsViewItem result = new WarningPopupWithTwoButtonsViewItem(
                    textTitle,
                    textDescription,
                    textSkipBackup,
                    textCancle);
                if (isLocalizationTest)
                {
                    // To check localzation - Are You Sure Popup page
                    string[] listOfPrimaryKeys = { "AreYouSure", "YouRiskLosingYourFundsForever", "SkipBackup", "Cancel" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textSkipBackup, textCancle };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Warning Popup Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get software update view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static WarningPopupWithTwoButtonsViewItem GetSoftwareUpdatePopupViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext = (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string textTitle;
            string textDescription;
            string textUpdate;
            string textNotNow;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.alertTitle).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.message).LastOrDefault().Text;
                    textUpdate = app.Query(AutomationID.button1).LastOrDefault().Text;
                    textNotNow = app.Query(AutomationID.button2).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[0]);
                    textDescription = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[1]);
                    textUpdate = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[2]);
                    textNotNow = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[3]);
                }

                WarningPopupWithTwoButtonsViewItem result = new WarningPopupWithTwoButtonsViewItem(
                    textTitle,
                    textDescription,
                    textUpdate,
                    textNotNow);
                if (isLocalizationTest)
                {
                    // To check localzation - Are You Sure Popup page
                    string[] listOfPrimaryKeys = { "NewVersionAvailable", "ThereIsANewerVersion", "Update", "NotNow" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textUpdate, textNotNow };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Software Update Popup Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get recovery phrase view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PageWithOneButtonViewItem GetRecoveryPhraseViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textTitle;
            string textDescription;
            string textStart;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.labelPrepareToWrite).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelIfYourDeviceGetsLost).LastOrDefault().Text;
                    textStart = app.Query(AutomationID.buttonStart).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.labelPrepareToWrite).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelIfYourDeviceGetsLost).Descendant(0))[0]);
                    textStart = extractTextContext(app.Query(c => c.Id(AutomationID.buttonStart).Descendant(0))[0]);
                }
                PageWithOneButtonViewItem result = new PageWithOneButtonViewItem(
                    textTitle,
                    textDescription,
                    textStart);
                if (isLocalizationTest)
                {
                    // To check localzation - Recovery Phrase page
                    string[] listOfPrimaryKeys = { "TotalPrepareToWrite", "TotalIfYourDeviceGetsLost", "BackupStart" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textStart };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Recovery Phrase Information Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get recovery phrase list view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PageWithOneButtonViewItem GetRecoveryPhraseListViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textTitle;
            string textDescription;
            string textNext;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.labelRecoveryPhraseHasBeenGenerated).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelWriteDownTheseWords).LastOrDefault().Text;
                    textNext = app.Query(AutomationID.buttonNext).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.labelRecoveryPhraseHasBeenGenerated).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelWriteDownTheseWords).Descendant(0))[0]);
                    textNext = extractTextContext(app.Query(c => c.Id(AutomationID.buttonNext).Descendant(0))[0]);
                }

                PageWithOneButtonViewItem result = new PageWithOneButtonViewItem(
                    textTitle,
                    textDescription,
                    textNext);
                if (isLocalizationTest)
                {
                    // To check localzation - Recovery Phrase List page
                    string[] listOfPrimaryKeys = { "RecoveryPhraseHasBeenGenerated", "WriteDownTheseWords", "Next" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textNext };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Recovery Phrase List Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get double check view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static DoubleCheckViewItem GetDoubleCheckViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textLetsDoubleCheck;
            string textSelect1;
            string textSelect2;
            string textSelect3;
            string textSelect4;
            string textDone;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textLetsDoubleCheck = app.Query(AutomationID.labelLetsDoubleCheck).LastOrDefault().Text;
                    textSelect1 = app.Query(AutomationID.textSelect1).LastOrDefault().Text;
                    textSelect2 = app.Query(AutomationID.textSelect2).LastOrDefault().Text;
                    textSelect3 = app.Query(AutomationID.textSelect3).LastOrDefault().Text;
                    textSelect4 = app.Query(AutomationID.textSelect4).LastOrDefault().Text;
                    textDone = app.Query(AutomationID.buttonNext).LastOrDefault().Text;
                }
                else // iOS
                {
                    textLetsDoubleCheck = extractTextContext(app.Query(c => c.Id(AutomationID.labelLetsDoubleCheck).Descendant(0))[0]);
                    textSelect1 = extractTextContext(app.Query(c => c.Id(AutomationID.textSelect1).Descendant(0))[0]);
                    textSelect2 = extractTextContext(app.Query(c => c.Id(AutomationID.textSelect2).Descendant(0))[0]);
                    textSelect3 = extractTextContext(app.Query(c => c.Id(AutomationID.textSelect3).Descendant(0))[0]);
                    textSelect4 = extractTextContext(app.Query(c => c.Id(AutomationID.textSelect4).Descendant(0))[0]);
                    textDone = extractTextContext(app.Query(c => c.Id(AutomationID.buttonNext).Descendant(0))[0]);
                }
                DoubleCheckViewItem result = new DoubleCheckViewItem(
                    textLetsDoubleCheck,
                    textSelect1,
                    textSelect2,
                    textSelect3,
                    textSelect4,
                    textDone);
                if (isLocalizationTest)
                {
                    // To check localzation - Double Check page - Need to handle Select word ##{params.question} and ##{params.question}번 복구문자를 선택하세요
                    string[] listOfPrimaryKeys = { "LetsDoubleCheck", "SelectWord", "SelectWord", "SelectWord", "SelectWord", "Done" };
                    string[] listOfTextsOnScreen = { textLetsDoubleCheck, textSelect1, textSelect2, textSelect3, textSelect4, textDone };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Double Check Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get wallet backed up popup view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PageWithOneButtonViewItem GetYourWalletIsNowBackedUpViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textTitle;
            string textDescription;
            string textNext;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.labelYourWalletIsBackedUp).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelProtectYourRecoveryPhrase).LastOrDefault().Text;
                    textNext = app.Query(AutomationID.buttonContinue).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.labelYourWalletIsBackedUp).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelProtectYourRecoveryPhrase).Descendant(0))[0]);
                    textNext = extractTextContext(app.Query(c => c.Id(AutomationID.buttonContinue).Descendant(0))[0]);
                }
                PageWithOneButtonViewItem result = new PageWithOneButtonViewItem(
                    textTitle,
                    textDescription,
                    textNext);
                if (isLocalizationTest)
                {
                    // To check localzation - Recovery Phrase List page
                    string[] listOfPrimaryKeys = { "YourWalletIsBackedUp", "ProtectYourRecoveryPhrase", "Continue" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textNext };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Recovery Phrase List Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get warning reset popup view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static WarningPopupWithTwoButtonsViewItem GetWarningResetPopupViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext = (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string textTitle;
            string textDescription;
            string textReset;
            string textCancle;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.alertTitle).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.message).LastOrDefault().Text;
                    textReset = app.Query(AutomationID.button1).LastOrDefault().Text;
                    textCancle = app.Query(AutomationID.button2).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[0]);
                    textDescription = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[1]);
                    textReset = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[2]);
                    textCancle = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[3]);
                }
                WarningPopupWithTwoButtonsViewItem result = new WarningPopupWithTwoButtonsViewItem(
                    textTitle,
                    textDescription,
                    textReset,
                    textCancle);
                if (isLocalizationTest)
                {
                    // To check localzation - Recovery Phrase List page
                    string[] listOfPrimaryKeys = { "Warning", "ResetWalletContent", "Reset", "Cancel" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textReset, textCancle };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Warning Popup Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get warning popup view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static WarningPopupWithTwoButtonsViewItem GetWarningPopupViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext = (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string textTitle;
            string textDescription;
            string textFinish;
            string textCancle;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.alertTitle).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.message).LastOrDefault().Text;
                    textFinish = app.Query(AutomationID.button1).LastOrDefault().Text;
                    textCancle = app.Query(AutomationID.button2).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[0]);
                    textDescription = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[1]);
                    textFinish = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[2]);
                    textCancle = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[3]);
                }
                WarningPopupWithTwoButtonsViewItem result = new WarningPopupWithTwoButtonsViewItem(
                    textTitle,
                    textDescription,
                    textFinish,
                    textCancle);
                if (isLocalizationTest)
                {
                    // To check localzation - Recovery Phrase List page
                    string[] listOfPrimaryKeys = { "Warning", "OnceFinish", "Finish", "Cancel" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textFinish, textCancle };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Warning Popup Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get restore wallet view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PageWithOneButtonViewItem GetRestoreWalletViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textTitle;
            string textDescription;
            string textButton;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.labelRestoreYourWallet).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelIfYouCreatedaBackup).LastOrDefault().Text;
                    textButton = app.Query(AutomationID.buttonRestoreWallet).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.labelRestoreYourWallet).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelIfYouCreatedaBackup).Descendant(0))[0]);
                    textButton = extractTextContext(app.Query(c => c.Id(AutomationID.buttonRestoreWallet).Descendant(0))[0]);

                }
                PageWithOneButtonViewItem result = new PageWithOneButtonViewItem(
                    textTitle,
                    textDescription,
                    textButton);
                if (isLocalizationTest)
                {
                    // To check localzation - Restore Wallet page
                    string[] listOfPrimaryKeys = { "RestoreYourWallet", "IfYouCreatedaBackup", "RestoreWallet" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textButton };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Restore Wallet Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Enter recovery phrase view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EnterRecoverPhraseViewItem GetEnterRecoveryPhraseViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textTitle;
            string textButton;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.labelEnterRecoveryPhrase).LastOrDefault().Text;
                    textButton = app.Query(AutomationID.buttonRestoreWallet).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.labelEnterRecoveryPhrase).Descendant(0))[0]);
                    textButton = extractTextContext(app.Query(c => c.Id(AutomationID.buttonRestoreWallet).Descendant(0))[0]);

                }
                EnterRecoverPhraseViewItem result = new EnterRecoverPhraseViewItem(
                    textTitle,
                    textButton);
                if (isLocalizationTest)
                {
                    // To check localzation - Enter Recovery Phrase page
                    string[] listOfPrimaryKeys = { "EnterRecoveryPhrase", "RestoreWallet" };
                    string[] listOfTextsOnScreen = { textTitle, textButton };
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
        /// Recovery phrase error view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="localizationPrimaryKey"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static ErrorViewItem GetEnterRecoveryPhraseErrorViewItem(IApp app, Platform platform, string localizationPrimaryKey, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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
            string automationID;
            switch (localizationPrimaryKey)
            {
                case "MnemonicValidate1":
                    app.WaitForElement(AutomationID.labelMnemonicValidate1);
                    automationID = AutomationID.labelMnemonicValidate1;
                    break;
                case "MnemonicValidate2":
                    app.WaitForElement(AutomationID.labelMnemonicValidate2);
                    automationID = AutomationID.labelMnemonicValidate2;
                    break;
                default:
                    app.WaitForElement(AutomationID.labelMnemonicValidate3);
                    automationID = AutomationID.labelMnemonicValidate3;
                    break;
            }

            string textMnemonicValidate;
            if (platform == Platform.Android) // Android
            {
                textMnemonicValidate = app.Query(automationID).LastOrDefault().Text;
            }
            else // iOS
            {
                textMnemonicValidate = extractTextContext(app.Query(c => c.Id(automationID)).LastOrDefault());
            }
            try
            {
                ErrorViewItem result = new ErrorViewItem(
                    textMnemonicValidate);
                if (isLocalizationTest)
                {
                    string[] listOfPrimaryKeys = { localizationPrimaryKey };
                    string[] listOfTextsOnScreen = { textMnemonicValidate };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery PhraseError Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get pass w/o description view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PasswordWithNoDescriptionViewItem GetPickANewPassword(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textTitle;
            string textCreateWallet;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.labelPickANewPassword).LastOrDefault().Text;
                    textCreateWallet = app.Query(AutomationID.buttonRestoreWallet).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.labelPickANewPassword).Descendant(0))[0]);
                    textCreateWallet = extractTextContext(app.Query(c => c.Id(AutomationID.buttonRestoreWallet).Descendant(0))[0]);
                }
                PasswordWithNoDescriptionViewItem result = new PasswordWithNoDescriptionViewItem(
                    textTitle,
                    textCreateWallet);
                if (isLocalizationTest)
                {
                    // To check localzation - Pick A NewPassword page
                    string[] listOfPrimaryKeys = { "PickANewPassword", "RestoreWallet" };
                    string[] listOfTextsOnScreen = { textTitle, textCreateWallet };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Pick a Password Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get open wallet view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PageWithTwoButtonsViewItem GetOpenWalletViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string textTitle;
            string textDescription;
            string textOpenWallet;
            string textForgotPassword;

            app.WaitForElement(AutomationID.labelEnterPassword);
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.labelOpenGluwaWallet).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelEnterPassword).LastOrDefault().Text;
                    textOpenWallet = app.Query(AutomationID.buttonOpenWallet).LastOrDefault().Text;
                    textForgotPassword = app.Query(AutomationID.textForgotPassword).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.labelOpenGluwaWallet).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelEnterPassword).Descendant(0))[0]);
                    textOpenWallet = extractTextContext(app.Query(c => c.Id(AutomationID.buttonOpenWallet).Descendant(0))[0]);
                    textForgotPassword = extractTextContext(app.Query(c => c.Id(AutomationID.textForgotPassword).Descendant(0))[0]);

                }
                PageWithTwoButtonsViewItem result = new PageWithTwoButtonsViewItem(
                    textTitle,
                    textDescription,
                    textOpenWallet,
                    textForgotPassword);

                if (isLocalizationTest)
                {
                    // To check localzation - Open Wallet page
                    string[] listOfPrimaryKeys = { "OpenGluwaWallet", "OpenWallet", "ForgotPassword" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textOpenWallet, textForgotPassword };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Open Wallet Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }
    }
}
