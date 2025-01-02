using Newtonsoft.Json;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostBurnBody
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
        [JsonProperty("burnRawTxnSignature")]
        public string BurnRawTxnSignature { get; set; }

        /// <summary>
        /// Unique idem
        /// </summary>
        [JsonProperty("idem")]
        [Optional]
        public string Idem { get; set; }
    }
}
