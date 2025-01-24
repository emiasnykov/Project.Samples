using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.BondAccountViewModels
{
    class TransactionSubmittedViewItem
    {
        public string TextTransactionSubmitted { get; private set; }
        public string TextAmount { get; private set; }
        //public string TextStatus { get; private set; }
        public string TextPending { get; private set; }
        public string TextTransaction { get; private set; }
        public string TextDone { get; private set; }

        public TransactionSubmittedViewItem(
            string textTransactionSubmitted,
            string textAmount,
            //string textStatus,
            string textPending,
            string textTransaction,
            string textDone
            )
        {
            TextTransactionSubmitted = textTransactionSubmitted;
            TextAmount = textAmount;
            //TextStatus = textStatus;
            TextPending = textPending;
            TextTransaction = textTransaction;
            TextDone = textDone;
        }
    }
}
