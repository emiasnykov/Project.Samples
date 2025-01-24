namespace GluwaPro.UITest.TestUtilities.Models.HomeViewModels
{
    class PasswordWithDescriptionViewItem
    {
        //To test localization
        public string TextTitle { get; private set; }
        public string TextDesciription { get; private set; }
        public string TextButton { get; private set; }

        public PasswordWithDescriptionViewItem(
            string textTitle,
            string textDesciription,
            string textButton)
        {
            TextTitle = textTitle;
            TextDesciription = textDesciription;
            TextButton = textButton;
        }
    }
}
