using Newtonsoft.Json;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostFtaAuthorizationBody
    {
        /// <summary>
        /// Source for deposits
        /// </summary>
        [JsonProperty("ownerAddress")]
        public string OwnerAddress { get; set; }

        /// <summary>
        /// Deposit amount
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Pool id
        /// </summary>
        [JsonProperty("poolId")]
        public string PoolId { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// GluwaNonce
        /// </summary>
        [JsonProperty("gluwaNonce")]
        public string GluwaNonce { get; set; }
    }
}
