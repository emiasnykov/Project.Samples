using GluwaPro.UITest.TestUtilities.CurrencyUtils;
using GluwaPro.UITest.TestUtilities.Methods;
using GluwaPro.UITest.TestUtilities.Methods.ExchangeMethod;
using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Methods.TestRailApiMethod;
using GluwaPro.UITest.TestUtilities.Models.ExchangeViewModels;
using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using Gurock.TestRail;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using Xamarin.UITest;

namespace GluwaPro.UITest.TestUtilities.Pages
{
    public class ExchangeCommonTest
    {
        public const bool DEFAULT_BBUTTONNEXT = false;
        private static string currentCurrency;
        private static readonly string correctPassword = Shared.walletPassword;
        private static readonly string wrongPassword = "dummy";
        private static readonly int timeToLive = 60;
        private static string sendCurrency;
        private static string receiveCurrency;
        private static int testIDForTestRail;
        private static dynamic resultViewItem;

        /// <summary>
        /// Must exchange currencies
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="conversion"></param>
        public static void CheckExchangeCurrenciesCommon(IApp app, Platform platform, ETestcaseTypes testCase, EConversion conversion, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage, bool isLocalizationTest)
        {
            // Arrange
            testCase = ETestcaseTypes.Positive;
            string quoteAmount = ExchangeFunction.EnterQuoteAmount(app, platform, conversion, testCase);
            ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // LogIn
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonNext, ExchangeViewHandle.QUOTE_ENTER_PASSWORD_VIEW);
            SharedViewInfo.GetEnterPasswordViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.EnterWalletPassword(app, platform, correctPassword);
            SharedViewHandle.HandlePasswordViewAsserts(app, platform, correctPassword, false, chosenLanguage, isLocalizationTest);

