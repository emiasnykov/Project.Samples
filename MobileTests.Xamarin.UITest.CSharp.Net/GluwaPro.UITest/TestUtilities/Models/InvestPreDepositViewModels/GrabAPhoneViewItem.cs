namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class GrabAWitnessNamePhoneViewItem
    {
        //To test localization
        public string TextGrabAPhone { get; private set; }
        public string TextDescription { get; private set; }
        public string TextWitnessName { get; private set; }
        public string TextCountryCode { get; private set; }
        public string TextWitnessMobilePhone { get; private set; }
        public string TextRequestOneTimePassword { get; private set; }
        public GrabAWitnessNamePhoneViewItem(
            string textGrabAPhone,
            string textDescription,
            string textWitnessName,
            string textCountryCode,
            string textWitnessMobilePhone,
            string textRequestOneTimePassword
            )
        {
            TextGrabAPhone = textGrabAPhone;
            TextDescription = textDescription;
            TextCountryCode = textCountryCode;
            TextWitnessMobilePhone = textWitnessMobilePhone;
            TextRequestOneTimePassword = textRequestOneTimePassword;
        }
    }
}
