using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class GrabPhoneViewItem
    {
        //To test localization
        public string TextGrabFriendSpousePhone { get; private set; }
        public string TextDescription { get; private set; }
        public string TextDescription2 { get; private set; }
        public string TextDescription3 { get; private set; }
        public string TextWitnessName { get; private set; }
        public string TextCountryCode { get; private set; }
        public string TextWitnessMobilePhone { get; private set; }
        public string TextRequestOneTimePassword { get; private set; }

        public GrabPhoneViewItem(
            string textGrabFriendSpousePhone,
            string textDescription,
            string textDescription2,
            string textDescription3,
            string textWitnessName,
            string textCountryCode,
            string textWitnessMobilePhone,
            string textRequestOneTimePassword
            )
        {
            TextGrabFriendSpousePhone = textGrabFriendSpousePhone;
            TextDescription = textDescription;
            TextDescription2 = textDescription2;
            TextDescription3 = textDescription3;
            TextWitnessName = textWitnessName;
            TextCountryCode = textCountryCode;
            TextWitnessMobilePhone = textWitnessMobilePhone;
            TextRequestOneTimePassword = textRequestOneTimePassword;
        }
    }
}
