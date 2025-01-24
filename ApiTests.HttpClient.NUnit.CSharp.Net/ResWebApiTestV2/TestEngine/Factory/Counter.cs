using ResWebApiTest.TestEngine.Managers;
using static ResWebApiTest.TestEngine.Constants.BasicEntity;
using static ResWebApiTest.TestEngine.WebApiTest;

namespace ResWebApiTest.TestEngine.Factory
{
    /// <summary>
    /// Counter static methods
    /// </summary>
    public class Counter
    {
        #region Public methods
        /// **************************************

        /// <summary>
        /// Check count based on Expected JSON object count and Test's asserts count 
        /// </summary>
        /// <param name="_CheckCount">Enumeration check count</param>
        /// <returns>true, if both counts are the same</returns>
        public static bool IsCountCorrect(CheckCount _CheckCount = CheckCount.Always)
        {
            bool res = false;

            // Check asserts counts used in the test
            int count = WebApiTestManager.ExpectedCount - WebApiTestManager.ResponseCount;

            // Revert value for last passed asserts
            WebApiTestManager.LastAssertPassed = !WebApiTestManager.LastAssertPassed;

            // If count is correct, means fine
            if (count == 0)
                res = true;

            // If we ignore the counts, please inform us
            if (_CheckCount == CheckCount.Ignore)
                WebApiTestManager.CheckAssertCount = false;

            // If check count is incorrect on behalf of ignore, will inform us as true
            if (!WebApiTestManager.CheckAssertCount)
                res = true;

            return res;
        }

        /// <summary>
        /// Send info on failure count on assertions
        /// </summary>
        /// <returns>Information about counts failure of assertions</returns>
        public static string InfoCountOnFailure()
        {
            return $"Asserts count differ from Expected count (response JSON object) in the test."+
                   $"{NewLine}"+
                   $"Asserts count = {WebApiTestManager.ResponseCount}. Expected count = {WebApiTestManager.ExpectedCount}.";
        }

        #endregion Public methods
    }
}
