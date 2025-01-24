using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.InvestPreDepositViewModels
{
    class DrawdownTransactionViewItem
    {
        public string TextTransactionPreview { get; private set; }
        public string TextTotal { get; private set; }
        public string TextTransferTo { get; private set; }
        public string TextDrawDown { get; private set; }

        public DrawdownTransactionViewItem(
            string textTransactionPreview,
            string textTotal,
            string textTransferTo,
            string textDrawDown
            )
        {
            TextTransactionPreview = textTransactionPreview;
            TextTotal = textTotal;
            TextTransferTo = textTransferTo;
            TextDrawDown = textDrawDown;
        }
    }
}
