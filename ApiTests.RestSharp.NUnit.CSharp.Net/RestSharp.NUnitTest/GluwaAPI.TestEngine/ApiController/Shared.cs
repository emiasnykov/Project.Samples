using System;

namespace GluwaAPI.TestEngine.ApiController
{
    public class Shared
    {
        // Shared data inputs
        public static string NULL_OR_EMPTY = null;
        public static string MIN_AMOUNT = "0.1";
        public static string NEGATIVE_AMOUNT = "-1";
        public static string NEGATIVE_FEE = "-1";
        public static string INVALID_AMOUNT = "1mm";
        public static string INVALID_CURRENCY = "CAKE";
        public static string INVALID_FORMAT_AMOUNT = "1,12";
        public static string INVALID_FORMAT_FEE = "1,12";
        public static string INVALID_IDEM = "1m";
        public static string INVALID_NONCE = "1m";
        public static string INVALID_ID = "*****************************************";
        public static string OVER_SIX_DECIMALS_AMOUNT = "100.2345123";
        public static string OVER_SIX_DECIMALS_FEE = "200.23451234";
        public static string TOO_SMALL_AMOUNT = "0.0000001";
        public static string VALID_IDEM = "***********************************";
        public static string VALID_SIGNATURE = "*****************************************";
        public static string VALID_TXNID = "*****************************************";
        public static string ZERO_BALANCES_ADDRESS = "*****************************************";
        public static string ORDER_NOT_FOUND = "*****************************************";
        public static string DRAW_NOT_FOUND = "*****************************************";
        public static string NONACCOUNT_ADDRESS = "*****************************************";
        public static string CHAIN_ID_GOERLI_TEST = "5";
        public static string CHAIN_ID_BSC_TEST = "97";
        public static string CHAIN_ID_LUNIVERSE_TEST = "*****************************************";
        public static string CHAIN_ID_MAIN = "*****************************************";
        public static string CONTRACT_ADDRESS_SGDG_LUNIVERSE = "*****************************************";
        public static string KALEIDO_CHAIN_ID = "*****************************************";
        public static string CONTRACT_ADDRESS_SGDG_KALEIDO = "*****************************************";
        public static string NOT_FOUND_ID = "*****************************************";
        public static string NOT_FOUND_TXNID = "*****************************************";
        public const string APPROVE_ABI = "*****************************************";
        public static string POOL_ID = "*****************************************";
        public static string POOL_HASH = "*****************************************";
        public static string ACCOUNT_ID = "*****************************************";

        // USDC
        public static string USDC_TEST_CONTRACT = "*****************************************";
        public static string USDC_MAIN_CONTRACT = "*****************************************";
        public static int USDC_DECIMALS = 6;

        // USDT
        public static string USDT_TEST_CONTRACT = "*****************************************";
        public static string USDT_MAIN_CONTRACT = "*****************************************";
        public static int USDT_DECIMALS = 6;

        // USDC-G
        public static string USDCG_TEST_CONTRACT = "*****************************************";
        public static string USDCG_MAIN_CONTRACT = "*****************************************";
        public static int USDCG_DECIMALS = 6;

        // NGN-G
        public static string NGNG_TEST_CONTRACT = "*****************************************";
        public static string NGNG_MAIN_CONTRACT = "*****************************************";
        public static int NGNG_DECIMALS = 18;

        // G-CRE
        public static string GCRE_TEST_CONTRACT = "*****************************************";
        public static string GCRE_MAIN_CONTRACT = "*****************************************";
        public static int GCRE_DECIMALS = 18;

        // sUSDC-G
        public static string SUSDCG_TEST_CONTRACT = "*****************************************";
        public static string SUSDCG_MAIN_CONTRACT = "*****************************************";
        public static int SUSDCG_DECIMALS = 6;

        // sNGN-G
        public static string SNGNG_TEST_CONTRACT = "*****************************************";
        public static string SNGNG_MAIN_CONTRACT = "*****************************************";
        public static int SNGNG_DECIMALS = 18;

        // GTD
        public static string GTD_TEST_CONTRACT = "*****************************************";
        public static string GTD_SANDBOX_CONTRACT = "*****************************************";
        public static string GTD_MAIN_CONTRACT = "*****************************************";
        public static int GTD_DECIMALS = 18;

