using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.InvestPreDepositViewModels
{
    class DrawdownMatureBondAccountBalanceViewItem
    {
        public string TextTitle { get; private set; }
        public string TextDescription { get; private set; }
        public string TextTitleAvailableAmount { get; private set; }
        public string TextAvailableAmount { get; private set; }
        public string TextTitleUnavailableLockedAmount { get; private set; }
        public string TextUnavailableLockedAmount { get; private set; }
        public string TextButton { get; private set; }

        public DrawdownMatureBondAccountBalanceViewItem(
            string textTitle,
            string textDescription,
            string textTitleAvailableAmount,
            string textAvailableAmount,
            string textTitleUnavailableLockedAmount,
            string textUnavailableLockedAmount,
            string textButton
            )
        {
            TextTitle = textTitle;
            TextDescription = textDescription;
            TextTitleAvailableAmount = textTitleAvailableAmount;
            TextAvailableAmount = textAvailableAmount;
            TextTitleUnavailableLockedAmount = textTitleUnavailableLockedAmount;
            TextUnavailableLockedAmount = textUnavailableLockedAmount;
            TextButton = textButton;
        }
    }
}
