using GluwaPro.UITest.TestUtilities.CurrencyUtils;
using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Models.ExchangeViewModels;
using GluwaPro.UITest.TestUtilities.Pages;
using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using NUnit.Framework;
using System.Linq;
using Xamarin.UITest;

namespace GluwaPro.UITest.TestUtilities.Methods.ExchangeMethod
{
    class ExchangeViewHandle
    {
        public const string QUOTE_CREATE_SUCCESS_VIEW = "viewQuoteCreateSuccess";
        public const string QUOTE_ENTER_PASSWORD_VIEW = "viewExchangePassword";
        public const string QUOTE_NOT_FOUND_VIEW = "viewQuoteCreateFail";
        public const string QUOTE_ACCEPT_FAIL_VIEW = "viewQuoteAcceptFail";
        public const string QUOTE_CREATE_FAIL_VIEW = "viewQuoteCreateFail";
        public const string QUOTE_BASE_EXCHANGE_VIEW = "viewExchange";
        public const string QUOTE_ACCEPT_SUCCESS_VIEW = "viewQuoteAcceptSuccess";
        public const string EXCHANGE_VIEW = "viewExchange";
        public const string TRANSACTION_HISTORY_VIEW = "viewTransactionHistory";
        private const string QUOTE_ACCEPT_FAIL_MESSAGE = "Quote accept failed";
        private const string QUOTE_CREATE_FAIL_MESSAGE = "Quote create failed";
        private const string DEFAULT_EXCHANGEAMOUNT = "0";
        private const string QUOTE_CREATE_MESSAGE = "Note: The amount you get is an estimate. You will either get equal to or less (even 0) than the estimate. However, the exchange rate is guaranteed to be equal or better than the quoted rate, so you will never receive less than you paid for. Any amount not exchanged will be returned to the user.";
        private const string QUOTE_CREATE_INSUFFICIENT_FUNDS_FORMAT = "Insufficient funds. Your current balance is {0}. If you think this is inaccurate, refresh your balance and retry by pressing the button below.";

        /* Handle View */

        /// <summary>
        /// Must check Quote expired page when expired
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        public static void HandleQuoteAcceptFailAsserts(IApp app, Platform platform, ELanguage chosenLanguage, bool isLocalizationTest)
        {
            // Arrange
            QuoteAcceptFailViewItem viewInfo = ExchangeViewInfo.GetQuoteAcceptFailViewItem(app, platform, chosenLanguage, isLocalizationTest);
            string quoteExpiredTitle = "Quote Expired";
            string quoteExpiredMessage = "We were unable to accept the quote because it has expired. Please get a new quote and try again.";

            // Checking element exists
            ScreenshotTools.AreEqualScreenshot(app, QUOTE_ACCEPT_FAIL_MESSAGE, viewInfo.TextHeaderBar, "Unexpected, not Quote Accept Fail.");
            ScreenshotTools.AreEqualScreenshot(app, quoteExpiredTitle, viewInfo.TextQuoteTitle, "Unexpected quoteTitle in viewQuoteAcceptFail.");
            ScreenshotTools.AreEqualScreenshot(app, quoteExpiredMessage, viewInfo.TextQuoteMessage, "Unexpected quoteMessage in viewQuoteAcceptFail.");
        }

