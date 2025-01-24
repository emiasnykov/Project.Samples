using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ResWebApiTest.TestEngine.Constants;
using ResWebApiTest.TestEngine.Enums;
using ResWebApiTest.TestEngine.Extensions;
using ResWebApiTest.TestEngine.Managers;
using static ResWebApiTest.TestEngine.WebApiTest;

namespace ResWebApiTest.TestEngine.Factory
{
    /// <summary>
    /// JSON object parser for the engine
    /// </summary>
    public class JSONParser
    {
        #region Properties
        /// **************************************

        // Static object list of constructed schema member items
        public static IList<String> SchemaJSON { get; set; }

        // Static schema member item flow
        // Note: 
        //  This variable is responsible of concatenating/decreasing of schema member prefix's item as a full flow
        private static string SchemaMemberItemFlow { get; set; }
        //Static member schema prefix for schema member item
        private static string SchemaTemplatePrefix { get; set; }

        // SchemaMember = Submember
        #endregion Properties

        #region Public methods
        /// **************************************

        /// <summary>
        /// Get parsed JSON contect
        /// </summary>
        /// <param name="_ObjAttached">Request [FromBody] object</param>
        /// <returns>JSON content based on attached dictionary object</returns>
        public static string ParseDictionaryObjectToJSON(Object _ObjAttached)
        {
            bool serializeObjAttached = true;
            string jsonContent = null;

            // Parse global JSON object as an array if exists
            if (_ObjAttached != null)
            {
                try 
                {                 
                    // Get dictionary
                    IDictionary<string, object> dictFromObjAttached = ConvertObjectExt.ToDictionary<string, object>(_ObjAttached);

                    // Set if key exists
                    bool bKeyExists = dictFromObjAttached.ContainsKey(FactoryParam.SchemBigArrayPlaceholderName);

                    // Do the logic when key exists
                    if (bKeyExists)
                    {
                        dictFromObjAttached.TryGetValue(FactoryParam.SchemBigArrayPlaceholderName, out object valueObj);
                        jsonContent = JsonConvert.SerializeObject(valueObj, Formatting.Indented);
                        serializeObjAttached = false;
                    }
                }
                catch {} // Do nothing in catch. Will never get too this line

                // Serailize it
                if (serializeObjAttached)
                    jsonContent = JsonConvert.SerializeObject(_ObjAttached, Formatting.Indented);
            }
            else
                // If this is not an array, leave it as it is
                jsonContent = JsonConvert.SerializeObject("", Formatting.Indented);

            return jsonContent;
        }

        /// <summary>
        /// Parse interpolated verbatim payload as stitched in variables
        /// </summary>
        /// <param name="_Payload"></param>
        /// <returns>Parsed payload</returns>
        public static string ParseInterpolatedVerbatimPayload(string _Payload)
        {
            // Find all interpolated verbatim strings
            MatchCollection matches = Regex.Matches(_Payload, FactoryParam.InterpolatedVerbatimPayloadRegex);

            // Search trough the matches and and replace the value
            foreach (Match match in matches)
            {
                bool replace = true;
                for (int i = 0; i < match.Groups.Count; i++)
                {
                    replace = !replace;
                    if (replace)
                        _Payload = _Payload.Replace(match.Groups[0].Value, match.Groups[1].Value);
                }
            }

            // Parse any unicode
            Regex regex = new Regex(FactoryParam.UnicodeStringRegex, RegexOptions.IgnoreCase);

            return Regex.Unescape(regex.Replace(_Payload.Trim(), match => char.ConvertFromUtf32(Int32.Parse(match.Groups[1].Value, System.Globalization.NumberStyles.HexNumber))));
        }

        /// <summary>
        /// Get parsed dictionary object content. See sample tests how to use it.
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_JSONContent">As itself</param>
        /// <returns>Dictionary object based on attached JSON content</returns>
        public static Dictionary<string, object> ParseJSONToDictionaryObject(ApiUri _ApiUri, string _JSONContent)
        {
            // Validate if JSON content is parseable
            string resJSON = JSONValidate.ValidateIfJSONContentIsParseable(_ApiUri, _JSONContent);

            // Do parse 
            if (!string.IsNullOrEmpty(resJSON))
                return ParseJSON(_ApiUri, resJSON);

            return null; // Should never come to this line
        }

