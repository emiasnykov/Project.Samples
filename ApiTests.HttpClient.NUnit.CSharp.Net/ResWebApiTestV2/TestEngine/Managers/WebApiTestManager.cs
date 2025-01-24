using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ResWebApiTest.TestEngine.Constants;
using ResWebApiTest.TestEngine.Factory;
using ResWebApiTest.TestEngine.Helpers;
using static ResWebApiTest.TestEngine.Constants.BasicEntity;
using static ResWebApiTest.TestEngine.Factory.WebApiUri;
using static ResWebApiTest.TestEngine.WebApiTest;

namespace ResWebApiTest.TestEngine.Managers
{
    /// <summary>
    /// WebApiTestManager - main reflection of the WebApiTestController (the singletone class).
    /// </summary>
    public class WebApiTestManager
    {
        // HttpClient
        // Static member objects to be used in whole testing Environment
        private static HttpClient httpClient;

        // Trace
        // Static member for stack trace
        private static StackTrace trace;

        #region Properties
        /// **************************************

        /// <summary>
        /// Static property HttpClient
        /// </summary>
        public static HttpClient HttpClient
        {
            get
            {
                if (httpClient == null)
                    httpClient = new HttpClient();
                return httpClient;
            }
            set
            {
                httpClient = value;
            }
        }

        /// <summary>
        /// Static property Trace
        /// </summary>
        public static StackTrace Trace
        {
            get
            {
                trace = new StackTrace(TraceNesting);
                return trace;
            }
            set
            {
                trace = value;
            }
        }

        // Json
        // Static member json serializer as a java script
        public static JavaScriptSerializer JsonSerializer { get; set; }

        // Response
        // Static member dictionary to keep response by Login
        public static Dictionary<string, object> LoginResponse { get; set; }
        // Static member dictionary to keep response by Request
        public static Dictionary<string, object> RequestResponse { get; set; }

        // Failed on
        // Static member to keep info if Login failed
        public static bool LoginFailed { get; set; }

        // Static member to keep info if Request failed
        public static bool RequestFailed { get; set; }

        // Token
        // Static member for keeping Bearer token
        public static string BearerToken { set; get; }

        // Static member for validation authorization
        public static bool BlockHttp { get; set; }

        // Tracing
        // Static member for trace nesting
        public static int TraceNesting { get; set; }

        // Static member to increase trace nesting
        public static bool TraceNestingIncrease { get; set; }

        // Counts
        // Static member for keeping Response count returned from Test's checked asserts
        public static int ResponseCount { get; set; }

        // Static member for keeping Expected count returned from JSON content 
        public static int ExpectedCount { get; set; }

        // Test names
        // Static member of testing class
        public static string TestClass { get; set; }

        // Static member of testing method
        public static string TestMethod { get; set; }

        // Asserts
        // Static member for last Assert if it were passed
        public static bool LastAssertPassed { get; set; }

        // Static member to ignore check count
        public static bool CheckAssertCount { get; set; }

        // Throw
        // Static member to allow throw exceptions on Login or Request. Used for check asserts on failed ones.
        public static bool ThrowEngineException { get; set; }

        #endregion Properties


        #region Initialization of test variables
        //**************************************

        /// <summary>
        /// Initialize engine variables
        /// </summary>
        /// <param name="_HttpMethod">As itself</param>
        /// <param name="_ThrowException">Flag to throw exceptions on login</param>
        public static void InitializeAll(HttpMethod _HttpMethod, ThrowAnyException _ThrowException)
        {
            ThrowEngineException = true;

            // Initialize assert vars
            CheckAssertCount = true;
            LastAssertPassed = false;

            // Initialize ResponseCount. Set to 0 as a default
            ResponseCount = 0;
            // Initialize ExpectedCount. Set to 0 as a default
            ExpectedCount = 0;

            // Initialize ThrowException
            if (_ThrowException == ThrowAnyException.No_AsItIsExpected)
                ThrowEngineException = false;

            // Initialize JsonSerializer
            if (JsonSerializer == null)
                JsonSerializer = new JavaScriptSerializer();

            // Set starting point trase nesting for callstack on initialization
            TraceNesting = FactoryParam.TracerInitTraceNesting; // DO NOT CHANGE 

            // Increase trace nesting on additional call to SendRequest with _JSONContent
            if (TraceNestingIncrease)
                TraceNesting++;

            // Initialize Logger is on/off
            if (!Logger.Initialized)
            {
                Logger.Init();
                StartLoging("Begin TEST", _HttpMethod);
            }
            else
            if (Logger.Initialized)
                StartLoging($"{NewLine}" +
                            $"[Test]" +
                            $"{NewLine}", _HttpMethod);
        }

