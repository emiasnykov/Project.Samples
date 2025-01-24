namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class EnterVerificationCodeViewItem
    {
        //To test localization
        public string TextEnterVerificationCode { get; private set; }
        public string TextDescription { get; private set; }
        public string TextSubmit { get; private set; }

        public EnterVerificationCodeViewItem(
            string textEnterVerificationCode,
            string textDescription,
            string textSubmit
            )
        {
            TextEnterVerificationCode = textEnterVerificationCode;
            TextDescription = textDescription;
            TextSubmit = textSubmit;
        }
    }
}
