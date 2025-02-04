using Gluwa.SDK_dotnet.Models.Exchange;
using Gluwa.SDK_dotnet.Tests.Models;
using NBitcoin;
using Nethereum.Signer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gluwa.SDK_dotnet.Tests
{
    public class SharedMethods
    {
        public static bool BASE_ENV;
        public static string BASE_URL;
        public static string API_KEY;
        public static string API_SECRET;
        public static string TxID_USDG;
        public static string TxID_sNGNG;
        public static string TxID_sUSDCG;       
        public static string SRC_ADR_KRWG;
        public static string SRC_PRIVATE_KRWG;
        public static string SRC_ADR_USDG;
        public static string SRC_PRIVATE_USDG;
        public static string SRC_ADR_sUSDCG;
        public static string SRC_PRIVATE_sUSDCG;
        public static string SRC_ADR_sNGNG;
        public static string SRC_PRIVATE_sNGNG;
        public static string SRC_ADR_BTC;
        public static string SRC_PRIVATE_BTC;
        public static string TRG_ADR_KRWG;
        public static string TRG_PRIVATE_KRWG;
        public static string TRG_ADR_USDG;
        public static string TRG_PRIVATE_USDG;
        public static string TRG_ADR_sNGNG;
        public static string TRG_ADR_sUSDCG;
        public static string TRG_PRIVATE_sUSDCG;
        public static string TRG_ADR_BTC;
        public static string TRG_PRIVATE_BTC;

        //ENV
        public static string BASE_URL_SANDBOX = "*************************";
        public static string BASE_URL_PROD = "*************************";
        public static string WEBHOOK_URL = "*************************";
        public static string BASE_URL_TEST = "*************************";

        //USDG
        public static string SRC_ADR_USDG_PROD = "*************************";
        public static string SRC_ADR_USDG_SANDBOX = "*************************";
        public static string SRC_PRIVATE_USDG_PROD = "*************************";
        public static string SRC_PRIVATE_USDG_SANDBOX = "*************************";
        public static string TRG_ADR_USDG_PROD = "*************************";
        public static string TRG_ADR_USDG_SANDBOX = "*************************";
        public static string TRG_PRIVATE_USDG_PROD = "*************************";
        public static string TRG_PRIVATE_USDG_SANDBOX = "*************************";
        public static string TxID_USDG_SANDBOX = "*************************";
        public static string TxID_USDG_PROD = "*************************";
        public static string DEST_ADR_USDG = "*************************";

        //sUSDC-G
        public static string SRC_ADR_sUSDCG_PROD = "*************************";
        public static string SRC_ADR_sUSDCG_SANDBOX = "*************************";
        public static string SRC_PRIVATE_sUSDCG_PROD = "*************************";
        public static string SRC_PRIVATE_sUSDCG_SANDBOX = "*************************";
        public static string TRG_ADR_sUSDCG_PROD = "*************************";
        public static string TRG_ADR_sUSDCG_SANDBOX = "*************************";
        public static string TRG_PRIVATE_sUSDCG_PROD = "*************************";
        public static string TRG_PRIVATE_sUSDCG_SANDBOX = "*************************";
        public static string TxID_sUSDCG_SANDBOX = "*************************";
        public static string TxID_sUSDCG_PROD = "*************************";
        public static string DEST_ADR_sUSDCG = "*************************";

        //sNGNG
        public static string SRC_ADR_sNGNG_PROD = "*************************";
        public static string SRC_ADR_sNGNG_SANDBOX = "*************************";
        public static string SRC_PRIVATE_sNGNG_PROD = "*************************";
        public static string SRC_PRIVATE_sNGNG_SANDBOX = "*************************";
        public static string TxID_sNGNG_PROD = "*************************";
        public static string TxID_sNGNG_SANDBOX = "*************************";
        public static string TRG_ADR_sNGNG_PROD = "*************************";
        public static string TRG_ADR_sNGNG_SANDBOX = "*************************";

        //KRWG
        public static string SRC_ADR_KRWG_PROD = "*************************";
        public static string SRC_ADR_KRWG_SANDBOX = "*************************";
        public static string SRC_PRIVATE_KRWG_PROD = "*************************";
        public static string SRC_PRIVATE_KRWG_SANDBOX = "*************************";
        public static string TRG_ADR_KRWG_PROD = "*************************";
        public static string TRG_ADR_KRWG_SANDBOX = "*************************";
        public static string TRG_PRIVATE_KRWG_PROD = "*************************";
        public static string TRG_PRIVATE_KRWG_SANDBOX = "*************************";
        public static string DEST_ADR_KRWG = "*************************";

        //BTC
        public static string SRC_ADR_BTC_PROD = "*************************";
        public static string SRC_ADR_BTC_SANDBOX = "*************************";
        public static string SRC_PRIVATE_BTC_PROD = "*************************";
        public static string SRC_PRIVATE_BTC_SANDBOX = "*************************";
        public static string TRG_ADR_BTC_PROD = "*************************";
        public static string TRG_ADR_BTC_SANDBOX = "*************************";
        public static string TRG_PRIVATE_BTC_PROD = "*************************";
        public static string TRG_PRIVATE_BTC_SANDBOX = "*************************";
        public static string DEST_ADR_BTC = "*************************";

        //MISCELLANEA
        public static string KRWG_USDG_ORDER_ID = "*************************";
        public static string BTC_QUOTE_ID = "*************************";
        public static string API_KEY_SANDBOX = "*************************";
        public static string API_SECRET_SANDBOX = "*************************";
        public static string API_KEY_TEST = "*************************";
        public static string API_SECRET_TEST = "*************************";
        public static string API_KEY_PROD = "*************************";
        public static string API_SECRET_PROD = "*************************";
        public static string WEBHOOK_USER_ID = "*************************";
        public static string EXCHANGE_ID = "*************************";
        public static string EXECUTOR = "*************************";
        public static string EXPIRYBLOCK_NO = "*************************";
        public static string FUNDS_ADR = "*************************";
        public static string FUNDS_SCRIPT = "*************************";

        //INVALID TEST DATA
        public static string INVALID_ADDRESS = "*************************";
        public static string INVALID_PRIVATE_KEY = "*************************";
        public static string invalidTxID = "*************************";
        public static string INVALID_API_KEY = "*************************";
        public static string INVALID_API_SECRET = "*************************";
        public static string INVALID_QUOTEID = "*************************";
        public static string INVALID_ORDERID = "*************************";
        public static string INVALID_BLOCK_NO = "*************************";

        //SET ENVIRONMENT VARIABLES
        public static void SetEnvironmentVariables(string environmment = "Sandbox")
        {
            switch (environmment)
            {
                case "Production":
                    BASE_ENV = false;
                    BASE_URL = BASE_URL_PROD;
                    API_KEY = API_KEY_PROD;
                    API_SECRET = API_SECRET_PROD;
                    TxID_USDG = TxID_USDG_PROD;
                    TxID_sNGNG = TxID_sNGNG_PROD;
                    TxID_sUSDCG = TxID_sUSDCG_PROD;

                    SRC_ADR_BTC = SRC_ADR_BTC_PROD;
                    SRC_ADR_KRWG = SRC_ADR_KRWG_PROD;
                    SRC_ADR_USDG = SRC_ADR_USDG_PROD;
                    SRC_ADR_sNGNG = SRC_ADR_sNGNG_PROD;
                    SRC_ADR_sUSDCG = SRC_ADR_sUSDCG_PROD;

                    SRC_PRIVATE_BTC = SRC_PRIVATE_BTC_PROD;
                    SRC_PRIVATE_KRWG = SRC_PRIVATE_KRWG_PROD;
                    SRC_PRIVATE_USDG = SRC_PRIVATE_USDG_PROD;
                    SRC_PRIVATE_sNGNG = SRC_PRIVATE_sNGNG_PROD;
                    SRC_PRIVATE_sUSDCG = SRC_PRIVATE_sUSDCG_PROD;

                    TRG_ADR_BTC = TRG_ADR_BTC_PROD;
                    TRG_ADR_KRWG = TRG_ADR_KRWG_PROD;
                    TRG_ADR_USDG = TRG_ADR_USDG_PROD;
                    TRG_ADR_sNGNG = TRG_ADR_sNGNG_PROD;
                    TRG_ADR_sUSDCG = TRG_ADR_sUSDCG_PROD;

                    TRG_PRIVATE_BTC = TRG_PRIVATE_BTC_PROD;
                    TRG_PRIVATE_KRWG = TRG_PRIVATE_KRWG_PROD;
                    TRG_PRIVATE_USDG = TRG_PRIVATE_USDG_PROD;
                    TRG_PRIVATE_sUSDCG = TRG_PRIVATE_sUSDCG_PROD;
                    break;

                default:
                    BASE_ENV = true;
                    BASE_URL = BASE_URL_SANDBOX;
                    API_KEY = API_KEY_SANDBOX;
                    API_SECRET = API_SECRET_SANDBOX;
                    TxID_USDG = TxID_USDG_SANDBOX;
                    TxID_sNGNG = TxID_sNGNG_SANDBOX;
                    TxID_sUSDCG = TxID_sUSDCG_SANDBOX;

                    SRC_ADR_BTC = SRC_ADR_BTC_SANDBOX;
                    SRC_ADR_KRWG = SRC_ADR_KRWG_SANDBOX;
                    SRC_ADR_USDG = SRC_ADR_USDG_SANDBOX;
                    SRC_ADR_sNGNG = SRC_ADR_sNGNG_SANDBOX;
                    SRC_ADR_sUSDCG = SRC_ADR_sUSDCG_SANDBOX;

                    SRC_PRIVATE_BTC = SRC_PRIVATE_BTC_SANDBOX;
                    SRC_PRIVATE_KRWG = SRC_PRIVATE_KRWG_SANDBOX;
                    SRC_PRIVATE_USDG = SRC_PRIVATE_USDG_SANDBOX;
                    SRC_PRIVATE_sNGNG = SRC_PRIVATE_sNGNG_SANDBOX;
                    SRC_PRIVATE_sUSDCG = SRC_PRIVATE_sUSDCG_SANDBOX;

                    TRG_ADR_BTC = TRG_ADR_BTC_SANDBOX;
                    TRG_ADR_KRWG = TRG_ADR_KRWG_SANDBOX;
                    TRG_ADR_USDG = TRG_ADR_USDG_SANDBOX;
                    TRG_ADR_sNGNG = TRG_ADR_sNGNG_SANDBOX;
                    TRG_ADR_sUSDCG = TRG_ADR_sUSDCG_SANDBOX;

                    TRG_PRIVATE_BTC = TRG_PRIVATE_BTC_SANDBOX;
                    TRG_PRIVATE_KRWG = TRG_PRIVATE_KRWG_SANDBOX;
                    TRG_PRIVATE_USDG = TRG_PRIVATE_USDG_SANDBOX;
                    TRG_PRIVATE_sUSDCG = TRG_PRIVATE_sUSDCG_SANDBOX;
                    break;
            }
        }

        //SHARED METHODS

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

                byte[] plainTextBytes = System.Text.Encoding.ASCII.GetBytes(message + "." + signedMessage);
                xRequestSignature = System.Convert.ToBase64String(plainTextBytes);
            }
            else
            {
                // for btc check if it's in main or test
                string gluwaApi = $"{BASE_URL_TEST}";
                Network network = Network.Main;
                if (gluwaApi.Contains("test"))
                {
                    network = Network.TestNet;
                }

                // generate XRequestSignature for BTC
                BitcoinSecret secret = new BitcoinSecret(privateKey, network);
                string signedMessage = secret.PrivateKey.SignMessage(message);

                byte[] plainTextBytes = Encoding.ASCII.GetBytes($"{message}.{signedMessage}");
                xRequestSignature = Convert.ToBase64String(plainTextBytes);
            }
            return xRequestSignature;
        }

        /// <summary>
        /// Creates an order and asserts if order has been made
        /// </summary>
        /// <param name="sender">Sending Address information</param>
        /// <param name="receiver">Receiver Address information</param>
        /// <returns></returns>
        public static string PostOrder(EConversion conversion,
                                       string sourceAddress,
                                       string sourcePrivate,
                                       string targetAddress,
                                       string targetPrivate,
                                       string url)
        {
            // Get Exchange SetUp Method 
            PostOrderBody body = CreatePostOrderBody(conversion, sourceAddress, sourcePrivate, targetAddress, targetPrivate);
            IRestResponse orderResponse = Api.GetResponse(Api.SetUrl(url, "v1/Orders"), Api.SendRequest(Method.POST, body));

            return JObject.Parse(orderResponse.Content).SelectToken("ID").ToString();

        }

        /// <summary>
        /// Create body for POST v1/Orders
        /// </summary>
        /// <param name="conversion"></param>
        /// <param name="sender">AddressItem</param>
        /// <param name="receiver">AddressItem</param>
        /// <returns></returns>
        public static PostOrderBody CreatePostOrderBody(EConversion conversion,
                                                        string sourceAddress,
                                                        string sourcePrivate,
                                                        string targetAddress,
                                                        string targetPrivate)
        {

            PostOrderBody body = new PostOrderBody()
            {
                Conversion = conversion.ToString(),
                SendingAddress = sourceAddress,
                SendingAddressSignature = GetXRequestSignature(sourcePrivate),
                ReceivingAddress = targetAddress,
                ReceivingAddressSignature = GetXRequestSignature(targetPrivate),
                SourceAmount = "1000",
                Price = "0.001"

            };
            return body;
        }

        /// <summary>
        /// Returns a quote ID based on an address
        /// </summary>
        /// <returns></returns>
        public static string GetQuoteByAddress(string currency, string address, string privateKey)
        {
            // Arrange 
            IRestResponse response = Api.GetResponse(Api.SetUrlAndClient($"{BASE_URL_TEST}", $"v1/{currency}/Addresses/{address}/Quotes"),
                                     Api.SendRequest(Method.GET, privateKey));
            // Trace response
            TestContext.WriteLine($"Response Content: \"{response.Content}\"");
            TestContext.WriteLine($"Response Status Code: {response.StatusCode}");

            // Parse
            JArray jArray = JArray.Parse(response.Content);
            JObject jObject = jArray.Children<JObject>().LastOrDefault();
            string quoteId = jObject.SelectToken("ID").ToString();

            return quoteId;
        }


        /// <summary>
        /// Calls the exchange request webhook endpoint
        /// </summary>
        /// <returns>Latest exchange made</returns>
        public static GetExchangeRequestResponse GetExchangeRequest(string conversion)
        {
            IRestResponse response = Api.GetResponse(Api.SetUrlAndClient(WEBHOOK_URL, "ExchangeRequest"),
                                                     Api.SendRequest(Method.GET)
                                                     .AddQueryParameter("userID", WEBHOOK_USER_ID)
                                                     .AddQueryParameter("limit", "1000"));
            // Trace response
            TestContext.WriteLine($"ExchangeRequest response: {response.Content}");

            // Try to return exchange list
            try
            {
                List<GetExchangeRequestResponse> exchangeRequestList = JsonConvert.DeserializeObject<List<GetExchangeRequestResponse>>(response.Content);
                List<GetExchangeRequestResponse> exchangeRequestListTest = exchangeRequestList.Where(er => er.Conversion == conversion).ToList();
                return exchangeRequestListTest[0];
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine(ex);
            }

            return null;
        }
    }
}
