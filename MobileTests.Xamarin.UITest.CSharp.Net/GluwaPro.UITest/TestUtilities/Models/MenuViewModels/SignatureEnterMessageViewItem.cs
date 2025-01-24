using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.MenuViewModels
{
    class SignatureEnterMessageViewItem
    {
        //To test localization
        public string TextHeaderBar { get; private set; }
        public string TextYouCanSign { get; private set; }
        public string TextSign { get; private set; }
        public SignatureEnterMessageViewItem(
            string textHeaderBar,
            string textYouCanSign,
            string textSign)
        {
            TextHeaderBar = textHeaderBar;
            TextYouCanSign = textYouCanSign;
            TextSign = textSign;
        }
    }
}
