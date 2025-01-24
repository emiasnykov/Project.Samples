using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.MenuViewModels
{
    class HistoryViewItem
    {
        //To test localization
        public string TextTransactionList { get; private set; }
        public string TextAddress { get; private set; }
        public string TextAmount { get; private set; }
        public string TextDateTime { get; private set; }

        public HistoryViewItem(
            string textTransactionList,
            string textAddress,
            string textAmount,
            string textDateTime)
        {
            TextTransactionList = textTransactionList;
            TextAddress = textAddress;
            TextAmount = textAmount;
            TextDateTime = textDateTime;
        }
    }
}
