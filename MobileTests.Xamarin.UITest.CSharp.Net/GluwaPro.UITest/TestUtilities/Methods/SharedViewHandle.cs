using GluwaPro.UITest.TestUtilities.Methods.LocalizationMethod;
using GluwaPro.UITest.TestUtilities.Models.ExchangeViewModels;
using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using NUnit.Framework;
using Xamarin.UITest;

namespace GluwaPro.UITest.TestUtilities.Methods
{
    class SharedViewHandle
    {
        /// <summary>
        /// Asserting quote create view
        /// Pass in viewBefore if testing "Go Back" button
        /// sendCurrency and receiveCurrency take forms of "Usdg", "Krwg", or "Btc"
        /// </summary>
        /// <param name="viewBefore"></param>
        public static void HandlePasswordViewAsserts(IApp app, Platform platform, string currentPassword, bool isLabelError, ELanguage chosenLanguage, bool isLocalizationTest)
        {
            EnterPasswordViewItem viewInfo = SharedViewInfo.GetEnterPasswordViewItem(app, platform, chosenLanguage, isLocalizationTest);
            ScreenshotTools.AreEqualScreenshot(app, currentPassword, viewInfo.EnterPassword, "Not the same password entered.");
            ScreenshotTools.AreEqualScreenshot(app, isLabelError, viewInfo.IsError, "Unexpected wrong password prompt.");
        }

        public static void HandleView(IApp app, string expectedView)
        {
            try
            {
                // Confirming the app is in the right view
                // Since the Repl() tree shows the app's view are stack on each other
                // Wait for past view can pass even though the app UI's on the new view
                app.WaitForElement(expectedView, Shared.timeOutWaitingForString + " " + expectedView, Shared.oneMin);
            }
            catch
            {
                try
                {
                   app.WaitForElement("viewQuoteCreateFail");      
                }

                catch 
                {
                    ReplTools.StartRepl(app);
                    ScreenshotTools.TakeErrorScreenshot(app, "Unexpected view, expecting {0}", expectedView);
                    Assert.Fail("Unexpected view, expecting {0}", expectedView);
                }

                Assert.Ignore("No matching order was found. Verify that the quote amount entered can be fulfilled by an order ");
            }
            ScreenshotTools.TakePosScreenshot(app, expectedView);
        }
    }
}

