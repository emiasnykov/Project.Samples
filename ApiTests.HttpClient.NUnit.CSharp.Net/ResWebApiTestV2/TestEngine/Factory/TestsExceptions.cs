using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ResWebApiTest.TestEngine.Constants;
using ResWebApiTest.TestEngine.Extensions;
using ResWebApiTest.TestEngine.Helpers;
using ResWebApiTest.TestEngine.Managers;
using ResWebApiTest.TestEngine.QA_Enums;
using ResWebApiTest.TestEngine.QA_InternalTools;
using static ResWebApiTest.TestEngine.Constants.BasicEntity;
using static ResWebApiTest.TestEngine.WebApiTest;

namespace ResWebApiTest.TestEngine.Factory
{
    public class TestsExceptions
    {
        /// <summary>
        /// Exception header
        /// </summary>
        public static readonly string ExceptionHeader = $"{NewLine}" +
                                                        $"{NewLine}*****************************************************" +
                                                        $"{NewLine}    WebApiTestController throws Exception !" +
                                                        $"{NewLine}*****************************************************";

        /// <summary>
        /// Exception footer
        /// </summary>
        public static readonly string ExceptionFooter = $"{NewLine}" +
                                                        $"{NewLine}IMPORTANT NOTE:" +
                                                        $"{NewLine}    Make sure all your data in Database are correct," +
                                                        $"{NewLine}    before running the Test again !" +
                                                        $"{NewLine}" +
                                                        $"{NewLine}*****************************************************";

        #region Exceptions
        /// **************************************

        /// <summary>
        /// Prettify text
        /// </summary>
        /// <param name="_ForExc">if true, prettify for Exception, else for Log</param>
        /// <param name="_MsgExc">Message exception</param>
        /// <returns>Prettified text</returns>
        public static string Prettify(bool _ForExc, string _MsgExc)
        {
            string indent = IndentLogShort;
            string indent2 = IndentLogLong;
            if (_ForExc)
            {
                indent = IndentEmpty;
                indent2 = indent;
            }
            else
                indent = IndentLogShort;
            return _MsgExc.Replace("_INDENT_", indent).Replace("_LONGINDENT_", indent2);
        }

        /// <summary>
        /// Build exception message much more readable for testers. General.
        /// </summary>
        /// <param name="_FailedOn">On what failed</param>
        /// <param name="_ExceptionOn">On what is exception</param>
        /// <param name="_ExceptionMessage">Exception message</param>
        /// <param name="_FaqInfo1">Faq information 1</param>
        /// <param name="_FaqInfo2">Faq information 2</param>
        /// <returns>Exception message</returns>
        public static string BuildExceptionMessage(string _FailedOn, string _ExceptionOn, string _ExceptionMessage, string _FaqInfo1, string _FaqInfo2 = "")
        {
            var faq2 = (String.IsNullOrEmpty(_FaqInfo2)) ? _FaqInfo2 : $"{NewLine}_INDENT_        {_FaqInfo2}.";
            return $"{NewLine}_INDENT_TEST FAILED ON: {_FailedOn}" +
                   $"{NewLine}_INDENT_" +
                   $"{NewLine}_INDENT_    EXCEPTION ON: {_ExceptionOn}" +
                   $"{NewLine}_INDENT_        {_ExceptionMessage}." +
                   $"{NewLine}_INDENT_" +
                   $"{NewLine}_INDENT_    FAQ:" +
                   $"{NewLine}_INDENT_        {_FaqInfo1}." + faq2;
        }

        /// <summary>
        /// Build iterated exception message much more readable for developers. General.
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_ExceptionOn">On what is exception</param>
        /// <param name="_FaqInfo1">Faq information 1</param>
        /// <param name="_FaqInfo2">Faq information 2</param>
        /// <returns>Exception message</returns>
        public static string BuildIteratedExceptionMessage(ApiUri _ApiUri, string _ExceptionOn, string _FaqInfo1, string _FaqInfo2 = "")
        {
            var faq2 = (String.IsNullOrEmpty(_FaqInfo2)) ? _FaqInfo2 : $"{NewLine}_INDENT_        {_FaqInfo2}.";
            return $"{NewLine}_INDENT_TEST FAILED ON: {WebApiUri.FailedOn(_ApiUri)}" +
                   $"{NewLine}_INDENT_" +
                   $"{NewLine}_INDENT_    EXCEPTION ON: {_ExceptionOn}" +
                   $"{NewLine}_INDENT_    (Below is full information about the Exception)" +
                   $"{BuilIterateResponseJSON(_ApiUri)}" +
                   $"{NewLine}_INDENT_" +
                   $"{NewLine}_INDENT_    FAQ:" +
                   $"{NewLine}_INDENT_        {_FaqInfo1}." + faq2;
        }

