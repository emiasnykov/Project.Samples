using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.MenuViewModels
{
    class IdentificationLoginViewItem
    {
        //To test localization
        public string TextHeaderBar { get; private set; }
        public string TextButtonLogin { get; private set; }
        public string TextButtonForgotPassword { get; private set; }

        public IdentificationLoginViewItem(
            string textHeaderBar,
            string textButtonLogin,
            string textButtonForgotPassword)
        {
            TextHeaderBar = textHeaderBar;
            TextButtonLogin = textButtonLogin;
            TextButtonForgotPassword = textButtonForgotPassword;
        }
    }
}
