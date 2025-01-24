using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.PasscodeViewModels
{
    class ChangePasscodeViewItem
    {
        //To test localization
        public string TextHeaderBar { get; private set; }
        public string TextTitlePasscode { get; private set; }
        public string TextTitleChangePasscode { get; private set; }

        public ChangePasscodeViewItem(
            string textHeaderBar,
            string textTitlePasscode,
            string textTitleChangePasscode)
        {
            TextHeaderBar = textHeaderBar;
            TextTitlePasscode = textTitlePasscode;
            TextTitleChangePasscode = textTitleChangePasscode;
        }
    }
}
