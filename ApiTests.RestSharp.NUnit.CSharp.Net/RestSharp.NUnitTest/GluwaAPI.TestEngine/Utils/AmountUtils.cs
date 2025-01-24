using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace GluwaAPI.TestEngine.Utils
{

    public class AmountUtils
    {
        //private static EConversion conversion;
        private static decimal remainingAmount;

        /// <summary>
        /// Convert gluwacoin amounts to the correct precision factor
        /// </summary>
        /// <param name="amountToConvert"></param>
        /// <returns></returns>
        public static BigInteger ConvertToGluwacoinPrecisionFactor(string amountToConvert, int factor)
        {
            BigDecimal bigDecimalAmount = BigDecimal.Parse(amountToConvert);
            return BigInteger.Parse((bigDecimalAmount * new BigDecimal(1, factor)).Floor().ToString());
        }

        /// <summary>
        /// Convert gluwacoin amount to special format of a big integer
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static BigInteger ConvertToGluwacoinBigInteger(string amount, int decimals)
        {
            int decimalPlaces = decimals;
            string[] splitStrings = amount.Split('.');

            string integralString = splitStrings[0];
            BigInteger integralPart = BigInteger.Parse(integralString);
            BigInteger decimalPart = 0;
            if (splitStrings.Length == 2)
            {
                string decimalString = splitStrings[1];
                decimalString = decimalString.PadRight(decimalPlaces, '0').Substring(0, decimalPlaces);
                decimalPart = BigInteger.Parse(decimalString);
            }

            return integralPart * BigInteger.Pow(10, decimalPlaces) + decimalPart;
        }

        /// <summary>
        /// Calulcates the Order amount and price based on the quote amount 
        /// QAs want to test. 
        /// We calculate based on quote amount since we want to try to match the quote with an order
        /// </summary>
        /// <param name="conversion"></param>
        /// <param name="quoteAmount"></param>
        /// <returns>Order Source Amount and Price</returns>
        public static (decimal, decimal) GetMatchedOrderAmountAndPrice(EConversion takerConversion, decimal quoteAmount)
        {
            // Calculate the least possible amount we could convert the takerAmount to
            decimal expectedConvertedAmount = takerConversion.ToExchangeCurrency().ToDefaultCurrencyAmount(); // In the exchanged currency         
            decimal price = Math.Round(quoteAmount / expectedConvertedAmount, 9); // Get the price to convert to the least possible amount

            // Calculate the Order Source Amount based on the NetworkFee
            var fee = "";
            if (takerConversion.ToExchangeCurrency() == ECurrency.sUsdcg)
            {
                fee = GetFee(takerConversion.ToExchangeCurrency(), expectedConvertedAmount.ToString());
            }
            else
            {
                fee = GetFee(takerConversion.ToExchangeCurrency());
            }
            decimal networkFee = takerConversion.ToNetworkFee(decimal.Parse(fee));

            decimal sourceAmount = expectedConvertedAmount + networkFee;
            return (sourceAmount, price);
        }

        /// <summary>
        /// Checks if there are unspent outputs available
        /// Ignores test if there aren't
        /// </summary>
        /// <param name="response">content response of the endpoint</param>
        /// <param name="address"> btc address</param>
        public static void CheckUnspentOutputs(string response, string address)
        {
            if (response.Contains("unspent output"))
            {
                Console.WriteLine($"There are no unspent outputs available in {address}.");
                Assert.Ignore("Please add unspent outputs");
            }
        }

        /// <summary>
        /// Returns nonce for gluwacoin transactions
        /// </summary>
        /// <returns></returns>
        public static string GetNonce(int nonceDigits = 75)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[8];

            StringBuilder sb = new StringBuilder();
            do
            {
                rng.GetBytes(buffer);
                string rngToString = (BitConverter.ToUInt64(buffer, 0)).ToString();

                sb.Append(rngToString);

            } while (sb.Length < nonceDigits);

            rng.Dispose();
            string nonceString = sb.ToString(0, nonceDigits);

            return nonceString;
        }

        /// <summary>
        /// Gets gluwa fee
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="environment"></param>
        /// <returns>
        /// Fee based on currency and environment
        /// </returns>
        public static string GetFee(ECurrency currency, dynamic environment = null)
        {
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Fee", environment: environment), Api.SendRequest(Method.GET));

            // handle response code for set up
            Assertions.HandleSetupAssertions(200, response);
            string fee = Api.GetResponseContentTokenPath(response.Content, "MinimumFee");

            return fee;
        }

        /// <summary>
        /// Gets gluwa fee
        /// </summary>
        /// <param name="source"></param>
        /// <returns>
        /// Fee based on currency and environment
        /// </returns>
        public static string GetFee(ECurrency currency)
        {
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Fee"), Api.SendRequest(Method.GET));

            // handle response code for set up
            Assertions.HandleSetupAssertions(200, response);
            string fee = Api.GetResponseContentTokenPath(response.Content, "MinimumFee");

            return fee;
        }

        /// <summary>
        /// Gets gluwa fee for given amount
        /// </summary>
        /// <param name="source"></param>
        /// <returns>
        /// Fee based on currency and amount
        /// </returns>
        public static string GetFee(ECurrency currency, string amount)
        {
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/{currency}/Fee?amount={amount}"), Api.SendRequest(Method.GET));

            // handle response code for set up
            Assertions.HandleSetupAssertions(200, response);
            string fee = Api.GetResponseContentTokenPath(response.Content, "MinimumFee");

            return fee;
        }

        /// <summary>
        /// Calculate QRCode amount from fee
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public decimal CalculateQRCodeAmountFromFee(decimal amount, ECurrency currency)
        {
            return decimal.Parse(GetFee(currency)) + amount;
        }

        /// <summary>
        /// Calculate QRCode sUsdcg amount from fee
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public decimal CalculateQRCodeAmountFromFee(decimal amount)
        {
            return decimal.Parse(GetFee(ECurrency.sUsdcg, amount.ToString())) + amount;
        }

        /// <summary>
        /// Calculates the source amount and price that would lead to a complete order
        /// </summary>
        /// <param name="conversion"></param>
        /// <param name="quoteAmount"></param>
        /// <returns></returns>
        public static (decimal, decimal) calculateCompleteOrderSourceAmount(EConversion conversion, decimal quoteAmount)
        {
            (decimal sourceAmount, decimal price) = GetMatchedOrderAmountAndPrice(conversion, quoteAmount);
            remainingAmount = conversion.ToExchangeCurrency().ToAmountTooSmallCurrencyAmount(); // remaining amount must be less than the valid quote amount
            sourceAmount += remainingAmount;

            return (sourceAmount, price);
        }

        /// <summary>
        /// Get Safe Gas Price
        /// </summary>
        /// <returns></returns>
        public static BigInteger GetSafeGasPrice()
        {
            try
            {
                var client = new RestClient("https://api.etherscan.io");
                var request = new RestRequest("/api?module=gastracker&action=gasoracle&apikey=YourApiKeyToken");
                var response = client.Post(request);
                dynamic responseJson = JsonConvert.DeserializeObject(response.Content);
                var safeGasPrice = responseJson.result.SafeGasPrice.ToString();
                var s = AmountUtils.ConvertToGluwacoinPrecisionFactor(safeGasPrice, 9);
                return s;
            }
            catch
            {
                return new BigInteger(35_000_000_000); // Use default value for gas price if the API fails
            }
        }

        /// <summary>
        /// Get Safe Gas Price For currency
        /// </summary>
        /// <returns></returns>
        public static (BigInteger, BigInteger) GetSafeGasPrice(string currency)
        {
            try
            {
                var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaInvestment/Fees?currency={currency}"),
                                               Api.SendRequest(Method.GET));
                // Deserialization
                JToken jsonObj = JToken.Parse(response.Content);
                dynamic data = JObject.Parse(jsonObj.ToString());
                BigInteger bigDecimalGas = BigInteger.Parse((string)data.SelectToken("GasPrice"));
                BigInteger bigDecimalGasLimit = BigInteger.Parse((string)data.SelectToken("CreateBalanceGasLimit"));
                return (bigDecimalGas, bigDecimalGasLimit);
            }
            catch
            {
                return (new BigInteger(35_000_000_000), new BigInteger(2_000_000_000)); // Use default value for gas price if the API fails
            }
        }

        [Function("transfer", "bool")]
        public class TransferFunction : FunctionMessage
        {
            [Parameter("address", "_to", 1)]
            public string To { get; set; }

            [Parameter("uint256", "_value", 2)]
            public BigInteger SendAmount { get; set; }
        }
    }
}