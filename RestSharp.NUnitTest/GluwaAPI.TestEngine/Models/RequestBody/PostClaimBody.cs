using Newtonsoft.Json;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostClaimBody
    {
        /// <summary>
        /// The raw transaction signature
        /// </summary>
        [JsonProperty("signature")]
        public string Signature { get; set; }

        /// <summary>
        /// Source address
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Unique idem
        /// </summary>
        [JsonProperty("idem")]
        [Optional]
        public string Idem { get; set; }
    }
}
