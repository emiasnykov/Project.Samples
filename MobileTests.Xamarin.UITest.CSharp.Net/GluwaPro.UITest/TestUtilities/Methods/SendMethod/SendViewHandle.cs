using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using NUnit.Framework;
using Xamarin.UITest;

namespace GluwaPro.UITest.TestUtilities.Methods.SendMethod
{
    class SendViewHandle
    {
        public const string SEND_VIEW = "viewSend";
        public const string SEND_PREVIEW = "viewSendPreview";
        public const string SEND_TO_ADDRESS_VIEW = "viewSendToAddress";
        public const string ENTER_PASSWORD_VIEW = "viewSendPassword";
        public const string SENT_TRANSACTION_SUCESS_VIEW = "viewSentTransactionSucess";
        public const string SENT_TRANSACTION_FAIL_VIEW = "viewSentTransactionFail";

        /// <summary>
        /// Choose send view
        /// </summary>
        /// <param name="app"></param>
        /// <param name="expectedView"></param>
        public static void HandleView(IApp app, string expectedView)
        {
            switch (expectedView)
            {
                case SEND_VIEW:
                    try
                    {
                        app.WaitForNoElement(SENT_TRANSACTION_FAIL_VIEW, "Timed out: SENT_TRANSACTION_FAIL_VIEW still exists", Shared.oneMin);
                        app.WaitForNoElement(SENT_TRANSACTION_SUCESS_VIEW, "Timed out: SENT_TRANSACTION_SUCESS_VIEW still exists", Shared.oneMin);
                    }
                    catch
                    {
                        ReplTools.StartRepl(app);
                        ScreenshotTools.TakeErrorScreenshot(app, "Unexpected view, expecting {0}", SEND_VIEW);
                        Assert.Fail("Unexpected view, expecting {0}", SEND_VIEW);
                    }
                    break;
                default:
                    try
                    {
                        // Confirming the app is in the right view
                        // Since the Repl() tree shows the app's view are stack on each other
                        // Wait for past view can pass even though the app UI's on the new view
                        app.WaitForElement(expectedView);
                    }
                    catch
                    {
                        ReplTools.StartRepl(app);
                        ScreenshotTools.TakeErrorScreenshot(app, "Unexpected view, expecting {0}", expectedView);
                        Assert.Fail("Unexpected view, expecting {0}", expectedView);
                    }
                    break;
            }

            ScreenshotTools.TakePosScreenshot(app, expectedView);
        }
    }
}
