using System;
using System.Collections.Generic;
using ResWebApiTest.TestEngine.Constants;
using ResWebApiTest.TestEngine.Managers;
using ResWebApiTest.TestEngine.QA_Enums;
using static ResWebApiTest.TestEngine.WebApiTest;

namespace ResWebApiTest.TestEngine.Factory
{
    /// <summary>
    /// Bearer token static methods
    /// </summary>
    public class Token
    {
        #region Public methods
        /// **************************************

        /// <summary>
        /// Validate authorization token
        /// If something is wrong throw exception
        /// </summary>
        public static void ValidateAuthorization()
        {
            // Throw exception if authorization failed
            if (WebApiTestManager.BlockHttp && !(!(WebApiTestManager.BearerToken is null) && (WebApiTestManager.BearerToken != "")))
            {
                // Set flag the request is failed
                WebApiTestManager.RequestFailed = true;

                // Throw exception - TODO refactor
                TestsExceptions.ThrowExceptionOnFailure(ApiUri.Request, QA_ServeExceptionMode.OnAuthorization);
            }
        }

        /// <summary>
        /// Checks under which context token exists
        /// </summary>
        /// <returns>Returns beared token</returns>
        public static string GetBearerToken(Dictionary<string, object> _ObjResponse)
        {
            string resultToken;

            // Check and use different possible context token response if exists
            bool keyExists = _ObjResponse.ContainsKey(FactoryParam.RequestTaskAccessToken);

            // Check key 
            if (keyExists)
            {
                // Get the access token
                _ObjResponse.TryGetValue(FactoryParam.RequestTaskAccessToken, out object valueObj);

                // Check the access token object
                if (!(valueObj is null))
                    resultToken = valueObj.ToString();
                else
                    // TODO throw proper exception
                    throw new Exception("keyExists = true, but bearer token can not be grabbed");
            }
            else
            {
                // Get the access token
                _ObjResponse.TryGetValue(FactoryParam.RequestTaskSimpleToken, out object valueObj);

                // Check the access token object
                if (!(valueObj is null))
                    resultToken = valueObj.ToString();
                else
                    // TODO throw proper exception
                    throw new Exception("keyExists = false, but bearer token can not be grabbed. what now?");
            }

            return resultToken;
        }

        #endregion Public methods
    }
}