        /// <summary>
        /// Must check the success quote page
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="conversion"></param>
        /// <param name="quoteAmount"></param>
        /// <param name="bError"></param>
        public static void HandleQuoteCreateSuccessAsserts(IApp app, Platform platform, EConversion conversion, string quoteAmount, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, bool bError = false)
        {
            // Arrange
            QuoteCreateSuccessViewItem viewInfo = ExchangeViewInfo.GetQuoteCreateSuccessViewItem(app, platform, chosenLanguage, isLocalizationTest);
            string quoteExchangeRateBase, quoteExchangeRateCurrency, makerPrice, makerCurrency;
            (quoteExchangeRateBase, quoteExchangeRateCurrency, makerPrice, makerCurrency) = ExchangeViewInfo.ParseExchangeRate(viewInfo.EstimatedExchangeRateAmount);

            // Verify the amounts shown are correct
            ScreenshotTools.AreEqualScreenshot(app, quoteAmount, viewInfo.ConvertedAmount, "Unexpected quoteAmount in viewQuoteCreateSuccess.");
            string expectedExchangeRateAmount = ExchangeFunction.CalculateExchangeAmount(quoteAmount, quoteExchangeRateBase, makerPrice);
            string expectedYouSendAmount = ExchangeFunction.CalculateYouSendAmount(viewInfo.ConvertedAmount, viewInfo.FeeAmount);
            ScreenshotTools.AreEqualScreenshot(app, expectedYouSendAmount, viewInfo.YouSendAmount.Replace(",", ""), "Unexpected youSendAmount in viewQuoteCreateSuccess."); ;
            ScreenshotTools.IsTrueScreenshot(app, expectedExchangeRateAmount.Contains((viewInfo.YouGetAmount).Replace(",", "")), $"Expected {expectedExchangeRateAmount} but found {viewInfo.YouGetAmount}");
            
            // Verify the correct currencies
            ScreenshotTools.IsTrueScreenshot(app, viewInfo.TextYouSend.Contains(ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion))));
            ScreenshotTools.IsTrueScreenshot(app, viewInfo.TextYouGet.Contains(ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion))));
            ScreenshotTools.AreEqualScreenshot(app, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), quoteExchangeRateCurrency, "Unexpected first currency in viewQuoteCreateSuccess.");
            ScreenshotTools.AreEqualScreenshot(app, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion)), makerCurrency, "Unexpected second currency in viewQuoteCreateSuccess.");

            // Asserting if element exists
            ScreenshotTools.IsTrueScreenshot(app, viewInfo.TextYouSend.Any());
            ScreenshotTools.IsTrueScreenshot(app, viewInfo.TextYouGet.Any());
            ScreenshotTools.IsTrueScreenshot(app, viewInfo.FeeAmount.Any());
            ScreenshotTools.IsTrueScreenshot(app, viewInfo.ConvertedAmount.Any());

            if (bError == false)
            {
                if (platform == Platform.Android) { ScreenshotTools.AreEqualScreenshot(app, QUOTE_CREATE_MESSAGE, viewInfo.QuoteCreatedText); }
            }
            else
            {
                // Make sure to call updateCurrentBalanceAndCurrency before
                string expectedMessage = string.Format(QUOTE_CREATE_INSUFFICIENT_FUNDS_FORMAT, ExchangeFunction.balanceCurrencyPair.Item1); ;
                ScreenshotTools.AreEqualScreenshot(app, expectedMessage, viewInfo.ErrorMessage);
            }
        }


        /// <summary>
        /// Handle send currency being changed based on receive currency matching send currency
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="sendCurrency"></param>
        /// <param name="receiveCurrency"></param>
        /// <param name="exchangeAmount"></param>
        /// <param name="bButtonNext"></param>
        public static void HandleQuoteCreateAsserts(IApp app, Platform platform, string sendCurrency, string receiveCurrency, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, string exchangeAmount = DEFAULT_EXCHANGEAMOUNT, bool bButtonNext = ExchangeCommonTest.DEFAULT_BBUTTONNEXT)
        {
            // Arrange
            // Handle send currency being changed based on receive currency matching send currency
            // TODO - Improve this to handle multiple currencies, e.g. KRW-G, sKRWC-G,sUSDC-G, NGN-G, etc.
            if (sendCurrency == receiveCurrency)
            {
                switch (receiveCurrency)
                {
                    case "USD-G":
                        sendCurrency = "BTC";
                        break;
                    //case "BTC":
                    case "KRW-G":
                        sendCurrency = "BTC";
                        break;
                    case "sUSDC-G":
                        sendCurrency = "BTC";
                        break;
                    default:
                        sendCurrency = "sUSD-G";
                        break;
                }
            }
            CreateExchangeViewItem viewInfo = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            ScreenshotTools.AreEqualScreenshot(app, sendCurrency, viewInfo.ExchangeSendCurrency, "Unexpected sendCurrency in viewCreateQuote.");
            ScreenshotTools.AreEqualScreenshot(app, receiveCurrency, viewInfo.ExchangeReceiveCurrency, "Unexpected receiveCurrency in viewCreateQuote.");
            ScreenshotTools.AreEqualScreenshot(app, exchangeAmount, viewInfo.ExchangeAmount, "Unexpected exchangeAmount in viewCreateQuote.");
            ScreenshotTools.AreEqualScreenshot(app, bButtonNext, viewInfo.IsButtonNext, "Unexpected buttonNext logic in viewCreateQuote.");

            // Asserting balanceAndCurrency's currency with send currency
            ScreenshotTools.AreEqualScreenshot(app, viewInfo.ExchangeSendCurrency, viewInfo.Currency, "Currency type in balanceAndCurrency is not same as sending currency.");
        }

        /// <summary>
        /// Must check correct Quote page
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="conversion"></param>
        /// <param name="quoteAmount"></param>
        public static void HandleQuoteAcceptAssertsCommon(IApp app, Platform platform, EConversion conversion, string quoteAmount, ELanguage chosenLanguage, bool isLocalizationTest)
        {
            // Arrange
            QuoteAcceptViewItem viewInfo = ExchangeViewInfo.GetQuoteAcceptViewItem(app, platform, chosenLanguage, isLocalizationTest);
            string quoteExchangeRateBase, quoteExchangeRateCurrency, makerPrice, makerCurrency;
            (quoteExchangeRateBase, quoteExchangeRateCurrency, makerPrice, makerCurrency) = ExchangeViewInfo.ParseExchangeRate(viewInfo.EstimatedExchangeRateAmount);

            // Verify the amounts shown are correct
            ScreenshotTools.AreEqualScreenshot(app, quoteAmount, viewInfo.ConvertedAmount, "Unexpected quoteAmount in viewQuoteAcceptSuccess.");
            string expectedExchangeRateAmount = ExchangeFunction.CalculateExchangeAmount(quoteAmount, quoteExchangeRateBase, makerPrice);
            string expectedYouSendAmount = ExchangeFunction.CalculateYouSendAmount(viewInfo.ConvertedAmount, viewInfo.FeeAmount);
            ScreenshotTools.AreEqualScreenshot(app, expectedYouSendAmount, viewInfo.YouSendAmount.Replace(",", ""), "Unexpected youSendAmount in viewQuoteAcceptSuccess.");

            if (platform == Platform.Android)   // FYI, iOS is NOT scrolled on some pages
            {
                ScreenshotTools.IsTrueScreenshot(app, expectedExchangeRateAmount.Contains((viewInfo.ExchangedAmount).Replace(",", "")), "Unexpected exchangeRateAmount in viewQuoteAcceptSuccess.");
                ScreenshotTools.IsTrueScreenshot(app, viewInfo.TextYouGet.Contains(ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion))));
                ScreenshotTools.IsTrueScreenshot(app, viewInfo.TextYouGet.Any());
            }

            // Verify the correct currencies
            ScreenshotTools.IsTrueScreenshot(app, viewInfo.TextYouSend.Contains(ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion))));
            ScreenshotTools.AreEqualScreenshot(app, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), quoteExchangeRateCurrency, "Unexpected first currency in viewQuoteAcceptSuccess.");
            ScreenshotTools.AreEqualScreenshot(app, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion)), makerCurrency, "Unexpeced second currency in viewQuoteAcceptSuccess.");

            // Asserting if element exists            
            ScreenshotTools.IsTrueScreenshot(app, viewInfo.TextYouSend.Any());
            ScreenshotTools.IsTrueScreenshot(app, viewInfo.FeeAmount.Any());
            ScreenshotTools.IsTrueScreenshot(app, viewInfo.ConvertedAmount.Any());
        }

        /// <summary>
        /// Must check the information when create failed on quote page
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        public static void HandleQuoteCreateFailAsserts(IApp app, Platform platform, ELanguage chosenLanguage, bool isLocalizationTest)
        {
            // Arrange
            QuoteCreateFailViewItem viewInfo = ExchangeViewInfo.GetQuoteCreateFailViewItem(app, platform, chosenLanguage, isLocalizationTest);
            string quoteNoFoundTitle = "No matching order was found";
            string quoteNoFoundMessage = "We were unable to find an order matching your request. Please try again later. You might get lucky next time.";

            // Checking element exists
            ScreenshotTools.AreEqualScreenshot(app, QUOTE_CREATE_FAIL_MESSAGE, viewInfo.TextTitleQuoteCreateFail, "Unexpected, not Quote Accept Fail.");
            ScreenshotTools.AreEqualScreenshot(app, quoteNoFoundTitle, viewInfo.TextQuoteDetail, "Unexpected quoteTitle in viewQuoteAcceptFail.");
            ScreenshotTools.AreEqualScreenshot(app, quoteNoFoundMessage, viewInfo.TextQuoteMessage, "Unexpected quoteMessage in viewQuoteAcceptFail.");
        }

        /// <summary>
        /// Must check basic exchange information of quote page
        /// </summary>
        /// <param name="app"></param>
        /// <param name="expectedView"></param>
        private static void HandleView(IApp app, string expectedView)
        {
            // Arrange
            switch (expectedView)
            {
                // Handle views
                case QUOTE_BASE_EXCHANGE_VIEW:
                    try
                    {
                        app.WaitForNoElement(QUOTE_CREATE_SUCCESS_VIEW, "Timed out: QUOTE_CREATE_SUCCESS_VIEW still exists", Shared.oneMin);
                        app.WaitForNoElement(QUOTE_CREATE_FAIL_VIEW, "Timed out: QUOTE_CREATE_FAIL_VIEW still exists", Shared.oneMin);
                        app.WaitForNoElement(QUOTE_ACCEPT_SUCCESS_VIEW, "Timed out: QUOTE_ACCEPT_SUCCESS_VIEW still exists", Shared.oneMin);
                        app.WaitForNoElement(QUOTE_ENTER_PASSWORD_VIEW, "Timed out: QUOTE_ENTER_PASSWORD_VIEW still exists", Shared.oneMin);
                    }
                    catch
                    {
                        ReplTools.StartRepl(app);
                        ScreenshotTools.TakeErrorScreenshot(app, "Unexpected view, expecting {0}", QUOTE_BASE_EXCHANGE_VIEW);
                        Assert.Fail("Unexpected view, expecting {0}", QUOTE_BASE_EXCHANGE_VIEW);
                    }
                    break;
                default:
                    try
                    {
                        // Confirming the app is in the right view
                        // Since the Repl() tree shows the app's view are stack on each other
                        // Wait for past view can pass even though the app UI's on the new view
                        app.WaitForElement(expectedView);
                    }
                    catch
                    {
                        ReplTools.StartRepl(app);
                        ScreenshotTools.TakeErrorScreenshot(app, "Unexpected view, expecting {0}", expectedView);
                        Assert.Fail("Unexpected view, expecting {0}", expectedView);
                    }
                    break;
            }
            ScreenshotTools.TakePosScreenshot(app, expectedView);
        }
    }
}
