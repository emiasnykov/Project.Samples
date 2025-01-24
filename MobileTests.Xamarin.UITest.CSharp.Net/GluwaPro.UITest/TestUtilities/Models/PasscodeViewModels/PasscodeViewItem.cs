using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.PasscodeViewModels
{
    class PasscodeViewItem
    {
        //To test localization
        public string TextHeaderBar { get; private set; }
        public string TextTitlePasscode { get; private set; }
        public PasscodeViewItem(
            string textHeaderBar,
            string textTitlePasscode)
        {
            TextHeaderBar = textHeaderBar;
            TextTitlePasscode = textTitlePasscode;
        }
    }
}
