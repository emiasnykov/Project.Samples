using GluwaAPI.TestEngine.CurrencyUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GluwaAPI.TestEngine.Utils
{
    /// <summary>
    /// Determines 
    /// </summary>
    public class TestSettingsUtil
    {
        /// <summary>
        /// Returns the conversion
        /// </summary>
        /// <returns></returns>
        public static EConversion Conversion
        {
            get
            {
                // Extracting conversion from testname
                string conversion = mTestElements.Find(c => Enum.IsDefined(typeof(EConversion), c) == true);
                return ConvertToEnum(conversion, EConversion.sUsdcgBtc);
            }
        }

        /// <summary>
        /// Returns the from currency
        /// </summary>
        /// <returns></returns>
        public static ECurrency Currency
        {
            get
            {
                string currency = mTestElements.Find(c => Enum.TryParse<ECurrency>(c, out var eCurrency));
                return ConvertToEnum(currency, ECurrency.Usdcg);
            }
        }

        /// <summary>
        /// Name gathered from test context
        /// </summary>
        private static string mTestName
        {
            get
            {
                return TestContext.CurrentContext.Test.Name;
            }
        }

        /// <summary>
        /// Test elements from test name
        /// </summary>
        private static List<string> mTestElements
        {
            get
            {
                return mTestName.Split("_", StringSplitOptions.None).ToList();
            }
        }

        /// <summary>
        /// Converts string to enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stringToConvert"></param>
        /// <returns></returns>
        public static T ConvertToEnum<T>(string stringToConvert, T defaultValue) where T : struct
        {
            if (string.IsNullOrEmpty(stringToConvert))
            {
                return defaultValue;
            }

            return Enum.TryParse(stringToConvert, true, out T result) ? result : defaultValue;
        }
    }
}
