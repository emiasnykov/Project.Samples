
namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class VerifyOtpRequest
    {
        public class PostEndUserOtpRequestBody
        {
            public string PhoneNumber { get; set; }
            public string Channel { get; set; } = "sms";
            public string Locale { get; set; } = "en";
            public string EndUserFirstName { get; set; }
            public string EndUserLastName { get; set; }
            public string EndUserBirthDate { get; set; }
        }

        public class WitnessOtpRequestBody
        {
            public string PhoneNumber { get; set; }
            public string Channel { get; set; } = "sms";
            public string Locale { get; set; } = "en";
            public string WitnessFirstName { get; set; }
            public string WitnessLastName { get; set; }
            public string WitnessStreetAddress1 { get; set; }
            public string WitnessStreetAddress2 { get; set; }
            public string WitnessCity { get; set; }
            public string WitnessProvince { get; set; }
            public string WitnessPostalCode { get; set; }
            public string WitnessCountry { get; set; }
            public string WitnessNationality { get; set; }
        }

        public class VerifyOtpRequestBody
        {
            public string PhoneNumber { get; set; }
            public string Code { get; set; }
        }
    }
}
