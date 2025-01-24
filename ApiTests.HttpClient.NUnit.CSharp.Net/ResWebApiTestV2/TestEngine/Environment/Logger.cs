using log4net;
using log4net.Config;
using ResWebApiTest.TestEngine.Constants;

namespace ResWebApiTest.TestEngine.Helpers
{
    /// <summary>
    /// Logger class for output info printouts
    /// </summary>
    public class Logger
    {
        #region Properties
        /// **************************************

        /// <summary>
        /// Property logger initialized
        /// </summary>
        public static bool Initialized { get; set; }

        #endregion Properties

        #region Private variables
        /// **************************************

        // Init log4net log
        private static readonly ILog iLog = LogManager.GetLogger(EnvironmentParam.LoggerName);

        // Log info 
        private static string messageLog;

        // Log flag for each test if passed/failed
        private static bool failed;

        #endregion Private variables

        #region Public methods
        /// **************************************

        /// <summary>
        /// Init logger
        /// </summary>
        public static void Init()
        {
            // Configure log path
            GlobalContext.Properties[EnvironmentParam.LoggerFilePath] = $"{Configurer.GetWebApiTestLogFolder()}";

            // Configuration property
            XmlConfigurator.Configure();

            // Set init vars
            failed = false;
            messageLog = "";

            // Check and set initialization flag
            if (!Initialized)
                Initialized = true;
        }

        /// <summary>
        /// Start test log
        /// </summary>
        /// <param name="_LogInfo">Log info</param>
        public static void StartLog(string _LogInfo)
        {
            // Check the log information and set proper message on start
            if (Initialized && !string.IsNullOrEmpty(_LogInfo))
                messageLog += _LogInfo;
        }

        /// <summary>
        /// End test log
        /// </summary>
        /// <param name="_LogInfo">Log info</param>
        public static void EndLog(string _LogInfo)
        {
            // Check the log information and set proper message on end and log it as info
            if (Initialized && (!string.IsNullOrEmpty(_LogInfo)))
                iLog.Info($"{_LogInfo}{BasicEntity.LoggerTrail}");

            // Set init flag
            Initialized = false;
        }

        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="_LogException">Log exception</param>
        public static void Error(string _LogException)
        {
            // Log info message
            LogMessage();

            // Set flag
            failed = true;

            // Log it as fatal
            iLog.Fatal(_LogException);
        }

        /// <summary>
        /// Test failed
        /// </summary>
        public static void TestFailed(bool _LastAssertPassed)
        {
            // Log info mesage
            LogMessage();

            // If flag is set to failed then log it as fatal on exception, otherwise log it as an error on test assertion
            if (failed)
                iLog.Fatal("FAILED on Exception.");
            else if (_LastAssertPassed)
                iLog.Error("FAILED on Test Assertion.");
        }

        /// <summary>
        /// Test passed
        /// </summary>
        /// <param name="_AsExpected">flag es axepected</param>
        public static void TestPassed(bool _AsExpected)
        {
            // If flag is set to not failed then log it as info log as passed, otherwise if log is failed and accepted log proper info as well
            if (!failed)
                iLog.Info("Test PASSED.");
            else if (failed && _AsExpected)
                iLog.Info("Test PASSED on Exception as excpected.");
        }

        #endregion Public methods

        #region Private methods
        /// **************************************

        // Log message
        private static void LogMessage()
        {
            // Simple log message
            if (Initialized && !string.IsNullOrEmpty(messageLog))
                iLog.Info($"{messageLog}");
        }

        #endregion Private methods
    }
}
