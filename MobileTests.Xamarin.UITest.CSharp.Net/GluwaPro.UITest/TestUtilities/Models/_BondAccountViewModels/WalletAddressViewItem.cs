using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.BondAccountViewModels
{
    class WalletAddressViewItem
    {
        public string TextAddress { get; private set; }
        public string TextWalletAddress { get; private set; }
        public string TextDesciption { get; private set; }
        public string TextNext { get; private set; }

        public WalletAddressViewItem(
            string textAddress,
            string textWalletAddress,
            string textDescription,
            string textNext
            )
        {
            TextAddress = textAddress;
            TextWalletAddress = textWalletAddress;
            TextDesciption = textDescription;
            TextNext = textNext;
        }
    }
}
