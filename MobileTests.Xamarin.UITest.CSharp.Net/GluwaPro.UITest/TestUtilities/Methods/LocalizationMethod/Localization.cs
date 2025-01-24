using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;

namespace GluwaPro.UITest.TestUtilities.Methods
{    
    class Localization
    {
        // In Phrase for localization google spreadsheet
        private static int indexOfPrimaryKey = 1;
        private static int indexOfEnglish = 2;
        private static int indexOfKorean = 3;
        private static string getPhrase;

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Google Sheets API .NET Quickstart";

        /// <summary>
        /// Get spreadsheet data from credentials.json
        /// </summary>
        /// <param name="selectPrimaryKey"></param>
        /// <param name="languageToTest"></param>
        /// <returns></returns>
        public static string GetSpreadsheetData(string selectPrimaryKey, ELanguage languageToTest)
        {
            UserCredential credential;
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            string spreadsheetId = "*************************";
            string range = "*************";
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Get Phrase by Primary key in the Phrase for localization spreadsheet:
            ValueRange response = request.Execute();
            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    // if the language to test is Korean
                    if (row[indexOfPrimaryKey].ToString() == selectPrimaryKey && languageToTest == ELanguage.Korean)
                    {
                        getPhrase = row[indexOfKorean].ToString();
                    }
                    // if the language to test is English
                    else if (row[indexOfPrimaryKey].ToString() == selectPrimaryKey && languageToTest == ELanguage.English)
                    {
                        getPhrase = row[indexOfEnglish].ToString();
                    }
                }
            }
            return getPhrase;
        }        
    }
}