using System;

namespace GluwaPro.UITest.TestUtilities.CurrencyUtils
{
    /// <summary>
    /// Active/Inactive conversion extensions
    /// </summary>
    public static class EConversionExtensions
    {
        /// <summary>
        /// Return source currency
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static ECurrency ToSourceCurrency(this EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.sUsdcgBtc:
                /*
                case EConversion.sUsdcgKrwg:
                case EConversion.sUsdcgUsdg:
                case EConversion.sUsdcgsKrwcg:
                */
                    return ECurrency.sUsdcg;
                                    
                case EConversion.BtcsUsdcg:
                /*
                case EConversion.BtcKrwg:
                case EConversion.BtcUsdg:
                case EConversion.BtcNgng:
                case EConversion.BtcsNgng:
                case EConversion.BtcsKrwcg:
                */
                    return ECurrency.Btc;
                /*
                case EConversion.sNgngBtc:
                    return ECurrency.sNgNg;

                case EConversion.UsdcgKrwg:
                case EConversion.UsdcgBtc:
                case EConversion.UsdcgUsdg:
                case EConversion.UsdcgsKrwcg:
                    return ECurrency.Usdcg;

                case EConversion.sKrwcgBtc:
                case EConversion.sKrwcgsUsdcg:
                    return ECurrency.sKrwcg;

                case EConversion.NgngBtc:
                    return ECurrency.NgNg;

                case EConversion.UsdgKrwg:
                case EConversion.UsdgBtc:
                case EConversion.UsdgsUsdcg:
                    return ECurrency.Usdg;

                case EConversion.KrwgUsdg:
                case EConversion.KrwgBtc:
                case EConversion.KrwgsUsdcg:
                    return ECurrency.Krwg;
                */

                default:
                    throw new ArgumentOutOfRangeException($"No currency corresponds with the conversion");
            }
        }

        /// <summary>
        /// Return exchange currency
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static ECurrency ToExchangeCurrency(this EConversion conversion)
        {
            switch (conversion)
            {       
                case EConversion.sUsdcgBtc:
                /*
                case EConversion.UsdgBtc:
                case EConversion.KrwgBtc:
                case EConversion.sNgngBtc:
                case EConversion.NgngBtc:
                case EConversion.sKrwcgBtc:
                */
                    return ECurrency.Btc;
      
                case EConversion.BtcsUsdcg:
                /*
                case EConversion.UsdgsUsdcg:
                case EConversion.KrwgsUsdcg:
                case EConversion.sKrwcgsUsdcg:
                */
                    return ECurrency.sUsdcg;
                /*
                case EConversion.KrwgUsdg:
                case EConversion.BtcUsdg:
                case EConversion.sUsdcgUsdg:
                    return ECurrency.Usdg;
                
                case EConversion.UsdgKrwg:
                case EConversion.BtcKrwg:
                case EConversion.sUsdcgKrwg:
                    return ECurrency.Krwg;
                
                case EConversion.sUsdcgsKrwcg:
                    return ECurrency.sKrwcg;

                case EConversion.BtcNgng:
                    return ECurrency.NgNg;

                case EConversion.BtcsNgng:
                    return ECurrency.sNgNg;
                */
                default:
                    throw new ArgumentOutOfRangeException($"No currency corresponds with the conversion");
            }
        }

        /// <summary>
        /// Return reversed conversion
        /// </summary>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static EConversion ToReverseConversion(this EConversion conversion)
        {
            switch (conversion)
            {
                case EConversion.sUsdcgBtc:
                    return EConversion.BtcsUsdcg;
                case EConversion.BtcsUsdcg:
                    return EConversion.sUsdcgBtc;
                /*
                case EConversion.KrwgUsdg:
                    return EConversion.UsdgKrwg;
                case EConversion.KrwgsUsdcg:
                    return EConversion.sUsdcgKrwg;
                case EConversion.UsdgKrwg:
                    return EConversion.KrwgUsdg;
                case EConversion.sUsdcgUsdg:
                    return EConversion.UsdgsUsdcg;
                case EConversion.UsdgsUsdcg:
                    return EConversion.sUsdcgUsdg;
                case EConversion.sUsdcgKrwg:
                    return EConversion.KrwgsUsdcg;
                case EConversion.UsdgBtc:
                    return EConversion.BtcUsdg;
                case EConversion.BtcUsdg:
                    return EConversion.UsdgBtc;
                case EConversion.KrwgBtc:
                    return EConversion.BtcKrwg;
                case EConversion.BtcKrwg:
                    return EConversion.KrwgBtc;
                case EConversion.sKrwcgBtc:
                    return EConversion.BtcsKrwcg;
                case EConversion.BtcsKrwcg:
                    return EConversion.sKrwcgBtc;
                case EConversion.sKrwcgsUsdcg:
                    return EConversion.sUsdcgsKrwcg;
                case EConversion.sUsdcgsKrwcg:
                    return EConversion.sKrwcgsUsdcg;
                case EConversion.sNgngBtc:
                    return EConversion.BtcsNgng;
                case EConversion.BtcsNgng:
                    return EConversion.sNgngBtc;
                case EConversion.BtcNgng:
                    return EConversion.NgngBtc;
                case EConversion.NgngBtc:
                    return EConversion.BtcNgng;
                */
                default:
                    throw new ArgumentOutOfRangeException($"No reverse conversion for {conversion}.");
            };
        }
    }
}
