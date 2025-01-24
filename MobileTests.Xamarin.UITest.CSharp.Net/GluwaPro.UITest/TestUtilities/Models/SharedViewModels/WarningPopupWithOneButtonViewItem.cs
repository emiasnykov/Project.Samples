using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.HomeViewModels
{
    class WarningPopupWithOneButtonViewItem
    {
        //To test localization
        public string TextWarning { get; private set; }
        public string TextDesciription { get; private set; }
        public string TextOkay { get; private set; }

        public WarningPopupWithOneButtonViewItem(
            string textWarning,
            string textDesciription,
            string textOkay)
        {
            TextWarning = textWarning;
            TextDesciription = textDesciription;
            TextOkay = textOkay;
        }
    }
}
