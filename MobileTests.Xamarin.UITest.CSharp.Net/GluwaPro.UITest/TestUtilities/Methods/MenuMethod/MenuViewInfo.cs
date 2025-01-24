using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Methods.TestRailApiMethod;
using GluwaPro.UITest.TestUtilities.Models.HomeViewModels;
using GluwaPro.UITest.TestUtilities.Models.MenuViewModels;
using GluwaPro.UITest.TestUtilities.Models.NavigationViewModels;
using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace GluwaPro.UITest.TestUtilities.Methods.SendMethod
{
    class MenuViewInfo
    {
        public const string MENU_VIEW = "viewMenu";
        public const string SIGNATURE_ENTER_MESSAGE_VIEW = "viewSignatureEnterMessage";
        public const string SIGNATURE_MESSAGE_SIGNED_VIEW = "viewSignatureMessageSigned";
        public const string IDENTIFICATION_VIEW = "viewIdentification";
        public const string IDENTIFICATION_LOGIN_VIEW = "viewIdentificationLogin";
        public const string APPEARANCE_VIEW = "viewAppearance";
        public const string SANDBOX_MODE_VIEW = "viewSandboxMode";
        public const string PRIVATE_KEY_VIEW = "viewPrivateKey";
        public const string LANGUAGE_VIEW = "viewLanguage";
        public const string PRIVATE_VIEW = "viewPrivate";

        /// <summary>
        /// Get menu title view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitleMenuViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string textTitleMenu;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitleMenu = app.Query(AutomationID.labelMenu).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitleMenu = extractTextContext(app.Query(c => c.Id(AutomationID.labelMenu).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(textTitleMenu);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "Menu" };
                    string[] listOfTextsOnScreen = { textTitleMenu };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get identification title view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitleIdentificationViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string TitleIdentification;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TitleIdentification = app.Query(AutomationID.labelIdentification).LastOrDefault().Text;
                }
                else // iOS
                {
                    TitleIdentification = extractTextContext(app.Query(c => c.Id(AutomationID.labelIdentification).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(TitleIdentification);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "Identification" };
                    string[] listOfTextsOnScreen = { TitleIdentification };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get signature title view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitleSignatureViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string TitleSignature;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TitleSignature = app.Query(AutomationID.labelSignature).LastOrDefault().Text;
                }
                else // iOS
                {
                    TitleSignature = extractTextContext(app.Query(c => c.Id(AutomationID.labelSignature).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(TitleSignature);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "Signature" };
                    string[] listOfTextsOnScreen = { TitleSignature };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get PK and addresses title view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitlePrivateKeysAndAddressesViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string TitlePrivate;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TitlePrivate = app.Query(AutomationID.labelPrivate).LastOrDefault().Text;
                }
                else // iOS
                {
                    TitlePrivate = extractTextContext(app.Query(c => c.Id(AutomationID.labelPrivate).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(TitlePrivate);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "PrivateKeysAndAddresses" };
                    string[] listOfTextsOnScreen = { TitlePrivate };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get appearance title view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitleAppearanceViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string textAppearance;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textAppearance = app.Query(AutomationID.labelAppearance).LastOrDefault().Text;
                }
                else // iOS
                {
                    textAppearance = extractTextContext(app.Query(c => c.Id(AutomationID.labelAppearance).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(textAppearance);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "Appearance" };
                    string[] listOfTextsOnScreen = { textAppearance };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get backup recovery title view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitleBackupRecoveryPhraseViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string textAppearance;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textAppearance = app.Query(AutomationID.labelBackupRecoveryPhrase).LastOrDefault().Text;
                }
                else // iOS
                {
                    textAppearance = extractTextContext(app.Query(c => c.Id(AutomationID.labelBackupRecoveryPhrase).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(textAppearance);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "BackupRecoveryPhrase" };
                    string[] listOfTextsOnScreen = { textAppearance };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }
        /// <summary>
        /// Get language title view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitleLanguageViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string textLanguage;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textLanguage = app.Query(AutomationID.labelLanguage).LastOrDefault().Text;
                }
                else // iOS
                {
                    textLanguage = extractTextContext(app.Query(c => c.Id(AutomationID.labelLanguage).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(textLanguage);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "Language" };
                    string[] listOfTextsOnScreen = { textLanguage };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get sanbox mode view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitleSandboxModeViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string textSandboxMode;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textSandboxMode = app.Query(AutomationID.labelSandbox).LastOrDefault().Text;
                }
                else // iOS
                {
                    textSandboxMode = extractTextContext(app.Query(c => c.Id(AutomationID.labelSandbox).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(textSandboxMode);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "SandboxMode" };
                    string[] listOfTextsOnScreen = { textSandboxMode };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get additional security title view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitleAdditionalSecurityViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string textAddSecurity;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textAddSecurity = app.Query(AutomationID.labelAddSecurity).LastOrDefault().Text;
                }
                else // iOS
                {
                    textAddSecurity = extractTextContext(app.Query(c => c.Id(AutomationID.labelAddSecurity).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(textAddSecurity);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "AdditionalSecurity" };
                    string[] listOfTextsOnScreen = { textAddSecurity };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get support title view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitleSupportViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string textSupport;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textSupport = app.Query(AutomationID.labelSupport).LastOrDefault().Text;
                }
                else // iOS
                {
                    textSupport = extractTextContext(app.Query(c => c.Id(AutomationID.labelSupport).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(textSupport);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "Support" };
                    string[] listOfTextsOnScreen = { textSupport };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get user guide title view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitleUserGuideViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string textUserGuide;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textUserGuide = app.Query(AutomationID.labelUserGuide).LastOrDefault().Text;
                }
                else // iOS
                {
                    textUserGuide = extractTextContext(app.Query(c => c.Id(AutomationID.labelUserGuide).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(textUserGuide);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "UserGuide" };
                    string[] listOfTextsOnScreen = { textUserGuide };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get privacy and terms title view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitlePrivacyAndTermsViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string textPrivacyAndTerms;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textPrivacyAndTerms = app.Query(AutomationID.labelPrivacyAndTerms).LastOrDefault().Text;
                }
                else // iOS
                {
                    textPrivacyAndTerms = extractTextContext(app.Query(c => c.Id(AutomationID.labelPrivacyAndTerms).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(textPrivacyAndTerms);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "PrivacyAndTerms" };
                    string[] listOfTextsOnScreen = { textPrivacyAndTerms };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get wallet title view 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitleGluwaSiteViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string textGluwaSite;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textGluwaSite = app.Query(AutomationID.labelGluwaSite).LastOrDefault().Text;
                }
                else // iOS
                {
                    textGluwaSite = extractTextContext(app.Query(c => c.Id(AutomationID.labelGluwaSite).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(textGluwaSite);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "Gluwa.com" };
                    string[] listOfTextsOnScreen = { textGluwaSite };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get reset wallet view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EachMenuViewItem GetTitleResetWalletViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string textResetWallet;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textResetWallet = app.Query(AutomationID.labelResetWallet).LastOrDefault().Text;
                }
                else // iOS
                {
                    textResetWallet = extractTextContext(app.Query(c => c.Id(AutomationID.labelResetWallet).Descendant(0))[0]);
                }
                EachMenuViewItem result = new EachMenuViewItem(textResetWallet);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "ResetWallet" };
                    string[] listOfTextsOnScreen = { textResetWallet };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get identification view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public static IdentificationViewItem GetIdentificationViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, EPageNames pageInfo = EPageNames.Menu)
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

            string viewName = MENU_VIEW;
            string textHeaderBar;
            string textUserIdentification;
            string textVerifyYourIdentify;
            string textSignup;
            string textLogin;
            try
            {                
                if (platform == Platform.Android) // Android
                {
                    textHeaderBar = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    textUserIdentification = app.Query(AutomationID.labelUserIdentification).LastOrDefault().Text;
                    textVerifyYourIdentify = app.Query(AutomationID.labelVerifyYourIdentify).LastOrDefault().Text;
                    textSignup = app.Query(AutomationID.buttonSignup).LastOrDefault().Text;
                    textLogin = app.Query(AutomationID.buttonLogin).LastOrDefault().Text;
                }
                else // iOS
                {
                    textHeaderBar = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textUserIdentification = extractTextContext(app.Query(c => c.Id(AutomationID.labelUserIdentification).Descendant(0))[0]);
                    textVerifyYourIdentify = extractTextContext(app.Query(c => c.Id(AutomationID.labelVerifyYourIdentify).Descendant(0))[0]);
                    textSignup = extractTextContext(app.Query(c => c.Id(AutomationID.buttonSignup).Descendant(0))[0]);
                    textLogin = extractTextContext(app.Query(c => c.Id(AutomationID.buttonLogin).Descendant(0))[0]);

                }
                IdentificationViewItem result = new IdentificationViewItem(
                    textHeaderBar,
                    textUserIdentification,
                    textVerifyYourIdentify,
                    textSignup,
                    textLogin);
                if (isLocalizationTest)
                {
                    // To check localzation
                    string[] listOfPrimaryKeys = { "TitleIdentification", "UserIdentification", "UserIdentificationDescriptionText", "Signup", "Login" };
                    string[] listOfTextsOnScreen = { textHeaderBar, textUserIdentification, textVerifyYourIdentify, textSignup, textLogin };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get login view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public static IdentificationLoginViewItem GetIdentificationLoginViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, EPageNames pageInfo = EPageNames.Menu)
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

            string viewName = IDENTIFICATION_LOGIN_VIEW;
            string textHeaderBar;
            string textButtonLogin;
            string textButtonForgotPassword;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textHeaderBar = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    textButtonLogin = app.Query(AutomationID.buttonLogin).LastOrDefault().Text;
                    textButtonForgotPassword = app.Query(AutomationID.buttonForgotPassword).LastOrDefault().Text;
                }
                else // iOS
                {
                    textHeaderBar = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textButtonLogin = extractTextContext(app.Query(c => c.Id(AutomationID.buttonLogin).Descendant(0))[0]);
                    textButtonForgotPassword = extractTextContext(app.Query(c => c.Id(AutomationID.buttonForgotPassword).Descendant(0))[0]);
                }
                IdentificationLoginViewItem result = new IdentificationLoginViewItem(
                    textHeaderBar,
                    textButtonLogin,
                    textButtonForgotPassword);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "TitleLogin", "Login", "ForgotPassword" };
                    string[] listOfTextsOnScreen = { textHeaderBar, textButtonLogin, textButtonForgotPassword };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get login error view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static ErrorViewItem GetIdentificationLoginErrorViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = IDENTIFICATION_LOGIN_VIEW;
            string textErrorMessage;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textErrorMessage = app.Query(AutomationID.labelInvalidLoginPassword).LastOrDefault().Text;
                }
                else // iOS
                {
                    textErrorMessage = extractTextContext(app.Query(c => c.Id(AutomationID.labelInvalidLoginPassword).Descendant(0))[0]);
                }
                ErrorViewItem result = new ErrorViewItem(
                    textErrorMessage);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "InvalidUserInfo" };
                    string[] listOfTextsOnScreen = { textErrorMessage };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get signature enter message view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static SignatureEnterMessageViewItem GetSignatureEnterMessageViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = SIGNATURE_ENTER_MESSAGE_VIEW;
            string textHeaderBar;
            string textYouCanSign;
            string textSign;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textHeaderBar = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    textYouCanSign = app.Query(AutomationID.labelYouCanSign).LastOrDefault().Text;
                    textSign = app.Query(AutomationID.buttonSign).LastOrDefault().Text;
                }
                else // iOS
                {
                    textHeaderBar = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textYouCanSign = extractTextContext(app.Query(c => c.Id(AutomationID.labelYouCanSign).Descendant(0))[0]);
                    textSign = extractTextContext(app.Query(c => c.Id(AutomationID.buttonSign).Descendant(0))[0]);

                }
                SignatureEnterMessageViewItem result = new SignatureEnterMessageViewItem(
                    textHeaderBar,
                    textYouCanSign,
                    textSign);
                if (isLocalizationTest)
                {
                    // To check localzation
                    string[] listOfPrimaryKeys = { "TitleSignature", "YouCanSign", "Sign" };
                    string[] listOfTextsOnScreen = { textHeaderBar, textYouCanSign, textSign };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get signature view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static SignatureViewItem GetSignatureViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Arrange
            string ownAddress = "************************";

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

            string viewName = SIGNATURE_MESSAGE_SIGNED_VIEW;
            string textMessageSigned;
            string textShareMessage;
            string textCurrencyAddress;
            string textCurrentAddress;
            string textButtonCopyAddress;
            string textMessage;
            string textCopyMessage;
            string textSignature;
            string textCopySignature;
            string textButtonDone;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textMessageSigned = app.Query(AutomationID.labelMessageSigned).LastOrDefault().Text;
                    textShareMessage = app.Query(AutomationID.labelShareMessage).LastOrDefault().Text;
                    textCurrencyAddress = app.Query(AutomationID.textCurrencyAddress).LastOrDefault().Text;
                    textCurrentAddress = app.Query(AutomationID.textCurrentAddress).LastOrDefault().Text;
                    textButtonCopyAddress = app.Query(AutomationID.buttonCopyAddress).LastOrDefault().Text;
                    textMessage = app.Query(AutomationID.textMessage).LastOrDefault().Text;
                    textCopyMessage = app.Query(AutomationID.textCopyMessage).LastOrDefault().Text;
                    Shared.ScrollDownToElement(app, platform, AutomationID.textCopySignature);
                    textSignature = app.Query(AutomationID.textSignature).LastOrDefault().Text;
                    textCopySignature = app.Query(AutomationID.textCopySignature).LastOrDefault().Text;
                    textButtonDone = app.Query(AutomationID.buttonDone).LastOrDefault().Text;
                }
                else // iOS
                {
                    textMessageSigned = extractTextContext(app.Query(c => c.Id(AutomationID.labelMessageSigned).Descendant(0))[0]);
                    textShareMessage = extractTextContext(app.Query(c => c.Id(AutomationID.labelShareMessage).Descendant(0))[0]);
                    textCurrencyAddress = extractTextContext(app.Query(c => c.Id(AutomationID.textCurrencyAddress).Descendant(0))[0]);
                    textCurrentAddress = extractTextContext(app.Query(c => c.Id(AutomationID.textCurrentAddress).Descendant(0))[0]);
                    textButtonCopyAddress = extractTextContext(app.Query(c => c.Id(AutomationID.buttonCopyAddress).Descendant(0))[0]);
                    textMessage = extractTextContext(app.Query(c => c.Id(AutomationID.textMessage).Descendant(0))[0]);
                    textCopyMessage = extractTextContext(app.Query(c => c.Id(AutomationID.textCopyMessage).Descendant(0))[0]);
                    Shared.ScrollDownToElement(app, platform, AutomationID.textCopySignature);
                    textSignature = extractTextContext(app.Query(c => c.Id(AutomationID.textSignature).Descendant(0))[0]);
                    textCopySignature = extractTextContext(app.Query(c => c.Id(AutomationID.textCopySignature).Descendant(0))[0]);
                    textButtonDone = extractTextContext(app.Query(c => c.Id(AutomationID.buttonDone).Descendant(0))[0]);

                };

                SignatureViewItem result = new SignatureViewItem(
                    textMessageSigned,
                    textShareMessage,
                    textCurrencyAddress,
                    textCurrentAddress,
                    textButtonCopyAddress,
                    textMessage,
                    textCopyMessage,
                    textSignature,
                    textCopySignature,
                    textButtonDone);
                if (isLocalizationTest)
                {
                    // To check localzation
                    string[] listOfPrimaryKeys = { "MessageSigned", "ShareMessage", "Address", ownAddress, "CopyAddress", "Message", "CopyMessage", "Signature", "CopySignature", "Done" };
                    string[] listOfTextsOnScreen = { textMessageSigned, textShareMessage, textCurrencyAddress, textCurrentAddress, textButtonCopyAddress, textMessage, textCopyMessage, textSignature, textCopySignature, textButtonDone };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get private keys view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PrivateKeysAddressViewItem GetPrivateKeysAddressViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = MENU_VIEW;
            string textHeaderBar;
            string textTitlePrivateKeyUSDCG;
            string textTitleAddressUSDCG;
            string textCurrencyUSDCG;
            string textPrivateKeyUSDCG;
            string textAddressUSDCG;
            string textTitlePrivateKeysUSDCG;
            string textTitleAddresssUSDCG;
            string textCurrencysUSDCG;
            string textPrivateKeysUSDCG;
            string textAddresssUSDCG;
            string textCurrencyCTC;
            string textTitlePrivateKeyCTC;
            string textPrivateKeyCTC;
            string textTitleAddressCTC;
            string textAddressCTC;
            string textCurrencysNGNG;
            string textTitlePrivateKeysNGNG;
            string textPrivateKeysNGNG;
            string textTitleAddresssNGNG;
            string textAddresssNGNG;
            string textCurrencyBTC;
            string textTitlePrivateKeyBTC;
            string textPrivateKeyBTC;
            string textTitleAddressBTC;
            string textAddressBTC;
            string textTitleTestnetPrivateKeyBTC;
            string textTestnetPrivateKeyBTC;
            string textTitleTestnetAddressBTC;
            string textTestnetAddressBTC;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textHeaderBar = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    textCurrencyUSDCG = app.Query(AutomationID.labelCurrencyUSDCG).LastOrDefault().Text;
                    textTitlePrivateKeyUSDCG = app.Query(AutomationID.labelTitlePrivateKeyUSDCG).LastOrDefault().Text;
                    textPrivateKeyUSDCG = app.Query(AutomationID.labelPrivateKeyUSDCG).LastOrDefault().Text;
                    textTitleAddressUSDCG = app.Query(AutomationID.labelTitleAddressUSDCG).LastOrDefault().Text;
                    textAddressUSDCG = app.Query(AutomationID.labelAddressUSDCG).LastOrDefault().Text;
                                        
                    textCurrencysUSDCG = app.Query(AutomationID.labelCurrencysUSDCG).LastOrDefault().Text;
                    textTitlePrivateKeysUSDCG = app.Query(AutomationID.labelTitlePrivateKeysUSDCG).LastOrDefault().Text;
                    textPrivateKeysUSDCG = app.Query(AutomationID.labelPrivateKeysUSDCG).LastOrDefault().Text;
                    textTitleAddresssUSDCG = app.Query(AutomationID.labelTitleAddresssUSDCG).LastOrDefault().Text;
                    textAddresssUSDCG = app.Query(AutomationID.labelAddresssUSDCG).LastOrDefault().Text;

                    textCurrencyCTC = app.Query(AutomationID.labelCurrencyCTC).LastOrDefault().Text;
                    textTitlePrivateKeyCTC = app.Query(AutomationID.labelTitlePrivateKeyCTC).LastOrDefault().Text;
                    textPrivateKeyCTC = app.Query(AutomationID.labelPrivateKeyCTC).LastOrDefault().Text;                    
                    textTitleAddressCTC = app.Query(AutomationID.labelTitleAddressCTC).LastOrDefault().Text;
                    Shared.ScrollDownToElement(app, platform, AutomationID.labelAddressCTC);
                    textAddressCTC = app.Query(AutomationID.labelAddressCTC).LastOrDefault().Text;

                    textCurrencysNGNG = app.Query(AutomationID.labelCurrencysNGNG).LastOrDefault().Text;
                    textTitlePrivateKeysNGNG = app.Query(AutomationID.labelTitlePrivateKeysNGNG).LastOrDefault().Text;
                    textPrivateKeysNGNG = app.Query(AutomationID.labelPrivateKeysNGNG).LastOrDefault().Text;
                    textTitleAddresssNGNG = app.Query(AutomationID.labelTitleAddresssNGNG).LastOrDefault().Text;
                    textAddresssNGNG = app.Query(AutomationID.labelAddresssNGNG).LastOrDefault().Text;

                    textCurrencyBTC = app.Query(AutomationID.labelCurrencyBTC).LastOrDefault().Text;
                    textTitlePrivateKeyBTC = app.Query(AutomationID.labelTitlePrivateKeyBTC).LastOrDefault().Text;
                    textPrivateKeyBTC = app.Query(AutomationID.labelPrivateKeyBTC).LastOrDefault().Text;
                    textTitleAddressBTC = app.Query(AutomationID.labelTitleAddressBTC).LastOrDefault().Text;
                    textAddressBTC = app.Query(AutomationID.labelAddressBTC).LastOrDefault().Text;
                    textTitleTestnetPrivateKeyBTC = app.Query(AutomationID.labelTitleTestnetPrivateKeyBTC).LastOrDefault().Text;
                    textTestnetPrivateKeyBTC = app.Query(AutomationID.labelTestnetPrivateKeyBTC).LastOrDefault().Text;
                    textTitleTestnetAddressBTC = app.Query(AutomationID.labelTitleTestnetAddressBTC).LastOrDefault().Text;
                    textTestnetAddressBTC = app.Query(AutomationID.labelTestnetAddressBTC).LastOrDefault().Text;
                }
                else // iOS
                {
                    textHeaderBar = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textCurrencyUSDCG = extractTextContext(app.Query(c => c.Id(AutomationID.labelCurrencyUSDCG).Descendant(0))[0]);
                    textTitlePrivateKeyUSDCG = extractTextContext(app.Query(c => c.Id(AutomationID.labelTitlePrivateKeyUSDCG).Descendant(0))[0]);
                    textPrivateKeyUSDCG = extractTextContext(app.Query(c => c.Id(AutomationID.labelPrivateKeyUSDCG).Descendant(0))[0]);
                    textTitleAddressUSDCG = extractTextContext(app.Query(c => c.Id(AutomationID.labelTitleAddressUSDCG).Descendant(0))[0]);
                    textAddressUSDCG = extractTextContext(app.Query(c => c.Id(AutomationID.labelAddressUSDCG).Descendant(0))[0]);

                    textCurrencysUSDCG = extractTextContext(app.Query(c => c.Id(AutomationID.labelCurrencysUSDCG).Descendant(0))[0]);
                    textTitlePrivateKeysUSDCG = extractTextContext(app.Query(c => c.Id(AutomationID.labelTitlePrivateKeysUSDCG).Descendant(0))[0]);
                    textPrivateKeysUSDCG = extractTextContext(app.Query(c => c.Id(AutomationID.labelPrivateKeysUSDCG).Descendant(0))[0]);
                    textTitleAddresssUSDCG = extractTextContext(app.Query(c => c.Id(AutomationID.labelTitleAddresssUSDCG).Descendant(0))[0]);
                    textAddresssUSDCG = extractTextContext(app.Query(c => c.Id(AutomationID.labelAddresssUSDCG).Descendant(0))[0]);

                    textCurrencyCTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelCurrencyCTC).Descendant(0))[0]);
                    textTitlePrivateKeyCTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelTitlePrivateKeyCTC).Descendant(0))[0]);
                    textPrivateKeyCTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelPrivateKeyCTC).Descendant(0))[0]);
                    textTitleAddressCTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelTitleAddressCTC).Descendant(0))[0]);
                    Shared.ScrollDownToElement(app, platform, AutomationID.labelAddressCTC);
                    textAddressCTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelAddressCTC).Descendant(0))[0]);

                    textCurrencysNGNG = extractTextContext(app.Query(c => c.Id(AutomationID.labelCurrencysNGNG).Descendant(0))[0]);
                    textTitlePrivateKeysNGNG = extractTextContext(app.Query(c => c.Id(AutomationID.labelTitlePrivateKeysNGNG).Descendant(0))[0]);
                    textPrivateKeysNGNG = extractTextContext(app.Query(c => c.Id(AutomationID.labelPrivateKeysNGNG).Descendant(0))[0]);
                    textTitleAddresssNGNG = extractTextContext(app.Query(c => c.Id(AutomationID.labelTitleAddresssNGNG).Descendant(0))[0]);
                    textAddresssNGNG = extractTextContext(app.Query(c => c.Id(AutomationID.labelAddresssNGNG).Descendant(0))[0]);

                    textCurrencyBTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelCurrencyBTC).Descendant(0))[0]);
                    textTitlePrivateKeyBTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelTitlePrivateKeyBTC).Descendant(0))[0]);
                    textPrivateKeyBTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelPrivateKeyBTC).Descendant(0))[0]);
                    textTitleAddressBTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelTitleAddressBTC).Descendant(0))[0]);
                    textAddressBTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelAddressBTC).Descendant(0))[0]);
                    textTitleTestnetPrivateKeyBTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelTitleTestnetPrivateKeyBTC).Descendant(0))[0]);
                    textTestnetPrivateKeyBTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelTestnetPrivateKeyBTC).Descendant(0))[0]);
                    textTitleTestnetAddressBTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelTitleTestnetAddressBTC).Descendant(0))[0]);
                    textTestnetAddressBTC = extractTextContext(app.Query(c => c.Id(AutomationID.labelTestnetAddressBTC).Descendant(0))[0]);
                };

                PrivateKeysAddressViewItem result = new PrivateKeysAddressViewItem(
                    textHeaderBar,
                    textCurrencyUSDCG,
                    textTitlePrivateKeyUSDCG,
                    textPrivateKeyUSDCG,
                    textTitleAddressUSDCG,
                    textAddressUSDCG,

                    textCurrencysUSDCG,
                    textTitlePrivateKeysUSDCG,
                    textPrivateKeysUSDCG,
                    textTitleAddresssUSDCG,
                    textAddresssUSDCG,

                    textCurrencyCTC,
                    textTitlePrivateKeyCTC,
                    textPrivateKeyCTC,
                    textTitleAddressCTC,
                    textAddressCTC,

                    textCurrencysNGNG,
                    textTitlePrivateKeysNGNG,
                    textPrivateKeysNGNG,
                    textTitleAddresssNGNG,
                    textAddresssNGNG,

                    textCurrencyBTC,
                    textTitlePrivateKeyBTC,
                    textPrivateKeyBTC,
                    textTitleAddressBTC,
                    textAddressBTC,
                    textTitleTestnetPrivateKeyBTC,
                    textTestnetPrivateKeyBTC,
                    textTitleTestnetAddressBTC,
                    textTestnetAddressBTC);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                        "PrivateKeysAndAddresses",
                        "YourPrivatekey", "YourAddress",
                        "YourPrivatekey", "YourAddress",
                        "YourPrivatekey", "YourAddress",
                        "YourPrivatekey", "YourAddress",
                        "YourPrivatekey", "YourAddress",
                        "YourTestnetprivatekey", "YourTestnetAddress",
                    };
                    string[] listOfTextsOnScreen = { 
                        textHeaderBar,
                        textTitlePrivateKeyUSDCG, textTitleAddressUSDCG, 
                        textTitlePrivateKeysUSDCG, textTitleAddresssUSDCG,
                        textTitlePrivateKeyCTC, textTitleAddressCTC,
                        textTitlePrivateKeysNGNG, textTitleAddresssNGNG, 
                        textTitlePrivateKeyBTC, textTitleAddressBTC, 
                        textTitleTestnetPrivateKeyBTC, textTitleTestnetAddressBTC
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get appearance view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static AppearanceViewItem GetAppearanceViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = APPEARANCE_VIEW;
            string textHeaderBar;
            string textUseDarkMode;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textHeaderBar = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    textUseDarkMode = app.Query(AutomationID.labelUseDarkMode).LastOrDefault().Text;
                }
                else // iOS
                {
                    textHeaderBar = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textUseDarkMode = extractTextContext(app.Query(c => c.Id(AutomationID.labelUseDarkMode).Descendant(0))[0]);

                }
                AppearanceViewItem result = new AppearanceViewItem(
                    textHeaderBar,
                    textUseDarkMode);
                if (isLocalizationTest)
                {
                    // To check localzation
                    string[] listOfPrimaryKeys = { "TitleAppearance", "UseDarkMode" };
                    string[] listOfTextsOnScreen = { textHeaderBar, textUseDarkMode };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get language view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static LanguageViewItem GetLanguageViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = LANGUAGE_VIEW;
            string textHeaderBar;
            string textLanguageDescription;
            string textButtonConfirm;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textHeaderBar = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    textLanguageDescription = app.Query(AutomationID.labelLanguageDescription).LastOrDefault().Text;
                    textButtonConfirm = app.Query(AutomationID.buttonConfirm).LastOrDefault().Text;
                }
                else // iOS
                {
                    textHeaderBar = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textLanguageDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelLanguageDescription).Descendant(0))[0]);
                    textButtonConfirm = extractTextContext(app.Query(c => c.Id(AutomationID.buttonConfirm).Descendant(0))[0]);
                }
                LanguageViewItem result = new LanguageViewItem(
                    textHeaderBar,
                    textLanguageDescription,
                    textButtonConfirm);
                if (isLocalizationTest)
                {
                    // To check localzation
                    string[] listOfPrimaryKeys = { "TitleLanguage", "LanguageDescription", "Confirm" };
                    string[] listOfTextsOnScreen = { textHeaderBar, textLanguageDescription, textButtonConfirm };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get sandbox mode view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static SandboxModeViewItem GetSandboxModeViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = SANDBOX_MODE_VIEW;
            string textHeaderBar;
            string textUseSandboxMode;
            string textSandboxDescription;
            string textButtonConfirm;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textHeaderBar = app.Query(AutomationID.textHeaderBar).LastOrDefault().Text;
                    textUseSandboxMode = app.Query(AutomationID.labelUseSandboxMode).LastOrDefault().Text;
                    textSandboxDescription = app.Query(AutomationID.labelSandboxDescription).LastOrDefault().Text;
                    textButtonConfirm = app.Query(AutomationID.buttonConfirm).LastOrDefault().Text;
                }
                else // iOS
                {
                    textHeaderBar = extractTextContext(app.Query(c => c.Id(AutomationID.textHeaderBar).Descendant(0))[0]);
                    textUseSandboxMode = extractTextContext(app.Query(c => c.Id(AutomationID.labelUseSandboxMode).Descendant(0))[0]);
                    textSandboxDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelSandboxDescription).Descendant(0))[0]);
                    textButtonConfirm = extractTextContext(app.Query(c => c.Id(AutomationID.buttonConfirm).Descendant(0))[0]);
                }
                SandboxModeViewItem result = new SandboxModeViewItem(
                    textHeaderBar,
                    textUseSandboxMode,
                    textSandboxDescription,
                    textButtonConfirm);
                if (isLocalizationTest)
                {
                    // To check localzation
                    string[] listOfPrimaryKeys = { "TitleSandboxMode", "UseSandboxMode", "SandboxDescription", "Confirm" };
                    string[] listOfTextsOnScreen = { textHeaderBar, textUseSandboxMode, textSandboxDescription, textButtonConfirm };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", viewName);
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get reset wallet popup view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static WarningPopupWithTwoButtonsViewItem GetResetWalletPopViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
        {
            // Extract description info in here
            Func<AppResult, string> extractTextContext = input =>
            {
                // Extracting text info from description
                // returning result from Description: "<... text: (result) frame ...>"
                // If broken, check repl for new pattern
                string pattern = @"\btext = (.*) frame\b";
                string description = input.Description;
                Match m = Regex.Match(description, pattern, RegexOptions.IgnoreCase);
                return m.Groups[1].ToString();
            };

            string textTitle;
            string textDescription;
            string textReset;
            string textCancle;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textTitle = app.Query(AutomationID.alertTitle).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.message).LastOrDefault().Text;
                    textReset = app.Query(AutomationID.button1).LastOrDefault().Text;
                    textCancle = app.Query(AutomationID.button2).LastOrDefault().Text;
                }
                else // iOS
                {
                    textTitle = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[0]);
                    textDescription = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[1]);
                    textReset = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[2]);
                    textCancle = extractTextContext(app.Query(c => c.ClassFull(Shared.popupLabelForIOS))[3]);
                };

                WarningPopupWithTwoButtonsViewItem result = new WarningPopupWithTwoButtonsViewItem(
                    textTitle,
                    textDescription,
                    textReset,
                    textCancle);
                if (isLocalizationTest)
                {
                    // To check localzation - Are You Sure Popup page
                    string[] listOfPrimaryKeys = { "Warning", "ResetWalletContent", "Reset", "Cancel" };
                    string[] listOfTextsOnScreen = { textTitle, textDescription, textReset, textCancle };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Reset");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }   
    }
}
