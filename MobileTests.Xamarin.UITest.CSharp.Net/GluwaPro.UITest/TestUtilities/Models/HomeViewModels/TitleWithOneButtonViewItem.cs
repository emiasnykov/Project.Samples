namespace GluwaPro.UITest.TestUtilities.Models.HomeViewModels
{
    class TitleWithOneButtonViewItem
    {
        //To test localization
        public string TextTitle { get; private set; }
        public string TextButton { get; private set; }

        public TitleWithOneButtonViewItem(
            string textTitle,
            string textButton)
        {
            TextTitle = textTitle;
            TextButton = textButton;
        }
    }
}
