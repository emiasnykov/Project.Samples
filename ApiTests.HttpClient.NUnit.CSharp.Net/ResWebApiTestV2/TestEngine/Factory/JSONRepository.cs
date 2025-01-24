using System.Collections.Generic;
using ResWebApiTest.TestEngine.Extensions;
using ResWebApiTest.TestEngine.Managers;

namespace ResWebApiTest.TestEngine.Factory
{
    /// <summary>
    /// Value repository of JSON dictionary object values to serve Statuses, Nodes or Tools
    /// </summary>
    public class JSONRepository
    {
        #region - N/D - (N - No List / No nesting - No item list) | (D - No List / First nesting - No item list)
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// No List / First nesting - No item list
        /// Template:
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         ...
        ///         "KeyName_1X": 1
        ///     }
        /// </summary>        
        public static object Value(string _KeyName, Dictionary<string, object> _Respond, bool _Count = true)
        {
            IDictionary<string, object> wrkDict = _Respond;
            bool bKeyExists = true;

            // Add count on true
            if (_Count)
                WebApiTestManager.ResponseCount++;

            // Check if key exists
            if (!string.IsNullOrEmpty(_KeyName))
                bKeyExists = wrkDict.ContainsKey(_KeyName);

            // Try to get value from the key
            wrkDict.TryGetValue(_KeyName, out object valueObj);

            // If sth goes wrong with a key or value - raise exception
            // TODO - csharp-event. Move to other place
            TestsExceptions.ThrowExceptionOnValueFailure(_KeyName, bKeyExists, valueObj);

            return valueObj;
        }

        public static T Value<T>(string _KeyName, Dictionary<string, object> _Respond, bool _Count = true)
        {
            return WebApiTestManager.JsonSerializer.ConvertToType<T>(Value(_KeyName, _Respond, _Count));
        }

        #endregion - N/D - (N - No List / No nesting - No item list) | (D - No List / First nesting - No item list)


        #region - DD - No List / First nesting - No item list / Second nesting - No item list
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// No List / First nesting - No item list / Second nesting - No item list
        /// Template:
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": {
        ///             "KeyName_21": "KeyValue_21",
        ///             "KeyName_22": true,
        ///             ...
        ///             "KeyName_2X": "KeyValue_2x",
        ///         },
        ///         ...
        ///         "KeyName_1X": 1
        ///     }
        /// </summary>
        public static object Value(string _KeyName, string _KeyName2, Dictionary<string, object> _Respond, bool _Count = true)
        {
            IDictionary<string, object> wrkDict2 = ConvertObjectExt.ToDictionary<string, object>(Value(_KeyName, _Respond, _Count));
            bool bKeyExists = true;

            // Check if key exists
            if (!string.IsNullOrEmpty(_KeyName2))
                bKeyExists = wrkDict2.ContainsKey(_KeyName2);

            // Try to get value from the key
            wrkDict2.TryGetValue(_KeyName2, out object valueObj2);

            // If sth goes wrong with a key or value - raise exception
            // TODO - csharp-event. Move to other place
            TestsExceptions.ThrowExceptionOnValueFailure(_KeyName2, bKeyExists, valueObj2);

            return valueObj2;
        }

        public static T Value<T>(string _KeyName, string _KeyName2, Dictionary<string, object> _Respond, bool _Count = true)
        {
            return WebApiTestManager.JsonSerializer.ConvertToType<T>(Value(_KeyName, _KeyName2, _Respond, _Count));
        }

        #endregion - DD - No List / First nesting - No item list / Second nesting - No item list


        #region - DI - No List / First nesting with array 
        /// ********************************************************************************************************************************************************

        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting - With item list / Second nesting - No item list
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": [ "X", true, 1 ],
        ///         ...
        ///         "KeyName_1X": 1
        ///     }
        /// </summary>    
        public static object Value(string _KeyName, int _ItemIdx, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);

            return Value(_KeyName, _ItemIdx, valueObj, _Count);
        }