        /// <summary>
        /// Fully parseable Dictionary (with anonymous object type) of any JSON content.
        /// Note:
        ///     Simplified method.
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_JSONContent">JSON content</param>
        /// <returns>Fully parsed Dictionary result</returns>
        public static Dictionary<string, object> ParseJSON(ApiUri _ApiUri, string _JSONContent)
        { 
            // Create JSON schema variables
            SchemaJSON = new List<string>();
            SchemaMemberItemFlow = "";
            SchemaTemplatePrefix = "";

            // Set zero expected count to be increased
            WebApiTestManager.ExpectedCount = 0;

            // Check if content comes as html. If so, get the most base information from it (as <title> node).
            // Note:
            //  This is most likely an error comming as html
            var tmpJSONContent = ParseHTMLTitleToJSONObject(_ApiUri, _JSONContent);

            // If this is an error (came back as HTML), which works with this one
            if (!string.IsNullOrEmpty(tmpJSONContent))
                _JSONContent = tmpJSONContent;

            // Parse JSON content
            return ParseJSON(_ApiUri, _JSONContent, 0, out int endIdxJsonContent);
        }

        #endregion Public methods

        #region Private methods
        /// **************************************


        #region Parser for HTML (mainly sth wrong)
        /// **************************************        

        // Parse possible html's title to JSON object
        // Note:
        //   This is most likely an error comming as html. If not, leave it.
        //   All other characters are also parametrized due to much simplified reading of the code
        //   just to avoid any typo and missunderstanding for all mentioned above.
        private static string ParseHTMLTitleToJSONObject(ApiUri _ApiUri, string _JSONContent)
        {
            string resJSON = null;

            // Check if content comes as html and has title node inside
            if ((_JSONContent.Contains(BasicEntity.HTMLDoctypeTag) || (_JSONContent.Contains(BasicEntity.HTMLHtmlTagBeg)) && (_JSONContent.Contains(BasicEntity.HTMLTitleTagBeg) && _JSONContent.Contains(BasicEntity.HTMLTitleTagEnd))))
            {
                int idx = _JSONContent.IndexOf(BasicEntity.HTMLHtmlTagBeg) + BasicEntity.HTMLHtmlTagBeg.Length;
                int length = _JSONContent.IndexOf(BasicEntity.HTMLHtmlTagEnd) - idx;
                string title = _JSONContent.Substring(idx, length);

                // For parsing, better avoid chars {}[]:; in value
                title = title.Replace("{", "(").Replace("}", ")").Replace("[", "(").Replace("]", ")").Replace(":", "-->").Replace(";", ".");

                // Construct JSON object
                // TODO in interpolated verbatim string, maybe in prittier?
                resJSON = "{\"JSONContentType\":\"HTML\"," +
                          "\"Message\":\"" + title + "\"," +
                          "\"Comment\":\"This is short base info (as <title> node) from html. If you find it as an error and FAQ didn't help, keep logs and inform CRS WEB QA (GDA).\"}";

                // If JSON content is wrongly constructed, rise exception, eventhough there might be set ThrowExceptionOnLogin.No_AsItIsExpected
                resJSON = JSONValidate.ValidateIfJSONContentIsParseable(_ApiUri, resJSON);

                // Return proper result
                if (!String.IsNullOrEmpty(resJSON))
                    return resJSON;
            }

            return resJSON;
        }

        #endregion Parser for HTML


        #region Parser for JSON 
        /// **************************************   

