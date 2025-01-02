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
        public static string INVALID_ID = "02614830-ACAE-4D0F-B441-A0D9EBC0304";
        public static string OVER_SIX_DECIMALS_AMOUNT = "100.2345123";
        public static string OVER_SIX_DECIMALS_FEE = "200.23451234";
        public static string TOO_SMALL_AMOUNT = "0.0000001";
        public static string VALID_IDEM = "4CB0EE26-A715-4E4C-8319-1E3333CA1F16";
        public static string VALID_SIGNATURE = "0xf8ac8201fa85076cb8b5cd830606f8940488efdeb8cf3b5c1a6afb0a5f84a2d0b37a5d4c80b844a9059cbb000000000000000000000000bbb8bbaf43fe8b9e5572b1860d5c94ac7ed87bb90000000000000000000000000000000000000000000000004563918244f400001ca08cb68be9d7a04324131e036efa9ee54ad4a1d0606a88262119d2b6b7f2a6675ca03c01bbdc8c3f5a945e37891cc8f3a3eed6b126175c7fa6d53c34e64a955dbe7f";
        public static string VALID_TXNID = "0x7d5c484005383e7e5c8cd2e55c9095cf510dbceca998fa0b3e245690c183b489";
        public static string ZERO_BALANCES_ADDRESS = "0x40F53Dfa9Ed2c94A2A1670B8D6f3A8ae5cC1ccE6";
        public static string ORDER_NOT_FOUND = "a877ef7a-6fda-416b-9866-707d32913820";
        public static string DRAW_NOT_FOUND = "{\"DrawId\":\"E598F483-60D4-4D42-AEEE-ECC86CB3D984\"}";
        public static string NONACCOUNT_ADDRESS = "0xaBB451D3aAAFfDc45B00548Bce55b4c3f8A37Af3";
        public static string CHAIN_ID_GOERLI_TEST = "5";
        public static string CHAIN_ID_BSC_TEST = "97";
        public static string CHAIN_ID_LUNIVERSE_TEST = "1635501961136826136";
        public static string CHAIN_ID_MAIN = "3158073271666164067";
        public static string CONTRACT_ADDRESS_SGDG_LUNIVERSE = "0x2b092b3866DD169Ddad2965281E257624AC23F8C";
        public static string KALEIDO_CHAIN_ID = "1245549440";
        public static string CONTRACT_ADDRESS_SGDG_KALEIDO = "0x0AFD1fBeDaA3bbCDe89e92985475Bc7Dc20d2f5C";
        public static string NOT_FOUND_ID = "E272D8E4-E576-4B54-BE44-F0BE6FF40EB9";
        public static string NOT_FOUND_TXNID = "C7A3D54A-58C9-4388-A555-2D5E2D29C76D";
        public const string APPROVE_ABI = "[{'constant':false, 'inputs':[{'internalType':'address', 'name':'spender', 'type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'}],'name':'approve','outputs':[{'internalType':'bool','name':'','type':'bool'}],'payable':false,'stateMutability':'nonpayable','type':'function'}]";
        public static string POOL_ID = "7460D6C9-D0A7-4802-A706-9FF01FA440FE";
        public static string POOL_HASH = "0x80F0B10E06D46EABE7B5E930F7362DAA60B03A4801958B5FF6D82F8894B458CA";
        public static string ACCOUNT_ID = "aacf9a96-39fc-42e0-b0d0-7bb94bd7647b";

        // USDC
        public static string USDC_TEST_CONTRACT = "0x07865c6e87b9f70255377e024ace6630c1eaa37f";
        public static string USDC_MAIN_CONTRACT = "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606eB48";
        public static int USDC_DECIMALS = 6;

        // USDT
        public static string USDT_TEST_CONTRACT = "0xC398577ebC7d3925565206Ea37DD24616DA608Dc";
        public static string USDT_MAIN_CONTRACT = "0xdac17f958d2ee523a2206206994597c13d831ec7";
        public static int USDT_DECIMALS = 6;

        // USDC-G
        public static string USDCG_TEST_CONTRACT = "0x000CC169848eC63466A7EfB59e1b925ABAb5F92b";
        public static string USDCG_MAIN_CONTRACT = "0x1b9c206cf3df47c3b04941539b07f5784cbdde42";
        public static int USDCG_DECIMALS = 6;

        // NGN-G
        public static string NGNG_TEST_CONTRACT = "0x1AA950bD468997A28927434cB4F030AE0f19c8a7";
        public static string NGNG_MAIN_CONTRACT = "0x4AB30B965A8Ef0F512DA064B5e574d9Ad73c0e79";
        public static int NGNG_DECIMALS = 18;

        // G-CRE
        public static string GCRE_TEST_CONTRACT = "0x0488efdeb8Cf3B5C1A6aFb0a5F84A2d0B37A5D4C";
        public static string GCRE_MAIN_CONTRACT = "0xa3ee21c306a700e682abcdfe9baa6a08f3820419";
        public static int GCRE_DECIMALS = 18;

        // sUSDC-G
        public static string SUSDCG_TEST_CONTRACT = "0xdbde880e1405a6914b387606933d7476a2296a06";
        public static string SUSDCG_MAIN_CONTRACT = "0x39589FD5A1D4C7633142A178F2F2b30314FB2BaF";
        public static int SUSDCG_DECIMALS = 6;

        // sNGN-G
        public static string SNGNG_TEST_CONTRACT = "0x6519f0342edc7bc4faa900b8c692eaa2df2e4ccf";
        public static string SNGNG_MAIN_CONTRACT = "0xc33496C93AaFf765e4925A4E3b873d5efc635405";
        public static int SNGNG_DECIMALS = 18;

        // GTD
        public static string GTD_TEST_CONTRACT = "0x923337A132b12796427d5036391F8629bbF2FA5B";
        public static string GTD_SANDBOX_CONTRACT = "0x3698b85C498fcf717Dc4691AD053A112FBC4143c";
        public static string GTD_MAIN_CONTRACT = "0xb00C9B1771Cf6Ae791E6476810b19c77f0457B2d";
        public static int GTD_DECIMALS = 18;

        // GATE
        public static string GATE_TEST_CONTRACT = "0x543793D4d576238869Ee19b471029e85b53845e9";
        public static string GATE_SANDBOX_CONTRACT = "0x38E522c651E3F6fbba0eEFDc5162bCb5fB5cA112";
        public static string GATE_MAIN_CONTRACT = "0xA2C3A0DC05Bfa9BC957732590143252d45DF30e2";
        public static int GATE_DECIMALS = 18;

        // sSGD-G
        public static string SSGDG_TEST_CONTRACT = "0x2b092b3866DD169Ddad2965281E257624AC23F8C";
        public static string SSGDG_SAND_CONTRACT = "0xC3a1A0Bee7C84612270e40d0b23953a1f4b68689";
        public static string SSGDG_STAGE_CONTRACT = "0x8d0B3E628D1D305cb537070037E6A186f9B561d3";
        public static string SSGDG_MAIN_CONTRACT = "0x216a5b0388e673C489C741bff1AfEA759350953f";
        public static int SSGDG_DECIMALS = 18;

        // Invalid test data
        public static string INVALID_ENV = "Mainet";

        //Shared constants
        public const string FTA_ABI = @"[{'anonymous':false,'inputs':[{'indexed':false,'internalType':'uint8','name':'version','type':'uint8'}],'name':'Initialized','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'recipient','type':'address'},{'indexed':false,'internalType':'uint256','name':'amount','type':'uint256'}],'name':'Invest','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'bytes32','name':'accountHash','type':'bytes32'},{'indexed':true,'internalType':'address','name':'owner','type':'address'}],'name':'LogAccount','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'bytes32','name':'balanceHash','type':'bytes32'},{'indexed':true,'internalType':'address','name':'owner','type':'address'},{'indexed':false,'internalType':'uint256','name':'deposit','type':'uint256'},{'indexed':false,'internalType':'uint256','name':'fee','type':'uint256'}],'name':'LogBalance','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'bytes32','name':'poolHash','type':'bytes32'}],'name':'LogPool','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'bytes32','name':'role','type':'bytes32'},{'indexed':true,'internalType':'bytes32','name':'previousAdminRole','type':'bytes32'},{'indexed':true,'internalType':'bytes32','name':'newAdminRole','type':'bytes32'}],'name':'RoleAdminChanged','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'bytes32','name':'role','type':'bytes32'},{'indexed':true,'internalType':'address','name':'account','type':'address'},{'indexed':true,'internalType':'address','name':'sender','type':'address'}],'name':'RoleGranted','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'bytes32','name':'role','type':'bytes32'},{'indexed':true,'internalType':'address','name':'account','type':'address'},{'indexed':true,'internalType':'address','name':'sender','type':'address'}],'name':'RoleRevoked','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'beneficiary','type':'address'},{'indexed':false,'internalType':'uint256','name':'amount','type':'uint256'}],'name':'Withdraw','type':'event'},{'inputs':[],'name':'CONTROLLER_ROLE','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'DEFAULT_ADMIN_ROLE','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'INTEREST_DENOMINATOR','outputs':[{'internalType':'uint32','name':'','type':'uint32'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'OPERATOR_ROLE','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'addAdmin','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'addController','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'addOperator','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'source','type':'address'},{'internalType':'bytes32','name':'poolHash','type':'bytes32'},{'internalType':'uint256','name':'amount','type':'uint256'}],'name':'addPoolRepayment','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'},{'internalType':'uint256','name':'fee','type':'uint256'},{'internalType':'bytes32','name':'identityHash','type':'bytes32'},{'internalType':'bytes32','name':'poolHash','type':'bytes32'}],'name':'createAccount','outputs':[{'internalType':'bool','name':'','type':'bool'},{'internalType':'bytes32','name':'','type':'bytes32'},{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'},{'internalType':'bytes32','name':'identityHash','type':'bytes32'},{'internalType':'bytes32','name':'poolHash','type':'bytes32'},{'internalType':'uint256','name':'gluwaNonce','type':'uint256'},{'internalType':'uint8','name':'v','type':'uint8'},{'internalType':'bytes32','name':'r','type':'bytes32'},{'internalType':'bytes32','name':'s','type':'bytes32'}],'name':'createAccountBySig','outputs':[{'internalType':'bool','name':'','type':'bool'},{'internalType':'bytes32','name':'','type':'bytes32'},{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'},{'internalType':'uint256','name':'fee','type':'uint256'},{'internalType':'bytes32','name':'poolHash','type':'bytes32'}],'name':'createBalance','outputs':[{'internalType':'bool','name':'','type':'bool'},{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'},{'internalType':'bytes32','name':'poolHash','type':'bytes32'},{'internalType':'uint256','name':'gluwaNonce','type':'uint256'},{'internalType':'uint8','name':'v','type':'uint8'},{'internalType':'bytes32','name':'r','type':'bytes32'},{'internalType':'bytes32','name':'s','type':'bytes32'}],'name':'createBalanceBySig','outputs':[{'internalType':'bool','name':'','type':'bool'},{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'uint32','name':'interestRate','type':'uint32'},{'internalType':'uint32','name':'tenor','type':'uint32'},{'internalType':'uint64','name':'openDate','type':'uint64'},{'internalType':'uint64','name':'closeDate','type':'uint64'},{'internalType':'uint64','name':'startDate','type':'uint64'},{'internalType':'uint128','name':'minimumRaise','type':'uint128'},{'internalType':'uint256','name':'maximumRaise','type':'uint256'}],'name':'createPool','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[],'name':'getAccount','outputs':[{'internalType':'uint64','name':'','type':'uint64'},{'internalType':'address','name':'','type':'address'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'enum GluwaInvestmentModel.AccountState','name':'','type':'uint8'},{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'getAccountFor','outputs':[{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'address','name':'','type':'address'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'enum GluwaInvestmentModel.AccountState','name':'','type':'uint8'},{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'uint64','name':'idx','type':'uint64'}],'name':'getAccountHashByIdx','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'balanceHash','type':'bytes32'}],'name':'getBalance','outputs':[{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'bytes32','name':'','type':'bytes32'},{'internalType':'bytes32','name':'','type':'bytes32'},{'internalType':'address','name':'','type':'address'},{'internalType':'uint32','name':'','type':'uint32'},{'internalType':'uint32','name':'','type':'uint32'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint64','name':'','type':'uint64'},{'internalType':'uint64','name':'','type':'uint64'},{'internalType':'enum GluwaInvestmentModel.BalanceState','name':'','type':'uint8'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'uint64','name':'idx','type':'uint64'}],'name':'getBalanceHashByIdx','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'balanceHash','type':'bytes32'}],'name':'getBalanceState','outputs':[{'internalType':'enum GluwaInvestmentModel.BalanceState','name':'','type':'uint8'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'owner','type':'address'}],'name':'getMatureBalances','outputs':[{'components':[{'internalType':'uint64','name':'idx','type':'uint64'},{'internalType':'address','name':'owner','type':'address'},{'internalType':'uint256','name':'principal','type':'uint256'},{'internalType':'uint256','name':'totalWithdrawal','type':'uint256'},{'internalType':'bytes32','name':'accountHash','type':'bytes32'},{'internalType':'bytes32','name':'poolHash','type':'bytes32'}],'internalType':'struct GluwaInvestmentModel.Balance[]','name':'','type':'tuple[]'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'poolHash','type':'bytes32'}],'name':'getPool','outputs':[{'components':[{'internalType':'uint32','name':'interestRate','type':'uint32'},{'internalType':'uint32','name':'tenor','type':'uint32'},{'internalType':'uint64','name':'idx','type':'uint64'},{'internalType':'uint64','name':'openingDate','type':'uint64'},{'internalType':'uint64','name':'closingDate','type':'uint64'},{'internalType':'uint64','name':'startingDate','type':'uint64'},{'internalType':'uint128','name':'minimumRaise','type':'uint128'},{'internalType':'uint256','name':'maximumRaise','type':'uint256'},{'internalType':'uint256','name':'totalDeposit','type':'uint256'},{'internalType':'uint256','name':'totalRepayment','type':'uint256'}],'internalType':'struct GluwaInvestmentModel.Pool','name':'','type':'tuple'},{'internalType':'enum GluwaInvestmentModel.PoolState','name':'','type':'uint8'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'}],'name':'getRoleAdmin','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'uint256','name':'index','type':'uint256'}],'name':'getRoleMember','outputs':[{'internalType':'address','name':'','type':'address'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'}],'name':'getRoleMemberCount','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'owner','type':'address'}],'name':'getUnstartedBalances','outputs':[{'components':[{'internalType':'uint64','name':'idx','type':'uint64'},{'internalType':'address','name':'owner','type':'address'},{'internalType':'uint256','name':'principal','type':'uint256'},{'internalType':'uint256','name':'totalWithdrawal','type':'uint256'},{'internalType':'bytes32','name':'accountHash','type':'bytes32'},{'internalType':'bytes32','name':'poolHash','type':'bytes32'}],'internalType':'struct GluwaInvestmentModel.Balance[]','name':'','type':'tuple[]'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'accountHash','type':'bytes32'}],'name':'getUserAccount','outputs':[{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'address','name':'','type':'address'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'uint256','name':'','type':'uint256'},{'internalType':'enum GluwaInvestmentModel.AccountState','name':'','type':'uint8'},{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'getUserBalanceList','outputs':[{'internalType':'uint64[]','name':'','type':'uint64[]'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'address','name':'account','type':'address'}],'name':'grantRole','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'address','name':'account','type':'address'}],'name':'hasRole','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'adminAccount','type':'address'},{'internalType':'address','name':'token','type':'address'}],'name':'initialize','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'recipient','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'}],'name':'invest','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'isAdmin','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'isController','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'uint256','name':'nonce','type':'uint256'}],'name':'isNonceUsed','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'isOperator','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'poolHash','type':'bytes32'},{'internalType':'bool','name':'isLocked','type':'bool'}],'name':'lockOrUnlockPool','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[],'name':'name','outputs':[{'internalType':'string','name':'','type':'string'}],'stateMutability':'pure','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'removeController','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'removeOperator','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[],'name':'renounceAdmin','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[],'name':'renounceController','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[],'name':'renounceOperator','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'address','name':'account','type':'address'}],'name':'renounceRole','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'address','name':'account','type':'address'}],'name':'revokeRole','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes32','name':'accountHash','type':'bytes32'},{'internalType':'enum GluwaInvestmentModel.AccountState','name':'state','type':'uint8'}],'name':'setAccountState','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[],'name':'settings','outputs':[{'internalType':'uint32','name':'','type':'uint32'},{'internalType':'contract IERC20Upgradeable','name':'','type':'address'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes4','name':'interfaceId','type':'bytes4'}],'name':'supportsInterface','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'version','outputs':[{'internalType':'string','name':'','type':'string'}],'stateMutability':'pure','type':'function'},{'inputs':[{'internalType':'bytes32[]','name':'balanceHashList','type':'bytes32[]'}],'name':'withdrawBalances','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes32[]','name':'balanceHashList','type':'bytes32[]'},{'internalType':'address','name':'ownerAddress','type':'address'},{'internalType':'uint256','name':'fee','type':'uint256'}],'name':'withdrawBalancesFor','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'bytes32[]','name':'balanceHashList','type':'bytes32[]'},{'internalType':'address','name':'recipient','type':'address'},{'internalType':'uint256','name':'fee','type':'uint256'}],'name':'withdrawUnclaimedMatureBalances','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes32[]','name':'balanceHashList','type':'bytes32[]'}],'name':'withdrawUnstartedBalances','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'},{'internalType':'bytes32[]','name':'balanceHashList','type':'bytes32[]'},{'internalType':'uint256','name':'fee','type':'uint256'}],'name':'withdrawUnstartedBalancesFor','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'nonpayable','type':'function'}]";
        public const string TRANSFER_ABI = "[{'constant':true,'inputs':[],'name':'name','outputs':[{'name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_upgradedAddress','type':'address'}],'name':'deprecate','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'_spender','type':'address'},{'name':'_value','type':'uint256'}],'name':'approve','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'deprecated','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_evilUser','type':'address'}],'name':'addBlackList','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'totalSupply','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_from','type':'address'},{'name':'_to','type':'address'},{'name':'_value','type':'uint256'}],'name':'transferFrom','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'upgradedAddress','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'name':'','type':'address'}],'name':'balances','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'decimals','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'maximumFee','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'_totalSupply','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[],'name':'unpause','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'name':'_maker','type':'address'}],'name':'getBlackListStatus','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'name':'','type':'address'},{'name':'','type':'address'}],'name':'allowed','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'paused','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'name':'who','type':'address'}],'name':'balanceOf','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[],'name':'pause','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'getOwner','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'owner','outputs':[{'name':'','type':'address'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'symbol','outputs':[{'name':'','type':'string'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_to','type':'address'},{'name':'_value','type':'uint256'}],'name':'transfer','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'newBasisPoints','type':'uint256'},{'name':'newMaxFee','type':'uint256'}],'name':'setParams','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'amount','type':'uint256'}],'name':'issue','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'amount','type':'uint256'}],'name':'redeem','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[{'name':'_owner','type':'address'},{'name':'_spender','type':'address'}],'name':'allowance','outputs':[{'name':'remaining','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[],'name':'basisPointsRate','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'name':'','type':'address'}],'name':'isBlackListed','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_clearedUser','type':'address'}],'name':'removeBlackList','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'MAX_UINT','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'newOwner','type':'address'}],'name':'transferOwnership','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':false,'inputs':[{'name':'_blackListedUser','type':'address'}],'name':'destroyBlackFunds','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'inputs':[{'name':'_initialSupply','type':'uint256'},{'name':'_name','type':'string'},{'name':'_symbol','type':'string'},{'name':'_decimals','type':'uint256'}],'payable':false,'stateMutability':'nonpayable','type':'constructor'},{'anonymous':false,'inputs':[{'indexed':false,'name':'amount','type':'uint256'}],'name':'Issue','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'amount','type':'uint256'}],'name':'Redeem','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'newAddress','type':'address'}],'name':'Deprecate','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'feeBasisPoints','type':'uint256'},{'indexed':false,'name':'maxFee','type':'uint256'}],'name':'Params','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'_blackListedUser','type':'address'},{'indexed':false,'name':'_balance','type':'uint256'}],'name':'DestroyedBlackFunds','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'_user','type':'address'}],'name':'AddedBlackList','type':'event'},{'anonymous':false,'inputs':[{'indexed':false,'name':'_user','type':'address'}],'name':'RemovedBlackList','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'name':'owner','type':'address'},{'indexed':true,'name':'spender','type':'address'},{'indexed':false,'name':'value','type':'uint256'}],'name':'Approval','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'name':'from','type':'address'},{'indexed':true,'name':'to','type':'address'},{'indexed':false,'name':'value','type':'uint256'}],'name':'Transfer','type':'event'},{'anonymous':false,'inputs':[],'name':'Pause','type':'event'},{'anonymous':false,'inputs':[],'name':'Unpause','type':'event'}]";
        public const string USDCG_ABI = @"[{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'owner','type':'address'},{'indexed':true,'internalType':'address','name':'spender','type':'address'},{'indexed':false,'internalType':'uint256','name':'value','type':'uint256'}],'name':'Approval','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'_burnFrom','type':'address'},{'indexed':false,'internalType':'uint256','name':'_value','type':'uint256'}],'name':'Burnt','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'_mintTo','type':'address'},{'indexed':false,'internalType':'uint256','name':'_value','type':'uint256'}],'name':'Mint','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'bytes32','name':'role','type':'bytes32'},{'indexed':true,'internalType':'bytes32','name':'previousAdminRole','type':'bytes32'},{'indexed':true,'internalType':'bytes32','name':'newAdminRole','type':'bytes32'}],'name':'RoleAdminChanged','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'bytes32','name':'role','type':'bytes32'},{'indexed':true,'internalType':'address','name':'account','type':'address'},{'indexed':true,'internalType':'address','name':'sender','type':'address'}],'name':'RoleGranted','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'bytes32','name':'role','type':'bytes32'},{'indexed':true,'internalType':'address','name':'account','type':'address'},{'indexed':true,'internalType':'address','name':'sender','type':'address'}],'name':'RoleRevoked','type':'event'},{'anonymous':false,'inputs':[{'indexed':true,'internalType':'address','name':'from','type':'address'},{'indexed':true,'internalType':'address','name':'to','type':'address'},{'indexed':false,'internalType':'uint256','name':'value','type':'uint256'}],'name':'Transfer','type':'event'},{'inputs':[],'name':'DEFAULT_ADMIN_ROLE','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'RELAYER_ROLE','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'WRAPPER_ROLE','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'owner','type':'address'},{'internalType':'address','name':'spender','type':'address'}],'name':'allowance','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'spender','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'}],'name':'approve','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'balanceOf','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'uint256','name':'amount','type':'uint256'}],'name':'burn','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'burner','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'},{'internalType':'uint256','name':'fee','type':'uint256'},{'internalType':'uint256','name':'nonce','type':'uint256'},{'internalType':'bytes','name':'sig','type':'bytes'}],'name':'burn','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'spender','type':'address'},{'internalType':'uint256','name':'subtractedValue','type':'uint256'}],'name':'decreaseAllowance','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'sender','type':'address'},{'internalType':'uint256','name':'nonce','type':'uint256'}],'name':'execute','outputs':[{'internalType':'bool','name':'success','type':'bool'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'sender','type':'address'},{'internalType':'uint256','name':'nonce','type':'uint256'}],'name':'getReservation','outputs':[{'internalType':'uint256','name':'amount','type':'uint256'},{'internalType':'uint256','name':'fee','type':'uint256'},{'internalType':'address','name':'recipient','type':'address'},{'internalType':'address','name':'executor','type':'address'},{'internalType':'uint256','name':'expiryBlockNum','type':'uint256'},{'internalType':'enum ERC20Reservable.ReservationStatus','name':'status','type':'uint8'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'}],'name':'getRoleAdmin','outputs':[{'internalType':'bytes32','name':'','type':'bytes32'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'uint256','name':'index','type':'uint256'}],'name':'getRoleMember','outputs':[{'internalType':'address','name':'','type':'address'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'}],'name':'getRoleMemberCount','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'address','name':'account','type':'address'}],'name':'grantRole','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'address','name':'account','type':'address'}],'name':'hasRole','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'spender','type':'address'},{'internalType':'uint256','name':'addedValue','type':'uint256'}],'name':'increaseAllowance','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'uint256','name':'amount','type':'uint256'}],'name':'mint','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'minter','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'},{'internalType':'uint256','name':'fee','type':'uint256'},{'internalType':'uint256','name':'nonce','type':'uint256'},{'internalType':'bytes','name':'sig','type':'bytes'}],'name':'mint','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[],'name':'name','outputs':[{'internalType':'string','name':'','type':'string'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'sender','type':'address'},{'internalType':'uint256','name':'nonce','type':'uint256'}],'name':'reclaim','outputs':[{'internalType':'bool','name':'success','type':'bool'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'address','name':'account','type':'address'}],'name':'renounceRole','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'sender','type':'address'},{'internalType':'address','name':'recipient','type':'address'},{'internalType':'address','name':'executor','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'},{'internalType':'uint256','name':'fee','type':'uint256'},{'internalType':'uint256','name':'nonce','type':'uint256'},{'internalType':'uint256','name':'expiryBlockNum','type':'uint256'},{'internalType':'bytes','name':'sig','type':'bytes'}],'name':'reserve','outputs':[{'internalType':'bool','name':'success','type':'bool'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'reservedBalanceOf','outputs':[{'internalType':'uint256','name':'amount','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'bytes32','name':'role','type':'bytes32'},{'internalType':'address','name':'account','type':'address'}],'name':'revokeRole','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'bytes4','name':'interfaceId','type':'bytes4'}],'name':'supportsInterface','outputs':[{'internalType':'bool','name':'','type':'bool'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'symbol','outputs':[{'internalType':'string','name':'','type':'string'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'token','outputs':[{'internalType':'contract IERC20Upgradeable','name':'','type':'address'}],'stateMutability':'view','type':'function'},{'inputs':[],'name':'totalSupply','outputs':[{'internalType':'uint256','name':'','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'address','name':'sender','type':'address'},{'internalType':'address','name':'recipient','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'},{'internalType':'uint256','name':'fee','type':'uint256'},{'internalType':'uint256','name':'nonce','type':'uint256'},{'internalType':'bytes','name':'sig','type':'bytes'}],'name':'transfer','outputs':[{'internalType':'bool','name':'success','type':'bool'}],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'recipient','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'}],'name':'transfer','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'sender','type':'address'},{'internalType':'address','name':'recipient','type':'address'},{'internalType':'uint256','name':'amount','type':'uint256'}],'name':'transferFrom','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[{'internalType':'address','name':'account','type':'address'}],'name':'unreservedBalanceOf','outputs':[{'internalType':'uint256','name':'amount','type':'uint256'}],'stateMutability':'view','type':'function'},{'inputs':[{'internalType':'string','name':'name','type':'string'},{'internalType':'string','name':'symbol','type':'string'},{'internalType':'uint8','name':'decimals_','type':'uint8'},{'internalType':'address','name':'admin','type':'address'},{'internalType':'contract IERC20Upgradeable','name':'token','type':'address'}],'name':'initialize','outputs':[],'stateMutability':'nonpayable','type':'function'},{'inputs':[],'name':'decimals','outputs':[{'internalType':'uint8','name':'','type':'uint8'}],'stateMutability':'view','type':'function'}]";


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
                return "{\"DrawId\":\"2E388C83-AA64-457E-A3A3-AABCCE18936F\"}";
            }
            else if (environment == "Staging")
            {
                return "{\"DrawId\":\"420BCB00-4B83-4A50-AAD5-90A17DDB90D8\"}";
            }
            else
                return "{\"DrawId\":\"E598F483-60D4-4D42-AEEE-ECC86CB3D985\"}";  // Production
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
                return "mitYWxozKm9adcMTXfeUnoYcgLrteXbbme";
            }
            else
                return "1MLgsLW16y997tq1ZRQKK9oin9WrqKAg8A";
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
                    return "9EB9C6CF-A16F-4DCE-BB0A-0791D070E7B1";
                }
                else if (environment == "Staging")
                {
                    return "AD2159B7-494A-4BAF-81A0-0ADADA0B2724";
                }
                else if (environment == "Production")
                {
                    return "9c3ced50-088c-4544-bf52-0da106ec1bf6";
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
                    return "b9715096-313b-4165-9cda-91967889678e";
                }
                else if (environment == "Staging")
                {
                    return "9255B9BC-02AF-425B-909A-0109ADBBD114";
                }
                else if (environment == "Production")
                {
                    return "9c3ced50-088c-4544-bf52-0da106ec1bf6";
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
                    return "C5B98013-E5B7-431E-AF89-5E4F48B53284";
                }
                else if (environment == "Staging")
                {
                    return "9255B9BC-02AF-425B-909A-0109ADBBD114";
                }
                else if (environment == "Production")
                {
                    return "45c57dec-8d89-48b4-a393-f609b4ccea1b";
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
                    return "66a93238-ac92-4f8b-947f-fb555c5990aa";
                }
                else if (environment == "Staging")
                {
                    return "9EB9C6CF-A16F-4DCE-BB0A-0791D070E7B1";
                }
                else if (environment == "Production")
                {
                    return "45c57dec-8d89-48b4-a393-f609b4ccea1b";
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
                    return "2451ECD8-C1A8-4EB5-BC26-1920342C4F6F";
                case "Staging":
                    return "F492F6B7-3BAE-4F00-9B80-C545035B9409";
                //case "Production":
                //return "0x4396a1b1d82e05ea6b0b4ae168030be135a36cf84a0affc12230b03b04788f11";
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
