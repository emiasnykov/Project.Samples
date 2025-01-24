using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.SendViewModels
{
    class SendPasswordViewItem
    {
        //To test localization
        public string TextEnterWalletPassword { get; private set; }
        public string TextButtonConfirm { get; private set; }
        public string TextForgotPassword { get; private set; }

        // To get values
        public bool IsButtonNextEnabled { get; private set; }

        public SendPasswordViewItem(
            string textEnterWalletPassword,
            string textButtonConfirm,
            string textForgotPassword
            )
        {
            TextEnterWalletPassword = textEnterWalletPassword;
            TextButtonConfirm = textButtonConfirm;
            TextForgotPassword = textForgotPassword;
        }
    }
}