        // Main parsing JSON object.
        // Fully parseable dictionary (with anonymous object type) of any JSON content.
        // Recusively adds another dictionary as a value to the parent key.
        // Sophisticated method for parsing json objects which are used in QA tools for recreating tests' assertions.
        // Counts all expecting data for any possible asserts.
        // Keeps fully schema json objects instead of deserialization (for there is no model).
        // Note:
        //     Schema:
        //         All the possible schema json member modes are used as parameters and
        //         any other characters are NOT parametrized due to much simplified reading of the code
        //         just to avoid any typo and missunderstanding for all mentioned above.
        //     Logic:
        //         Keep the order of the logic (non-changeable) !!!
        // Params:
        //     _ApiUri: Enumaration for Api Uri
        //     _JSONContent: JSON object content
        //     _BeginIdxJSONContent: Recursively used begin index of the JSON content
        //     _EndIdxJSONContent: Recursively used end index of the JSON content
        // Returns:
        //     Fully parsed Dictionary result
        private static Dictionary<string, object> ParseJSON(ApiUri _ApiUri, string _JSONContent, int _BeginIdxJSONContent, out int _EndIdxJSONContent)
        {
            bool escBegin = false;
            bool escEnd = false;
            bool inQuotes = false;
            int autoKey = 0;
            string key = null;
            Dictionary<string, object> dictResult = new Dictionary<string, object>();
            List<object> arraylist = null;
            StringBuilder dictResultValue = new StringBuilder();
            Regex regex = new Regex(FactoryParam.UnicodeStringRegex, RegexOptions.IgnoreCase);

            // Base process on brackets for neutral member mode
            if (!_JSONContent.Contains("{") && !_JSONContent.Contains("}") && !_JSONContent.Equals("[]"))
            {
                SchemaTemplatePrefix = SchemaItemTemplateMode.Neutral.Prefix();

                if (_JSONContent.Length > 0)
                {
                    key = FactoryParam.MainIndex;

                    dictResultValue.Append(_JSONContent.ToString().Replace("\"", ""));
                    dictResult.Add(key, DecodeString(regex, dictResultValue.ToString()));

                    // Add to SchemaJSON
                    SchemaJSON.Add($"{SchemaTemplatePrefix}{key}");

                    _EndIdxJSONContent = _JSONContent.Length;

                    // Process manager logic
                    WebApiTestManager.ExpectedCount++;
                }
                else
                    _EndIdxJSONContent = 0; 

                return dictResult;
            }

            // For loop to build dictionary based on json content with anonymous object type
            for (int idx = _BeginIdxJSONContent; idx < _JSONContent.Length; idx++)
            {
                char jsonChar = _JSONContent[idx];

                // Process backslash char 
                if (jsonChar == '\\')
                    escBegin = !escBegin;

                // Continue processing on carret char and new line char - skip them
                if ((jsonChar == '\r') || (jsonChar == '\n'))  
                    continue;

                // Process data as !escBegin
                if (!escBegin)
                {
                    // Process double quotes char
                    if (jsonChar == '"')
                    {
                        inQuotes = !inQuotes;

                        if (!inQuotes && arraylist != null)
                        {
                            // Change the last prefix from A to I
                            ChangeLastPrefixOrIndexInSchemaItem(SchemaItemTemplateMode.Array, SchemaItemTemplateMode.Iteration, arraylist);

                            // Set proper SchemaTemplatePrefix
                            SchemaTemplatePrefix = SchemaTemplatePrefix.Remove(SchemaTemplatePrefix.Length - 1, 1);
                            SchemaTemplatePrefix += SchemaItemTemplateMode.Iteration.Prefix();

                            // Add to SchemaJSON
                            SchemaJSON.Add(UpdateValidatedSchemaItem(_ApiUri));

                            // Add item
                            arraylist.Add(DecodeString(regex, dictResultValue.ToString()));

                            // Process manager logic
                            WebApiTestManager.ExpectedCount++;

                            dictResultValue.Length = 0;
                        }

                        continue;
                    }

                    // Continue process whitespace and no inQuotes flag
                    if ((jsonChar == ' ') && (inQuotes == false))
                        continue;

                    // Process data not in quotes
                    if (!inQuotes)
                    {
                        // Switch on JSON character
                        switch (jsonChar)
                        {
                            // Process curly bracket beg
                            case '{':
                                if (idx != _BeginIdxJSONContent)
                                {
                                    // Set proper SchemaMemberItemFlow if key is not null and also item not exists by prefix and key for Dictionary item
                                    if ((key != null) && (!SchemaItemExists(SchemaItemTemplateMode.Dictionary, key)))                                    
                                        SchemaMemberItemFlow += $"{SetSchemaItem(SchemaItemTemplateMode.Dictionary, -1, key)},";
                                   
                                    // Parse it recursively again
                                    var childResult = ParseJSON(_ApiUri, _JSONContent, idx, out int childEnd);

                                    if (arraylist != null)
                                    {
                                        arraylist.Add(childResult);
                                        ChangeLastIndexInSchemaItem(SchemaItemTemplateMode.Array, arraylist.Count());
                                    }
                                    else
                                    {
                                        if (childResult.Count == 0)
                                        {
                                            dictResult.Add(key, FactoryParam.Null);

                                            // Process manager logic
                                            WebApiTestManager.ExpectedCount++;
                                        }
                                        else
                                        {
                                            dictResult.Add(key, childResult);
                                            RemoveSchemaItem(SchemaItemTemplateMode.Dictionary, key);
                                        }
                                        key = null;
                                    }
                                    idx = childEnd;
                                }
                                else
                                    // Set proper SchemaTemplatePrefix
                                    SchemaTemplatePrefix += SchemaItemTemplateMode.Dictionary.Prefix();

                                continue;

                            // Process curly bracket end
                            case '}':
                                _EndIdxJSONContent = idx;

                                if (key != null)
                                {
                                    if (arraylist != null)
                                        dictResult.Add(key, arraylist);
                                    else
                                    {
                                        dictResult.Add(key, DecodeString(regex, dictResultValue.ToString()));

                                        // Add to SchemaJSON
                                        SchemaJSON.Add(UpdateValidatedSchemaItem(_ApiUri, SchemaItemTemplateMode.Dictionary, key));

                                        // Set proper SchemaTemplatePrefix
                                        SchemaTemplatePrefix = SchemaTemplatePrefix.Remove(SchemaTemplatePrefix.Length - 1, 1);

                                        // Process manager logic
                                        WebApiTestManager.ExpectedCount++;
                                    }
                                }

                                // Set proper SchemaTemplatePrefix on arraylist null and also key null
                                if (arraylist == null && key == null)
                                    SchemaTemplatePrefix = SchemaTemplatePrefix.Remove(SchemaTemplatePrefix.Length - 1, 1);

                                return dictResult;

                            // Process square bracket beg
                            case '[':
                                arraylist = new List<object>();

                                // Set proper SchemaTemplatePrefix
                                SchemaTemplatePrefix += SchemaItemTemplateMode.Array.Prefix();

                                // Set proper SchemaMemberItemFlow if key is not null
                                if (key != null)                                
                                    SchemaMemberItemFlow += $"{SetSchemaItem(SchemaItemTemplateMode.Dictionary, -1, key)},";

                                // Refactor SchemaMemberItemFlow value
                                SchemaMemberItemFlow += $"{SetSchemaItem(SchemaItemTemplateMode.Array, arraylist.Count(), null)},";

                                continue;

                            // Process square bracket end
                            case ']':
                               var bSkipClearAll = false;

                                if (key == null)
                                {
                                    key = FactoryParam.BigArrayPlaceholder + autoKey.ToString();
                                    autoKey++;
                                }

                                if (arraylist != null && dictResultValue.Length > 0)
                                {
                                    // TODO to check what value is here, to simplify those two ifs
                                    int h = arraylist.Count();

                                    // Change the last prefix from A to I
                                    ChangeLastPrefixOrIndexInSchemaItem(SchemaItemTemplateMode.Array, SchemaItemTemplateMode.Iteration, arraylist);

                                    // Set proper SchemaTemplatePrefix
                                    SchemaTemplatePrefix = SchemaTemplatePrefix.Remove(SchemaTemplatePrefix.Length - 1, 1);
                                    SchemaTemplatePrefix += SchemaItemTemplateMode.Iteration.Prefix();

                                    // Add to SchemaJSON
                                    SchemaJSON.Add(UpdateValidatedSchemaItem(_ApiUri));

                                    // Add item
                                    arraylist.Add(DecodeString(regex, dictResultValue.ToString()));

                                    // Process manager logic
                                    WebApiTestManager.ExpectedCount++;

                                    dictResultValue.Length = 0;
                                }

                                if (arraylist != null && arraylist.Count() == 0 && dictResultValue.Length == 0)
                                {
                                    // Change the last prefix from A to I
                                    ChangeLastPrefixOrIndexInSchemaItem(SchemaItemTemplateMode.Array, SchemaItemTemplateMode.Iteration, arraylist);

                                    // Set proper proper SchemaTemplatePrefix
                                    SchemaTemplatePrefix = SchemaTemplatePrefix.Remove(SchemaTemplatePrefix.Length - 1, 1);
                                    SchemaTemplatePrefix += SchemaItemTemplateMode.Iteration.Prefix();

                                    // Add to SchemaJSON
                                    SchemaJSON.Add(UpdateValidatedSchemaItem(_ApiUri));

                                    // Add empty item
                                    arraylist.Add("");

                                    //Process manager logic
                                    WebApiTestManager.ExpectedCount++;
                                    dictResult.Add(key, FactoryParam.Null);
                                    ClearStrictDictionaryValueAndIterationNullItems(key, 0);
                                    
                                    bSkipClearAll = true;
                                }
                                else
                                    dictResult.Add(key, arraylist);

                                if (bSkipClearAll == false)
                                    ClearAllNonDefinitiveItems(key, arraylist.Count());

                                // Set proper SchemaTemplatePrefix
                                SchemaTemplatePrefix = SchemaTemplatePrefix.Remove(SchemaTemplatePrefix.Length - 1, 1);

                                arraylist = null;
                                key = null;

                                continue;

                            // Process comma
                            case ',':
                                if (arraylist == null && key != null)
                                {
                                    dictResult.Add(key, DecodeString(regex, dictResultValue.ToString().Replace("\\", "-")));

                                    // Add to SchemaJSON
                                    SchemaJSON.Add(UpdateValidatedSchemaItem(_ApiUri, SchemaItemTemplateMode.Dictionary, key));

                                    dictResultValue.Length = 0;
                                    key = null;

                                    // Process manager logic
                                    WebApiTestManager.ExpectedCount++;
                                }

                                if (arraylist != null && dictResultValue.Length > 0)
                                {
                                    // Set proper SchemaTemplatePrefix
                                    SchemaTemplatePrefix = SchemaTemplatePrefix.Remove(SchemaTemplatePrefix.Length - 1, 1);
                                    SchemaTemplatePrefix += SchemaItemTemplateMode.Iteration.Prefix();
                                    
                                    ChangeLastPrefixOrIndexInSchemaItem(SchemaItemTemplateMode.Array, SchemaItemTemplateMode.Iteration, arraylist);

                                    // Add to SchemaJSON
                                    SchemaJSON.Add(UpdateValidatedSchemaItem(_ApiUri));

                                    arraylist.Add(dictResultValue.ToString());
                                    dictResultValue.Length = 0;

                                    // Process manager logic
                                    WebApiTestManager.ExpectedCount++;
                                }

                                continue;

                            // Process colon
                            case ':':
                                key = DecodeString(regex, dictResultValue.ToString());
                                dictResultValue.Length = 0;

                                continue;

                            default:
                                // Do the default action
                                break;
                        }
                    }
                }

                dictResultValue.Append(jsonChar);

                if (escEnd) 
                    escBegin = false;

                if (escBegin) 
                    escEnd = true;
                else 
                    escEnd = false;
            }

            _EndIdxJSONContent = _JSONContent.Length - 1;

            return dictResult;
        }


