using GluwaPro.UITest.TestUtilities.TestDebugging;
using GluwaPro.UITest.TestUtilities.TestLogging;
using NUnit.Framework;
using Xamarin.UITest;

namespace GluwaPro.UITest.TestUtilities.Methods.HomeMethod
{
    class HomeViewHandle
    {      
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
                ReplTools.StartRepl(app);
                ScreenshotTools.TakeErrorScreenshot(app, "Unexpected view, expecting {0}", expectedView);
                Assert.Fail("Unexpected view, expecting {0}", expectedView);
            }
            ScreenshotTools.TakePosScreenshot(app, expectedView);
        }
    }
}
