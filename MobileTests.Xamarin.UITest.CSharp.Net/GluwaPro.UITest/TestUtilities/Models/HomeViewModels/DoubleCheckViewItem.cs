using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.HomeViewModels
{
    class DoubleCheckViewItem
    {
        //To test localization
        public string TextLetsDoubleCheck { get; private set; }
        public string TextSelect1 { get; private set; }
        public string TextSelect2 { get; private set; }
        public string TextSelect3 { get; private set; }
        public string TextSelect4 { get; private set; }
        public string TextDone { get; private set; }

        public DoubleCheckViewItem(
            string textLetsDoubleCheck,
            string textSelect1,
            string textSelect2,
            string textSelect3,
            string textSelect4,
            string textDone)
        {
            TextLetsDoubleCheck = textLetsDoubleCheck;
            TextSelect1 = textSelect1;
            TextSelect2 = textSelect2;
            TextSelect3 = textSelect3;
            TextSelect4 = textSelect4;
            TextDone = textDone;
        }
    }
}