        #region ParseJSON Helpers 
        /// ************************************** 
        /// Note:
        ///     Those are helpers for ParseJSON method and not used anywhere else. 

        // Decode String as a value for K, V of Dictionary result of parsing JSON content
        // Returns result od decoded string
        private static string DecodeString(Regex _Regex, string _String)
        {
            // Find any decimal numbers to be parsed
            var internalRes = Regex.Unescape(_Regex.Replace(_String, match => char.ConvertFromUtf32(int.Parse(match.Groups[1].Value, System.Globalization.NumberStyles.HexNumber))));

            // JSON returns integers as string with some '0.0' or '.0000' values, that needs to be removed as well
            if (internalRes.Contains(FactoryParam.DecodeValueDecMillionthParts) || internalRes.Equals(FactoryParam.DecodeValueDecZeroDecimalParts))
            {
                int number = int.Parse(internalRes.Substring(0, internalRes.IndexOf(".")));
                internalRes = number.ToString();
            }

            return internalRes.Trim();
        }

        // Set schema item by index or key
        private static string SetSchemaItem(SchemaItemTemplateMode _SchemaItem, int _Idx, string _Key) => 
            $"{_SchemaItem.Prefix()}{((_Idx > -1) ? $"{Convert.ToString(_Idx)}" : "")}{((!String.IsNullOrEmpty(_Key)) ? $"{_Key}" : "")}";

