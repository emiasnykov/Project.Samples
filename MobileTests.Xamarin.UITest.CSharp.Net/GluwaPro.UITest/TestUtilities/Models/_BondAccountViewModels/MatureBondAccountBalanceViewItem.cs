using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.BondAccountViewModels
{
    class MatureBondAccountBalanceViewItem
    {
        public string TextMatureBondAccountBalance { get; private set; }
        public string TextDescription { get; private set; }
        public string TextAvailableAmount { get; private set; }
        public string TextUnavailableLockedAmount { get; private set; }
        public string TextDrawDown { get; private set; }

        public MatureBondAccountBalanceViewItem(
            string textMatureBondAccountBalance,
            string textDescription,
            string textAvailableAmount,
            string textUnavailableLockedAmount,
            string textDrawDown
            )
        {
            TextMatureBondAccountBalance = textMatureBondAccountBalance;
            TextDescription = textDescription;
            TextAvailableAmount = textAvailableAmount;
            TextUnavailableLockedAmount = textUnavailableLockedAmount;
            TextDrawDown = textDrawDown;
        }
    }
}
