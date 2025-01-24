using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GluwaPro.UITest.TestUtilities.Models.InvestPreViewModels
{
    class AccountSetupStepsCheckListViewItem
    {
        //To test localization
        public string TextAccountSetupStepsCompleted { get; private set; }
        public string TextIdentityVerification { get; private set; }
        public string TextPersonalInformation { get; private set; }
        public string TextProofOfAddress { get; private set; }
        public string TextTermsAndConditionsAgreement { get; private set; }
        public string TextOneTimePasswordWitnessVerification { get; private set; }

        public AccountSetupStepsCheckListViewItem(
            string textAccountSetupStepsCompleted,
            string textIdentityVerification,
            string textPersonalInformation,
            string textProofOfAddress,
            string textTermsAndConditionsAgreement,
            string textOneTimePasswordWitnessVerification
            )
        {
            TextAccountSetupStepsCompleted = textAccountSetupStepsCompleted;
            TextIdentityVerification = textIdentityVerification;
            TextPersonalInformation = textPersonalInformation;
            TextProofOfAddress = textProofOfAddress;
            TextTermsAndConditionsAgreement = textTermsAndConditionsAgreement;
            TextOneTimePasswordWitnessVerification = textOneTimePasswordWitnessVerification;
        }
    }
}