            // Act - create success quote
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, ExchangeViewHandle.QUOTE_CREATE_SUCCESS_VIEW);
            ExchangeViewHandle.HandleQuoteCreateSuccessAsserts(app, platform, conversion, quoteAmount, chosenLanguage, isLocalizationTest);
            string buttonOk;
            if (platform == Platform.Android) { buttonOk = AutomationID.button1; }
            else { if (chosenLanguage == ELanguage.English) { buttonOk = "OK"; } else { buttonOk = "확인"; } }
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonAccept, buttonOk);

            // To remove the informative popup
            try 
            {
                SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, buttonOk, ExchangeViewHandle.QUOTE_ACCEPT_SUCCESS_VIEW); 
            }
            catch // On Android, sometime the popup shows twice 
            {               
                SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, buttonOk, buttonOk);
                SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, buttonOk, ExchangeViewHandle.QUOTE_ACCEPT_SUCCESS_VIEW);
            }
            ExchangeViewHandle.HandleQuoteAcceptAssertsCommon(app, platform, conversion, quoteAmount, chosenLanguage, isLocalizationTest);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonFinish, ExchangeViewHandle.QUOTE_BASE_EXCHANGE_VIEW);
            ExchangeViewHandle.HandleQuoteCreateAsserts(app, platform, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion)), chosenLanguage, isLocalizationTest);

            // Assert view
            // Post result
            testIDForTestRail = EConversionTestIDForTestRail.GetExchangeCurrencies(conversion);
            resultViewItem = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }

        /// <summary>
        /// Must cancel the exchange on quote page
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="conversion"></param>
        public static void CheckExchangeCancelQuoteCommon(IApp app, Platform platform, ETestcaseTypes testCase, EConversion conversion, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage, bool isLocalizationTest)
        {
            // Atrange
            testCase = ETestcaseTypes.Positive;
            string quoteAmount = ExchangeFunction.EnterQuoteAmount(app, platform, conversion, testCase);
            ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // LogIn
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonNext, ExchangeViewHandle.QUOTE_ENTER_PASSWORD_VIEW);
            Shared.EnterWalletPassword(app, platform, correctPassword);
            SharedViewHandle.HandlePasswordViewAsserts(app, platform, correctPassword, false, chosenLanguage, isLocalizationTest);

            // Act - create success quote
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, ExchangeViewHandle.QUOTE_CREATE_SUCCESS_VIEW);
            ExchangeViewHandle.HandleQuoteCreateSuccessAsserts(app, platform, conversion, quoteAmount, chosenLanguage, isLocalizationTest);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonClose, ExchangeViewHandle.QUOTE_BASE_EXCHANGE_VIEW);

            // Assert view
            // Post result
            testIDForTestRail = EConversionTestIDForTestRail.GetExchangeCancelQuote(conversion);
            resultViewItem = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }

        /// <summary>
        /// Must cancel by software/hardware back button on quote
        /// This is only for android devices
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="conversion"></param>
        public static void CheckExchangeGoBackCommon(IApp app, Platform platform, ETestcaseTypes testCase, EConversion conversion, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage, bool isLocalizationTest, int i)
        {
            // Atrange
            testCase = ETestcaseTypes.Positive;
            string quoteAmount = ExchangeFunction.EnterQuoteAmount(app, platform, conversion, testCase);
            ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // LogIn
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonNext, ExchangeViewHandle.QUOTE_ENTER_PASSWORD_VIEW);
            Shared.EnterWalletPassword(app, platform, correctPassword);
            SharedViewHandle.HandlePasswordViewAsserts(app, platform, correctPassword, false, chosenLanguage, isLocalizationTest);

            // Act - create success view
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, ExchangeViewHandle.QUOTE_CREATE_SUCCESS_VIEW);
            ExchangeViewHandle.HandleQuoteCreateSuccessAsserts(app, platform, conversion, quoteAmount, chosenLanguage, isLocalizationTest);
            if (i < 1)
            {
                ExchangeFunction.TapHardwareBackButton(app, platform);
            }
            else
                SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonClose, ExchangeViewHandle.QUOTE_BASE_EXCHANGE_VIEW);

            // Assert view
            // Post result
            testIDForTestRail = EConversionTestIDForTestRail.GetExchangeGoBack(conversion);
            resultViewItem = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }

        /// <summary>
        /// Must cancel by software/hardware back button on quote whenever users try
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="conversion"></param>
        public static void CheckExchangeGoBackMultiTimesCommon(IApp app, Platform platform, ETestcaseTypes testCase, EConversion conversion, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage, bool isLocalizationTest)
        {
            // Arrange
            int MAX_RECREATE = 2;
            for (int i = 0; i < MAX_RECREATE; i++)
            {
                CheckExchangeGoBackCommon(app, platform, testCase, conversion, client, jsonDataFromTestRail, chosenLanguage, isLocalizationTest, i);
                Thread.Sleep(Shared.oneSec);
            }

            // Assert view
            // Post result
            testIDForTestRail = EConversionTestIDForTestRail.GetExchangeGoBackMultiTimes(conversion);
            resultViewItem = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }

        /// <summary>
        /// Must not be exchanged an expired quote
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="conversion"></param>
        /// <param name="client"></param>
        /// <param name="jsonDataFromTestRail"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        public static void CheckExchangeExpiredQuoteCommon(IApp app, Platform platform, ETestcaseTypes testCase, EConversion conversion, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage, bool isLocalizationTest)
        {
            // Arrange
            string quoteAmount = ExchangeFunction.EnterQuoteAmount(app, platform, conversion, testCase);
            ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // LogIn
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonNext, ExchangeViewHandle.QUOTE_ENTER_PASSWORD_VIEW);
            Shared.EnterWalletPassword(app, platform, correctPassword);
            SharedViewHandle.HandlePasswordViewAsserts(app, platform, correctPassword, false, chosenLanguage, isLocalizationTest);

            // Act - create success quote
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, ExchangeViewHandle.QUOTE_CREATE_SUCCESS_VIEW);
            ExchangeViewHandle.HandleQuoteCreateSuccessAsserts(app, platform, conversion, quoteAmount, chosenLanguage, isLocalizationTest);

            // Accept Expired Quote
            // Sleep until quote expires
            Thread.Sleep(new TimeSpan(0, 0, timeToLive + 10));

            // Act - create fail quote
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonAccept, ExchangeViewHandle.QUOTE_ACCEPT_FAIL_VIEW);
            ExchangeViewHandle.HandleQuoteAcceptFailAsserts(app, platform, chosenLanguage, isLocalizationTest);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonClose, ExchangeViewHandle.QUOTE_BASE_EXCHANGE_VIEW);
            ExchangeViewHandle.HandleQuoteCreateAsserts(app, platform, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion)), chosenLanguage, isLocalizationTest);

            // Assert view
            // Post result
            testIDForTestRail = EConversionTestIDForTestRail.GetExchangeExpiredQuote(conversion);
            resultViewItem = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }

        /// <summary>
        /// Must not be exchanged over balance
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="conversion"></param>
        public static void CheckExchangeAmountOverBalanceCommon(IApp app, Platform platform, ETestcaseTypes testCase, EConversion conversion, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Arrange
            SelectSendReceiveCommon(app, platform, conversion);
            (string balance, string _) = ExchangeViewInfo.GetBalanceAndCurrency(app, platform);
            string amount = ExchangeFunction.CalculateAmountOverBalance(balance);
            Shared.TapAmountCustom(app, amount);

            // Act
            CreateExchangeViewItem viewBefore = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            ExchangeViewHandle.HandleQuoteCreateAsserts(app, platform, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion)), chosenLanguage, isLocalizationTest, viewBefore.ExchangeAmount, false);

            // Assert view
            // Post result
            testIDForTestRail = EConversionTestIDForTestRail.GetExchangeAmountOverBalance(conversion);
            resultViewItem = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }

        /// <summary>
        /// Must check the information when users enter wrong password
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="conversion"></param>
        public static void CheckExchangeWrongPasswordCommon(IApp app, Platform platform, ETestcaseTypes testCase, EConversion conversion, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Arange
            testCase = ETestcaseTypes.Positive;
            string _ = ExchangeFunction.EnterQuoteAmount(app, platform, conversion, testCase, chosenLanguage, isLocalizationTest);
            ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // Act
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonNext, ExchangeViewHandle.QUOTE_ENTER_PASSWORD_VIEW);
            Shared.EnterWalletPassword(app, platform, wrongPassword);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, ExchangeViewHandle.QUOTE_ENTER_PASSWORD_VIEW);
            app.DismissKeyboard();
            SharedViewHandle.HandlePasswordViewAsserts(app, platform, wrongPassword, true, chosenLanguage, isLocalizationTest);

            // Assert view
            // Post result
            testIDForTestRail = EConversionTestIDForTestRail.GetExchangeWrongPassword(conversion);
            resultViewItem = SharedViewInfo.GetEnterPasswordViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }

        /// <summary>
        /// Must not be exchanged when entering below minimum amount of each coins(BTC = 0.00009, KRWG = 999, USDG, sUSDG, NGNG = 0.9)
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="conversion"></param>
        public static void CheckExchangeAmountTooSmallCommon(IApp app, Platform platform, ETestcaseTypes testCase, EConversion conversion, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Arrange
            testCase = ETestcaseTypes.AmountTooSmall;
            ExchangeFunction.EnterQuoteAmount(app, platform, conversion, testCase);

            // Act
            CreateExchangeViewItem viewBefore = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            ExchangeViewHandle.HandleQuoteCreateAsserts(app, platform, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion)), chosenLanguage, isLocalizationTest, viewBefore.ExchangeAmount, false);

            // Assert view
            // Post result
            testIDForTestRail = EConversionTestIDForTestRail.GetExchangeAmountTooSmall(conversion);
            resultViewItem = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }

        /// <summary>
        /// Must not be exchanged when quote not found
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="conversion"></param>
        /// <param name="client"></param>
        /// <param name="jsonDataFromTestRail"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        public static void CheckExchangeNotFoundCommon(IApp app, Platform platform, ETestcaseTypes testCase, EConversion conversion, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Arange
            testCase = ETestcaseTypes.QuoteNotFound;
            ExchangeFunction.EnterQuoteAmount(app, platform, conversion, testCase);
            ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // LogIn
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonNext, ExchangeViewHandle.QUOTE_ENTER_PASSWORD_VIEW);
            Shared.EnterWalletPassword(app, platform, correctPassword);

            // New code for checking Quote is not created showing Not Found
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonConfirm, ExchangeViewHandle.QUOTE_CREATE_FAIL_VIEW);
            ExchangeViewHandle.HandleQuoteCreateFailAsserts(app, platform, chosenLanguage, isLocalizationTest);

            // Assert view
            // Post result
            testIDForTestRail = EConversionTestIDForTestRail.GetExchangeNotFound(conversion);
            resultViewItem = ExchangeViewInfo.GetQuoteCreateFailViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
            SharedFunction.TapNavigationButtonAndHandleNextView(app, platform, AutomationID.buttonFinish, AutomationID.viewExchange); // Need to change the automationID from buttonFinish to buttonClose            
        }

        /// <summary>
        /// Must not be the same Sender and Receiver 
        /// When Sender is choosen as BTC and then Receiver is choosen as BTC, then Sender must be changed
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="conversion"></param>
        public static void CheckExchangeInactiveCommon(IApp app, Platform platform, ETestcaseTypes testCase, EConversion conversion, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Enter quote amount
            ExchangeFunction.EnterQuoteAmount(app, platform, conversion, testCase);
            CreateExchangeViewItem viewBefore = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // Assert view
            // Post result
            ExchangeViewHandle.HandleQuoteCreateAsserts(app, platform, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion)), chosenLanguage, isLocalizationTest, viewBefore.ExchangeAmount, false);
            testIDForTestRail = EConversionTestIDForTestRail.GetExchangeInactive(conversion);
            resultViewItem = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }

        /// <summary>
        /// Must be able to swap currencies
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="currencyTest"></param>
        /// <param name="bSend"></param>
        public static void CheckExchangeSwapCurrenciesCommon(IApp app, Platform platform, EConversion conversion, bool bSend, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {

            // Swap to BTC
            // Tap my Wallets
            ExchangeFunction.SelectCurrency(app, platform, conversion, bSend);
            string message = "Swapping send currency " + conversion;
            ScreenshotTools.TakePosScreenshot(app, message);
            CreateExchangeViewItem viewCurrent = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);

            // Asserting sending currency
            sendCurrency = ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion));
            receiveCurrency = viewCurrent.ExchangeReceiveCurrency;
            ExchangeViewHandle.HandleQuoteCreateAsserts(app, platform, sendCurrency, receiveCurrency);              
            testIDForTestRail = TestIDForTestRail.SWAP_CURRENCIES_SWAP_WALLET;
            resultViewItem = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }

        /// <summary>
        /// Must be able to swap currencies even if users enter amount of coins on exchange view page
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="currencyWallet"></param>
        public static void CheckExchangeSwapWalletsAmountNot0Common(IApp app, Platform platform, ETestcaseTypes testCase, EConversion conversion, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Arrange
            testCase = ETestcaseTypes.QuoteNotFound;
            ExchangeFunction.EnterQuoteAmount(app, platform, conversion, testCase);
            ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            
            // Tap my Wallets
            Shared.TapMyWalletsAndTapCurrency(app, platform, EConversionExtensions.ToSourceCurrency(conversion), AutomationID.viewExchange);
            app.WaitForElement(AutomationID.labelBalanceAndCurrency);
            if (platform == Platform.Android) // Android
            {
                currentCurrency = app.Query(AutomationID.labelBalanceAndCurrency).LastOrDefault().Text.Split()[1];
            }
            else // iOS
            {
                currentCurrency = app.WaitForElement(AutomationID.labelBalanceAndCurrency).LastOrDefault().Description.Split()[6];
            }
            app.WaitForElement(AutomationID.labelBalanceAndCurrency);

            //  Asserting current currency
            Assert.AreEqual(currentCurrency, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)));
            string screenshotName = "Swapping to " + ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion));
            ScreenshotTools.TakePosScreenshot(app, screenshotName);
            CreateExchangeViewItem viewCurrent = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            
            // Asserting sending currency
            ExchangeViewHandle.HandleQuoteCreateAsserts(app, platform, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), viewCurrent.ExchangeReceiveCurrency, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.SWAP_CURRENCIES_SWAP_WALLET_AMOUNT_NOT_0;
            resultViewItem = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }

        /// <summary>
        /// Must be able to change the currency by My Wallets page
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="testCase"></param>
        /// <param name="currencyWallet"></param>
        public static void CheckExchangeSwapWalletsCommon(IApp app, Platform platform, ETestcaseTypes testCase, EConversion conversion, ApiClient client, dynamic jsonDataFromTestRail, ELanguage chosenLanguage, bool isLocalizationTest)
        {
            // Arange
            SelectSendReceiveCommon(app, platform, conversion);

            // Tap my Wallets
            Shared.TapMyWalletsAndTapCurrency(app, platform, EConversionExtensions.ToSourceCurrency(conversion), AutomationID.viewExchange, chosenLanguage, isLocalizationTest);
            app.WaitForElement(AutomationID.labelBalanceAndCurrency);
            if (platform == Platform.Android) // Android
            {
                currentCurrency = app.Query(AutomationID.labelBalanceAndCurrency).LastOrDefault().Text.Split()[1];
            }
            else // iOS
            {
                currentCurrency = app.WaitForElement(AutomationID.labelBalanceAndCurrency).LastOrDefault().Description.Split()[6];
            }

            // Asserting current currency
            Assert.AreEqual(currentCurrency, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)));
            CreateExchangeViewItem viewCurrent = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            
            // Asserting sending currency
            ExchangeViewHandle.HandleQuoteCreateAsserts(app, platform, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), viewCurrent.ExchangeReceiveCurrency, chosenLanguage, isLocalizationTest);
            testIDForTestRail = TestIDForTestRail.SWAP_CURRENCIES_SWAP_WALLET;
            resultViewItem = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            Shared.PostTestResultForTestRail(client, jsonDataFromTestRail.id, resultViewItem, Shared.testResult, testIDForTestRail);
        }

        /// <summary>
        /// Must be albe to select both sender and receiver on exchange page
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="conversion"></param>
        public static void SelectSendReceiveCommon(IApp app, Platform platform, EConversion conversion)
        {
            try
            {
                // First drop down
                ExchangeFunction.SelectCurrency(app, platform, conversion, true);
                Thread.Sleep(Shared.twoSec);
				
                // Second drop down
                ExchangeFunction.SelectCurrency(app, platform, conversion, false);
                Thread.Sleep(Shared.twoSec);
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR can't select conversion {0}/{1}", ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion)));
                Assert.Fail("ERROR can't select conversion {0}/{1}", ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion)));
            }
            ScreenshotTools.TakePosScreenshot(app, "Selecting conversion {0}/{1}", ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion)));
        }
    }
}
