using Newtonsoft.Json;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostTokenWrappingBody
    {
        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Source address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// The raw transaction signature
        /// </summary>
        [JsonProperty("approveTxnSignature")]
        public string ApproveTxnSignature { get; set; }

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
        /// The signature signed by the address
        /// </summary>
        [JsonProperty("mintTxnSignature")]
        public string MintTxnSignature { get; set; }

        /// <summary>
        /// Unique idem
        /// </summary>
        [JsonProperty("idempotentKey")]
        [Optional]
        public string IdempotentKey { get; set; }
    }
}