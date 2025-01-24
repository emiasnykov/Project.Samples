using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;

namespace GluwaAPI.TestEngine.Setup
{

    public partial class GluwaTestApi
    {
        public static EUserType QAGluwaUser { get; set; }

        public static string InitializeGluwaApi { get; set; }

        /// <summary>
        /// The base api for an given environment
        /// </summary>
        public static string GluwaApiUrl
        {
            get
            {
                return EnvironmentInfo.SelectToken("gluwaApi").ToString();
            }
        }


        /// <summary>
        /// The base api for an given environment
        /// </summary>
        public static string GluwaAuthUrl
        {
            get
            {
                return EnvironmentInfo.SelectToken("gluwaAuthUrl").ToString();
            }
        }


        /// <summary>
        /// The secondary api for an given environment
        /// </summary>
        public static string GluwaTest2ApiUrl
        {
            get
            {
                return EnvironmentInfo.SelectToken("gluwaTest2Api").ToString();
            }
        }


        /// <summary>
        /// Webhook receiver
        /// </summary>
        public static string WebhookReceiver
        {
            get
            {
                return EnvironmentInfo.SelectToken("webhookReceiver").ToString();
            }

        }

        /// <summary>
        /// QA functions exchange app
        /// </summary>
        public static string QAFunctionsExchange
        {
            get
            {
                return EnvironmentInfo.SelectToken("qafunctionsexchange").ToString();
            }
        }

        /// <summary>
        /// QA Functions app
        /// </summary>
        public static string QAFunctions
        {
            get
            {
                return EnvironmentInfo.SelectToken("qafunctions").ToString();
            }
        }


        /// <summary>
        /// ApiKey
        /// </summary>
        public static string ApiKey
        {
            get
            {
                return mCurrentUser.SelectToken("apiKey").ToString();
            }
        }



        /// <summary>
        /// ApiSecret
        /// </summary>
        public static string ApiSecret
        {
            get
            {
                return mCurrentUser.SelectToken("apiSecret").ToString();

            }
        }

        /// <summary>
        /// Webhook User ID for ExchangeRequest
        /// </summary>
        public static string WebhookUserID
        {
            get
            {
                return mCurrentUser.SelectToken("webhookUserID").ToString();
            }
        }


        /// <summary>
        /// Grabs the environment info from Json
        /// </summary>
        public static JToken EnvironmentInfo
        {
            get
            {

                string path = Path.Combine(getFolderPath("GluwaAPI.TestEngine"), "Settings/environment.json");
                JObject jObj = StreamFileTextIntoObject(path);
                JToken environmentInfo = jObj.SelectToken(InitializeGluwaApi);

                return environmentInfo;
            }
        }



        /// <summary>
        /// Returns order expiry date
        /// </summary>
        public static int OrderExpiryInMinutes
        {
            get
            {
                return int.Parse(EnvironmentInfo.SelectToken("orderExpiryInMinutes").ToString());
            }
        }


        /// <summary>
        /// Network for environment
        /// </summary>
        public static NBitcoin.Network CurrentNetwork
        {
            get
            {
                if (GluwaApiUrl.Contains("test") || GluwaApiUrl.Contains("sandbox") || GluwaApiUrl.Contains("test2"))
                {
                    return NBitcoin.Network.TestNet;
                }
                else
                {
                    return NBitcoin.Network.Main;
                }
            }

        }

        /// <summary>
        /// Returns current user
        /// </summary>
        private static JToken mCurrentUser
        {
            get
            {
                string userType = Enum.GetName(typeof(EUserType), QAGluwaUser);
                return EnvironmentInfo.SelectToken("users").SelectToken(userType.ToLower());
            }
        }



        /// <summary>
        /// Writes QR Code to text files
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="qrCode"></param>
        public static void WriteQRCodeToFile(string testName, string qrCode)
        {
            string path = Path.Combine(getFolderPath("Transfer.Tests"), "bin\\Debug\\QRCodeTextFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(Path.Combine(path, testName), qrCode);

        }

        public static void SetUpGluwaTests(EUserType qaUser, string environment)
        {
            QAGluwaUser = qaUser;
            InitializeGluwaApi = environment;
        }


        /// <summary>
        /// Setup key name 
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string SetUpGluwaKeyName(string environment)
        {
            if (ApiController.Api.IsTestEnvironment(environment) && (environment != "Sandbox"))    // Test environment  
            {
                return "Sender";
            }
            else
                return "MainnetSender";
        }

        /// <summary>
        /// Get folder math
        /// </summary>
        /// <param name="mainDirectory"></param>
        /// <returns></returns>
        private static string getFolderPath(string mainDirectory)
        {
            string workingDirectory = System.AppContext.BaseDirectory;
            string currentDirectory = Path.Combine(workingDirectory, mainDirectory);

            while (!Directory.Exists(currentDirectory))
            {
                workingDirectory = Directory.GetParent(workingDirectory).FullName;
                Debug.Assert(!workingDirectory.EndsWith("repos"), $"Can't find {mainDirectory} in RestSharp folder");
                currentDirectory = Path.Combine(workingDirectory, mainDirectory);

            }

            return currentDirectory;
        }

        private static JObject StreamFileTextIntoObject(string path)
        {
            using StreamReader r = new StreamReader(path);
            string json = r.ReadToEnd();
            JObject jObj = JObject.Parse(json);

            return jObj;
        }
    }
}




