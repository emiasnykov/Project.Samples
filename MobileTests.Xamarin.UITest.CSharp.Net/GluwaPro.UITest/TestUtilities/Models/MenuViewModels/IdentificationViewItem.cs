using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.NavigationViewModels
{
    class IdentificationViewItem
    {
        //To test localization
        public string TextHeaderBar { get; private set; }
        public string TextUserIdentification { get; private set; }
        public string TextVerifyYourIdentify { get; private set; }
        public string TextSignup { get; private set; }
        public string TextLogin { get; private set; }
        public IdentificationViewItem(
            string textHeaderBar,
            string textUserIdentification,
            string textVerifyYourIdentify,
            string textSignup,
            string textLogin)
        {
            TextHeaderBar = textHeaderBar;
            TextUserIdentification = textUserIdentification;
            TextVerifyYourIdentify = textVerifyYourIdentify;
            TextSignup = textSignup;
            TextLogin = textLogin;
        }
    }
}