        // Rework schema member item flow by subcontracting schema items
        private static void ReworkSchemaMemberItemFlow(List<string> _ItemList)
        {
            var item = "";

            foreach (string itm in _ItemList)
                item += $"{itm},";

            SchemaMemberItemFlow = item;
        }

        // Get schema item list
        private static List<string> GetLastRebuildSchemaMemberItemFlow() => 
            SchemaMemberItemFlow.Remove(SchemaMemberItemFlow.Length - 1, 1).Split(',').ToList();

        // Check if schema item exists by prefix and key
        private static bool SchemaItemExists(SchemaItemTemplateMode _SchemaItem, string _Key) => 
            SchemaMemberItemFlow.Contains(SetSchemaItem(_SchemaItem, -1, _Key));

        // Get schema item with ApiUri, Prefix and Key
        private static string UpdateValidatedSchemaItem(ApiUri _ApiUri, SchemaItemTemplateMode _SchemaItem, string _Key)
        {
            // Throw exception if no maching schema JSON template and missing Value
            JSONValidate.ValidateSchemaTemplateItemWithSchemaJSONTemplate(_ApiUri, SchemaTemplatePrefix);

            return $"{SchemaMemberItemFlow}{SetSchemaItem(_SchemaItem, -1, _Key)}";
        }

