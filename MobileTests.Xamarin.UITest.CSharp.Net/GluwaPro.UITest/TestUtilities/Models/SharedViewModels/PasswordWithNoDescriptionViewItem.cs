namespace GluwaPro.UITest.TestUtilities.Models.HomeViewModels
{
    class PasswordWithNoDescriptionViewItem
    {
        //To test localization
        public string TextTitle { get; private set; }
        public string TextButton { get; private set; }

        public PasswordWithNoDescriptionViewItem(
            string textTitle,
            string textButton)
        {
            TextTitle = textTitle;
            TextButton = textButton;
        }
    }
}
