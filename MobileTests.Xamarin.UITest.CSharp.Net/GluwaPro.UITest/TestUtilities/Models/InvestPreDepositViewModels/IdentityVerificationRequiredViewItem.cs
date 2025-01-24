using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class IdentityVerificationRequiredViewItem
    {
        //To test localization
        public string TextIdentityVerificationRequired { get; private set; }
        public string TextOurInvestProductsRequireUsToVerify { get; private set; }
        public string TextWeAreUnableToOfferThisProductToUS1 { get; private set; }
        public string TextSelectBeginToStart { get; private set; }
        public string TextForBestResults1 { get; private set; }
        public string TextWhatIsKYC { get; private set; }
        public string TextBegin { get; private set; }

        public IdentityVerificationRequiredViewItem(
            string textIdentityVerificationRequired,
            string textOurInvestProductsRequireUsToVerify,
            string textWeAreUnableToOfferThisProductToUS1,
            string textSelectBeginToStart,
            string textForBestResults1,
            string textWhatIsKYC,
            string textBegin
            )
        {
            TextIdentityVerificationRequired = textIdentityVerificationRequired;
            TextOurInvestProductsRequireUsToVerify = textOurInvestProductsRequireUsToVerify;
            TextWeAreUnableToOfferThisProductToUS1 = textWeAreUnableToOfferThisProductToUS1;
            TextSelectBeginToStart = textSelectBeginToStart;
            TextForBestResults1 = textForBestResults1;
            TextWhatIsKYC = textWhatIsKYC;
            TextBegin = textBegin;
        }
    }
}