        // Get schema item with ApiUri
        private static string UpdateValidatedSchemaItem(ApiUri _ApiUri)
        {
            // Throw exception if no maching schema JSON template and missing Value
            JSONValidate.ValidateSchemaTemplateItemWithSchemaJSONTemplate(_ApiUri, SchemaTemplatePrefix);

            return $"{SchemaMemberItemFlow.Remove(SchemaMemberItemFlow.Length - 1, 1)}";
        }

        // Check if non-definitive schema template prefix exists
        private static bool SchemaTemplatePrefixExists(SchemaItemTemplateMode _SchemaItem) => SchemaTemplatePrefix.Contains(_SchemaItem.Prefix());

        // TODO will be removed when ifs are done
        // Pick what to be changed based on arrays count
        private static void ChangeLastPrefixOrIndexInSchemaItem(SchemaItemTemplateMode _OldSchemaItem, SchemaItemTemplateMode _NewSchemaItem, List<object> _Arraylist)
        {
            int arrCount = _Arraylist.Count();

            // Pick what and where to be changed
            if (arrCount == 0)
                ChangeLastPrefixInSchemaItem(_OldSchemaItem, _NewSchemaItem, 0);
            else
                ChangeLastIndexInSchemaItem(_NewSchemaItem, arrCount);
        }

        // Clear all non-definitive schema items (by prefix: A,D,I) on finished, by key or count
        // Do it for A,D,I
        private static void ClearAllNonDefinitiveItems(string _Key, int _Count)
        {
            // Dictionary
            if (SchemaTemplatePrefixExists(_SchemaItem: SchemaItemTemplateMode.Dictionary))
                RemoveSchemaItem(SchemaItemTemplateMode.Dictionary, _Key);

            // Array
            if (SchemaTemplatePrefixExists(_SchemaItem: SchemaItemTemplateMode.Array))
                RemoveSchemaItem(SchemaItemTemplateMode.Array, _Count);

            // Iteration
            if (SchemaTemplatePrefixExists(_SchemaItem: SchemaItemTemplateMode.Iteration))
                RemoveSchemaItem(SchemaItemTemplateMode.Iteration, _Count);
        }

        // Change last prefix in schema itema by old prefix with new prefix and new index
        private static void ChangeLastPrefixInSchemaItem(SchemaItemTemplateMode _OldSchemaItem, SchemaItemTemplateMode _NewSchemaItem, int _Idx)
        {
            // Grab and remove what is not needed from the list
            List<string> itmLst = GetLastRebuildSchemaMemberItemFlow();

            // Make sure of proper input
            if (_OldSchemaItem.Prefix().Equals(SchemaItemTemplateMode.Array.Prefix()) && _Idx > -1)
            {
                // Reverse the list from top to bottom (for work)
                itmLst.Reverse();

                // Rework SchemaItemTemplateMode with old and new item by the passed index
                var item = SetSchemaItem(_OldSchemaItem, _Idx, null);
                var idx = itmLst.IndexOf(item);
                itmLst[idx] = SetSchemaItem(_NewSchemaItem, _Idx, null);

                // Reverse the list from bottom to top (to get original sort)
                itmLst.Reverse();
            }
            else
                itmLst.Clear();

            // Rebuild schema member with items
            ReworkSchemaMemberItemFlow(itmLst);
        }

