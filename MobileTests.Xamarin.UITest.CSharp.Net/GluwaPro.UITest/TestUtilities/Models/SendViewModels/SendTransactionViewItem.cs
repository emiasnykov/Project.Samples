namespace GluwaPro.UITest.TestUtilities.Models.SendViewModels
{
    class SendTransactionViewItem
    { //To test localization
        public string TextTransactionSubmitted { get; private set; }
        public string TextTotal { get; private set; }
        public string TextBlockchainText { get; private set; }
        public string TextButtonFinish { get; private set; }

        // To get values
        public bool IsButtonFinishEnabled { get; private set; }

        public SendTransactionViewItem(
            string textTransactionSubmitted,
            string textTotal,
            string textBlockchainText,
            string textButtonFinish,

            bool isButtonFinishEnabled)
        {
            TextTransactionSubmitted = textTransactionSubmitted;
            TextTotal = textTotal;
            TextBlockchainText = textBlockchainText;
            TextButtonFinish = textButtonFinish;
            IsButtonFinishEnabled = isButtonFinishEnabled;
        }
    }
}
