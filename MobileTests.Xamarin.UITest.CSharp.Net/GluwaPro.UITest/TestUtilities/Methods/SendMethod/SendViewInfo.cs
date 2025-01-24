using GluwaPro.UITest.TestUtilities.CurrencyUtils;
using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Models.NavigationViewModels;
using GluwaPro.UITest.TestUtilities.Models.SendViewModels;
using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace GluwaPro.UITest.TestUtilities.Methods.SendMethod
{
    class SendViewInfo
    {
        private const string MY_WALLETS_VIEW = "viewMyWallets";
        private const string SEND_VIEW = SendViewHandle.SEND_VIEW;
        private const string SEND_TO_ADDRESS_VIEW = SendViewHandle.SEND_TO_ADDRESS_VIEW;
        private const string SENT_TRANSACTION_SUCESS_VIEW = SendViewHandle.SENT_TRANSACTION_SUCESS_VIEW;

        /// <summary>
        /// Get send transaction view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static SendViewItem GetSendViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = SEND_VIEW;
            string textTitle;
            string textSendBottomNavigation;
            string textAddressBottomNavigation;
            string textHistoryBottomNavigation;
            string textExchangeBottomNavigation;
            string textMenuBottomNavigation;
            string textButtonNext;
            bool isButtonNextEnabled = false;
            app.WaitForElement(AutomationID.labelSend);
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.textHeader).LastOrDefault().Text;
                    textSendBottomNavigation = app.Query(AutomationID.labelSend).LastOrDefault().Text;
                    textAddressBottomNavigation = app.Query(AutomationID.labelAddress).LastOrDefault().Text;
                    textHistoryBottomNavigation = app.Query(AutomationID.labelHistory).LastOrDefault().Text;
                    textExchangeBottomNavigation = app.Query(AutomationID.labelExchange).LastOrDefault().Text;
                    textMenuBottomNavigation = app.Query(AutomationID.labelMenu).LastOrDefault().Text;
                    textButtonNext = app.Query(AutomationID.buttonNextDisabled).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textSendBottomNavigation = extractTextContext(app.Query(c => c.Id(AutomationID.labelSend).Descendant(0))[0]);
                    textAddressBottomNavigation = extractTextContext(app.Query(c => c.Id(AutomationID.labelAddress).Descendant(0))[0]);
                    textHistoryBottomNavigation = extractTextContext(app.Query(c => c.Id(AutomationID.labelHistory).Descendant(0))[0]);
                    textExchangeBottomNavigation = extractTextContext(app.Query(c => c.Id(AutomationID.labelExchange).Descendant(0))[0]);
                    textMenuBottomNavigation = extractTextContext(app.Query(c => c.Id(AutomationID.labelMenu).Descendant(0))[0]);
                    textButtonNext = extractTextContext(app.Query(c => c.Id(AutomationID.buttonNextDisabled).Descendant(0))[0]);

                };

                AppResult labelButtonNextDisabled = app.Query(c => c.Id(AutomationID.buttonNextDisabled)).LastOrDefault();
                if (labelButtonNextDisabled == null)
                {
                    AppResult labelButtonNext = app.Query(c => c.Id(AutomationID.buttonNext)).LastOrDefault();
                    if (labelButtonNext != null)
                    {
                        isButtonNextEnabled = true;
                    }
                    else
                    {
                        TestContext.WriteLine("Unexpected buttonNext");
                    }
                }
                else
                {
                    isButtonNextEnabled = false;
                };

                SendViewItem result = new SendViewItem(
                    textTitle,
                    textSendBottomNavigation,
                    textAddressBottomNavigation,
                    textHistoryBottomNavigation,
                    textExchangeBottomNavigation,
                    textMenuBottomNavigation,
                    textButtonNext,

                    isButtonNextEnabled);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { 
                        "Send" ,
                        "Send",
                        "Address",
                        "History",
                        "Exchange",
                        "Menu",
                        "Next",
                    };
                    string[] listOfTextsOnScreen = { 
                        textTitle,
                        textSendBottomNavigation,
                        textAddressBottomNavigation,
                        textHistoryBottomNavigation,
                        textExchangeBottomNavigation,
                        textMenuBottomNavigation,
                        textButtonNext
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
        /// Get send page with entered amount view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static SendViewItem GetSendPageWithEnteredAmountViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = SEND_VIEW;
            string textTitle;
            string textSendBottomNavigation;
            string textAddressBottomNavigation;
            string textHistoryBottomNavigation;
            string textExchangeBottomNavigation;
            string textMenuBottomNavigation;
            string textButtonNext;
            bool isButtonNextEnabled = false;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.textHeader).LastOrDefault().Text;
                    textSendBottomNavigation = app.Query(AutomationID.labelSend).LastOrDefault().Text;
                    textAddressBottomNavigation = app.Query(AutomationID.labelAddress).LastOrDefault().Text;
                    textHistoryBottomNavigation = app.Query(AutomationID.labelHistory).LastOrDefault().Text;
                    textExchangeBottomNavigation = app.Query(AutomationID.labelExchange).LastOrDefault().Text;
                    textMenuBottomNavigation = app.Query(AutomationID.labelMenu).LastOrDefault().Text;
                    textButtonNext = app.Query(AutomationID.buttonNext).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textSendBottomNavigation = extractTextContext(app.Query(c => c.Id(AutomationID.labelSend).Descendant(0))[0]);
                    textAddressBottomNavigation = extractTextContext(app.Query(c => c.Id(AutomationID.labelAddress).Descendant(0))[0]);
                    textHistoryBottomNavigation = extractTextContext(app.Query(c => c.Id(AutomationID.labelHistory).Descendant(0))[0]);
                    textExchangeBottomNavigation = extractTextContext(app.Query(c => c.Id(AutomationID.labelExchange).Descendant(0))[0]);
                    textMenuBottomNavigation = extractTextContext(app.Query(c => c.Id(AutomationID.labelMenu).Descendant(0))[0]);
                    textButtonNext = extractTextContext(app.Query(c => c.Id(AutomationID.buttonNext).Descendant(0))[0]);
                };

                AppResult labelButtonNextDisabled = app.Query(c => c.Id(AutomationID.buttonNext)).LastOrDefault();
                if (labelButtonNextDisabled == null)
                {
                    AppResult labelButtonNext = app.Query(c => c.Id(AutomationID.buttonNext)).LastOrDefault();
                    if (labelButtonNext != null)
                    {
                        isButtonNextEnabled = true;
                    }
                    else
                    {
                        TestContext.WriteLine("Unexpected buttonNext");
                    }
                }
                else
                {
                    isButtonNextEnabled = false;
                }
                SendViewItem result = new SendViewItem(
                    textTitle,
                    textSendBottomNavigation,
                    textAddressBottomNavigation,
                    textHistoryBottomNavigation,
                    textExchangeBottomNavigation,
                    textMenuBottomNavigation,
                    textButtonNext,

                    isButtonNextEnabled);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                        "Send" ,
                        "Send",
                        "Address",
                        "History",
                        "Exchange",
                        "Menu",
                        "Next",
                    };
                    string[] listOfTextsOnScreen = {
                        textTitle,
                        textSendBottomNavigation,
                        textAddressBottomNavigation,
                        textHistoryBottomNavigation,
                        textExchangeBottomNavigation,
                        textMenuBottomNavigation,
                        textButtonNext
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
        /// Get enter address initial view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static EnterAddressViewItem GetEnterAddressInitialViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, ECurrency currency = ECurrency.sUsdcg, string amount = "")
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

            string viewName = SEND_TO_ADDRESS_VIEW;
            string textTitle;
            string textReceiverAddress;
            string textEnterAddress;
            string textButtonNext;
            bool isButtonNextEnabled = false;
            app.WaitForElement(AutomationID.labelReceiversAddress);
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.textHeader).LastOrDefault().Text;
                    textReceiverAddress = app.Query(AutomationID.labelReceiversAddress).LastOrDefault().Text;
                    textEnterAddress = app.Query(AutomationID.labelEnterAddress).LastOrDefault().Text;
                    textButtonNext = app.Query(AutomationID.buttonNextDisabled).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textReceiverAddress = extractTextContext(app.Query(c => c.Id(AutomationID.labelReceiversAddress).Descendant(0))[0]);
                    textEnterAddress = extractTextContext(app.Query(c => c.Id(AutomationID.labelEnterAddress).Descendant(0))[0]);
                    textButtonNext = extractTextContext(app.Query(c => c.Id(AutomationID.buttonNextDisabled).Descendant(0))[0]);

                };

                AppResult labelButtonNextDisabled = app.Query(c => c.Id(AutomationID.buttonNextDisabled)).LastOrDefault();
                if (labelButtonNextDisabled == null)
                {
                    AppResult labelButtonNext = app.Query(c => c.Id(AutomationID.buttonNext)).LastOrDefault();
                    if (labelButtonNext != null)
                    {
                        isButtonNextEnabled = true;
                    }
                    else
                    {
                        TestContext.WriteLine("Unexpected buttonNext");
                    }
                }
                else
                {
                    isButtonNextEnabled = false;
                }
                EnterAddressViewItem result = new EnterAddressViewItem(
                    textTitle,
                    textReceiverAddress,
                    textEnterAddress,
                    textButtonNext,
                    isButtonNextEnabled);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                        "Send" ,
                        "ReceiversAddressWithCurrency",
                        "Address",
                        "Next",
                    };
                    string[] listOfTextsOnScreen = {
                        textTitle,
                        textReceiverAddress,
                        textEnterAddress,
                        textButtonNext
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen, currency, amount);
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
        /// Get enter address view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static EnterAddressViewItem GetEnterAddressViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, ECurrency currency = ECurrency.sUsdcg, string amount = "")
        {
            // Arrange
            string sendToAddress;
            switch (currency)
            {
                case ECurrency.Btc:
                    sendToAddress = "******************************";
                    break;
                default:
                    sendToAddress = "*******************************";
                    break;
            };

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

            string viewName = SEND_TO_ADDRESS_VIEW;
            string textTitle;
            string textReceiverAddress;
            string textEnterAddress;
            string textButtonNext;
            bool isButtonNextEnabled = false;
            app.WaitForElement(AutomationID.labelReceiversAddress);
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.textHeader).LastOrDefault().Text;
                    textReceiverAddress = app.Query(AutomationID.labelReceiversAddress).LastOrDefault().Text;
                    textEnterAddress = app.Query(AutomationID.labelEnterAddress).LastOrDefault().Text;
                    textButtonNext = app.Query(AutomationID.buttonNext).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textReceiverAddress = extractTextContext(app.Query(c => c.Id(AutomationID.labelReceiversAddress).Descendant(0))[0]);
                    textEnterAddress = extractTextContext(app.Query(c => c.Id(AutomationID.labelEnterAddress).Descendant(0))[0]);
                    textButtonNext = extractTextContext(app.Query(c => c.Id(AutomationID.buttonNext).Descendant(0))[0]);

                };

                AppResult labelButtonNextDisabled = app.Query(c => c.Id(AutomationID.buttonNextDisabled)).LastOrDefault();
                if (labelButtonNextDisabled == null)
                {
                    AppResult labelButtonNext = app.Query(c => c.Id(AutomationID.buttonNext)).LastOrDefault();
                    if (labelButtonNext != null)
                    {
                        isButtonNextEnabled = true;
                    }
                    else
                    {
                        TestContext.WriteLine("Unexpected buttonNext");
                    }
                }
                else
                {
                    isButtonNextEnabled = false;
                }
                EnterAddressViewItem result = new EnterAddressViewItem(
                    textTitle,
                    textReceiverAddress,
                    textEnterAddress,
                    textButtonNext,

                    isButtonNextEnabled);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                        "Send" ,
                        "ReceiversAddressWithCurrency",
                        sendToAddress,
                        "Next",
                    };
                    string[] listOfTextsOnScreen = {
                        textTitle,
                        textReceiverAddress,
                        textEnterAddress,
                        textButtonNext
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen, currency, amount);
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
        /// Get send preview
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static SendPreviewItem GetSendPreviewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewSendPreview;
            string textTitle;
            string textReceiverAddress;
            string textEnvironment;
            string textYouSendAmount;
            string textFeeAmount;
            string textButtonInfo;
            string textTotalAmount;
            string textButtonConfirm;
            string receiverAddress;
            string environment;
            string youSendAmount;
            string feeAmount;
            string totalAmount;
            bool isButtonConfirmEnabled = false;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    textReceiverAddress = app.Query(AutomationID.labelReceiverAddress).LastOrDefault().Text;
                    textEnvironment = app.Query(AutomationID.labelEnvironment).LastOrDefault().Text;
                    textYouSendAmount = app.Query(AutomationID.labelYouSend).LastOrDefault().Text;
                    textFeeAmount = app.Query(AutomationID.labelFee).LastOrDefault().Text;
                    textButtonInfo = app.Query(AutomationID.buttonInfo).LastOrDefault().Text;
                    textTotalAmount = app.Query(AutomationID.labelTotal).LastOrDefault().Text;

                    receiverAddress = app.Query(AutomationID.labelReceiverAddressValue).LastOrDefault().Text;
                    environment = app.Query(AutomationID.labelEnvironmentValue).LastOrDefault().Text;
                    youSendAmount = app.Query(AutomationID.labelYouSendAmount).LastOrDefault().Text;
                    feeAmount = app.Query(AutomationID.labelFeeAmount).LastOrDefault().Text;
                    totalAmount = app.Query(AutomationID.labelTotalAmount).LastOrDefault().Text;
                    textButtonConfirm = app.Query(AutomationID.buttonConfirm).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textReceiverAddress = extractTextContext(app.Query(c => c.Id(AutomationID.labelReceiverAddress).Descendant(0))[0]);
                    textEnvironment = extractTextContext(app.Query(c => c.Id(AutomationID.labelEnvironment).Descendant(0))[0]);
                    textYouSendAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelYouSend).Descendant(0))[0]);
                    textFeeAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelFee).Descendant(0))[0]);
                    textButtonInfo = extractTextContext(app.Query(c => c.Id(AutomationID.buttonInfo).Descendant(0))[0]);
                    textTotalAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelTotal).Descendant(0))[0]);

                    receiverAddress = extractTextContext(app.Query(c => c.Id(AutomationID.labelReceiverAddressValue).Descendant(0))[0]);
                    environment = extractTextContext(app.Query(c => c.Id(AutomationID.labelEnvironmentValue).Descendant(0))[0]);
                    youSendAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelYouSendAmount).Descendant(0))[0]);
                    feeAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelFeeAmount).Descendant(0))[0]);
                    totalAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelTotalAmount).Descendant(0))[0]);
                    // Check this button ID on IOS. It should be enabled as well as on Android
                    textButtonConfirm = extractTextContext(app.Query(c => c.Id(AutomationID.buttonConfirmDisabled).Descendant(0))[0]);

                };

                AppResult labelButtonNextDisabled = app.Query(c => c.Id(AutomationID.buttonConfirmDisabled)).LastOrDefault();
                if (labelButtonNextDisabled == null)
                {
                    AppResult labelButtonConfirm = app.Query(c => c.Id(AutomationID.buttonConfirm)).LastOrDefault();
                    if (labelButtonConfirm != null)
                    {
                        isButtonConfirmEnabled = true;
                    }
                    else
                    {
                        TestContext.WriteLine("Unexpected buttonNext");
                    }
                }
                else
                {
                    isButtonConfirmEnabled = false;
                };

                SendPreviewItem result = new SendPreviewItem(
                    textTitle,
                    textReceiverAddress,
                    textEnvironment,
                    textYouSendAmount,
                    textFeeAmount,
                    textButtonInfo,
                    textTotalAmount,
                    textButtonConfirm,

                    receiverAddress,
                    environment,
                    youSendAmount,
                    feeAmount,
                    totalAmount,
                    isButtonConfirmEnabled);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                        "TitlePreview" ,
                        "ReceiversAddress",
                        "Environment",
                        "YouSendWithCurrency",
                        "FeeWithCurrency",
                        "Info",
                        "Total",
                        "Confirm",
                    };
                    string[] listOfTextsOnScreen = {
                        textTitle,
                        textReceiverAddress,
                        textEnvironment,
                        textYouSendAmount,
                        textFeeAmount,
                        textButtonInfo,
                        textTotalAmount,
                        textButtonConfirm
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
        /// Get send with insufficient funds preview
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static SendPreviewWithInsufficientFunds GetSendPreviewWithInsufficientFunds(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewSendPreview;
            string textTitle;
            string textReceiverAddress;
            string textEnvironment;
            string textYouSendAmount;
            string textFeeAmount;
            string textButtonInfo;
            string textTotalAmount;
            string textError;
            string textButtonConfirm;
            string receiverAddress;
            string environment;
            string youSendAmount;
            string feeAmount;
            string totalAmount;
            bool isButtonConfirmEnabled = false;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    textReceiverAddress = app.Query(AutomationID.labelReceiverAddress).LastOrDefault().Text;
                    textEnvironment = app.Query(AutomationID.labelEnvironment).LastOrDefault().Text;
                    textYouSendAmount = app.Query(AutomationID.labelYouSend).LastOrDefault().Text;
                    textFeeAmount = app.Query(AutomationID.labelFee).LastOrDefault().Text;
                    textButtonInfo = app.Query(AutomationID.buttonInfo).LastOrDefault().Text;
                    textTotalAmount = app.Query(AutomationID.labelTotal).LastOrDefault().Text;
                    textError = app.Query(AutomationID.labelError).LastOrDefault().Text;

                    receiverAddress = app.Query(AutomationID.labelReceiverAddressValue).LastOrDefault().Text;
                    environment = app.Query(AutomationID.labelEnvironmentValue).LastOrDefault().Text;
                    youSendAmount = app.Query(AutomationID.labelYouSendAmount).LastOrDefault().Text;
                    feeAmount = app.Query(AutomationID.labelFeeAmount).LastOrDefault().Text;
                    totalAmount = app.Query(AutomationID.labelTotalAmount).LastOrDefault().Text;
                    textButtonConfirm = app.Query(AutomationID.buttonConfirmDisabled).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textReceiverAddress = extractTextContext(app.Query(c => c.Id(AutomationID.labelReceiverAddress).Descendant(0))[0]);
                    textEnvironment = extractTextContext(app.Query(c => c.Id(AutomationID.labelEnvironment).Descendant(0))[0]);
                    textYouSendAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelYouSend).Descendant(0))[0]);
                    textFeeAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelFee).Descendant(0))[0]);
                    textButtonInfo = extractTextContext(app.Query(c => c.Id(AutomationID.buttonInfo).Descendant(0))[0]);
                    textTotalAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelTotal).Descendant(0))[0]);
                    textError = extractTextContext(app.Query(c => c.Id(AutomationID.labelError).Descendant(0))[0]);

                    receiverAddress = extractTextContext(app.Query(c => c.Id(AutomationID.labelReceiverAddressValue).Descendant(0))[0]);
                    environment = extractTextContext(app.Query(c => c.Id(AutomationID.labelEnvironmentValue).Descendant(0))[0]);
                    youSendAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelYouSendAmount).Descendant(0))[0]);
                    feeAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelFeeAmount).Descendant(0))[0]);
                    totalAmount = extractTextContext(app.Query(c => c.Id(AutomationID.labelTotalAmount).Descendant(0))[0]);
                    textButtonConfirm = extractTextContext(app.Query(c => c.Id(AutomationID.buttonConfirmDisabled).Descendant(0))[0]);

                };

                AppResult labelButtonNextDisabled = app.Query(c => c.Id(AutomationID.buttonConfirmDisabled)).LastOrDefault();
                if (labelButtonNextDisabled == null)
                {
                    AppResult labelButtonConfirm = app.Query(c => c.Id(AutomationID.buttonConfirm)).LastOrDefault();
                    if (labelButtonConfirm != null)
                    {
                        isButtonConfirmEnabled = true;
                    }
                    else
                    {
                        TestContext.WriteLine("Unexpected buttonNext");
                    }
                }
                else
                {
                    isButtonConfirmEnabled = false;
                };

                SendPreviewWithInsufficientFunds result = new SendPreviewWithInsufficientFunds(
                    textTitle,
                    textReceiverAddress,
                    textEnvironment,
                    textYouSendAmount,
                    textFeeAmount,
                    textButtonInfo,
                    textTotalAmount,
                    textButtonConfirm,
                    textError,

                    receiverAddress,
                    environment,
                    youSendAmount,
                    feeAmount,
                    totalAmount,
                    isButtonConfirmEnabled) ;
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                        "TitlePreview" ,
                        "ReceiversAddress",
                        "Environment",
                        "YouSendWithCurrency",
                        "FeeWithCurrency",
                        "TitleInfo",
                        "Total",
                        "InsufficientFunds",
                        "Confirm",
                    };
                    string[] listOfTextsOnScreen = {
                        textTitle,
                        textReceiverAddress,
                        textEnvironment,
                        textYouSendAmount,
                        textFeeAmount,
                        textButtonInfo,
                        textTotalAmount,
                        textError,
                        textButtonConfirm
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
        /// Get send password view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static SendPasswordViewItem GetSendPasswordViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewSendPassword;
            string textEnterWalletPassword;
            string textButtonConfirm;
            string textForgotPassword;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textEnterWalletPassword = app.Query(AutomationID.labelEnterWalletPassword).LastOrDefault().Text;
                    textButtonConfirm = app.Query(AutomationID.buttonConfirm).LastOrDefault().Text;
                    textForgotPassword = app.Query(AutomationID.textForgotPassword).LastOrDefault().Text;
                }
                else // iOS
                {
                    textEnterWalletPassword = extractTextContext(app.Query(c => c.Id(AutomationID.labelEnterWalletPassword).Descendant(0))[0]);
                    textButtonConfirm = extractTextContext(app.Query(c => c.Id(AutomationID.buttonConfirm).Descendant(0))[0]);
                    textForgotPassword = extractTextContext(app.Query(c => c.Id(AutomationID.textForgotPassword).Descendant(0))[0]);
                }
                SendPasswordViewItem result = new SendPasswordViewItem(
                    textEnterWalletPassword,
                    textButtonConfirm,
                    textForgotPassword);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                        "EnterWalletPassword" ,
                        "Confirm",
                        "ForgotPassword"
                        };
                    string[] listOfTextsOnScreen = {
                        textEnterWalletPassword,
                        textButtonConfirm,
                        textForgotPassword
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
        /// Get send transaction view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static SendTransactionViewItem GetSendTransactionViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = SENT_TRANSACTION_SUCESS_VIEW;
            string textTransactionSubmitted;
            string textTotal;
            string textBlockchainText;
            string textButtonFinish;
            bool isButtonFinishEnabled = false;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTransactionSubmitted = app.Query(AutomationID.labelTransactionStatus).LastOrDefault().Text;
                    textTotal = app.Query(AutomationID.labelTotal).LastOrDefault().Text;
                    textBlockchainText = app.Query(AutomationID.labelBlockchainText).LastOrDefault().Text;
                    textButtonFinish = app.Query(AutomationID.buttonFinish).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTransactionSubmitted = extractTextContext(app.Query(c => c.Id(AutomationID.labelTransactionStatus).Descendant(0))[0]);
                    textTotal = extractTextContext(app.Query(c => c.Id(AutomationID.labelTotal).Descendant(0))[0]);
                    textBlockchainText = extractTextContext(app.Query(c => c.Id(AutomationID.labelBlockchainText).Descendant(0))[0]);
                    textButtonFinish = extractTextContext(app.Query(c => c.Id(AutomationID.buttonFinish).Descendant(0))[0]);
                };

                AppResult labelButtonNextDisabled = app.Query(c => c.Id(AutomationID.buttonNextDisabled)).LastOrDefault();
                if (labelButtonNextDisabled == null)
                {
                    AppResult labelButtonNext = app.Query(c => c.Id(AutomationID.buttonNext)).LastOrDefault();
                    if (labelButtonNext != null)
                    {
                        isButtonFinishEnabled = true;
                    }
                    else
                    {
                        TestContext.WriteLine("Unexpected buttonNext");
                    }
                }
                else
                {
                    isButtonFinishEnabled = false;
                };

                SendTransactionViewItem result = new SendTransactionViewItem(
                    textTransactionSubmitted,
                    textTotal,
                    textBlockchainText,
                    textButtonFinish,
                    isButtonFinishEnabled);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                        "TransactionSubmitted" ,
                        "TotalWithCurrency",
                        "ABlockchainTransactionNeeds",
                        "Finish",
                    };
                    string[] listOfTextsOnScreen = {
                        textTransactionSubmitted,
                        textTotal,
                        textBlockchainText,
                        textButtonFinish,
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
        /// Get my wallet view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static MyWalletsViewItem GetMyWalletsViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MY_WALLETS_VIEW;
            string textMyWallets;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textMyWallets = app.Query(AutomationID.labelMyWallet).LastOrDefault().Text;
                }
                else // iOS
                {
                    textMyWallets = extractTextContext(app.Query(c => c.Id(AutomationID.labelMyWallet).Descendant(0))[0]);
                };

                MyWalletsViewItem result = new MyWalletsViewItem(textMyWallets);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "MyWallets" };
                    string[] listOfTextsOnScreen = { textMyWallets };
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
        /// Get error view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static SynchronizingViewItem GetErrorViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MY_WALLETS_VIEW;
            string textSynchronizing;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textSynchronizing = app.Query(AutomationID.labelError).LastOrDefault().Text;
                }
                else // iOS
                {
                    textSynchronizing = extractTextContext(app.Query(c => c.Id(AutomationID.labelError).Descendant(0))[0]);
                };

                SynchronizingViewItem result = new SynchronizingViewItem(
                    textSynchronizing);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "Synchronize" };
                    string[] listOfTextsOnScreen = { textSynchronizing };
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
        /// Get QR code scanner view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static QRCodeScannerViewItem GetQRCodeScanner(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MY_WALLETS_VIEW;
            string textHeaderBar;
            string textButtonBack;
            string textButtonImagePicker;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textHeaderBar = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    textButtonBack = app.Query(AutomationID.buttonBack).LastOrDefault().Text;
                    textButtonImagePicker = app.Query(AutomationID.buttonImagePicker).LastOrDefault().Text;
                }
                else // iOS
                {
                    textHeaderBar = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textButtonBack = extractTextContext(app.Query(c => c.Id(AutomationID.buttonBack).Descendant(0))[0]);
                    textButtonImagePicker = extractTextContext(app.Query(c => c.Id(AutomationID.buttonImagePicker).Descendant(0))[0]);
                };

                QRCodeScannerViewItem result = new QRCodeScannerViewItem(
                    textHeaderBar,
                    textButtonBack,
                    textButtonImagePicker
                    );
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
    }
}