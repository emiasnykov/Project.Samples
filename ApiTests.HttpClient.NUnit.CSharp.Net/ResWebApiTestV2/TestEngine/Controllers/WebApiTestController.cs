using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using ResWebApiTest.TestEngine.Factory;
using ResWebApiTest.TestEngine.Managers;
using ResWebApiTest.TestEngine.QA_InternalTools;

namespace ResWebApiTest.TestEngine
{
    /// <summary>
    /// WebApiTest singleton controller class - Full lazy instantiation
    ///     Here, instantiation is triggered by the first reference to the static member of the nested class, which only occurs in the Instance. 
    ///     This means the implementation is fully lazy, but has all the performance benefits of the any singletons' 
    /// Note: 
    ///     Although nested classes have access to the enclosing class's private members, the reverse is not true, 
    ///         hence the need for the instance to be internal here. 
    ///     That doesn't raise any other problems, though, as the class itself is private. 
    /// </summary>
    public sealed class WebApiTest : ApiController
    {
        #region Properties
        /// **************************************

        // Note:
        //  All below the properties have to stay here as part of WebApiTest to be used in tests

        // Uri
        // Static part of Api URI login
        public static string ApiUriLogin { get; set; }

        // Static part of Api URI request
        public static string ApiUriRequest { get; set; }

        #endregion Properties

        #region C-tor
        /// **************************************
        
        // DO NOTE TOUCH C-tor to not spoil full lazy instantiation
        // Note:
        //  This c-tor needs to be empty !
        private WebApiTest()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static WebApiTest Instance { get { return Nested.instance; } }

        // Nested internal instance
        private class Nested : IDisposable
        {
            // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
            // Note:
            //  This nesting needs to be empty !
            static Nested()
            {
            }

            // Internal WebApiTest instance for lazy instantiation
            internal static readonly WebApiTest instance = new WebApiTest();

            #region IDisposable Implementation

            // Flag for beeing disposed or not
            protected bool disposed;

            // Dispoze lazy instatiation
            protected virtual void Dispose(bool disposing)
            {
                lock (this)
                {
                    // Do nothing if the object has already been disposed of.
                    if (disposed) return;

                    // If the object is still not disposed
                    if (disposing)
                    {
                        // Release disposable objects used by this instance here.
                        if (instance != null)
                            instance.Dispose();
                    }

                    // Release unmanaged resources here. Don't access reference type fields.
                    // Remember that the object has been disposed of.
                    disposed = true;
                }
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public virtual void Dispose()
            {
                // Dispose it
                Dispose(true);

                // Unregister object for finalization.
                GC.SuppressFinalize(this);
            }

            #endregion
        }

        #endregion C-tor

        #region Teardown
        /// **************************************

        /// <summary>
        /// Vitality of trearing down all variables
        /// Note: 
        ///     This have to stay here for beeing called from WebApi tests    
        /// </summary>
        public static void ClearAll()
        {
            // Call the engine and return expected value
            WebApiTestManager.TeardownAll();
        }

        #endregion Teardown

        #region Api/Uri
        /// **************************************

        /// <summary>
        /// Enumaration for Api Uri part for Login or Request
        ///     Login		- for login request
        ///     Request		- for main request
        /// Note: 
        ///     This have to stay here for beeing called from WebApi tests    
        /// </summary>
        public enum ApiUri
        {
            Login,
            Request
        };

        #endregion Api/Uri

        #region Request
        /// **************************************

        /// <summary>
        /// Enumaration for throws.
        ///     Always              - for any throws
        ///     No_AsItIsExpected   - for not throwing any exception it is expected
        /// Note: 
        ///     This have to stay here for beeing called from WebApi tests    
        /// </summary>
        public enum ThrowAnyException 
        { 
            Always, 
            No_AsItIsExpected 
        }

