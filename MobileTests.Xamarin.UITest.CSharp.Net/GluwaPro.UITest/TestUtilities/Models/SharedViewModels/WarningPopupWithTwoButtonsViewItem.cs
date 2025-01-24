namespace GluwaPro.UITest.TestUtilities.Models.HomeViewModels
{
    class WarningPopupWithTwoButtonsViewItem
    {
        //To test localization
        public string TextTitle { get; private set; }
        public string TextDescription { get; private set; }
        public string TextButton1 { get; private set; }
        public string TextButton2 { get; private set; }

        public WarningPopupWithTwoButtonsViewItem(
            string textTitle,
            string textDescription,
            string textButton1,
            string textButton2)
        {
            TextTitle = textTitle;
            TextDescription = textDescription;
            TextButton1 = textButton1;
            TextButton2 = textButton2;
        }
    }
}
