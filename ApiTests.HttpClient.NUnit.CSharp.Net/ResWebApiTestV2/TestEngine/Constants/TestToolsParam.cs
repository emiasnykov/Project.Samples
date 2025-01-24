namespace ResWebApiTest.TestEngine.Constants
{
    /// <summary>
    /// Internal QA tool constant params for tests (non changeable)
    /// </summary
    sealed public class TestToolsParam
    {
        #region QA_Invocation params
        /// **************************************

        // Main QA tool params for internal invocation inside the test
        internal const string QA_GenerateAsserts = "WebApiTest.QA_GenerateAsserts();";

        #endregion QA_Invocation params


        #region Test params
        /// **************************************

        // Test api dictionary
        internal const string TestApiDirectory = "Tests";

        // Test file extension
        internal const string TestFileExt = "cs";

        // Test asserts
        internal const string TestAsserts = "Asserts";
        internal const string TestRegion = "#region Asserts for ";

        #endregion Test params
    }
}
