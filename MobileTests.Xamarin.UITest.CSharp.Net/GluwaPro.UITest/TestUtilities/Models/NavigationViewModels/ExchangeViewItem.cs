using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.NavigationViewModels
{
    class ExchangeViewItem
    {
        //To test localization
        public string TextExchangeSourceCurrency { get; private set; }
        public string TextExchangeTargetCurrency { get; private set; }
        public string TextSendBottomNavigation { get; private set; }
        public string TextAddressBottomNavigation { get; private set; }
        public string TextHistoryBottomNavigation { get; private set; }
        public string TextExchangeBottomNavigation { get; private set; }
        public string TextMenuBottomNavigation { get; private set; }
        public string TextButtonNext { get; private set; }

        // To get values
        public bool IsButtonNextEnabled { get; private set; }

        public ExchangeViewItem(
            string textExchangeSourceCurrency,
            string textExchangeTargetCurrency,
            string textSendBottomNavigation,
            string textAddressBottomNavigation,
            string textHistoryBottomNavigation,
            string textExchangeBottomNavigation,
            string textMenuBottomNavigation,
            string textButtonNext,

            bool isButtonNextEnabled)
        {
            TextExchangeSourceCurrency = textExchangeSourceCurrency;
            TextExchangeTargetCurrency = textExchangeTargetCurrency;
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
