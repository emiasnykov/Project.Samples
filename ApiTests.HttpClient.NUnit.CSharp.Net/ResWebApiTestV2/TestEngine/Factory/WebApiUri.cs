using System;
using System.Collections.Generic;
using ResWebApiTest.TestEngine.Helpers;
using ResWebApiTest.TestEngine.Managers;
using static ResWebApiTest.TestEngine.WebApiTest;

namespace ResWebApiTest.TestEngine.Factory
{
    /// <summary>
    /// WebApi uri static methods
    /// </summary>
    public class WebApiUri
    {
        #region Public methods
        /// **************************************

        /// <summary>
        /// Get full Api Uri path
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <returns>Full Uri</returns>
        public static Uri GetFullPath(ApiUri _ApiUri)
        {
            return new Uri(GetBaseOne(_ApiUri));
        }

        /// <summary>
        /// Set response for login/request
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_ObjResponse">Response object</param>
        public static void SetPathResponse(ApiUri _ApiUri, Dictionary<string, object> _ObjResponse)
        {
            // Set proper response
            switch (_ApiUri)
            {
                // Get login response
                case ApiUri.Login:
                    WebApiTestManager.LoginResponse = _ObjResponse;
                    break;

                // Get request response
                case ApiUri.Request:
                    WebApiTestManager.RequestResponse = _ObjResponse;
                    break;

                // Do the default action
                default:
                    break;
            }
        }

        /// <summary>
        /// Get response for login/request
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        public static Dictionary<string, object> GetPathResponse(ApiUri _ApiUri)
        {
            Dictionary<string, object> objResponse = null;
             
            // Get proper response
            switch (_ApiUri)
            {
                // Get login response
                case ApiUri.Login:
                    objResponse = WebApiTestManager.LoginResponse;
                    break;

                // Get request response
                case ApiUri.Request:
                    objResponse = WebApiTestManager.RequestResponse;
                    break;

                // Do the default action
                default:
                    break;
            }

            return objResponse;
        }

        /// <summary>
        /// Mark on what has failed on ApiUri
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        public static void MarkFailingPath(ApiUri _ApiUri)
        {
            // Mark stright away what is failed
            switch (_ApiUri)
            {
                // Set login flag failed
                case ApiUri.Login:
                    WebApiTestManager.LoginFailed = true;
                    break;

                // Set request flag failed
                case ApiUri.Request:
                    WebApiTestManager.RequestFailed = true;
                    break;

                // Do the default action
                default:                    
                    break;
            }
        }

        /// <summary>
        /// Check if has anything failed on login or request
        /// </summary>
        /// <returns>Check by bool operator if anything went wrong</returns>
        public static bool HasAnyoneFailed() => WebApiTestManager.LoginFailed ^ WebApiTestManager.RequestFailed;

        /// <summary>
        /// Check to failed on what
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <returns>Set on what possibly failed</returns>
        public static string FailedOn(ApiUri _ApiUri) => Enum.GetName(typeof(ApiUri), _ApiUri);

        #endregion public methods

        #region Private methods
        /// **************************************

        // Full Api URL. Concatenated with const base API URI
        private static string GetBaseOne(ApiUri _ApiUri)
        {
            string uri = "";

            // Set full path for ApiUri on Login/Request
            switch (_ApiUri)
            {
                // Get uri from login
                case ApiUri.Login:
                    uri = ApiUriLogin;
                    break;

                // Get uri from request
                case ApiUri.Request:
                    uri = ApiUriRequest;
                    break;

                // Do the default action
                default:
                    break;
            }

            return Configurer.GetWebApiUrl() + uri;
        }

        #endregion Private methods
    }
}
