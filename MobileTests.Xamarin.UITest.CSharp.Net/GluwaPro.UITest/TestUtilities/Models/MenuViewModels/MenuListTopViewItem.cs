namespace GluwaPro.UITest.TestUtilities.Models.NavigationViewModels
{
    class MenuListTopViewItem
    {
        //To test localization
        public string TextMenu { get; private set; }
        public string TextIdentification { get; private set; }
        public string TextSignature { get; private set; }
        public string TextPrivate { get; private set; }
        public string TextAppearance { get; private set; }
        public string TextLanguage { get; private set; }

        public MenuListTopViewItem(
            string textMenu,
            string textIdentification,
            string textSignature,
            string textPrivate,
            string textAppearance,
            string textLanguage
            )
        {
            TextMenu = textMenu;
            TextIdentification = textIdentification;
            TextSignature = textSignature;
            TextPrivate = textPrivate;
            TextAppearance = textAppearance;
            TextLanguage = textLanguage;
        }
    }
}
