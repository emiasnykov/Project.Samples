using System.Linq;
using Xamarin.UITest;

namespace GluwaPro.UITest.TestUtilities.Methods
{
    class SharedFunction
    {
        /// <summary>
        /// Tap buttons to go to next view and assert next view
        /// </summary>
        /// <param name="buttonToTap"></param>
        /// <param name="expectedNextView"></param>
        public static void TapNavigationButtonAndHandleNextView(IApp app, Platform platform, string buttonToTap, string expectedNextView)
        {
            if (platform == Platform.Android) // Android
            {
                if (expectedNextView == "viewSendPreview" || expectedNextView == "viewSentTransactionSucess" || expectedNextView == AutomationID.labelStatus)
                {
                    app.Tap(c => c.Marked(buttonToTap).Index(1));
                }
                else if (expectedNextView == "viewWellDone")
                {
                    app.Tap(app.Query(c => c.Marked(buttonToTap)).LastOrDefault().Id);
                }
                else if (buttonToTap == AutomationID.buttonLogin)
                {
                    try { app.Tap(c => c.Marked(buttonToTap).Index(1)); }
                    catch { app.Tap(buttonToTap); }
                }
                else
                {
                    app.WaitForElement(buttonToTap);
                    app.Tap(buttonToTap);
                }
            }
            else // iOS
            {
                if (expectedNextView == "viewWellDone" || expectedNextView == "viewSendPreview" || expectedNextView == "viewSentTransactionSucess")
                {
                    app.WaitForElement(c => c.Id(buttonToTap));
                    app.Tap(c => c.Id(buttonToTap).Index(1));
                }
                else if (expectedNextView == "viewQuoteAcceptSuccess")
                {
                    app.Tap(buttonToTap);
                }
                else if (expectedNextView == "viewSend")
                {
                    if (buttonToTap == AutomationID.buttonOpenWallet)
                    {
                        app.Tap(c => c.Id(AutomationID.buttonOpenWallet));
                    }
                    else
                    {
                        app.Tap(c => c.Id(buttonToTap));
                    }
                }
                else if (expectedNextView == "labelRequestOneTimePassword" || expectedNextView == "labelWitnessMobilePhone")
                {
                    app.Tap(buttonToTap);
                }
                else if (buttonToTap == AutomationID.buttonClose)
                {
                    app.Tap(c => c.Marked(AutomationID.buttonClose).Index(1));
                }
                else
                {
                    app.WaitForElement(buttonToTap);
                    app.Tap(c => c.Id(buttonToTap));
                }
            }
            SharedViewHandle.HandleView(app, expectedNextView);
        }
    }
}


