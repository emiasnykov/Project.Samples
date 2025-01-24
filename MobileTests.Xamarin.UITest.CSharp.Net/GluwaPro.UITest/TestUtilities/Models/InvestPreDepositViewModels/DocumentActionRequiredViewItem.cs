using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.InvestPreDepositViewModels
{
    class DocumentActionRequiredViewItem
    {
        //To test localization
        public string TextAddressDocumentRequired { get; private set; }
        public string TextDescription { get; private set; }
        public string TextDescription1 { get; private set; }
        public string TextDescription2 { get; private set; }
        public string TextDescription3 { get; private set; }
        public string TextOK { get; private set; }

        public DocumentActionRequiredViewItem(
            string textAddressDocumentRequired,
            string textDescription,
            string textDescription1,
            string textDescription2,
            string textDescription3,
            string textOK
            )
        {
            TextAddressDocumentRequired = textAddressDocumentRequired;
            TextDescription = textDescription;
            TextDescription1 = textDescription1;
            TextDescription2 = textDescription2;
            TextDescription3 = textDescription3;
            TextOK = textOK;
        }
    }
}
