namespace GluwaPro.UITest.TestUtilities.Models.ExchangeViewModels
{
    public class EnterPasswordViewItem
    {
        public string EnterPassword { get; private set; }
        public string TextError { get; private set; }
        public bool IsError { get; private set; }

        public EnterPasswordViewItem(
            string enterPassword,
            bool bError)
        {
            EnterPassword = enterPassword;
            IsError = bError;
        }
    }
}
