using System;
using Newtonsoft.Json;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostInvestmentRequestBody
    {
        /// <summary>
        /// Deposit, Withdraw
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        /// <summary>
        /// Bond, Savings, Prize
        /// </summary>
        [JsonProperty("accountType")]
        public string AccountType { get; set; }

        /// <summary>
        /// Source for deposits, destination for withdrawals
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// For Deposits
        /// </summary>
        [JsonProperty("approveTxnSignature")]
        [Optional]
        public string ApproveTxnSignature { get; set; }

        /// <summary>
        /// For Deposits
        /// </summary>
        [JsonProperty("amount")]
        [Optional]
        public string Amount { get; set; }

        /// <summary>
        /// For Deposits
        /// </summary>
        [JsonProperty("sideLetterDocumentId")]
        [Optional]
        public string SideLetterDocumentId { get; set; }

        /// <summary>
        /// For Deposits
        /// </summary>
        [JsonProperty("subscriptionAgreementDocumentId")]
        [Optional]
        public string SubscriptionAgreementDocumentId { get; set; }
    }
}
