using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.BondAccountViewModels
{
    class TransactionHistoryDepositConfirmFailViewItem
    {
        //To test localization
        public string TextTitleDeposit { get; private set; }
        public string TextDepositPending { get; private set; }
        public string TextTransferFrom { get; private set; }
        public string TextTransferType { get; private set; }
        public string TextDaysUntilMaturityAvailableToDrawdown { get; private set; }
        public string TextMaturityDate { get; private set; }
        public string TextTransactionSubmitted { get; private set; }
        public string TextTransactionConfirmed { get; private set; }
        public string TextTransactionReference { get; private set; }

        public TransactionHistoryDepositConfirmFailViewItem(
            string textTitleDeposit,
            string textDepositPending,
            string textTransferFrom,
            string textTransferType,
            string textDaysUntilMaturityAvailableToDrawdown,
            string textMaturityDate,
            string textTransactionSubmitted,
            string textTransactionConfirmed,
            string textTransactionReference
            )
        {
            TextTitleDeposit = textTitleDeposit;
            TextDepositPending = textDepositPending;
            TextTransferFrom = textTransferFrom;
            TextTransferType = textTransferType;
            TextDaysUntilMaturityAvailableToDrawdown = textDaysUntilMaturityAvailableToDrawdown;
            TextMaturityDate = textMaturityDate;
            TextTransactionSubmitted = textTransactionSubmitted;
            TextTransactionConfirmed = textTransactionConfirmed;
            TextTransactionReference = textTransactionReference;
        }
    }
}
