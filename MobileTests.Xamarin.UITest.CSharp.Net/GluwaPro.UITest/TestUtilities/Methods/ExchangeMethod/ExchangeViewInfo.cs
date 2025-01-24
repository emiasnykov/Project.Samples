using GluwaPro.UITest.TestUtilities.Methods.ExchangeMethod;
using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Models.ExchangeViewModels;
using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace GluwaPro.UITest.TestUtilities.Methods
{
    public class ExchangeViewInfo
    {
        /// <summary>
        /// Create exchange view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static CreateExchangeViewItem GetCreateExchangeViewItem(IApp app, Platform platform, ELanguage chosenLanguage, bool isLocalizationTest)
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
            string textButtonNext;
            string exchangeSendCurrency;
            string exchangeReceiveCurrency;
            string exchangeAmount;
            string balanceAndCurrency;
            bool bButtonNext = false;
            app.WaitForElement(AutomationID.labelExchangeSourceCurrency);
            if (platform == Platform.Android) // Android
            {
                // Parsing info from app (Android)
                exchangeSendCurrency = app.WaitForElement(AutomationID.labelExchangeSourceCurrency).LastOrDefault().Text;
                exchangeReceiveCurrency = app.WaitForElement(AutomationID.labelExchangeTargetCurrency).LastOrDefault().Text;
                exchangeAmount = app.WaitForElement(AutomationID.labelExchangeAmount).LastOrDefault().Text;
                balanceAndCurrency = app.WaitForElement(AutomationID.labelBalanceAndCurrency).LastOrDefault().Text;
                string labelButtonNext = app.Query(c => c.Marked("Next"))[0].Label; // Need to change for localization
                switch (labelButtonNext)
                {
                    case "buttonNextDisabled":
                        bButtonNext = false;
                        textButtonNext = app.WaitForElement(AutomationID.buttonNextDisabled).LastOrDefault().Text;
                        break;
                    case "buttonNext":
                        bButtonNext = true;
                        textButtonNext = app.WaitForElement(AutomationID.buttonNext).LastOrDefault().Text;
                        break;
                    default:
                        bButtonNext = false;
                        textButtonNext = app.WaitForElement(AutomationID.buttonNextDisabled).LastOrDefault().Text;
                        Assert.Warn("Unexpected buttonNext logic GetCreateExchangeViewItem");
                        break;
                }
            }
            else // iOS
            {
                // Parsing info from app (iOS)
                exchangeSendCurrency = extractTextContext(app.Query(c => c.Id(AutomationID.labelExchangeSourceCurrency).Descendant(0))[0]);
                exchangeReceiveCurrency = extractTextContext(app.Query(c => c.Id(AutomationID.labelExchangeTargetCurrency).Descendant(0))[0]);
                exchangeAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelExchangeAmount))[0]);
                balanceAndCurrency = extractTextContext(app.Query(c => c.Id(AutomationID.labelBalanceAndCurrency))[0]);
                textButtonNext = "";
                AppResult labelButtonNextDisabled = app.Query(c => c.Id(AutomationID.buttonNextDisabled)).LastOrDefault();
                if (labelButtonNextDisabled == null)
                {
                    AppResult labelButtonNext = app.Query(c => c.Id(AutomationID.buttonNext)).LastOrDefault();
                    if (labelButtonNext != null)
                    {
                        bButtonNext = true; 
                        textButtonNext = extractTextContext(app.Query(c => c.Id(AutomationID.buttonNext).Descendant(0))[0]);
                    }
                    else
                    {
                        TestContext.WriteLine("Unexpected buttonNext");
                        textButtonNext = extractTextContext(app.Query(c => c.Id(AutomationID.buttonNextDisabled).Descendant(0))[0]);
                    }
                }
                else
                {
                    bButtonNext = false;
                }
            }
            (string balance, string currency) = parseBalanceAndCurrency(balanceAndCurrency);

            CreateExchangeViewItem result = new CreateExchangeViewItem(
                exchangeSendCurrency,
                exchangeReceiveCurrency,
                exchangeAmount,
                balance,
                currency,
                bButtonNext);
            if (isLocalizationTest)
            {
                // To check localzation 
                string[] listOfPrimaryKeys = { "Next" };
                string[] listOfTextsOnScreen = { textButtonNext };
                Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
            }
            return result;
        }

        /// <summary>
        /// Get balance and currency
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        public static (string, string) GetBalanceAndCurrency(IApp app, Platform platform)
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
            string balanceAndCurrency;
            app.WaitForElement(AutomationID.labelBalanceAndCurrency);
            if (platform == Platform.Android) // Android
            {
                balanceAndCurrency = app.Query(c => c.Marked(AutomationID.labelBalanceAndCurrency))[0].Text;
            }
            else // iOS
            {
                balanceAndCurrency = extractTextContext(app.Query(c => c.Id(AutomationID.labelBalanceAndCurrency))[0]);
            }
            (string balance, string currency) = parseBalanceAndCurrency(balanceAndCurrency);
            return (balance, currency);
        }
                
        /// <summary>
        /// Create failed quote view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static QuoteCreateFailViewItem GetQuoteCreateFailViewItem(IApp app, Platform platform, ELanguage chosenLanguage, bool isLocalizationTest)
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
            string viewName = ExchangeViewHandle.QUOTE_NOT_FOUND_VIEW;
            try
            {
                string textTitleQuoteCreateFail;
                string textTitle;
                string textQuoteDetail;
                string textMessage;
                string textQuoteMessage;
                string textFinish;

                if (platform == Platform.Android) // Android
                {
                    app.WaitForElement(AutomationID.labelQuoteDetail);
                    textTitleQuoteCreateFail = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelTitleQuoteCreateFail))[0].Text;
                    textTitle = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelTitle))[0].Text;
                    textQuoteDetail = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelQuoteDetail))[0].Text;
                    textMessage = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelMessage))[0].Text;
                    textQuoteMessage = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelQuoteMessage))[0].Text;
                    textFinish = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.buttonFinish))[0].Text;
                }
                else // iOS
                {
                    textTitleQuoteCreateFail = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTitleQuoteCreateFail))[0]);
                    textTitle = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTitle))[0]);
                    textQuoteDetail = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelQuoteDetail))[0]);
                    textMessage = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelMessage))[0]);
                    textQuoteMessage = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelQuoteMessage))[0]);
                    textFinish = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonFinish))[0]);
                }

                QuoteCreateFailViewItem result = new QuoteCreateFailViewItem(
                    textTitleQuoteCreateFail,
                    textTitle,
                    textQuoteDetail,
                    textMessage,
                    textQuoteMessage,
                    textFinish);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "QuoteCreateFailed", "Title", "QuoteFailedTitle", "Message", "QuoteFailedMessage", "Finish" };
                    string[] listOfTextsOnScreen = { textTitleQuoteCreateFail, textTitle, textQuoteDetail, textMessage, textQuoteMessage, textFinish };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Create failed accept quote view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static QuoteAcceptFailViewItem GetQuoteAcceptFailViewItem(IApp app, Platform platform, ELanguage chosenLanguage, bool isLocalizationTest)
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
            string viewName = "viewQuoteAcceptFail";
            try
            {
                string textHeaderBar;
                string textTitle;
                string textQuoteTitle;
                string textMessage;
                string textQuoteMessage;
                string textButtonClose;
                if (platform == Platform.Android) // Android
                {
                    textHeaderBar = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelTitleQuoteAcceptFail))[0].Text;
                    textTitle = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelTitle))[0].Text;
                    textQuoteTitle = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelQuoteTitle))[0].Text;
                    textMessage = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelMessage))[0].Text;
                    textQuoteMessage = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelQuoteMessage))[0].Text;
                    textButtonClose = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.buttonClose))[0].Text;
                }                
                else // iOS
                {
                    textHeaderBar = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTitleQuoteAcceptFail))[0]);
                    textTitle = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTitle))[0]);
                    textQuoteTitle = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelQuoteTitle))[0]);
                    textMessage = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelMessage))[0]);
                    textQuoteMessage = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelQuoteMessage))[0]);
                    textButtonClose = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonClose))[0]);
                }

                QuoteAcceptFailViewItem result = new QuoteAcceptFailViewItem(
                    textHeaderBar,
                    textTitle,
                    textQuoteTitle,
                    textMessage,
                    textQuoteMessage,
                    textButtonClose);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                                "QuoteAcceptFailed",
                                "Title",
                                "QuoteAcceptFailedTitleWith403",
                                "Message",
                                "QuoteAcceptFailedMessageWith403",
                                "Close" };
                    string[] listOfTextsOnScreen = {
                                textHeaderBar,
                                textTitle,
                                textQuoteTitle,
                                textMessage,
                                textQuoteMessage,
                                textButtonClose
                            };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Create success quote view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static QuoteCreateSuccessViewItem GetQuoteCreateSuccessViewItem(IApp app, Platform platform, ELanguage chosenLanguage, bool isLocalizationTest)
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

            // Extract description info in here
            Func<AppResult, string> extractTextMessage = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext: (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };
            string viewName = "viewQuoteCreateSuccess";
            string textYouSend;
            string youSendAmount;
            string textFee;
            string feeAmount;
            string textConverted;
            string convertedAmount;
            string textEstimatedExchangeRate;
            string estimatedExchangeRateAmount;
            string textYouGet;
            string youGetAmount;
            string textYouGetDetail;
            string textButtonAccept;
            bool isButtonAccept;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textYouSend = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelYouSend))[0].Text;
                    youSendAmount = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelYouSendAmount))[0].Text;
                    textFee = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelFee))[0].Text;
                    feeAmount = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelFeeAmount))[0].Text;
                    textConverted = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelConverted))[0].Text;
                    convertedAmount = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelConvertedAmount))[0].Text;
                    textEstimatedExchangeRate = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelEstimatedExchangeRate))[0].Text;
                    estimatedExchangeRateAmount = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelEstimatedExchangeRateAmount))[0].Text;
                    textYouGet = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelYouGet))[0].Text;
                    youGetAmount = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelExchangedAmount))[0].Text;
                    Shared.ScrollDownToElement(app, platform, AutomationID.labelQuoteCreatedText);
                    textYouGetDetail = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelQuoteCreatedText))[0].Text;
                    textButtonAccept = app.Query(c => c.Marked(AutomationID.buttonAccept))[0].Text;                                
                    textButtonAccept = app.Query(c => c.Marked(AutomationID.buttonAccept))[0].Label;
                    switch (textButtonAccept)
                    {
                        case "buttonAcceptDisabled":
                            isButtonAccept = false;
                            break;
                        case "buttonAccept":
                            isButtonAccept = true;
                            break;
                        default:
                            isButtonAccept = false;
                            Assert.Warn("Unexpected buttonNext logic in GetQuoteCreateSuccessViewItem");
                            break;
                    }
                }
                else // iOS
                {
                    textYouSend = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelYouSend).Descendant(0))[0]);
                    youSendAmount = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelYouSendAmount).Descendant(0))[0]);
                    textFee = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelFee).Descendant(0))[0]);
                    feeAmount = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelFeeAmount).Descendant(0))[0]);
                    textConverted = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelConverted).Descendant(0))[0]);
                    convertedAmount = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelConvertedAmount).Descendant(0))[0]);
                    textEstimatedExchangeRate = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelEstimatedExchangeRate).Descendant(0))[0]);
                    estimatedExchangeRateAmount = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelEstimatedExchangeRateAmount).Descendant(0))[0]);
                    textYouGet = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelYouGet).Descendant(0))[0]);
                    youGetAmount = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelExchangedAmount).Descendant(0))[0]);
                    Shared.ScrollDownToElement(app, platform, AutomationID.labelQuoteCreatedText);
                    textYouGetDetail = extractTextContext(app.Query(c => c.Id(AutomationID.labelQuoteCreatedText).Descendant(0))[0]);
                    textButtonAccept = extractTextContext(app.Query(c => c.Id(AutomationID.buttonAccept).Descendant(0))[0]);
                    AppResult labelButtonAcceptDisabled = app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonAcceptDisabled)).LastOrDefault();
                    if (labelButtonAcceptDisabled == null)
                    {
                        AppResult labelButtonAccept = app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonAccept)).LastOrDefault();
                        if (labelButtonAccept != null)
                        {
                            isButtonAccept = true;
                        }
                        else
                        {
                            isButtonAccept = false;
                            TestContext.WriteLine("Unexpected buttonAccept");
                        }                    
                    }
                    else
                    {
                        isButtonAccept = false;
                    }
                }

                QuoteCreateSuccessViewItem result = new QuoteCreateSuccessViewItem(
                    textYouSend,
                    youSendAmount,
                    textFee,
                    feeAmount,
                    textConverted,
                    convertedAmount,
                    textEstimatedExchangeRate,
                    estimatedExchangeRateAmount,
                    textYouGet,
                    youGetAmount,
                    textYouGetDetail,
                    isButtonAccept);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                                "YouSendWithCurrency",
                                "FeeWithCurrency",
                                "ConvertedAmountWithCurrency",
                                "EstimatedExchangeRate",
                                "YouGetWithCurrency",
                                "TheAmountYouGet",
                                "Accept"};
                    string[] listOfTextsOnScreen = {
                                textYouSend,
                                textFee,
                                textConverted,
                                textEstimatedExchangeRate,
                                textYouGet,
                                textYouGetDetail,
                                textButtonAccept
                            };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }

                AppResult errorAppResult, quoteCreatedResult;
                if (platform == Platform.Android) // Android
                {
                    try
                    {
                        Shared.ScrollDownToElement(app, platform, AutomationID.labelQuoteCreatedText); // For Exchange Success
                    }
                    catch
                    {
                        Shared.ScrollDownToElement(app, platform, AutomationID.labelError); // For Exchange fail
                    }
                    errorAppResult = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelError)).LastOrDefault();
                    quoteCreatedResult = app.Query(c => c.Marked(viewName).Descendant().Marked(AutomationID.labelQuoteCreatedText)).LastOrDefault();
                    if (errorAppResult != null)
                    {
                        result.ErrorMessage = errorAppResult.Text;
                    }
                    if (quoteCreatedResult != null)
                    {
                        result.QuoteCreatedText = quoteCreatedResult.Text;
                    }
                }
                else // iOS
                {
                    errorAppResult = app.Query(c => c.Marked(viewName).Descendant().Id(AutomationID.labelError)).LastOrDefault();
                    quoteCreatedResult = app.Query(c => c.Marked(viewName).Descendant().Id(AutomationID.labelQuoteCreatedText)).LastOrDefault();
                    if (errorAppResult != null)
                    {
                        result.ErrorMessage = extractTextMessage(errorAppResult);
                    }
                    if (quoteCreatedResult != null)
                    {
                        result.QuoteCreatedText = extractTextMessage(quoteCreatedResult);
                    }
                }              
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Create accept quote view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="bAccepted"></param>
        /// <returns></returns>
        public static QuoteAcceptViewItem GetQuoteAcceptViewItem(IApp app, Platform platform, ELanguage chosenLanguage, bool isLocalizationTest, bool bAccepted = false)
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

            string viewName = "viewQuoteAcceptSuccess";
            string textYouSend;
            string youSendAmount;
            string textFee;
            string feeAmount;
            string textConverted;
            string convertedAmount;
            string textEstimatedExchangeRate;
            string estimatedExchangeRateAmount;
            string textYouGet;
            string exchangedAmount;
            string quoteAcceptedText;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textYouSend = app.Query(c => c.Marked(AutomationID.labelYouSend))[0].Text;
                    youSendAmount = app.Query(c => c.Marked(AutomationID.labelYouSendAmount))[0].Text;
                    textFee = app.Query(c => c.Marked(AutomationID.labelFee))[0].Text;
                    feeAmount = app.Query(c => c.Marked(AutomationID.labelFeeAmount))[0].Text;
                    textConverted = app.Query(c => c.Marked(AutomationID.labelConverted))[0].Text;
                    convertedAmount = app.Query(c => c.Marked(AutomationID.labelConvertedAmount))[0].Text;
                    textEstimatedExchangeRate = app.Query(c => c.Marked(AutomationID.labelEstimatedExchangeRate))[0].Text;
                    estimatedExchangeRateAmount = app.Query(c => c.Marked(AutomationID.labelEstimatedExchangeRateAmount))[0].Text;
                    Shared.ScrollDownToElement(app, platform, AutomationID.labelExchangedAmount);
                    textYouGet = app.Query(c => c.Marked(AutomationID.labelYouGet))[0].Text;
                    exchangedAmount = app.Query(c => c.Marked(AutomationID.labelExchangedAmount))[0].Text;
                    Shared.ScrollDownToElement(app, platform, AutomationID.labelQuoteAcceptedText);
                    quoteAcceptedText = app.Query(c => c.Marked(AutomationID.labelQuoteAcceptedText))[0].Text;
                }
                else // iOS
                {
                    textYouSend = extractTextContext(app.Query(c => c.Id(AutomationID.labelYouSend).Descendant(0))[0]);
                    youSendAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelYouSendAmount).Descendant(0))[0]);
                    textFee = extractTextContext(app.Query(c => c.Id(AutomationID.labelFee).Descendant(0))[0]);
                    feeAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelFeeAmount).Descendant(0))[0]);
                    textConverted = extractTextContext(app.Query(c => c.Id(AutomationID.labelConverted).Descendant(0))[0]);
                    convertedAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelConvertedAmount).Descendant(0))[0]);
                    textEstimatedExchangeRate = extractTextContext(app.Query(c => c.Id(AutomationID.labelEstimatedExchangeRate).Descendant(0))[0]);
                    estimatedExchangeRateAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelEstimatedExchangeRateAmount).Descendant(0))[0]);
                    Shared.ScrollDownToElement(app, platform, AutomationID.labelExchangedAmount);
                    textYouGet = extractTextContext(app.Query(c => c.Id(AutomationID.labelYouGet).Descendant(0))[0]);
                    exchangedAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelExchangedAmount).Descendant(0))[0]);
                    Shared.ScrollDownToElement(app, platform, AutomationID.labelQuoteAcceptedText);
                    quoteAcceptedText = extractTextContext(app.Query(c => c.Id(AutomationID.labelQuoteAcceptedText).Descendant(0))[0]);

                    textYouGet = "textYouGet";
                    exchangedAmount = "exchangedAmount";
                }

                QuoteAcceptViewItem result = new QuoteAcceptViewItem(

                    textYouSend,
                    youSendAmount,
                    textFee,
                    feeAmount,
                    textConverted,
                    convertedAmount,
                    textEstimatedExchangeRate,
                    estimatedExchangeRateAmount,
                    textYouGet,
                    exchangedAmount,
                    quoteAcceptedText);     
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Parse amounts and currencies from exchange rate format: "(amountCurrency1) (Currency1) = (amountCurrency2) (Currency2)"
        /// Returning amountCurrency1, Currency1, amountCurrency2, Currency2 in this order
        /// </summary>
        /// <param name="exchangeRate"></param>
        /// <returns></returns>
        public static (string, string, string, string) ParseExchangeRate(string exchangeRate)
        {
            //returning result from exchangeRate: "(amountCurrency1) (Currency1) = (amountCurrency2) (Currency2)"
            //If broken, check repl for new pattern
            string pattern = @"(\d) (.*) = ([,\d]+([.]\d+)?) (.*)";
            Match matchExchangeRate = Regex.Match(exchangeRate, pattern, RegexOptions.IgnoreCase);
            return (matchExchangeRate.Groups[1].ToString(), matchExchangeRate.Groups[2].ToString(), matchExchangeRate.Groups[3].ToString(), matchExchangeRate.Groups[5].ToString());
        }

        /// <summary>
        /// Parse balance and currency from CreateQuote view BalanceAndCurrency
        /// </summary>
        /// <param name="balanceAndCurrency"></param>
        /// <returns></returns>
        private static (string, string) parseBalanceAndCurrency(string balanceAndCurrency)
        {
            string pattern = @"([,.\d]+) (.*)";
            Match matchBalanceAndCurrency = Regex.Match(balanceAndCurrency, pattern, RegexOptions.IgnoreCase);
            return (matchBalanceAndCurrency.Groups[1].ToString(), matchBalanceAndCurrency.Groups[2].ToString());
        }
    }
}
