using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models.ResponseBody
{
#nullable enable
    public sealed class GetBalanceResponse
    {
        [Required]
        public string? Balance { get; set; }

        [Required]
        public string Currency { get; set; }

        public List<UnspentOutputs> UnspentOutputs { get; set; }

        public GetBalanceResponse(string balance, string currency, List<UnspentOutputs> unspentOutputs)
        {
            Balance = balance;
            Currency = currency;
            UnspentOutputs = unspentOutputs;
        }
    }


#nullable enable
    public class UnspentOutputs
    {

        [Required]
        public string? Amount { get; set; }

        [Required]
        public string? TxnHash { get; set; }

        [Required]
        public string? Index { get; set; }

        [Required]
        public string? Confirmations { get; set; }

        [Required]
        public string? Tx { get; set; }

        public UnspentOutputs(
            string amount,
            string txnHash,
            string index,
            string confirmations,
            string tx)
        {
            Amount = amount;
            TxnHash = txnHash;
            Index = index;
            Confirmations = confirmations;
            Tx = tx;
        }

    }
}