        /// <summary>
        /// Build exception message based in iteraration of response JSON
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <returns>Exception message</returns>
        public static string BuilIterateResponseJSON(ApiUri _ApiUri)
        {
            string res = "";
            List<string> elLst = null;
            IList<string> errLst = QA_AssertsGenerator.IterateJSONObject(_ApiUri, JSONParser.SchemaJSON);

            //Serves most common D,DAD. This may be enhanced 
            foreach (string err in errLst)
            {
                elLst = new List<string>();
                elLst = err.Split('|').ToList();
                res += $"{NewLine}_INDENT_" +
                       $"{NewLine}_INDENT_        {((elLst[0].ToString().Equals("0")) ? "Result" : elLst[0])}:" +
                       $"{NewLine}_INDENT_            {elLst[1]}";
            }
            return res;
        }

        /// <summary>
        /// Throw exception and write out to Log
        /// </summary>
        /// <param name="_MsgExc">Message exception</param>
        public static void ThrowException(string _MsgExc)
        {
            // Throw if no empty
            if (String.IsNullOrEmpty(_MsgExc))
                return;

            // Don't Prettify message when it goes to the log
            var msgLog = Prettify(false, _MsgExc);

            // Log message
            Logger.Error(msgLog);

            // Throw it if allowed
            if (WebApiTestManager.ThrowEngineException)
            {
                // Prettify message on throw
                string msgExc = Prettify(true, _MsgExc);

                // Throw exception
                throw new WebApiTestException($"{ExceptionHeader}{NewLine}{msgExc}{ExceptionFooter}{NewLine}{NewLine}");
            }
        }

