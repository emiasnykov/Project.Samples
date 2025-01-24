using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
#nullable enable
    public sealed class PostTransactionReplaceDataBody
    {
        [Optional]
        public string? TransactionId { get; set; }

        [Optional]
        public string? Idem { get; set; }

        [Optional]
        public string? Signature { get; set; }

        [Optional]
        public string? TxnHash { get; set; }

        [Required]
        public string? ReplaceId { get; set; }

        [Required]
        public string? Currency { get; set; }


        public PostTransactionReplaceDataBody(
            string id,
            string idem,
            string signature,
            string hash,
            string replaceId,
            string currency
            )
        {
            TransactionId = id;
            Idem = idem;
            Signature = signature;
            TxnHash = hash;
            ReplaceId = replaceId;
            Currency = currency;
        }

        public PostTransactionReplaceDataBody() { }
    }
}
