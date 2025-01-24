using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class PleaseDocumentsLinkedViewItem
    {
        //To test localization
        public string TextPleaseDocumentsLinked { get; private set; }
        public string TextDescription { get; private set; }
        public string TextDescription2 { get; private set; }
        public string TextIAgree { get; private set; }

        public PleaseDocumentsLinkedViewItem(
            string textPleaseDocumentsLinked,
            string textDescription,
            string textDescription2,
            string textIAgree
            )
        {
            TextPleaseDocumentsLinked = textPleaseDocumentsLinked;
            TextDescription = textDescription;
            TextDescription2 = textDescription2;
            TextIAgree = textIAgree;
        }
    }
}
