using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Azure;

namespace GluwaAPI.TestEngine.Setup
{
    public class QAKeyVault
    {
        /// <summary>
        /// Get Gluwa Address as either a Sender (Source) or Receiver (Target)
        /// </summary>
        /// <param name="addressType"></param>
        /// <returns></returns>
        public static Models.AddressItem GetGluwaAddress(string addressType)
        {
            return getAddress(formatGluwaAddressByType(addressType));
        }

        /// <summary>
        /// Get Btc Address either as a Sender (Source) or Receiver (Target)
        /// Either Testnet or Main net for environment
        /// </summary>
        /// <param name="addressType"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static Models.AddressItem GetBtcAddress(string addressType, string environment)
        {
            return getAddress(formatBtcAddressByTypeAndEnvironment(addressType, environment));
        }

        /// <summary>
        /// Get Gluwa Maker or Taker Address
        /// </summary>
        /// <param name="senderAddressKeyName">SenderAddress name</param>
        /// <param name="receiverAddressKeyName">ReceiverAddress name</param>
        /// <returns></returns>
        public static Models.ExchangeAddressItem GetGluwaExchangeAddress(string senderAddressKeyName, string receiverAddressKeyName)
        {
            var senderAddress = getAddress(formatGluwaAddressByType(senderAddressKeyName));
            var receiverAddress = getAddress(formatGluwaAddressByType(receiverAddressKeyName));

            return new Models.ExchangeAddressItem
            {
                Sender = senderAddress,
                Receiver = receiverAddress
            };
        }

        /// <summary>
        /// Returns ExchangeAddress for Btc -> Gluwacoin 
        /// </summary>
        /// <param name="senderAddressKeyName">Btc SendeAddress name</param>
        /// <param name="receiverAddressKeyName">Btc receiver Address name</param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static Models.ExchangeAddressItem GetBtcGluwacoinExchangeAddress(string senderAddressKeyName, string receiverAddressKeyName, string environment)
        {

            return new Models.ExchangeAddressItem
            {
                Sender = createBtcAddressWithPublicKey(senderAddressKeyName, environment),
                Receiver = getAddress(formatGluwaAddressByType(receiverAddressKeyName))
            };

        }

        /// <summary>
        /// Returns ExchangeAddress for Gluwacoin -> BTC conversions
        /// </summary>
        /// <param name="senderAddressKeyName"></param>
        /// <param name="receiverAddressKeyName"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static Models.ExchangeAddressItem GetGluwacoinBtcExchangeAddress(string senderAddressKeyName, string receiverAddressKeyName, string environment)
        {
            return new Models.ExchangeAddressItem
            {
                Sender = getAddress(formatGluwaAddressByType(senderAddressKeyName)),
                Receiver = getAddress(formatBtcAddressByTypeAndEnvironment(receiverAddressKeyName, environment))
            };
        } 

        /// <summary>
        /// Returns the address from the QA key Vault using a keyname
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        private static Models.AddressItem getAddress(string keyName)
        {
            var secret = getSecretFromKeyVaulAsync(keyName).Result;

            string address = secret.Value.Properties.ContentType;
            string privateKey = secret.Value.Value;

            if (!isBitcoin(keyName))
            {
                address = "0x" + address;
                privateKey = "0x" + privateKey;
            }

            return new Models.AddressItem
            {
                Address = address,
                PrivateKey = privateKey
            };
        }

        /// <summary>
        /// Returns secret from keyvault by name
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        private static async Task<Response<KeyVaultSecret>> getSecretFromKeyVaulAsync(string keyName)
        {
            string keyVaultName = "Gluwa-QA-Addresses";
            var kvUri = "https://" + keyVaultName + ".vault.azure.net";
            var secret = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            return await secret.GetSecretAsync(keyName);
        }

        private static bool isBitcoin(string name)
        {
            string lowercaseName = name.ToLower();
            return lowercaseName.Contains("btc") || lowercaseName.Contains("bitcoin");
        }

        /// <summary>
        /// Returns a BTC address that contains the public key
        /// </summary>
        /// <param name="addressType"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        private static Models.AddressItem createBtcAddressWithPublicKey(string addressType, string environment)
        {
            var senderSecret = getSecretFromKeyVaulAsync(formatBtcAddressByTypeAndEnvironment(addressType, environment)).Result;
            return new Models.AddressItem
            {
                Address = senderSecret.Value.Properties.ContentType,
                PrivateKey = senderSecret.Value.Value,
                PublicKey = senderSecret.Value.Properties.Tags["publicKey"]
            };
        }

        /// <summary>
        /// Formats Gluwa Address whether Sender/Receiver/Execute Fail
        /// </summary>
        /// <param name="addressType"></param>
        /// <returns></returns>
        private static string formatGluwaAddressByType(string addressType)
        {
            return string.Format($"Gluwacoin{addressType}Address");
        }

        /// <summary>
        /// Formats BTC address whether Sender/Reciever/ExecuteFail/MarketMaker
        /// And also if testnet or mainnet
        /// </summary>
        /// <param name="addressType"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        private static string formatBtcAddressByTypeAndEnvironment(string addressType, string environment)
        {
            if (ApiController.Api.IsTestEnvironment(environment))
                return string.Format($"BitcoinTestnet{addressType}Address");
            else
                return string.Format($"BitcoinMainnet{addressType}Address");
        }
    }
}
