using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Models.ExchangeViewModels;
using GluwaPro.UITest.TestUtilities.Models.HomeViewModels;
using GluwaPro.UITest.TestUtilities.Models.SendViewModels;
using GluwaPro.UITest.TestUtilities.Models.SharedViewModels;
using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace GluwaPro.UITest.TestUtilities.Methods
{
    class SharedViewInfo
    {
        /// <summary>
        /// Enter password view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EnterPasswordViewItem GetEnterPasswordViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext = '(.*)'";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };
            string enterPassword;
            bool bError;

            if (platform == Platform.Android) // Android
            {
                enterPassword = app.Query(c => c.Marked(AutomationID.labelEnterPassword))[0].Text;
                bError = app.Query(c => c.Marked(AutomationID.labelError)).Any();
            }
            else
            {
                enterPassword = app.Query(c => c.Id(AutomationID.labelEnterPassword)).LastOrDefault().Text;
                bError = app.Query(c => c.Id(AutomationID.labelError)).Any();
            }
            EnterPasswordViewItem result = new EnterPasswordViewItem(enterPassword, bError);
            if (isLocalizationTest)
            {
                // To check localzation 
                string[] listOfPrimaryKeys = { "Password" };
                string[] listOfTextsOnScreen = { enterPassword };
                Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
            }
            return result;
        }

        /// <summary>
        /// Wallet backup view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PageWithOneButtonViewItem GetWalletIsBackedUpViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext: (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string textTitle;
            string textDescription;
            string textButton;
            if (platform == Platform.Android) // Android
            {
                textTitle = app.Query(AutomationID.labelYourWalletIsBackedUp).LastOrDefault().Text;
                textDescription = app.Query(AutomationID.labelProtectYourRecoveryPhrase).LastOrDefault().Text;
                textButton = app.Query(AutomationID.buttonContinue).LastOrDefault().Text;
            }
            else // iOS
            {
                textTitle = extractTextContext(app.Query(c => c.Id(AutomationID.labelYourWalletIsBackedUp).Descendant(0))[0]);
                textDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelProtectYourRecoveryPhrase).Descendant(0))[0]);
                textButton = extractTextContext(app.Query(c => c.Id(AutomationID.buttonContinue).Descendant(0))[0]);

            };

            PageWithOneButtonViewItem result = new PageWithOneButtonViewItem(
                textTitle,
                textDescription,
                textButton);
            if (isLocalizationTest)
            {
                // To check localzation - Wallet Is BackedUp page
                string[] listOfPrimaryKeys = { "YourWalletIsBackedUp", "ProtectYourRecoveryPhrase", "Continue" };
                string[] listOfTextsOnScreen = { textTitle, textDescription, textButton };
                Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
            }
            return result;
        }

        /// <summary>
        /// Send to address view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static SendToAddressViewItem GetSendToAddressViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            //Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext = '(.*)'";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string textEnterAddress;
            string textButtonNext;
            if (platform == Platform.Android) // Android
            {
                textEnterAddress = app.Query(c => c.Marked(AutomationID.labelEnterAddress))[0].Text;
                textButtonNext = app.Query(c => c.Marked(AutomationID.buttonNext))[0].Text;
            }
            else
            {
                textEnterAddress = extractTextContext(app.Query(c => c.Id(AutomationID.labelEnterAddress)).LastOrDefault());
                textButtonNext = extractTextContext(app.Query(c => c.Id(AutomationID.buttonNext)).LastOrDefault());
            };

            SendToAddressViewItem result = new SendToAddressViewItem(
                textEnterAddress,
                textButtonNext);
            if (isLocalizationTest)
            {
                // To check localzation 
                string[] listOfPrimaryKeys = { "Address", "Next" };
                string[] listOfTextsOnScreen = { textEnterAddress, textButtonNext };
                Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
            }
            return result;
        }

        /// <summary>
        /// Get wallet pass view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EnterWalletPasswordViewItem GetEnterWalletPasswordViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext = '(.*)'";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string enterPassword;
            string textEnterWalletPassword;
            string textButtonConfirm;
            if (platform == Platform.Android) // Android
            {
                enterPassword = app.Query(c => c.Marked(AutomationID.textHeaderBar))[0].Text;
                textEnterWalletPassword = app.Query(c => c.Marked(AutomationID.labelEnterWalletPassword))[0].Text;
                textButtonConfirm = app.Query(c => c.Marked(AutomationID.buttonConfirm))[0].Text;
            }
            else
            {
                enterPassword = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar)).LastOrDefault());
                textEnterWalletPassword = extractTextContext(app.Query(c => c.Id(AutomationID.labelEnterWalletPassword)).LastOrDefault());
                textButtonConfirm = extractTextContext(app.Query(c => c.Id(AutomationID.buttonConfirm)).LastOrDefault());
            };

            EnterWalletPasswordViewItem result = new EnterWalletPasswordViewItem(
                enterPassword,
                textEnterWalletPassword,
                textButtonConfirm);
            if (isLocalizationTest)
            {
                // To check localzation 
                string[] listOfPrimaryKeys = { "Password", "EnterYourWalletPassword", "confirm" };
                string[] listOfTextsOnScreen = { enterPassword, textEnterWalletPassword, textButtonConfirm };
                Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
            }
            return result;
        }

        /// <summary>
        /// Get error pass view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static ErrorViewItem GetErrorPasswordViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext = '(.*)'";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };
            string errorPasswordInfo;
            if (platform == Platform.Android) // Android
            {
                errorPasswordInfo = app.Query(c => c.Marked(AutomationID.labelError))[0].Text;
            }
            else
            {
                errorPasswordInfo = extractTextContext(app.Query(c => c.Id(AutomationID.labelError)).LastOrDefault());
            }
            ErrorViewItem result = new ErrorViewItem(errorPasswordInfo);
            if (isLocalizationTest)
            {
                // To check localzation - Pick A NewPassword page
                string[] listOfPrimaryKeys = { "InvalidPassword" };
                string[] listOfTextsOnScreen = { errorPasswordInfo };
                Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
            }
            return result;
        }

        /// <summary>
        /// Get invalid login view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public static ErrorViewItem GetInvalidLoginUsernameViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, EPageNames pageInfo = EPageNames.Menu)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext = '(.*)'";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string errorPasswordInfo;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    errorPasswordInfo = app.Query(c => c.Marked(AutomationID.labelInvalidLoginPassword))[0].Text;
                }
                else
                {
                    errorPasswordInfo = extractTextContext(app.Query(c => c.Id(AutomationID.labelInvalidLoginPassword)).LastOrDefault());
                };

                ErrorViewItem result = new ErrorViewItem(errorPasswordInfo);
                if (isLocalizationTest)
                {
                    // To check localzation - Pick A NewPassword page
                    string[] listOfPrimaryKeys = { "InvalidLoginUsername" };
                    string[] listOfTextsOnScreen = { errorPasswordInfo };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }                
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "InvalidLoginUsername");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");                
            }
            return null;
        }

        /// <summary>
        /// Get invalid pass view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public static ErrorViewItem GetInvalidLoginPasswordViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, EPageNames pageInfo = EPageNames.Menu)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext = '(.*)'";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };
            string errorPasswordInfo;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    errorPasswordInfo = app.Query(c => c.Marked(AutomationID.labelInvalidLoginPassword))[0].Text;
                }
                else
                {
                    errorPasswordInfo = extractTextContext(app.Query(c => c.Id(AutomationID.labelInvalidLoginPassword)).LastOrDefault());
                };
                 
                ErrorViewItem result = new ErrorViewItem(errorPasswordInfo);
                if (isLocalizationTest)
                {
                    // To check localzation - Pick A NewPassword page
                    string[] listOfPrimaryKeys = { "InvalidLoginUsername" };
                    string[] listOfTextsOnScreen = { errorPasswordInfo };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "InvalidLoginUsername");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Warning popup view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static WarningPopupWithOneButtonViewItem GetWarningPopupWithOneButtonForOtpViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext = '(.*)'";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };
            string textWarning;
            string textDesciription;
            string textOkay;

            if (platform == Platform.Android) // Android
            {
                textWarning = app.Query(c => c.Marked(AutomationID.alertTitle))[0].Text;
                textDesciription = app.Query(c => c.Marked(AutomationID.message))[0].Text;
                textOkay = app.Query(c => c.Marked(AutomationID.button1))[0].Text;
            }
            else
            {
                textWarning = extractTextContext(app.Query(c => c.Id(AutomationID.alertTitle)).LastOrDefault());
                textDesciription = extractTextContext(app.Query(c => c.Id(AutomationID.message)).LastOrDefault());
                textOkay = extractTextContext(app.Query(c => c.Id(AutomationID.button1)).LastOrDefault());
            }
            WarningPopupWithOneButtonViewItem result = new WarningPopupWithOneButtonViewItem(
                textWarning,
                textDesciription,
                textOkay
                );
            if (isLocalizationTest)
            {
                // To check localzation - Pick A NewPassword page
                string[] listOfPrimaryKeys = {
                    "Warning",
                    "YourRequestOTPHasFailed",
                    "OK"
                };
                string[] listOfTextsOnScreen = {
                    textWarning,
                    textDesciription,
                    textOkay
                };
                Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
            }
            return result;
        }
    }
}
