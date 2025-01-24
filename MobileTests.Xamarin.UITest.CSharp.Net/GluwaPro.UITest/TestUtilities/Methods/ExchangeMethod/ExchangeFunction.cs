using GluwaPro.UITest.TestUtilities.CurrencyUtils;
using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Models.ExchangeViewModels;
using GluwaPro.UITest.TestUtilities.Pages;
using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xamarin.UITest;

namespace GluwaPro.UITest.TestUtilities.Methods.ExchangeMethod
{
    class ExchangeFunction
    {
        public static Tuple<string, string> balanceCurrencyPair;
        private const string USDCG_USUAL_FEE = "50";
        private const string SUSDCG_USUAL_FEE = "0.5";
        private const string NGNG_USUAL_FEE = "0"; // sNGNG_USUAL_FEE is the same
        private const string BTC_USUAL_FEE = "0.001";
        public static object Enumerations { get; private set; }

        /// <summary>
        /// Calculate exchange amount
        /// </summary>
        /// <param name="quoteAmount"></param>
        /// <param name="quoteBaseAmount"></param>
        /// <param name="makerPrice"></param>
        /// <returns></returns>
        public static string CalculateExchangeAmount(string quoteAmount, string quoteBaseAmount, string makerPrice)
        {
            // Calculates exchange amount from the price in the db
            decimal quoteAmountDecimal = decimal.Parse(quoteAmount);
            decimal quoteBaseAmountDecimal = decimal.Parse(quoteBaseAmount);
            decimal makerPriceDecimal = decimal.Parse(makerPrice);
            decimal exchangeAmount = (quoteAmountDecimal * makerPriceDecimal) / quoteBaseAmountDecimal;
            string[] amountParts = exchangeAmount.ToString().Split('.');
            string exchangeAmountString = String.Join(".", amountParts[0], amountParts[1]);
            return exchangeAmountString;
        }

        /// <summary>
        /// Generate over balance (current balance + 1)
        /// </summary>
        /// <param name="balance"></param>
        /// <returns></returns>
        public static string CalculateAmountOverBalance(string balance)
        {
            decimal balanceDecimal = decimal.Parse(balance);
            decimal balanceFloor = decimal.Floor(balanceDecimal);

            return (balanceFloor + 1).ToString();
        }

        /// <summary>
        /// Calculate amount of coins with fee
        /// </summary>
        /// <param name="convertedAmount"></param>
        /// <param name="feeAmount"></param>
        /// <returns></returns>
        public static string CalculateYouSendAmount(string convertedAmount, string feeAmount)
        {
            decimal convertedAmountDecimal = decimal.Parse(convertedAmount);
            decimal feeAmountDecimal = decimal.Parse(feeAmount);
            decimal youSendDecimal = convertedAmountDecimal + feeAmountDecimal;

            return youSendDecimal.ToString();
        }
                
        /// <summary>
        /// Calculate over balance(current balance + fee)
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="currency"></param>
        /// <returns></returns>  
        public static string CalculateAmountAndFeeOverBalanceCommon(string balance, string currency)
        {
            decimal balanceDecimal = decimal.Parse(balance);
            decimal balanceFloor = decimal.Floor(balanceDecimal);

            // Updating balanceCurrencyPair for AmountAndFee over balance
            balanceCurrencyPair = new Tuple<string, string>(balance, currency);

            switch (currency)
            {
                case "USDC-G":
                    return (balanceFloor - decimal.Parse(USDCG_USUAL_FEE) / 2).ToString();
                case "sUSDC-G":
                    return (balanceFloor - decimal.Parse(SUSDCG_USUAL_FEE) / 2).ToString();
                case "sNGN-G":
                    return (balanceFloor - decimal.Parse(NGNG_USUAL_FEE) / 2).ToString();
                case "BTC":
                    return (balanceDecimal - decimal.Parse(BTC_USUAL_FEE) / 2).ToString();
                /*
                case "USD-G":
                    return (balanceFloor - decimal.Parse(USDG_USUAL_FEE) / 2).ToString();
                case "KRW-G":
                    return (balanceFloor - decimal.Parse(KRWG_USUAL_FEE) / 2).ToString();
                case "sKRWC-G":
                    return (balanceFloor - decimal.Parse(KRWG_USUAL_FEE) / 2).ToString();
                case "NGN-G":
                    return (balanceFloor - decimal.Parse(NGNG_USUAL_FEE) / 2).ToString();
                */
                default:
                    Assert.Fail("Unknown currency type");
                    return null;

            }
        }

