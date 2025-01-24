using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.SendViewModels
{
    class SynchronizingViewItem
    {
        //To test localization
        public string TextMyWallets { get; private set; }

        public SynchronizingViewItem(
            string textMyWallets)
        {
            TextMyWallets = textMyWallets;
        }
    }
}
