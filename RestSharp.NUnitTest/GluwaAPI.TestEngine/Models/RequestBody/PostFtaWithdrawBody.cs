using Newtonsoft.Json;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostFtaWithdrawBody
    {
        /// <summary>
        /// Source for deposits
        /// </summary>
        [JsonProperty("ownerAddress")]
        public string OwnerAddress { get; set; }

        /// <summary>
        /// Transaction signature
        /// </summary>
        [JsonProperty("txnSignature")]
        public string TxnSignature { get; set; }

        /// <summary>
        /// Pool id
        /// </summary>
        [JsonProperty("poolId")]
        public string PoolID { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("gluwaNonce")]
        public string GluwaNonce { get; set; }
    }
}
