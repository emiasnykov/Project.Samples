namespace GluwaPro.UITest.TestUtilities.Models.NavigationViewModels
{
    class MenuListBottomViewItem
    {
        //To test localization
        public string TextAddSecurity { get; private set; }
        public string TextSupport { get; private set; }
        public string TextUserGuide { get; private set; }
        public string TextPrivacyAndTerms { get; private set; }
        public string TextResetWallet { get; private set; }

        public MenuListBottomViewItem(
            string textAddSecurity,
            string textSupport,
            string textUserGuide,
            string textPrivacyAndTerms,
            string textResetWallet)
        {
            TextAddSecurity = textAddSecurity;
            TextSupport = textSupport;
            TextUserGuide = textUserGuide;
            TextPrivacyAndTerms = textPrivacyAndTerms;
            TextResetWallet = textResetWallet;
        }
    }
}
