namespace ResWebApiTest.TestEngine.Constants
{
    /// <summary>
    /// Environment constant params (non changeable)
    /// </summary
    sealed public class EnvironmentParam
    {
        #region Configurer params
        /// **************************************

        // Main configuration string to be used
        internal const string ConfigurerUseConfigMode = "UseConfigMode";

        // Main WebApi url
        internal const string ConfigurerWebApiUrl = "WebApi_Url";

        // Main WebApiTest folders
        internal const string ConfigurerLogFolder = "WebApiTest_LogFolder";
        internal const string ConfigurerRootPath = "WebApiTest_RootPath";
        internal const string ConfigurerMainPath = "WebApiTest_MainPath";

        // Main WebApiTest source tests folder
        internal const string ConfigurerSourceTestsFolder = @"\Tests\Api\";

        #endregion Configurer params


        #region Logger params
        /// **************************************

        // Main params
        internal const string LoggerFilePath = "LoggerFilePath";
        internal const string LoggerName = "LoggerName";

        #endregion Logger params


        #region RequestTask params
        /// **************************************

        // Main params
        internal const string RequestTaskApplicationJson = "application/json";
        internal const string RequestTaskDefaultRequestHeadersAuthorization = "Authorization";
        internal const string RequestTastBearerToken = "Bearer";

        #endregion RequestTask params
    }
}
