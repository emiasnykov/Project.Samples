using GluwaPro.UITest.TestUtilities.CurrencyUtils;
using GluwaPro.UITest.TestUtilities.Methods.HomeMethod;
using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Methods.SendMethod;
using GluwaPro.UITest.TestUtilities.Methods.TestRailApiMethod;
using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using Gurock.TestRail;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace GluwaPro.UITest.TestUtilities.Methods
{
    public static class Shared
    {
        // Timeouts
        public static TimeSpan oneMilliSec = new TimeSpan(0, 0, 0, 1);
        public static TimeSpan twoMilliSec = new TimeSpan(0, 0, 0, 2);
        public static TimeSpan oneSec = new TimeSpan(0, 0, 1);
        public static TimeSpan twoSec = new TimeSpan(0, 0, 2);
        public static TimeSpan threeSec = new TimeSpan(0, 0, 3);
        public static TimeSpan fiveSec = new TimeSpan(0, 0, 5);
        public static TimeSpan tenSec = new TimeSpan(0, 0, 10);
        public static TimeSpan oneMin = new TimeSpan(0, 1, 0);
        public static TimeSpan twoMin = new TimeSpan(0, 2, 0);
        public static TimeSpan fiveMin = new TimeSpan(0, 5, 0);

        // Wallet creds
        public static readonly string walletPassword = "*********************";
        public static readonly string sendAddress = "*********************";
        public static readonly string fromAddress = "*********************";
        public static readonly string tBtcToAddress = "*********************";
        public static readonly string btcToAddress = "*********************";
        public static readonly string btcAddress = "*********************";
        public static readonly string tBTCAddress = "*********************";

        // Shared messages
        public static readonly string timeOutWaitingForString = "Timed out waiting for";
        public static string enterInvalidpassword = "invalidpassword";
        public static string titleTestCase = "";
        public static string popupViewForIOS = "_UIAlertControllerView";
        public static string popupLabelForIOS = "UILabel";
        public static string checkNameForLocalization;
        public const string MNOMONIC_STRING = "rack walnut mango fancy tongue liar embark suit section era spoil drastic industry chuckle valid evidence butter start few tree eagle brisk debate nothing";
        
        // Api client
        public static ApiClient client;
        public static dynamic jsonDataFromTestRail;
        public static ETestResult testResult = ETestResult.Pass;
        public static int testIDForTestRail;

        // Shared views
        public const string RECOVERY_PHRASE_LIST_VIEW = HomeViewInfo.RECOVERY_PHRASE_LIST_VIEW;
        public const string DOUBLE_CHECK_VIEW = HomeViewInfo.DOUBLE_CHECK_VIEW;
        public const string YOUR_WALLET_IS_BACK_UP_VIEW = HomeViewInfo.YOUR_WALLET_IS_BACK_UP_VIEW;
        public const string WELCOME_HOME_VIEW = HomeViewInfo.WELCOME_HOME_VIEW;
        public const string PICK_A_PASSWORD_VIEW = HomeViewInfo.PICK_A_PASSWORD_VIEW;
        public const string WELL_DONE_VIEW = HomeViewInfo.WELL_DONE_VIEW;
        public const string RESTORE_WALLET_VIEW = HomeViewInfo.RESTORE_WALLET_VIEW;
        public const string ENTER_RECOVERY_PHRASE = HomeViewInfo.ENTER_RECOVERY_PHRASE;
        public const string PICK_A_NEW_PASSWORD_VIEW = HomeViewInfo.PICK_A_NEW_PASSWORD_VIEW;
        public const string SEND_VIEW = SendViewHandle.SEND_VIEW;
        public const string MENU_VIEW = MenuViewInfo.MENU_VIEW;
        public const string IDENTIFICATION_LOGIN_VIEW = MenuViewInfo.IDENTIFICATION_LOGIN_VIEW;
        private const int PROJECT_ID_OF_GLUWA_WALLET_APP = 9;
        private const int SUITE_ID_OF_GLUWA_WALLET_MOBILE_APP_V6 = 14;

        // Shared methods
        // TODO move to SharedFunctions

        /// <summary>
        /// Check names for localization tests
        /// </summary>
        /// <param name="languageToTest"></param>
        /// <param name="listOfPrimaryKeys"></param>
        /// <param name="listOfTextsOnScreen"></param>
        /// <param name="textCurrency"></param>
        /// <param name="testAmount"></param>
        public static void CheckLocalization(ELanguage languageToTest, string[] listOfPrimaryKeys, string[] listOfTextsOnScreen, ECurrency textCurrency = ECurrency.sUsdcg, string testAmount = "")
        {
            int indexForAutomationID = 0;
            string anotherSentance;
            foreach (string eachPrimaryKey in listOfPrimaryKeys)
            {
                checkNameForLocalization = Localization.GetSpreadsheetData(eachPrimaryKey, languageToTest);
                if (eachPrimaryKey == "YourPrivatekey")
                {
                    anotherSentance = Localization.GetSpreadsheetData("Your", languageToTest);
                    checkNameForLocalization = anotherSentance + " " + "private key";
                }
                else if (eachPrimaryKey == "YourAddress")
                {
                    anotherSentance = Localization.GetSpreadsheetData("Your", languageToTest);
                    checkNameForLocalization = anotherSentance + " " + "address";
                }
                else if (eachPrimaryKey == "YourTestnetprivatekey")
                {
                    anotherSentance = Localization.GetSpreadsheetData("Your", languageToTest);
                    checkNameForLocalization = anotherSentance + " " + "testnet private key";
                }
                else if (eachPrimaryKey == "YourTestnetAddress")
                {
                    anotherSentance = Localization.GetSpreadsheetData("Your", languageToTest);
                    checkNameForLocalization = anotherSentance + " " + "testnet address";
                }
                else if (eachPrimaryKey == "ResetWalletContent" || eachPrimaryKey == "TotalYourWalletIsStored")
                {
                    anotherSentance = Localization.GetSpreadsheetData(eachPrimaryKey, languageToTest);
                    checkNameForLocalization = anotherSentance.Replace("\\n", "\n");
                }
                else if (eachPrimaryKey == "ReceiversAddressWithCurrency")
                {
                    string getCurrency = ECurrencyExtensions.ToGetCurrencyByMobileFormat(textCurrency);
                    anotherSentance = Localization.GetSpreadsheetData(eachPrimaryKey, languageToTest);
                    checkNameForLocalization = anotherSentance.Replace("#{params.currentNaming}", getCurrency);
                    checkNameForLocalization = anotherSentance.Replace("#{'\n'}", "\n");
                }
                else if (eachPrimaryKey == "FeeWithCurrency" || eachPrimaryKey == "YouSendWithCurrency" || eachPrimaryKey == "YouGetWithCurrency" || eachPrimaryKey == "TotalWithCurrency")
                {
                    string getCurrency = ECurrencyExtensions.ToGetCurrencyByMobileFormat(textCurrency);
                    anotherSentance = Localization.GetSpreadsheetData(eachPrimaryKey, languageToTest);
                    checkNameForLocalization = anotherSentance.Replace("#{currency}", getCurrency);
                }
                else if (eachPrimaryKey == "TitleSendWithCurrency" || eachPrimaryKey == "InsufficientFunds")
                {
                    string getCurrency = ECurrencyExtensions.ToGetCurrencyByMobileFormat(textCurrency);
                    anotherSentance = Localization.GetSpreadsheetData(eachPrimaryKey, languageToTest);
                    checkNameForLocalization = anotherSentance.Replace("#{amount}", testAmount);
                    checkNameForLocalization = anotherSentance.Replace("#{currency}", getCurrency);
                }
                else if (eachPrimaryKey == "EstimatedExchangeRate")
                {
                    string quoteExchangeRateBase, quoteExchangeRateCurrency, makerPrice, makerCurrency;
                    (quoteExchangeRateBase, quoteExchangeRateCurrency, makerPrice, makerCurrency) = ExchangeViewInfo.ParseExchangeRate(listOfTextsOnScreen[indexForAutomationID]);
                    checkNameForLocalization = checkNameForLocalization + " " + "(" + makerCurrency + "/" + quoteExchangeRateCurrency + ")";
                }                
                indexForAutomationID++;
                Assert.AreEqual(checkNameForLocalization, listOfTextsOnScreen[indexForAutomationID]);
            }
        }

        /// <summary>
        /// Set text
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platfortm"></param>
        /// <param name="marked"></param>
        /// <param name="text"></param>
        /// <param name="viewItem"></param>
        public static void EnterText(this IApp app, Platform platfortm, string marked, string text, string viewItem = "")
        {
            Thread.Sleep(oneSec); // To avoid Pick a new password issues
            if (platfortm == Platform.Android) // Android
            {                
                app.EnterText(marked, text);
                app.Query(e => e.Marked(marked).Invoke("setText", text));
            }
            else // iOS
            {
                if (viewItem != AutomationID.viewOpenGluwaWallet) { app.EnterText(marked, text); }
                else { app.Query(e => e.Marked(marked).Invoke("setText", text)); }
            }

        }

        /// <summary>
        /// Update popup
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="testCase"></param>
        /// <param name="automationID"></param>
        /// <param name="resultPageElement"></param>
        public static void CheckUpdatePopup(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, string testCase = "", string automationID = "", string resultPageElement = "")
        {
            if (testCase == "updatePopup")
            {
                AppResult[] result;
                if (platform == Platform.Android) // Android
                {
                    result = app.WaitForElement(AutomationID.alertTitle, timeOutWaitingForString + " " + "messsage", oneMin);
                }
                else // iOS
                {
                    result = app.WaitForElement(popupViewForIOS, timeOutWaitingForString + " " + "messsage", oneMin);
                };

                // Assert - open popup
                ScreenshotTools.TakePosScreenshot(app, resultPageElement + " Page");
                Assert.IsTrue(result.Any());

                // UI Test with its Automation ids & Localization Test                
                dynamic resultViewItem = HomeViewInfo.GetSoftwareUpdatePopupViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.UPDATE_POPUP;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

                // Close popup
                app = AppInitializer.StartApp(platform, restartOnly: true); // Reopen app
                if (platform == Platform.Android) //  Android
                {
                    result = app.WaitForElement(AutomationID.alertTitle, timeOutWaitingForString + " " + "messsage", oneMin);
                }
                else  // iOS
                {
                    result = app.WaitForElement(popupViewForIOS, timeOutWaitingForString + " " + "messsage", oneMin);
                };

                // Assert - close wallet
                ScreenshotTools.TakePosScreenshot(app, resultPageElement + " Page");
                Assert.IsTrue(result.Any());
                dynamic resultViewItem1 = HomeViewInfo.GetSoftwareUpdatePopupViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.UPDATE_POPUP_CLOSE_WALLET_WHILE_THE_POPUP_IS_OPEN;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem1, Shared.testResult, testIDForTestRail);

                // Test Update button
                if (platform == Platform.Android) // Android
                {
                    app.Tap(AutomationID.button1); // Update button of Update popup
                    // Impossible to get back Gluwa wallet tapping Back button via app.Back(), so just reopen the app
                    app = AppInitializer.StartApp(platform, restartOnly: true); // Reopen app
                }
                else // iOS
                {
                    app.Tap(c => c.ClassFull(popupViewForIOS).Index(1)); // Update button of Update popup
                }

                // Assert - verify popup updates 
                result = app.WaitForElement(automationID);
                dynamic resultViewItem2 = HomeViewInfo.GetSoftwareUpdatePopupViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.UPDATE_POPUP_UPDATE;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);

                // Assert - verify on the page
                ScreenshotTools.TakePosScreenshot(app, automationID + ": Menu Page");
                Assert.IsTrue(result.Any());

                // Test Not now button
                if (platform == Platform.Android)     // Android
                {
                    app.Tap(AutomationID.button2);    // Not now button of Update popup
                }
                else // iOS
                {
                    app.Tap(c => c.ClassFull(popupViewForIOS).Index(0)); // Not now button of Update popup
                };

                // Assert - not now case
                dynamic resultViewItem3 = HomeViewInfo.GetWelcomeViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.UPDATE_POPUP_NOT_NOW;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);
                result = app.WaitForElement(resultPageElement, timeOutWaitingForString + " " + "messsage", oneMin);
                ScreenshotTools.TakePosScreenshot(app, resultPageElement + " Page");
                Assert.IsTrue(result.Any());

                // Close popup
                app = AppInitializer.StartApp(platform, restartOnly: true); // Reopen app
                dynamic resultViewItem4 = HomeViewInfo.GetWelcomeViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.UPDATE_POPUP_CLOSE_WALLET_AFTER_TAPPING_NOT_NOW;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);
                if (platform == Platform.Android) // Android
                {
                    app.WaitForNoElement(AutomationID.alertTitle, timeOutWaitingForString + " " + "messsage", oneMin);
                }
                else // iOS
                {
                    app.WaitForNoElement(popupViewForIOS, timeOutWaitingForString + " " + "messsage", oneMin);
                }
                ScreenshotTools.TakePosScreenshot(app, resultPageElement + " Page");
            }
            else
            {
                app.WaitForElement(AutomationID.buttonCreateWallet, timeOutWaitingForString + " " + "messsage", twoMin);
            };

            // To double check to close the update popup for the regular testing
            try
            {
                // Test Not now button
                if (platform == Platform.Android) // Android
                {
                    app.Tap(AutomationID.button2);   // Not now button of Update popup
                }
                else // iOS
                {
                    app.Tap(c => c.ClassFull(popupViewForIOS).Index(0));   // Not now button of Update popup
                }
            }
            catch
            {
                SharedViewHandle.HandleView(app, WELCOME_HOME_VIEW);
            }
        }

        /// <summary>
        /// Handle popup actions
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="automationID"></param>
        /// <param name="resultPageElement"></param>
        /// <param name="isFullPopupTest"></param>
        public static void HandlePopup(IApp app, Platform platform, string testCase, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, string automationID = "", string resultPageElement = "", bool isFullPopupTest = false)
        {
            AppResult[] result;
            dynamic resultViewItem;
            int testIDForTestRailButton = testIDForTestRail;

            // UI Test with its Automation ids & Localization Test
            if ((automationID == AutomationID.textSkip && resultPageElement == AutomationID.viewSend) || (automationID == "Skip backup" && resultPageElement == AutomationID.viewSend))
            {                
                // Need to change for each page
                resultViewItem = HomeViewInfo.GetAreYouSurePopupViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.CREATE_WALLET_SKIP;
            }
            else if (automationID == AutomationID.buttonNext && resultPageElement == AutomationID.labelYourWalletIsBackedUp)
            {                
                // Need to change for each page
                resultViewItem = HomeViewInfo.GetWarningPopupViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.BACKUP_WALLET;
            }
            else
            {                
                resultViewItem = HomeViewInfo.GetWarningResetPopupViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.RESET_WALLET_CANCEL;
            }

            // Test cancel button
            if (isFullPopupTest)
            {
                if (platform == Platform.Android) // Android
                {
                    app.Tap(AutomationID.button2); // Skip  button of Warning popup
                }
                else // iOS
                {
                    app.Tap(c => c.ClassFull(popupLabelForIOS).Index(2));
                }
                testIDForTestRail = testIDForTestRailButton;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
                result = app.WaitForElement(automationID);

                // Verify on the page
                ScreenshotTools.TakePosScreenshot(app, automationID + ": Menu Page");
                Assert.IsTrue(result.Any());
                app.Tap(automationID);
            }

            // Test Reset button
            if (platform == Platform.Android) // Android
            {
                app.Tap(AutomationID.button1); // Reset button of Warning popup
            }
            else // iOS
            {
                app.Tap(c => c.ClassFull(popupLabelForIOS).Index(3)); // Reset button
            }            
            result = app.WaitForElement(resultPageElement, timeOutWaitingForString + " " + "messsage", oneMin);
            /*
            // Need to change for each page
            if ((automationID == AutomationID.textSkip && resultPageElement == AutomationID.viewSend) || (automationID == "Skip backup" && resultPageElement == AutomationID.viewSend))
            {
                // Need to change for each page
                resultViewItem = SendViewInfo.GetSendViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.CREATE_WALLET_SKIP;
            }
            else if (automationID == AutomationID.buttonNext && resultPageElement == AutomationID.labelYourWalletIsBackedUp)
            {
                // Need to change for each page
                resultViewItem = SendViewInfo.GetSendViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.BACKUP_WALLET;
            }
            else
            {
                resultViewItem = HomeViewInfo.GetWelcomeViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.RESET_WALLET_CONFIRM_RESET;
            } 
            */

            // Assert
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            ScreenshotTools.TakePosScreenshot(app, resultPageElement + " Page");
            Assert.IsTrue(result.Any());
        }

        /// <summary>
        /// Reset wallet
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="bTakeScreenshots"></param>
        /// <param name="client"></param>
        /// <param name="jsonDataFromTestRail"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="enterMnemonicString"></param>
        public static void RestoreWallet(IApp app, Platform platform, bool bTakeScreenshots, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, string enterMnemonicString = MNOMONIC_STRING)
        {
            // Update pop-up handling
            // Changed the test ID for v6
            CheckUpdatePopup(app, platform, chosenLanguage, isLocalizationTest); 
            SharedViewHandle.HandleView(app, WELCOME_HOME_VIEW);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, RESTORE_WALLET_VIEW);
            
            // Restore Your Wallet page
            // UI Test with its Automation ids & Localization Test
            if (bTakeScreenshots) { ScreenshotTools.TakePosScreenshot(app, titleTestCase + " Intro"); }
            HomeViewInfo.GetRestoreYourWalletViewItem(app, platform, chosenLanguage, isLocalizationTest);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, ENTER_RECOVERY_PHRASE);

            // Enter Recovery Phrase page
            // UI Test with its Automation ids & Localization Test
            HomeViewInfo.GetEnterRecoveryPhraseViewItem(app, platform, chosenLanguage, isLocalizationTest);
            EnterRecoveryPhrase(app, platform, enterMnemonicString);

            // Recovery Phrase page
            // UI Test with its Automation ids & Localization Test           
            HomeViewInfo.GetRestoreWalletViewItem(app, platform, chosenLanguage, isLocalizationTest);
            if (bTakeScreenshots) { ScreenshotTools.TakePosScreenshot(app, titleTestCase + " Entered"); }
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, PICK_A_NEW_PASSWORD_VIEW);

            // Enter Password page
            // UI Test with its Automation ids & Localization Test
            dynamic resultViewItem = HomeViewInfo.GetPickANewPassword(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.RESTORE_WALLET_PICK_A_NEW_PASSWORD;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, testResult, testIDForTestRail);
            EnterPasswordCommon(app, platform, false);
            if (bTakeScreenshots) { ScreenshotTools.TakePosScreenshot(app, titleTestCase + " - Completed"); }
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, SEND_VIEW);

            // Send page
            dynamic resultViewItem2 = SendViewInfo.GetSendViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.RESTORE_WALLET;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, testResult, testIDForTestRail);
            AppResult[] sendVisible = app.WaitForElement(AutomationID.viewSend, timeOutWaitingForString + " " + titleTestCase, twoMin);
            Assert.IsTrue(sendVisible.Any());
            if (bTakeScreenshots) { ScreenshotTools.TakePosScreenshot(app, titleTestCase); }
        }

        /// <summary>
        /// Create wallet
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="bTakeScreenshots"></param>
        /// <param name="client"></param>
        /// <param name="jsonDataFromTestRail"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        public static void CreateWallet(IApp app, Platform platform, bool bTakeScreenshots, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            //Update pop-up handling
            // UI Test with its Automation ids & Localization Test
            titleTestCase = "Create Wallet";
            CheckUpdatePopup(app, platform, chosenLanguage, isLocalizationTest);
            if (bTakeScreenshots) { ScreenshotTools.TakePosScreenshot(app, "Landing Page"); }
            SharedViewHandle.HandleView(app, WELCOME_HOME_VIEW);

            // Welcome Home Page
            // UI Test with its Automation ids & Localization Test
            HomeViewInfo.GetWelcomeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonCreateWallet, PICK_A_PASSWORD_VIEW);
            

            // Enter Password Page
            // UI Test with its Automation ids & Localization Test
            HomeViewInfo.GetPickAPassword(app, platform, chosenLanguage, isLocalizationTest);
            bool isFullPasswordTest = false; // if you want to test full password test then it should be true
            EnterPasswordCommon(app, platform, isFullPasswordTest, chosenLanguage, isLocalizationTest);
            //app.Screenshot(titleTestCase + " - Completed");
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonCreateWallet, WELL_DONE_VIEW);
                        
            if (bTakeScreenshots)
            {
                // Well Done page
                dynamic resultViewItem = HomeViewInfo.GetWellDoneViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.CREATE_WALLET;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, testResult, testIDForTestRail);
                titleTestCase = "Well Done";
                AppResult[] wellDoneVisible = app.WaitForElement(AutomationID.buttonBackupMyWallet, timeOutWaitingForString + " " + titleTestCase + " message", Shared.oneMin);
                ScreenshotTools.TakePosScreenshot(app, titleTestCase);
                Assert.IsTrue(wellDoneVisible.Any());
            }
            else
            {
                string buttonSkip;
                if (platform == Platform.Android) { buttonSkip = AutomationID.textSkip; }
                else { if (chosenLanguage == ELanguage.English) { buttonSkip = "Skip backup"; } else { buttonSkip = "백업하지 않겠습니다"; } }
                SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.textSkip, buttonSkip);
                dynamic resultViewItem = HomeViewInfo.GetAreYouSurePopupViewItem(app, platform, chosenLanguage, isLocalizationTest);
                testIDForTestRail = TestIDForTestRail.CREATE_WALLET_SKIP;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, testResult, testIDForTestRail);
                HandlePopup(app, platform, titleTestCase, chosenLanguage, isLocalizationTest, buttonSkip, AutomationID.viewSend);
            }
        }

        /// <summary>
        /// Tap exchange tab
        /// </summary>
        /// <param name="app"></param>
        public static void TapExchange(IApp app)
        {
            try
            {
                app.Tap(AutomationID.labelExchange);
                Thread.Sleep(2000);
                app.WaitForElement(AutomationID.labelExchangeSourceCurrency, timeOutWaitingForString + " " + "exchange page", oneMin); // To check to get Exchange page.
            }
            catch
            {
                ReplTools.StartRepl(app);
                MethodBase methodBase = MethodBase.GetCurrentMethod();
                Assert.Fail("ERROR from {0}.{1}", methodBase.ReflectedType.Name, methodBase.Name);
            }        
        }

        /// <summary>
        /// Tap invest tab
        /// </summary>
        /// <param name="app"></param>
        public static void TapInvest(IApp app)
        {
            try
            {
                app.Tap(AutomationID.labelInvest);                
            }
            catch
            {
                ReplTools.StartRepl(app);
                MethodBase methodBase = MethodBase.GetCurrentMethod();
                Assert.Fail("ERROR from {0}.{1}", methodBase.ReflectedType.Name, methodBase.Name);
            }
        }

        /// <summary>
        /// Tap back button
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        public static void PressBackButton(IApp app, Platform platform)
        {
            if (platform == Platform.Android) // Android
            {
                app.Tap(AutomationID.buttonBack);
            }
            else // iOS
            {
                app.Tap(c => c.Id(AutomationID.buttonBack));
            }
        }

        /// <summary>
        /// Tap change language
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        public static void ChangeLanguage(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            if (chosenLanguage == ELanguage.Korean)
            {
                // "언어" means "Language" in Korean
                string koreanLanguage = " 언어 ";
                GoToMenuPage(app, platform, false, true);
                app.Tap(AutomationID.labelLanguage);
                app.WaitForElement(AutomationID.labelKorean);
                app.Tap(AutomationID.labelKorean);
                app.Tap(AutomationID.buttonConfirm);
                Thread.Sleep(twoSec); // Wait for changing the Language
                if (platform == Platform.Android)
                {
                    Assert.AreEqual(koreanLanguage, app.Query(AutomationID.textHeaderBar).LastOrDefault().Text);
                }
                else
                {
                    Assert.IsTrue(app.Query(AutomationID.textHeaderBar).LastOrDefault().Description.Contains(koreanLanguage));
                }
                app.Tap(AutomationID.buttonBack);
                ScrollDownToElement(app, platform, AutomationID.labelResetWallet);
                HandlePopup(app, platform, titleTestCase, chosenLanguage, isLocalizationTest, AutomationID.labelResetWallet, AutomationID.labelWelcomeToGluwa);
            }
        }

        /// <summary>
        /// Tap menu tab
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="bReset"></param>
        /// <param name="bRestore"></param>
        public static void GoToMenuPage(IApp app, Platform platform, bool bReset, bool bRestore)
        {

            if (bReset == true)
            {
                CreateWallet(app, platform, false, client, jsonDataFromTestRail);
            }
            else if (bRestore == true)
            {
                RestoreWallet(app, platform, false, client, jsonDataFromTestRail);

            }
            app.WaitForElement(SEND_VIEW);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelMenu, MENU_VIEW);
        }

        /// <summary>
        /// Enter amount
        /// </summary>
        /// <param name="app"></param>
        /// <param name="amount"></param>
        public static void TapAmountCustom(IApp app, string amount)
        {
            foreach (char i in amount)
            {
                string numLabel;
                if (i == '.')
                {
                    numLabel = AutomationID.buttonDecimal;
                    app.Tap(c => c.Marked(numLabel));
                    Thread.Sleep(oneSec);
                }
                else if (i == ',')
                {
                    numLabel = ""; // To handle the ',' in currency amounts
                }
                else
                {
                    numLabel = "button" + i.ToString();
                    app.Tap(c => c.Marked(numLabel));
                    Thread.Sleep(oneSec);
                }
            }
        }


        /// <summary>
        /// Enter password
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="password"></param>
        public static void EnterWalletPassword(IApp app, Platform platform, string password)
        {
            titleTestCase = "Password";
            try
            {
                app.WaitForElement(AutomationID.labelEnterWalletPassword, timeOutWaitingForString + " " + titleTestCase + " " + "confirm", oneMin);
                app.EnterText(platform, AutomationID.labelEnterPassword, password);
            }
            catch
            {
                MethodBase methodBase = MethodBase.GetCurrentMethod();
                TestContext.WriteLine("ERROR from {0}.{1}", methodBase.ReflectedType.Name, methodBase.Name);
                ReplTools.StartRepl(app);
            }
            ScreenshotTools.TakePosScreenshot(app, titleTestCase + "View");
        }

        /// <summary>
        /// Check recovery phrase
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="testCaseFrom"></param>
        public static void CheckRecoveryPhrase(IApp app, Platform platform, ELanguage chosenLanguage, bool isLocalizationTest, string testCaseFrom = "")
        {
            string textString = "text";
            string selectWordString = "Select word";

            // Recovery Phrase Page
            // UI Test with its Automation ids & Localization Test
            dynamic resultViewItem = HomeViewInfo.GetRecoveryPhraseViewItem(app, platform, chosenLanguage, isLocalizationTest);
            if (testCaseFrom == "BackupRecoverPhrase")
            {
                testIDForTestRail = TestIDForTestRail.VALIDATE_BACKUP_RECOVERY_PHRASE_PREPARE_TO_WRITE_DOWN_PAGE;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            }
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonStart, RECOVERY_PHRASE_LIST_VIEW);
            ScreenshotTools.TakePosScreenshot(app, "Recovery Phrase");
            List<string> recoveryPhrase = new List<string>();
            AppResult[] rawValues = new AppResult[0];
            if (platform == Platform.Android) // Android
            {
                string reactScrollView = "reactScrollView";
                app.Tap(c => c.Class(reactScrollView));
                app.ScrollUp();
                rawValues = app.Query(c => c.Class(reactScrollView).Child().Child());

                foreach (AppResult value in rawValues)
                {
                    if (value.Text != null) 
                    {
                        recoveryPhrase.Add(value.Text);
                    }                   
                }

                app.Tap(c => c.Class(reactScrollView));
                app.ScrollDown();
                rawValues = app.Query(c => c.Class(reactScrollView).Child().Child());

                foreach (AppResult value in rawValues)
                {
                    if (value.Text != null)
                    {
                        recoveryPhrase.Add(value.Text);
                    }
                }

                recoveryPhrase = recoveryPhrase.Distinct().ToList();
                recoveryPhrase.Sort();
                foreach (string phrase in recoveryPhrase)
                {
                    Console.WriteLine(recoveryPhrase.ToString());
                }
            }
            else // iOS
            {
                app.WaitForElement(a => a.Marked(AutomationID.labelMnemonic).Child(0).Child(0));
                string phrase = app.Query(a => a.Marked(AutomationID.labelMnemonic).Child(0).Child(0)).FirstOrDefault().Label;
                recoveryPhrase = phrase.Split(' ').ToList();
            }

            // Recovery Test Page
            // UI Test with its Automation ids & Localization Test
            dynamic resultViewItem2 = HomeViewInfo.GetRecoveryPhraseListViewItem(app, platform, chosenLanguage, isLocalizationTest);
            if (testCaseFrom == "BackupRecoverPhrase")
            {
                testIDForTestRail = TestIDForTestRail.VALIDATE_BACKUP_RECOVERY_PHRASE_YOUR_RECOVERY_PHRASE_HAS_GENERATED;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);
            }

            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonNext, DOUBLE_CHECK_VIEW);
            dynamic resultViewItem3 = HomeViewInfo.GetDoubleCheckViewItem(app, platform, chosenLanguage, isLocalizationTest);
            if (testCaseFrom == "BackupRecoverPhrase")
            {
                testIDForTestRail = TestIDForTestRail.VALIDATE_BACKUP_RECOVERY_PHRASE_LETS_DOUBLE_CHECK_IT;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);
            }

            ScreenshotTools.TakePosScreenshot(app, "Recovery Test");
            AppResult[] recoveryWordTests = new AppResult[0];
            string[] recoveryWordTestsiOS = new string[0];
            if (platform == Platform.Android) // Android 
            {
                app.WaitForElement(c => c.Property(textString).Contains(selectWordString));
                recoveryWordTests = app.Query(c => c.Property(textString).Contains(selectWordString));
            }
            else // iOS
            {
                app.WaitForElement(AutomationID.textSelect1);
                app.WaitForElement(AutomationID.textSelect2);
                app.WaitForElement(AutomationID.textSelect3);
                app.WaitForElement(AutomationID.textSelect4);
                string textSelect1 = app.Query(AutomationID.textSelect1).LastOrDefault().Description.Split(';', ' ')[9];
                string textSelect2 = app.Query(AutomationID.textSelect2).LastOrDefault().Description.Split(';', ' ')[9];
                string textSelect3 = app.Query(AutomationID.textSelect3).LastOrDefault().Description.Split(';', ' ')[9];
                string textSelect4 = app.Query(AutomationID.textSelect4).LastOrDefault().Description.Split(';', ' ')[9];
                recoveryWordTestsiOS = new string[] { textSelect1, textSelect2, textSelect3, textSelect4 };
            }
            List<string> recoveryWordNumbers = new List<string>();

            if (platform == Platform.Android) // Android
            {
                foreach (AppResult word in recoveryWordTests)
                {
                    recoveryWordNumbers.Add(word.Text.Replace(selectWordString + " #", "") + '.');
                }
            }
            else // iOS
            {
                foreach (string word in recoveryWordTestsiOS)
                {
                    // Need to fix functional issue - Done                  
                    recoveryWordNumbers.Add(word.Replace("#", "") + '.');
                }
            }

            foreach (string number in recoveryWordNumbers)
            {
                string searchValue = number;
                string correctWord = string.Empty;
                correctWord = recoveryPhrase.Where(x => x.StartsWith(searchValue, StringComparison.Ordinal)).FirstOrDefault().Split('.')[1].Trim();
                app.Tap(correctWord);
            }
            ScreenshotTools.TakePosScreenshot(app, "Recovery Test - Completed");
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonNext, YOUR_WALLET_IS_BACK_UP_VIEW);
        }


        /// <summary>
        /// Enter recovery phrase
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="enterMnemonicString"></param>
        public static void EnterRecoveryPhrase(IApp app, Platform platform, string enterMnemonicString = MNOMONIC_STRING)
        {
            titleTestCase = "Recovery Phrase";
            app.EnterText(platform, AutomationID.labelMnemonic, enterMnemonicString);
            app.DismissKeyboard();
            Thread.Sleep(threeSec); // Wait for keyboard animation to go away
        }

        /// <summary>
        /// Test Password by Gluwa Password Policy
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="bFullPasswordTest"> if needs to be tested in pull password testing, it should be true, but normally it must be false</param>
        public static void EnterPasswordCommon(IApp app, Platform platform, bool isFullPasswordTest = false, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            app.WaitForElement(AutomationID.labelEnterPassword, timeOutWaitingForString + " " + "Enter wallet password text", oneMin);
            if (isFullPasswordTest)
            {
                // To get random password - random can not get value as regular-expressions
                string includeUpperLetter = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
                string includeLowerLetter = "abcdefghijkmnopqrstuvwxyz";
                string includeNumber = "0123456789";
                string includeSpecialCharacter = "!@$?_-#%^&*";
                string includeSpace = " ";
                int numberOfPassword = 8; // Password Policy - Password must be at least 8 characters long

                int[] listTempt = new int[] { numberOfPassword - 2, // To check "Must be at least 8 characters long"
                                              numberOfPassword,
                                              numberOfPassword + 4 // To check "Password strength is strong"
                                            };
                string[] listPasswordPolicy = new string[]
                {
                    includeUpperLetter, includeLowerLetter, includeNumber, includeSpecialCharacter, includeSpace ,
                    includeUpperLetter + includeLowerLetter + includeNumber + includeSpecialCharacter
                };
                foreach (int eachTempt in listTempt)
                {
                    foreach (string eachPasswordPolicy in listPasswordPolicy)
                    {
                        CheckNewPasswordPolicy(app, platform, eachPasswordPolicy, eachTempt, chosenLanguage, isLocalizationTest);
                    }
                }

                app.EnterText(platform, AutomationID.labelEnterPassword, walletPassword);
            }
            else
            {
                app.EnterText(platform, AutomationID.labelEnterPassword, walletPassword);
            }
            app.DismissKeyboard();
        }

        /// <summary>
        /// Generate password with given length
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomPassword(this string chars, int length)
        {
            var randomString = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
                randomString.Append(chars[random.Next(chars.Length)]);

            return randomString.ToString();
        }

        /// <summary>
        /// Check new password policies
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="passwordPolicy"></param>
        /// <param name="numberOfPassword"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        public static void CheckNewPasswordPolicy(IApp app, Platform platform, string passwordPolicy, int numberOfPassword, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            string localizationPrimaryKey;
            // To check the password policy
            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasUpperChar = new Regex(@"[A-Z]+");
            Regex hasLowerChar = new Regex(@"[a-z]+");
            Regex hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");


            string randomPassword = passwordPolicy.GetRandomPassword(numberOfPassword);

            app.EnterText(platform, AutomationID.labelEnterPassword, randomPassword);
            app.DismissKeyboard();
            if (randomPassword.Length < 8)
            {
                app.WaitForElement(AutomationID.labelPasswordValidate1);
                if (isLocalizationTest)
                {
                    // To check localzation - Must be at least 8 characters long
                    localizationPrimaryKey = "PasswordValidate1";
                    dynamic resultViewItem = HomeViewInfo.GetPasswordError(app, platform, localizationPrimaryKey, chosenLanguage, isLocalizationTest);
                    testIDForTestRail = TestIDForTestRail.PASSWORD_ERROR_MESSAGE_WITH_ONE_WORD;
                    Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
                    
                }
            }
            else if (string.IsNullOrWhiteSpace(randomPassword))
            {
                app.WaitForElement(AutomationID.labelPasswordValidate2);
                if (isLocalizationTest)
                {
                    // To check localzation - Must not contain space characters
                    localizationPrimaryKey = "PasswordValidate2";                    
                    dynamic resultViewItem = HomeViewInfo.GetPasswordError(app, platform, localizationPrimaryKey, chosenLanguage, isLocalizationTest);
                    testIDForTestRail = TestIDForTestRail.PASSWORD_ERROR_MESSAGE_WITH_SPACE;
                    Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
                }
            }
            else
            {
                if (!hasLowerChar.IsMatch(randomPassword) || !hasUpperChar.IsMatch(randomPassword) || !hasNumber.IsMatch(randomPassword) || !hasSymbols.IsMatch(randomPassword))
                {
                    app.WaitForElement(AutomationID.labelPasswordValidate3);
                    if (isLocalizationTest)
                    {
                        // To check localzation - Must contain at least 1 number, 1 upper letter, 1 lower letter and 1 special character
                        localizationPrimaryKey = "PasswordValidate3";
                        dynamic resultViewItem = HomeViewInfo.GetPasswordError(app, platform, localizationPrimaryKey, chosenLanguage, isLocalizationTest);
                        testIDForTestRail = TestIDForTestRail.PASSWORD_ERROR_MESSAGE_WITH_NUMBERS_AND_UPPER_LETTERS;
                        Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
                    }
                }
                else
                {
                    if (randomPassword.Length > 10)
                    {
                        app.WaitForElement(AutomationID.labelPasswordStrong);
                        if (isLocalizationTest)
                        {
                            // To check localzation - Password strength is strong
                            localizationPrimaryKey = "PasswordStrong";
                            dynamic resultViewItem = HomeViewInfo.GetPasswordError(app, platform, localizationPrimaryKey, chosenLanguage, isLocalizationTest);
                            testIDForTestRail = TestIDForTestRail.PASSWORD_STRONG;
                            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
                        }
                    }
                    else
                    {
                        app.WaitForElement(AutomationID.labelPasswordNormal);
                        if (isLocalizationTest)
                        {
                            // To check localzation - Password strength is normal
                            localizationPrimaryKey = "PasswordNormal";
                            dynamic resultViewItem = HomeViewInfo.GetPasswordError(app, platform, localizationPrimaryKey, chosenLanguage, isLocalizationTest);
                            testIDForTestRail = TestIDForTestRail.PASSWORD_NORMAL;
                            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
                        }
                    }
                }
            }
            app.ClearText(AutomationID.labelEnterPassword);
        }

        /// <summary>
        /// Check invalid password
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="invalidpassword"></param>
        /// <param name="button"></param>
        public static void EnterInvalidPassword(IApp app, Platform platform, string invalidpassword, string button)
        {
            // Test invalid password
            app.EnterText(platform, AutomationID.labelEnterPassword, invalidpassword);
            app.DismissKeyboard();
            if (platform == Platform.Android) // Android
            {
                app.Tap(c => c.Marked(button).Index(0));
                AppResult[] invalidPassword = app.WaitForElement("Invalid password");
                Assert.IsTrue(invalidPassword.Any());
            }
            else // iOS
            {
                if (button == AutomationID.buttonOpenWallet)
                {
                    app.Tap(c => c.Id(AutomationID.buttonOpenWallet));
                }
                else
                {
                    app.Tap(button);
                }
            }

            // Enter correct password
            app.ClearText(AutomationID.labelEnterPassword);
        }


        /// <summary>
        /// Tap done btn
        /// </summary>
        /// <param name="app"></param>
        public static void PressDoneButton(IApp app)
        {
            //Done button for first drop down
            try
            {
                app.Tap("done_button");
            }
            catch
            {
                bool bTextInput = app.Query("pickerExchangeSendCurrency").Any();
                while (bTextInput == false)
                {
                    Thread.Sleep(oneSec);
                    app.Tap("done_button");
                    bTextInput = app.Query("pickerExchangeSendCurrency").Any();
                }
            }
        }

        /// <summary>
        /// Scroll down until element
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="automationID"></param>
        /// <param name="viewAutomationID"></param>
        public static void ScrollDownToElement(IApp app, Platform platform, string automationID, string viewAutomationID = "")
        {
            if (platform == Platform.Android) // Android
            {
                try
                {
                    app.ScrollDownTo(automationID); // For "labelQuoteCreatedText"
                }
                catch
                {
                    app.ScrollTo(automationID);
                }
                if (automationID == AutomationID.labelResetWallet || automationID == AutomationID.labelAddSecurity)
                {
                    app.ScrollDown();
                }
            }
            else // iOS
            {
                if (automationID == AutomationID.labelAddSecurity)
                {
                    app.ScrollDown();
                }
                else if (automationID == AutomationID.textCopySignature || automationID == AutomationID.labelSynchronizing || automationID == AutomationID.labelQuoteAcceptedText || automationID == AutomationID.labelQuoteCreatedText || automationID == AutomationID.labelTestnetAddressBTC || automationID == AutomationID.labelCurrencyBTC || automationID == AutomationID.labelCurrencysNGNG || automationID == AutomationID.labelExchangedAmount || automationID == AutomationID.labelExchangedAmount || automationID == AutomationID.labelGrabFriendSpousePhone)
                {
                    app.ScrollDown(c => c.Class("UIScrollView"), ScrollStrategy.Auto);
                }
                else if (automationID == AutomationID.labelBitcoin || automationID == AutomationID.labelsNgnGluwacoin)
                {
                    app.ScrollDownTo(a => a.Marked(AutomationID.labelBitcoin), b => b.Marked(AutomationID.viewMyWallet), ScrollStrategy.Gesture, swipePercentage: 0.67);
                }
                else if (automationID == AutomationID.labelResetWallet)
                {
                    app.ScrollDownTo(a => a.Marked(automationID), b => b.Marked(viewAutomationID), ScrollStrategy.Gesture, swipePercentage: 0.67);
                }
                // Check Device size first: if the menu is not on current page, then scrolling down.
                else if (app.Query().First().Rect.CenterY < app.Query(automationID).First().Rect.CenterY)
                {
                    app.WaitForElement(automationID);
                    app.ScrollDown();
                }
            }
        }

        /// <summary>
        /// Scroll up until element
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="automationID"></param>
        public static void ScrollUpToElement(IApp app, Platform platform, string automationID)
        {
            app.ScrollUpTo(automationID);
        }

        public static void TapMyWallet(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            if (platform == Platform.Android) // Android
            {
                app.Tap(AutomationID.buttonMyWallet);
                try
                {
                    app.WaitForElement(AutomationID.viewMyWallet); // To check to get My Wallets page.
                }
                catch // Android tablet hack
                {
                    Thread.Sleep(threeSec);
                }
            }
            else // iOS
            {
                app.Tap(c => c.Id(AutomationID.buttonMyWallet));
                try
                {
                    app.WaitForElement(AutomationID.viewMyWallet);
                }
                catch // iPad hack
                {
                    Thread.Sleep(threeSec);
                }
            }
            SendViewInfo.GetMyWalletsViewItem(app, platform, chosenLanguage, isLocalizationTest);
        }

        /// <summary>
        /// Tap wallet then currency
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="currency"></param>
        /// <param name="viewPage"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        public static void TapMyWalletsAndTapCurrency(IApp app, Platform platform, ECurrency currency, string viewPage, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            try
            {
                TapMyWallet(app, platform, chosenLanguage, isLocalizationTest);
                string currencyString = ECurrencyExtensions.GetMyWalletCurrencyFullname(currency);
                ScrollDownToElement(app, platform, currencyString);
                app.Tap(currencyString);
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "Can't tap my wallet");
            }
            app.WaitForElement(viewPage, timeOutWaitingForString + " " + viewPage, oneMin);
        }

        public static void TurnOnSandboxMode(IApp app, Platform platform)
        {
            titleTestCase = "Sandbox Mode";
            try // Firstly, Check that Sandbox is not activated, then turn on Sandbox mode
            {
                app.WaitForNoElement(AutomationID.buttonSandboxMode, timeOutWaitingForString + " " + titleTestCase + " " + "button", Shared.oneMin);
                GoToMenuPage(app, platform, false, false);
                ScrollDownToElement(app, platform, AutomationID.labelSandbox); // To make sure to select Sandbox menu on small sized devices.
                app.Tap(AutomationID.labelSandbox);
                AppResult[] result = app.WaitForElement(AutomationID.toggleSandbox, timeOutWaitingForString + " " + "toggle sandbox", Shared.oneMin);
                ScreenshotTools.Screenshot(app, titleTestCase + " " + "Page");

                Assert.IsTrue(result.Any());

                app.Tap(AutomationID.toggleSandbox);
                app.Tap(AutomationID.buttonConfirm);

                app.WaitForElement(AutomationID.labelSendAmount, timeOutWaitingForString + " " + "element");
                ScreenshotTools.Screenshot(app, titleTestCase + " " + "Enabled");
            }
            catch // if Sandbox is activated, then let it be
            {
                app.WaitForElement(AutomationID.buttonSandboxMode, timeOutWaitingForString + " " + titleTestCase + " " + "button", Shared.oneMin);
                ScreenshotTools.Screenshot(app, titleTestCase + " " + "Enabled");
            }
        }

        /// <summary>
        /// Turn off sanbox mode
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        public static void TurnOffSandboxMode(IApp app, Platform platform)
        {
            titleTestCase = "Sandbox mode";
            try // Firstly, check that Sandbox is not activated, then let it be
            {
                app.WaitForNoElement(AutomationID.buttonSandboxMode, timeOutWaitingForString + " " + titleTestCase + " " + "button", Shared.oneMin);
                ScreenshotTools.Screenshot(app, titleTestCase + " " + "Disabled");
            }
            catch // if Sandbox is activated, then turn on Sandbox mode
            {
                app.WaitForElement(AutomationID.buttonSandboxMode, timeOutWaitingForString + " " + titleTestCase + " " + "button", Shared.oneMin);
                GoToMenuPage(app, platform, false, false);
                ScrollDownToElement(app, platform, AutomationID.labelSandbox); // To make sure to select Sandbox menu on small sized devices.
                app.Tap(AutomationID.labelSandbox);
                AppResult[] result = app.WaitForElement(AutomationID.toggleSandbox, timeOutWaitingForString + " " + "toggle sandbox", Shared.oneMin);
                ScreenshotTools.Screenshot(app, titleTestCase + " " + "Page");

                Assert.IsTrue(result.Any());
                app.Tap(AutomationID.toggleSandbox);
                app.Tap(AutomationID.buttonConfirm);
                app.WaitForElement(AutomationID.labelSendAmount, timeOutWaitingForString + " " + "element", Shared.oneMin);
                ScreenshotTools.Screenshot(app, titleTestCase + " " + "Disabled");
            }
        }

        /// <summary>
        /// Set API Client for TestRail
        /// </summary>
        /// <returns></returns>
        public static ApiClient SetApiClientForTestRail()
        {
            client = new ApiClient("************************");
            client.User = "***********************************";
            client.Password = "********************************";
            return client;
        }

        /// <summary>
        /// Based on 'One test run per day"
        /// If there is a test run already exist for day, get the test run ID 
        /// Otherwise, post new test run.
        /// </summary>
        /// <returns></returns>
        public static dynamic SetTestRunForTestRail()
        {
            dynamic checkTestRun = JObject.Parse(GetAllTestRunsForTestRail(client).ToString());
            if (checkTestRun.runs.Count == 0 )
            {
                JObject resultOfJSonData = PostNewTestRunForTestRail(client);
                jsonDataFromTestRail = JObject.Parse(resultOfJSonData.ToString(Newtonsoft.Json.Formatting.None));
            }
            else
            {
                dynamic data = checkTestRun.runs[0];
                JObject resultOfJSonData = Shared.GetSpecificTestRunForTestRail(client, data.id);
                jsonDataFromTestRail = JObject.Parse(resultOfJSonData.ToString(Newtonsoft.Json.Formatting.None));
            }
            return jsonDataFromTestRail;
        }

        /// <summary>
        /// Post test results to TestRail
        /// </summary>
        /// <param name="client"></param>
        /// <param name="testResult"></param>
        /// <param name="testRunID"></param>
        /// <param name="testCaseID"></param>
        public static void PostTestResultForTestRail(ApiClient client, dynamic testRunID, dynamic resultViewItem, ETestResult testResult, int testCaseID)
        {
            int statusId = 0;
            string testResultToComment = "";

            if (resultViewItem == null) { testResult = ETestResult.Failed; }

            switch (testResult)
            {
                case ETestResult.Pass:
                    statusId = 1;
                    testResultToComment = "Pass: This UI test works fine";
                    break;
                case ETestResult.Blocked:
                    statusId = 2;
                    testResultToComment = "Blocked: Make test status Blocked";
                    break;
                case ETestResult.Untested:
                    statusId = 3;
                    testResultToComment = "Untested: Make test status Untested";
                    break;
                case ETestResult.Retest:
                    statusId = 4;
                    testResultToComment = "Retest: Make test status Retest";
                    break;
                case ETestResult.Failed:
                    statusId = 5;
                    testResultToComment = "Failed: There is something wrong with testID or accessibilityLabel.";
                    break;
            }
            var data = new Dictionary<string, object>
            {
                { "status_id", statusId },
                { "comment", testResultToComment }
            };

            client.SendPost("add_result_for_case/" + testRunID + "/" + testCaseID.ToString(), data);
        }

        /// <summary>
        /// Post new test run to TestRail
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static JObject PostNewTestRunForTestRail(ApiClient client)
        {
            var data = new Dictionary<string, object>
            { 
                { "suite_id", SUITE_ID_OF_GLUWA_WALLET_MOBILE_APP_V6 },
                { "name", "Automation run: v6 " + DateTime.Today.ToString("d")}
            };
            JObject resultOfJSonData = (JObject)client.SendPost("add_run/" + PROJECT_ID_OF_GLUWA_WALLET_APP.ToString(), data);
            return resultOfJSonData;
        }


        /// <summary>
        /// Post new ss run to TestRail
        /// </summary>
        /// <param name="client"></param>
        /// <param name="testCaseID"></param>
        /// <returns></returns>
        public static JObject PostScreenshotForTestRail(ApiClient client, dynamic testCaseID)
        {
            JObject resultOfJSonData = (JObject)client.SendPost("add_attachment_to_case/" + testCaseID, @"C:\Users\Anonymous\Desktop\attachment.png");
            return resultOfJSonData;
        }

        /// <summary>
        /// Get test runs from TestRails
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static JObject GetAllTestRunsForTestRail(ApiClient client)
        {
            string unixTimestamp = Convert.ToString((int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            JObject resultOfJSonData = (JObject)client.SendGet("get_runs/" + PROJECT_ID_OF_GLUWA_WALLET_APP.ToString() + "&created_after=" + unixTimestamp);
            return resultOfJSonData;
        }

        /// <summary>
        /// Get the test run from TestRail
        /// </summary>
        /// <param name="client"></param>
        /// <param name="specificRunID"></param>
        /// <returns></returns>
        public static JObject GetSpecificTestRunForTestRail(ApiClient client, dynamic specificRunID)
        {
            JObject resultOfJSonData = (JObject)client.SendGet("get_run/" + specificRunID);
            return resultOfJSonData;
        }

        /// <summary>
        /// Need to set client_ids and client_secrets for both Sandbox and Prod in appSetting.js on GluwaPro Mobile
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="userId"></param>
        /// <param name="userPassword"></param>
        public static void LogInGluwa(IApp app, Platform platform, ELanguage chosenLanguage, bool isLocalizationTest, ApiClient client, dynamic jsonDataFromTestRail, string userId, string userPassword, string pageAfterLoggedIn, EPageNames pageInfo = EPageNames.Menu)
        {
            ETestResult testResult = ETestResult.Pass;
            int testCaseInTestRail; 
            // Test Log in page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonLogin, IDENTIFICATION_LOGIN_VIEW);
            testCaseInTestRail = TestIDForTestRail.INVEST_LOG_IN_PAGE;
            dynamic resultViewItem = MenuViewInfo.GetIdentificationLoginViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, testResult, testCaseInTestRail);

            /*
            //Enter wrong ID - Negative test
            EnterIdAndPassword(app, platform, userId + "wrong", userPassword, pageAfterLoggedIn);
            testCaseInTestRail = TestIDForTestRail.INVEST_REFERRAL_INVALIDE_USERNAME;
            dynamic resultInvalidLoginUsernameViewItem = SharedViewInfo.GetInvalidLoginUsernameViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultInvalidLoginUsernameViewItem, testResult, testCaseInTestRail);

            //Enter wrong Password - Negative test
            EnterIdAndPassword(app, platform, userId, userPassword + "wrong", pageAfterLoggedIn);
            testCaseInTestRail = TestIDForTestRail.INVEST_REFERRAL_INVALID_PASSWORD;
            dynamic resultInvalidLoginPasswordViewItem = SharedViewInfo.GetInvalidLoginPasswordViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultInvalidLoginPasswordViewItem, testResult, testCaseInTestRail);
            */
            // Enter correct Password - Positive test
            EnterIdAndPassword(app, platform, userId, userPassword, pageAfterLoggedIn);            
        }

        /// <summary>
        /// Enter ID and password
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="usderIdToTest"></param>
        /// <param name="userPasswordToTest"></param>
        /// <param name="pageAfterLoggedIn"></param>
        public static void EnterIdAndPassword(IApp app, Platform platform, string usderIdToTest, string userPasswordToTest, string pageAfterLoggedIn)
        {
            app.EnterText(platform, AutomationID.labelEnterLoginId, usderIdToTest);
            app.EnterText(platform, AutomationID.labelEnterLoginPassword, userPasswordToTest);
            app.DismissKeyboard();
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonLogin, pageAfterLoggedIn);
        }
    }
}