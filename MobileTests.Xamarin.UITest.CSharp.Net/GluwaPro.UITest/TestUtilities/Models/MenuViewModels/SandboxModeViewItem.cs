using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.NavigationViewModels
{
    class SandboxModeViewItem
    {
        //To test localization
        public string TextHeaderBar { get; private set; }
        public string TextUseSandboxMode { get; private set; }
        public string TextSandboxDescription { get; private set; }
        public string TextConfirm { get; private set; }

        public SandboxModeViewItem(
            string textHeaderBar,
            string textUseSandboxMode,
            string textSandboxDescription,
            string textConfirm)
        {
            TextHeaderBar = textHeaderBar;
            TextUseSandboxMode = textUseSandboxMode;
            TextSandboxDescription = textSandboxDescription;
            TextConfirm = textConfirm;
        }
    }
}
