using Newtonsoft.Json;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostFtaDepositBody
    {
        /// <summary>
        /// Source for deposits
        /// </summary>
        [JsonProperty("ownerAddress")]
        public string OwnerAddress { get; set; }

        /// <summary>
        /// Gluwa Nonce
        /// </summary>
        [JsonProperty("gluwaNonce")]
        public string GluwaNonce { get; set; }

        /// <summary>
        /// Operator’s signature
        /// </summary>
        [JsonProperty("depositTxnSignature")]
        public string DepositTxnSignature { get; set; }

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
        /// SL document id
        /// </summary>
        [JsonProperty("sideLetterDocumentId")]
        public string SideLetterDocumentId { get; set; }

        /// <summary>
        /// SA document id
        /// </summary>
        [JsonProperty("subscriptionAgreementDocumentId")]
        public string SubscriptionAgreementDocumentId { get; set; }
    }
}
