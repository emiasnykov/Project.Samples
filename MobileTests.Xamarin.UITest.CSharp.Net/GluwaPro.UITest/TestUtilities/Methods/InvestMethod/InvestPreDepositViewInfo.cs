using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Models.InvestPreDepositViewModels;
using GluwaPro.UITest.TestUtilities.Models.InvestPreViewModels;
using GluwaPro.UITest.TestUtilities.Models.InvestViewModels;
using GluwaPro.UITest.TestUtilities.Models.SharedViewModels;
using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace GluwaPro.UITest.TestUtilities.Methods.InvestMethod
{
    class InvestPreDepositViewInfo
    {
        /// <summary>
        /// Get invest title
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static TitleInvestViewItem GetTitleInvestViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewSend;
            string TitleInvest;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TitleInvest = app.Query(AutomationID.labelInvest).LastOrDefault().Text;
                }
                else // iOS
                {
                    TitleInvest = extractTextContext(app.Query(c => c.Id(AutomationID.labelInvest).Descendant(0))[0]);
                }
                TitleInvestViewItem result = new TitleInvestViewItem(TitleInvest);
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = { "Invest" };
                    string[] listOfTextsOnScreen = { TitleInvest };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }                  
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get initial invest details
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static AvailableToInvestViewItem GetInitialInvestViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewAvailableNow;
            string TextAvailableNow;
            string TextInvestDescription;
            string TextIdentityVerificationRequired;
            app.WaitForElement(AutomationID.labelAvailableNow);
            if (platform == Platform.Android) // Android
            {
                TextAvailableNow = app.Query(AutomationID.labelAvailableNow).LastOrDefault().Text;
                TextInvestDescription = app.Query(AutomationID.labelInvestDescription1).LastOrDefault().Text;
                TextIdentityVerificationRequired = app.Query(AutomationID.labelStatus).LastOrDefault().Text;
            }
            else // iOS
            {
                TextAvailableNow = extractTextContext(app.Query(c => c.Id(AutomationID.labelAvailableNow).Descendant(0))[0]);
                TextInvestDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelInvestDescription1).Descendant(0))[0]);
                TextIdentityVerificationRequired = extractTextContext(app.Query(c => c.Id(AutomationID.labelStatus).Descendant(0))[0]);
            }
            AvailableToInvestViewItem result = new AvailableToInvestViewItem(
                TextAvailableNow,
                TextInvestDescription,
                TextIdentityVerificationRequired
                );
            if (isLocalizationTest)
            {
                // To check localzation 
                string[] listOfPrimaryKeys = {
                        "AvailableNow",
                        "InvestDescription1",
                        "IdentityVerificationRequired"
                    };
                string[] listOfTextsOnScreen = {
                        TextAvailableNow,
                        TextInvestDescription,
                        TextIdentityVerificationRequired
                    };
                Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
            }
            return result;
        }

        /// <summary>
        /// Get required identity verification
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public static IdentityVerificationRequiredViewItem GetIdentityVerificationRequiredViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false, EPageNames pageInfo = EPageNames.Menu)
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

            string viewName = AutomationID.labelReferralStatus;
            string textIdentityVerificationRequired;
            string textOurInvestProductsRequireUsToVerify;
            string textWeAreUnableToOfferThisProductToUS1;
            string textSelectBeginToStart;
            string textForBestResults1;
            string textWhatIsKYC;
            string textBegin;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textIdentityVerificationRequired = app.Query(AutomationID.labelIdentityVerificationRequired).LastOrDefault().Text;
                    textOurInvestProductsRequireUsToVerify = app.Query(AutomationID.labelOurInvestProductsRequireUsToVerify).LastOrDefault().Text;
                    textWeAreUnableToOfferThisProductToUS1 = app.Query(AutomationID.labelWeAreUnableToOfferThisProductToUS1).LastOrDefault().Text;
                    textSelectBeginToStart = app.Query(AutomationID.labelSelectBeginToStart).LastOrDefault().Text;
                    textForBestResults1 = app.Query(AutomationID.labelForBestResults1).LastOrDefault().Text;
                    textWhatIsKYC = app.Query(AutomationID.labelWhatIsKYC).LastOrDefault().Text;
                    textBegin = app.Query(AutomationID.buttonBegin).LastOrDefault().Text;
                }
                else // iOS
                {
                    textIdentityVerificationRequired = extractTextContext(app.Query(c => c.Id(AutomationID.labelIdentityVerificationRequired).Descendant(0))[0]);
                    textOurInvestProductsRequireUsToVerify = extractTextContext(app.Query(c => c.Id(AutomationID.labelOurInvestProductsRequireUsToVerify).Descendant(0))[0]);
                    textWeAreUnableToOfferThisProductToUS1 = extractTextContext(app.Query(c => c.Id(AutomationID.labelWeAreUnableToOfferThisProductToUS1).Descendant(0))[0]);
                    textSelectBeginToStart = extractTextContext(app.Query(c => c.Id(AutomationID.labelSelectBeginToStart).Descendant(0))[0]);
                    textForBestResults1 = extractTextContext(app.Query(c => c.Id(AutomationID.labelForBestResults1).Descendant(0))[0]);
                    textWhatIsKYC = extractTextContext(app.Query(c => c.Id(AutomationID.labelWhatIsKYC).Descendant(0))[0]);
                    textBegin = extractTextContext(app.Query(c => c.Id(AutomationID.buttonBegin).Descendant(0))[0]);
                }
                IdentityVerificationRequiredViewItem result = new IdentityVerificationRequiredViewItem(
                    textIdentityVerificationRequired,
                    textOurInvestProductsRequireUsToVerify,
                    textWeAreUnableToOfferThisProductToUS1,
                    textSelectBeginToStart,
                    textForBestResults1,
                    textWhatIsKYC,
                    textBegin
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                        "IdentityVerificationRequired",
                        "OurInvestProductsRequireUsToVerify",
                        "WeAreUnableToOfferThisProductToUS1",
                        "SelectBeginToStart",
                        "ForBestResults1",
                        "WhatIsKYC",
                        "Begin"
                        };
                    string[] listOfTextsOnScreen = {
                        textIdentityVerificationRequired,
                        textOurInvestProductsRequireUsToVerify,
                        textWeAreUnableToOfferThisProductToUS1,
                        textSelectBeginToStart,
                        textForBestResults1,
                        textWhatIsKYC,
                        textBegin
                        };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get completed setup steps
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static AccountSetupStepsCompletedViewItem GetAccountSetupStepsCompletedViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewAvailableNow;
            string textAvailableToInvest;
            string textDescription;
            string textAccountSetupStepsCompleted;
            string textBond;
            string textSavings;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textAvailableToInvest = app.Query(AutomationID.labelAvailableNow).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelInvestDescription1).LastOrDefault().Text;
                    textAccountSetupStepsCompleted = app.Query(AutomationID.labelStatus).LastOrDefault().Text;
                    textBond = app.Query(AutomationID.labelBond).LastOrDefault().Text;
                    textSavings = app.Query(AutomationID.labelSavings).LastOrDefault().Text;
                }
                else // iOS
                {
                    textAvailableToInvest = extractTextContext(app.Query(c => c.Id(AutomationID.labelAvailableNow).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(AutomationID.labelInvestDescription1).Descendant(0))[0]);
                    textAccountSetupStepsCompleted = extractTextContext(app.Query(c => c.Id(AutomationID.labelStatus).Descendant(0))[0]);
                    textBond = extractTextContext(app.Query(c => c.Id(AutomationID.labelBond).Descendant(0))[0]);
                    textSavings = extractTextContext(app.Query(c => c.Id(AutomationID.labelSavings).Descendant(0))[0]);
                }
                AccountSetupStepsCompletedViewItem result = new AccountSetupStepsCompletedViewItem(
                    textAvailableToInvest,
                    textDescription,
                    textAccountSetupStepsCompleted,
                    textBond,
                    textSavings
                    );

                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "AvailableNow",
                    "InvestDescription1",
                    "AuthenticationIncomplete",
                    "Bond",
                    "Savings",
                    };
                    string[] listOfTextsOnScreen = {
                    textAvailableToInvest,
                    textDescription,
                    textAccountSetupStepsCompleted,
                    textBond,
                    textSavings
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get completed setup steps checklist
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static AccountSetupStepsCheckListViewItem GetAccountSetupStepsCheckListViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewAvailableNow;
            string textAccountSetupStepsCompleted;
            string textIdentityVerification;
            string textPersonalInformation;
            string textProofOfAddress;
            string textTermsAndConditionsAgreement;
            string textOneTimePasswordWitnessVerification;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textAccountSetupStepsCompleted = app.Query(AutomationID.labelStatus).LastOrDefault().Text;
                    textIdentityVerification = app.Query(AutomationID.labelIdentityVerification).LastOrDefault().Text;
                    textPersonalInformation = app.Query(AutomationID.labelPersonalInformation).LastOrDefault().Text;
                    textProofOfAddress = app.Query(AutomationID.labelProofOfAddress).LastOrDefault().Text;
                    textTermsAndConditionsAgreement = app.Query(AutomationID.labelTermsAndConditionsAgreement).LastOrDefault().Text;
                    textOneTimePasswordWitnessVerification = app.Query(AutomationID.labelOneTimePasswordWitnessVerification).LastOrDefault().Text;
                }
                else // iOS
                {
                    textAccountSetupStepsCompleted = extractTextContext(app.Query(c => c.Id(AutomationID.labelStatus).Descendant(0))[0]);
                    textIdentityVerification = extractTextContext(app.Query(c => c.Id(AutomationID.labelIdentityVerification).Descendant(0))[0]);
                    textPersonalInformation = extractTextContext(app.Query(c => c.Id(AutomationID.labelPersonalInformation).Descendant(0))[0]);
                    textProofOfAddress = extractTextContext(app.Query(c => c.Id(AutomationID.labelProofOfAddress).Descendant(0))[0]);
                    textTermsAndConditionsAgreement = extractTextContext(app.Query(c => c.Id(AutomationID.labelTermsAndConditionsAgreement).Descendant(0))[0]);
                    textOneTimePasswordWitnessVerification = extractTextContext(app.Query(c => c.Id(AutomationID.labelOneTimePasswordWitnessVerification).Descendant(0))[0]);
                }
                AccountSetupStepsCheckListViewItem result = new AccountSetupStepsCheckListViewItem(
                    textAccountSetupStepsCompleted,
                    textIdentityVerification,
                    textPersonalInformation,
                    textProofOfAddress,
                    textTermsAndConditionsAgreement,
                    textOneTimePasswordWitnessVerification
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "AuthenticationIncomplete",
                    "IdentityVerification",
                    "PersonalInformation",
                    "ProofOfAddress",
                    "TermsAndConditionsAgreement",
                    "OneTimePasswordWitnessVerification"
                    };
                    string[] listOfTextsOnScreen = {
                    textAccountSetupStepsCompleted,
                    textIdentityVerification,
                    textPersonalInformation,
                    textProofOfAddress,
                    textTermsAndConditionsAgreement,
                    textOneTimePasswordWitnessVerification
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get additional information
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static WellNeedALittleMoreInformationFirstViewItem GetWellNeedALittleMoreInformationFirstViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewWellNeedALittleMoreInformationFirst;
            string textWellNeedALittleMoreInformationFirst;
            string textYourInformationWillBeKeptPrivateAndStored;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    app.DismissKeyboard();
                    textWellNeedALittleMoreInformationFirst = app.Query(AutomationID.labelWellNeedALittleMoreInformationFirst).LastOrDefault().Text;
                    textYourInformationWillBeKeptPrivateAndStored = app.Query(AutomationID.labelYourInformationWillBeKeptPrivateAndStored).LastOrDefault().Text;
                }
                else // iOS
                {
                    app.DismissKeyboard();
                    textWellNeedALittleMoreInformationFirst = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelWellNeedALittleMoreInformationFirst).Descendant(0))[0]);
                    textYourInformationWillBeKeptPrivateAndStored = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelYourInformationWillBeKeptPrivateAndStored).Descendant(0))[0]);                    
                }
                WellNeedALittleMoreInformationFirstViewItem result = new WellNeedALittleMoreInformationFirstViewItem(
                    textWellNeedALittleMoreInformationFirst,
                    textYourInformationWillBeKeptPrivateAndStored
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "WellNeedALittleMoreInformationFirst",
                    "YourInformationWillBeKeptPrivateAndStored"
                    };
                    string[] listOfTextsOnScreen = {
                    textWellNeedALittleMoreInformationFirst,
                    textYourInformationWillBeKeptPrivateAndStored
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get address document
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static AddressDocumentRequiredViewItem GetAddressDocumentRequiredViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewAddressDocumentRequired;
            string textAddressDocumentRequired;
            string textDescription;
            string textDescription2;
            string textDescription3;
            string textUpload;
            string textSkip;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textAddressDocumentRequired = app.Query(AutomationID.labelAddressDocumentRequired).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelAddressDocumentRequiredParagraph1_1).LastOrDefault().Text;
                    textDescription2 = app.Query(AutomationID.labelAddressDocumentRequiredParagraph1_2).LastOrDefault().Text;
                    textDescription3 = app.Query(AutomationID.labelAddressDocumentRequiredParagraph2_1).LastOrDefault().Text;
                    textUpload = app.Query(AutomationID.buttonUpload).LastOrDefault().Text;
                    textSkip = app.Query(AutomationID.buttonSkip).LastOrDefault().Text;
                }
                else // iOS
                {
                    textAddressDocumentRequired = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentRequired).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentRequiredParagraph1_1).Descendant(0))[0]);
                    textDescription2 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentRequiredParagraph1_2).Descendant(0))[0]);
                    textDescription3 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentRequiredParagraph2_1).Descendant(0))[0]);
                    textUpload = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonUpload).Descendant(0))[0]);
                    textSkip = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonSkip).Descendant(0))[0]);
                }
                AddressDocumentRequiredViewItem result = new AddressDocumentRequiredViewItem(
                    textAddressDocumentRequired,
                    textDescription,
                    textDescription2,
                    textDescription3,
                    textUpload,
                    textSkip
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "AddressDocumentRequired",
                    "AddressDocumentRequiredParagraph1_1",
                    "AddressDocumentRequiredParagraph1_2",
                    "AddressDocumentRequiredParagraph2_1",
                    "Upload",
                    "Skip"
                    };
                    string[] listOfTextsOnScreen = {
                    textAddressDocumentRequired,
                    textDescription,
                    textDescription2,
                    textDescription3,
                    textUpload,
                    textSkip
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get address document status
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static DocumentRequiredViewItem GetDocumentRequiredViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewAddressDocumentRequired;
            string textAddressDocumentRequired;
            string textDescription;
            string textDescription2;
            string textDescription3;
            string textOK;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textAddressDocumentRequired = app.Query(AutomationID.labelAddressDocumentStatusRequired).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelAddressDocumentStatusRequiredParagraph1).LastOrDefault().Text;
                    textDescription2 = app.Query(AutomationID.labelAddressDocumentRequiredParagraph2_1).LastOrDefault().Text;
                    textDescription3 = app.Query(AutomationID.labelAddressDocumentStatusRequiredParagraph3).LastOrDefault().Text;
                    textOK = app.Query(AutomationID.buttonOK).LastOrDefault().Text;
                }
                else // iOS
                {
                    textAddressDocumentRequired = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentStatusRequired).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentStatusRequiredParagraph1).Descendant(0))[0]);
                    textDescription2 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentRequiredParagraph2_1).Descendant(0))[0]);
                    textDescription3 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentStatusRequiredParagraph3).Descendant(0))[0]);
                    textOK = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonOK).Descendant(0))[0]);
                }

                DocumentRequiredViewItem result = new DocumentRequiredViewItem(
                    textAddressDocumentRequired,
                    textDescription,
                    textDescription2,
                    textDescription3,
                    textOK
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "AddressDocumentRequired",
                    "AddressDocumentRequiredParagraph1_1",
                    "AddressDocumentRequiredParagraph1_2",
                    "AddressDocumentRequiredParagraph2_1",
                    "OK"
                    };
                    string[] listOfTextsOnScreen = {
                    textAddressDocumentRequired,
                    textDescription,
                    textDescription2,
                    textDescription3,
                    textOK
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get document review progress
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static DocumentReviewInProgressViewItem GetReviewInProgressViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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
            string viewName = AutomationID.viewAddressDocumentRequired;

            string textAddressDocumentRequired;
            string textDescription;
            string textDescription2;
            string textOK;

            try
            {
                if (platform == Platform.Android) // Android
                {
                    textAddressDocumentRequired = app.Query(AutomationID.labelAddressDocumentStatusReviewInProgress).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelAddressDocumentStatusRequiredParagraph4).LastOrDefault().Text;
                    textDescription2 = app.Query(AutomationID.labelAddressDocumentStatusRequiredParagraph3).LastOrDefault().Text;
                    textOK = app.Query(AutomationID.buttonOK).LastOrDefault().Text;
                }
                else // iOS
                {
                    textAddressDocumentRequired = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentStatusReviewInProgress).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentStatusRequiredParagraph4).Descendant(0))[0]);
                    textDescription2 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentStatusRequiredParagraph3).Descendant(0))[0]);
                    textOK = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonOK).Descendant(0))[0]);
                }

                DocumentReviewInProgressViewItem result = new DocumentReviewInProgressViewItem(
                    textAddressDocumentRequired,
                    textDescription,
                    textDescription2,
                    textOK
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "AddressDocumentStatusReviewInProgress",
                    "AddressDocumentStatusRequiredParagraph4",
                    "AddressDocumentStatusRequiredParagraph3",
                    "OK"
                    };
                    string[] listOfTextsOnScreen = {
                    textAddressDocumentRequired,
                    textDescription,
                    textDescription2,
                    textOK
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get document action required view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static DocumentActionRequiredViewItem GetActionRequiredViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewAddressDocumentRequired;
            string textAddressDocumentRequired;
            string textDescription;
            string textDescription1;
            string textDescription2;
            string textDescription3;
            string textOK;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textAddressDocumentRequired = app.Query(AutomationID.labelAddressDocumentStatusActionRequired).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelAddressDocumentStatusRequiredParagraph5).LastOrDefault().Text;
                    textDescription1 = app.Query(AutomationID.labelAddressDocumentStatusRequiredParagraph6).LastOrDefault().Text;
                    textDescription2 = app.Query(AutomationID.labelAddressDocumentRequiredParagraph2_1).LastOrDefault().Text;
                    textDescription3 = app.Query(AutomationID.labelAddressDocumentStatusRequiredParagraph7).LastOrDefault().Text;
                    textOK = app.Query(AutomationID.buttonOK).LastOrDefault().Text;
                }
                else // iOS
                {
                    textAddressDocumentRequired = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentStatusActionRequired).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentStatusRequiredParagraph5).Descendant(0))[0]);
                    textDescription1 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentStatusRequiredParagraph6).Descendant(0))[0]);
                    textDescription2 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentRequiredParagraph2_1).Descendant(0))[0]);
                    textDescription3 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAddressDocumentStatusRequiredParagraph7).Descendant(0))[0]);
                    textOK = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonOK).Descendant(0))[0]);
                }
                DocumentActionRequiredViewItem result = new DocumentActionRequiredViewItem(
                    textAddressDocumentRequired,
                    textDescription,
                    textDescription1,
                    textDescription2,
                    textDescription3,
                    textOK
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "AddressDocumentStatusRequired",
                    "AddressDocumentStatusRequiredParagraph5",
                    "AddressDocumentStatusRequiredParagraph6",
                    "AddressDocumentRequiredParagraph2_1",
                    "AddressDocumentStatusRequiredParagraph7",
                    "OK"
                    };
                    string[] listOfTextsOnScreen = {
                    textAddressDocumentRequired,
                    textDescription,
                    textDescription1,
                    textDescription2,
                    textDescription3,
                    textOK
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get please linked documents view 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PleaseDocumentsLinkedViewItem GetPleaseDocumentsLinkedViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewImportant;
            string textPleaseDocumentsLinked;
            string textDescription;
            string textDescription2;
            string textIAgree;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textPleaseDocumentsLinked = app.Query(AutomationID.labelImportant).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelPleaseDocumentsLinkedParagraph1).LastOrDefault().Text;
                    textDescription2 = app.Query(AutomationID.labelPleaseDocumentsLinkedParagraph2).LastOrDefault().Text;
                    textIAgree = app.Query(AutomationID.buttonIAgree).LastOrDefault().Text;
                }
                else // iOS
                {
                    textPleaseDocumentsLinked = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelImportant).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelPleaseDocumentsLinkedParagraph1).Descendant(0))[0]);
                    textDescription2 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelPleaseDocumentsLinkedParagraph2).Descendant(0))[0]);
                    textIAgree = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonIAgree).Descendant(0))[0]);
                }

                PleaseDocumentsLinkedViewItem result = new PleaseDocumentsLinkedViewItem(
                    textPleaseDocumentsLinked,
                    textDescription,
                    textDescription2,
                    textIAgree
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "PleaseDocumentsLinked",
                    "PleaseDocumentsLinkedParagraph1",
                    "PleaseDocumentsLinkedParagraph2",
                    "IAgree"
                    };
                    string[] listOfTextsOnScreen = {
                    textPleaseDocumentsLinked,
                    textDescription,
                    textDescription2,
                    textIAgree
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get grab phone view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static GrabPhoneViewItem GetGrabPhoneViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewGrabFriendSpousePhone;
            string textGrabFriendSpousePhone;
            string textDescription;
            string textDescription2;
            string textDescription3;
            string textWitnessName;
            string textCountryCode;
            string textWitnessMobilePhone;
            string textRequestOneTimePassword;
            Shared.ScrollDownToElement(app, platform, AutomationID.labelGrabFriendSpousePhone);
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textGrabFriendSpousePhone = app.Query(AutomationID.labelGrabFriendSpousePhone).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelGrabFriendSpousePhoneOrderItem1).LastOrDefault().Text;
                    textDescription2 = app.Query(AutomationID.labelGrabFriendSpousePhoneOrderItem2).LastOrDefault().Text;
                    textDescription3 = app.Query(AutomationID.labelGrabFriendSpousePhoneOrderItem3).LastOrDefault().Text;
                    textWitnessName = app.Query(AutomationID.labelWitnessName).LastOrDefault().Text;
                    textCountryCode = app.Query(AutomationID.labelCountryCode).LastOrDefault().Text;
                    textWitnessMobilePhone = app.Query(AutomationID.labelWitnessMobilePhone).LastOrDefault().Text;
                    textRequestOneTimePassword = app.Query(AutomationID.labelRequestOneTimePassword).LastOrDefault().Text;
                }
                else // iOS
                {
                    textGrabFriendSpousePhone = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelGrabFriendSpousePhone).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelGrabFriendSpousePhoneOrderItem1).Descendant(0))[0]);
                    textDescription2 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelGrabFriendSpousePhoneOrderItem2).Descendant(0))[0]);
                    textDescription3 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelGrabFriendSpousePhoneOrderItem3).Descendant(0))[0]);
                    textWitnessName = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelWitnessName).Descendant(0))[0]);
                    textCountryCode = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelCountryCode).Descendant(0))[0]);
                    textWitnessMobilePhone = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelWitnessMobilePhone).Descendant(0))[0]);
                    textRequestOneTimePassword = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelRequestOneTimePassword).Descendant(0))[0]);
                }
                GrabPhoneViewItem result = new GrabPhoneViewItem(
                    textGrabFriendSpousePhone,
                    textDescription,
                    textDescription2,
                    textDescription3,
                    textWitnessName,
                    textCountryCode,
                    textWitnessMobilePhone,
                    textRequestOneTimePassword
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "GrabFriendSpousePhone",
                    "GrabFriendSpousePhoneParagraph1",
                    "GrabFriendSpousePhoneParagraph2",
                    "GrabFriendSpousePhoneParagraph3",
                    "witnessName",
                    "countryCode",
                    "witnessMobilePhone",
                    "RequestOneTimePassword"
                    };
                    string[] listOfTextsOnScreen = {
                    textGrabFriendSpousePhone,
                    textDescription,
                    textDescription2,
                    textDescription3,
                    textWitnessName,
                    textCountryCode,
                    textWitnessMobilePhone,
                    textRequestOneTimePassword
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get enter verification code
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static EnterVerificationCodeViewItem GetEnterVerificationCodeViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewGrabFriendSpousePhone;
            string textEnterVerificationCode;
            string textDescription;
            string textSubmit;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textEnterVerificationCode = app.Query(AutomationID.labelEnterVerificationCode).LastOrDefault().Text;
                    textDescription = app.Query(AutomationID.labelEnterVerificationCodeParagraph1).LastOrDefault().Text;
                    textSubmit = app.Query(AutomationID.buttonSubmit).LastOrDefault().Text;
                }
                else // iOS
                {
                    textEnterVerificationCode = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelEnterVerificationCode).Descendant(0))[0]);
                    textDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelEnterVerificationCodeParagraph1).Descendant(0))[0]);
                    textSubmit = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonSubmit).Descendant(0))[0]);
                }
                EnterVerificationCodeViewItem result = new EnterVerificationCodeViewItem(
                    textEnterVerificationCode,
                    textDescription,
                    textSubmit
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "EnterVerificationCode",
                    "EnterVerificationCodeParagraph1",
                    "Submit"
                    };
                    string[] listOfTextsOnScreen = {
                    textEnterVerificationCode,
                    textDescription,
                    textSubmit
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get verification view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static VerificationCompleteViewItem VerificationViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewVerificationComplete;
            string textVerificationComplete;
            string textTempParagraph1;
            string textTempParagraph2;
            string textTempParagraph3;
            string textVerifiedParagraph2;
            string textVerifiedParagraph3;
            string textVerifiedParagraph4;
            string testDone;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    textVerificationComplete = app.Query(AutomationID.labelVerificationComplete).LastOrDefault().Text;
                    textTempParagraph1 = app.Query(AutomationID.labelTempParagraph1).LastOrDefault().Text;
                    textTempParagraph2 = app.Query(AutomationID.labelTempParagraph2).LastOrDefault().Text;
                    textTempParagraph3 = app.Query(AutomationID.labelTempParagraph3).LastOrDefault().Text;
                    textVerifiedParagraph2 = app.Query(AutomationID.labelVerifiedParagraph2).LastOrDefault().Text;
                    textVerifiedParagraph3 = app.Query(AutomationID.labelVerifiedParagraph3).LastOrDefault().Text;
                    textVerifiedParagraph4 = app.Query(AutomationID.labelVerifiedParagraph4).LastOrDefault().Text;
                    testDone = app.Query(AutomationID.buttonDone).LastOrDefault().Text;
                }
                else // iOS
                {
                    textVerificationComplete = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelVerificationComplete).Descendant(0))[0]);
                    textTempParagraph1 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTempParagraph1).Descendant(0))[0]);
                    textTempParagraph2 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTempParagraph2).Descendant(0))[0]);
                    textTempParagraph3 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelTempParagraph3).Descendant(0))[0]);
                    textVerifiedParagraph2 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelVerifiedParagraph2).Descendant(0))[0]);
                    textVerifiedParagraph3 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelVerifiedParagraph3).Descendant(0))[0]);
                    textVerifiedParagraph4 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelVerifiedParagraph4).Descendant(0))[0]);
                    testDone = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonDone).Descendant(0))[0]);
                }

                VerificationCompleteViewItem result = new VerificationCompleteViewItem(
                    textVerificationComplete,
                    textTempParagraph1,
                    textTempParagraph2,
                    textTempParagraph3,
                    textVerifiedParagraph2,
                    textVerifiedParagraph3,
                    textVerifiedParagraph4,
                    testDone
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "VerificationComplete",
                    "TempParagraph1",
                    "TempParagraph2",
                    "TempParagraph3",
                    "VerifiedParagraph2",
                    "VerifiedParagraph3",
                    "VerifiedParagraph4",
                    "Done"
                    };
                    string[] listOfTextsOnScreen = {
                    textVerificationComplete,
                    textTempParagraph1,
                    textTempParagraph2,
                    textTempParagraph3,
                    textVerifiedParagraph2,
                    textVerifiedParagraph3,
                    textVerifiedParagraph4,
                    testDone
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get invest with referrals view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static AvailableToInvestViewItem GetInvestWithMyReferralVeiwItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewAvailableNow;
            string TextAvailableNow;
            string TextInvestDescription1;
            string TextMyReferralCode;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextAvailableNow = app.Query(AutomationID.labelAvailableNow).LastOrDefault().Text;
                    TextInvestDescription1 = app.Query(AutomationID.labelInvestDescription1).LastOrDefault().Text;
                    TextMyReferralCode = app.Query(AutomationID.labelCreatingReferralCode).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextAvailableNow = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAvailableNow).Descendant(0))[0]);
                    TextInvestDescription1 = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelInvestDescription1).Descendant(0))[0]);
                    TextMyReferralCode = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelCreatingReferralCode).Descendant(0))[0]);
                }
                AvailableToInvestViewItem result = new AvailableToInvestViewItem(
                    TextAvailableNow,
                    TextInvestDescription1,
                    TextMyReferralCode
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "AvailableNow",
                    "InvestDescription1",
                    "CreatingReferralCode",
                    };
                    string[] listOfTextsOnScreen = {
                    TextAvailableNow,
                    TextInvestDescription1,
                    TextMyReferralCode
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get invest dashboard with referrals view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static InvestDashbaordWithReferralCodeViewItem GetInvestDashbaordWithReferralCodeViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewAvailableNow;
            string TextAvailableToInvest;
            string TextInvestDescription;
            string TextIdentityVerification;
            string TextMyReferralCode;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextAvailableToInvest = app.Query(AutomationID.labelAvailableNow).LastOrDefault().Text;
                    TextInvestDescription = app.Query(AutomationID.labelInvestDescription1).LastOrDefault().Text;
                    TextIdentityVerification = app.Query(AutomationID.labelReferralStatus).LastOrDefault().Text;
                    TextMyReferralCode = app.Query(AutomationID.labelReferralStatus).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextAvailableToInvest = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAvailableNow).Descendant(0))[0]);
                    TextInvestDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelInvestDescription1).Descendant(0))[0]);
                    TextIdentityVerification = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelReferralStatus).Descendant(0))[0]);
                    TextMyReferralCode = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelReferralStatus).Descendant(0))[0]);
                }

                InvestDashbaordWithReferralCodeViewItem result = new InvestDashbaordWithReferralCodeViewItem(
                    TextAvailableToInvest,
                    TextInvestDescription,
                    TextIdentityVerification,
                    TextMyReferralCode
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "AvailableNow",
                    "InvestDescription1",
                    "IdentityVerificationRequired",
                    "MyReferralCode"
                    };
                    string[] listOfTextsOnScreen = {
                    TextAvailableToInvest,
                    TextInvestDescription,
                    TextIdentityVerification,
                    TextMyReferralCode
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get invest dashboard with resubmit view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static InvestDashbaordWithReferralCodeViewItem GetInvestDashbaordWithResubmitViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewAvailableNow;
            string TextAvailableToInvest;
            string TextInvestDescription;
            string TextIdentityVerification;
            string TextMyReferralCode;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextAvailableToInvest = app.Query(AutomationID.labelAvailableNow).LastOrDefault().Text;
                    TextInvestDescription = app.Query(AutomationID.labelInvestDescription1).LastOrDefault().Text;
                    TextIdentityVerification = app.Query(AutomationID.labelReferralStatus).LastOrDefault().Text;
                    TextMyReferralCode = app.Query(AutomationID.labelReferralStatus).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextAvailableToInvest = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAvailableNow).Descendant(0))[0]);
                    TextInvestDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelInvestDescription1).Descendant(0))[0]);
                    TextIdentityVerification = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelReferralStatus).Descendant(0))[0]);
                    TextMyReferralCode = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelReferralStatus).Descendant(0))[0]);
                };

                InvestDashbaordWithReferralCodeViewItem result = new InvestDashbaordWithReferralCodeViewItem(
                    TextAvailableToInvest,
                    TextInvestDescription,
                    TextIdentityVerification,
                    TextMyReferralCode
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "AvailableNow",
                    "InvestDescription1",
                    "Resubmit",
                    "MyReferralCode"
                    };
                    string[] listOfTextsOnScreen = {
                    TextAvailableToInvest,
                    TextInvestDescription,
                    TextIdentityVerification,
                    TextMyReferralCode
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get invest dashboard with declined view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static InvestDashbaordWithReferralCodeViewItem GetInvestDashbaordWithDeclinedViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewAvailableNow;
            string TextAvailableToInvest;
            string TextInvestDescription;
            string TextIdentityVerification;
            string TextMyReferralCode;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextAvailableToInvest = app.Query(AutomationID.labelAvailableNow).LastOrDefault().Text;
                    TextInvestDescription = app.Query(AutomationID.labelInvestDescription1).LastOrDefault().Text;
                    TextIdentityVerification = app.Query(AutomationID.labelReferralStatus).LastOrDefault().Text;
                    TextMyReferralCode = app.Query(AutomationID.labelReferralStatus).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextAvailableToInvest = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAvailableNow).Descendant(0))[0]);
                    TextInvestDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelInvestDescription1).Descendant(0))[0]);
                    TextIdentityVerification = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelReferralStatus).Descendant(0))[0]);
                    TextMyReferralCode = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelReferralStatus).Descendant(0))[0]);
                }
                InvestDashbaordWithReferralCodeViewItem result = new InvestDashbaordWithReferralCodeViewItem(
                    TextAvailableToInvest,
                    TextInvestDescription,
                    TextIdentityVerification,
                    TextMyReferralCode
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "AvailableNow",
                    "InvestDescription1",
                    "Declined",
                    "MyReferralCode"
                    };
                    string[] listOfTextsOnScreen = {
                    TextAvailableToInvest,
                    TextInvestDescription,
                    TextIdentityVerification,
                    TextMyReferralCode
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get invest dashboard with approved view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static InvestDashbaordWithReferralCodeViewItem GetInvestDashbaordWithApprovedViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewAvailableNow;
            string TextAvailableToInvest;
            string TextInvestDescription;
            string TextIdentityVerification;
            string TextMyReferralCode;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextAvailableToInvest = app.Query(AutomationID.labelAvailableNow).LastOrDefault().Text;
                    TextInvestDescription = app.Query(AutomationID.labelInvestDescription1).LastOrDefault().Text;
                    TextIdentityVerification = app.Query(AutomationID.labelReferralStatus).LastOrDefault().Text;
                    TextMyReferralCode = app.Query(AutomationID.labelMyReferralCode).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextAvailableToInvest = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelAvailableNow).Descendant(0))[0]);
                    TextInvestDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelInvestDescription1).Descendant(0))[0]);
                    TextIdentityVerification = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelReferralStatus).Descendant(0))[0]);
                    TextMyReferralCode = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelMyReferralCode).Descendant(0))[0]);
                };

                InvestDashbaordWithReferralCodeViewItem result = new InvestDashbaordWithReferralCodeViewItem(
                    TextAvailableToInvest,
                    TextInvestDescription,
                    TextIdentityVerification,
                    TextMyReferralCode
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "AvailableNow",
                    "InvestDescription1",
                    "Approved",
                    "MyReferralCode"
                    };
                    string[] listOfTextsOnScreen = {
                    TextAvailableToInvest,
                    TextInvestDescription,
                    TextIdentityVerification,
                    TextMyReferralCode
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get resubmit view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PageWithDescriptionAndOneButton GetResubmitViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewVerificationResubmit;
            string TextTitle;
            string TextDescription;
            string TextButton;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextTitle = app.Query(AutomationID.labelVerificationResubmit).LastOrDefault().Text;
                    TextDescription = app.Query(AutomationID.labelVerificationResubmitParagraph1).LastOrDefault().Text;
                    TextButton = app.Query(AutomationID.buttonContinue).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextTitle = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelVerificationResubmit).Descendant(0))[0]);
                    TextDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelVerificationResubmitParagraph1).Descendant(0))[0]);
                    TextButton = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonContinue).Descendant(0))[0]);
                }
                PageWithDescriptionAndOneButton result = new PageWithDescriptionAndOneButton(
                    TextTitle,
                    TextDescription,
                    TextButton
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "Resubmit",
                    "VerificationResubmitParagraph1",
                    "Continue",
                    };
                    string[] listOfTextsOnScreen = {
                    TextTitle,
                    TextDescription,
                    TextButton
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get declined view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PageWithDescriptionAndOneButton GetDeclinedViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewVerificationFailed;
            string TextTitle;
            string TextDescription;
            string TextButton;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextTitle = app.Query(AutomationID.labelVerificationFailed).LastOrDefault().Text;
                    TextDescription = app.Query(AutomationID.labelVerificationFailedParagraph1).LastOrDefault().Text;
                    TextButton = app.Query(AutomationID.buttonDone).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextTitle = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelVerificationFailed).Descendant(0))[0]);
                    TextDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelVerificationFailedParagraph1).Descendant(0))[0]);
                    TextButton = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonDone).Descendant(0))[0]);
                };

                PageWithDescriptionAndOneButton result = new PageWithDescriptionAndOneButton(
                    TextTitle,
                    TextDescription,
                    TextButton
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "Declined",
                    "VerificationFailedParagraph1",
                    "Done",
                    };
                    string[] listOfTextsOnScreen = {
                    TextTitle,
                    TextDescription,
                    TextButton
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }

        /// <summary>
        /// Get approved view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="platform"></param>
        /// <param name="chosenLanguage"></param>
        /// <param name="isLocalizationTest"></param>
        /// <returns></returns>
        public static PageWithDescriptionAndOneButton GetApprovedViewItem(IApp app, Platform platform, ELanguage chosenLanguage = ELanguage.English, bool isLocalizationTest = false)
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

            string viewName = AutomationID.viewVerificationComplete;
            string TextTitle;
            string TextDescription;
            string TextButton;
            try
            {
                if (platform == Platform.Android) // Android
                {
                    TextTitle = app.Query(AutomationID.labelVerificationComplete).LastOrDefault().Text;
                    TextDescription = app.Query(AutomationID.labelVerifiedParagraph1).LastOrDefault().Text;
                    TextButton = app.Query(AutomationID.buttonDone).LastOrDefault().Text;
                }
                else // iOS
                {
                    TextTitle = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelVerificationComplete).Descendant(0))[0]);
                    TextDescription = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.labelVerifiedParagraph1).Descendant(0))[0]);
                    TextButton = extractTextContext(app.Query(c => c.Id(viewName).Descendant().Id(AutomationID.buttonDone).Descendant(0))[0]);
                }
                PageWithDescriptionAndOneButton result = new PageWithDescriptionAndOneButton(
                    TextTitle,
                    TextDescription,
                    TextButton
                    );
                if (isLocalizationTest)
                {
                    // To check localzation 
                    string[] listOfPrimaryKeys = {
                    "VerificationComplete",
                    "VerifiedParagraph1",
                    "Done",
                    };
                    string[] listOfTextsOnScreen = {
                    TextTitle,
                    TextDescription,
                    TextButton
                    };
                    Shared.CheckLocalization(chosenLanguage, listOfPrimaryKeys, listOfTextsOnScreen);
                }
                return result;
            }
            catch
            {
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "ERROR Can't parse a label in {0}", "Enter Recovery Phrase Page");
                TestContext.WriteLine("ERROR: Can't parse a label, please check tree in Repl()");
            }
            return null;
        }
    }
}
