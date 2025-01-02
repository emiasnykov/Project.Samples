using Newtonsoft.Json;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostTokenUnwrappingBody
    {
        /// <summary>
        /// Source Token currency
        /// </summary>
        [JsonProperty("sourceToken")]
        public string SourceToken { get; set; }

        /// <summary>
        /// Target Token currency
        /// </summary>
        [JsonProperty("targetToken")]
        public string TargetToken { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// The signature signed by user’s private key
        /// </summary>
        [JsonProperty("burnSignature")]
        public string BurnSignature { get; set; }

        /// <summary>
        /// Nonce
        /// </summary>
        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// Fee
        /// </summary>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// Source address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Unique idem
        /// </summary>
        [JsonProperty("idempotentKey")]
        [Optional]
        public string IdempotentKey { get; set; }
    }
}