namespace GluwaPro.UITest.TestUtilities.Models.SendViewModels
{
    public class SendViewItem
    {
        //To test localization
        public string TextTitle { get; private set; }
        public string TextSendBottomNavigation { get; private set; }
        public string TextAddressBottomNavigation { get; private set; }
        public string TextHistoryBottomNavigation { get; private set; }
        public string TextExchangeBottomNavigation { get; private set; }
        public string TextMenuBottomNavigation { get; private set; }
        public string TextButtonNext { get; private set; }

        // To get values
        public bool IsButtonNextEnabled { get; private set; }

        public SendViewItem(
            string textTitle,
            string textSendBottomNavigation,
            string textAddressBottomNavigation,
            string textHistoryBottomNavigation,
            string textExchangeBottomNavigation,
            string textMenuBottomNavigation,
            string textButtonNext,

            bool isButtonNextEnabled)
        {
            TextTitle = textTitle;
            TextSendBottomNavigation = textSendBottomNavigation;
            TextAddressBottomNavigation = textAddressBottomNavigation;
            TextHistoryBottomNavigation = textHistoryBottomNavigation;
            TextExchangeBottomNavigation = textExchangeBottomNavigation;
            TextMenuBottomNavigation = textMenuBottomNavigation;
            TextButtonNext = textButtonNext;

            IsButtonNextEnabled = isButtonNextEnabled;
        }
    }
}
