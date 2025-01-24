using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.BondAccountViewModels
{
    class DepositTransactionPreviewViewItem
    {
        public string TextTransactionPreview { get; private set; }
        public string TextTotal { get; private set; }
        public string TextTransferFrom { get; private set; }
        public string TextDeposit { get; private set; }

        public DepositTransactionPreviewViewItem(
            string textTransactionPreview,
            string textTotal,
            string textTransferFrom,
            string textDeposit
            )
        {
            TextTransactionPreview = textTransactionPreview;
            TextTotal = textTotal;
            TextTransferFrom = textTransferFrom;
            TextDeposit = textDeposit;
        }
    }
}
