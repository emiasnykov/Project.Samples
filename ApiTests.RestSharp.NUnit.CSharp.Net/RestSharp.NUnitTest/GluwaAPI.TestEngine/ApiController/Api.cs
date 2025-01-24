using GluwaAPI.TestEngine.Setup;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace GluwaAPI.TestEngine.ApiController
{
    public class Api : GluwaTestApi
    {
        /// <summary>
        /// Return Response
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static IRestResponse GetResponse(RestClient client, IRestRequest request)
        {
            return client.Execute(request);
        }

        /// <summary>
        /// Executes requests with body (PATCH/PUT/POST)
        /// </summary>
        /// <param name="method">HttpMethod</param>
        /// <param name="body">Body of the test</param>
        /// <returns></returns>
        public static IRestRequest SendRequest(Method method, object body)
        {
            var result = new RestRequest(method)
                   .AddHeader("Content-Type", "application/json")
                   .AddJsonBody(body);
            return result;
        }

        /// <summary>
        /// Executes Get request
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static IRestRequest SendRequest(Method method)
        {
            return new RestRequest(method)
                .AddHeader("Content-Type", "application/json");
            //.AddHeader("Accept-Language", "ko-KR");       // Keep to test Korean text messages     
        }

        /// <summary>
        /// Set UrlAndClient
        /// </summary>
        /// <param name="url"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static RestClient SetUrlAndClient(string url, string endpoint)
        {
            return new RestClient(url + endpoint);
        }

        /// <summary>
        /// Set GluwaApi client
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static RestClient SetGluwaApiUrlWithAuth(string endpoint)
        {
            RestClient client = SetGluwaApiUrl(endpoint);
            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(ApiKey, ApiSecret);
            return client;
        }

        /// <summary>
        /// Set GluwaApi client
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static RestClient SetGluwaApiUrlWithAuth(string endpoint, string environment = "Test")
        {
            RestClient client = SetGluwaApiUrl(endpoint, environment: environment);
            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(ApiKey, ApiSecret);
            return client;
        }

        /// <summary>
        /// Set GluwaApi client
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="environment"></param>
        /// <param name="rngNo"></param>
        /// <returns></returns>
        public static RestClient SetGluwaApiUrlWithAuth(string endpoint, string rngNo = "1823", string environment = "Test")
        {
            RestClient client = SetGluwaApiUrl(endpoint, rngNo: rngNo, environment: environment);
            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(ApiKey, ApiSecret);
            return client;
        }

        /// <summary>
        /// Set Gluwa Api URL
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static RestClient SetGluwaApiUrl(string endpoint)
        {
            return new RestClient(GluwaApiUrl + endpoint);
        }

        /// <summary>
        /// Set Gluwa Api URL with support for Eph environments
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="rngNo"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static RestClient SetGluwaApiUrl(string endpoint, string rngNo = "1878", string environment = "")
        {
            if (environment.Equals("EphEnv"))
            {
                return new RestClient(GluwaApiUrl.Replace("xxx", rngNo) + endpoint);
            }

            return new RestClient(GluwaApiUrl + endpoint);
        }

        /// <summary>
        /// Set GluwaTest2Api client
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static RestClient SetGluwaTest2ApiUrlWithAuth(string endpoint)
        {
            RestClient client = SetGluwa2ApiUrl(endpoint);
            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(ApiKey, ApiSecret);
            return client;
        }

        /// <summary>
        /// Set GluwaTest2Api url
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static RestClient SetGluwa2ApiUrl(string endpoint)
        {
            return new RestClient(GluwaTest2ApiUrl + endpoint);
        }

        /// <summary>
        /// Set webhook reciever client
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static RestClient SetWebhookReceiver(string endpoint)
        {
            RestClient client = new RestClient(WebhookReceiver + endpoint)
            {
                Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(GluwaTestApi.ApiKey, GluwaTestApi.ApiSecret)
            };
            return client;
        }

        /// <summary>
        /// QA Functions client
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static RestClient SetQAFunctions(string endpoint)
        {
            RestClient client = new RestClient(QAFunctions + endpoint)
            {
                Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(GluwaTestApi.ApiKey, GluwaTestApi.ApiSecret)
            };
            return client;
        }


        /// <summary>
        /// QA Functions exchange client
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static RestClient SetQAFunctionsExchange(string endpoint)
        {
            RestClient client = new RestClient(QAFunctionsExchange + endpoint)
            {
                Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(GluwaTestApi.ApiKey, GluwaTestApi.ApiSecret)
            };
            return client;
        }

        /// <summary>
        /// Get Response content object
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetResponseContentTokenPath(string content, string path)
        {
            return JObject.Parse(content).SelectToken(path).ToString();
        }

        /// <summary>
        /// Check the environment
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static bool IsTestEnvironment(string environment)
        {
            return (environment.ToLower() == "test") || (environment.ToLower() == "sandbox");
        }

        /// <summary>
        /// Check the currency is bitcoin
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static bool IsBitcoin(CurrencyUtils.ECurrency currency)
        {
            return (currency == CurrencyUtils.ECurrency.Btc);
        }

        /// <summary>
        /// Returns bearer token from auth api
        /// </summary>
        /// <param name="isTestEnvironment"></param>
        /// <returns></returns>
        public static string GetBearerToken(string environment, string userName = "*********", string pass = "***********", string rngNo = "**********")
        {
            string tokenBody;
            string authUrl;
            if (IsTestEnvironment(environment) && (environment != "Sandbox"))    // Test environment  
            {
                // identification for GluwacoinSenderAddress
                tokenBody = $"******************************************";
                authUrl = "************************";
            }
            else if (environment == "EphEnv") // Rng environment  
            {
                tokenBody = $"*********************************************";
                authUrl = GluwaAuthUrl.Replace("xxx", rngNo);
            }
            else if (environment == "Sandbox")    // Sandbox environment
            {
                // identification for GluwacoinSenderAddress
                tokenBody = $"**********************************************";
                authUrl = "*****************************";
            }
            else if (environment == "Staging")    // Staging environment
            {
                // identification for GluwacoinMainnetSenderAddress
                tokenBody = $"************************************************";
                authUrl = "************************************";
            }
            else                                   // Production environment
            {
                // identification for GluwacoinSenderAddress
                tokenBody = $"**************************************************";
                authUrl = "*******************************";
            }

            var body = System.Text.Encoding.UTF8.GetBytes(tokenBody);
            var authRequest = new RestRequest(Method.POST).
                              AddHeader("Content-Type", "application/x-www-form-urlencoded")
                              .AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);
            var response = GetResponse(SetUrlAndClient(authUrl, "connect/token"), authRequest);
            NUnit.Framework.Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            var jObj = JObject.Parse(response.Content);

            return jObj.GetValue("access_token").ToString();
        }
    }
}
