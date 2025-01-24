namespace GluwaPro.UITest.TestUtilities.Models.NavigationViewModels
{
    class MessageSignedViewItem
    {
        //To test localization
        public string TextMessageSigned { get; private set; }
        public string TextShareMessage { get; private set; }
        public string TextCurrencyAddress { get; private set; }
        public string TextCurrentAddress { get; private set; }
        public string TextCopyAddress { get; private set; }
        public string TextMessage { get; private set; }
        public string TextUserMessage { get; private set; }
        public string TextCopyMessage { get; private set; }
        public string TextSignature { get; private set; }
        public string TextSignatureAddress { get; private set; }
        public string TextCopySignature { get; private set; }
        public string TextDone { get; private set; }

        public MessageSignedViewItem(
            string textMessageSigned,
            string textShareMessage,
            string textCurrencyAddress,
            string textCurrentAddress,
            string textCopyAddress,
            string textMessage,
            string textUserMessage,
            string textCopyMessage,
            string textSignature,
            string textSignatureAddress,
            string textCopySignature,
            string textDone)
        {
            TextMessageSigned = textMessageSigned;;
            TextShareMessage = textShareMessage;
            TextCurrencyAddress = textCurrencyAddress;
            TextCurrentAddress = textCurrentAddress;
            TextCopyAddress = textCopyAddress;
            TextMessage = textMessage;
            TextUserMessage = textUserMessage;
            TextSignature = textSignature;
            TextCopyMessage = textCopyMessage;
            TextSignature = textSignature;
            TextSignatureAddress = textSignatureAddress;
            TextCopySignature = textCopySignature;
            TextDone = textDone;
        }
    }
}
