using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.BondAccountViewModels
{
    class BondAccountViewItem
    {
        //To test localization
        public string TextBondAccount { get; private set; }
        public string TextTotalBanance { get; private set; }
        public string TextAvailableToDrawdown { get; private set; }
        public string TextInterestAccrued { get; private set; }
        public string TextEffectiveAPY { get; private set; }
        public string TextRecentTransactions { get; private set; }
        public string TextDrawdown { get; private set; }
        public string TextDeposit { get; private set; }

        public BondAccountViewItem(
            string textBondAccount,
            string textTotalBanance,
            string textAvailableToDrawdown,
            string textInterestAccrued,
            string textEffectiveAPY,
            string textRecentTransactions,
            string textDrawdown,
            string textDeposit
            )
        {
            TextBondAccount = textBondAccount;
            TextTotalBanance = textTotalBanance;
            TextAvailableToDrawdown = textAvailableToDrawdown;
            TextInterestAccrued = textInterestAccrued;
            TextEffectiveAPY = textEffectiveAPY;
            TextRecentTransactions = textRecentTransactions;
            TextDrawdown = textDrawdown;
            TextDeposit = textDeposit;
        }
    }
}
