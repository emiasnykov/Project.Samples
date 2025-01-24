using Xamarin.UITest;

namespace GluwaPro.UITest.TestUtilities.TestDebugging
{
    public class ReplTools
    {
        public static bool IsEnableRepl { get; set; }

        public static void StartRepl(IApp app)
        {
            if (IsEnableRepl)
            {
                app.Repl();
            }
        }
    }
}
