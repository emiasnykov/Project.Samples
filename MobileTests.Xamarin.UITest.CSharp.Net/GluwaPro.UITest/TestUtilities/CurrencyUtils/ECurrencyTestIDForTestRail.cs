using GluwaPro.UITest.TestUtilities.Methods.TestRailApiMethod;
using System;

namespace GluwaPro.UITest.TestUtilities.CurrencyUtils
{
    /// <summary>
    /// Active/Inactive currency test IDs for TestRail
    /// </summary>
    class ECurrencyTestIDForTestRail
    {
        /// <summary>
        /// Change currency test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForChangeTheCurrency(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_SELECT_USDG;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_SELECT_sUSDCG;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_SELECT_sNGNG;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_BTC_SELECT_BTC;
                /*
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_SELECT_sNGNG;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_SELECT_KRWG;
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_SELECT_USDG;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Send amount test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForTapAmountCustom(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_ENTER_AMOUNT;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_ENTER_AMOUNT;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_ENTER_AMOUNT;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_tBTC_ENTER_AMOUNT;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_ENTER_AMOUNT;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_ENTER_AMOUNT;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_ENTER_AMOUNT;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Enter address test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForEnterAddress(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_ENTER_ADDRESS;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_ENTER_ADDRESS;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_ENTER_ADDRESS;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_tBTC_ENTER_ADDRESS;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_ENTER_ADDRESS;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_ENTER_ADDRESS;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_ENTER_ADDRESS;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Enter password test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForEnterPassword(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_PASSWORD_PAGE;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_PASSWORD_PAGE;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_PASSWORD_PAGE;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_tBTC_PASSWORD_PAGE;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_PASSWORD_PAGE;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_PASSWORD_PAGE;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_PASSWORD_PAGE;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Send transaction test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForSendTransaction(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_TRANSACTION_PAGE;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_TRANSACTION_PAGE;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_TRANSACTION_PAGE;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_tBTC_TRANSACTION_PAGE;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_TRANSACTION_PAGE;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_TRANSACTION_PAGE;
                
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_TRANSACTION_PAGE;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Send view test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForSendView(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_GO_TO_SEND_PAGE;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_GO_TO_SEND_PAGE;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_GO_TO_SEND_PAGE;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_tBTC_GO_TO_SEND_PAGE;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_GO_TO_SEND_PAGE;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_GO_TO_SEND_PAGE;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_GO_TO_SEND_PAGE;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Turn on sandbox test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForTurnOnSandboxMode(ECurrency currency)
        {
            switch (currency)
            {

                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_SANDBOX_MODE_ON;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_SANDBOX_MODE_ON;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_SANDBOX_MODE_ON;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_tBTC_SANDBOX_MODE_ON;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_SANDBOX_MODE_ON;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_SANDBOX_MODE_ON;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_SANDBOX_MODE_ON;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Select currency test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForSELECTCURRENCY(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_SELECT_USDG;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_SELECT_sUSDCG;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_SELECT_sNGNG;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_tBTC_SELECT_BTC;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_SELECT_USDG;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_SELECT_KRWG;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_SELECT_sNGNG;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Invalid address test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForInvalidAddresses(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_GO_TO_BTC_ADDRESS;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_BTC_GO_TO_ETH_ADDRESS;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_GO_TO_BTC_ADDRESS;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_GO_TO_BTC_ADDRESS;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_GO_TO_BTC_ADDRESS;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_GO_TO_BTC_ADDRESS;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_GO_TO_BTC_ADDRESS;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Invalid own address test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForOwnAddresses(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_INVALID_ADDRESS_ENTER_USERS_OWN_ADDRESS;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_BTC_GO_TO_TESTNET_BTC_ADDRESS;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_INVALID_ADDRESS_ENTER_USERS_OWN_ADDRESS;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_INVALID_ADDRESS_ENTER_USERS_OWN_ADDRESS;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_INVALID_ADDRESS_ENTER_USERS_OWN_ADDRESS;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_INVALID_ADDRESS_ENTER_USERS_OWN_ADDRESS;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_INVALID_ADDRESS_ENTER_USERS_OWN_ADDRESS;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Short address test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForShorterAddresses(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_INVALID_ADDRESS_ENTER_41_CHARACTERS;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_INVALID_ADDRESS_ENTER_41_CHARACTERS;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_INVALID_ADDRESS_ENTER_41_CHARACTERS;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_INVALID_ADDRESS_ENTER_41_CHARACTERS;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_INVALID_ADDRESS_ENTER_41_CHARACTERS;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_INVALID_ADDRESS_ENTER_41_CHARACTERS;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Send minimum amount test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForSendMinimumAmount(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_MINIMUM_ACCOUNT;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_MINIMUM_ACCOUNT;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_MINIMUM_ACCOUNT;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_tBTC_MINIMUM_ACCOUNT;
                /*
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_MINIMUM_ACCOUNT;
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_MINIMUM_ACCOUNT;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_MINIMUM_ACCOUNT;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Send min success amount test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForSendMinimumSuccessAmount(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_MINIMUM_ACCOUNT_SUCCESS;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_MINIMUM_ACCOUNT_SUCCESS;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_MINIMUM_ACCOUNT_SUCCESS;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_tBTC_MINIMUM_ACCOUNT_SUCCESS;
                /*
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_MINIMUM_ACCOUNT_SUCCESS;
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_MINIMUM_ACCOUNT_SUCCESS;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_MINIMUM_ACCOUNT_SUCCESS;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Insufficient funds test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForSendAmountInsufficientFunds(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_INSUFFICIENT_FUNDS_FOR_FEE;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_INSUFFICIENT_FUNDS_FOR_FEE;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_INSUFFICIENT_FUNDS_FOR_FEE;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_tBTC_INSUFFICIENT_FUNDS_FOR_FEE;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_INSUFFICIENT_FUNDS_FOR_FEE;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_INSUFFICIENT_FUNDS_FOR_FEE;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_INSUFFICIENT_FUNDS_FOR_FEE;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Insufficient funds confirm test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForSendAmountInsufficientFundsConfirm(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_INSUFFICIENT_FUNDS_FOR_FEE_CONFIRM;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_INSUFFICIENT_FUNDS_FOR_FEE_CONFIRM;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_INSUFFICIENT_FUNDS_FOR_FEE_CONFIRM;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_tBTC_INSUFFICIENT_FUNDS_FOR_FEE_CONFIRM;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDCG_INSUFFICIENT_FUNDS_FOR_FEE_CONFIRM;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_INSUFFICIENT_FUNDS_FOR_FEE_CONFIRM;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_INSUFFICIENT_FUNDS_FOR_FEE_CONFIRM;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Send too small amount test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForSendBelowMinimumAmount(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDcG_BELOW_THEN_MINIMUM_ACCOUNT;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_BELOW_THEN_MINIMUM_ACCOUNT;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_BELOW_THEN_MINIMUM_ACCOUNT;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_tBTC_BELOW_THEN_MINIMUM_ACCOUNT;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDcG_BELOW_THEN_MINIMUM_ACCOUNT;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_BELOW_THEN_MINIMUM_ACCOUNT;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_BELOW_THEN_MINIMUM_ACCOUNT;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }

        /// <summary>
        /// Send over balance amount test IDs
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static int GetTestIDForTestRailForSendAmountOverBalance(ECurrency currency)
        {
            switch (currency)
            {
                case ECurrency.Usdcg:
                    return TestIDForTestRail.SEND_USDCG_OVER_THE_BALANCE;
                case ECurrency.sUsdcg:
                    return TestIDForTestRail.SEND_sUSDCG_OVER_THE_BALANCE;
                case ECurrency.sNgNg:
                    return TestIDForTestRail.SEND_sNGNG_OVER_THE_BALANCE;
                case ECurrency.Btc:
                    return TestIDForTestRail.SEND_BTC_OVER_THE_BALANCE;
                /*
                case ECurrency.Usdg:
                    return TestIDForTestRail.SEND_USDG_OVER_THE_BALANCE;
                case ECurrency.Krwg:
                    return TestIDForTestRail.SEND_KRWG_OVER_THE_BALANCE;
                case ECurrency.NgNg:
                    return TestIDForTestRail.SEND_NGNG_OVER_THE_BALANCE;
                */
                default:
                    throw new Exception("No existing TestIDForTestRail for currency");
            }
        }
    }
}