        /// <summary>
        /// Start loging
        /// </summary>
        /// <param name="_Prefix">Prefix for login</param>
        /// <param name="_HttpMethod">As itself</param>
        private static void StartLoging(string _Prefix, HttpMethod _HttpMethod)
        {
            string msg = "";

            // Keep test names
            TestClass = Trace.GetFrame(0).GetMethod().DeclaringType.Name;
            TestMethod = Trace.GetFrame(0).GetMethod().Name;

            // Log for login
            if (ApiUriLogin != null)
            {
                // Create message
                msg = $"{_Prefix} [{TestClass}.{TestMethod}]";
                msg += TestsExceptions.Prettify(false, $"{NewLine}" +
                                                       $"[Login]" +
                                                       $"{NewLine}" +
                                                       $"{IndentLogShort}{_HttpMethod} :: {Convert.ToString(ApiUriLogin)}");
            }

            // Log for request
            if (ApiUriRequest != null)
            {
                // Create message
                msg = $"{_Prefix}{IndentLogShort}{TestClass}.{TestMethod}";
                msg += TestsExceptions.Prettify(false, $"{NewLine}" +
                                                       $"[Request]" +
                                                       $"{NewLine}" +
                                                       $"{IndentLogShort}{_HttpMethod} :: {Convert.ToString(ApiUriRequest)}");
            }

            // Start logger
            Logger.StartLog(msg);
        }

        #endregion Initialization of test variables


        #region Factory of WebApiTest (alphabetically)
        /// **************************************


        #region Counter
        /// **************************************

        /// <summary>
        /// Check count based on Expected JSON object count and Test's asserts count
        /// </summary>
        /// <param name="_CheckCount">Enumeration check count</param>
        /// <returns>true, if both counts are the same</returns>
        public static bool IsCountCorrect(CheckCount _CheckCount)
        {
            // Call counter helper to check if counts are correct
            return Counter.IsCountCorrect(_CheckCount);
        }

        /// <summary>
        /// Send info on failure count
        /// </summary>
        /// <returns></returns>
        public static string InfoCountOnFailure()
        {
            // Call counter helper to get info on failure
            return Counter.InfoCountOnFailure();
        }

        #endregion Counter


        #region Send request 
        /// **************************************

        /// <summary>
        /// Send request with [FromBody] JSON string
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, _ApiUri, _JSONContent, _ThrowException);
        ///     Where:
        ///         _HttpMethod     - this is for POST, PUT, GET, DELETE
        ///         _ApiUri         - the enumeration of authorization (for Login) or not (for Request)
        ///         _JSONContent    - requested string caming from the request needs to be more factorized, because of 
        ///                             coming from the code itself and needs to be parsed in a proper way
        ///         _ThrowException - this is the flag that informs us to thow exception is anything goes wwrong or keep it calm as it is requested
        /// </summary>
        /// <param name="_HttpMethod">As iselt</param>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_JSONContent">Request [FromBody] JSON content</param>
        /// <param name="_ThrowException">Exceptions will be thrown, eventhough the failure messages will be kept and saved to the log file</param>
        /// <returns>Response dictionary object</returns>
        public static Dictionary<string, object> SendRequest(HttpMethod _HttpMethod, 
                                                             ApiUri _ApiUri, 
                                                             string _JSONContent, 
                                                             ThrowAnyException _ThrowException)
        {
            object objAttached = null;

            TraceNestingIncrease = true;

            // Parse interpolated verbatim payload and parse it to working dictionary
            // Note: If JSON content is wrongly constructed, rise exception, eventhough there might be set ThrowExceptionOnLogin.No_AsItIsExpected
            if (!string.IsNullOrEmpty(_JSONContent))
            {
                // Parse interpolated payload as a verbatim string
                _JSONContent = JSONParser.ParseInterpolatedVerbatimPayload(_JSONContent);

                // Parse the JSON object to the proper dictionary
                objAttached = JSONParser.ParseJSONToDictionaryObject(_ApiUri, _JSONContent);
            } else
            {
                // TODO
            }

            // Pass parsed JSON object
            Dictionary<string, object> objResponse = SendRequest(_HttpMethod, _ApiUri, objAttached, _ThrowException);

            return objResponse;
        }

