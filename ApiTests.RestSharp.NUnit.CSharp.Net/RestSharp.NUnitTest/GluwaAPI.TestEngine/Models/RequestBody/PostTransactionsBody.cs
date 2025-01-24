using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
#nullable enable
    public sealed class PostTransactionsBody
    {
        [Required]
        public string? Signature { get; set; }

        [Required]
        public string? Currency { get; set; }

        [Required]
        public string? Source { get; set; }

        [Required]
        public string? Target { get; set; }

        [Required]
        public string? Amount { get; set; }

        [Required]
        public string? Fee { get; set; }

        [Optional]
        public string? Nonce { get; set; }

        [Optional]
        public string? Note { get; set; }

        [Optional]
        public string? Idem { get; set; }

        public PostTransactionsBody(
            string signature,
            string amount,
            string source,
            string currency,
            string target,
            string fee,
            string idem,
            string note
            )
        {
            Signature = signature;
            Amount = amount;
            Source = source;
            Currency = currency;
            Target = target;
            Fee = fee;
            Idem = idem;
            Note = note;
        }



        public PostTransactionsBody(){}

    }
}