        /// <summary>
        /// Send request with [FromBody] JSON string
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, _ApiUri, _JSONContent, _ThrowException);
        ///     Where:
        ///         _HttpMethod     - is it for POST, PUT, GET, DELETE
        ///         _ApiUri         - is the enumeration of authorization (for Login) or not (for Request)
        ///         _JSONContent    - is requested string caming from the request needs to be more factorized, because:
        ///                             it is comming from the code itself and needs to be parsed in a proper way
        ///         _ThrowException - is the flag that informs us to thow exception is anything goes wwrong or keep it calm as it is requested
        /// </summary>
        /// <param name="_HttpMethod">As iselt</param>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_JSONContent">Request [FromBody] JSON content</param>
        /// <param name="_ThrowException">Exceptions will be thrown, eventhough the failure messages will be kept and saved to the log file</param>/// 
        /// <returns>Response dictionary object</returns>
        public static Dictionary<string, object> SendRequest(HttpMethod _HttpMethod, 
                                                             ApiUri _ApiUri, 
                                                             string _JSONContent, 
                                                             ThrowAnyException _ThrowException = ThrowAnyException.Always)
        {
            // Call the engine and return expected value
            return WebApiTestManager.SendRequest(_HttpMethod, _ApiUri, _JSONContent, _ThrowException);
        }

        /// <summary>
        /// Send request with [FromBody] JSON string
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, _ApiUri, _JSONContent, _ThrowException);
        ///     Where:
        ///         _HttpMethod     - is for POST, PUT, GET, DELETE
        ///         _ApiUri         - is the enumeration of authorization (for Login) or not (for Request)
        ///         _ObjAttached    - is requested object that is caming from the request as itself (no refactor is needed) or non of possibilities
        ///         _ThrowException - is the flag that informs us to thow exception is anything goes wwrong or keep it calm as it is requested
        /// </summary>
        /// <param name="_HttpMethod">As itself</param>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_ObjAttached">Request [FromBody] object</param>
        /// <param name="_ThrowException">Exceptions will be thrown only for Login, eventhough the failure messages will be kept and saved to the log file</param>
        /// <returns>Response dictionary object</returns>
        public static Dictionary<string, object> SendRequest(HttpMethod _HttpMethod, 
                                                            ApiUri _ApiUri, 
                                                            object _ObjAttached = null,
                                                            ThrowAnyException _ThrowException = ThrowAnyException.Always)
        {
            // Call the engine and return expected value
            return WebApiTestManager.SendRequest(_HttpMethod, _ApiUri, _ObjAttached, _ThrowException);
        }

        #endregion Request

        #region Count
        /// **************************************

        /// <summary>
        /// Enumaration for Count.
        ///     Always  - for always check count
        ///     Ignore  - for ignore check count
        /// Note: 
        ///     This have to stay here for beeing called from WebApi tests    
        /// </summary>
        public enum CheckCount 
        { 
            Always, 
            Ignore 
        }

        /// <summary>
        /// Check count based on Expected JSON object count and Test's asserts count
        /// Note: 
        ///     This have to stay here for beeing called from WebApi tests   
        /// </summary>
        /// <param name="_CheckCount">Enumeration check count</param>
        /// <returns>True, if both counts are the same</returns>
        public static bool IsCountCorrect(CheckCount _CheckCount = CheckCount.Always)
        {
            return WebApiTestManager.IsCountCorrect(_CheckCount);
        }

        /// <summary>
        /// Send info on failure count
        /// Note: 
        ///     This have to stay here for beeing called from WebApi tests   
        /// </summary>
        /// <returns>Message on any count failure</returns>
        public static string InfoCountOnFailure()
        {
            return WebApiTestManager.InfoCountOnFailure();
        }

        #endregion Count

        #region Statuses/Nodes
        /// **************************************


        #region Statuses


