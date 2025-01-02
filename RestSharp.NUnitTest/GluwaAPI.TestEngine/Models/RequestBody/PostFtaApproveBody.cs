using Newtonsoft.Json;
using System;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostFtaApproveBody
    {
        /// <summary>
        /// Source for deposits
        /// </summary>
        [JsonProperty("ownerAddress")]
        public string OwnerAddress { get; set; }

        /// <summary>
        /// Approve Txn Signature
        /// </summary>
        [JsonProperty("approveTxnSignature")]
        public string ApproveTxnSignature { get; set; }

        /// <summary>
        /// Deposit amount
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Pool id
        /// </summary>
        [JsonProperty("poolId")]
        public string PoolID { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        [Optional]
        public string Currency { get; set; }

        /// <summary>
        /// Fee
        /// </summary>
        [JsonProperty("fee")]
        [Optional]
        public string Fee { get; set; }
    }
}
