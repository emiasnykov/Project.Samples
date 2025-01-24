using Newtonsoft.Json;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostStakeBody
    {
        /// <summary>
        /// Source address
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// The raw transaction signature
        /// </summary>
        [JsonProperty("mintToStakeSignature")]
        public string MintToStakeSignature { get; set; }

        /// <summary>
        /// The raw transaction signature
        /// </summary>
        [JsonProperty("approvalSignature")]
        public string ApprovalSignature { get; set; }

        /// <summary>
        /// Unique idem
        /// </summary>
        [JsonProperty("idem")]
        [Optional]
        public string Idem { get; set; }
    }
}
