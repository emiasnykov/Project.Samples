using System;

namespace GluwaAPI.TestEngine.CurrencyUtils
{
    public static class EConversionExtensions
    {
        /// <summary>
        /// To source currency
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static ECurrency ToSourceCurrency(this EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.BtcsUsdcg:
                case EConversion.BtcUsdcg:
                    return ECurrency.Btc;

                case EConversion.sUsdcgBtc:
                case EConversion.sUsdcgUsdcg:
                    return ECurrency.sUsdcg;

                case EConversion.UsdcgBtc:
                case EConversion.UsdcgsUsdcg:
                    return ECurrency.Usdcg;

                default:
                    throw new ArgumentOutOfRangeException($"No currency corresponds with the conversion");
            }
        }

        /// <summary>
        /// To exchange currency
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static ECurrency ToExchangeCurrency(this EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.sUsdcgBtc:
                case EConversion.UsdcgBtc:
                    return ECurrency.Btc;

                case EConversion.BtcsUsdcg:
                case EConversion.UsdcgsUsdcg:
                    return ECurrency.sUsdcg;

                case EConversion.BtcUsdcg:
                case EConversion.sUsdcgUsdcg:
                    return ECurrency.Usdcg;

                default:
                    throw new ArgumentOutOfRangeException($"No currency corresponds with the conversion");
            }
        }

        /// <summary>
        /// Reverse conversion
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static EConversion ToReverseConversion(this EConversion conversion)
        {
            return conversion switch
            {
                // reversed from Btc
                EConversion.sUsdcgBtc => EConversion.BtcsUsdcg,
                EConversion.UsdcgBtc => EConversion.BtcUsdcg,

                // reversed from Usdcg
                EConversion.BtcUsdcg => EConversion.UsdcgBtc,
                EConversion.sUsdcgUsdcg => EConversion.UsdcgsUsdcg,

                // reversed from sUsdcg
                EConversion.BtcsUsdcg => EConversion.sUsdcgBtc,
                EConversion.UsdcgsUsdcg => EConversion.sUsdcgUsdcg,
                _ => throw new ArgumentOutOfRangeException($"No reverse conversion for {conversion}."),
            };
        }

        /// <summary>
        /// Gets the Network Fee for Exchange
        /// </summary>
        /// <param name="conversion"></param>
        /// <param name="fee"></param>
        /// <returns></returns>
        public static decimal ToNetworkFee(this EConversion conversion, decimal fee)
        {
            switch (conversion)
            {
                case EConversion.BtcsUsdcg:
                    return 100m * fee;       // Min sUsdcg ExchangeFee

                case EConversion.BtcUsdcg:
                    return 2.6m * fee;

                case EConversion.sUsdcgBtc:
                case EConversion.UsdcgBtc:
                    return 2m * fee;

                case EConversion.UsdcgsUsdcg:
                case EConversion.sUsdcgUsdcg:
                    return 0;

                default:
                    throw new Exception($"No network fee matches with {conversion}");
            }
        }
    }
}