        // GATE
        public static string GATE_TEST_CONTRACT = "*****************************************";
        public static string GATE_SANDBOX_CONTRACT = "*****************************************";
        public static string GATE_MAIN_CONTRACT = "*****************************************";
        public static int GATE_DECIMALS = 18;

        // sSGD-G
        public static string SSGDG_TEST_CONTRACT = "*****************************************";
        public static string SSGDG_SAND_CONTRACT = "*****************************************";
        public static string SSGDG_STAGE_CONTRACT = "*****************************************";
        public static string SSGDG_MAIN_CONTRACT = "*****************************************";
        public static int SSGDG_DECIMALS = 18;

        // Invalid test data
        public static string INVALID_ENV = "Mainet";

        //Shared constants
        public const string FTA_ABI = "*****************************************";
        public const string TRANSFER_ABI = "*****************************************";
        public const string USDCG_ABI = "*****************************************";


        // TODO move all data to security vault


        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static bool isTestEnvironment(string environment)
        {
            string lowercaseEnv = environment.ToLower();
            return lowercaseEnv == "test" || lowercaseEnv == "sandbox" || lowercaseEnv == "test2";
        }

        /// <summary>
        /// Get existing DrawId
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetDrawId(string environment)
        {
            if (environment == "Test")
            {
                return "{\"DrawId\":\"*****************************\"}";
            }
            else if (environment == "Staging")
            {
                return "{\"DrawId\":\"*****************************\"}";
            }
            else
                return "{\"DrawId\":\"*****************************\"}";  // Production
        }

        /// <summary>
        /// Get btc address with zero balance
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetZeroBalanceBtcAddress(string environment)
        {
            if (environment == "Test")
            {
                return "*****************************";
            }
            else
                return "*****************************";
        }

        /// <summary>
        /// Get SL docId by type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetSideLetterDocument(string type, string environment)
        {
            if (type == "Bond")
            {
                if (environment == "Test")
                {
                    return "*****************************";
                }
                else if (environment == "Staging")
                {
                    return "*****************************";
                }
                else if (environment == "Production")
                {
                    return "*****************************";
                }
                else
                {
                    return "SL Not Found";
                }
            }
            else if (type == "Fta")
            {
                if (environment == "Test")
                {
                    return "";
                }
                else if (environment == "Staging")
                {
                    return "";
                }
                else if (environment == "Production")
                {
                    return "";
                }
                else
                {
                    return "SL Not Found";
                }
            }
            else
            {
                if (environment == "Test")
                {
                    return "*****************************";
                }
                else if (environment == "Staging")
                {
                    return "*****************************";
                }
                else if (environment == "Production")
                {
                    return "*****************************";
                }
                else
                {
                    return "SL Not Found";
                }
            }
        }

        /// <summary>
        /// Get SA docId by type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string SubscriptionAgreementDocumen(string type, string environment)
        {
            if (type == "Bond")
            {
                if (environment == "Test")
                {
                    return "*****************************";
                }
                else if (environment == "Staging")
                {
                    return "*****************************";
                }
                else if (environment == "Production")
                {
                    return "*****************************";
                }
                else
                {
                    return "SA Not Found";
                }
            }
            else if (type == "Fta")
            {
                if (environment == "Test")
                {
                    return "";
                }
                else if (environment == "Staging")
                {
                    return "";
                }
                else if (environment == "Production")
                {
                    return "";
                }
                else
                {
                    return "SA Not Found";
                }
            }
            else
            {
                if (environment == "Test")
                {
                    return "*****************************";
                }
                else if (environment == "Staging")
                {
                    return "*****************************";
                }
                else if (environment == "Production")
                {
                    return "*****************************";
                }
                else
                {
                    return "SA Not Found";
                }
            }
        }

        /// <summary>
        /// Get conflicting idem for DAO request
        /// </summary>
        /// <returns>Idem</returns>
        public static string GetDaoConflictIdem(string environment)
        {
            switch (environment)
            {
                case "Test":
                    return "*****************************";
                case "Staging":
                    return "*****************************";
                //case "Production":
                //return "*****************************";
                default:
                    throw new Exception($"No conflict idem for {environment}");
            }
        }

        /// <summary>
        /// Get ChainId 
        /// </summary>
        /// <returns>Idem</returns>
        public static string GetChainId(string environment)
        {
            switch (environment)
            {
                case "Test":
                    return "5";
                case "Staging":
                    return "1";
                default:
                    throw new Exception($"No chainId for {environment}");
            }
        }
    }
}
