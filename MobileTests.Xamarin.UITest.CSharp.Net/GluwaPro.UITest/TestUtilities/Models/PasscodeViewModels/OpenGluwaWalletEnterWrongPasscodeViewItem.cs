using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.PasscodeViewModels
{
    class OpenGluwaWalletEnterWrongPasscodeViewItem
    {
        //To test localization
        public string TextOpenGluwaWallet { get; private set; }
        public string TextTitlePasscode { get; private set; }
        public string TextForgotPasscode { get; private set; }

        public OpenGluwaWalletEnterWrongPasscodeViewItem(
            string textOpenGluwaWallet,
            string textTitlePasscode,
            string textForgotPasscode)
        {
            TextOpenGluwaWallet = textOpenGluwaWallet;
            TextTitlePasscode = textTitlePasscode;
            TextForgotPasscode = textForgotPasscode;
        }
    }
}
