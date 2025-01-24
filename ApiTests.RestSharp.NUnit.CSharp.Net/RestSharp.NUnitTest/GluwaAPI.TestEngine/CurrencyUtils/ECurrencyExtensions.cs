using System;

namespace GluwaAPI.TestEngine.CurrencyUtils
{
    public static class ECurrencyExtensions
    {
        /// <summary>
        /// Get default currency amount for test
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static decimal ToDefaultCurrencyAmount(this ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.sUsdcg:
                case ECurrency.Usdcg:
                case ECurrency.sSgDg:
                    return 1m;
                case ECurrency.Btc:
                    return 0.0001m;
                case ECurrency.NgNg:
                case ECurrency.sNgNg:
                    return 501m;
                default:
                    throw new Exception("No existing amount for currency");
            }
        }

        /// <summary>
        /// Test Amounts we use for AmountTooSmall
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static decimal ToAmountTooSmallCurrencyAmount(this ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.sUsdcg:
                case ECurrency.Usdcg:
                    return 0.99m;
                case ECurrency.Btc:
                    return 0.000099m;
                default:
                    throw new Exception("No existing smallest amount for currency");
            }
        }

        /// <summary>
        /// Get default currency fee for test
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToDefaultCurrencyFee(this ECurrency currency, decimal fee)
        {
            switch (currency)
            {
                case ECurrency.sUsdcg:
                case ECurrency.Usdcg:
                    return (2m * fee).ToString();
                case ECurrency.Btc:
                    return (2.5m * fee).ToString();
                default:
                    throw new Exception("No existing fee for currency");
            }
        }

        /// <summary>
        /// Return Expired X-Request Signature
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToExpiredXRequestSignature(this ECurrency currency)
        {
            return currency switch
            {
                ECurrency.NgNg => "**********************************************",
                ECurrency.sNgNg => "**********************************************",
                ECurrency.sUsdcg => "**********************************************",
                ECurrency.Btc => "**********************************************",
                ECurrency.Usdcg => "**********************************************",
                ECurrency.Eth => "**********************************************",
                ECurrency.Usdc => "**********************************************",
                ECurrency.Usdt => "**********************************************",
                ECurrency.Gcre => "**********************************************",
                ECurrency.Gate => "**********************************************",
                _ => throw new Exception($"No expired X-Request Signature available for {currency}"),
            };
        }
    }
}
