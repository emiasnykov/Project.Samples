using Azure.Identity;
using Azure.Storage.Blobs;
using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Models;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using NBitcoin;
using Nethereum.ABI;
using Nethereum.ABI.ABIDeserialisation;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.NonceServices;
using Nethereum.RPC.TransactionManagers;
using Nethereum.Signer;
using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using NUnit.Framework;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GluwaAPI.TestEngine.Utils
{
    public class SignaturesUtil : AmountUtils
    {
        // Web3 URLs
        private const string URL = "http://baas-rpc.luniverse.io:8545?l***********************";
        private const string URL_MAINNET = "http://baas-rpc.luniverse.io:8545?lChainId=***********************";
        private const string URL_ETH = "https://goerli.infura.io/v3/***********************";
        private const string URL_ETH_MAINNET = "https://mainnet.infura.io/v3/***********************";
        private const string URL_INFURA_RPC = "https://goerli.infura.io/v3/***********************";
        private const string URL_INFURA_RPC_MAINNET = "https://mainnet.infura.io/v3/***********************";
        private const string URL_BINANCE_RPC = "https://data-seed-prebsc-1-s1.binance.org";
        private const string URL_LUNIVERSE_RPC = "https://baas-rpc.luniverse.io";

        // Contract Addresses
        private const string BURN_FROM_ADDRESS = "***********************";
        private const string BURN_FROM_ADDRESS_MAINNET = "***********************";
        private const string FTA_ADDRESS = "***********************";
        private const string FTA_ADDRESS_MAINNET = "***********************";

        // Gas limit constants
        public static readonly BigInteger TRANSFER_GAS_LIMIT = new BigInteger(550_000);
        public static readonly BigInteger RESERVE_GAS_LIMIT = new BigInteger(550_000);
        public static readonly BigInteger RECLAIM_GAS_LIMIT = new BigInteger(500_000);
        public static readonly BigInteger EXECUTE_GAS_LIMIT = new BigInteger(550_000);
        public static readonly BigInteger FTA_GAS_LIMIT = new BigInteger(2_200_000);
        public const long INCLUDE_DECIMAL_CONVERSION = 1_000_000; // 6 decimals for USDC
        public const long INCLUDE_DECIMAL_CONVERSION_BSC = 1_000_000_000_000_000_000; // 18 decimals for BNB (USDC)
        public static readonly BigInteger ETH_GAS_LIMIT = 21000;
        private static HexBigInteger gas;
        private static HexBigInteger gasPrice;

        /// <summary>
        /// Create Btc Signatures for exchange acceptance
        /// </summary>
        /// <param name="order"></param>
        /// <param name="privateKey"></param>
        /// <param name="callerName"></param>
        /// <returns></returns>
        public static CreateSignatureResponse CreateBtcExchangeSignature(dynamic order, AddressItem address, [CallerMemberName] string callerName = "")
        {
            // Check whether test or main
            AssertionHandlers.Assertions.ExchangeSetUpMethod = callerName;

            // Arange signature body for test
            var body = new
            {
                Currency = "BTC",
                SenderPrivateKey = address.PrivateKey,
                DestinationAddress = order.DestinationAddress,
                Amount = order.SourceAmount,
                Fee = order.Fee,
                ReservedFundsAddress = order.ReservedFundsAddress,
                ReservedFundsRedeemScript = order.ReservedFundsRedeemScript
            };

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetWebhookReceiver("Signature"),
                                        Api.SendRequest(Method.POST, body));
            // Assert
            CheckUnspentOutputs(response.Content, address.Address);
            AssertionHandlers.Assertions.HandleSetupAssertions(200, response);
            CreateSignatureResponse quoteSignatures = JsonConvert.DeserializeObject<CreateSignatureResponse>(response.Content);

            return quoteSignatures;
        }

        /// <summary>
        /// Createst the transfer signature for BTC needed in the request
        /// </summary>
        /// <param name="fee">GetFee</param>
        /// <param name="amount"></param>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="fromPrivateKey"></param>
        /// <returns></returns>
        public static string CreateBtcTransferSignature(string fee,
                                                        string amount,
                                                        string fromAddress,
                                                        string toAddress,
                                                        string fromPrivateKey)
        {
            Money parsedAmount = Money.Parse(amount);
            Money parsedFee = Money.Parse(fee);
            Network network = Setup.GluwaTestApi.CurrentNetwork;
            var bitcoinPrivateKey = new BitcoinSecret(fromPrivateKey, network);
            var client = new QBitNinjaClient("https://tapi.qbit.ninja/", network);
            BalanceModel unspentOutputs = new BalanceModel();
            try
            {
                unspentOutputs = client.GetBalance(new BitcoinPubKeyAddress(fromAddress, network), true).Result;
            }
            catch (Exception err)
            {
                Assert.Ignore($"Failed to get unspent outputs for {fromPrivateKey}: {err}");
            }
            var largestUnspentOutput = unspentOutputs.Operations.OrderByDescending(ou => ou.Amount).FirstOrDefault();
            Money unspentOutputAmount = Money.Satoshis(largestUnspentOutput.Amount);

            if (unspentOutputAmount < parsedAmount + parsedFee)
            {
                Assert.Ignore($"The address {fromAddress} has no unspent output large enough to fulfill the transaction.");
            }

            GetTransactionResponse transactionResponse = new GetTransactionResponse();
            try
            {
                transactionResponse = client.GetTransaction(largestUnspentOutput.TransactionId).Result;
            }
            catch (Exception err)
            {
                Assert.Ignore($"Failed to get transaction details for {largestUnspentOutput.TransactionId}: {err}");
            }

            var receivedCoins = transactionResponse.ReceivedCoins;
            OutPoint outPointToSpend = null;
            Script scriptPubKey = bitcoinPrivateKey.GetAddress(ScriptPubKeyType.Legacy).ScriptPubKey;

            foreach (var coin in receivedCoins)
            {
                if (coin.TxOut.ScriptPubKey == scriptPubKey)
                {
                    outPointToSpend = coin.Outpoint;
                }
            }
            if (outPointToSpend == null)
                throw new Exception("TxOut doesn't contain our ScriptPubKey");
            Console.WriteLine("We want to spend {0}. outpoint:", outPointToSpend.N);
            var outputAmount = receivedCoins[(int)outPointToSpend.N].Amount.ToString();
            BitcoinAddress sourceAddress = BitcoinAddress.Create(fromAddress, network);
            BitcoinAddress targetAddress = BitcoinAddress.Create(toAddress, network);
            BitcoinSecret secret = new BitcoinSecret(fromPrivateKey, network);
            TransactionBuilder builder = network.CreateTransactionBuilder();
            NBitcoin.Transaction txn = builder
                            .AddKeys(secret)
                            .AddCoins(
                                new Coin(
                                    fromTxHash: new uint256(largestUnspentOutput.TransactionId),
                                    fromOutputIndex: (outPointToSpend.N),
                                    amount: outputAmount,
                                    scriptPubKey: scriptPubKey
                                )
                            )
                            .Send(targetAddress, parsedAmount)
                            .SetChange(sourceAddress)
                            .SendFees(parsedFee)
                            .BuildTransaction(true);

            string signature = txn.ToHex();
            builder.Verify(txn, out NBitcoin.Policy.TransactionPolicyError[] errors);

            return signature;
        }

        /// <summary>
        /// Returns generated X Request signature 
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string GetXRequestSignature(string privateKey)
        {
            string xRequestSignature;
            long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string message = unixTimestamp.ToString();
            bool bGluwacoin = privateKey.StartsWith("0x");

            if (bGluwacoin == true)
            {
                // generate X-RequestSignature for gluwacoin (USD-G/KRW-G/NGN-G)
                EthereumMessageSigner signer = new EthereumMessageSigner();
                string signedMessage = signer.Sign(Encoding.ASCII.GetBytes(message), privateKey);

                byte[] plainTextBytes = Encoding.ASCII.GetBytes(message + "." + signedMessage);
                xRequestSignature = Convert.ToBase64String(plainTextBytes);

            }
            else
            {
                // for btc check if it's in main or test
                Network network = Setup.GluwaTestApi.CurrentNetwork;

                // generate XRequestSignature for BTC
                BitcoinSecret secret = new BitcoinSecret(privateKey, network);
                string signedMessage = secret.PrivateKey.SignMessage(message);

                byte[] plainTextBytes = Encoding.ASCII.GetBytes($"{message}.{signedMessage}");
                xRequestSignature = Convert.ToBase64String(plainTextBytes);

            }

            return xRequestSignature;
        }

        /// <summary>
        /// Generates a signature with a message
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string GenerateMessageSignature(string message, string privateKey)
        {
            bool bGluwacoin = privateKey.StartsWith("0x");

            if (bGluwacoin)
            {
                // Eth based addresses
                EthereumMessageSigner signer = new EthereumMessageSigner();
                string signature = signer.EncodeUTF8AndSign(message, new EthECKey(privateKey));

                return signature;
            }
            else
            {
                // Bitcoin addresses
                Network network = Setup.GluwaTestApi.CurrentNetwork;
                BitcoinSecret secret = network.CreateBitcoinSecret(privateKey);
                string signature = secret.PrivateKey.SignMessage(Encoding.UTF8.GetBytes(message), false);

                return signature;
            }
        }

        /// <summary>
        /// Create Gluwacoin reserved txn signature for quotes or orders
        /// </summary>
        /// <param name="sendingAddress"></param>
        /// <param name="privateKey"></param>
        /// <param name="contractAddress"></param>
        /// <param name="factor"></param>
        /// <param name="quoteOrOrder"></param>
        /// <returns></returns>
        public static GluwacoinSignature GetGluwacoinReservedTxnSignature(string sendingAddress, string privateKey, dynamic quoteOrOrder, string environment, ECurrency currency)
        {
            var contractAddressItem = TransferFunctions.GetContractAddress(environment, currency.ToString());
            decimal precisionFactor = (decimal)Math.Pow(10, contractAddressItem.Decimals);
            BigInteger amount = (BigInteger)(decimal.Parse(quoteOrOrder.SourceAmount) * precisionFactor);
            BigInteger fee = (BigInteger)(decimal.Parse(quoteOrOrder.Fee) * precisionFactor);
            BigInteger nonce = BigInteger.Parse(GetNonce());
            long expiryBlockNumber = long.Parse(quoteOrOrder.ExpiryBlockNumber);

            ABIEncode abiEncode = new ABIEncode();
            var hash = abiEncode.GetSha3ABIEncodedPacked(
                new ABIValue("address", contractAddressItem.Address),
                new ABIValue("address", sendingAddress), // source
                new ABIValue("address", quoteOrOrder.DestinationAddress), // target
                new ABIValue("address", quoteOrOrder.Executor), // executor
                new ABIValue("uint256", amount), // amount - 50000000000000000000 = 50 / 50000000000000000 = 0.05
                new ABIValue("uint256", fee), // fee - 2000000000000000000000 = 2000 / 2000000000000000000 = 2
                new ABIValue("uint256", nonce), // nonce
                new ABIValue("uint256", expiryBlockNumber) // expiryblocknum
                );

            var msgSigner = new EthereumMessageSigner();
            string signature = msgSigner.Sign(hash, privateKey);

            GluwacoinSignature gluwaSignatures = new GluwacoinSignature()
            {
                ReserveTxnSignature = signature,
                Nonce = nonce.ToString()
            };

            return gluwaSignatures;
        }

        /// <summary>
        /// Generate Gluwacoin Txn Signature for transfer api tests
        /// Peg process
        /// </summary>
        /// <param name="toAddress">Address used to send txn</param>
        /// <param name="fromAddress">Target address</param>
        /// <param name="contractAddress"></param>
        /// <param name="fee">Txn Fee </param>
        /// <param name="amount">Txn Amount</param>
        /// <param name="nonce">Nonce accounted precision factor</param>
        /// <returns></returns>
        public static (string, string) GetGluwacoinTxnSignature(
                                                     string fromAddress,
                                                     string fromPrivateKey,
                                                     string toAddress,
                                                     string fee,
                                                     string amount,
                                                     string currency,
                                                     string environment)
        {
            ContractAddressItem contract = TransferFunctions.GetContractAddress(environment, currency);
            BigInteger feeBig = ConvertToGluwacoinPrecisionFactor(fee, contract.Decimals);
            BigInteger amountBig = ConvertToGluwacoinPrecisionFactor(amount, contract.Decimals);
            string nonce = GetNonce();
            BigInteger nonceBig = BigInteger.Parse(nonce);
            var abiEncode = new ABIEncode();
            byte[] hash;

            if (currency == "Usdcg" && environment == "Test")
            {
                hash = abiEncode.GetSha3ABIEncodedPacked(
                    new ABIValue("uint8", 3), // domain id
                    new ABIValue("uint256", 5), // chain id
                    new ABIValue("address", contract.Address), //contract
                    new ABIValue("address", fromAddress), //source
                    new ABIValue("address", toAddress), //target
                    new ABIValue("uint256", amountBig), //amount
                    new ABIValue("uint256", feeBig), //fee
                    new ABIValue("uint256", nonceBig) //nonce
                );
            }
            else if (currency == "sSgDg" && environment == "Test")
            {
                hash = abiEncode.GetSha3ABIEncodedPacked(
                    new ABIValue("uint8", (short)3), // domain id
                    new ABIValue("uint256", BigInteger.Parse(Shared.CHAIN_ID_GOERLI_TEST)), // chain id
                    new ABIValue("address", contract.Address), //contract
                    new ABIValue("address", fromAddress), //source
                    new ABIValue("address", toAddress), //target
                    new ABIValue("uint256", amountBig), //amount
                    new ABIValue("uint256", feeBig), //fee
                    new ABIValue("uint256", nonceBig) //nonce
                );
            }
            else if (currency == "Usdcg" && environment != "Test")
            {
                hash = abiEncode.GetSha3ABIEncodedPacked(
                    new ABIValue("uint8", 3), // domain id
                    new ABIValue("uint256", 1), // chain id
                    new ABIValue("address", contract.Address), //contract
                    new ABIValue("address", fromAddress), //source
                    new ABIValue("address", toAddress), //target
                    new ABIValue("uint256", amountBig), //amount
                    new ABIValue("uint256", feeBig), //fee
                    new ABIValue("uint256", nonceBig) //nonce
                );
            }
            else if (currency == "sSgDg" && environment != "Test")
            {
                hash = abiEncode.GetSha3ABIEncodedPacked(
                    new ABIValue("uint8", (short)3), // domain id
                    new ABIValue("uint256", BigInteger.Parse(Shared.CHAIN_ID_MAIN)), // chain id
                    new ABIValue("address", contract.Address), //contract
                    new ABIValue("address", fromAddress), //source
                    new ABIValue("address", toAddress), //target
                    new ABIValue("uint256", amountBig), //amount
                    new ABIValue("uint256", feeBig), //fee
                    new ABIValue("uint256", nonceBig) //nonce
                );
            }
            else
            {
                hash = abiEncode.GetSha3ABIEncodedPacked(
                    new ABIValue("address", contract.Address), //contract
                    new ABIValue("address", fromAddress), //source
                    new ABIValue("address", toAddress), //target
                    new ABIValue("uint256", amountBig), //amount
                    new ABIValue("uint256", feeBig), //fee
                    new ABIValue("uint256", nonceBig) //nonce
                );
            }

            var msgSigner = new EthereumMessageSigner();
            string signature = msgSigner.Sign(hash, fromPrivateKey);
            TestContext.WriteLine($"address {msgSigner.EcRecover(hash, signature)}");
            return (signature, nonce);
        }

        /// <summary>
        /// Get unpeg allowance signature
        /// Unpeg process
        /// </summary>
        /// <param name="approvePrivateKey"></param>
        /// <param name="spenderAddress"></param>
        /// <param name="contractAddress"></param>
        /// <param name="sidechainABI"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string GetAllowanceSignature(ECurrency currency, string approvePrivateKey, string sidechain, string amount, string environment)
        {
            string urlBurn;
            string burnFromAddress;

            if (Shared.isTestEnvironment(environment))
            {
                urlBurn = URL;
                burnFromAddress = BURN_FROM_ADDRESS;
            }
            else
            {
                urlBurn = URL_MAINNET;
                burnFromAddress = BURN_FROM_ADDRESS_MAINNET;
            }

            Account approverAccount = new Account(approvePrivateKey);
            var web3 = new Web3(urlBurn);
            var contractAddressItem = TransferFunctions.GetContractAddress(environment, currency.ToString());
            BigInteger amountBig = ConvertToGluwacoinPrecisionFactor(amount, contractAddressItem.Decimals);
            Contract contract = web3.Eth.GetContract(sidechain, contractAddressItem.Address);
            Function contractFunction = contract.GetFunction("approve");

            TransactionInput input = contractFunction.CreateTransactionInput(approverAccount.Address,
                new HexBigInteger(TRANSFER_GAS_LIMIT),
                new HexBigInteger(GetSafeGasPrice()),
                new HexBigInteger(BigInteger.Zero),
                burnFromAddress,
                amountBig);

            input.Nonce = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(approverAccount.Address).Result;
            string signedTxn = new AccountOfflineTransactionSigner().SignTransaction(approverAccount, input);

            return signedTxn;
        }

        /// <summary>
        /// Coin Transfer Signature 
        /// CTC, USDC, USDT
        /// </summary>
        /// <param name="addressSource"></param>
        /// <param name="addressTarget"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string CreateCoinTransferSignature(AddressItem addressSource,
                                                        string addressTarget,
                                                        decimal amount,
                                                        ECurrency currency,
                                                        string environment)
        {
            BigInteger amount_big;
            string url; string contractABI;

            if (Shared.isTestEnvironment(environment))
            {
                url = URL_ETH;
            }
            else
            {
                url = URL_ETH_MAINNET;
            }

            if (currency.ToString() == "Usdc" || currency.ToString() == "Usdt")
            {
                amount_big = ConvertToGluwacoinBigInteger(amount.ToString(), int.Parse("6"));
            }
            else
            {
                amount_big = ConvertToGluwacoinBigInteger(amount.ToString(), int.Parse("18"));
            }


            if (currency.ToString() == "Usdc" || currency.ToString() == "Usdt" || currency.ToString() == "Gcre")
            {
                contractABI = Shared.TRANSFER_ABI;
            }
            else
            {
                contractABI = TransferFunctions.GetContractAbi(currency.ToString().ToUpper(), "MainNet");  
            }

            var account = new Account(addressSource.PrivateKey);
            var web3 = new Web3(account, url);
            var contractAddress = TransferFunctions.GetContractAddress(environment, currency.ToString());
            Contract contract = web3.Eth.GetContract(contractABI, contractAddress.Address);                  
            Function contractFunction = contract.GetFunction("transfer");

            TransactionInput input = contractFunction.CreateTransactionInput(addressSource.Address,
                new HexBigInteger(GetSafeGasPrice(currency.ToString()).Item2),
                new HexBigInteger(GetSafeGasPrice(currency.ToString()).Item1),
                new HexBigInteger(BigInteger.Zero),
                addressTarget,                           // Receiver address
                amount_big);

            input.Nonce = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(account.Address).Result;
            string signedTxn = new AccountOfflineTransactionSigner().SignTransaction(account, input);

            return ("0x" + signedTxn);
        }

        /// <summary>
        /// Coin Transfer Signature obsolete Netherium 3.8
        /// CTC, USDC, USDT
        /// </summary>
        /// <param name="addressSource"></param>
        /// <param name="addressTarget"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static async Task<string> CreateCoinTransferSignatureAsync(AddressItem addressSource, string addressTarget, decimal amount, ECurrency currency, string environment)
        {
            BigInteger amount_big;
            string urlEth;

            if (Shared.isTestEnvironment(environment))
            {
                urlEth = URL_ETH;
            }
            else
            {
                urlEth = URL_ETH_MAINNET;
            }

            var account = new Account(addressSource.PrivateKey);
            var web3 = new Web3(account, urlEth);
            var contractAddress = TransferFunctions.GetContractAddress(environment, currency.ToString());
            var transferHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();

            if (currency.ToString() == "Usdc" || currency.ToString() == "Usdt")
            {
                amount_big = ConvertToGluwacoinBigInteger(amount.ToString(), int.Parse("6"));
            }
            else
            {
                amount_big = ConvertToGluwacoinBigInteger(amount.ToString(), int.Parse("18"));
            }

            var transfer = new TransferFunction()
            {
                To = addressTarget,
                SendAmount = amount_big
            };

            HexBigInteger gasPriceInWei = new HexBigInteger(GetSafeGasPrice());
            transfer.GasPrice = gasPriceInWei;
            account.NonceService = new InMemoryNonceService(account.Address, web3.Client);
            var futureNonce = await account.NonceService.GetNextNonceAsync();
            transfer.Nonce = futureNonce;
            var signature = await transferHandler.SignTransactionAsync(contractAddress.Address.ToString(), transfer);

            return signature;
        }


        /// <summary>
        /// Ether Transfer Signature
        /// ETH
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="target"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string CreateEtherTransferSignature(string privateKey, string target, decimal amount, ECurrency currency, string environment)
        {
            string urlEth;

            if (Shared.isTestEnvironment(environment))
            {
                urlEth = URL_ETH;
            }
            else
            {
                urlEth = URL_ETH_MAINNET;
            }

            var account = new Account(privateKey);
            var web3 = new Web3(account, urlEth);
            var toAddress = target;
            var transactionManager = web3.TransactionManager;
            var fromAddress = transactionManager.Account.Address;

            // TODO: replace hardcoded values with real ones 
            BigInteger gasLimit = ETH_GAS_LIMIT;
            var gasPrice = Web3.Convert.FromWei(GetSafeGasPrice(), UnitConversion.EthUnit.Gwei);
            account.NonceService = new InMemoryNonceService(account.Address, web3.Client);
            var nonce = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(account.Address).Result;
            TransactionInput transactionInput = EtherTransferTransactionInputBuilder.CreateTransactionInput(fromAddress, toAddress, amount, gasPrice, gasLimit, nonce);
            string signedTxn = new AccountOfflineTransactionSigner().SignTransaction(account, transactionInput);

            return signedTxn;
        }

        /// <summary>
        /// Get allowance Txn signature
        /// Fixed-Term Interest Account
        /// </summary>
        /// <param name="approverPrivateKey"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string GetAllowanceSignature(string approverPrivateKey, string amount, string type, string environment)
        {
            string url;
            string accountAddress;
            string sidechainABI = TransferFunctions.GetContractAbi("Usd", "Sidechain");

            if (Shared.isTestEnvironment(environment))
            {
                url = URL;
                accountAddress = TransferFunctions.GetAccountAddress(type, environment);
            }
            else
            {
                url = URL_MAINNET;
                accountAddress = TransferFunctions.GetAccountAddress(type, environment);
            }

            Account approverAccount = new Account(approverPrivateKey);
            Web3 web3 = new Web3(url);
            ContractAddressItem contractsUSDCG = TransferFunctions.GetContractAddress(environment, ECurrency.sUsdcg.ToString());
            Contract contract = web3.Eth.GetContract(sidechainABI, contractsUSDCG.Address);
            BigInteger amount_big = ConvertToGluwacoinBigInteger(amount, int.Parse("6"));
            Function contractFunction = contract.GetFunction("increaseAllowance");

            TransactionInput input = contractFunction.CreateTransactionInput(approverAccount.Address,
                new HexBigInteger(TRANSFER_GAS_LIMIT),
                new HexBigInteger(GetSafeGasPrice()),
                new HexBigInteger(BigInteger.Zero),
                accountAddress,
                amount_big);

            input.Nonce = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(approverAccount.Address).Result;
            string signedTxn = new AccountOfflineTransactionSigner().SignTransaction(approverAccount, input);

            return signedTxn;
        }

        /// <summary>
        /// Get approve Txn signature
        /// Fixed-Term Interest Account
        /// </summary>
        /// <param name="approverPrivateKey"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string GetFtaApproveSignature(AddressItem addressSource, string amount, string environment, ECurrency currency = ECurrency.Usdc)
        {
            CurrencyItem currencyInfo = TransferFunctions.GetCurrencyInfo(currency, environment);
            Account approverAccount = null;
            string signedTxn = "";
            Web3 web3 = new Web3();
            string ftaContract = "";

            if (currency.Equals(ECurrency.Usdc) && (environment.Equals("Test")))
            {
                web3 = new Web3(URL_INFURA_RPC);
                ftaContract = FTA_ADDRESS;
                gas = new HexBigInteger(GetSafeGasPrice(currency.ToString()).Item2);
                gasPrice = new HexBigInteger(GetSafeGasPrice(currency.ToString()).Item1);
                approverAccount = new Account(addressSource.PrivateKey);
            }
            else if (currency.Equals(ECurrency.Usdc) && (environment.Equals("Staging") || (environment.Equals("Production"))))
            {
                web3 = new Web3(currencyInfo.nodeUrl);
                ftaContract = "***********************";
                gas = new HexBigInteger(FTA_GAS_LIMIT);
                gasPrice = new HexBigInteger(GetSafeGasPrice());
                approverAccount = new Account(addressSource.PrivateKey);
            }
            else if (currency.Equals(ECurrency.BnbUsdc) && (environment.Equals("Test")))
            {
                web3 = new Web3(currencyInfo.nodeUrl);
                ftaContract = "***********************";
                gas = new HexBigInteger(FTA_GAS_LIMIT);
                gasPrice = new HexBigInteger(GetSafeGasPrice());
                approverAccount = new Account(addressSource.PrivateKey, currencyInfo.chainId);
            }
            else if (currency.Equals(ECurrency.BnbUsdc) && (!environment.Equals("Test")))
            {
                web3 = new Web3(currencyInfo.nodeUrl);
                ftaContract = "***********************";
                gas = new HexBigInteger(FTA_GAS_LIMIT);
                gasPrice = new HexBigInteger(GetSafeGasPrice());
                approverAccount = new Account(addressSource.PrivateKey, currencyInfo.chainId);
            }
            else
            {
                Assert.Fail("Currency not found");
            }

            BigInteger amount_big = ConvertToGluwacoinBigInteger(amount, int.Parse(currencyInfo.numberOfDigits));
            var contractFunction = getFunction("approve", currencyInfo, currency, environment);
            TransactionInput input = contractFunction.CreateTransactionInput(
                approverAccount.Address,
                gas,
                gasPrice,
                new HexBigInteger(BigInteger.Zero),
                ftaContract,
                amount_big);

            input.Nonce = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(approverAccount.Address).Result;
            signedTxn = new AccountOfflineTransactionSigner().SignTransaction(approverAccount, input);

            return ("0x" + signedTxn);
        }


        /// <summary>
        /// Generate Raw Approve Signature
        /// TokenWrapping(Mint) process
        /// </summary>
        /// <param name="ownerPrivateKey"></param>
        /// <param name="amount"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static string GenerateRawApproveSignature(string ownerPrivateKey, BigInteger amount, BigInteger nonce, string environment)
        {
            string urlMint;
            string contractUSDC;
            string sidechainABI = TransferFunctions.GetContractAbi("Usdc", "Sidechain");
            ContractAddressItem contractUSDCG = TransferFunctions.GetContractAddress(environment, ECurrency.Usdcg.ToString());
            Account approverAccount = new Account(ownerPrivateKey);

            if (Shared.isTestEnvironment(environment))
            {
                urlMint = URL_INFURA_RPC;
                contractUSDC = TransferFunctions.GetContractAddress(environment, "USDC").Address;
            }
            else
            {
                urlMint = URL_INFURA_RPC_MAINNET;
                contractUSDC = TransferFunctions.GetContractAddress(environment, "USDC").Address;
            }
            Web3 web3 = new Web3(urlMint);
            Contract contract = web3.Eth.GetContract(sidechainABI, contractUSDC);
            Function contractFunction = contract.GetFunction("approve");

            TransactionInput input = contractFunction.CreateTransactionInput(approverAccount.Address,
                new HexBigInteger(TRANSFER_GAS_LIMIT),
                new HexBigInteger(GetSafeGasPrice()),
                new HexBigInteger(BigInteger.Zero),
                contractUSDCG.Address,
                amount);

            input.Nonce = new HexBigInteger(nonce);
            string signedTxn = new AccountOfflineTransactionSigner().SignTransaction(approverAccount, input);

            return ("0x" + signedTxn);
        }

        /// <summary>
        /// Generate Raw Mint Signature obsolete Netherium 3.8
        /// TokenWrapping(Mint) process
        /// </summary>
        /// <param name="ownerPrivateKey"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static (string, string) GenerateRawMintSignature(string ownerPrivateKey, BigInteger amount, string environment)
        {
            string urlMint;
            ContractAddressItem contractUSDCG = TransferFunctions.GetContractAddress(environment, ECurrency.Usdcg.ToString());
            Account owner = new Account(ownerPrivateKey);

            if (Shared.isTestEnvironment(environment))
            {
                urlMint = URL_INFURA_RPC;
            }
            else
            {
                urlMint = URL_INFURA_RPC_MAINNET;
            }

            Web3 web3 = new Web3(owner, urlMint);
            var mintHandler = web3.Eth.GetContractTransactionHandler<MintFunction>();

            HexBigInteger txCount = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(owner.Address).Result;
            var nonce = txCount.Value + 1;

            MintFunction input = new MintFunction()
            {
                Gas = new HexBigInteger(TRANSFER_GAS_LIMIT),
                GasPrice = new HexBigInteger(GetSafeGasPrice()),
                AmountToSend = new HexBigInteger(BigInteger.Zero),
                Nonce = nonce,
                TokenAmount = amount
            };

            var signature = GenerateRawApproveSignature(ownerPrivateKey, amount, txCount, environment);

            string signedTxn = mintHandler.SignTransactionAsync(
                contractUSDCG.Address,
                input).Result;

            return (signature, "0x" + signedTxn);
        }

        /// <summary>
        /// Generate Raw Mint Signature  Netherium 4.8
        /// TokenWrapping(Mint) process
        /// </summary>
        /// <param name="ownerPrivateKey"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static (string, string) GenerateRawMintSignature(AddressItem addressOwner, BigInteger amount, string environment)
        {
            string urlMint;

            if (Shared.isTestEnvironment(environment))
            {
                urlMint = URL_INFURA_RPC;
            }
            else
            {
                urlMint = URL_INFURA_RPC_MAINNET;
            }

            object[] functionInput = new object[]
            {
                amount
            };

            var account = new Account(addressOwner.PrivateKey);
            var web3 = new Web3(account, urlMint);
            var contractAddress = TransferFunctions.GetContractAddress(environment, ECurrency.Usdcg.ToString());
            Contract contract = web3.Eth.GetContract(Shared.USDCG_ABI, contractAddress.Address);
            Function contractFunction = contract.GetFunction("mint");

            HexBigInteger txCount = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(addressOwner.Address).Result;
            var nonce = txCount.Value + 1;

            TransactionInput transactionInput = contractFunction.CreateTransactionInput(
                from: addressOwner.Address,
                gas: new HexBigInteger(GetSafeGasPrice("Usdcg").Item2),
                gasPrice: new HexBigInteger(GetSafeGasPrice("Usdcg").Item2),
                value: new HexBigInteger(BigInteger.Zero),
                functionInput
            );

            transactionInput.Nonce = new HexBigInteger(nonce);
            string signedTxn = new AccountOfflineTransactionSigner().SignTransaction(account, transactionInput);
            var signature = GenerateRawApproveSignature(addressOwner.PrivateKey, amount, txCount, environment);

            return (signature, "0x" + signedTxn);
        }

        /// <summary>
        /// Get Burn Signature
        /// TokenUnWrapping(Burn) process
        /// </summary>
        /// <param name="sourcePrivateKey"></param>
        /// <param name="amount"></param>
        /// <param name="fee"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static string GetBurnSignature(string sourcePrivateKey,
                                                BigInteger amount,
                                                BigInteger fee,
                                                BigInteger nonce,
                                                string environment)
        {
            if (environment == "Test")
            {
                var contract = TransferFunctions.GetContractAddress(environment, ECurrency.Usdcg.ToString());
                Account account = new Account(sourcePrivateKey);
                ABIEncode abiEncode = new ABIEncode();
                byte[] messageHash = abiEncode.GetSha3ABIEncodedPacked(
                new ABIValue("uint8", 1),                   // Burn
                new ABIValue("uint256", 5),                 // Testnet
                new ABIValue("address", contract.Address),
                new ABIValue("address", account.Address),
                new ABIValue("uint256", amount),
                new ABIValue("uint256", fee),
                new ABIValue("uint256", nonce)
                );

                EthereumMessageSigner signer = new EthereumMessageSigner();
                string signature = signer.Sign(messageHash, sourcePrivateKey);

                return signature;
            }
            else
            {
                var contract = TransferFunctions.GetContractAddress(environment, ECurrency.Usdcg.ToString());
                Account account = new Account(sourcePrivateKey);
                ABIEncode abiEncode = new ABIEncode();
                byte[] messageHash = abiEncode.GetSha3ABIEncodedPacked(
                new ABIValue("uint8", 1),                  // Burn
                new ABIValue("uint256", 1),                // Mainnet
                new ABIValue("address", contract.Address),
                new ABIValue("address", account.Address),
                new ABIValue("uint256", amount),
                new ABIValue("uint256", fee),
                new ABIValue("uint256", nonce)
                );

                EthereumMessageSigner signer = new EthereumMessageSigner();
                string signature = signer.Sign(messageHash, sourcePrivateKey);

                return signature;
            }
        }

        /// <summary>
        /// Generate Mint To Stake Signature
        /// Token Stake process
        /// </summary>
        public static (string, string) GenerateMintToStakeSignature(AddressItem owner,
                                                                    string amount,
                                                                    string environment)
        {
            string url;
            Account approverAccount = new Account(owner.PrivateKey.ToString());

            if (Shared.isTestEnvironment(environment))
            {
                url = URL_INFURA_RPC;
            }
            else
            {
                url = URL_INFURA_RPC_MAINNET;
            }

            Web3 web3 = new Web3(url);
            ContractAddressItem contractAddressItem = TransferFunctions.GetContractAddress(environment, ECurrency.Gtd.ToString());
            string contractAbiBlob = TransferFunctions.GetContractAbi("GTD", "MainNet");
            Contract contract = web3.Eth.GetContract(contractAbiBlob, contractAddressItem.Address);
            BigInteger amountBig = ConvertToGluwacoinPrecisionFactor(amount, contractAddressItem.Decimals);
            Function contractFunction = contract.GetFunction("mintToStake");

            TransactionInput input = contractFunction.CreateTransactionInput(approverAccount.Address,
                                                                            new HexBigInteger(TRANSFER_GAS_LIMIT),
                                                                            new HexBigInteger(GetSafeGasPrice()),
                                                                            new HexBigInteger(BigInteger.Zero),
                                                                            amountBig);

            input.Nonce = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(approverAccount.Address).Result;
            input.Nonce.Value += 1;
            string signedTxn = new AccountOfflineTransactionSigner().SignTransaction(approverAccount, input);
            var signature = GenerateGcreApproveSignature(owner, amount, environment);

            return (signature, "0x" + signedTxn);
        }

        /// <summary>
        /// Generate Gcre Approve Signature
        /// Token Stake process
        /// </summary>
        public static string GenerateGcreApproveSignature(AddressItem owner,
                                                          string amount,
                                                          string environment)
        {
            string url;
            Account approverAccount = new Account(owner.PrivateKey.ToString());

            if (Shared.isTestEnvironment(environment))
            {
                url = URL_INFURA_RPC;
            }
            else
            {
                url = URL_INFURA_RPC_MAINNET;
            }

            Web3 web3 = new Web3(url);
            ContractAddressItem contractAddressItem = TransferFunctions.GetContractAddress(environment, ECurrency.Gcre.ToString());
            string contractAbiBlob = TransferFunctions.GetContractAbi("Gcre", "MainNet");
            string contractGtd = TransferFunctions.GetContractAddress(environment, ECurrency.Gtd.ToString()).Address;
            Contract contract = web3.Eth.GetContract(contractAbiBlob, contractAddressItem.Address);
            BigInteger amountBig = ConvertToGluwacoinPrecisionFactor(amount, contractAddressItem.Decimals);
            Function contractFunction = contract.GetFunction("approve");

            TransactionInput input = contractFunction.CreateTransactionInput(approverAccount.Address,
                                                                            new HexBigInteger(TRANSFER_GAS_LIMIT),
                                                                            new HexBigInteger(GetSafeGasPrice()),
                                                                            new HexBigInteger(BigInteger.Zero),
                                                                            contractGtd,
                                                                            amountBig);

            input.Nonce = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(approverAccount.Address).Result;
            string signedTxn = new AccountOfflineTransactionSigner().SignTransaction(approverAccount, input);

            return ("0x" + signedTxn);
        }


        /// <summary>
        /// Generate Unstake Signature
        /// Gtd Unstake process
        /// </summary>
        public static string GenerateGtdUnstakeSignature(AddressItem owner,
                                                        string amount,
                                                        string environment)
        {
            string url;
            Account approverAccount = new Account(owner.PrivateKey.ToString());

            if (Shared.isTestEnvironment(environment))
            {
                url = URL_INFURA_RPC;
            }
            else
            {
                url = URL_INFURA_RPC_MAINNET;
            }

            Web3 web3 = new Web3(url);
            ContractAddressItem contractAddressItem = TransferFunctions.GetContractAddress(environment, ECurrency.Gtd.ToString());
            string contractAbiBlob = TransferFunctions.GetContractAbi("GTD", "MainNet");
            Contract contract = web3.Eth.GetContract(contractAbiBlob, contractAddressItem.Address);
            BigInteger amountBig = ConvertToGluwacoinPrecisionFactor(amount, contractAddressItem.Decimals);
            Function contractFunction = contract.GetFunction("unstake");

            TransactionInput input = contractFunction.CreateTransactionInput(approverAccount.Address,
                                                                            new HexBigInteger(TRANSFER_GAS_LIMIT),
                                                                            new HexBigInteger(GetSafeGasPrice()),
                                                                            new HexBigInteger(BigInteger.Zero),
                                                                            amountBig);

            input.Nonce = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(approverAccount.Address).Result;
            string signedTxn = new AccountOfflineTransactionSigner().SignTransaction(approverAccount, input);

            return ("0x" + signedTxn);
        }


        /// <summary>
        /// Generate Burn Signature
        /// Token burn process
        /// </summary>
        public static string GenerateGtdBurnSignature(AddressItem owner,
                                                        string amount,
                                                        string environment)
        {
            string url;
            Account approverAccount = new Account(owner.PrivateKey.ToString());

            if (Shared.isTestEnvironment(environment))
            {
                url = URL_INFURA_RPC;
            }
            else
            {
                url = URL_INFURA_RPC_MAINNET;
            };

            Web3 web3 = new Web3(url);
            ContractAddressItem contractAddressItem = TransferFunctions.GetContractAddress(environment, ECurrency.Gtd.ToString());
            //contractAddressItem.Address = "";
            string contractAbiBlob = TransferFunctions.GetContractAbi("GTD", "MainNet");
            Contract contract = web3.Eth.GetContract(contractAbiBlob, contractAddressItem.Address);
            BigInteger amountBig = ConvertToGluwacoinPrecisionFactor(amount, contractAddressItem.Decimals);
            Function contractFunction = contract.GetFunction("burn");

            TransactionInput input = contractFunction.CreateTransactionInput(approverAccount.Address,
                                                                            new HexBigInteger(TRANSFER_GAS_LIMIT),
                                                                            new HexBigInteger(GetSafeGasPrice()),
                                                                            new HexBigInteger(BigInteger.Zero),
                                                                            amountBig);

            input.Nonce = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(approverAccount.Address).Result;
            string signedTxn = new AccountOfflineTransactionSigner().SignTransaction(approverAccount, input);

            return ("0x" + signedTxn);
        }


        /// <summary>
        /// Generate Claim rewards Signature
        /// Token claim process
        /// </summary>
        public static string GenerateGateMintAllRewardSignature(AddressItem owner,
                                                                string environment)
        {
            string url;
            Account approverAccount = new Account(owner.PrivateKey.ToString());

            if (Shared.isTestEnvironment(environment))
            {
                url = URL_INFURA_RPC;
            }
            else
            {
                url = URL_INFURA_RPC_MAINNET;
            }

            Web3 web3 = new Web3(url);
            ContractAddressItem contractAddressItem = TransferFunctions.GetContractAddress(environment, ECurrency.Gate.ToString());
            string contractAbiBlob = TransferFunctions.GetContractAbi("GATE", "MainNet");
            Contract contract = web3.Eth.GetContract(contractAbiBlob, contractAddressItem.Address);
            Function contractFunction = contract.GetFunction("mintAllReward");

            TransactionInput input = contractFunction.CreateTransactionInput(approverAccount.Address,
                                                                            new HexBigInteger(TRANSFER_GAS_LIMIT),
                                                                            new HexBigInteger(GetSafeGasPrice()),
                                                                            new HexBigInteger(BigInteger.Zero));

            input.Nonce = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(approverAccount.Address).Result;
            string signedTxn = new AccountOfflineTransactionSigner().SignTransaction(approverAccount, input);

            return ("0x" + signedTxn);
        }

        /// <summary>
        /// Withdraw Balance Sig
        /// Fixed-Term Interest Account
        /// </summary>
        /// <param name="ownerPrivateKey"></param>
        /// <param name="ownerAddress"></param>
        /// <param name="balanceHashHexStrings"></param>
        /// <param name="environment"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static async Task<string> CreateWithdrawBalancesSignature(string ownerPrivateKey, string ownerAddress, string[] balanceHashHexStrings, string environment, ECurrency currency)
        {

            Web3 web3 = new Web3();
            CurrencyItem currencyInfo = new CurrencyItem();
            Account ownerAccount = null;
            Function withdrawBalancesFunc = null;
            List<byte[]> balanceHashes = new List<byte[]>();
            foreach (string balanceHash in balanceHashHexStrings)
            {
                balanceHashes.Add(balanceHash.HexToByteArray());
            }

            object[] functionInput = new object[]
            {
                balanceHashes.ToArray()
            };

            if ((currency == ECurrency.Usdc) && (environment == "Test"))
            {
                ownerAccount = new Account(ownerPrivateKey);
                gas = new HexBigInteger(GetSafeGasPrice(currency.ToString()).Item2);
                gasPrice = new HexBigInteger(GetSafeGasPrice(currency.ToString()).Item1);
                web3 = new Web3(ownerAccount, URL_INFURA_RPC);
                withdrawBalancesFunc = getFunction("withdrawBalances");
            }
            else if ((currency == ECurrency.BnbUsdc) && (environment == "Test"))
            {
                currencyInfo = TransferFunctions.GetCurrencyInfo(currency, environment);
                ownerAccount = new Account(ownerPrivateKey, currencyInfo.chainId);
                gas = new HexBigInteger(FTA_GAS_LIMIT);
                gasPrice = new HexBigInteger(GetSafeGasPrice());
                web3 = new Web3(ownerAccount, currencyInfo.nodeUrl);
                withdrawBalancesFunc = getFunction("withdrawBalances", currencyInfo, currency, environment);
            }
            else if ((currency == ECurrency.Usdc) && (environment == "Staging" || environment == "Production"))
            {
                ownerAccount = new Account(ownerPrivateKey, 1);
                gas = new HexBigInteger(FTA_GAS_LIMIT);
                gasPrice = new HexBigInteger(GetSafeGasPrice());
                web3 = new Web3(ownerAccount, URL_ETH_MAINNET);
                withdrawBalancesFunc = getFunction("withdrawBalances");
            }
            else if ((currency == ECurrency.BnbUsdc) && (environment == "Staging" || environment == "Production"))
            {
                currencyInfo = TransferFunctions.GetCurrencyInfo(currency, environment);
                ownerAccount = new Account(ownerPrivateKey, currencyInfo.chainId);
                gas = new HexBigInteger(FTA_GAS_LIMIT);
                gasPrice = new HexBigInteger(GetSafeGasPrice());
                web3 = new Web3(ownerAccount, currencyInfo.nodeUrl);
                withdrawBalancesFunc = getFunction("withdrawBalances", currencyInfo, currency, environment);
            }

            HexBigInteger nonce = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(ownerAddress);
            TransactionInput transactionInput = withdrawBalancesFunc.CreateTransactionInput(
                from: ownerAddress,
                gas: gas,
                gasPrice: gasPrice,
                value: new HexBigInteger(BigInteger.Zero),
                functionInput
            );

            transactionInput.Nonce = nonce;
            var signature = new AccountOfflineTransactionSigner().SignTransaction(ownerAccount, transactionInput);

            return signature;
        }

        /// <summary>
        /// Create Account By Sig
        /// Fixed-Term Interest Account
        /// </summary>
        public static async Task<string> CreateAccountBySigSignature(string ownerPrivateKey,
                                                                      string ownerAddress,
                                                                      string amount,
                                                                      string identityHash,
                                                                      string poolHash,
                                                                      BigInteger gluwaNonce,
                                                                      int v,
                                                                      string r,
                                                                      string s,
                                                                      string environment,
                                                                      ECurrency currency = ECurrency.Usdc)
        {
            Account ownerAccount = null;
            CurrencyItem currencyInfo = TransferFunctions.GetCurrencyInfo(currency, environment);
            BigInteger amount_big = ConvertToGluwacoinBigInteger(amount, int.Parse(currencyInfo.numberOfDigits));
            string url = currencyInfo.nodeUrl;
            object[] functionInput = new object[]
            {
                ownerAddress,
                amount_big,
                identityHash.HexToByteArray(),
                poolHash.HexToByteArray(),
                gluwaNonce,
                v,
                r.HexToByteArray(),
                s.HexToByteArray()
            };

            if (currency.Equals(ECurrency.Usdc) && Shared.isTestEnvironment(environment))
            {
                ownerAccount = new Account(ownerPrivateKey);
                gas = new HexBigInteger(GetSafeGasPrice(currency.ToString()).Item2);
                gasPrice = new HexBigInteger(GetSafeGasPrice(currency.ToString()).Item1);
            } 
            else if (currency.Equals(ECurrency.BnbUsdc) && Shared.isTestEnvironment(environment))
            {
                ownerAccount = new Account(ownerPrivateKey, currencyInfo.chainId);
                gas = new HexBigInteger(FTA_GAS_LIMIT);
                gasPrice = new HexBigInteger(GetSafeGasPrice());
            }
            else if (currency.Equals(ECurrency.Usdc) && !Shared.isTestEnvironment(environment))
            {
                ownerAccount = new Account(ownerPrivateKey);
                gas = new HexBigInteger(FTA_GAS_LIMIT);
                gasPrice = new HexBigInteger(GetSafeGasPrice());
            }
            else if (currency.Equals(ECurrency.BnbUsdc) && !Shared.isTestEnvironment(environment))
            {
                ownerAccount = new Account(ownerPrivateKey, currencyInfo.chainId);
                gas = new HexBigInteger(FTA_GAS_LIMIT);
                gasPrice = new HexBigInteger(GetSafeGasPrice());
            }
           
            Web3 web3 = new Web3(ownerAccount, url);
            HexBigInteger nonce = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(ownerAddress);

            var createAccountFunc = getFunction("createAccountBySig", currencyInfo, currency, environment);
            TransactionInput transactionInput = createAccountFunc.CreateTransactionInput(
                from: ownerAddress,
                gas: gas,
                gasPrice: gasPrice,
                value: new HexBigInteger(BigInteger.Zero),
                functionInput
            );

            transactionInput.Nonce = nonce;
            var signature = new AccountOfflineTransactionSigner().SignTransaction(ownerAccount, transactionInput);

            return signature;
        }

        /// <summary>
        /// Create Balance By Sig
        /// Fixed-Term Interest Account
        /// </summary>
        public static async Task<string> CreateBalanceBySigSignature(string ownerPrivateKey,
                                                                      string ownerAddress,
                                                                      string amount,
                                                                      string poolHash,
                                                                      BigInteger gluwaNonce,
                                                                      int v,
                                                                      string r,
                                                                      string s,
                                                                      ECurrency currency = ECurrency.Usdc,
                                                                      string environment = "")
        {
            CurrencyItem currencyInfo = TransferFunctions.GetCurrencyInfo(currency, environment);
            BigInteger amountBig = ConvertToGluwacoinBigInteger(amount, int.Parse(currencyInfo.numberOfDigits));

            object[] functionInput = new object[]
            {
                ownerAddress,
                amountBig,
                poolHash.HexToByteArray(),
                gluwaNonce,
                v,
                r.HexToByteArray(),
                s.HexToByteArray()
            };
            Account ownerAccount = null;
            Function createBalanceFunc = null;
            Web3 web3 = new Web3();
            if (currency.Equals(ECurrency.Usdc) && environment == "Test")
            {
                ownerAccount = new Account(ownerPrivateKey);
                web3 = new Web3(ownerAccount, URL_INFURA_RPC);
                gas = new HexBigInteger(GetSafeGasPrice("Usdc").Item2);
                gasPrice = new HexBigInteger(GetSafeGasPrice("Usdc").Item1);
            }
            else if (currency.Equals(ECurrency.BnbUsdc) && environment == "Test")
            {
                ownerAccount = new Account(ownerPrivateKey, currencyInfo.chainId);
                web3 = new Web3(ownerAccount, currencyInfo.nodeUrl);
                gas = new HexBigInteger(FTA_GAS_LIMIT);
                gasPrice = new HexBigInteger(GetSafeGasPrice());
            } 
            else if (currency.Equals (ECurrency.Usdc) && environment != "Test")
            {
                ownerAccount = new Account(ownerPrivateKey);
                web3 = new Web3(ownerAccount, URL_INFURA_RPC_MAINNET);
                gas = new HexBigInteger(FTA_GAS_LIMIT);
                gasPrice = new HexBigInteger(GetSafeGasPrice());
            }
            else if (currency.Equals(ECurrency.BnbUsdc) && environment != "Test")
            {
                ownerAccount = new Account(ownerPrivateKey, currencyInfo.chainId);
                web3 = new Web3(ownerAccount, currencyInfo.nodeUrl);        
                gas = new HexBigInteger(FTA_GAS_LIMIT);
                gasPrice = new HexBigInteger(GetSafeGasPrice());
            }
   
            createBalanceFunc = getFunction("createBalanceBySig", currencyInfo, currency, environment);
            HexBigInteger nonce = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(ownerAddress);

            TransactionInput transactionInput = createBalanceFunc.CreateTransactionInput(
                from: ownerAddress,
                gas: gas,
                gasPrice: gasPrice,
                value: new HexBigInteger(BigInteger.Zero),
                functionInput
            );

            transactionInput.Nonce = nonce;
            var signature = new AccountOfflineTransactionSigner().SignTransaction(ownerAccount, transactionInput);

            return signature;
        }

        /// <summary>
        /// create Account or Balance By Sig based on currency and environment
        /// </summary>
        private static Function getFunction(string functionName, ECurrency currency = ECurrency.Usdc, string environment = "Test")
        {
            Web3 web3 = new Web3();
            Contract contract = web3.Eth.GetContract("", "");

            if ((currency == ECurrency.Usdc) && (environment == "Test") && (functionName.Equals("approve")))
            {
                web3 = new Web3(URL_ETH);
                contract = web3.Eth.GetContract(Shared.APPROVE_ABI, "***********************");
            }
            else if ((currency == ECurrency.Usdc) && (environment == "Test"))
            {
                web3 = new Web3(URL_ETH);
                contract = web3.Eth.GetContract(Shared.FTA_ABI, FTA_ADDRESS);
            }
            else if ((currency == ECurrency.Usdc) && (environment == "Staging"))
            {
                web3 = new Web3(URL_ETH_MAINNET);
                contract = web3.Eth.GetContract(Shared.FTA_ABI, "***********************");
            }
            else if ((currency == ECurrency.Usdc) && (environment == "Production"))
            {
                web3 = new Web3(URL_ETH_MAINNET);
                contract = web3.Eth.GetContract(Shared.FTA_ABI, "***********************");
            }

            Function contractFunction = contract.GetFunction(functionName);
            return contractFunction;
        }


        /// <summary>
        /// create Account or Balance By Sig based on currency and environment
        /// </summary>
        private static Function getFunction(string functionName, CurrencyItem currencyInfo, ECurrency currency = ECurrency.Usdc, string environment = "Test")
        {
            Web3 web3 = new Web3();
            Contract contract = web3.Eth.GetContract("", "");
            
            if ((currency == ECurrency.Usdc) && (environment == "Test") && (!functionName.Equals("approve")))
            {
                web3 = new Web3(URL_INFURA_RPC);
                contract = web3.Eth.GetContract(Shared.FTA_ABI, FTA_ADDRESS);
            }
            else if (currency.Equals(ECurrency.Usdc) && (environment == "Test") && (functionName.Equals("approve")))
            {
                web3 = new Web3(URL_INFURA_RPC);
                contract = web3.Eth.GetContract(Shared.APPROVE_ABI, currencyInfo.contractAddress);
            }
            else if (currency.Equals(ECurrency.BnbUsdc) && (environment == "Test") && (functionName.Equals("approve")))
            {
                web3 = new Web3(currencyInfo.nodeUrl);
                contract = web3.Eth.GetContract(Shared.APPROVE_ABI, currencyInfo.contractAddress);
            }
            else if (currency.Equals(ECurrency.BnbUsdc) && (environment == "Test") && (!functionName.Equals("approve")))
            {
                web3 = new Web3(currencyInfo.nodeUrl);
                contract = web3.Eth.GetContract(Shared.FTA_ABI, "***********************");
            }
            else if ((currency == ECurrency.Usdc) && (environment == "Staging"))
            {
                web3 = new Web3(URL_ETH_MAINNET);
                contract = web3.Eth.GetContract(Shared.FTA_ABI, FTA_ADDRESS);
            }else if ((currency == ECurrency.Usdc) && (environment == "Production"))
            {
                web3 = new Web3(URL_ETH_MAINNET);
                contract = web3.Eth.GetContract(Shared.FTA_ABI, "***********************");
            }

            Function contractFunction = contract.GetFunction(functionName);
            return contractFunction;
        }

        /// <summary>
        /// Get approve Txn signature
        /// Fixed-Term Interest Account
        /// </summary>
        /// <param name="approverPrivateKey"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static async Task<string> PostTransferBnbSignatureAsync(AddressItem addressSource, string receiverAddress, decimal amount, string environment, ECurrency currency = ECurrency.Usdc)
        {
            CurrencyItem currencyInfo = TransferFunctions.GetCurrencyInfo(currency, environment);
            Account approverAccount = new Account(addressSource.PrivateKey);
            string signedTxn = "";
            Contract contract = null;
            Web3 web3 = new Web3();

            if (currency.Equals(ECurrency.Usdc))
            {
                web3 = new Web3(URL_INFURA_RPC);
            }
            else if (currency.Equals(ECurrency.BnbUsdc))
            {
                web3 = new Web3(currencyInfo.nodeUrl);
            }
            else
            {
                Assert.Fail("Currency not found");
            }
            var abi = await GetAbiAsync("MainNetGcreContractABI.txt");
            contract = web3.Eth.GetContract(abi, currencyInfo.contractAddress);
            BigInteger amount_big = ConvertToGluwacoinBigInteger(amount.ToString(), int.Parse(currencyInfo.numberOfDigits));

            var contractFunction = contract.GetFunction("transfer");
            TransactionInput input = contractFunction.CreateTransactionInput(
                approverAccount.Address,
                receiverAddress, amount_big);
            input.Gas = new HexBigInteger(TRANSFER_GAS_LIMIT);
            input.GasPrice = new HexBigInteger(GetSafeGasPrice());
            input.Nonce = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(approverAccount.Address).Result;
            signedTxn = new AccountOfflineTransactionSigner().SignTransaction(approverAccount, input, currencyInfo.chainId);

            return ("0x" + signedTxn);
        }

        // TODO: Delete this method when ready
        internal static async Task<string> GetAbiAsync(string name)
        {
            var containerClient = new BlobContainerClient(new Uri("***********************"), new DefaultAzureCredential());
            var path = name;
            var blobClient = containerClient.GetBlobClient(path);

            using (var memoryStream = new MemoryStream())
            {
                await blobClient.DownloadToAsync(memoryStream);
                var rawAbi = Encoding.UTF8.GetString(memoryStream.ToArray());
                return rawAbi;
            }
        }
    }
}