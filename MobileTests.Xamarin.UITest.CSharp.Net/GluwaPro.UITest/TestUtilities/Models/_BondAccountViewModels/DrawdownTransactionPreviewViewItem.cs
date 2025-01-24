using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.BondAccountViewModels
{
    class DrawdownTransactionPreviewViewItem
    {
        public string TextTransactionPreview { get; private set; }
        public string TextTotoal { get; private set; }
        public string TextTransferTo { get; private set; }
        public string TextDrawDown { get; private set; }

        public DrawdownTransactionPreviewViewItem(
            string textTransactionPreview,
            string textTotal,
            string textTransferTo,
            string textDrawDown
            )
        {
            TextTransactionPreview = textTransactionPreview;
            TextTotoal = textTotal;
            TextTransferTo = textTransferTo;
            TextDrawDown = textDrawDown;
        }
    }
}
