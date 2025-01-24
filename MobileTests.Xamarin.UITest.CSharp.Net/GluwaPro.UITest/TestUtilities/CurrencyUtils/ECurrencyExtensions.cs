using System;

namespace GluwaPro.UITest.TestUtilities.CurrencyUtils
{
    /// <summary>
    /// Active/Inactive currency extensions
    /// </summary>
    public static class ECurrencyExtensions
    {
        /// <summary>
        /// Get default currency amount for test
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToDefaultCurrencyAmount(this ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                case ECurrency.sUsdcg:
                //case ECurrency.Usdg:
                    return "1";
                case ECurrency.Btc:
                    return "0.0001";                
                case ECurrency.sNgNg:
                //case ECurrency.NgNg:
                    return "500";
                /*
                case ECurrency.sKrwcg:
                case ECurrency.Krwg:
                    return "1000";
                */
                default:
                    throw new Exception("No existing amount for currency");
            }
        }

        /// <summary>
        /// Get currency amount for failed quote test
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToFailedQuoteAmount(this ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                case ECurrency.sUsdcg:
                //case ECurrency.Usdg:
                    return "0.0095";
                case ECurrency.Btc:
                    return "0.000123";
                case ECurrency.sNgNg:
                //case ECurrency.NgNg:
                    return "499";
                /*
                case ECurrency.sKrwcg:
                case ECurrency.Krwg:
                    return "2000";
                */
                default:
                    throw new Exception("No existing amount for currency");
            }
        }

        /// <summary>
        /// Get current currency name
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string GetMyWalletCurrencyFullname(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return AutomationID.labelUsdcGluwacoin;
                case ECurrency.sUsdcg:
                    return AutomationID.labelsUsdcGluwacoin;
                case ECurrency.sNgNg:
                    return AutomationID.labelsNgnGluwacoin;
                case ECurrency.Btc:
                    return AutomationID.labelBitcoin;
                /*
                case ECurrency.Krwg:
                    return AutomationID.labelKrwGluwacoin;
                case ECurrency.Usdg:
                    return AutomationID.labelUsdGluwacoin;
                case ECurrency.sKrwcg:
                    return AutomationID.labelSidechainKrwcGluwacoin;
                */
                default:
                    throw new Exception("No existing amount for currency");
            }
        }        

        /// <summary>
        /// Get currency for Gluwa wallet
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToGetCurrencyByMobileFormat(this ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return "USDC-G";
                case ECurrency.sUsdcg:
                    return "sUSDC-G";
                case ECurrency.sNgNg:
                    return "sNGN-G";
                case ECurrency.Btc:
                    return "BTC";
                /*
                case ECurrency.Usdg:
                    return "USD-G";
                case ECurrency.Krwg:
                    return "KRW-G";
                case ECurrency.sKrwcg:
                    return "sKRWC-G";
                case ECurrency.NgNg:
                    return "NGN-G";
                */
                default:
                    throw new Exception("No existing amount for currency");
            }
        }

        /// <summary>
        /// Test Amounts we use for AmountTooSmall
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToAmountTooSmallCurrencyAmount(this ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                case ECurrency.sUsdcg:
                //case ECurrency.Usdg:
                    return "0."; // App does not allow to enter . for minimum amount (i.e. if entering 0.9 as minimum amount, app shows 9)
                case ECurrency.Btc:
                    return "0.0000";
                case ECurrency.sNgNg:
                //case ECurrency.NgNg:
                    return "499";
                /*
                case ECurrency.Krwg:
                case ECurrency.sKrwcg:
                    return "999";
                */
                default:
                    throw new Exception("No existing smallest amount for currency");
            }
        }
    }
}