        #region - N/D - (N - No List / No nesting - No item list) | (D - No List / First nesting - No item list)
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary
        /// Template:
        ///     {...
        ///         "KeyName1": "KeyValue1",
        ///         ...
        ///         "KeyNameX": "KeyValueX"
        ///     ...}
        /// Usage:
        ///     WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Login, _CredentialsObject);
        ///     Where:
        ///         _CredentialsObject is credentials object
        ///     ...
        ///     WebApiTest.Status(WebApiTest.ApiUri.Login,_KeyName);
        /// Example:  
        ///     WebApiTest.Status(WebApiTest.ApiUri.Login, "TokenType") == "bearer"
        /// Note: This have to stay here for beeing called from WebApi tests   
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_KeyName">Key name</param>
        /// <returns>Status value</returns>
        public static string Status(ApiUri _ApiUri, string _KeyName)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, WebApiUri.GetPathResponse(_ApiUri), false));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Status<T>(_KeyName);
        /// Example:  
        ///     WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Login, "StatusCode") == HttpStatusCode.BadRequest
        ///     WebApiTest.Status<IList<string>>("ErrorList") == new List<string>(new string[] { "The following errors were detected:", "No matching values found" })
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <typeparam name="T">T type cast</typeparam>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_KeyName">Key name</param>
        /// <returns>Status value as T type</returns>
        public static T Status<T>(ApiUri _ApiUri, string _KeyName)
        {
            return JSONRepository.Value<T>(_KeyName, WebApiUri.GetPathResponse(_ApiUri), false);
        }

        #endregion - N/D - (N - No List / No nesting - No item list) | (D - No List / First nesting - No item list)


        #region - DI - No List / First nesting with array
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary
        /// Template:
        ///     {...
        ///         "KeyName1": "KeyValue1",
        ///         ...
        ///         "KeyNameX": "KeyValueX"
        ///     ...}
        /// Usage:
        ///     WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Login, _CredentialsObject);
        ///     Where:
        ///         _CredentialsObject is credentials object
        ///     ...
        ///     WebApiTest.Status(WebApiTest.ApiUri.Login,_KeyName);
        /// Example:  
        ///     WebApiTest.Status(WebApiTest.ApiUri.Login, "TokenType") == "bearer"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_KeyName">Key name</param>
        /// <returns>Status value</returns>
        public static string Status(ApiUri _ApiUri, string _KeyName, int _ItemIdx)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, _ItemIdx, WebApiUri.GetPathResponse(_ApiUri), false));
        }

        #endregion - DI - No List / First nesting with array


        #endregion Statuses


        #region Nodes


        #region - N - No List / No nesting - No item list
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// Converts JSON part of Request response as below and returns the data as dictionary as No List / No nesting - No item list
        /// Template:
        ///    "Value"
        ///   Or:
        ///     true
        ///   Or:
        ///     false
        ///   Or:
        ///     123
        ///   etc.  
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node();
        /// Example:  
        ///     WebApiTest.Node() == "EN"
        ///     WebApiTest.Node() == "true"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <returns>Node value</returns>
        public static string Node()
        {
            return Convert.ToString(JSONRepository.Value("0", WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>();
        /// Example:  
        ///     WebApiTest.Node<int>() == 5
        ///     WebApiTest.Node<bool>() == true
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <typeparam name="T">T type cast</typeparam>
        /// <returns>Node value as T type</returns>
        public static T Node<T>()
        {
            return JSONRepository.Value<T>("0", WebApiTestManager.RequestResponse);
        }

        #endregion - N - No List / No nesting - No item list


        #region - D - No List / First nesting - No item list
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting - No item list
        /// Template:
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         ...
        ///         "KeyName_1X": 1
        ///     }
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_KeyName);
        /// Example:  
        ///     WebApiTest.Node("KeyName_11") == "KeyValue_11"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <returns>Node value</returns>
        public static string Node(string _KeyName)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_KeyName);
        /// Example:  
        ///     WebApiTest.Node<int>("KeyName_1X") == 1
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(string _KeyName)
        {
            return JSONRepository.Value<T>(_KeyName, WebApiTestManager.RequestResponse);
        }

        #endregion - D - No List / First nesting - No item list


        #region - DD - No List / First nesting - No item list / Second nesting - No item list
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting - No item list / Second nesting - No item list
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_KeyName, _KeyName2);
        /// Example:  
        ///     WebApiTest.Node("KeyName_12", "KeyName_21") == "KeyValue_21"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <returns>Node value</returns>
        public static string Node(string _KeyName, string _KeyName2)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, _KeyName2, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_KeyName, _KeyName2);
        /// Example:  
        ///     WebApiTest.Node<bool>("KeyName_12", "KeyName_22") == true
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(string _KeyName, string _KeyName2)
        {
            return JSONRepository.Value<T>(_KeyName, _KeyName2, WebApiTestManager.RequestResponse);
        }

        #endregion - DD - No List / First nesting - No item list / Second nesting - No item list


        #region - DI - No List / First nesting with array 
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting with array 
        /// Template:
        ///     {
        ///         "KeyName_11": "KeyValue_11",
        ///         "KeyName_12": [ "X", true, 1 ],
        ///         ...
        ///         "KeyName_1X": 1
        ///     }
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_KeyName, _ItemIdx);
        /// Example:  
        ///     WebApiTest.Node("KeyName_12", 0) == "X"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <returns>Node value</returns>
        public static string Node(string _KeyName, int _ItemIdx)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, _ItemIdx, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_KeyName, _ItemIdx);
        /// Example:  
        ///     WebApiTest.Node<bool>("KeyName_12", 1) == true
        ///     WebApiTest.Node<int>("KeyName_12", 2) == 1
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param> 
        /// <returns>Node value as T type</returns>
        public static T Node<T>(string _KeyName, int _ItemIdx)
        {
            return JSONRepository.Value<T>(_KeyName, _ItemIdx, WebApiTestManager.RequestResponse);
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_KeyName, _KeyName2, _KeyName3);
        /// Example:  
        ///     WebApiTest.Node("KeyName_12", "KeyName_23", "KeyName_31") == "KeyValue_31"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <param name="_KeyName3">Key name 3</param>
        /// <returns>Node value</returns>
        public static string Node(string _KeyName, string _KeyName2, string _KeyName3)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, _KeyName2, _KeyName3, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_KeyName, _KeyName2, _KeyName3);
        /// Example:  
        ///     WebApiTest.Node<decimal>("KeyName_12", "KeyName_22", "KeyName_32") == 1.9999
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <param name="_KeyName3">Key name 3</param>/// 
        /// <returns>Node value as T type</returns>
        public static T Node<T>(string _KeyName, string _KeyName2, string _KeyName3)
        {
            return JSONRepository.Value<T>(_KeyName, _KeyName2, _KeyName3, WebApiTestManager.RequestResponse);
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_KeyName, _KeyName2, _ItemIdx, _KeyName3);
        /// Example:  
        ///     WebApiTest.Node("KeyName_12", "KeyName_12_A1", 0, "KeyName_12_A3_A1") == "KeyValue_12_A3_A1"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_KeyName2">Key name 2/param>
        /// <param name="_ItemIdx">Indexed item in the list(0)
        /// <param name="_KeyName3">Key name 3</param>
        /// <returns>Node value</returns>
        public static string Node(string _KeyName, string _KeyName2, int _ItemIdx, string _KeyName3)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, _KeyName2, _ItemIdx, _KeyName3, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_KeyName, _ItemIdx, _KeyName2);
        /// Example:  
        ///     WebApiTest.Node<bool>("KeyName_12", "KeyName_12_A3", 0, "KeyName_12_A3_A2") == true
        ///     WebApiTest.Node<int>("KeyName_12", "KeyName_12_B3", 1, "KeyName_12_B3_B3") == 10
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_KeyName2">Key name 2/param>
        /// <param name="_ItemIdx">Indexed item in the list(0)
        /// <param name="_KeyName3">Key name 3</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(string _KeyName, string _KeyName2, int _ItemIdx, string _KeyName3)
        {
            return JSONRepository.Value<T>(_KeyName, _KeyName2, _ItemIdx, _KeyName3, WebApiTestManager.RequestResponse);
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_KeyName, _ItemIdx, _KeyName2);
        /// Example:  
        ///     WebApiTest.Node("KeyName_12", 0, "KeyName_12_A1") == "KeyValue_12_A1"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <returns>Node value</returns>
        public static string Node(string _KeyName, int _ItemIdx, string _KeyName2)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, _ItemIdx, _KeyName2, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_KeyName, _ItemIdx, _KeyName2);
        /// Example:  
        ///     WebApiTest.Node<bool>("KeyName_12", 0, "KeyName_12_A2") == true
        ///     WebApiTest.Node<int>("KeyName_12", 1, "KeyName_12_BX") == 10
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(string _KeyName, int _ItemIdx, string _KeyName2)
        {
            return JSONRepository.Value<T>(_KeyName, _ItemIdx, _KeyName2, WebApiTestManager.RequestResponse);
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2);
        /// Example:  
        ///     WebApiTest.Node("KeyName_12", 0, "KeyName_12_A3", 0) == "X"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <param name="_ItemIdx2">Indexed item 2 in the list(0)</param>
        /// <returns>Node value</returns>
        public static string Node(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2);
        /// Example:  
        ///     WebApiTest.Node<bool>("KeyName_12", 0, "KeyName_12_A3", 1) == true
        ///     WebApiTest.Node<int>("KeyName_12", 0, "KeyName_12_A3", 2) == 10
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <param name="_ItemIdx2">Indexed item 2 in the list(0)</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2)
        {
            return JSONRepository.Value<T>(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, WebApiTestManager.RequestResponse);
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_KeyName, _ItemIdx, _KeyName2, KeyName3);
        /// Example:  
        ///     WebApiTest.Node("KeyName_12", 0, "KeyName_12_A1", "KeyName_12_A3_A1") == "KeyValue_12_A3_A1"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name 2/param>
        /// <param name="_KeyName3">Key name 3</param>
        /// <returns>Node value</returns>
        public static string Node(string _KeyName, int _ItemIdx, string _KeyName2, string _KeyName3)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, _ItemIdx, _KeyName2, _KeyName3, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_KeyName, _ItemIdx, _KeyName2);
        /// Example:  
        ///     WebApiTest.Node<bool>("KeyName_12", 0, "KeyName_12_A3", "KeyName_12_A3_A2") == true
        ///     WebApiTest.Node<int>("KeyName_12", 1, "KeyName_12_B3", "KeyName_12_B3_B1") == 10
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name 2/param>
        /// <param name="_KeyName3">Key name 3</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(string _KeyName, int _ItemIdx, string _KeyName2, string _KeyName3)
        {
            return JSONRepository.Value<T>(_KeyName, _ItemIdx, _KeyName2, _KeyName3, WebApiTestManager.RequestResponse);
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, _KeyName3);
        /// Example:  
        ///     WebApiTest.Node("KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_11") == "KeyValue_12_AZ_11"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <param name="_ItemIdx2">Indexed item 2 in the list(0)</param>
        /// <param name="_KeyName3">Key name 3</param>
        /// <returns>Node value</returns>
        public static string Node(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, string _KeyName3)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, _KeyName3, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, _KeyName3);
        /// Example:  
        ///     WebApiTest.Node<bool>("KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_12") == true
        ///     WebApiTest.Node<int>("KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_13") == 1
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <param name="_ItemIdx2">Indexed item 2 in the list(0)</param>
        /// <param name="_KeyName3">Key name 3</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, string _KeyName3)
        {
            return JSONRepository.Value<T>(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, _KeyName3, WebApiTestManager.RequestResponse);
        }

        #endregion - DADAD - No List / First nesting - With item list / Second nesting - with item list / Third nesting - No item list


        #region - DADADI - No List / First nesting - With item list / Second nesting - with item list / Third nesting with array
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as No List / First nesting - With item list / Second nesting - with item list / Third nesting with array
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, _KeyName3, _ItemIdx3);
        /// Example:  
        ///     WebApiTest.Node("KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_13", 0) == "X"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <param name="_ItemIdx2">Indexed item 2 in the list(0)</param>
        /// <param name="_KeyName3">Key name 3</param>
        /// <param name="_ItemIdx3">Indexed item 3 in the list(0)</param>
        /// <returns>Node value</returns>
        public static string Node(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, string _KeyName3, int _ItemIdx3)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, _KeyName3, _ItemIdx3, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, _KeyName3);
        /// Example:  
        ///     WebApiTest.Node<bool>("KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_13", 1) == true
        ///     WebApiTest.Node<int>("KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_13", 2) == 1
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <param name="_ItemIdx2">Indexed item 2 in the list(0)</param>
        /// <param name="_KeyName3">Key name 3</param>
        /// <param name="_ItemIdx3">Indexed item 3 in the list(0)</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, string _KeyName3, int _ItemIdx3)
        {
            return JSONRepository.Value<T>(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, _KeyName3, _ItemIdx3, WebApiTestManager.RequestResponse);
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, _KeyName3, _ItemId3, _KeyName4);
        /// Example:  
        ///     WebApiTest.Node("KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_14", 0, "KeyName_12_AZ_14_1A") == "KeyName_12_AZ_14_1A_11"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <param name="_ItemIdx2">Indexed item 2 in the list(0)</param>
        /// <param name="_KeyName3">Key name 3</param>
        /// <param name="_ItemIdx3">Indexed item 3 in the list(0)</param>
        /// <param name="_KeyName4">Key name 4</param>/// 
        /// <returns>Node value</returns>
        public static string Node(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, string _KeyName3, int _ItemIdx3, string _KeyName4)
        {
            return Convert.ToString(JSONRepository.Value(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, _KeyName3, _ItemIdx3, _KeyName4, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, _KeyName3, _ItemId3, _KeyName4);
        /// Example:  
        ///     WebApiTest.Node<bool>("KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_14", 0, "KeyName_12_AZ_14_1B") == true
        ///     WebApiTest.Node<int>("KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_14", 0, ""KeyName_12_AZ_14_1C") == 1
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name 2</param>
        /// <param name="_ItemIdx2">Indexed item 2 in the list(0)</param>
        /// <param name="_KeyName3">Key name 3</param>
        /// <param name="_ItemIdx3">Indexed item 3 in the list(0)</param>
        /// <param name="_KeyName4">Key name 4</param>/// 
        /// <returns>Node value as T type</returns>
        public static T Node<T>(string _KeyName, int _ItemIdx, string _KeyName2, int _ItemIdx2, string _KeyName3, int _ItemIdx3, string _KeyName4)
        {
            return JSONRepository.Value<T>(_KeyName, _ItemIdx, _KeyName2, _ItemIdx2, _KeyName3, _ItemIdx3, _KeyName4, WebApiTestManager.RequestResponse);
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_ArrayIdx, _ItemIdx, _KeyName);
        /// Example:  
        ///     WebApiTest.Node(0, 0, "KeyName_1A1") == "KeyValue_1A1"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <returns>Node value</returns>
        public static string Node(int _ArrayIdx, int _ItemIdx, string _KeyName)
        {
            return Convert.ToString(JSONRepository.Value(_ArrayIdx, _ItemIdx, _KeyName, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_ArrayIdx, _ItemIdx, _KeyName);
        /// Example:  
        ///     WebApiTest.Node<bool>(0, 0, "KeyName_1A2") == true
        ///     WebApiTest.Node<int>(0, 0, "KeyName_1XZ") == 10
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(int _ArrayIdx, int _ItemIdx, string _KeyName)
        {
            return JSONRepository.Value<T>(_ArrayIdx, _ItemIdx, _KeyName, WebApiTestManager.RequestResponse);
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_ArrayIdx, _ItemIdx, _KeyName, _KeyName2);
        /// Example:  
        ///     WebApiTest.Node(0, 0, "KeyName_1A1", "KeyName_1A3_1") == "KeyValue_1A3_1"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <returns>Node value</returns>
        public static string Node(int _ArrayIdx, int _ItemIdx, string _KeyName, string _KeyName2)
        {
            return Convert.ToString(JSONRepository.Value(_ArrayIdx, _ItemIdx, _KeyName, _KeyName2, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_ArrayIdx, _ItemIdx, _KeyName, _KeyName2);
        /// Example:  
        ///     WebApiTest.Node<bool>(0, 0, "KeyName_1A3", "KeyName_2A3_2") == true
        ///     WebApiTest.Node<int>(0, 0, "KeyName_1A3", "KeyName_1A3_X") == 10
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_KeyName">Key name</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, string _KeyName2)
        {
            return JSONRepository.Value<T>(_ArrayIdx, _ItemIdx, _KeyName, _KeyName2, WebApiTestManager.RequestResponse);
        }

        #endregion - ADD - Big list / First nesting - No item list / Second nesting - No item list


        #region - ADI - Big list / First nesting with array 
        /// *******************************************************************************************************************************************************

        /// <summary>
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2);
        /// Example:  
        ///     WebApiTest.Node(0, 0, "KeyName_1A3", 0) == "X"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx2">Item index 2. Starts from 0</param>
        /// <returns>Node value</returns>
        public static string Node(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2)
        {
            return Convert.ToString(JSONRepository.Value(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2);
        /// Example:  
        ///     WebApiTest.Node<bool>(0, 0, "KeyName_1A3", 1) == true
        ///     WebApiTest.Node<int>(0, 0, "KeyName_1A3", 2) == 1
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx2">Item index 2. Starts from 0</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2)
        {
            return JSONRepository.Value<T>(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, WebApiTestManager.RequestResponse);
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx,2 _KeyName2);
        /// Example:  
        ///     WebApiTest.Node(0, 0, "KeyName_12", 0, "KeyName_12_A1") == "KeyValue_12_A1"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx2">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name in the list</param>
        /// <returns>Node value</returns>
        public static string Node(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2)
        {
            return Convert.ToString(JSONRepository.Value(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2);
        /// Example:  
        ///     WebApiTest.Node<bool>(0 ,0, "KeyName_12", 0, "KeyName_12_A2") == true
        ///     WebApiTest.Node<int>(0, 0, "KeyName_12", 1, "KeyName_12_BX") == 10
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx2">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name in the list</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2)
        {
            return JSONRepository.Value<T>(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2, WebApiTestManager.RequestResponse);
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2, _ItemIdx3);
        /// Example:  
        ///     WebApiTest.Node(0, 0, "KeyName_12", 0, "KeyName_12_A3", 0) == "X"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx2">Item index 2. Starts from 0</param>
        /// <param name="_KeyName2">Key name 2 in the list</param>
        /// <param name="_ItemIdx3">Item index 3. Starts from 0</param>
        /// <returns>Node value</returns>
        public static string Node(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, int _ItemIdx3)
        {
            return Convert.ToString(JSONRepository.Value(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2, _ItemIdx3, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2);
        /// Example:  
        ///     WebApiTest.Node<bool>(0 ,0, "KeyName_12", 0, "KeyName_12_A3", 1) == true
        ///     WebApiTest.Node<int>(0, 0, "KeyName_12", 1, "KeyName_12_A3", 2) == 1
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx2">Item index 2. Starts from 0</param>
        /// <param name="_KeyName2">Key name 2 in the list</param>
        /// <param name="_ItemIdx3">Item index 3. Starts from 0</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, int _ItemIdx3)
        {
            return JSONRepository.Value<T>(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2, _ItemIdx3, WebApiTestManager.RequestResponse);
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
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2, _ItemIdx3, _KeyName3);
        /// Example:  
        ///     WebApiTest.Node(0, 0, "KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_11") == "KeyValue_12_AZ_11"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx2">Indexed2 item in the list(0)</param>
        /// <param name="_KeyName2">Key name2 in the list</param>
        /// <param name="_ItemIdx3">Indexed3 item in the list(0)</param>
        /// <param name="_KeyName3">Key name3 in the list</param>
        /// <returns>Node value</returns>
        public static string Node(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, int _ItemIdx3, string _KeyName3)
        {
            return Convert.ToString(JSONRepository.Value(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2, _ItemIdx3, _KeyName3, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2,_ItemIdx3, _KeyName3);
        /// Example:  
        ///     WebApiTest.Node<bool>(0 ,0, "KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_12" ) == true
        ///     WebApiTest.Node<int>(0, 0, "KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_13") == 1
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx2">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name in the list</param>
        /// <param name="_ItemIdx3">Indexed3 item in the list(0)</param>
        /// <param name="_KeyName3">Key name3 in the list</param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, int _ItemIdx3, string _KeyName3)
        {
            return JSONRepository.Value<T>(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2, _ItemIdx3, _KeyName3, WebApiTestManager.RequestResponse);
        }

        #endregion - ADADAD - Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list


        #region - ADADADD - Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list / Fourth nesting - No item list
        /// *******************************************************************************************************************************************************

        /// <summary>
        /// Converts JSON part of Login response as below and returns the data as dictionary as Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list / Fourth nesting - No item list
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
        ///                      },
        ///                      {
        ///                      ...
        ///                      }
        ///                  ]
        ///             },
        ///             {
        ///             ...
        ///             }
        ///         ]
        ///         ...
        ///         "KeyName_1X": 1
        ///     ...}
        ///     ]
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, WebApiTest.ApiUri.Request, _RequestObject);
        ///     Where:
        ///         _HttpMethod is POST, PUT, GET, DELETE
        ///         _RequestObject is request object
        ///     ...
        ///     WebApiTest.Node(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2, _ItemIdx3, _KeyName3, _KeyName4);
        /// Example:  
        ///     WebApiTest.Node(0, 0, "KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_14", "KeyName_12_AZ_14_1") == "KeyValue_12_AZ_14_1"
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx2">Indexed2 item in the list(0)</param>
        /// <param name="_KeyName2">Key name2 in the list</param>
        /// <param name="_ItemIdx3">Indexed3 item in the list(0)</param>
        /// <param name="_KeyName3">Key name3 in the list</param>
        /// <param name="_KeyName4">Key name4 </param>
        /// <returns>Node value</returns>
        public static string Node(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, int _ItemIdx3, string _KeyName3, string _KeyName4)
        {
            return Convert.ToString(JSONRepository.Value(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2, _ItemIdx3, _KeyName3, _KeyName4, WebApiTestManager.RequestResponse));
        }

        /// <summary>
        /// As Template above, but using cast into T type
        /// Usage:
        ///     WebApiTest.Node<T>(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2,_ItemIdx3, _KeyName3);
        /// Example:  
        ///     WebApiTest.Node<bool>(0 ,0, "KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_14", "KeyName_12_AZ_14_2" ) == true
        ///     WebApiTest.Node<int>(0, 0, "KeyName_12", 0, "KeyName_12_AZ", 0, "KeyName_12_AZ_14", "KeyName_12_AZ_14_3" ) == 1
        /// Note: This have to stay here for beeing called from WebApi tests
        /// </summary>
        /// <param name="_ArrayIdx">List index. Starts from 0</param>
        /// <param name="_ItemIdx">Item index. Starts from 0</param>
        /// <param name="_KeyName">Key name</param>
        /// <param name="_ItemIdx2">Indexed item in the list(0)</param>
        /// <param name="_KeyName2">Key name in the list</param>
        /// <param name="_ItemIdx3">Indexed3 item in the list(0)</param>
        /// <param name="_KeyName3">Key name3 in the list</param>
        /// <param name="_KeyName4">Key name4 </param>
        /// <returns>Node value as T type</returns>
        public static T Node<T>(int _ArrayIdx, int _ItemIdx, string _KeyName, int _ItemIdx2, string _KeyName2, int _ItemIdx3, string _KeyName3, string _KeyName4)
        {
            return JSONRepository.Value<T>(_ArrayIdx, _ItemIdx, _KeyName, _ItemIdx2, _KeyName2, _ItemIdx3, _KeyName3, _KeyName4, WebApiTestManager.RequestResponse);
        }

        #endregion - ADADADD - Big list / First nesting - With item list / Second nesting - With item list / Third nesting - No item list / Fourth nesting - No item list


        #endregion Nodes


        #endregion Statuses/Nodes

        #region QA tools
        /// **************************************

        /// <summary>
        /// Generates automatically Asserts, where the invocation took place
        /// </summary>
        public static void QA_GenerateAsserts()
        {
            QA_AssertsGenerator.GenerateAsserts();
        }

        /// <summary>
        /// Generates random fixed data (ie: password) and return the value
        /// </summary>
        public static string QA_GenerateRandomFixedData() => QA_RandomDataGenerator.GenerateRandomFixedData();

        /// <summary>
        /// Encrypts string using AES 
        /// </summary>
        /// <param name="_Input">String to be encrypted</param>
        /// <returns>Encrypted string</returns>
        public static string QA_SSOEncryptParam(string _Input) => QA_SSOGenerator.AESEncrypt(_Input);

        /// <summary>
        /// Decrypts URL-encoded, AES-encrypted string
        /// </summary>
        /// <param name="_Input">String to be decrypted</param>
        /// <returns>Decrypted string</returns>
        public static string QA_SSODecryptParam(string _Input) => QA_SSOGenerator.AESDecrypt(_Input);

        #endregion QA tools
    }
}
