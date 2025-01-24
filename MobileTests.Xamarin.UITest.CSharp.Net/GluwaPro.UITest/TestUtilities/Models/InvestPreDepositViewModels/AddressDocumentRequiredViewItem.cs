using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class AddressDocumentRequiredViewItem
    {

        //To test localization
        public string TextAddressDocumentRequired { get; private set; }
        public string TextDescription { get; private set; }
        public string TextDescription2 { get; private set; }
        public string TextDescription3 { get; private set; }
        public string TextUpload { get; private set; }
        public string TextSkip { get; private set; }

        public AddressDocumentRequiredViewItem(
            string textAddressDocumentRequired,
            string textDescription,
            string textDescription2,
            string textDescription3,
            string textUpload,
            string textSkip
            )
        {
            TextAddressDocumentRequired = textAddressDocumentRequired;
            TextDescription = textDescription;
            TextDescription2 = textDescription2;
            TextDescription3 = textDescription3;
            TextUpload = textUpload;
            TextSkip = textSkip;
        }
    }
}
