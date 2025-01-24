namespace GluwaPro.UITest.TestUtilities.Models.InvestViewModels
{
    class WeNeedMoreInformationFirstViewItem
    {
        //To test localization
        public string TextWeNeedMoreInformationFirst { get; private set; }
        public string TextDescription { get; private set; }
        public string TextFirstName { get; private set; }
        public string TextLastName { get; private set; }
        public string TextDateOfBirth { get; private set; }
        public string TextStreetAddress { get; private set; }
        public string TextCity { get; private set; }
        public string TestRegionState { get; private set; }
        public string TestPostalZipcode { get; private set; }
        public string TestCountry { get; private set; }
        public string TestNationality { get; private set; }
        public string TestPlaceOfBirth { get; private set; }
        public string TestPassportIDNumber { get; private set; }
        public string TestOccupation { get; private set; }
        public string TestSourceOfFunds { get; private set; }
        public string TestCountinue { get; private set; }

        public WeNeedMoreInformationFirstViewItem(
            string textWeNeedMoreInformationFirst,
            string textDescription,
            string textFirstName,
            string textLastName,
            string textDateOfBirth,
            string textStreetAddress,
            string textCity,
            string testRegionState,
            string testPostalZipcode,
            string testCountry,
            string testNationality,
            string testPlaceOfBirth,
            string testPassportIDNumber,
            string testOccupation,
            string testSourceOfFunds,
            string testCountinue
            )
        {
            TextWeNeedMoreInformationFirst = textWeNeedMoreInformationFirst;
            TextDescription = textDescription;
            TextFirstName = textFirstName;
            TextLastName = textLastName;
            TextDateOfBirth = textDateOfBirth;
            TextStreetAddress = textStreetAddress;
            TextCity = textCity;
            TestRegionState = testRegionState;
            TestPostalZipcode = testPostalZipcode;
            TestCountry = testCountry;
            TestNationality = testNationality;
            TestPlaceOfBirth = testPlaceOfBirth;
            TestPassportIDNumber = testPassportIDNumber;
            TestOccupation = testOccupation;
            TestSourceOfFunds = testSourceOfFunds;
            TestCountinue = testCountinue;
        }
    }
}
