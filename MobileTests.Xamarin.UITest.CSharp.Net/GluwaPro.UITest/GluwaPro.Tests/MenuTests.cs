using GluwaPro.UITest.TestUtilities.Methods;
using GluwaPro.UITest.TestUtilities.Methods.HomeMethod;
using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Methods.SendMethod;
using GluwaPro.UITest.TestUtilities.Methods.TestRailApiMethod;
using GluwaPro.UITest.TestUtilities.TestLogging;
using Gurock.TestRail;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace GluwaPro.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    [Category("MainMenu")]
    public class MenuTests
    {
        public static string walletPassword = Shared.walletPassword;
        public static string mnemonicString = Shared.MNOMONIC_STRING;
        private IApp app;
        private readonly Platform platform;
        private static string titleTestCase = Shared.titleTestCase;
        private static string enterInvalidPassword = Shared.enterInvalidpassword;
        private static string fromPrivateKey = "********";
        private static string btcPrivateKey = "***********";
        private static string tBTCPrivateKey = "***********";

        // Set true if you want to take screenshots for error debugging
        // By default should be false
        private readonly bool IS_SCREENSHOT = true;
        private bool isLocalizationTest = false;
        private ELanguage chosenLanguage;
        private const string PREPARE_TO_WRITE = HomeViewInfo.PREPARE_TO_WRITE;
        private const string ENTER_PASSWORD_VIEW = SendViewHandle.ENTER_PASSWORD_VIEW;
        private const string SEND_VIEW = SendViewHandle.SEND_VIEW;
        private const string MENU_VIEW = MenuViewInfo.MENU_VIEW;
        private const string SIGNATURE_ENTER_MESSAGE_VIEW = MenuViewInfo.SIGNATURE_ENTER_MESSAGE_VIEW;
        private const string SIGNATURE_MESSAGE_SIGNED_VIEW = MenuViewInfo.SIGNATURE_MESSAGE_SIGNED_VIEW;
        private const string IDENTIFICATION_VIEW = MenuViewInfo.IDENTIFICATION_VIEW;
        private const string IDENTIFICATION_LOGIN_VIEW = MenuViewInfo.IDENTIFICATION_LOGIN_VIEW;
        private const string PRIVATE_KEY_VIEW = MenuViewInfo.PRIVATE_KEY_VIEW;
        private const string APPEARANCE_VIEW = MenuViewInfo.APPEARANCE_VIEW;
        private const string LANGUAGE_VIEW = MenuViewInfo.LANGUAGE_VIEW;
        private const string SANDBOX_MODE_VIEW = MenuViewInfo.SANDBOX_MODE_VIEW;
        private const string PRIVATE_VIEW = MenuViewInfo.PRIVATE_VIEW;
        private ApiClient client;
        private dynamic jsonDataFromTestRail;
        private static int testIDForTestRail = Shared.testIDForTestRail;

        public MenuTests(Platform platform)
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
            app = AppInitializer.StartApp(platform);
            ScreenshotTools.IsTakePosScreenshot = IS_SCREENSHOT;
            ScreenshotTools.IsTakeErrorScreenshot = IS_SCREENSHOT;

            // Set up TestRail API
            client = Shared.SetApiClientForTestRail();
            jsonDataFromTestRail = Shared.SetTestRunForTestRail();
        }

        [Test, Order(0)]
        [Category("Positive")]
        public void Validate_Idenfitication_Pos()
        {
            // Arrange
            string userId = "*****";
            string userPassword = "******";
            Shared.GoToMenuPage(app, platform, false, true);
            MenuViewInfo.GetTitleIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // Ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelIdentification, IDENTIFICATION_VIEW);
            dynamic resultViewItem = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_IDENFITICATION;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userId, userPassword, IDENTIFICATION_LOGIN_VIEW);

            // Test Log in page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonLogin, IDENTIFICATION_LOGIN_VIEW);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationLoginViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_IDENFITICATION_LOG_IN;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);
            app.EnterText(platform, AutomationID.labelEnterLoginId, userId);
            app.EnterText(platform, AutomationID.labelEnterLoginPassword, userPassword);
            app.DismissKeyboard();
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonLogin, IDENTIFICATION_LOGIN_VIEW);
            MenuViewInfo.GetIdentificationLoginErrorViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // Go back to previous page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonBack, IDENTIFICATION_VIEW);

            // Test Sign up page
            dynamic resultViewItem3 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_IDENFITICATION_SIGN_UP;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);

            // Assert
            app.Tap(AutomationID.buttonSignup);
            ScreenshotTools.TakePosScreenshot(app, "Idenfitication Sing up Link");
            SharedViewHandle.HandleView(app, MENU_VIEW);
        }


        [Test, Order(1)]
        [Category("Positive")]
        public void Create_Signature_Pos()
        {
            // TestIDForTestRail = TestIDForTestRail.CREATE_SIGNATURE;
            string userSignMessage = "*****";
            Shared.GoToMenuPage(app, platform, false, true);
            MenuViewInfo.GetTitleSignatureViewItem(app, platform, chosenLanguage, isLocalizationTest);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelSignature, SIGNATURE_ENTER_MESSAGE_VIEW);
            MenuViewInfo.GetSignatureEnterMessageViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // Sign message
            app.EnterText(platform, AutomationID.labelEnterMessage, userSignMessage);
            app.DismissKeyboard();

            // Verify created signature
            // Assert view
            AppResult[] result = app.WaitForElement(AutomationID.labelEnterMessage, Shared.timeOutWaitingForString + " " + "element", Shared.oneMin);
            ScreenshotTools.TakePosScreenshot(app, AutomationID.labelSignature);
            Assert.IsTrue(result.Any());
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonSign, SIGNATURE_MESSAGE_SIGNED_VIEW);
            MenuViewInfo.GetSignatureViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // Verify back at Menu page
            // Assert view
            result = app.WaitForElement(AutomationID.labelMessageSigned, Shared.timeOutWaitingForString + " " + "menu page", Shared.oneMin);
            Assert.IsTrue(result.Any());
            Shared.ScrollDownToElement(app, platform, AutomationID.textCopySignature, AutomationID.viewSignatureMessageSigned);
            AppResult[] signatureAddressResult = app.WaitForElement(AutomationID.textSignatureAddress, Shared.timeOutWaitingForString + " " + "element", Shared.oneMin);
            Assert.IsTrue(signatureAddressResult.Any());

            if (platform == Platform.Android) // Android
            {
                Assert.IsTrue(signatureAddressResult.LastOrDefault().Text.Contains("0x"));
            } 
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonDone, MENU_VIEW);
            dynamic resultViewItem = MenuViewInfo.GetTitleMenuViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.CREATE_SIGNATURE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }


        [Test, Order(2)]
        [Category("Positive")]
        public void Validate_Private_Key_And_Address_Pos()
        {
            // TestIDForTestRail = TestIDForTestRail.VALIDATE_PRIVATE_KEY_AND_ADDRESS_PAGE_PRIVATE_KEY_AND_ADDRESS_PAGE;
            Shared.GoToMenuPage(app, platform, false, true);
            dynamic resultViewItem = MenuViewInfo.GetTitlePrivateKeysAndAddressesViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_PRIVATE_KEY_AND_ADDRESS_TITLE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelPrivate, PRIVATE_VIEW);
  
            // Verify valid PK and address
            app.EnterText(platform, AutomationID.labelEnterPassword, Shared.walletPassword);
            app.DismissKeyboard();
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, PRIVATE_KEY_VIEW);
            AppResult[] privateKeyUS = app.WaitForElement((AutomationID.labelPrivateKeyUSDCG), Shared.timeOutWaitingForString + " " + "PrivateKey label", Shared.oneMin);
            AppResult[] address = app.WaitForElement((AutomationID.labelAddressUSDCG), Shared.timeOutWaitingForString + " " + "element Address label", Shared.oneMin);
            ScreenshotTools.TakePosScreenshot(app, "Private Key and Address");
            Thread.Sleep(Shared.twoSec);
            dynamic resultViewItem4 = MenuViewInfo.GetPrivateKeysAddressViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_PRIVATE_KEY_AND_ADDRESS_MAINPAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);

            // Check available currencies
            Shared.ScrollDownToElement(app, platform, AutomationID.labelCurrencysNGNG);
            Shared.ScrollDownToElement(app, platform, AutomationID.labelCurrencyBTC);
            AppResult[] privateKeyBTC = app.WaitForElement(AutomationID.labelPrivateKeyBTC);
            AppResult[] addressBTC = app.Query(AutomationID.labelAddressBTC);
            AppResult[] privateKeytBTC = app.Query(AutomationID.labelTestnetPrivateKeyBTC);
            AppResult[] addresstBTC = app.Query(AutomationID.labelTestnetAddressBTC);

            // Verify BTC key
            if (platform == Platform.Android) // Android
            {
                Shared.ScrollDownToElement(app, platform, AutomationID.labelCurrencyBTC);
                //verify private key US
                Assert.IsTrue(privateKeyUS.LastOrDefault().Text.Contains(fromPrivateKey));
                Assert.AreEqual(Shared.fromAddress, address.LastOrDefault().Text);
                Shared.ScrollDownToElement(app, platform, AutomationID.labelTestnetAddressBTC);       
                ScreenshotTools.TakePosScreenshot(app, "BTC Private Key and Address");

                // Verify btcAddress
                Assert.IsTrue(privateKeyBTC.LastOrDefault().Text.Contains(btcPrivateKey));
                Assert.AreEqual(Shared.btcAddress, addressBTC.LastOrDefault().Text);

                // Verify tbtcAddress
                Assert.IsTrue(privateKeytBTC.LastOrDefault().Text.Contains(tBTCPrivateKey));
                Assert.AreEqual(Shared.tBTCAddress, addresstBTC.LastOrDefault().Text);
            }
            else // iOS
            {                
                Assert.IsTrue(privateKeyUS.LastOrDefault().Description.Contains(fromPrivateKey));
                Assert.IsTrue(address.LastOrDefault().Description.Contains(Shared.fromAddress));
                Shared.ScrollDownToElement(app, platform, AutomationID.labelTestnetAddressBTC);
                Assert.IsTrue(privateKeyBTC.LastOrDefault().Description.Contains(btcPrivateKey));
                Assert.IsTrue(addressBTC.LastOrDefault().Description.Contains(Shared.btcAddress));
                Assert.IsTrue(privateKeytBTC.LastOrDefault().Description.Contains(tBTCPrivateKey));
                Assert.IsTrue(addresstBTC.LastOrDefault().Description.Contains(Shared.tBTCAddress));
            }

            // Back to main menu
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonBack, MENU_VIEW);
            dynamic resultViewItem5 = MenuViewInfo.GetTitleMenuViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_PRIVATE_KEY_AND_ADDRESS_PAGE_GO_BACK;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
        }


        [Test, Order(3)]
        [Category("Positive")]
        public void Validate_Backup_Recovery_Phrase_Pos()
        {
            // TestIDForTestRail = TestIDForTestRail.VALIDATE_BACKUP_RECOVERY_PHRASE;
            Shared.GoToMenuPage(app, platform, false, true);
            dynamic resultViewItem = MenuViewInfo.GetTitleBackupRecoveryPhraseViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_BACKUP_RECOVERY_PHRASE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelBackupRecoveryPhrase, ENTER_PASSWORD_VIEW);
            SharedViewInfo.GetEnterPasswordViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // TestIDForTestRail = TestIDForTestRail.VALIDATE_BACKUP_RECOVERY_PHRASE_ENTER_INCORRECT_PASSWORD;
            Shared.EnterInvalidPassword(app, platform, enterInvalidPassword, AutomationID.buttonConfirm);
            dynamic resultViewItem2 = SharedViewInfo.GetErrorPasswordViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_BACKUP_RECOVERY_PHRASE_ENTER_INCORRECT_PASSWORD;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);
            SharedViewHandle.HandleView(app, ENTER_PASSWORD_VIEW);

            // TestIDForTestRail = TestIDForTestRail.VALIDATE_BACKUP_RECOVERY_PHRASE_ENTER_CORRECT_PASSWORD;
            app.EnterText(platform, AutomationID.labelEnterPassword, Shared.walletPassword);
            app.DismissKeyboard();
            dynamic resultViewItem3 = SharedViewInfo.GetEnterWalletPasswordViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_BACKUP_RECOVERY_PHRASE_ENTER_CORRECT_PASSWORD;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, AutomationID.labelPrepareToWrite);

            // TestIDForTestRail = TestIDForTestRail.VALIDATE_BACKUP_RECOVERY_PHRASE_PREPARE_TO_WRITE_DOWN_PAGE;
            Shared.CheckRecoveryPhrase(app, platform, chosenLanguage, isLocalizationTest, "BackupRecoverPhrase");
            dynamic resultViewItem4 = HomeViewInfo.GetYourWalletIsNowBackedUpViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_BACKUP_RECOVERY_PHRASE_YOUR_WALLET_IS_NOW_BACKED_UP;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonContinue, MENU_VIEW);

            // Assert
            AppResult[] sendVisible = app.WaitForElement(MENU_VIEW);
            Assert.IsTrue(sendVisible.Any());
            ScreenshotTools.TakePosScreenshot(app, MENU_VIEW);
        }


        [Test, Order(4)]
        [Category("Positive")]
        public void Validate_Appearance_Pos()
        {
            // Validate appearance - default
            Shared.GoToMenuPage(app, platform, false, true);
            MenuViewInfo.GetTitleAppearanceViewItem(app, platform, chosenLanguage, isLocalizationTest);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelAppearance, APPEARANCE_VIEW);
            dynamic resultViewItem = MenuViewInfo.GetAppearanceViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_APPEARANCE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            
            // Turn Darkmode on
            app.Tap(AutomationID.toggleDarkMode);
            dynamic resultViewItem2 = MenuViewInfo.GetAppearanceViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_APPEARANCE_DARK;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);
            ScreenshotTools.TakePosScreenshot(app, "Validate Appearance page - Dark mode");

            // Turn Darkmode off
            app.Tap(AutomationID.toggleDarkMode);
            dynamic resultViewItem3 = MenuViewInfo.GetAppearanceViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_APPEARANCE_LIGHT;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);
            ScreenshotTools.TakePosScreenshot(app, "Validate Appearance page - Normal mode");
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonBack, MENU_VIEW);
        }


        [Test, Order(5)]
        [Category("Positive")]
        public void Validate_Language_Pos()
        {
            // Validate appearance - default
            Shared.GoToMenuPage(app, platform, false, true);
            MenuViewInfo.GetTitleLanguageViewItem(app, platform, chosenLanguage, isLocalizationTest);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelLanguage, LANGUAGE_VIEW);
            dynamic resultViewItem = MenuViewInfo.GetLanguageViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_LANGUAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Change Language
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelKorean, LANGUAGE_VIEW);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, LANGUAGE_VIEW);

            // Validate Language page - Korean
            dynamic resultViewItem2 = MenuViewInfo.GetLanguageViewItem(app, platform, ELanguage.Korean, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_LANGUAGE_KOREAN;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);       
            if (platform == Platform.Android) { Assert.AreEqual(" 언어 ", resultViewItem2.TextHeaderBar); } // " 언어 " means "Language" in English
            ScreenshotTools.TakePosScreenshot(app, "Validate Language page - Korean");
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonBack, MENU_VIEW);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelLanguage, LANGUAGE_VIEW);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelEnglish, LANGUAGE_VIEW);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, LANGUAGE_VIEW);

            // Validate Language page - Korean
            dynamic resultViewItem3 = MenuViewInfo.GetLanguageViewItem(app, platform, ELanguage.English, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_LANGUAGE_ENGLISH;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);
            if (platform == Platform.Android) { Assert.AreEqual(" Language ", resultViewItem3.TextHeaderBar); }
            ScreenshotTools.TakePosScreenshot(app, "Validate Language page - English");         
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonBack, MENU_VIEW);
        }


        [Test, Order(6)]
        [Category("Positive")]
        public void Validate_Sandbox_Pos()
        {
            // Validate sandbox mode - default on view 
            titleTestCase = "Sandbox Mode";
            string offString = "Off";
            string onString = "On";
            Shared.GoToMenuPage(app, platform, false, true);
            Shared.ScrollDownToElement(app, platform, AutomationID.labelSandbox);
            ScreenshotTools.TakePosScreenshot(app, titleTestCase + " " + offString);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelSandbox, SANDBOX_MODE_VIEW);
            dynamic resultViewItem = MenuViewInfo.GetSandboxModeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_SANDBOX_MODE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Assert
            AppResult[] result = app.WaitForElement(AutomationID.toggleSandbox, Shared.timeOutWaitingForString + " " + "toggle sandbox", Shared.oneMin);
            ScreenshotTools.TakePosScreenshot(app, titleTestCase);
            Assert.IsTrue(result.Any());

            // Validate sandbox mode - cancel
            app.Tap(AutomationID.toggleSandbox);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonBack, MENU_VIEW);
            dynamic resultViewItem2 = MenuViewInfo.GetTitleMenuViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_SANDBOX_MODE_CANCEL;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelSandbox, SANDBOX_MODE_VIEW);
            app.Tap(AutomationID.toggleSandbox);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, SEND_VIEW);

            // Validate sandbox mode - confirm
            app.WaitForElement(AutomationID.buttonSandboxMode, Shared.timeOutWaitingForString + " " + titleTestCase + "element: buttonSandboxMode", Shared.oneMin);
            dynamic resultViewItem3 = SendViewInfo.GetSendViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_SANDBOX_MODE_CONFIRM;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);
            ScreenshotTools.TakePosScreenshot(app, titleTestCase + " " + onString);

            // Validate sandbox mode - off view 
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonSandboxMode, SANDBOX_MODE_VIEW);      
            app.Tap(AutomationID.toggleSandbox);  // Turn off sandbox mode
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, SEND_VIEW);
            app.WaitForElement(AutomationID.labelSendAmount, Shared.timeOutWaitingForString + " " + "element", Shared.oneMin);
            app.WaitForNoElement(AutomationID.buttonSandboxMode, Shared.timeOutWaitingForString + " " + titleTestCase + "element: buttonSandboxMode", Shared.oneMin);      
            dynamic resultViewItem4 = SendViewInfo.GetSendViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_SANDBOX_MODE_OFF;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);
            ScreenshotTools.TakePosScreenshot(app, titleTestCase + " " + offString);
        }


        [Test, Order(7)]
        [Category("Positive")]
        public void Validate_Support_Link_Pos()
        {
            // Validate Support Link
            Shared.GoToMenuPage(app, platform, false, true);
            Shared.ScrollDownToElement(app, platform, AutomationID.labelResetWallet, AutomationID.viewMenu);
            dynamic resultViewItem = MenuViewInfo.GetTitleSupportViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_SUPPORT_LINK;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            app.Tap(AutomationID.labelSupport);

            // Wait for page to load
            Thread.Sleep(Shared.fiveSec);
            ScreenshotTools.TakePosScreenshot(app, "Validate Support Link");
        }


        [Test, Order(8)]
        [Category("Positive")]
        public void Validate_User_Guide_Link_Pos()
        {
            // Validate User Guide Link
            Shared.GoToMenuPage(app, platform, false, true);
            Shared.ScrollDownToElement(app, platform, AutomationID.labelResetWallet, AutomationID.viewMenu);
            dynamic resultViewItem = MenuViewInfo.GetTitleUserGuideViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_USER_GUIDE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            app.Tap(AutomationID.labelUserGuide);

            // Wait for page to load
            Thread.Sleep(Shared.tenSec);
            ScreenshotTools.TakePosScreenshot(app, "Validate User Guide Link");
        }


        [Test, Order(9)]
        [Category("Positive")]
        public void Validate_Privacy_And_Terms_Pos()
        {
            // Validate Privacy and Terms link 
            Shared.GoToMenuPage(app, platform, false, true);
            Shared.ScrollDownToElement(app, platform, AutomationID.labelResetWallet, AutomationID.viewMenu);
            dynamic resultViewItem = MenuViewInfo.GetTitlePrivacyAndTermsViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_PRIVACY_AND_TERMS_LINK;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            app.Tap(AutomationID.labelPrivacyAndTerms);

            // Wait for page to load
            Thread.Sleep(Shared.fiveSec);
            ScreenshotTools.TakePosScreenshot(app, "Validate Privacy and Terms");
        }


        [Test, Order(10)]
        [Category("Positive")]
        public void Validate_Gluwa_Site_Link_Pos()
        {
            // Validate Gluwa Site Link"
            Shared.GoToMenuPage(app, platform, false, true);
            Shared.ScrollDownToElement(app, platform, AutomationID.labelResetWallet, AutomationID.viewMenu);
            dynamic resultViewItem = MenuViewInfo.GetTitleGluwaSiteViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.VALIDATE_GLUWA_SITE_LINK;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            app.Tap(AutomationID.labelGluwaSite);

            // Wait for page to load
            Thread.Sleep(Shared.threeSec);
            ScreenshotTools.TakePosScreenshot(app, "Validate Gluwa Site Link");
        }


        [Test, Order(11)]
        [Category("Positive")]
        public void Reset_Wallet_Pos()
        {
            // Validate Reset Link"
            titleTestCase = "Reset Wallet";
            Shared.GoToMenuPage(app, platform, false, true);
            Shared.ScrollDownToElement(app, platform, AutomationID.labelResetWallet, AutomationID.viewMenu);
            MenuViewInfo.GetTitleResetWalletViewItem(app, platform, chosenLanguage, isLocalizationTest);
            string buttonReset;
            if (platform == Platform.Android) { 
                buttonReset = AutomationID.labelResetWallet; 
            }
            else { if (chosenLanguage == ELanguage.English) {
                    buttonReset = "Reset"; 
                } 
                else { buttonReset = "초기화"; 
                } 
            }
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelResetWallet, buttonReset);
            Shared.HandlePopup(app, platform, titleTestCase, chosenLanguage, isLocalizationTest, buttonReset, AutomationID.labelWelcomeToGluwa);
            SharedViewHandle.HandleView(app, AutomationID.viewWelcomeHome);
        }
    }
}