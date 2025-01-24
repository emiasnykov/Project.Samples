namespace GluwaPro.UITest.TestUtilities.Models.InvestPreDepositViewModels
{
    class DocumentRequiredCommonViewItem
    {//To test localization
        public string TextTitle { get; private set; }
        public string TextDescription { get; private set; }
        public string TextButton { get; private set; }

        public DocumentRequiredCommonViewItem(
            string textTitle,
            string textDescription,
            string textButton
            )
        {
            TextTitle = textTitle;
            TextDescription = textDescription;
            TextButton = textButton;
        }
    }
}
