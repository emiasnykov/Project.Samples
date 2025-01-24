using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.NavigationViewModels
{
    class LanguageViewItem
    {
        //To test localization
        public string TextHeaderBar { get; private set; }
        public string TextLanguageDescription { get; private set; }
        public string TextConfirm { get; private set; }

        public LanguageViewItem(
            string textHeaderBar,
            string textLanguageDescription,
            string textConfirm)
        {
            TextHeaderBar = textHeaderBar;
            TextLanguageDescription = textLanguageDescription;
            TextConfirm = textConfirm;
        }
    }
}
