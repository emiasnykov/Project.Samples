using System;
using System.Text;

namespace ResWebApiTest.TestEngine.QA_InternalTools
{
    /// <summary>
    /// Random data generator class used in Api tests
    /// </summary>
    public class QA_RandomDataGenerator
    {
        // Instantiate random number generator.  
        private static readonly Random _random = new Random();

        #region Public methods
        /// **************************************

        /// <summary>
        /// Generates random fixed data (ie: password)
        /// </summary>
        /// <returns>4-LowerCase + 4-Digits + 2-UpperCase</returns>
        public static string GenerateRandomFixedData()
        {
            var dataBuilder = new StringBuilder();

            // 4-Letters lower case   
            dataBuilder.Append(RandomString(4, true));

            // 4-Digits between 1000 and 9999  
            dataBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case  
            dataBuilder.Append(RandomString(2));

            return dataBuilder.ToString();
        }

        #endregion public methods

        #region Private methods
        /// **************************************

        // Generates a random number within a range    
        private static int RandomNumber(int _Min, int _Max)
        {
            return _random.Next(_Min, _Max);
        }

        // Generates a random string with a given size   
        private static string RandomString(int _Size, bool _LowerCase = false)
        {
            var builder = new StringBuilder(_Size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = _LowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < _Size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return _LowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        #endregion Private methods
    }
}
