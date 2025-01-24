using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.NavigationViewModels
{
    class MyWalletsViewItem
    {
        //To test localization
        public string TextMyWallets { get; private set; }

        public MyWalletsViewItem(
            string textMyWallets)
        {
            TextMyWallets = textMyWallets;
        }
    }
}
