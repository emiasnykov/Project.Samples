using GluwaPro.UITest.TestUtilities.Methods.TestRailApiMethod;
using System;

namespace GluwaPro.UITest.TestUtilities.CurrencyUtils
{
    /// <summary>
    /// Active/Inactive conversion test IDs for TestRail
    /// </summary>
    class EConversionTestIDForTestRail
    {
        /// <summary>
        /// Exchange currency test IDs
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static int GetExchangeCurrencies(EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.BtcsUsdcg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_sUSDCG;
                case EConversion.sUsdcgBtc:
                    return TestIDForTestRail.EXCHANGE_sUSDCG_TO_BTC;
                /*
                case EConversion.BtcUsdg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_USDG;
                case EConversion.UsdgBtc:
                    return TestIDForTestRail.EXCHANGE_USDG_TO_BTC;
                case EConversion.BtcKrwg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_KRWG;
                case EConversion.KrwgBtc:
                    return TestIDForTestRail.EXCHANGE_KRWG_TO_BTC;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Cancel quote test IDs
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static int GetExchangeCancelQuote(EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.BtcsUsdcg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_sUSDCG_CANCEL;
                case EConversion.sUsdcgBtc:
                    return TestIDForTestRail.EXCHANGE_sUSDCG_TO_BTC_CANCEL;
                /*
                case EConversion.BtcUsdg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_USDG_CANCEL;
                case EConversion.UsdgBtc:
                    return TestIDForTestRail.EXCHANGE_USDG_TO_BTC_CANCEL;
                case EConversion.BtcKrwg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_KRWG_CANCEL;
                case EConversion.KrwgBtc:
                    return TestIDForTestRail.EXCHANGE_KRWG_TO_BTC_CANCEL;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Exchange go back test IDs
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static int GetExchangeGoBackMultiTimes(EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.BtcsUsdcg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_sUSDCG_GO_BACK_MULTIPLE_TIMES;
                case EConversion.sUsdcgBtc:
                    return TestIDForTestRail.EXCHANGE_sUSDCG_TO_BTC_GO_BACK_MULTIPLE_TIMES;
                /*
                case EConversion.BtcUsdg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_USDG_GO_BACK_MULTIPLE_TIMES;
                case EConversion.UsdgBtc:
                    return TestIDForTestRail.EXCHANGE_USDG_TO_BTC_GO_BACK_MULTIPLE_TIMES;
                case EConversion.BtcKrwg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_KRWG_GO_BACK_MULTIPLE_TIMES;
                case EConversion.KrwgBtc:
                    return TestIDForTestRail.EXCHANGE_KRWG_TO_BTC_GO_BACK_MULTIPLE_TIMES;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Exchange go back test IDs
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static int GetExchangeGoBack(EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.BtcsUsdcg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_sUSDCG_GO_BACK;
                case EConversion.sUsdcgBtc:
                    return TestIDForTestRail.EXCHANGE_sUSDCG_TO_BTC_GO_BACK;
                /*
                case EConversion.BtcUsdg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_USDG_GO_BACK;
                case EConversion.UsdgBtc:
                    return TestIDForTestRail.EXCHANGE_USDG_TO_BTC_GO_BACK;
                case EConversion.BtcKrwg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_KRWG_GO_BACK;
                case EConversion.KrwgBtc:
                    return TestIDForTestRail.EXCHANGE_KRWG_TO_BTC_GO_BACK;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Exchange expired quote test IDs
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static int GetExchangeExpiredQuote(EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.BtcsUsdcg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_sUSDCG_EXPIRED_QUOTE;
                case EConversion.sUsdcgBtc:
                    return TestIDForTestRail.EXCHANGE_sUSDCG_TO_BTC_EXPIRED_QUOTE;
                /*
                case EConversion.BtcUsdg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_USDG_EXPIRED_QUOTE;
                case EConversion.UsdgBtc:
                    return TestIDForTestRail.EXCHANGE_USDG_TO_BTC_EXPIRED_QUOTE;
                case EConversion.BtcKrwg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_KRWG_EXPIRED_QUOTE;
                case EConversion.KrwgBtc:
                    return TestIDForTestRail.EXCHANGE_KRWG_TO_BTC_EXPIRED_QUOTE;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Exchange amount over balance test IDs
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static int GetExchangeAmountOverBalance(EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.BtcsUsdcg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_sUSDCG_AMOUNT_OVER_BALANCE;
                case EConversion.sUsdcgBtc:
                    return TestIDForTestRail.EXCHANGE_sUSDCG_TO_BTC_AMOUNT_OVER_BALANCE;
                /*
                case EConversion.BtcUsdg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_USDG_AMOUNT_OVER_BALANCE;
                case EConversion.UsdgBtc:
                    return TestIDForTestRail.EXCHANGE_USDG_TO_BTC_AMOUNT_OVER_BALANCE;
                case EConversion.BtcKrwg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_KRWG_AMOUNT_OVER_BALANCE;
                case EConversion.KrwgBtc:
                    return TestIDForTestRail.EXCHANGE_KRWG_TO_BTC_AMOUNT_OVER_BALANCE;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Exchange wrong password test IDs
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static int GetExchangeWrongPassword(EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.BtcsUsdcg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_sUSDCG_WRONG_PASSWORD;
                case EConversion.sUsdcgBtc:
                    return TestIDForTestRail.EXCHANGE_sUSDCG_TO_BTC_WRONG_PASSWORD;
                /*
                case EConversion.BtcUsdg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_USDG_WRONG_PASSWORD;
                case EConversion.UsdgBtc:
                    return TestIDForTestRail.EXCHANGE_USDG_TO_BTC_WRONG_PASSWORD;
                case EConversion.BtcKrwg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_KRWG_WRONG_PASSWORD;
                case EConversion.KrwgBtc:
                    return TestIDForTestRail.EXCHANGE_KRWG_TO_BTC_WRONG_PASSWORD;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Exchange amount too small test IDs
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static int GetExchangeAmountTooSmall(EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.BtcsUsdcg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_sUSDCG_AMOUNT_TOO_SMALL;
                case EConversion.sUsdcgBtc:
                    return TestIDForTestRail.EXCHANGE_sUSDCG_TO_BTC_WRONG_PASSWORD;
                /*
                case EConversion.BtcUsdg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_USDG_AMOUNT_TOO_SMALL;
                case EConversion.UsdgBtc:
                    return TestIDForTestRail.EXCHANGE_USDG_TO_BTC_AMOUNT_TOO_SMALL;
                case EConversion.BtcKrwg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_KRWG_AMOUNT_TOO_SMALL;
                case EConversion.KrwgBtc:
                    return TestIDForTestRail.EXCHANGE_KRWG_TO_BTC_AMOUNT_TOO_SMALL;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Exchange not found test IDs
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static int GetExchangeNotFound(EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.BtcsUsdcg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_sUSDCG_NOT_FOUND;
                case EConversion.sUsdcgBtc:
                    return TestIDForTestRail.EXCHANGE_sUSDCG_TO_BTC_NOT_FOUND;
                /*
                case EConversion.BtcUsdg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_USDG_NOT_FOUND;
                case EConversion.UsdgBtc:
                    return TestIDForTestRail.EXCHANGE_USDG_TO_BTC_NOT_FOUND;
                case EConversion.BtcKrwg:
                    return TestIDForTestRail.EXCHANGE_BTC_TO_KRWG_NOT_FOUND;
                case EConversion.KrwgBtc:
                    return TestIDForTestRail.EXCHANGE_KRWG_TO_BTC_NOT_FOUND;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Exchange inactive test IDs
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static int GetExchangeInactive(EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.BtcsUsdcg:
                    return TestIDForTestRail.EXCHANGE_USDG_TO_USDG_INACTIVE;
                case EConversion.sUsdcgBtc:
                    return TestIDForTestRail.EXCHANGE_sUSDCG_TO_sUSDCG_INACTIVE;
                /*
                case EConversion.UsdgBtc:
                    return TestIDForTestRail.EXCHANGE_USDG_TO_USDG_INACTIVE;
                case EConversion.KrwgBtc:
                    return TestIDForTestRail.EXCHANGE_KRWG_TO_KRWG_INACTIVE;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }        
    }
}