        /// <summary>
        /// Get exchange amount on Quote page
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="conversion"></param>
        /// <param name="testCase"></param>
        /// <returns></returns>
        public static string EnterQuoteAmount(IApp app, Platform platform, EConversion conversion, ETestcaseTypes testCase, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            if (conversion != EConversion.sUsdcgBtc)
            {
                ExchangeCommonTest.SelectSendReceiveCommon(app, platform, conversion);
            }
            TapAmount(app, ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion)), testCase);
            CreateExchangeViewItem viewInfo = ExchangeViewInfo.GetCreateExchangeViewItem(app, platform, chosenLanguage, isLocalizationTest);
            return viewInfo.ExchangeAmount;
        }

        /// <summary>
        /// Must be able to tap valid amount 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="source"></param>
        /// <param name="testCase"></param>
        public static void TapAmount(IApp app, string source, ETestcaseTypes testCase)
        {
            string amount = GetAmount(source, testCase);
            Shared.TapAmountCustom(app, amount);
            ScreenshotTools.TakePosScreenshot(app, "Tapped {0} amount", amount);
        }

        /// <summary>
        /// Must be able to get amount 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="testCase"></param>
        /// <returns></returns>
        private static string GetAmount(string source, ETestcaseTypes testCase)
        {
            string amount;

            switch (testCase)
            {
                case ETestcaseTypes.QuoteNotFound: // Negative cases
                    {
                        switch (source)
                        {
                            case "BTC":
                                {
                                    amount = ECurrencyExtensions.ToDefaultCurrencyAmount(ECurrency.Btc);
                                    return amount;
                                }
                            case "sNGN-G":
                                {
                                    amount = ECurrencyExtensions.ToDefaultCurrencyAmount(ECurrency.sNgNg);
                                    return amount;
                                }
                            /*
                            case "KRW-G":
                                {
                                    amount = ECurrencyExtensions.ToDefaultCurrencyAmount(ECurrency.Krwg);
                                    return amount;
                                }
                            case "sKRWC-G":
                                {
                                    amount = ECurrencyExtensions.ToDefaultCurrencyAmount(ECurrency.sKrwcg);
                                    return amount;
                                }
                            */
                            default:
                                {
                                    // for USD-G, sUSD-G, NGN-G
                                    amount = ECurrencyExtensions.ToDefaultCurrencyAmount(ECurrency.sUsdcg);
                                    return amount;
                                }
                        }
                    }
                case ETestcaseTypes.AmountTooSmall:  // Negative cases
                    {
                        switch (source)
                        {
                            case "BTC":
                                {
                                    amount = ECurrencyExtensions.ToAmountTooSmallCurrencyAmount(ECurrency.Btc);
                                    return amount;
                                }
                            case "sNGN-G":
                                {
                                    amount = ECurrencyExtensions.ToAmountTooSmallCurrencyAmount(ECurrency.sNgNg);
                                    return amount;
                                }
                            /*
                            case "KRW-G":
                                {
                                    amount = ECurrencyExtensions.ToAmountTooSmallCurrencyAmount(ECurrency.Krwg);
                                    return amount;
                                }
                            case "sKRWC-G":
                                {
                                    amount = ECurrencyExtensions.ToAmountTooSmallCurrencyAmount(ECurrency.sKrwcg);
                                    return amount;
                                }
                            */
                            default:
                                {
                                    // for USD-G, sUSD-G, NGN-G
                                    amount = ECurrencyExtensions.ToAmountTooSmallCurrencyAmount(ECurrency.sUsdcg);
                                    return amount;
                                }

                        }

                    }
                case ETestcaseTypes.Positive:  // Positive cases
                default:
                    {            
                        switch (source)
                        {
                            case "BTC":
                                {
                                    amount = ECurrencyExtensions.ToDefaultCurrencyAmount(ECurrency.Btc);
                                    return amount;
                                }
                            case "sNGN-G":
                                {
                                    amount = ECurrencyExtensions.ToDefaultCurrencyAmount(ECurrency.sNgNg);
                                    return amount;
                                }
                            /*
                            case "KRW-G":
                                {
                                    amount = ECurrencyExtensions.ToDefaultCurrencyAmount(ECurrency.Krwg);
                                    return amount;
                                }
                            case "sKRWC-G":
                                {
                                    amount = ECurrencyExtensions.ToDefaultCurrencyAmount(ECurrency.sKrwcg);
                                    return amount;
                                }
                            */
                            default:
                                {
                                    // for USD-G, sUSD-G, NGN-G
                                    amount = ECurrencyExtensions.ToDefaultCurrencyAmount(ECurrency.sUsdcg);
                                    return amount;
                                }
                        }
                    }
            }
        }

        /// <summary>
        /// Select currency to test
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="currency"></param>
        /// <param name="bSend"></param>
        public static void SelectCurrency(IApp app, Platform platform, EConversion conversion, bool bSend)
        {
            string pickerExchangeCurrencyType;
            string pickerCurrency;
            string source;

            if (bSend == true)
            {
                pickerExchangeCurrencyType = AutomationID.sourceIcon;
                pickerCurrency = AutomationID.labelExchangeSourceCurrency;
                source = ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion));
            }
            else
            {
                pickerExchangeCurrencyType = AutomationID.targetIcon;
                pickerCurrency = AutomationID.labelExchangeTargetCurrency;
                source = ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToExchangeCurrency(conversion));
            }

            try
            {
                app.Tap(c => c.Marked(pickerExchangeCurrencyType));
            }
            catch
            {
                ReplTools.StartRepl(app);
                MethodBase methodBase = MethodBase.GetCurrentMethod();
                Assert.Fail("ERROR from {0}.{1}", methodBase.ReflectedType.Name, methodBase.Name);
            }
            string conversionCurrency;

            try
            {
                if (platform == Platform.Android) // Android
                {                   
                    string dialogListView = "select_dialog_listview";
                    app.WaitForElement(c => c.Id(dialogListView), Shared.timeOutWaitingForString + " " + "picker select", Shared.oneMin);    
                    app.Tap(c => c.Id(dialogListView).Child().Text(source));
                    Thread.Sleep(Shared.twoSec);
                }
                else // iOS
                {
                    // int elementIndex = app.Query(ECurrencyExtensions.ToGetCurrencyByMobileFormat(EConversionExtensions.ToSourceCurrency(conversion))).Count();
                    // app.Tap(c => c.Marked(source).Index(elementIndex - 1));
                    app.Tap(c => c.Marked(source));
                    Shared.PressDoneButton(app);
                    Thread.Sleep(Shared.twoSec);
                }
            }
            catch
            {
                ReplTools.StartRepl(app);
                MethodBase methodBase = MethodBase.GetCurrentMethod();
                Assert.Fail("ERROR from {0}.{1}", methodBase.ReflectedType.Name, methodBase.Name);
            }

            if (platform == Platform.Android) // Android
            {
                Thread.Sleep(Shared.twoSec);
                conversionCurrency = app.WaitForElement(pickerCurrency).LastOrDefault().Text;

                // Assert
                // Verify that the correct currency is on
                Assert.AreEqual(source, conversionCurrency);
            }
        }

        /// <summary>
        /// Tap software/hardware back button to go back 
        /// This is only for android devices
        /// </summary>
        /// <param name="app"></param>
        public static void TapHardwareBackButton(IApp app, Platform platform)
        {
            if (platform == Platform.Android)
            {
                app.Back(); // Tap Hardware back button to go back
                app.WaitForElement(AutomationID.viewExchange);
            }
            else
            {
                app.Tap(AutomationID.buttonClose);
                app.WaitForElement(AutomationID.viewExchange);
            }
        }
    }
}
