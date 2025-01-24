using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.SharedViewModels
{
    class EnterWalletPasswordViewItem
    {
        public string TextHeaderBar { get; private set; }
        public string TextEnterWalletPassword { get; private set; }
        public string TextButtonConfirm { get; private set; }

        public EnterWalletPasswordViewItem(
            string enterPassword,
            string textEnterWalletPassword,
            string textButtonConfirm)
        {
            TextHeaderBar = enterPassword;
            TextEnterWalletPassword = textEnterWalletPassword;
            TextButtonConfirm = textButtonConfirm;
        }
    }
}
