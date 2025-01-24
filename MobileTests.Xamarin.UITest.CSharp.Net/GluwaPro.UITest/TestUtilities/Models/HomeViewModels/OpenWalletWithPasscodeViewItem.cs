using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.HomeViewModels
{
    class OpenWalletWithPasscodeViewItem
    {
        //To test localization
        public string TextOpenGluwaWallet { get; private set; }
        public string TextEnterPasscode { get; private set; }
        public string TextForgotPasscode { get; private set; }

        public OpenWalletWithPasscodeViewItem(
            string textOpenGluwaWallet,
            string textEnterPasscode,
            string textForgotPasscode)
        {
            TextOpenGluwaWallet = textOpenGluwaWallet;
            TextEnterPasscode = textEnterPasscode;
            TextForgotPasscode = textForgotPasscode;
        }
    }
}
