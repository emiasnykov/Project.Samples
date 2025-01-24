using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.MenuViewModels
{
    class AddressViewItem
    {
        //To test localization
        public string TextAddress { get; private set; }
        public string TextShare { get; private set; }
        public string TextFromAddress { get; private set; }

        public AddressViewItem(
            string textAddress,
            string textShare,
            string textFromAddress)
        {
            TextAddress = textAddress;
            TextShare = textShare;
            TextFromAddress = textFromAddress;
        }
    }
}
