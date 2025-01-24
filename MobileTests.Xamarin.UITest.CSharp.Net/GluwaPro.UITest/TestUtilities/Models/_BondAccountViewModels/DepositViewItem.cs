using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.BondAccountViewModels
{
    class DepositViewItem
    {
        public string TextDeposit { get; private set; }
        public string TextMax { get; private set; }
        public string TextNext { get; private set; }

        public DepositViewItem(
            string textDeposit,
            string textMax,
            string textNext
            )
        {
            TextDeposit = textDeposit;
            TextMax = textMax;
            TextNext = textNext;
        }
            
    }
}
