using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.BondAccountViewModels
{
    class NoTransactionHistoryViewItem
    {
        //To test localization
        public string TextTransactions { get; private set; }
        public string TextDeposit { get; private set; }
        public string TextDrawdowns { get; private set; }

        public NoTransactionHistoryViewItem(
            string textTransactions,
            string textDeposit,
            string textDrawdowns
            )
        {
            TextTransactions = textTransactions;
            TextDeposit = textDeposit;
            TextDrawdowns = textDrawdowns;
        }
    }
}
