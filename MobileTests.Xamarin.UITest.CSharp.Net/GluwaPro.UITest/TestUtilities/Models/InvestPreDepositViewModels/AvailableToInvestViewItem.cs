namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class AvailableToInvestViewItem
    {
        //To test localization
        public string TextAvailableToInvest { get; private set; }
        public string TextInvestDescription { get; private set; }
        public string TextIdentityVerificationRequiredOrMyReferralCode { get; private set; }

        public AvailableToInvestViewItem (
            string textAvailableToInvest, 
            string textInvestDescription, 
            string textIdentityVerificationRequiredOrMyReferralCode
            )
        {
            TextAvailableToInvest = textAvailableToInvest;
            TextInvestDescription = textInvestDescription;
            TextIdentityVerificationRequiredOrMyReferralCode = textIdentityVerificationRequiredOrMyReferralCode;
        }
    }
}
