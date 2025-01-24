using GluwaPro.UITest.TestUtilities.Methods;
using GluwaPro.UITest.TestUtilities.Methods.HomeMethod;
using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Methods.SendMethod;
using GluwaPro.UITest.TestUtilities.Methods.TestRailApiMethod;
using GluwaPro.UITest.TestUtilities.TestLogging;
using Gurock.TestRail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Assert = NUnit.Framework.Assert;

namespace GluwaPro.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    [Category("Home")]
    public class HomeTests
    {
        public static string mnemonicString = Shared.MNOMONIC_STRING;
        private IApp app;
        private readonly Platform platform;
        private string titleTestCase = Shared.titleTestCase;
        private static string walletPassword = Shared.walletPassword;
        private static readonly string recoveryPhraseMustConsistOf24Words = "The recovery phrase must consist of 24 words.";
        private static readonly string recoveryPharseMoreThen24WordsEntered = "Recovery Phrase Invalid 24 Words Entered";
        private readonly bool IS_SCREENSHOT = true;
        private bool isLocalizationTest = false;
        private ELanguage chosenLanguage;
        private ApiClient client;
        private dynamic jsonDataFromTestRail;
        private int testIDForTestRail = Shared.testIDForTestRail;
        private const string PREPARE_TO_WRITE = HomeViewInfo.PREPARE_TO_WRITE;
        private const string SEND_VIEW = SendViewHandle.SEND_VIEW;
        private const string OPEN_GLUWA_WALLET = HomeViewInfo.OPEN_GLUWA_WALLET;
        private const string WELCOME_HOME_VIEW = HomeViewInfo.WELCOME_HOME_VIEW;
        private const string RESTORE_WALLET_VIEW = HomeViewInfo.RESTORE_WALLET_VIEW;
        private const string ENTER_RECOVERY_PHRASE = HomeViewInfo.ENTER_RECOVERY_PHRASE;
        private const string WELL_DONE_VIEW = HomeViewInfo.WELL_DONE_VIEW;

        public HomeTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void Setup()
        {
            // If you want to run localization test, please make isLocalizationTest true as isLocalizationTest = true;
            // Normally isLocalizationTest = false; 
            isLocalizationTest = false;

            // If you want to run localization test for other language(Korean), please make chosenLanguage (e.g. ELanguage.Korean;)
            // Normally isLocalizationTest = ELanguage.English; 
            chosenLanguage = ELanguage.English;

            // Prevent localization tests from running in App Center
            if (isLocalizationTest == true && Environment.GetEnvironmentVariable("APP_CENTER_TEST") == "1")
            {
                Assert.Inconclusive(String.Format("Cannot run tests on Production environment in App Center"));
            }

            // Start app
            app = AppInitializer.StartApp(platform);
            ScreenshotTools.IsTakePosScreenshot = IS_SCREENSHOT;
            ScreenshotTools.IsTakeErrorScreenshot = IS_SCREENSHOT;

            // Set up TestRail API
            client = Shared.SetApiClientForTestRail();
            jsonDataFromTestRail = Shared.SetTestRunForTestRail();
            if (isLocalizationTest) { Shared.ChangeLanguage(app, platform, chosenLanguage); }
        }

        [TestMethod]
        public void RestoreWallet(IApp app, Platform platform, bool bTakeScreenshots, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, string enterMnemonicString = Shared.MNOMONIC_STRING)
        {
            // Update pop-up handling
            Shared.CheckUpdatePopup(app, platform, chosenLanguage, isLocalizationTest);

            // Changed the test ID for v6
            SharedViewHandle.HandleView(app, WELCOME_HOME_VIEW);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, RESTORE_WALLET_VIEW);

            // Restore your wallet page
            // UI Test with its Automation ids & Localization test
            if (bTakeScreenshots) { ScreenshotTools.TakePosScreenshot(app, Shared.titleTestCase + " Intro"); }
            HomeViewInfo.GetRestoreYourWalletViewItem(app, platform, chosenLanguage, isLocalizationTest);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, ENTER_RECOVERY_PHRASE);

            // Enter recovery phrase page
            // UI Test with its Automation ids & Localization test
            HomeViewInfo.GetEnterRecoveryPhraseViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.EnterRecoveryPhrase(app, platform, enterMnemonicString);

            // Recovery Phrase page
            // UI Test with its Automation ids & Localization test           
            HomeViewInfo.GetRestoreWalletViewItem(app, platform, chosenLanguage, isLocalizationTest);
            if (bTakeScreenshots) { ScreenshotTools.TakePosScreenshot(app, Shared.titleTestCase + " Entered"); }
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, Shared.PICK_A_NEW_PASSWORD_VIEW);

            // Enter password page
            // UI Test with its Automation ids & Localization test
            dynamic resultViewItem = HomeViewInfo.GetPickANewPassword(app, platform, chosenLanguage, isLocalizationTest);
            Shared.testIDForTestRail = TestIDForTestRail.RESTORE_WALLET_PICK_A_NEW_PASSWORD;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, Shared.testIDForTestRail);
            Shared.EnterPasswordCommon(app, platform, false);
            if (bTakeScreenshots) { ScreenshotTools.TakePosScreenshot(app, Shared.titleTestCase + " - Completed"); }
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, SEND_VIEW);

            // Act - send page
            dynamic resultViewItem2 = SendViewInfo.GetSendViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.testIDForTestRail = TestIDForTestRail.RESTORE_WALLET;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, Shared.testIDForTestRail);
            AppResult[] sendVisible = app.WaitForElement(AutomationID.viewSend, Shared.timeOutWaitingForString + " " + Shared.titleTestCase, Shared.twoMin);

            // Assert
            Assert.IsTrue(sendVisible.Any());
            if (bTakeScreenshots) { ScreenshotTools.TakePosScreenshot(app, Shared.titleTestCase); }
        }


        [TestMethod]
        public void CreateWallet(IApp app, Platform platform, bool bTakeScreenshots, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            Shared.titleTestCase = "Create Wallet";

            // Update pop-up handling
            // UI Test with its Automation ids & Localization test
            Shared.CheckUpdatePopup(app, platform, chosenLanguage, isLocalizationTest);
            if (bTakeScreenshots) {
                ScreenshotTools.TakePosScreenshot(app, "Landing Page"); 
            }
            SharedViewHandle.HandleView(app, WELCOME_HOME_VIEW);

            // Welcome home page
            // UI Test with its Automation ids & Localization test
            HomeViewInfo.GetWelcomeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonCreateWallet, Shared.PICK_A_PASSWORD_VIEW);

            // Enter password page
            // UI Test with its Automation ids & Localization test
            HomeViewInfo.GetPickAPassword(app, platform, chosenLanguage, isLocalizationTest);
            bool isFullPasswordTest = false; // if you want to test full password test then it should be true
            Shared.EnterPasswordCommon(app, platform, isFullPasswordTest, chosenLanguage, isLocalizationTest);

            // Screenshot(titleTestCase + " - Completed");
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonCreateWallet, WELL_DONE_VIEW);

            // Act - done page
            // Assert view
            // Post result
            if (bTakeScreenshots)
            {
                dynamic resultViewItem = HomeViewInfo.GetWellDoneViewItem(app, platform, chosenLanguage, isLocalizationTest);
                Shared.testIDForTestRail = TestIDForTestRail.CREATE_WALLET;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, Shared.testIDForTestRail);
                Shared.titleTestCase = "Well Done";
                AppResult[] wellDoneVisible = app.WaitForElement(AutomationID.buttonBackupMyWallet, Shared.timeOutWaitingForString + " " + Shared.titleTestCase + " message", Shared.oneMin);
                ScreenshotTools.TakePosScreenshot(app, Shared.titleTestCase);
                Assert.IsTrue(wellDoneVisible.Any());
            }
            else
            {
                string buttonSkip;
                if (platform == Platform.Android) { 
                    buttonSkip = AutomationID.textSkip; 
                }
                else { 
                    if (chosenLanguage == ELanguage.English) {
                        buttonSkip = "Skip backup"; 
                    } else {
                        buttonSkip = "백업하지 않겠습니다"; 
                    } 
                }
                SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.textSkip, buttonSkip);
                dynamic resultViewItem = HomeViewInfo.GetAreYouSurePopupViewItem(app, platform, chosenLanguage, isLocalizationTest);
                Shared.testIDForTestRail = TestIDForTestRail.CREATE_WALLET_SKIP;
                Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, Shared.testIDForTestRail);
                Shared.HandlePopup(app, platform, Shared.titleTestCase, chosenLanguage, isLocalizationTest, buttonSkip, AutomationID.viewSend);
            }
        }


        [Test, Order(0)]
        [Category("SmokeTest"), Category("Positive")]
        public void Create_Wallet_Pos()
        {
            // With screenshots
            CreateWallet(app, platform, true, client, jsonDataFromTestRail, chosenLanguage, isLocalizationTest);
        }


        [Test, Order(1)]
        [Category("SmokeTest"), Category("Positive")]
        public void Create_Wallet_Skip_Pos()
        {
            // Without screenshots
            CreateWallet(app, platform, false, client, jsonDataFromTestRail, chosenLanguage, isLocalizationTest);
        }


        [Test, Order(2)]
        [Category("SmokeTest"), Category("Positive")]
        public void Restore_Wallet_Pos()
        {
            // With screenshots
            RestoreWallet(app, platform, false, client, jsonDataFromTestRail, chosenLanguage, isLocalizationTest);
        }


        [Test, Order(4)]
        [Category("SmokeTest"), Category("Positive")]
        public void Login_After_Restore_Wallet_Pos()
        {
            // Arrange
            // Restore wallet
            RestoreWallet(app, platform, false, client, jsonDataFromTestRail, chosenLanguage, isLocalizationTest);
            app = AppInitializer.StartApp(platform, restartOnly: true);

            // Open wallet page
            // UI Test with its Automation ids & Localization test
            SharedViewHandle.HandleView(app, OPEN_GLUWA_WALLET);
            HomeViewInfo.GetOpenWalletViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // Act - send view after restore wallet
            // Assert view
            // Post result
            app.EnterText(platform, AutomationID.labelEnterPassword, walletPassword);
            app.DismissKeyboard();
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonOpenWallet, SEND_VIEW);
            dynamic resultViewItem = SendViewInfo.GetSendViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.LOGIN_AFTER_RESOTRE_WALLET;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            AppResult[] sendVisible = app.WaitForElement(SEND_VIEW);

            // Assert
            Assert.IsTrue(sendVisible.Any());
            ScreenshotTools.TakePosScreenshot(app, SEND_VIEW);
        }


        [Test, Order(5)]
        [Category("SmokeTest"), Category("Positive")]
        public void Backup_Wallet_Pos()
        {
            // Arrange
            // Create wallet
            Create_Wallet_Pos();

            // Backup wallet page
            SharedViewHandle.HandleView(app, WELL_DONE_VIEW);

            // Well done page
            dynamic resultViewItem = HomeViewInfo.GetWellDoneViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.BACKUP_WALLET_WARNING_POP_UP;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Start backup page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonBackupMyWallet, PREPARE_TO_WRITE);
            ScreenshotTools.TakePosScreenshot(app, "Start Backup");
            Shared.CheckRecoveryPhrase(app, platform, chosenLanguage, isLocalizationTest);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonContinue, SEND_VIEW);

            // Send page
            dynamic resultViewItem2 = SendViewInfo.GetSendViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.BACKUP_WALLET;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);
            AppResult[] sendVisible = app.WaitForElement(SEND_VIEW);

            // Assert
            Assert.IsTrue(sendVisible.Any());
            ScreenshotTools.TakePosScreenshot(app, SEND_VIEW);
        }

       
        [Test, Order(3)]
        [Category("SmokeTest"), Category("Negative")]
        public void Restore_Wallet_Less_Or_More_Words_Neg()
        {
            // Arrange
            SharedViewHandle.HandleView(app, WELCOME_HOME_VIEW);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, RESTORE_WALLET_VIEW);

            // Restore wallet page
            // UI Test with its Automation ids & Localization test
            HomeViewInfo.GetRestoreYourWalletViewItem(app, platform, chosenLanguage, isLocalizationTest);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, ENTER_RECOVERY_PHRASE);
            ScreenshotTools.TakePosScreenshot(app, "Restore Wallet Intro");

            // Recovery phrase page
            // Enter less than 24 words phrase
            // UI Test with its Automation ids & Localization test
            string localizationPrimaryKey;
            string shorterString = Shared.MNOMONIC_STRING.Split(new char[] { ' ' }, 2)[1];
            Shared.EnterRecoveryPhrase(app, platform, shorterString);
            ScreenshotTools.TakePosScreenshot(app, recoveryPharseMoreThen24WordsEntered);

            // Act1 - attempt to restore wallet(<24)
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, ENTER_RECOVERY_PHRASE);
            localizationPrimaryKey = "MnemonicValidate2";
            dynamic resultViewItem = HomeViewInfo.GetEnterRecoveryPhraseErrorViewItem(app, platform, localizationPrimaryKey, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.ATTEMPT_RESTORE_WALLET_WITH_LESS_THAN_24_WORDS_IN_RECOVERY_PHRASE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            app.ClearText(AutomationID.labelMnemonic);

            // Enter invalid phrase
            string invalidString = shorterString + " invalid";
            Shared.EnterRecoveryPhrase(app, platform, invalidString);
            ScreenshotTools.TakePosScreenshot(app, recoveryPhraseMustConsistOf24Words);

            // Act2 - attempt to restore wallet(invalid phrase)
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, ENTER_RECOVERY_PHRASE);
            localizationPrimaryKey = "MnemonicValidate3";
            dynamic resultViewItem2 = HomeViewInfo.GetEnterRecoveryPhraseErrorViewItem(app, platform, localizationPrimaryKey, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.ATTEMPT_RESTORE_WALLET_WITH_INVALID_RECOVERY_PHRASE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);
            app.ClearText(AutomationID.labelMnemonic);

            // Enter invalid phrase
            string longerString = invalidString + " !";
            Shared.EnterRecoveryPhrase(app, platform, longerString);

            // Act3 - attempt to restore wallet(>24)
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonRestoreWallet, ENTER_RECOVERY_PHRASE);
            localizationPrimaryKey = "MnemonicValidate1";
            dynamic resultViewItem3 = HomeViewInfo.GetEnterRecoveryPhraseErrorViewItem(app, platform, localizationPrimaryKey, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.ATTEMPT_RESTORE_WALLET_WITH_MORE_THAN_24_WORDS_IN_RECOVERY_PHRASE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);
            AppResult[] sendVisible = app.WaitForElement(SEND_VIEW);

            // Assert
            Assert.IsTrue(sendVisible.Any());
            ScreenshotTools.TakePosScreenshot(app, SEND_VIEW);
        }        
    }
}