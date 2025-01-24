namespace GluwaPro.UITest.TestUtilities.Models.HomeViewModels
{
    class PageWithOneButtonViewItem
    {
        //To test localization
        public string TextTitle { get; private set; }
        public string TextDesciription { get; private set; }
        public string TextButton { get; private set; }

        public PageWithOneButtonViewItem(
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
