namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class PortfolioDashboardAccountSetupIncompleteExpendViewItem
    {
        //To test localization
        public string TextAccountSetupIncomplete { get; private set; }
        public string TextIdentityVerification { get; private set; }
        public string TextPersonalInformatione { get; private set; }
        public string TextProofOfAddress { get; private set; }
        public string TextTermsAndConditionsAgreement { get; private set; }
        public string TextOneTimePasswordWitnessVerification { get; private set; }
        public string TextTotalValue { get; private set; }
        public string TextTotalDeposits { get; private set; }
        public string TextInterestEarned { get; private set; }
        public string TextCurrentEffectiveAPY { get; private set; }
        public string TextAccount { get; private set; }
        public string TextBond { get; private set; }
        public string TextTransfer { get; private set; }
        public PortfolioDashboardAccountSetupIncompleteExpendViewItem(
            string textAccountSetupIncomplete,
            string textIdentityVerification,
            string textPersonalInformatione,
            string textProofOfAddress,
            string textTermsAndConditionsAgreement,
            string textOneTimePasswordWitnessVerification,
            string textTotalValue,
            string textTotalDeposits,
            string textInterestEarned,
            string textCurrentEffectiveAPY,
            string textAccount,
            string textBond,
            string textTransfer
            )
        {
            TextAccountSetupIncomplete = textAccountSetupIncomplete;
            TextIdentityVerification = textIdentityVerification;
            TextPersonalInformatione = textPersonalInformatione;
            TextProofOfAddress = textProofOfAddress;
            TextTermsAndConditionsAgreement = textTermsAndConditionsAgreement;
            TextOneTimePasswordWitnessVerification = textOneTimePasswordWitnessVerification;
            TextTotalValue = textTotalValue;
            TextTotalDeposits = textTotalDeposits;
            TextInterestEarned = textInterestEarned;
            TextCurrentEffectiveAPY = textCurrentEffectiveAPY;
            TextAccount = textAccount;
            TextBond = textBond;
            TextTransfer = textTransfer;
        }
    }
}
