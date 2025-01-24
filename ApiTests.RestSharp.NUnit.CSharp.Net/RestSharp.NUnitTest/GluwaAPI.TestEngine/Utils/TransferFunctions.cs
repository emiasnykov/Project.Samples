using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.Models;
using GluwaAPI.TestEngine.Models.RequestBody;
using GluwaAPI.TestEngine.Models.ResponseBody;
using GluwaAPI.TestEngine.Setup;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace GluwaAPI.TestEngine.Utils
{
    /// <summary>
    /// Methods and variables used throughout the transfer tests
    /// </summary>
    public class TransferFunctions
    {
        /// <summary>
        /// Returns all transactions related to that address
        /// </summary>
        /// <param name="address"></param>
        /// <param name="signature"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static JArray GetAddressesTransactions(string address, string signature, string currency, string environment = "Test")
        {

            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Addresses/{address}/Transactions", environment: environment),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));

            // Verify that response returns OK
            Assertions.HandleSetupAssertions(200, response);
            JArray transactions = JArray.Parse(response.Content);
            
            return transactions;
        }

        /// <summary>
        /// Returns all transactions related to that Eth address
        /// </summary>
        /// <param name="address"></param>
        /// <param name="signature"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static JArray GetEthAddressesTransactions(string address, string signature, string currency) //List<GetEthTransactionsResponse>
        {

            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"V1/Addresses/{address}/Transactions/Ethereum?Currency={currency}"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("X-REQUEST-SIGNATURE", signature));

            // Verify that response returns OK
            Assertions.HandleSetupAssertions(200, response);
            JArray items = (JArray)JObject.Parse(response.Content)["Items"];

            return items;
        }


        /// <summary>
        /// Returns all transactions related to that Eth address
        /// </summary>
        /// <param name="addressItem"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static JToken GetEthTransactionDetails(AddressItem address, ECurrency currency)
        {

            var response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"V1/Addresses/{address.Address}/Transactions/Ethereum?Currency={currency}&type=Pay"),
                                            Api.SendRequest(Method.GET)
                                               .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));

            // Verify that response returns OK
            Assertions.HandleSetupAssertions(200, response);
            JArray items = (JArray)JObject.Parse(response.Content)["Items"];

            // Return confirmed transactions only
            var txn = items
             .FirstOrDefault(x => x.Value<string>("Status") == "Confirmed");

            return txn;
        }


        /// <summary>
        /// Get address balance for currency state
        /// </summary>
        /// <param name="address"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static JToken GetAddressBalance(AddressItem address, string type)
        {
            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/balances/{address.Address}/gatewaydao"),
                                                     Api.SendRequest(Method.GET));
            // Extract address balance by currency state
            JArray obj = JArray.Parse(response.Content);
            var balance = obj.First(x => x.Value<string>("State") == type);

            return balance;
        }

        /// <summary>
        /// Get address balance for currency
        /// </summary>
        /// <param name="address"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static JToken GetAddressBalance(string address, string currency)
        {
            // Execute
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/balances/{address}"),
                                                     Api.SendRequest(Method.GET));
            // Extract address balance by currency state
            JArray obj = JArray.Parse(response.Content);
            var balance = obj.First(x => x.Value<string>("Currency") == currency);

            return balance;
        }

        /// <summary>
        /// Return the X-Request signature and txnHash based on the address
        /// </summary>
        /// <param name="address"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public (string, string) generateSignatureAndHash(AddressItem address, ECurrency currency)
        {
            string signature = SignaturesUtil.GetXRequestSignature(address.PrivateKey);    
            JArray transactions = GetAddressesTransactions(address.Address, signature, currency.ToString());
            JToken transaction = transactions.FirstOrDefault(x => x.Value<string>("Status") == "Confirmed"); // Find transaction with Confirmed status
            string txnHash = (string)transaction.SelectToken("TxnHash");
            
            return (signature, txnHash);
        }


        /// <summary>
        /// Return the X-Request signature and txnHash based on the address
        /// </summary>
        /// <param name="address"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public (string, string) generateEthSignatureAndHash(AddressItem address, ECurrency currency)
        {
            string signature = SignaturesUtil.GetXRequestSignature(address.PrivateKey);       
            JArray transactions = GetEthAddressesTransactions(address.Address, signature, currency.ToString());
            JToken transaction = transactions.FirstOrDefault(x => x.Value<string>("Status") == "Confirmed"); // Find transaction with Confirmed status
            string txnHash = (string)transaction.SelectToken("TxnHash");
            
            return (signature, txnHash);
        }


        /// <summary>
        /// TxnHash that can't be found in the environment
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string NotFoundTxnHash(string environment)
        {
            switch (environment)
            {
                case "Test":
                    return "***********************";
                case "Staging":
                    return "***********************";
                case "Production":
                    return "***********************";
                default:
                    throw new Exception($"No txnHashes for {environment}");
            }
        }

        /// <summary>
        /// Get shared access signature (SAS)
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetSasSignature(string currency)
        {
            switch (currency)
            {
                case "Usd":
                    return "***********************";
                case "Usdc":
                    return "***********************";
                case "GTD":
                    return "***********************";
                case "GATE":
                    return "***********************";
                case "Gcre":
                    return "***********************";
                default:
                    throw new Exception($"No ABI Contract for {currency}");
            }
        }

        /// <summary>
        /// Generates idem 
        /// </summary>
        /// <returns></returns>
        public static string GenerateIdem()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Get Contract ABI Blob
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetContractAbi(string currency, string environment)
        {
            string abiUrl = "***********************";
            string signature = GetSasSignature(currency);
            string endpoint = $"{environment}{currency}{signature}";

            var response = Api.GetResponse(Api.SetUrlAndClient(abiUrl, endpoint), Api.SendRequest(Method.GET));
            Assertions.HandleSetupAssertions(200, response);
            Assert.IsFalse(string.IsNullOrEmpty(response.Content), "Response didn't return contract abi");

            return response.Content;
        }

        /// <summary>
        /// Get Luniverse Gateway contract address
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string GetGatewayContract(string environment, string currency)
        {
            // Get environment for parameter
            string env;
            if (Api.IsTestEnvironment(environment)) { env = "test"; } else { env = "main"; }

            var response = Api.GetResponse(Api.SetQAFunctions("api/GetLuniverseGatewayContract"),
                                            Api.SendRequest(Method.GET)
                                                .AddParameter("env", env)
                                                .AddParameter("currency", currency.ToLower()));
            Assertions.HandleSetupAssertions(200, response);
            var token = Newtonsoft.Json.Linq.JToken.Parse(response.Content);
            return token.ToString();
        }

        /// <summary>
        /// Get Gluwa Fixed-Term Interest Account contract address for environment
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetGluwaBondAccountContract(string environment)
        {
            var response = Api.GetResponse(Api.SetQAFunctions("api/GetGluwaBondAccountContract"),
                                           Api.SendRequest(Method.GET)
                                              .AddParameter("env", environment));

            Assertions.HandleSetupAssertions(200, response);
            var token = JToken.Parse(response.Content);

            return token.ToString();
        }

        /// <summary>
        /// Get Gluwa Prize Account contract address for environment
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetGluwaPrizeAccountContract(string environment)
        {
            var response = Api.GetResponse(Api.SetQAFunctions("api/GetGluwaPrizeAccountContract"),
                                           Api.SendRequest(Method.GET)
                                              .AddParameter("env", environment));

            Assertions.HandleSetupAssertions(200, response);
            var token = JToken.Parse(response.Content);

            return token.ToString();
        }

        public string Get_AddressBalance(Models.AddressItem address, ECurrency currency)
        {
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/{currency}/Addresses/{address.Address}"),
                                                       Api.SendRequest(Method.GET));
            // Extract balance item
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content);

            return balanceItem.Balance;
        }

        /// <summary>
        /// Get available balance for current user
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public string Get_AccountBalance(string environment, string type)
        {
            string endpoint;

            if (type == "Bond")
            {
                endpoint = "***********************";
            }
            else
            {
                endpoint = "***********************";
            }

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl(endpoint), Api.SendRequest(Method.GET)
                                                .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Extract balance item
            GetBalanceResponse balanceItem = JsonConvert.DeserializeObject<GetBalanceResponse>(response.Content);

            return balanceItem.Balance;
        }

        /// <summary>
        /// Get Id of Fixed-Term Interest account
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetBondAccountId(string environment)
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl("v1/Accounts"), Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            JToken jsonObj = JToken.Parse(response.Content);
            dynamic data = JObject.Parse(jsonObj.ToString());
            var id = (string)data.SelectToken("Accounts")[0].Id;

            return id;
        }

        /// <summary>
        /// Get TxId for given Fixed-Term Interest account
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string getTransactionById(string Id, string environment)
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{Id}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            JArray jsonArray = JArray.Parse(response.Content);
            dynamic data = JObject.Parse(jsonArray[0].ToString());
            var Txid = (string)data.SelectToken("Id");

            return Txid;
        }

        /// <summary>
        /// Get TxId for given lottery account
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string getPrizeTransaction(string environment)
        {
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v2/Accounts/Prize/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            // Deserialization
            JObject objects = JObject.Parse(response.Content);
            var items = objects["Items"];
            var Txid = (string)items[0].SelectToken("Id");

            return Txid;
        }

        /// <summary>
        /// Count transactions for given Fixed-Term Interest account
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static int CountAccountTransactions(string environment)
        {
            string Id = GetBondAccountId(environment);

            // Execute response
            var TxbyId = Api.GetResponse(Api.SetGluwaApiUrl($"v1/Accounts/{Id}/Transactions"), Api.SendRequest(Method.GET)
                                            .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));

            JArray jsonArray = JArray.Parse(TxbyId.Content);
            var s = jsonArray.Count;

            return s;
        }
        /// <summary>
        /// Return contract address from environment by currency
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static ContractAddressItem GetContractAddress(string environment, string currency)
        {
            string url;
            string endpoint;

            if (Shared.isTestEnvironment(environment))
            {
                url = "***********************";
                endpoint = $"v1/Contract/Address/{currency}/Testnet";
            }

            else
            {
                url = "***********************";
                endpoint = $"v1/Contract/Address/{currency}/Mainnet";
            }
            var client = new RestClient(url + endpoint);
            var response = Api.GetResponse(client, Api.SendRequest(Method.GET));
            Assertions.HandleSetupAssertions(200, response);

            return JsonConvert.DeserializeObject<ContractAddressItem>(response.Content);
        }

        /// <summary>
        /// Get invest account address
        /// </summary>
        /// <param name="type"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetAccountAddress(string type, string environment)
        {
            string accountAddress;
            accountAddress = null;

            switch (type)
            {
                case "Bond":
                    accountAddress = TransferFunctions.GetGluwaBondAccountContract(environment.ToLower());
                    break;
                case "Prize":
                    accountAddress = TransferFunctions.GetGluwaPrizeAccountContract(environment.ToLower());
                    break;
            }

            return accountAddress;
        }

        /// <summary>
        /// Get full currency info
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static CurrencyItem GetCurrencyInfo(ECurrency currency = ECurrency.Usdc, string environment = "")
        {
            // Declare
            CurrencyItem currencyInfo = new CurrencyItem();

            // Get currency info
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/currencies/{currency}", environment: environment),
                                           Api.SendRequest(Method.GET));
            // Assert OK
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response);
            
            // Deserialize
            JObject responceCurrency = JObject.Parse(response.Content);

            // Extract values
            string networkName = (string)responceCurrency.GetValue("NetworkName");
            currencyInfo.numberOfDigits = (string)responceCurrency.GetValue("NumberOfDigits");
            currencyInfo.contractAddress = (string)responceCurrency.GetValue("ContractAddress");

            // Get node URL for currency
            var responseNode = Api.GetResponse(Api.SetGluwaApiUrl($"v1/networks/{networkName}/evmconnectioninfo", environment: environment),
                                           Api.SendRequest(Method.GET));
            // Assert OK
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, responseNode);

            // Deserialize
            JObject nodeInfo = JObject.Parse(responseNode.Content);     
            currencyInfo.chainId = (int)nodeInfo.GetValue("ChainId");

            switch (networkName)
            {
                case "ethereum":
                    {
                        currencyInfo.nodeUrl = (string)nodeInfo.GetValue("NodeUrl").ToString();
                        break;
                    }
                case "binance":
                    {
                        currencyInfo.nodeUrl = (string)nodeInfo.GetValue("NodeUrl");
                        break;
                    }
            }

            return currencyInfo;
        }


        /// <summary>
        /// Get Id of FTA Pool
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="poolName"></param>
        /// <param name="poolState"></param>
        /// <returns></returns>
        public static string GetFtaPoolId(string environment, 
                                            string poolName = "", string poolState = "", 
                                            short itemAtIndex = 0, 
                                            string investorName = "Sender", 
                                            ECurrency currency = ECurrency.Usdc, 
                                            string rngNo = "1987")
        {
            // Arrange
            AddressItem investor = QAKeyVault.GetGluwaAddress(investorName);
            string endpoint = $"v1/FtaPools/{investor.Address}";
            endpoint = SetChainId(endpoint, currency);
            RestClient requestUrl = Api.SetGluwaApiUrl(endpoint, rngNo: rngNo, environment: environment);

            // Execute request
            var response = Api.GetResponse(requestUrl,
                                  Api.SendRequest(Method.GET)
                                     .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                     .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));

            // PUT ACTUAL ERROR MESSAGE INTO Contains()
            if (response.Content.Contains($"PASS HERE CORRECT ERRROR MESSAGE"))
            {
                Assert.Ignore($"No pool has been found with status: {poolState}");
            }

            // Deserialization
            JArray jsonObj = JArray.Parse(response.Content);
            JToken[] filteredJsonObj = null;

            if (!poolState.Equals(""))
            {
                filteredJsonObj = jsonObj.Where(x => x.Value<string>("PoolState") == poolState).ToArray();
                if (filteredJsonObj.Length != 0)
                {
                    JToken poolDetails = filteredJsonObj[itemAtIndex];
                    var id = (string)poolDetails.SelectToken("ID");
                    return id;
                }
                else
                {
                    Assert.Ignore($"Pool with State = '{poolState}' Not Found.");
                    return null;
                }
            }
            // Select pool according to pool name
            else if (!poolName.Equals(""))
            {
                filteredJsonObj = jsonObj.Where(x => x.Value<string>("PoolName") == poolName).ToArray();
                if (filteredJsonObj.Length != 0)
                {
                    JToken poolDetails = filteredJsonObj[0];
                    var id = (string)poolDetails.SelectToken("ID");
                    return id;
                }
                else
                {
                    Assert.Ignore($"Pool with Name = '{poolName}' Not Found.");
                    return null;
                }
            }
            else
            {
                Assert.Ignore("Pool Not Found.");
                return null;
            }
        }

        /// <summary>
        /// Get Id of FTA Pool
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="poolName"></param>
        /// <param name="poolState"></param>
        /// <returns></returns>
        public static string GetFtaPoolIdForWithdraw(string environment, 
                                                        string poolState = "", 
                                                        string investorName = "Sender", 
                                                        ECurrency currency = ECurrency.Usdc)
        {
            // Arrange
            AddressItem investor = QAKeyVault.GetGluwaAddress(investorName);
            string endpoint = $"v1/FtaPools/{investor.Address}";
            endpoint = SetChainId(endpoint, currency);
            RestClient requestUrl = Api.SetGluwaApiUrl(endpoint, environment: environment);

            // Execute request
            var response = Api.GetResponse(requestUrl,
                                  Api.SendRequest(Method.GET)
                                     .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                     .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));

            // Deserialization
            JArray jsonObj = JArray.Parse(response.Content);
            JToken[] filteredJsonObj = null;
            string id = "";

            if (!poolState.Equals(""))
            {
                filteredJsonObj = jsonObj.Where(x => x.Value<string>("PoolState") == poolState).ToArray();
                if (filteredJsonObj.Length != 0)
                {
                    try
                    {
                        JToken[] withdrawObj = filteredJsonObj.Where(x => x.Value<string>("CurrentRaise") != "0").ToArray();
                        withdrawObj = withdrawObj.Where(x => x.Value<string>("Balance") != "0").ToArray();
                        id = (string)withdrawObj.First().SelectToken("ID");
                        
                    }
                    catch (Exception ex)
                    {
                        TestContext.WriteLine(ex);
                        Assert.Ignore("No pool with withdrawable amount found");
                    }
                }
                else
                {
                    Assert.Ignore($"Pool with State = '{poolState}' Not Found.");
                    return null;
                }
            }
            return id;
        }

        /// <summary>
        /// Get Id of FTA Pool
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="poolName"></param>
        /// <param name="poolState"></param>
        /// <returns></returns>
        public static Tuple<string,string> GetFtaPoolIdAndName(string environment,
                                                                string poolName = "", 
                                                                string poolState = "", 
                                                                short itemAtIndex = 0,
                                                                string investorName = "Sender",
                                                                ECurrency currency = ECurrency.Usdc,
                                                                string rngNo = "1987")
        {
            // Arrange
            AddressItem investor = QAKeyVault.GetGluwaAddress(investorName);
            string endpoint = $"v1/FtaPools/{investor.Address}";
            endpoint = SetChainId(endpoint, currency);
            RestClient requestUrl = Api.SetGluwaApiUrl(endpoint, rngNo: rngNo, environment: environment);

            // Execute request
            var response = Api.GetResponse(requestUrl,
                                  Api.SendRequest(Method.GET)
                                     .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                     .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialization
            JArray jsonObj = JArray.Parse(response.Content);
            JToken[] filteredJsonObj = null;

            // Select pool according to pool state
            if (!poolState.Equals(""))
            {
                filteredJsonObj = jsonObj.Where(x => x.Value<string>("PoolState") == poolState).ToArray();
                if (filteredJsonObj.Length != 0)
                {
                    JToken poolDetails = filteredJsonObj[itemAtIndex];
                    var id = (string)poolDetails.SelectToken("ID");
                    string poolsName = (string)poolDetails.SelectToken("PoolName");
                    return Tuple.Create(id,poolsName);
                }
                else
                {
                    Assert.Ignore($"Pool with State = '{poolState}' Not Found.");
                    return null;
                }
            }
            // Select pool according to pool name
            else if (!poolName.Equals(""))
            {
                filteredJsonObj = jsonObj.Where(x => x.Value<string>("PoolName") == poolName).ToArray();
                if (filteredJsonObj.Length != 0)
                {
                    JToken poolDetails = filteredJsonObj[0];
                    var id = (string)poolDetails.SelectToken("ID");
                    string poolsName = (string)poolDetails.SelectToken("PoolName");
                    return Tuple.Create(id, poolsName);
                }
                else
                {
                    Assert.Ignore($"Pool with Name = '{poolName}' Not Found.");
                    return null;
                }
            }
            else
            {
                Assert.Ignore("Pool Not Found.");
                return null;
            }
        }

        /// <summary>
        /// Get Id of FTA Pool
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="poolState"></param>
        /// <param name="isActiveApprovedAmount"></param>
        /// <returns></returns>
        public static Tuple<string, string> GetFtaPoolIdAndAmount(string environment, 
                                                                    bool isActiveApprovedAmount = false, 
                                                                    string investorName = "Sender", 
                                                                    ECurrency currency = ECurrency.Usdc, 
                                                                    string rngNo = "1987",
                                                                    string userName = "qa.assertible")
        {
            // Arrange
            JToken poolDetails = "";
            string id = "";
            string amount = "1";
            AddressItem investor = QAKeyVault.GetGluwaAddress(investorName);

            string endpoint = $"v1/FtaPools/{investor.Address}";
            endpoint = SetChainId(endpoint, currency);
            RestClient requestUrl = Api.SetGluwaApiUrl(endpoint, rngNo: rngNo, environment: environment);

            // Execute request
            var response = Api.GetResponse(requestUrl,
                                  Api.SendRequest(Method.GET)
                                     .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment, userName))
                                     .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));

            // Deserialization
            JArray jsonObj = JArray.Parse(response.Content);

            if (isActiveApprovedAmount)
            {
                try
                {
                    poolDetails = jsonObj.Where(x => x.Value<string>("ActiveApprovedAmount") != "0").ToArray()[0];
                    id = poolDetails.Value<string>("ID");
                    amount = poolDetails.Value<string>("ActiveApprovedAmount");
                    return Tuple.Create(id, amount);
                }
                catch (Exception ex)
                {
                    Assert.Ignore($"Pool with ActiveApprovedAmount was not found: {ex}");
                }
            }
            return Tuple.Create(id, amount);
        }

        /// <summary>
        /// Get Id of FTA Transaction
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetFTATransactionId(AddressItem investor, string poolId, string environment)
        {
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"v1/FtaPools/{poolId}/{investor.Address}/Transactions"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));
            // Deserialization
            JObject jsonObj = JObject.Parse(response.Content);
            JArray data = JArray.Parse(jsonObj["Items"].ToString());

            // Get random id from the list
            Random randNumber = new Random();
            int txnTotalCount = int.Parse(jsonObj.SelectToken("TotalCount").ToString());     
            string id = (string)data[randNumber.Next(0, txnTotalCount)].SelectToken("ID");
            
            return id;
        }

        /// <summary>
        /// Get RSV and hash
        /// </summary>
        /// <returns></returns>
        public static (string, string, int, bool, string, string) generateRsvAndHash(AddressItem senderAddress, 
                                                                                    string poolId, 
                                                                                    string amount,
                                                                                    string nonce,
                                                                                    ECurrency currency,
                                                                                    string environment)
        {
            // Create body
            PostFtaAuthorizationBody body = RequestTestBody.CreatePostFtaAuthorizationBody(senderAddress,
                                                                                           poolId,
                                                                                           amount,
                                                                                           nonce,
                                                                                           currency);
            // Arrange
            string r = "";
            string s = "";
            int v = 0;
            bool account = false;
            string poolHash = "";
            string idIdentityHash = "";
            string endpoint = $"V1/FtaPools/GenerateRSV?investoraddress={senderAddress.Address}";

            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl(endpoint, environment: environment),
                                           Api.SendRequest(Method.POST, body)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(senderAddress.PrivateKey)));
            // Parse response
            JObject content = JObject.Parse(response.Content);

            // Negative cases
            if (response.Content.Contains($"is not accepting any more deposits"))
            {
                Assert.Ignore($"The pool doesn't accept any more deposits");
                setDummyValues(r, s, poolHash, idIdentityHash);
            }
            else if (response.Content.Contains($"No approval"))
            {
                Assert.Ignore($"No approval for deposit");
                setDummyValues(r, s, poolHash, idIdentityHash);
            }
            else if (response.Content.Contains("does not exist"))
            {
                Assert.Ignore($"Pool does not exist");
                setDummyValues(r, s, poolHash, idIdentityHash);
            }
            else if (response.Content.Contains($"Conflict"))
            {
                Assert.Ignore($"Approval is still in progress");
                setDummyValues(r, s, poolHash, idIdentityHash);
            }
            else if (response.Content.Contains("Pool is not available on blockchain"))
            {
                Assert.Ignore("Pool is not available on blockchain");
                setDummyValues(r, s, poolHash, idIdentityHash);
            }
            // Positive case
            else
            {
                account = (bool)content.SelectToken("IsCreateAccount");
                r = content.SelectToken("R").ToString();
                s = content.SelectToken("S").ToString();
                v = (int)content.SelectToken("V");
                poolHash = content.SelectToken("PoolHash").ToString();

                if (account == true)
                {
                    idIdentityHash = content.SelectToken("IdentityHash").ToString();  // Use when create a new account
                }
                else
                {
                    idIdentityHash = "";
                }
            }
            return (r, s, v, account, poolHash, idIdentityHash);
        }


        /// <summary>
        /// Set No Values
        /// </summary>
        private static void setDummyValues(string r, string s, string poolHash, string identityHash) 
        {
            r = "";
            s = "";
            poolHash = "";
            identityHash = "";
        }

        /// <summary>
        /// Get Mature Balance Hash
        /// </summary>
        /// <param name="poolId"></param>
        /// <param name="currency"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static List<string> getMatureBalanceHash(AddressItem investor, 
                                                        string poolId, 
                                                        ECurrency currency, 
                                                        string environment)
        {
            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/MatureBalanceHashes/{poolId}/{investor.Address}/{currency}", environment: environment),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(investor.PrivateKey)));

            // take care of Pool is not in Mature state same as in Deposit
            List<string> hashList = new List<string>();
            if (response.Content.Contains("is not matured yet")) {
                Assert.Ignore($"The Pool '{poolId}' is not in Mature state yet");
            } 
            else if (response.Content.Contains($"No matured balance to withdraw"))
            {
                Assert.Ignore($"No matured balance to withdraw for this investor {investor.Address} from this pool {poolId}");
            }
            else if (response.Content.Contains($"Sorry, we are processing the payment to Pool"))
            {
                Assert.Ignore($"Please add pool replayment using smart contract");
            }
            else
            {
                // Deserialization
                JArray items = (JArray)JObject.Parse(response.Content)["Hashes"];

                string[] obj = items.Select(jv => (string)jv).ToArray();

                hashList = obj.ToList();
            }
            return hashList;
           
        }

        /// <summary>
        /// Get fta account ID
        /// </summary>
        /// <param name="address"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetFtaAccountId(AddressItem address, string environment)
        {
            // Execute response
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V2/Accounts/Summary/IncludeFta/{address.Address}"),
                           Api.SendRequest(Method.GET)
                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment))
                              .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(address.PrivateKey)));

            // Deserialization
            JArray accounts = (JArray)JObject.Parse(response.Content)["Accounts"]; // Get all invest accounts
            string ftaAccountId = (string)accounts.Where(x => x.Value<string>("Type") == "Fta").First().SelectToken("ID"); // Get FTA account from list

            return ftaAccountId;
        }

        /// <summary>
        /// Get fta docs
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static List<string> GetFtaAgreementDocs(string environment, string userName = "qa.assertible")
        {
            // Execute 
            var documentIdResponse = Api.GetResponse(Api.SetGluwaApiUrl($"V1/KnowYourCustomers/AgreementDocuments/Fta/{"onboarding"}"),
                                                     Api.SendRequest(Method.GET)
                                                        .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment, userName))
                                                        .AddHeader("X-REQUEST-SIGNATURE", SignaturesUtil.GetXRequestSignature(QAKeyVault.GetGluwaAddress("Sender").PrivateKey)));

            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, documentIdResponse);

            // Pretty JSON
            JToken jsonObj = JArray.Parse(documentIdResponse.Content);
            List<string> ids = jsonObj.Values<string>("Id").ToList();
            
            return ids;
        }

        /// <summary>
        /// Set chain ID
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string SetChainId(string endpoint, ECurrency currency)
        {
            string endpointLocal = endpoint;
            // Execute response based on currency
            switch (currency)
            {
                case ECurrency.Usdc:
                    endpointLocal = $"{endpoint}";
                    break;
                case ECurrency.BnbUsdc:
                    endpointLocal = $"{endpoint}?ChainID={Shared.CHAIN_ID_BSC_TEST}";
                    break;
                default:
                    Assert.Ignore($"Currency {currency} does not exist or unsupported");
                    break;
            }
            return endpointLocal;
        }
    }
}

