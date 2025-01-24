using System;
using System.Configuration;
using ResWebApiTest.TestEngine.Constants;
using ResWebApiTest.TestEngine.Enums;

namespace ResWebApiTest.TestEngine.Helpers
{
    /// <summary>
    /// Configurer class for environment settings
    /// </summary>
    public class Configurer
    {
        #region Public methods
        /// **************************************

        /// <summary>
        /// Get WebApi url path
        /// </summary>
        /// <returns>WebApi url path</returns>
        public static string GetWebApiUrl()
        {
            string webApiUrl = $"{Enum.GetName(typeof(ConfigurerMode), UseConfigMode())}_{EnvironmentParam.ConfigurerWebApiUrl}";

            return ConfigurationManager.AppSettings[webApiUrl]; 
        }

        /// <summary>
        /// Get WebApiTest log folder 
        /// </summary>
        /// <returns>WebApiTest log folder/returns>
        public static string GetWebApiTestLogFolder()
        {
            string logFolder = $"{Enum.GetName(typeof(ConfigurerMode), UseConfigMode())}_{EnvironmentParam.ConfigurerLogFolder}";

            return $"{GetWebApiTestBasePath()}{ConfigurationManager.AppSettings[logFolder]}";
        }

        /// <summary>
        /// Get WebApiTest source tests folder 
        /// </summary>
        /// <returns>WebApiTest Source tests folder</returns>
        public static string GetWebApiTestSourceTestsFolder()
        {
            return $"{GetWebApiTestBasePath()}{EnvironmentParam.ConfigurerSourceTestsFolder}";
        }

        #endregion Public methods

        #region Private methods
        /// **************************************

        // Get config mode from Setup Environment
        private static ConfigurerMode UseConfigMode()
        {
            // Set conguration mode for IISExpress as a default one
            ConfigurerMode configMode = ConfigurerMode.IISExpress;

            // Get config mode
            string useConfigMode = ConfigurationManager.AppSettings[EnvironmentParam.ConfigurerUseConfigMode];

            // Validate there is no mistake in the config file and the config mode exists in the enumeration
            if (useConfigMode != null || useConfigMode.Length > 0)
            {
                // Grab all possible config modes
                Array ConfigModeValues = Enum.GetValues(typeof(ConfigurerMode));

                // Loop the config mode
                foreach (ConfigurerMode allowedConfigMode in ConfigModeValues)
                {
                    // Check the config mode type and set it on found (should be)
                    if (Enum.GetName(typeof(ConfigurerMode), allowedConfigMode).Equals(useConfigMode))
                    {
                        // Set proper config mode
                        configMode = allowedConfigMode;

                        break;
                    }
                }
            }

            return configMode;
        }

        // Get base paths
        private static string GetWebApiTestBasePath()
        {
            // Get paths
            string rootPath = $"{Enum.GetName(typeof(ConfigurerMode), UseConfigMode())}_{EnvironmentParam.ConfigurerRootPath}";
            string mainPath = $"{Enum.GetName(typeof(ConfigurerMode), UseConfigMode())}_{EnvironmentParam.ConfigurerMainPath}";

            // Set paths
            rootPath = ConfigurationManager.AppSettings[rootPath];
            mainPath = ConfigurationManager.AppSettings[mainPath];

            return $"{rootPath}{mainPath}";
        }

        #endregion Private methods
    }
}
