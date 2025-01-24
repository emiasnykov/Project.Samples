namespace GluwaPro.UITest.TestUtilities.Models.NavigationViewModels
{
    class AppearanceViewItem
    {
        //To test localization
        public string TextHeaderBar { get; private set; }
        public string TextUseDarkMode { get; private set; }

        public AppearanceViewItem(
            string textHeaderBar,
            string textUseDarkMode)
        {
            TextHeaderBar = textHeaderBar;
            TextUseDarkMode = textUseDarkMode;
        }
    }
}
