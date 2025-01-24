using GluwaPro.UITest.TestUtilities.Methods;
using GluwaPro.UITest.TestUtilities.Methods.InvestMethod;
using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Methods.SendMethod;
using GluwaPro.UITest.TestUtilities.Methods.TestRailApiMethod;
using GluwaPro.UITest.TestUtilities.TestLogging;
using Gurock.TestRail;
using NUnit.Framework;
using System;
using System.Threading;
using Xamarin.UITest;

namespace GluwaPro.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    [Category("Invest")]
    public class InvestTest
    {
        private string userID = "********";
        private string userPassword = "********";
        private int testIDForTestRail;
        private IApp app;
        private readonly Platform platform;
        private readonly bool IS_SCREENSHOT = true;
        private bool isLocalizationTest = false;
        private ELanguage chosenLanguage;
        private ApiClient client;
        private dynamic jsonDataFromTestRail;
        private EPageNames pageInfo = EPageNames.Invest;

        public InvestTest(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void Setup()
        {
            // if you want to run localization test, please make isLocalizationTest true as isLocalizationTest = true;
            // Normally isLocalizationTest = false; 
            isLocalizationTest = false;

            // if you want to run localization test for other language(Korean), please make chosenLanguage (e.g. ELanguage.Korean;)
            // Normally isLocalizationTest = ELanguage.English; 
            chosenLanguage = ELanguage.English;

            // Prevent localization tests from running in App Center
            if (isLocalizationTest == true && Environment.GetEnvironmentVariable("APP_CENTER_TEST") == "1")
            {
                Assert.Inconclusive(String.Format("Cannot run tests on Production environment in App Center"));
            }
            ScreenshotTools.IsTakePosScreenshot = IS_SCREENSHOT;
            ScreenshotTools.IsTakeErrorScreenshot = IS_SCREENSHOT;

            // Set up TestRail API
            client = Shared.SetApiClientForTestRail();
            jsonDataFromTestRail = Shared.SetTestRunForTestRail();

            // Start app
            app = AppInitializer.StartApp(platform);
            Shared.RestoreWallet(app, platform, false, client, jsonDataFromTestRail, chosenLanguage, isLocalizationTest);
            Shared.TurnOnSandboxMode(app, platform);
            Shared.TapInvest(app);
            if (isLocalizationTest) { Shared.ChangeLanguage(app, platform, chosenLanguage); }
        }

        [Test]
        [Category("Positive")]
        public void Check_Invest_Pre_deposit_Identity_Verification_Required_Pos()
        {
            // Arrange
            userID = "mobiletest101";

            // To check to get Initial Invest page(Before Identity Verification).
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Invest ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);

            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);
            Thread.Sleep(Shared.threeSec); // To get the page

            // Invest pre-deposit page
            dynamic resultViewItem3 = InvestPreDepositViewInfo.GetIdentityVerificationRequiredViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_LETS_GET_YOU_VERIFIED;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);

            // Act - start session
            string buttonStartSession;
            if (platform == Platform.Android) { buttonStartSession = AutomationID.toolbar_btn_close; }
            else { buttonStartSession = "START SESSION"; }

            // Assert
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonBegin, buttonStartSession);
        }


        [Test]
        [Category("Negative")]
        public void Check_Invest_Pre_deposit_Personal_Information_Page_Neg()
        {
            // Arrange
            userID = "mobiletest100";

            // To check to get Initial Invest page(Before Identity Verification).
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Invest ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);

            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);
            Thread.Sleep(Shared.threeSec); // To get the page

            // Account setup
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE;
            dynamic resultViewItem3 = InvestPreDepositViewInfo.GetAccountSetupStepsCompletedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);

            // Act1 - incomplete personal info
            // Assert view
            // Post result
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE_PERSONAL_INFORMATION;
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.labelPersonalInformation);
            dynamic resultViewItem4 = InvestPreDepositViewInfo.GetAccountSetupStepsCheckListViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);

            // Act2 - incomplete invest info
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelPersonalInformationNotSubmitted, AutomationID.labelWellNeedALittleMoreInformationFirst);
            testIDForTestRail = TestIDForTestRail.INVEST_WE_NEED_INFORMATION_FIRST;
            dynamic resultViewItem5 = InvestPreDepositViewInfo.GetWellNeedALittleMoreInformationFirstViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
        }


        [Test]
        [Category("Negative")]
        public void Check_Invest_Pre_deposit_Proof_Of_Address_Page_Neg()
        {
            // Arrange
            userID = "mobiletest211";

            //To check to get Initial Invest page(Before Identity Verification).
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Invest ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);

            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);
            Thread.Sleep(Shared.threeSec); // To get the page

            // Account setup
            dynamic resultViewItem3 = InvestPreDepositViewInfo.GetAccountSetupStepsCompletedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);

            // Act - incomplete address info
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.labelProofOfAddressNotSubmitted);
            dynamic resultViewItem4 = InvestPreDepositViewInfo.GetAccountSetupStepsCheckListViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE_PROOF_OF_ADDRESS;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);
        }


        [Test]
        [Category("Negative")]
        public void Check_Invest_Pre_deposit_Address_Document_Required_Neg()
        {
            // Arrange
            userID = "mobiletest211";

            //To check to get Initial Invest page(Before Identity Verification).
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Invest ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);

            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);
            Thread.Sleep(Shared.threeSec); // To get the page

            // Account setup
            dynamic resultViewItem3 = InvestPreDepositViewInfo.GetAccountSetupStepsCompletedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);

            // Act - incomplete document
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelProofOfAddressNotSubmitted, AutomationID.labelAddressDocumentStatusRequired);
            dynamic resultViewItem5 = InvestPreDepositViewInfo.GetDocumentRequiredViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE_DOCUMENTS_REQUIRED;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonOK, AutomationID.labelAvailableNow);
        }


        [Test]
        [Category("Negative")]
        public void Check_Invest_Pre_deposit_Address_Document_Review_In_Progress_Neg()
        {
            // Arrange
            userID = "mobiletest212";

            // To check to get Initial Invest page(Before Identity Verification).
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Invest ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);

            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);
            Thread.Sleep(Shared.threeSec); // To get the page

            // Account setup
            dynamic resultViewItem3 = InvestPreDepositViewInfo.GetAccountSetupStepsCompletedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);

            // Act - incomplete document review(In progress)
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelProofOfAddressSubmitted, AutomationID.labelAddressDocumentStatusReviewInProgress);
            dynamic resultViewItem5 = InvestPreDepositViewInfo.GetReviewInProgressViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE_REVIEW_IN_PROGRESS;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonOK, AutomationID.labelAvailableNow);
        }


        [Test]
        [Category("Negative")]
        public void Check_Invest_Pre_deposit_Address_Document_Action_Required_Neg()
        {
            // Arrange
            userID = "mobiletest213";

            // To check to get Initial Invest page(Before Identity Verification).
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Invest ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);

            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);
            Thread.Sleep(Shared.threeSec); // To get the page

            // Account setup
            dynamic resultViewItem3 = InvestPreDepositViewInfo.GetAccountSetupStepsCompletedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);

            // Act - setup incomplete. Action required
            // Assert view
            // Post result
            dynamic resultViewItem5 = InvestPreDepositViewInfo.GetActionRequiredViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE_ACTION_REQUIRED;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonOK, AutomationID.labelAvailableNow);
        }


        [Test]
        [Category("Negative")]
        public void Check_Invest_Pre_deposit_Terms_And_Conditions_Agreement_Page_Neg()
        {
            // Arrange
            userID = "mobiletest103";

            //To check to get Initial Invest page(Before Identity Verification).
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Invest ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);

            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);
            Thread.Sleep(Shared.threeSec); // To get the page

            // Account setup
            dynamic resultViewItem3 = InvestPreDepositViewInfo.GetAccountSetupStepsCompletedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.labelProofOfAddress);

            // Act1 - setup incomplete. Checklist
            // Assert view
            // Post result
            dynamic resultViewItem4 = InvestPreDepositViewInfo.GetAccountSetupStepsCheckListViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE_TERMS_AND_CONDITIONS_AGREEMENT;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);

            // Act2 - observe linked documents page
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelTermsAndConditionsAgreementNotSubmitted, AutomationID.labelImportant);
            Thread.Sleep(Shared.threeSec); // To get the page
            dynamic resultViewItem5 = InvestPreDepositViewInfo.GetPleaseDocumentsLinkedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_PLEASE_VIEW_DOCUMENTS;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
        }


        [Test]
        [Category("Negative")]
        public void Check_Invest_Pre_deposit_OTP_Witness_Verification_Page_Neg()
        {
            // Arrange
            userID = "mobiletest104";
            string countryCode = "Afghanistan +93";

            //To check to get Initial Invest page(Before Identity Verification).
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Invest ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);

            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);
            Thread.Sleep(Shared.threeSec); // To get the page

            // Account setup
            dynamic resultViewItem3 = InvestPreDepositViewInfo.GetAccountSetupStepsCompletedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);

            // Act - incomplete account witness verification
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.labelProofOfAddress);
            dynamic resultViewItem4 = InvestPreDepositViewInfo.GetAccountSetupStepsCheckListViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_ACCOUNT_SETUP_INCOMPLETE_ONE_TIME_PASSWORD_WITNESS_VERIFICATION;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelOneTimePasswordWitnessVerificationNotSubmitted, AutomationID.labelGrabFriendSpousePhone);
            app.DismissKeyboard();

            // Grab Phone page
            dynamic resultViewItem5 = InvestPreDepositViewInfo.GetGrabPhoneViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_GRAB_WITNESS_PHONE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);

            // Enter name of Witness
            app.EnterText(platform, AutomationID.labelWitnessName, userID);
            app.DismissKeyboard();

            // Choose the country
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelCountryCode, countryCode);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, countryCode, AutomationID.labelWitnessMobilePhone);
            if (platform == Platform.iOS) { app.Tap(AutomationID.labelGrabFriendSpousePhoneOrderItem1); }

            // Enter phone number
            app.EnterText(platform, AutomationID.labelWitnessMobilePhone, "0000000000");
            app.DismissKeyboard();
            string buttonOk;
            if (platform == Platform.Android) { buttonOk = AutomationID.button1; }
            else { if (chosenLanguage == ELanguage.English) { buttonOk = "OK"; } else { buttonOk = "확인"; } }
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelRequestOneTimePassword, buttonOk);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, buttonOk, AutomationID.labelRequestOneTimePassword);
            // Need to find the way to go through it
            //InvestViewInfo.GetEnterVerificationCodeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            //app.EnterText(platform, AutomationID.labelWitnessMobilePhone, "123456");
            //app.DismissKeyboard();
            //SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelRequestOneTimePassword, AutomationID.labelGrabFriendSpousePhone);
            //SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonSubmit, AutomationID.labelYourRequestOTPVerifyHasFailed);
        }


        [Test, Order(0)]
        [Category("Positive")]
        public void Check_Invest_Bond_Account_Go_To_Depoist_Page_Again_After_Transaction_Submitted_Pos()
        {
            //To check to Invest Deposit page
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Invest ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);

            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);

            // Act1 - check dashboard page
            // Assert view
            // Post result
            Thread.Sleep(Shared.threeSec); // To get the page
            dynamic resultViewItem3 = InvestDepositViewInfo.GetInitialPortfolioDashboardViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PORTFOLIO_DASHBOARD;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelTransfer, AutomationID.viewDeposit);
            dynamic resultViewItem4 = InvestDepositViewInfo.GetSendingDepositViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_DEPOSIT;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);
            app.Tap(AutomationID.buttonKeypad1);

            // Act2 - check bond account document page
            // Assert view
            // Post result
            Thread.Sleep(Shared.threeSec); // To get the page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonNext, AutomationID.labelPleaseDocumentsLinked);
            dynamic resultViewItem5 = InvestDepositViewInfo.GetPleaseDocumentsLinkedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_DEPOSIT_PLEASE_VIEW_DOCUMENTS_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonIAgree, AutomationID.labelPleaseDocumentsLinked);

            // Act3 - check bond account transaction preview page
            // Assert view
            // Post result
            Thread.Sleep(Shared.threeSec); // To get the page
            dynamic resultViewItem6 = InvestDepositViewInfo.GetDepositTransactionPreviewViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_DEPOSIT_TRANCATION_PREVIEW_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem6, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonContinue, AutomationID.labelEnterPassword);
            Shared.EnterPasswordCommon(app, platform, false);

            // Act4 - check bond account transaction page
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, AutomationID.labelTransactionStatus);
            dynamic resultViewItem7 = InvestDepositViewInfo.GetTransactionSubmittedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_DEPOSIT_TRANSACTION_SUBMITTED;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem7, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonDone2, AutomationID.viewBondAccount);
            dynamic resultViewItem8 = InvestDepositViewInfo.GetBondAccountViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_DEPOSIT;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem8, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonDeposit, AutomationID.viewDeposit);
            dynamic resultViewItem9 = InvestDepositViewInfo.GetSendingDepositViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_DEPOSIT;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem9, Shared.testResult, testIDForTestRail);
        }


        [Test]
        [Category("Positive")]
        public void Check_Invest_Bond_Account_Documents_Required_Pos()
        {
            // Arrange
            userID = "mobiletest";

            // Invest ident page
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);
            
            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);

            // Act1 - check completed documents
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelBond, AutomationID.viewBondAccount);
            dynamic resultViewItem4 = InvestDepositViewInfo.GetBondAccountViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_DOCUMENTS_REQUIRED;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);

            // Act2 - check completed document details
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelVeriffStatusForProofOfAddress, AutomationID.labelAddressDocumentStatusRequired);
            dynamic resultViewItem5 = InvestPreDepositViewInfo.GetDocumentRequiredViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_DOCUMENTS_REQUIRED_DETAIL_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonOK, AutomationID.viewBondAccount);
        }


        [Test]
        [Category("Negative")]
        public void Check_Invest_Bond_Account_Documents_Under_Review_Neg()
        {
            // Arrange
            userID = "mobiletest3";

            // Invest ident page
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);

            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);

            // Dashboard page
            Thread.Sleep(Shared.threeSec); // To get the page
            dynamic resultViewItem3 = InvestDepositViewInfo.GetInitialPortfolioDashboardViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PORTFOLIO_DASHBOARD;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);

            // Act1 - documents under review
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelBond, AutomationID.viewBondAccount);
            dynamic resultViewItem4 = InvestDepositViewInfo.GetBondAccountViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_UNDER_REVIEW;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);

            // Act2 - document details under review
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelVeriffStatusForProofOfAddress, AutomationID.labelAddressDocumentStatusReviewInProgress);
            dynamic resultViewItem5 = InvestPreDepositViewInfo.GetReviewInProgressViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_UNDER_REVIEW_DETAIL_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonOK, AutomationID.viewBondAccount);
        }


        [Test]
        [Category("Negativee")]
        public void Check_Invest_Bond_Account_Documents_Action_Required_Neg()
        {
            // Arrange
            userID = "mobiletest6";

            // Invest ident page
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);
            
            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);

            // Dashboard page
            Thread.Sleep(Shared.threeSec); // To get the page
            dynamic resultViewItem3 = InvestDepositViewInfo.GetInitialPortfolioDashboardViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PORTFOLIO_DASHBOARD;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);

            // Act1 - document page. Action required
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelBond, AutomationID.viewBondAccount);
            dynamic resultViewItem4 = InvestDepositViewInfo.GetBondAccountViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_ACTION_REQUIRED;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);

            // Act2 - document details page. Action required
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelVeriffStatusForProofOfAddress, AutomationID.labelAddressDocumentStatusActionRequired);
            dynamic resultViewItem5 = InvestPreDepositViewInfo.GetActionRequiredViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_ACTION_REQUIRED_DETAIL_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonOK, AutomationID.viewBondAccount);
        }


        [Test, Order(2)]
        [Category("Positive")]
        public void Check_Invest_Bond_Account_Transactions_Page_Again_After_Transaction_Submitted_Pos()
        {
            // Arrange
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Invest ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);
            
            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);

            // Dashboard page
            Thread.Sleep(Shared.threeSec); // To get the page
            dynamic resultViewItem3 = InvestDepositViewInfo.GetInitialPortfolioDashboardViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PORTFOLIO_DASHBOARD;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelTransfer, AutomationID.viewDeposit);
            dynamic resultViewItem4 = InvestDepositViewInfo.GetSendingDepositViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_DEPOSIT;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);
            app.Tap(AutomationID.buttonKeypad1);

            // Complete documents with details
            Thread.Sleep(Shared.threeSec); // To get the page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonNext, AutomationID.labelPleaseDocumentsLinked);
            dynamic resultViewItem5 = InvestDepositViewInfo.GetPleaseDocumentsLinkedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_DEPOSIT_PLEASE_VIEW_DOCUMENTS_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonIAgree, AutomationID.labelPleaseDocumentsLinked);
            
            // Transaction preview page
            Thread.Sleep(Shared.threeSec); // To get the page
            dynamic resultViewItem6 = InvestDepositViewInfo.GetDepositTransactionPreviewViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_DEPOSIT_TRANCATION_PREVIEW_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem6, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonContinue, AutomationID.labelEnterPassword);
            Shared.EnterPasswordCommon(app, platform, false);

            // Submit transaction
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, AutomationID.labelTransactionStatus);
            dynamic resultViewItem7 = InvestDepositViewInfo.GetTransactionSubmittedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_DEPOSIT_TRANSACTION_SUBMITTED;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem7, Shared.testResult, testIDForTestRail);

            // Act - history page. Observe history transactions
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonTransactions, AutomationID.labelTransactionsHistory);
            dynamic resultViewItem8 = InvestDepositViewInfo.GetTransactionHistoryViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_TRANSACTION_HISTORY_DEPOSIT;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem8, Shared.testResult, testIDForTestRail);
        }


        [Test, Order(1)]
        [Category("Positive")]
        public void Check_Invest_Draw_Down_Go_To_Depoist_Page_Again_After_Transaction_Submitted_Pos()
        {
            // Arrange
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Invest ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);

            // Dashboard page
            Thread.Sleep(Shared.threeSec); // To get the page
            dynamic resultViewItem3 = InvestDepositViewInfo.GetInitialPortfolioDashboardViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PORTFOLIO_DASHBOARD;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelBond, AutomationID.viewBondAccount);
            dynamic resultViewItem4 = InvestDrawDownViewInfo.GetBondAccountViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);

            // Act1 - drawdown page. Observe drawdown transactions
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonDrawdown, AutomationID.labelYouHaveAMatureBondAccountBalance);
            dynamic resultViewItem5 = InvestDrawDownViewInfo.GetMatureBondAccountBalanceViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_DRAWDOWN;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonDrawdown, AutomationID.labelDrawdownTransferTo);
            dynamic resultViewItem6 = InvestDrawDownViewInfo.GetDrawdownTransactionPreviewViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_DRAWDOWN;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem6, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonDrawdown, AutomationID.labelEnterPassword);
            Shared.EnterPasswordCommon(app, platform, false);

            // Act2 - submit drawdown transaction. Observe deposits on account page
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, AutomationID.labelTransactionStatus);
            dynamic resultViewItem7 = InvestDepositViewInfo.GetTransactionSubmittedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_DRAWDOWN_TRANSACTION_SUBMITTED;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem7, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonDone2, AutomationID.viewBondAccount);
            dynamic resultViewItem8 = InvestDepositViewInfo.GetBondAccountViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_DEPOSIT;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem8, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonDeposit, AutomationID.viewDeposit);
            dynamic resultViewItem9 = InvestDepositViewInfo.GetSendingDepositViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_DEPOSIT;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem9, Shared.testResult, testIDForTestRail);
        }


        [Test, Order(3)]
        [Category("Positive")]
        public void Check_Invest_Draw_Down_Transactions_Page_Again_After_Transaction_Submitted_Pos()
        {
            // Arrange
            dynamic resultViewItem = InvestPreDepositViewInfo.GetInitialInvestViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_INITIAL_INVEST_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);

            // Invest ident page
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelStatus, AutomationID.viewIdentification);
            dynamic resultViewItem2 = MenuViewInfo.GetIdentificationViewItem(app, platform, chosenLanguage, isLocalizationTest, pageInfo);
            testIDForTestRail = TestIDForTestRail.INVEST_IDENTIFICATION_PAGE;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem2, Shared.testResult, testIDForTestRail);
            
            // LogIn
            Shared.LogInGluwa(app, platform, chosenLanguage, isLocalizationTest, client, jsonDataFromTestRail, userID, userPassword, AutomationID.labelStatus, pageInfo);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, ETestResult.Pass, TestIDForTestRail.INVEST_VALID_USERNAME_EMAIL_PASSWORD);

            // Dashboard page
            Thread.Sleep(Shared.threeSec); // To get the page
            dynamic resultViewItem3 = InvestDepositViewInfo.GetInitialPortfolioDashboardViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PORTFOLIO_DASHBOARD;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem3, Shared.testResult, testIDForTestRail);

            // Act1 - drawdown page. Observe drawdown transactions
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.labelBond, AutomationID.viewBondAccount);
            dynamic resultViewItem4 = InvestDrawDownViewInfo.GetBondAccountViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem4, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonDrawdown, AutomationID.labelYouHaveAMatureBondAccountBalance);
            dynamic resultViewItem5 = InvestDrawDownViewInfo.GetMatureBondAccountBalanceViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_DRAWDOWN;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem5, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonDrawdown, AutomationID.labelDrawdownTransferTo);
            dynamic resultViewItem6 = InvestDrawDownViewInfo.GetDrawdownTransactionPreviewViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_PAGE_DRAWDOWN;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem6, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonDrawdown, AutomationID.labelEnterPassword);
            Shared.EnterPasswordCommon(app, platform, false);

            // Act2 - submit drawdown transaction. Observe deposits on account page
            // Assert view
            // Post result
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, AutomationID.labelTransactionStatus);
            dynamic resultViewItem7 = InvestDepositViewInfo.GetTransactionSubmittedViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_DRAWDOWN_TRANSACTION_SUBMITTED;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem7, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonTransactions, AutomationID.labelTransactionsHistory);

            // Act3 - history page. Observe deposits on history page
            // Assert view
            // Post result
            dynamic resultViewItem8 = InvestDepositViewInfo.GetTransactionHistoryViewItem(app, platform, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.INVEST_BOND_ACCOUNT_TRANSACTION_HISTORY_DRAWDOWNS;
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem8, Shared.testResult, testIDForTestRail);
        }
    }
}