        /// <summary>
        /// Send request with [FromBody] JSON string
        /// Usage:
        ///     WebApiTest.SendRequest(_HttpMethod, _ApiUri, _JSONContent, _ThrowException);
        ///     Where:
        ///         _HttpMethod     - this is for POST, PUT, GET, DELETE
        ///         _ApiUri         - the enumeration of authorization (for Login) or not (for Request)
        ///         _ObjAttached    - requested object that is caming from the request as itself (no refactor is needed) or non of possibilities
        ///         _ThrowException - this is the flag that informs us to thow exception is anything goes wwrong or keep it calm as it is requested
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
            Dictionary<string, object> objResponse = null;

            // Initialization all variables on send request
            InitializeAll(_HttpMethod, _ThrowException);

            // Tasks
            switch (_HttpMethod.Method)
            {
                // Do POST
                case "POST":
                    var post = Task.Run(() => RequestTask.PostAsync(_ApiUri, _ObjAttached));
                    post.Wait();
                    objResponse = post.Result;
                    break;

                // Do GET
                case "GET":
                    var get = Task.Run(() => RequestTask.GetAsync(_ApiUri));
                    get.Wait();
                    objResponse = get.Result;
                    break;

                // Do PUT
                case "PUT":
                    var put = Task.Run(() => RequestTask.PutAsync(_ApiUri, _ObjAttached));
                    put.Wait();
                    objResponse = put.Result;
                    break;

                // Do DELETE
                case "DELETE":
                    var delete = Task.Run(() => RequestTask.DeleteAsync(_ApiUri));
                    delete.Wait();
                    objResponse = delete.Result;
                    break;

                // Do the default action
                default:
                    break;
            }

            // Clear api uri for login and request
            ApiUriLogin = null;
            ApiUriRequest = null;

            // Clear TraceNestingIncrease flag if was On
            if (TraceNestingIncrease)
                TraceNestingIncrease = false;

            return objResponse;
        }

        #endregion Send request


        #endregion Factory of WebApiTest (alphabetically)


        #region Tear down test variables
        /// **************************************

        /// <summary>
        /// Vitality of tearing down all variables
        /// </summary>
        public static void TeardownAll()
        {
            // On Count check its value and log
            bool countCorrect = Counter.IsCountCorrect();

            // End logger and clear it
            if (!countCorrect || HasAnyoneFailed() || LastAssertPassed)           
                Logger.TestFailed(LastAssertPassed);

            // Log info
            Logger.EndLog($"End TEST [{TestClass}.{TestMethod}]");

            // Clear LogManager initialization flag
            Logger.Initialized = false;

            // Clear increasing test nesting
            TraceNestingIncrease = false;

            // Clear ApiUri for login
            ApiUriLogin = null;

            // Clear ApiUri for any request
            ApiUriRequest = null;

            // Clear login failed
            LoginFailed = false;

            // Clear request failed
            RequestFailed = false;

            // Clear any beare token
            BearerToken = null;

            // Clear any login 
            LoginResponse = null;

            // Clear any response
            RequestResponse = null;

            // Clear expected count
            ExpectedCount = 0;

            // Clear ResponseCount
            ResponseCount = 0;

            // Clear assert vars
            CheckAssertCount = false;
            LastAssertPassed = false;

            // Clear blocking http
            BlockHttp = false;

            // Clear HttpClient 
            HttpClient = null;
        }

        #endregion Tear down test variables
    }
}
