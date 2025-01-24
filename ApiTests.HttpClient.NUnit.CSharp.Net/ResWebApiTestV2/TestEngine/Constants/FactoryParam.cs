using System.Collections.Generic;

namespace ResWebApiTest.TestEngine.Constants
{
    /// <summary>
    /// Factory constant params (non changeable)
    /// </summary
    sealed public class FactoryParam
    {
        #region Common params
        /// **************************************       

        internal const string Colon = ":";
        internal const string Comma = ",";
        internal const string CurlyBracketBeg = "{";
        internal const string CurlyBracketEnd = "}";
        internal const string Null = "null";
        internal const string Semicolon = ";";
        internal const string SquareBracketBeg = "[";
        internal const string SquareBracketEnd = "]";
        internal const string SquareBrackets = "[]";

        #endregion Common params


        #region JSONParser params
        /// **************************************

        // Decoding values
        internal const string DecodeValueDecMillionthParts = ".0000";
        internal const string DecodeValueDecZeroDecimalParts = "0.0";

        // Interpolated verbatim payload regex
        internal const string InterpolatedVerbatimPayloadRegex = @"\$""(.*?)""";

        // Main index
        internal const string MainIndex = "0";

        // Unicode regex
        internal const string UnicodeStringRegex = @"\\u([0-9a-z]{4})";

        #endregion JSONParser params


        #region JSONSchema params
        /// **************************************

        // Array placeholder name
        internal const string BigArrayPlaceholder = "Array";

        // Big array placeholder name
        internal const string SchemBigArrayPlaceholderName = BigArrayPlaceholder + MainIndex;

        // Static schema JSON templates
        // Note:
        //  This is particular place to add new templates
        public static readonly IList<string> SchemaJSONTemplate =
            new List<string>(
                new string[] {
                    "N",            // No List / No nesting - No item list
                    "D",            // No List / First nesting - No item list
                    "I",            // Not served by member, just iteration
                    "DD",           // No List / First nesting - No item list / Second nesting - No item list
                    "DI",           // No List / First nesting with array 
                    "DDD",          // No List / First nesting - No item list / Second nesting - No item list / Third nesting - No item list
                    "DDAD",         // No List / First nesting - No item list / Second nesting - With item list / Third nesting - No item list
                    "DAD",          // No List / First nesting - With item list / Second nesting - No item list  
                    "DADI",         // No List / First nesting - With item list / Second nesting with array
                    "DADD",         // No List / First nesting - With item list / Second nesting - No item list / Third nesting - No item list    
                    "DADAD",        // No List / First nesting - With item list / Second nesting - with item list / Third nesting - No item list
                    "DADADI",       // No List / First nesting - With item list / Second nesting - with item list / Third nesting with array
                    "DADADAD",      // No List / First nesting - With item list / Second nesting - with item list / Third nesting - with item list / Fourth nesting - No item list
                    "AD",           // Big list / First nesting - No item list
                    "ADD",          // Big list / First nesting - No item list / Second nesting - No item list
                    "ADI",          // Big list / First nesting with array 
                    "ADAD",         // Big list / First nesting - With item list / Second nesting - No item list
                    "ADADI",        // Big list / First nesting - With item list / Second nesting with array
                    "ADADAD",       // Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list
                    "ADADADD"       // Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list / Fourth nesting - No item list
                });

        #endregion JSONSchema params


        #region RequestTask params
        /// **************************************

        // Main StatusCode params
        internal const string RequestTaskStatusCode = "StatusCode";

        // Main possible response tokens
        internal const string RequestTaskAccessToken = "access_token";
        internal const string RequestTaskSimpleToken = "Token";

        #endregion RequestTask params


        #region Tracer params
        /// **************************************

        // Main stack tracing
        internal const int TracerInitTraceNesting = 5;
        internal const int TracerCloseTraceNesting = 3; //not used

        #endregion Tracer params
    }
}
