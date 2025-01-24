using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ResWebApiTest.TestEngine.Constants;
using ResWebApiTest.TestEngine.Managers;
using ResWebApiTest.TestEngine.QA_Enums;
using static ResWebApiTest.TestEngine.Factory.WebApiUri;
using static ResWebApiTest.TestEngine.Factory.Token;
using static ResWebApiTest.TestEngine.Managers.WebApiTestManager;
using static ResWebApiTest.TestEngine.Factory.TestsExceptions;
using static ResWebApiTest.TestEngine.WebApiTest;

namespace ResWebApiTest.TestEngine.Factory
{
    /// <summary>
    /// Request tasks methods
    /// </summary>
    public class RequestTask
    {
        #region Public methods
        /// **************************************

        /// <summary>
        /// POST Request
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_ObjAttached">Request [FromBody] object</param>
        /// <returns>Response dictionary object</returns>
        public static async Task<Dictionary<string, object>> PostAsync(ApiUri _ApiUri, object _ObjAttached)
        {
            HttpResponseMessage x = null;

            // Prepare POST request content
            HttpContent httpContent = new StringContent(JSONParser.ParseDictionaryObjectToJSON(_ObjAttached), Encoding.UTF8, EnvironmentParam.RequestTaskApplicationJson);
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = GetFullPath(_ApiUri),
                Content = httpContent
            };

            // Do Send. 
            // This is voulnerable case as it may be the first to be used. We need to make sure IIS works fine. 
            try
            {
                x = await WebApiTestManager.HttpClient.SendAsync(request);

            }
            catch (Exception ex)
            {
                // Throw exception as ServiceUnavailable
                TestsExceptions.ThrowExceptionOnFailure(_ApiUri, QA_ServeExceptionMode.OnNullSendRequest, HttpStatusCode.ServiceUnavailable, ex);
            }

            // TODO refactor non positive HttpStatus
            // if (!x.IsSuccessStatusCode)
            //    throw new Exception($"Incorrect status code => {x.Headers}:{x.Content}");

            //Get validated response
            return await GetValidatedResponse(_ApiUri, x);
        }

        /// <summary>
        /// GET Request
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <returns>Response dictionary object</returns>
        public static async Task<Dictionary<string, object>> GetAsync(ApiUri _ApiUri)
        {
            HttpResponseMessage x = null;

            // Do Get. 
            // This is voulnerable case as it may be the first to be used. We need to make sure IIS works fine. 
            try
            {
                x = await WebApiTestManager.HttpClient.GetAsync(GetFullPath(_ApiUri));
            }
            catch (Exception ex)
            {
                // Throw exception as ServiceUnavailable
                TestsExceptions.ThrowExceptionOnFailure(_ApiUri, QA_ServeExceptionMode.OnNullGetRequest, HttpStatusCode.ServiceUnavailable, ex);
            }

            // TODO refactor non positive HttpStatus
            // if (!x.IsSuccessStatusCode)
            //    throw new Exception($"Incorrect status code => {x.Headers}:{x.Content}");

            //Get validated response
            return await GetValidatedResponse(_ApiUri, x);
        }

        /// <summary>
        /// PUT Request
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <param name="_ObjAttached">Request [FromBody] object</param>
        /// <returns>Response dictionary object</returns>
        public static async Task<Dictionary<string, object>> PutAsync(ApiUri _ApiUri, Object _ObjAttached)
        {
            HttpResponseMessage x = null;

            // Prepare PUT content
            HttpContent httpContent = new StringContent(JSONParser.ParseDictionaryObjectToJSON(_ObjAttached), Encoding.UTF8, EnvironmentParam.RequestTaskApplicationJson);

            // Do Put. 
            // This is voulnerable case as it may be the first to be used. We need to make sure IIS works fine. 
            try
            {
                x = await WebApiTestManager.HttpClient.PutAsync(GetFullPath(_ApiUri), httpContent);
            }
            catch (Exception ex)
            {
                // Throw exception as ServiceUnavailable
                ThrowExceptionOnFailure(_ApiUri, QA_ServeExceptionMode.AnyOtherUnknown, HttpStatusCode.ServiceUnavailable, ex);
            }

            // TODO refactor non positive HttpStatus
            // if (!x.IsSuccessStatusCode)
            //    throw new Exception($"Incorrect status code => {x.Headers}:{x.Content}");

            // Get validated response
            return await GetValidatedResponse(_ApiUri, x);
        }

