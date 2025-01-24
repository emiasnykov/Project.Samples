using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.BondAccountViewModels
{
    class TransactionHistoryViewItem
    {
        //To test localization
        public string TextTransactions { get; private set; }
        public string TextDeposit { get; private set; }
        public string TextDrawdowns { get; private set; }
        public string TextBondAccDeposit { get; private set; }
        public string TextSavingAccDeposit { get; private set; }

        public TransactionHistoryViewItem(
            string textTransactions,
            string textDeposit,
            string textDrawdowns,
            string textBondAccDeposit,
            string textSavingAccDeposit
            )
        {
            TextTransactions = textTransactions;
            TextDeposit = textDeposit;
            TextDrawdowns = textDrawdowns;
            TextBondAccDeposit = textBondAccDeposit;
            TextSavingAccDeposit = textSavingAccDeposit;
        }
    }
}
