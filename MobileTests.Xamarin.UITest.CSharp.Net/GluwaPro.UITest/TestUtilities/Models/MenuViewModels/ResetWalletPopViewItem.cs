namespace GluwaPro.UITest.TestUtilities.Models.NavigationViewModels
{
    class ResetWalletPopViewItem
    {
        //To test localization
        public string TextAlertTitle { get; private set; }
        public string TextMessage { get; private set; }
        public string TextCancel { get; private set; }
        public string TextReset { get; private set; }

        public ResetWalletPopViewItem(
            string textAlertTitle,
            string textMessage,
            string textCancel,
            string textReset)
        {
            TextAlertTitle = textAlertTitle;
            TextMessage = textMessage;
            TextCancel = textCancel;
            TextReset = textReset;
        }
    }
}