        /// <summary>
        /// Throw exception on failure StatusCode or Exception
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_QA_ServeExceptionMode">Known exception to be served</param>
        /// <param name="_HttpStatusCode">StatusCode</param>
        /// <param name="_Ex">Exception message</param>
        public static void ThrowExceptionOnFailure(ApiUri _ApiUri, QA_ServeExceptionMode _QA_ServeExceptionMode, HttpStatusCode _HttpStatusCode = HttpStatusCode.Unused, Exception _Ex = null)
        {
            string msgExc = null;

            // Check exceptions by ApiUri
            switch (_ApiUri)
            {
                case ApiUri.Login:      // Check Login response and failure flag
                    if (WebApiTestManager.LoginFailed)
                    {
                        if (WebApiTestManager.LoginResponse != null)
                            if (!WebApiTestManager.LoginResponse.ContainsKey(FactoryParam.RequestTaskStatusCode))
                                msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri), FactoryParam.RequestTaskStatusCode, "The StatusCode doesn't exist on Request", "Check if the Database connection is still On");
                            else
                            {
                                try
                                {
                                    // Get known failure StatusCode from Login and build message for its known code
                                    var statusCodeFromLogin = (ReadableForQAFailureStatusCode)Enum.Parse(typeof(ReadableForQAFailureStatusCode), Convert.ToString(JSONRepository.Value(FactoryParam.RequestTaskStatusCode, WebApiTestManager.LoginResponse)));
                                    msgExc = BuildMessageException(_ApiUri, statusCodeFromLogin, _QA_ServeExceptionMode);
                                }
                                catch
                                {
                                    // Cast unknow StatusCode and build message for any exception
                                    ReadableForQAFailureStatusCode readableForQAFailureStatusCode = (ReadableForQAFailureStatusCode)_HttpStatusCode;
                                    msgExc = BuildMessageException(_ApiUri, readableForQAFailureStatusCode, QA_ServeExceptionMode.AnyOtherUnknown);
                                }
                            }
                        else
                        {
                            if ((_QA_ServeExceptionMode == QA_ServeExceptionMode.OnAttachedJSONParse) || (_QA_ServeExceptionMode == QA_ServeExceptionMode.OnUnknownSchemaJSONParsingTemplate))
                                msgExc = BuildMessageException(_ApiUri, ReadableForQAFailureStatusCode.None_JustSkipOnDemand, _QA_ServeExceptionMode);
                            else
                            {
                                // Cast unknow StatusCode and build message for any exception
                                ReadableForQAFailureStatusCode readableForQAFailureStatusCode = (ReadableForQAFailureStatusCode)_HttpStatusCode;
                                msgExc = BuildMessageException(_ApiUri, readableForQAFailureStatusCode, _QA_ServeExceptionMode, _Ex);
                            }
                        }
                    }
                    else
                    {
                        // Cast unknow StatusCode and build message for any exception
                        ReadableForQAFailureStatusCode readableForQAFailureStatusCode = (ReadableForQAFailureStatusCode)_HttpStatusCode;
                        msgExc = BuildMessageException(_ApiUri, readableForQAFailureStatusCode, _QA_ServeExceptionMode, _Ex);
                    }
                    break;
                case ApiUri.Request:    //Check Request response and failure flag
                    // Build message in no failed flag set or serve exception is not defined 
                    if ((!WebApiTestManager.RequestFailed) ||
                        ((WebApiTestManager.RequestResponse == null) &&
                            ((_QA_ServeExceptionMode != QA_ServeExceptionMode.OnAuthorization) || (_QA_ServeExceptionMode != QA_ServeExceptionMode.OnAttachedJSONParse) || (_QA_ServeExceptionMode != QA_ServeExceptionMode.OnUnknownSchemaJSONParsingTemplate))))
                        msgExc = BuildMessageExceptionForUnknownStatusCodeOrUnknownQA_ServeExceptionMode(_ApiUri, _QA_ServeExceptionMode, _HttpStatusCode, _Ex);
                    else
                    //Build message for null response
                    if (WebApiTestManager.RequestResponse == null)
                        msgExc = BuildMessageException(_ApiUri, ReadableForQAFailureStatusCode.None_JustSkipOnDemand, _QA_ServeExceptionMode, _Ex);
                    else
                    // Build message if response doesn't contain StatusCode
                    if (!WebApiTestManager.RequestResponse.ContainsKey(FactoryParam.RequestTaskStatusCode))
                        msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri), FactoryParam.RequestTaskStatusCode, "The StatusCode doesn't exist on Request", "Check if the Database connection is still On");
                    else
                        // Build message for known/unknown StatusCode
                        try
                        {
                            // Get known failure StatusCode from Request and build message for its known code
                            var statusCodeR = (ReadableForQAFailureStatusCode)Enum.Parse(typeof(ReadableForQAFailureStatusCode), Convert.ToString(JSONRepository.Value(FactoryParam.RequestTaskStatusCode, WebApiTestManager.RequestResponse)));
                            msgExc = BuildMessageException(_ApiUri, statusCodeR, _QA_ServeExceptionMode);
                        }
                        catch
                        {
                            // Cast unknow StatusCode and build message for any exception
                            msgExc = BuildMessageExceptionForUnknownStatusCodeOrUnknownQA_ServeExceptionMode(_ApiUri, QA_ServeExceptionMode.AnyOtherUnknown, _HttpStatusCode, _Ex);
                        }
                    break;
                default:
                    // Do the default action
                    break;
            }

            // Mark flag if still points to not failed
            WebApiUri.MarkFailingPath(_ApiUri);

            // Throw if no empty
            if (!(String.IsNullOrEmpty(msgExc)))
                ThrowException(msgExc);
        }

        /// <summary>
        /// Build message exception for unknown StatusCode or unknown QA_ServeExceptionMode
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_QA_ServeExceptionMode">Known exception to be served</param>
        /// <param name="_HttpStatusCode">StatusCode</param>
        /// <param name="_Ex">Exception message</param>
        /// <returns>Built exception message</returns>
        public static string BuildMessageExceptionForUnknownStatusCodeOrUnknownQA_ServeExceptionMode(ApiUri _ApiUri, QA_ServeExceptionMode _QA_ServeExceptionMode, HttpStatusCode _HttpStatusCode = HttpStatusCode.Unused, Exception _Ex = null)
        {
            // Cast unknow StatusCode and build message for any exception
            ReadableForQAFailureStatusCode readableForQAFailureStatusCode = (ReadableForQAFailureStatusCode)_HttpStatusCode;
            return BuildMessageException(_ApiUri, readableForQAFailureStatusCode, _QA_ServeExceptionMode, _Ex);
        }

        /// <summary>
        /// Throws an Exception on Value failure
        /// </summary>
        /// <param name="_KeyName">Key name used in Value methods</param>
        /// <param name="_KeyExists">true, If key exists in the Value method</param>
        /// <param name="_ValueObj">Object to be converted to the List. If the List is empty, raise the exception</param>
        /// <param name="_ItemExists">true, if the whole item list, described by any Key form Value method, exists</param>
        public static void ThrowExceptionOnValueFailure(string _KeyName, bool _KeyExists, object _ValueObj, bool _ItemExists = true)
        {
            string msgExc = null;

            // Build exception message
            if (String.IsNullOrEmpty(_KeyName))
                msgExc = BuildExceptionMessage("Response Value",
                                                "Key itself",
                                                "The Key is Null or Empty",
                                                "Check test Nodes, if non-of them has empty or null Key name");
            else
            if ((!_KeyExists) && !_KeyName.Equals("0"))
                msgExc = BuildExceptionMessage("Response Value",
                                                $"Key Name = '{Convert.ToString(_KeyName)}'",
                                                $"This Key Name doesn't exist in the Response JSON object",
                                                "Check test Nodes, the Key name may have a typo");
            else
            if ((_KeyExists) && (!_ItemExists))
                msgExc = BuildExceptionMessage("Response Value",
                                                "Item",
                                                $"The whole Item object doesn't exist in the Response JSON object, for the Key '{Convert.ToString(_KeyName)}'",
                                                "The Node itself is correct, unfortunatelly the item you are looking for doesn't exist at all",
                                                "Try to change the request, to get response with more data including this item");
            else
            if (!String.IsNullOrEmpty(_KeyName) && (ConvertObjectExt.ToList<object>(_ValueObj, true).Count == 0))
            {
                string msgList = (_KeyName.Equals("0")) ? "the 'Big List'" : $"the Key '{Convert.ToString(_KeyName)}'";
                msgExc = BuildExceptionMessage("Response Value",
                                                "Node usage",
                                                $"No items found under { msgList} in the Response JSON object.",
                                                "The list is empty while you are calling a Node",
                                                "Try to change the request to get proper data and/or add missing Database data");
            }

            // Throw if no empty
            if (!(String.IsNullOrEmpty(msgExc)))
            {
                ThrowException(msgExc);
            }
        }

        /// Internal class of exception
        internal class WebApiTestException : Exception
        {
            public WebApiTestException()
            {
            }

            public WebApiTestException(string message) : base(message)
            {
            }

            public WebApiTestException(string message, Exception inner) : base(message, inner)
            {
            }
        }

        // Enumaration for FailureStatusCode for readable exception for QA
        private enum ReadableForQAFailureStatusCode
        {
            None_JustSkipOnDemand,
            MethodNotAllowed,
            WrongRequestArguments,
            BadRequest,
            InternalServerError,
            ServiceUnavailable,
            NotFound
        }

        /// <summary>
        /// Build message more readable exceptions for QA
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_StatusCode">StatusCode</param>
        /// <param name="_ReadableForQA">Flag to mark more readable exception for QA</param>
        /// <param name="_Ex">Exception message</param>
        /// <returns>Built exception message</returns>
        private static string BuildMessageException(ApiUri _ApiUri, ReadableForQAFailureStatusCode _StatusCode, QA_ServeExceptionMode _ReadableForQA = QA_ServeExceptionMode.OnStatusCode, Exception _Ex = null)
        {
            string msgExc = "";

            // Readable on not
            switch (_ReadableForQA)
            {
                case QA_ServeExceptionMode.OnStatusCode:
                    switch (_StatusCode)
                    {
                        case ReadableForQAFailureStatusCode.MethodNotAllowed:
                            msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri),
                                                           $"{FactoryParam.RequestTaskStatusCode} = {_StatusCode.ToString()}",
                                                           "Error code is 'UnsupportedApiVersion'",
                                                           $"Your '{WebApiUri.FailedOn(_ApiUri)}ApiUri' may contain invalid Api method to be called",
                                                           "Check above and/or your HttpMethod (in your request) if matches the Api request as well");
                            break;
                        case ReadableForQAFailureStatusCode.WrongRequestArguments:
                            msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri),
                                                           $"{FactoryParam.RequestTaskStatusCode} = {_StatusCode.ToString()}",
                                                           "The request's argument doesn't exist",
                                                           "Check your WebApiTest.ApiUriRequest, maybe there is a typo in request's argument");
                            break;
                        case ReadableForQAFailureStatusCode.BadRequest:
                            msgExc = BuildIteratedExceptionMessage(_ApiUri,
                                                                   $"{FactoryParam.RequestTaskStatusCode} = {_StatusCode.ToString()}",
                                                                   "Try to change the request to get proper data and/or add missing Database data");
                            break;
                        case ReadableForQAFailureStatusCode.InternalServerError:
                            msgExc = BuildIteratedExceptionMessage(_ApiUri,
                                                                   $"{FactoryParam.RequestTaskStatusCode} = {_StatusCode.ToString()}",
                                                                   "Check if the database connection is On",
                                                                   "Also, some of our failure Api responses return 'InternalServerError' as the database data didn't match");
                            break;
                        case ReadableForQAFailureStatusCode.NotFound:
                            if ((_Ex != null) && (_Ex.Message.Contains("URI")))  // Serve by Exception
                            {
                                int idx = _Ex.Message.IndexOf("URI") + 7;
                                int length = _Ex.Message.Length - idx;
                                string msg = _Ex.Message.Substring(idx, length);
                                msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri),
                                                               $"{FactoryParam.RequestTaskStatusCode} = {_StatusCode.ToString()}",
                                                               msg,
                                                               "Check your URI", "The API method may have a typo, or the argument list");
                            }
                            else
                            if (WebApiUri.GetPathResponse(_ApiUri) != null)  // Serve by Response
                            {
                                msgExc = BuildIteratedExceptionMessage(_ApiUri,
                                                                       $"{FactoryParam.RequestTaskStatusCode} = {_StatusCode.ToString()}",
                                                                       "The request method couldn't be found due to non-existsing method request, or argument list typo",
                                                                       "Check the request URI if it doesn't contain any incorrect entries/typos");
                            }
                            else // Not IIS on
                                msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri),
                                                                        $"{FactoryParam.RequestTaskStatusCode} = {_StatusCode.ToString()}",
                                                                        Convert.ToString(_Ex.Message),
                                                                        "Check if the IIS service is On");
                            break;
                        default:
                            // Do the default action
                            break;
                    }
                    break;
                case QA_ServeExceptionMode.OnNullSendRequest:
                    if (_Ex != null)
                        msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri),
                                                       "Send request",
                                                       $"{Convert.ToString(_Ex.Message)}",
                                                       "Start IIS with your WebApi");
                    else
                        msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri),
                                                       "Send request", 
                                                       "Something went wrong",
                                                       "Start IIS with your WebApi");
                    break;
                case QA_ServeExceptionMode.OnNullGetRequest:
                    if (_Ex != null)
                        msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri),
                                                       "Get request",
                                                       $"{Convert.ToString(_Ex.Message)}",
                                                       "Start IIS with your WebApi");
                    else
                        msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri),
                                                       "Get request", 
                                                       "Something went wrong",
                                                       "Start IIS with your WebApi");
                    break;
                case QA_ServeExceptionMode.OnAuthorization:
                    msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri),
                                                    "Authorization Token",
                                                    "Missing Bearer token",
                                                    "Token has expired",
                                                    "Check if the Database connection is still On");
                    break;
                case QA_ServeExceptionMode.OnAttachedJSONParse:
                    msgExc = BuildExceptionMessage("Attached JSON object",
                                                   "JSON content",
                                                   "JSON object didn't pass validation",
                                                   "Check your JSON object again", "It is not constructed correctly");
                    break;
                case QA_ServeExceptionMode.OnUnknownSchemaJSONParsingTemplate:
                    if (_Ex != null)
                        msgExc = BuildExceptionMessage("WebApiTest's Engine JSON parser failed on parsing",
                                                   $"{Convert.ToString(_Ex.Message)}",
                                                   "New Status/Node has come out, which is not defined and implemented",
                                                   "Inform CRS WEB QA (GDA) and send logs to add new Status/Node in WebApiTest Engine");
                    else
                        msgExc = BuildExceptionMessage("WebApiTest's Engine JSON parser failed on parsing",
                                                   "Something went wrong",
                                                   "New Status/Node has come out, which is not defined and implemented",
                                                   "Inform CRS WEB QA (GDA) and send logs to add new Status/Node in WebApiTest Engine");
                    break;
                case QA_ServeExceptionMode.AnyOtherUnknown:
                    if (_Ex != null)
                        msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri),
                                                   $"{FactoryParam.RequestTaskStatusCode} = {_StatusCode.ToString()}",
                                                   $"{Convert.ToString(_Ex.Message)}",
                                                   "Inform CRS WEB QA (GDA) and send logs to serve the Exception/StatusCode in WebApiTest Engine");
                    else
                        msgExc = BuildExceptionMessage(WebApiUri.FailedOn(_ApiUri),
                                                   $"{FactoryParam.RequestTaskStatusCode} = {_StatusCode.ToString()}",
                                                   "Something went wrong",
                                                   "Inform CRS WEB QA (GDA) and send logs to serve the Exception/StatusCode in WebApiTest Engine");
                    break;
                default:
                    // Do the default action
                    break;
            }
            return msgExc;
        }

        #endregion Exceptions
    }
}
