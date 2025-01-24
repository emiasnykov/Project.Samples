using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.NavigationViewModels
{
    class AdditionalSecurityViewItem
    {
        //To test localization
        public string TextHeaderBar { get; private set; }
        public string TextUsePasscodeLock { get; private set; }

        public AdditionalSecurityViewItem(
            string textHeaderBar,
            string textUsePasscodeLock)
        {
            TextHeaderBar = textHeaderBar;
            TextUsePasscodeLock = textUsePasscodeLock;
        }
    }
}
