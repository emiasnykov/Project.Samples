namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class AvailableToInvestExpendViewItem
    {
        //To test localization
        public string TextAvailableToInvest { get; private set; }
        public string TextInvestDescription { get; private set; }
        public string TextIdentityVerification { get; private set; }
        public string TextPersonalInformatione { get; private set; }
        public string TextProofOfAddress { get; private set; }
        public string TextTermsAndConditionsAgreement { get; private set; }
        public string TextOneTimePasswordWitnessVerification { get; private set; }
        public string TextIdentityVerificationRequiredOrMyReferralCode { get; private set; }

        public AvailableToInvestExpendViewItem(
            string textAvailableToInvest,
            string textInvestDescription,
            string textIdentityVerification,
            string textPersonalInformatione,
            string textProofOfAddress,
            string textTermsAndConditionsAgreement,
            string textOneTimePasswordWitnessVerification,
            string textIdentityVerificationRequiredOrMyReferralCode
            )
        {
            TextAvailableToInvest = textAvailableToInvest;
            TextInvestDescription = textInvestDescription;
            TextIdentityVerification = textIdentityVerification;
            TextPersonalInformatione = textPersonalInformatione;
            TextProofOfAddress = textProofOfAddress;
            TextTermsAndConditionsAgreement = textTermsAndConditionsAgreement;
            TextOneTimePasswordWitnessVerification = textOneTimePasswordWitnessVerification;
            TextIdentityVerificationRequiredOrMyReferralCode = textIdentityVerificationRequiredOrMyReferralCode;
        }
    }
}
