namespace GluwaPro.UITest.TestUtilities.Models.HomeViewModels
{
    class EnterRecoverPhraseViewItem
    {
        //To test localization
        public string TextTitle { get; private set; }

        public string TextButton { get; private set; }

        public EnterRecoverPhraseViewItem(
            string textTitle,
            string textButton)
        {
            TextTitle = textTitle;
            TextButton = textButton;
        }
    }
}
