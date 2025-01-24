namespace GluwaPro.UITest.TestUtilities.Models.ExchangeViewModels
{
    public class QuoteAcceptFailViewItem
    {
        public QuoteAcceptFailViewItem(string textHeaderBar, string textTitle, string textQuoteTitle, string textMessage, string textQuoteMessage, string textButtonClose)
        {
            TextHeaderBar = textHeaderBar;
            TextTitle = textTitle;
            TextQuoteTitle = textQuoteTitle;
            TextMessage = textMessage;
            TextQuoteMessage = textQuoteMessage;
            TextButtonClose = textButtonClose;
        }

        public string TextHeaderBar { get; private set; }
        public string TextTitle { get; private set; }
        public string TextQuoteTitle { get; private set; }
        public string TextMessage { get; private set; }
        public string TextQuoteMessage { get; private set; }
        public string TextButtonClose
        {
            get; private set;
        }
    }
}
