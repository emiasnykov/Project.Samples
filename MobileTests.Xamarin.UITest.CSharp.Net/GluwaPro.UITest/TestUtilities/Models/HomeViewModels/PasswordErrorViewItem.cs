namespace GluwaPro.UITest.TestUtilities.Models.HomeViewModels
{
    class PasswordErrorViewItem
    {
        //To test localization
        public string TextPasswordValidate { get; private set; }

        public PasswordErrorViewItem(
            string textPasswordValidate)
        {
            TextPasswordValidate = textPasswordValidate;
        }
    }
}