        // TODO - serve _Count
        public static object Value(string _KeyName, int _ItemIdx, object _Respond, bool _Count = true)
        {
            // This if is done on purpose to check by 'null' string to return a null object
            if (_Respond.Equals("null"))
                return null;

            IList<object> reslist = WebApiTestManager.JsonSerializer.ConvertToType<IList<object>>(_Respond);
            List<object> itmList = null;
            bool bKeyExists = false;
            bool bValueExists = false;
            int idx = 0;

            foreach (object entry in reslist)
            {
                if (idx.Equals(_ItemIdx))
                {
                    WebApiTestManager.ResponseCount--;
                    bKeyExists = true;
                    bValueExists = true;
                    itmList = new List<object>(new object[] { entry });
                    return entry;
                }
                idx++;
            }

            // If sth goes wrong with a key or value - raise exception
            // TODO - csharp-event. Move to other place
            TestsExceptions.ThrowExceptionOnValueFailure(_KeyName, bKeyExists, itmList, bValueExists);

            return null; //should not ever get up here
        }

        public static T Value<T>(string _KeyName, int _ItemIdx, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);

            return Value<T>(_KeyName, _ItemIdx, valueObj, _Count);
        }

        public static T Value<T>(string _KeyName, int _ItemIdx, object _Respond, bool _Count = true)
        {
            IList<object> reslist = WebApiTestManager.JsonSerializer.ConvertToType<IList<object>>(_Respond);
            int idx = 0;

            foreach (object entry in reslist)
            {
                if (idx.Equals(_ItemIdx))
                {
                    WebApiTestManager.ResponseCount--;
                    return WebApiTestManager.JsonSerializer.ConvertToType<T>(Value(_KeyName, _ItemIdx, _Respond, _Count));
                }
                idx++;
            }

            return default; // should not ever get up here
        }

        #endregion - DI - No List / First nesting with array


        #region - DDD - No List / First nesting - No item list / Second nesting - No item list / Third nesting - No item list
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting - No item list / Second nesting - No item list / Third nesting - No item list
        /// Template:
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": {
        ///             "KeyName_21": "KeyValue_21",
        ///             "KeyName_22": true,
        ///             "KeyName_23": {
        ///                 "KeyName_31": "KeyValue_31",
        ///                 "KeyName_32": 1.9999,
        ///                 ...
        ///                 "KeyName_3X": "KeyValue_3X",
        ///             },
        ///             ...
        ///             "KeyName_2X": "KeyValue_2x",
        ///         },
        ///         ...
        ///         "KeyName_1X": 1
        ///     }
        /// </summary>
        public static object Value(string _KeyName, string _KeyName2, string _KeyName3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            IDictionary<string, object> wrkDict3 = ConvertObjectExt.ToDictionary<string, object>(Value(_KeyName, _KeyName2, _Respond, _Count));
            bool bKeyExists = true;

            // Check if key exists
            if (!string.IsNullOrEmpty(_KeyName3))
                bKeyExists = wrkDict3.ContainsKey(_KeyName3);

            // Try to get value from the key
            wrkDict3.TryGetValue(_KeyName3, out object valueObj3);

            // If sth goes wrong with a key or value - raise exception
            // TODO - csharp-event
            TestsExceptions.ThrowExceptionOnValueFailure(_KeyName3, bKeyExists, valueObj3);

            return valueObj3;
        }

        public static T Value<T>(string _KeyName, string _KeyName2, string _KeyName3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            return WebApiTestManager.JsonSerializer.ConvertToType<T>(Value(_KeyName, _KeyName2, _KeyName3, _Respond, _Count));
        }

        #endregion - DDD - No List / First nesting - No item list / Second nesting - No item list / Third nesting - No item list


        #region - DDAD - No List / First nesting - No item list / Second nesting - With item list / Third nesting - No item list
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting - No item list / Second nesting - With item list / Third nesting - No item list
        /// Template:
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": {
        ///             "KeyName_12_A1": "KeyValue_12_A1",
        ///             "KeyName_12_A2": true,
        ///             "KeyName_12_A3": [
        ///                 {
        ///                     "KeyName_12_A3_A1": "KeyValue_12_A3_A1",
        ///                     "KeyName_12_A3_A2": true,
        ///                     "KeyName_12_A3_A3": 10       
        ///                 },
        ///                 {
        ///                     "KeyName_12_B3_B1": "KeyValue_12_B3_B1",
        ///                     "KeyName_12_B3_B2": true,
        ///                     "KeyName_12_B3_B3": 10  
        ///                 },
        ///                 ...
        ///                 {
        ///                     "KeyName_12_Z3_Z1": "KeyValue_12_Z3_Z1",
        ///                     "KeyName_12_Z3_Z2": true,
        ///                     "KeyName_12_Z3_Z3": 10  
        ///                 }
        ///             ]
        ///         },
        ///         ...
        ///         "KeyName_1X": 1
        ///    } 
        /// </summary>
        public static object Value(string _KeyName, string _KeyName2, int _ItemIdx, string _KeyName3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _KeyName2, _Respond, _Count);

            return Value(_ItemIdx, _KeyName3, valueObj, _Count);
        }

        public static T Value<T>(string _KeyName, string _KeyName2, int _ItemIdx, string _KeyName3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _KeyName2, _Respond, _Count);

            return Value<T>(_ItemIdx, _KeyName3, valueObj, _Count);
        }

        #endregion - DDAD - No List / First nesting - No item list / Second nesting - With item list / Third nesting - No item list


        #region - DAD - No List / First nesting - With item list / Second nesting - No item list
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting - With item list / Second nesting - No item list
        /// Template:
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": [
        ///             {
        ///                 "KeyName_12_A1": "KeyValue_12_A1",
        ///                 "KeyName_12_A2": true,
        ///                 ...
        ///                 "KeyName_12_AX": 10
        ///             },
        ///             {
        ///                 "KeyName_12_B1": "KeyValue_12_B1",
        ///                 "KeyName_12_B2": true,
        ///                 ...
        ///                 "KeyName_12_BX": 10
        ///             },
        ///             ...
        ///             {
        ///                 "KeyName_12_Z1": "KeyValue_12_Z1",
        ///                 "KeyName_12_Z2": true,
        ///                 ...
        ///                 "KeyName_12_ZX": 10
        ///             }
        ///         ]
        ///         ...
        ///         "KeyName_1X": 1
        ///     }
        /// </summary>
        public static object Value(string _KeyName, int _ItemIdx, string _KeyName2, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);

            return Value(_ItemIdx, _KeyName2, valueObj, _Count);
        }

        public static T Value<T>(string _KeyName, int _ItemIdx, string _KeyName2, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);

            return Value<T>(_ItemIdx, _KeyName2, valueObj, _Count);
        }

        #endregion - DAD - No List / First nesting - With item list / Second nesting - No item list


        #region - DADI - No List / First nesting - With item list / Second nesting with array
        /// *******************************************************************************************************************************************************
        
        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting - With item list / Second nesting - No item list
        /// Template:
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": [
        ///             {
        ///                 "KeyName_12_A1": "KeyValue_12_A1",
        ///                 "KeyName_12_A2": true,
        ///                 "KeyName_12_A3": [ "X", true, 1 ],
        ///                 ...
        ///                 "KeyName_12_AX": 10
        ///             },
        ///             ...
        ///         ],
        ///         ...
        ///     }
        /// </summary>
        public static object Value(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName2, valueObj, _Count);

            return Value(_KeyName2, _ItemIdx2, valueObj2, _Count);
        }

        public static T Value<T>(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName2, valueObj, _Count);

            return Value<T>(_KeyName2, _ItemIdx2, valueObj2, _Count);
        }

        #endregion - DADI - No List / First nesting - With item list / Second nesting with array


        #region - DADD - No List / First nesting - With item list / Second nesting - No item list / Third nesting - No item list
        /// *******************************************************************************************************************************************************
        
        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting - With item list / Second nesting - No item list / Third nesting - No item list
        /// Template:
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": [
        ///             {
        ///                 "KeyName_12_A1": "KeyValue_12_A1",
        ///                 "KeyName_12_A2": true,
        ///                 "KeyName_12_A3": {
        ///                     "KeyName_12_A3_A1": "KeyValue_12_A3_A1",
        ///                     "KeyName_12_A3_A2": true,
        ///                     "KeyName_12_A3_A3": 10       
        ///                 }
        ///             },
        ///             {
        ///                 "KeyName_12_B1": "KeyValue_12_B1",
        ///                 "KeyName_12_B2": true,
        ///                 "KeyName_12_B3": {
        ///                     "KeyName_12_B3_B1": "KeyValue_12_B3_B1",
        ///                     "KeyName_12_B3_B2": true,
        ///                     "KeyName_12_B3_B3": 10  
        ///                 }
        ///             },
        ///             ...
        ///             {
        ///                 "KeyName_12_Z1": "KeyValue_12_Z1",
        ///                 "KeyName_12_Z2": true,
        ///                 "KeyName_12_Z3": {
        ///                     "KeyName_12_Z3_Z1": "KeyValue_12_Z3_Z1",
        ///                     "KeyName_12_Z3_Z2": true,
        ///                     "KeyName_12_Z3_Z3": 10  
        ///                 }
        ///             }
        ///         ]
        ///         ...
        ///         "KeyName_1X": 1
        ///     }    
        /// </summary>
        public static object Value(string _KeyName, int _ItemIdx, string _KeyName2, string _KeyName3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);

            return Value(_ItemIdx, _KeyName2, _KeyName3, valueObj, _Count);
        }

        public static T Value<T>(string _KeyName, int _ItemIdx, string _KeyName2, string _KeyName3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);

            return Value<T>(_ItemIdx, _KeyName2, _KeyName3, valueObj, _Count);
        }

        #endregion - DADD - No List / First nesting - With item list / Second nesting - No item list / Third nesting - No item list


        #region - DADAD - No List / First nesting - With item list / Second nesting - with item list / Third nesting - No item list
        /// *******************************************************************************************************************************************************
        
        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting - With item list / Second nesting - with item list / Third nesting - No item list
        /// Template:
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": [
        ///             {
        ///                 "KeyName_12_A1": "KeyValue_12_A1",
        ///                 "KeyName_12_A2": true,
        ///                 ...
        ///                 "KeyName_12_AX": 10,
        ///                 "KeyName_12_AZ": [
        ///                     {
        ///                            "KeyName_12_AZ_11": "KeyValue_12_AZ_11",
        ///                            "KeyName_12_AZ_12": true,
        ///                            "KeyName_12_AZ_13": 1
        ///                        },
        ///                        {
        ///                        ...
        ///                        }
        ///                    ]
        ///             },
        ///             ...
        ///         ]
        ///     }  
        /// </summary>
        public static object Value(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, string _KeyName3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName2, valueObj, _Count);

            return Value(_ItemIdx2, _KeyName3, valueObj2, _Count);
        }

        public static T Value<T>(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, string _KeyName3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName2, valueObj, _Count);

            return Value<T>(_ItemIdx2, _KeyName3, valueObj2, _Count);
        }

        #endregion - DADAD - No List / First nesting - With item list / Second nesting - with item list / Third nesting - No item list


        #region - DADADI - No List / First nesting - With item list / Second nesting - with item list / Third nesting with array
        /// *******************************************************************************************************************************************************
        
        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting - With item list / Second nesting - with item list / Third nesting - No item list
        /// Template:
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": [
        ///             {
        ///                 "KeyName_12_A1": "KeyValue_12_A1",
        ///                 "KeyName_12_A2": true,
        ///                 ...
        ///                 "KeyName_12_AX": 10,
        ///                 "KeyName_12_AZ": [
        ///                     {
        ///                         "KeyName_12_AZ_11": "KeyValue_12_AZ_11",
        ///                         "KeyName_12_AZ_12": true,
        ///                         "KeyName_12_AZ_13": [ "X", true, 1 ],
        ///                     },
        ///                        ...
        ///                 ]
        ///             },
        ///             ...
        ///         ]
        ///     }
        /// </summary>
        public static object Value(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, string _KeyName3, int _ItemIdx3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName2, valueObj, _Count);
            var valueObj3 = Value(_ItemIdx2, _KeyName3, valueObj2, _Count);

            return Value(_KeyName3, _ItemIdx3, valueObj3, _Count);
        }

        public static T Value<T>(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, string _KeyName3, int _ItemIdx3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName2, valueObj, _Count);
            var valueObj3 = Value(_ItemIdx2, _KeyName3, valueObj2, _Count);

            return Value<T>(_KeyName3, _ItemIdx3, valueObj3, _Count);
        }

        #endregion - DADADI - No List / First nesting - With item list / Second nesting - with item list / Third nesting with array


        #region - DADADAD - No List / First nesting - With item list / Second nesting - with item list / Third nesting - with item list / Fourth nesting - No item list
        /// *******************************************************************************************************************************************************
        
        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting - With item list / Second nesting - with item list / Third nesting - with item list / Fourth nesting - No item list
        /// Template:
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": [
        ///             {
        ///                 "KeyName_12_A1": "KeyValue_12_A1",
        ///                 "KeyName_12_A2": true,
        ///                 ...
        ///                 "KeyName_12_AX": 10,
        ///                 "KeyName_12_AZ": [
        ///                     {
        ///                         "KeyName_12_AZ_11": "KeyValue_12_AZ_11",
        ///                         "KeyName_12_AZ_12": true,
        ///                         "KeyName_12_AZ_13": 1,
        ///                         "KeyName_12_AZ_14": [
        ///                             {
        ///                                 "KeyName_12_AZ_14_1A": "KeyName_12_AZ_14_1A_11";
        ///                                 "KeyName_12_AZ_14_1B": true,
        ///                                 "KeyName_12_AZ_14_1C": 1
        ///                             },
        ///                             ...
        ///                         ]
        ///                     },
        ///                     ...
        ///                 ]
        ///             },
        ///             ...
        ///         ]
        ///     }
        /// </summary>
        public static object Value(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, string _KeyName3, int _ItemIdx3, string _KeyName4, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName2, valueObj, _Count);
            var valueObj3 = Value(_ItemIdx2, _KeyName3, valueObj2, _Count);

            return Value(_ItemIdx3, _KeyName4, valueObj3, _Count);
        }

        public static T Value<T>(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, string _KeyName3, int _ItemIdx3, string _KeyName4, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value(_KeyName, _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName2, valueObj, _Count);
            var valueObj3 = Value(_ItemIdx2, _KeyName3, valueObj2, _Count);

            return Value<T>(_ItemIdx3, _KeyName4, valueObj3, _Count);
        }

        #endregion - DADADAD - No List / First nesting - With item list / Second nesting - with item list / Third nesting - with item list / Fourth nesting - No item list


        #region - AD - Big list / First nesting - No item list
        /// *******************************************************************************************************************************************************
        
        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as Big list / First nesting - No item list
        /// Template:
        ///     [                                    
        ///         {
        ///             "KeyName_1A1": "KeyValue_1A1",
        ///             "KeyName_1A2": true,
        ///             ...
        ///             "KeyName_1AZ": 1
        ///         },
        ///         ...
        ///         {
        ///             "KeyName_1X1": "KeyValue_1X1",
        ///             "KeyName_1X2: true,
        ///             ...
        ///             "KeyName_1XZ": 10
        ///         }
        ///     ]
        /// </summary>
        public static object Value(int _ArrayIdx, int _ItemIdx, string _KeyName, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);

            return Value(_ItemIdx, _KeyName, valueObj, _Count);
        }

        public static object Value(int _ItemIdx, string _KeyName, object _Respond, bool _Count = true)
        {
            IList<object> reslist = WebApiTestManager.JsonSerializer.ConvertToType<IList<object>>(_Respond);
            bool bKeyExists = false;
            bool bValueExists = false;
            int idx = 0;

            foreach (Dictionary<string, object> entry in reslist)
            {
                if (!string.IsNullOrEmpty(_KeyName)) 
                    bKeyExists = entry.ContainsKey(_KeyName);
                if (idx.Equals(_ItemIdx) && bKeyExists)
                {
                    WebApiTestManager.ResponseCount--;
                    bValueExists = true;
                    return Value(_KeyName, entry, _Count);
                }
                idx++;
            }

            // If sth goes wrong with a key or value - raise exception
            // TODO - csharp-event
            TestsExceptions.ThrowExceptionOnValueFailure(_KeyName, bKeyExists, null, bValueExists);

            return null; //should not ever get up here
        }

        public static T Value<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);

            return Value<T>(_ItemIdx, _KeyName, valueObj, _Count);
        }

        public static T Value<T>(int _ItemIdx, string _KeyName, object _Respond, bool _Count = true)
        {
            IList<object> reslist = WebApiTestManager.JsonSerializer.ConvertToType<IList<object>>(_Respond);
            int idx = 0;

            foreach (Dictionary<string, object> entry in reslist)
            {
                if (idx.Equals(_ItemIdx))
                {
                    WebApiTestManager.ResponseCount--;
                    return WebApiTestManager.JsonSerializer.ConvertToType<T>(Value(_KeyName, entry, _Count));
                }
                idx++;
            }

            return default; // should not ever get up here
        }

        #endregion - AD - Big list / First nesting - No item list


        #region - ADD - Big list / First nesting - No item list / Second nesting - No item list
        /// *******************************************************************************************************************************************************
        
        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as Big list / First nesting - No item list / Second nesting - No item list
        /// Template:
        ///     [                                    
        ///         {
        ///             "KeyName_1A1": "KeyValue_1A1",
        ///             "KeyName_1A2": true,
        ///             "KeyName_1A3": {
        ///                 "KeyName_1A3_1": "KeyValue_1A3_1",
        ///                 "KeyName_2A3_2": true,
        ///                 ...
        ///                 "KeyName_1A3_X": 10
        ///             }
        ///             ...
        ///             "KeyName_1AZ": 1
        ///         },
        ///         ...
        ///         {
        ///             "KeyName_1X1": "KeyValue_1X1",
        ///             "KeyName_1X2: true,
        ///             ...
        ///             "KeyName_1XY": {
        ///                 "KeyName_1XY_1": "KeyValue_1XY_1",
        ///                 "KeyName_2XY_2": true,
        ///                 ...
        ///                 "KeyName_1XY_X": 10
        ///             }
        ///             "KeyName_1XZ": 10
        ///         }
        ///     ]
        /// </summary>   
        public static object Value(int _ArrayIdx, int _ItemIdx, string _KeyName, string _KeyName2, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);

            return Value(_ItemIdx, _KeyName, _KeyName2, valueObj, _Count);
        }

        public static object Value(int _ItemIdx, string _KeyName, string _KeyName2, object _Respond, bool _Count = true)
        {
            IList<object> reslist = WebApiTestManager.JsonSerializer.ConvertToType<IList<object>>(_Respond);
            int idx = 0;

            foreach (Dictionary<string, object> entry in reslist)
            {
                if (idx.Equals(_ItemIdx))
                {
                    WebApiTestManager.ResponseCount--;
                    return Value(_KeyName, _KeyName2, entry, _Count);
                }
                idx++;
            }

            return null; //should not ever get up here
        }

        public static T Value<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, string _KeyName2, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);

            return Value<T>(_ItemIdx, _KeyName, _KeyName2, valueObj, _Count);
        }

        public static T Value<T>(int _ItemIdx, string _KeyName, string _KeyName2, object _Respond, bool _Count = true)
        {
            IList<object> reslist = WebApiTestManager.JsonSerializer.ConvertToType<IList<object>>(_Respond);
            int idx = 0;

            foreach (Dictionary<string, object> entry in reslist)
            {
                if (idx.Equals(_ItemIdx))
                {
                    WebApiTestManager.ResponseCount--;
                    return WebApiTestManager.JsonSerializer.ConvertToType<T>(Value(_KeyName, _KeyName2, entry, _Count));
                }
                idx++;
            }

            return default; // should not ever get up here
        }

        #endregion - ADD - Big list / First nesting - No item list / Second nesting - No item list


        #region - ADI - Big list / First nesting with array 
        /// *******************************************************************************************************************************************************
        
        /// Converts JSON part of Login response as below and returns the data as dictionary as Big list / First nesting with array 
        /// Template:
        ///     [                                    
        ///         {
        ///             "KeyName_1A1": "KeyValue_1A1",
        ///             "KeyName_1A2": true,
        ///             "KeyName_1A3": [ "X", true, 1 ],
        ///             ...
        ///             "KeyName_1AZ": 1
        ///         },
        ///         ...
        ///     ]
        public static object Value(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName, valueObj, _Count);

            return Value(_KeyName, _ItemIdx2, valueObj2);
        }

        public static T Value<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName, valueObj, _Count);

            return Value<T>(_KeyName, _ItemIdx2, valueObj2);
        }

        #endregion - ADI - Big list / First nesting with array 


        #region - ADAD - Big list / First nesting - With item list / Second nesting - No item list
        /// *******************************************************************************************************************************************************
        
        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as Big list / First nesting - With item list / Second nesting - No item list
        /// Template:
        ///     [
        ///       {...
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": [
        ///             {
        ///                 "KeyName_12_A1": "KeyValue_12_A1",
        ///                 "KeyName_12_A2": true,
        ///                 ...
        ///                 "KeyName_12_AX": 10
        ///             },
        ///             {
        ///                 "KeyName_12_B1": "KeyValue_12_B1",
        ///                 "KeyName_12_B2": true,
        ///                 ...
        ///                 "KeyName_12_BX": 10
        ///             },
        ///             ...
        ///             {
        ///                 "KeyName_12_Z1": "KeyValue_12_Z1",
        ///                 "KeyName_12_Z2": true,
        ///                 ...
        ///                 "KeyName_12_ZX": 10
        ///             }
        ///         ]
        ///         ...
        ///         "KeyName_1X": 1
        ///     ...}
        ///     ]
        /// </summary>
        public static object Value(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName, valueObj, _Count);

            return Value(_ItemIdx2, _KeyName2, valueObj2, _Count);
        }

        public static T Value<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName, valueObj, _Count);

            return Value<T>(_ItemIdx2, _KeyName2, valueObj2, _Count);
        }

        #endregion - ADAD - Big list / First nesting - With item list / Second nesting - No item list


        #region - ADADI - Big list / First nesting - With item list / Second nesting with array
        /// *******************************************************************************************************************************************************
        
        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as Big list / First nesting - With item list / Second nesting with array
        /// Template:
        ///     [
        ///       {...
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": [
        ///             {
        ///                 "KeyName_12_A1": "KeyValue_12_A1",
        ///                 "KeyName_12_A2": true,
        ///                 "KeyName_12_A3": [ "X", true, 1 ],
        ///                 ...
        ///                 "KeyName_12_AX": 10
        ///             },
        ///             ...
        ///         ]
        ///         ...
        ///         "KeyName_1X": 1
        ///     ...}
        ///     ]
        /// </summary>
        public static object Value(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, int _ItemIdx3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName, valueObj, _Count);
            var valueObj3 = Value(_ItemIdx2, _KeyName2, valueObj2, _Count);

            return Value(_KeyName2, _ItemIdx3, valueObj3);
        }

        public static T Value<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, int _ItemIdx3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName, valueObj, _Count);
            var valueObj3 = Value(_ItemIdx2, _KeyName2, valueObj2, _Count);

            return Value<T>(_KeyName2, _ItemIdx3, valueObj3);
        }

        #endregion - ADADI - Big list / First nesting - With item list / Second nesting with array


        #region - ADADAD - Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list
        /// *******************************************************************************************************************************************************
        
        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list
        /// Template:
        ///     [
        ///       {...
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": [
        ///             {
        ///                 "KeyName_12_A1": "KeyValue_12_A1",
        ///                 "KeyName_12_A2": true,
        ///                 ...
        ///                 "KeyName_12_AX": 10
        ///                 "KeyName_12_AZ": [
        ///                     {
        ///                            "KeyName_12_AZ_11": "KeyValue_12_AZ_11",
        ///                            "KeyName_12_AZ_12": true,
        ///                            "KeyName_12_AZ_13": 1
        ///                        },
        ///                        {
        ///                        ...
        ///                        }
        ///                    ]
        ///             },
        ///             {
        ///             ...
        ///             }
        ///         ]
        ///         ...
        ///         "KeyName_1X": 1
        ///     ...}
        ///     ]
        /// </summary>
        public static object Value(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, int _ItemIdx3, string _KeyName3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName, valueObj, _Count);
            var valueObj3 = Value(_ItemIdx2, _KeyName2, valueObj2, _Count);

            return Value(_ItemIdx3, _KeyName3, valueObj3, _Count);
        }

        public static T Value<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, int _ItemIdx3, string _KeyName3, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName, valueObj, _Count);
            var valueObj3 = Value(_ItemIdx2, _KeyName2, valueObj2, _Count);

            return Value<T>(_ItemIdx3, _KeyName3, valueObj3, _Count);
        }

        #endregion - ADADAD - Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list


        #region - ADADADD - Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list / Fourth nesting - No item list
        /// *******************************************************************************************************************************************************
        
        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as Big list / First nesting - With item list / Second nesting - With item list / Fourth nesting - No item list
        /// Template:
        ///     [
        ///       {...
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": [
        ///             {
        ///                 "KeyName_12_A1": "KeyValue_12_A1",
        ///                 "KeyName_12_A2": true,
        ///                 ...
        ///                 "KeyName_12_AX": 10
        ///                 "KeyName_12_AZ": [
        ///                     {
        ///                            "KeyName_12_AZ_11": "KeyValue_12_AZ_11",
        ///                            "KeyName_12_AZ_12": true,
        ///                            "KeyName_12_AZ_13": 1
        ///                            "KeyName_12_AZ_14": {
        ///                                    "KeyName_12_AZ_14_1": "KeyValue_12_AZ_14_1",
        ///                                    "KeyName_12_AZ_14_1": true,
        ///                                    "KeyName_12_AZ_14_1": 1
        ///                                }
        ///                        },
        ///                        {
        ///                        ...
        ///                        }
        ///                    ]
        ///             },
        ///             {
        ///             ...
        ///             }
        ///         ]
        ///         ...
        ///         "KeyName_1X": 1
        ///     ...}
        ///     ]
        /// </summary>
        public static object Value(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, int _ItemIdx3, string _KeyName3, string _KeyName4, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName, valueObj, _Count);
            var valueObj3 = Value(_ItemIdx2, _KeyName2, valueObj2, _Count);

            return Value(_ItemIdx3, _KeyName3, _KeyName4, valueObj3, _Count);
        }

        public static T Value<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, int _ItemIdx3, string _KeyName3, string _KeyName4, Dictionary<string, object> _Respond, bool _Count = true)
        {
            var valueObj = Value("Array" + _ArrayIdx.ToString(), _Respond, _Count);
            var valueObj2 = Value(_ItemIdx, _KeyName, valueObj, _Count);
            var valueObj3 = Value(_ItemIdx2, _KeyName2, valueObj2, _Count);

            return Value<T>(_ItemIdx3, _KeyName3, _KeyName4, valueObj3, _Count);
        }

        #endregion - ADADADD - Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list / Fourth nesting - No item list
    }
}
