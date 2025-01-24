namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class PortfolioDashboardViewItem
    {
        //To test localization
        public string TextAccountSetupIncomplete { get; private set; }
        public string TextTotalValue { get; private set; }
        public string TextTotalDeposits { get; private set; }
        public string TextInterestEarned { get; private set; }
        public string TextCurrentEffectiveAPY { get; private set; }
        public string TextAccount { get; private set; }
        public string TextBond { get; private set; }
        public string TextTransfer { get; private set; }
        public PortfolioDashboardViewItem(
            string textAccountSetupIncomplete,
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
