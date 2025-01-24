namespace GluwaPro.UITest.TestUtilities.Models.ExchangeViewModels
{
    public class QuoteCreateFailViewItem
    {
        public string TextTitleQuoteCreateFail { get; private set; }
        public string TextTitle { get; private set; }
        public string TextQuoteDetail { get; private set; }
        public string TextMessage { get; private set; }
        public string TextQuoteMessage { get; private set; }
        public string TextFinish { get; private set; }

        public QuoteCreateFailViewItem(
            string textTitleQuoteCreateFail,
            string textTitle,
            string textQuoteDetail,
            string textMessage,
            string textQuoteMessage,
            string textFinish)
        {
            TextTitleQuoteCreateFail = textTitleQuoteCreateFail;
            TextTitle = textTitle;
            TextQuoteDetail = textQuoteDetail;
            TextMessage = textMessage;
            TextQuoteMessage = textQuoteMessage;
            TextFinish = textFinish;
        }
    }
}