        // Change last index in schema itema by prefix and new index
        private static void ChangeLastIndexInSchemaItem(SchemaItemTemplateMode _SchemaItem, int _NewIdx)
        {
            // Grab and remove what is not needed from the list
            List<string> itmLst = GetLastRebuildSchemaMemberItemFlow();

            // Make sure of proper input
            if ((_SchemaItem.Prefix().Equals(SchemaItemTemplateMode.Array.Prefix()) || _SchemaItem.Prefix().Equals(SchemaItemTemplateMode.Iteration.Prefix())) && _NewIdx > -1)
            {
                // Reverse the list from top to bottom (for work)
                itmLst.Reverse();

                // Rework SchemaItemTemplateMode with old and new item by the previous ans new index
                var item = SetSchemaItem(_SchemaItem, _NewIdx - 1, null);
                var idx = itmLst.IndexOf(item);
                itmLst[idx] = SetSchemaItem(_SchemaItem, _NewIdx, null);

                // Reverse the list from bottom to top (to get original sort)
                itmLst.Reverse();
            }
            else
                itmLst.Clear();

            // Rebuild schema member with items
            ReworkSchemaMemberItemFlow(itmLst);
        }

        // Clear last Dictionary (with value) + Iteration (null) 
        private static void ClearStrictDictionaryValueAndIterationNullItems(string _Key, int _Count)
        {
            // Grab and remove what is not needed from the list
            List<string> itmLst = GetLastRebuildSchemaMemberItemFlow();

            // Reverse the list from top to bottom (for work)
            itmLst.Reverse();

            // Get Iteration item and remove it
            var item = SetSchemaItem(SchemaItemTemplateMode.Iteration, _Count, null);
            itmLst.Remove(item);

            // Check the proper item key to be remove the Dictionary item
            if (_Key != null && _Key != FactoryParam.SchemBigArrayPlaceholderName)
            {
                // Get Dictionary item and remove it
                item = SetSchemaItem(SchemaItemTemplateMode.Dictionary, -1, _Key);
                itmLst.Remove(item);
            }

            // Reverse the list from bottom to top (to get original sort)
            itmLst.Reverse();

            // Rebuild schema member with items
            ReworkSchemaMemberItemFlow(itmLst);
        }

        // Remove schema item by prefix and Array index
        private static void RemoveSchemaItem(SchemaItemTemplateMode _SchemaItem, int _Idx)
        {
            // Grab and remove what is not needed from the list
            List<string> itmLst = GetLastRebuildSchemaMemberItemFlow();

            // Make sure of proper input
            if (_SchemaItem.Prefix().Equals(SchemaItemTemplateMode.Array.Prefix()) && _Idx > -1)
            {
                // Reverse the list from top to bottom (for work)
                itmLst.Reverse();

                // Get expected item and remove it
                var item = SetSchemaItem(_SchemaItem, _Idx, null);
                itmLst.Remove(item);

                // Reverse the list from bottom to top (to get original sort)
                itmLst.Reverse();
            }
            else
                itmLst.Clear();

            // Rebuild schema member with items
            ReworkSchemaMemberItemFlow(itmLst);
        }

        // Remove schema item by prefix and Dictionary key
        private static void RemoveSchemaItem(SchemaItemTemplateMode _SchemaItem, string _Key)
        {
            // Grab and remove what is not needed from the list
            List<string> itmLst = GetLastRebuildSchemaMemberItemFlow();

            // Make sure of proper input
            if (_SchemaItem.Prefix().Equals(SchemaItemTemplateMode.Dictionary.Prefix()) && _Key != null)
            {
                // Get expected item and remove it
                var item = SetSchemaItem(_SchemaItem, -1, _Key);
                itmLst.Remove(item);
            }
            else
                itmLst.Clear();

            // Rebuild schema member with items
            ReworkSchemaMemberItemFlow(itmLst);
        }

        #endregion ParseJSON Helpers  


        #endregion Parser for JSON


        #endregion Private methods
    }
}
