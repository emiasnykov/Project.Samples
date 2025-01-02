using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable enable
namespace GluwaAPI.TestEngine.Models.ResponseBody
{
    public sealed class GetTransactionsResponse
    {
        [Required]
        public string? Status { get; set; }

        [Required]
        public string? Amount { get; set; }

        [Required]
        public string? TotalAmount { get; set; }

        [Required]
        public string? Currency { get; set; }

        [Required]
        public List<string>? Sources { get; set; }

        [Required]
        public List<string>? Targets { get; set; }

        [Required]
        public string TxnHash { get; set; }

        [Required]
        public DateTime? CreatedDateTime { get; set; }

        [Required]
        public DateTime? ModifiedDateTime { get; set; }

        [Required]
        public string? Fee { get; set; }

        [Required]
        public string? Note { get; set; }

        [Required]
        public string? MerchantOrderID { get; set; }

        public GetTransactionsResponse(string status,
                                        string amount,
                                        string fee,
                                        string totalAmount,
                                        string currency,
                                        List<string> sources,
                                        List<string> targets,
                                        string txnHash,
                                        DateTime createdDateTime,
                                        DateTime modifiedDateTime,
                                        string note,
                                        string merchantOrderID)
        {
            Status = status;
            Amount = amount;
            Fee = fee;
            TotalAmount = totalAmount;
            Currency = currency;
            Sources = sources;
            Targets = targets;
            TxnHash = txnHash;
            CreatedDateTime = createdDateTime;
            ModifiedDateTime = modifiedDateTime;
            Note = note;
            MerchantOrderID = merchantOrderID;

        }
    }
}