        /// <summary>
        /// DELETE Request
        /// </summary>
        /// <param name="_ApiUri">Enumaration for Api Uri</param>
        /// <returns>Response dictionary object</returns>
        public static async Task<Dictionary<string, object>> DeleteAsync(ApiUri _ApiUri)
        {
            HttpResponseMessage x = null;

            // Do Delete. 
            // This is voulnerable case as it may be the first to be used. We need to make sure IIS works fine. 
            try
            {
                x = await WebApiTestManager.HttpClient.DeleteAsync(GetFullPath(_ApiUri));
            }
            catch (Exception ex)
            {
                // Throw exception as ServiceUnavailable
                ThrowExceptionOnFailure(_ApiUri, QA_ServeExceptionMode.AnyOtherUnknown, HttpStatusCode.ServiceUnavailable, ex);
            }

            // TODO refactor non positive HttpStatus
            // if (!x.IsSuccessStatusCode)
            //    throw new Exception($"Incorrect status code => {x.Headers}:{x.Content}");

            // Get validated response
            return await GetValidatedResponse(_ApiUri, x);
        }

        #endregion Public methods

        #region Private methods
        /// **************************************

        // Get validated response object on success or failure
        private static async Task<Dictionary<string, object>> GetValidatedResponse(ApiUri _ApiUri, HttpResponseMessage _HttpResponseMessage)
        {
            // Validate authorization
            ValidateAuthorization();

            // Return response for succeed/failed StatusCode
            if (_HttpResponseMessage.IsSuccessStatusCode)
                return await GetResponseOnSuccess(_ApiUri, _HttpResponseMessage);
            else
                return await GetResponseOnFailure(_ApiUri, _HttpResponseMessage);
        }
        
        // Get response on success and keep StatusCode
        private static async Task<Dictionary<string, object>> GetResponseOnSuccess(ApiUri _ApiUri, HttpResponseMessage _HttpResponseMessage)
        {
            Dictionary<string, object> objResponse;

            // Get parsed response and set StatusCode on success
            objResponse = await GetParsedResponseAndSetStatusCode(_ApiUri, _HttpResponseMessage);

            // Get response with added StatusCode
            switch (_ApiUri)
            {
                case ApiUri.Login:

                    // Get bearer token
                    BearerToken = GetBearerToken(objResponse);

                    // Set default authorization
                    WebApiTestManager.HttpClient.DefaultRequestHeaders.Add(EnvironmentParam.RequestTaskDefaultRequestHeadersAuthorization,
                                                                        $"{EnvironmentParam.RequestTastBearerToken} {BearerToken}");
                    // Set login http vars
                    BlockHttp = true;
                    LoginFailed = false;

                    break;

                case ApiUri.Request:

                    // Set request http vars
                    BlockHttp = false;
                    RequestFailed = false;

                    break;

                default:

                    // Do the default action
                    break;
            }

            // Keep response
            SetPathResponse(_ApiUri, objResponse);

            return objResponse;
        }

        // Get response on failure and keep StatusCode
        private static async Task<Dictionary<string, object>> GetResponseOnFailure(ApiUri _ApiUri, HttpResponseMessage _HttpResponseMessage)
        {
            Dictionary<string, object> objResponse;

            // Mark stright away what is failed
            MarkFailingPath(_ApiUri);

            // Get parsed response and set StatusCode on failure
            objResponse = await GetParsedResponseAndSetStatusCode(_ApiUri, _HttpResponseMessage);

            // Keep response
            SetPathResponse(_ApiUri, objResponse);

            // Throw exception on wrong StatusCode
            ThrowExceptionOnFailure(_ApiUri, QA_ServeExceptionMode.OnStatusCode, _HttpResponseMessage.StatusCode);

            return objResponse;
        }

        // Get parsed response and set StatusCode for success or failure
        private static async Task<Dictionary<string, object>> GetParsedResponseAndSetStatusCode(ApiUri _ApiUri, HttpResponseMessage _HttpResponseMessage)
        {
            Dictionary<string, object> objResponse;

            // Get content
            string _JSONContent = await _HttpResponseMessage.Content.ReadAsStringAsync();

            // Parse response on JSON content
            objResponse = JSONParser.ParseJSON(_ApiUri, _JSONContent);

            // AddStatus Status to the Request response
            objResponse.Add(FactoryParam.RequestTaskStatusCode, _HttpResponseMessage.StatusCode);

            return objResponse;
        }

        #endregion Private methods    
    }
}
