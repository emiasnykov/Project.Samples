using Newtonsoft.Json;
using System;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostKYCDetailsBody
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("addressLine2")]
        [Optional]
        public string AddressLine2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("countryIsoCode")]
        public string CountryIsoCode { get; set; }

        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("birthPlace")]
        public string BirthPlace { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        [JsonProperty("idCardNo")]
        public string IdCardNo { get; set; }

        [JsonProperty("occupation")]
        public string Occupation { get; set; }

        [JsonProperty("sourceOfFunds")]
        public string SourceOfFunds { get; set; }

        [JsonProperty("taxReferenceNumberType")]
        public string TaxReferenceNumberType { get; set; }

        [JsonProperty("taxReferenceNumber")]
        public string TaxReferenceNumber { get; set; }
    }
}
