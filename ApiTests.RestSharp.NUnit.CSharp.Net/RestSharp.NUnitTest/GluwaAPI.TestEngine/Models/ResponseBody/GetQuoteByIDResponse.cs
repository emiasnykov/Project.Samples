using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models.ResponseBody
{
    /// <summary>
    /// Response body of the GET v1/Quote/ID 
    /// </summary>
    public sealed class GetQuoteByIDResponse
    {
#nullable enable
        public string ID { get; private set; }

        public string SendingAddress { get; private set; }

        public string SourceAmount { get; private set; }

        public string Fee { get; private set; }

        public string EstimatedExchangedAmount { get; private set; }

        public string ReceivingAddress { get; private set; }

        public string Status { get; private set; }

        public string Conversion { get; private set; }

        public List<QuoteMatchedOrders> MatchedOrders { get; private set; }

        public GetQuoteByIDResponse(
            string id,
            string sendingAddress,
            string sourceAmount,
            string fee,
            string estimatedExchangedAmount,
            string receivingAddress,
            string status,
            string conversion,
            List<QuoteMatchedOrders> matchedOrders
            )
        {
            ID = id;
            SendingAddress = sendingAddress;
            SourceAmount = sourceAmount;
            Fee = fee;
            EstimatedExchangedAmount = estimatedExchangedAmount;
            ReceivingAddress = receivingAddress;
            Status = status;
            Conversion = conversion;
            MatchedOrders = matchedOrders;
        }

        public sealed class QuoteMatchedOrders
        {
            [Required]
            public string? SourceAmount { get; private set; }

            [Required]
            public string? Fee { get; private set; }

            [Required]
            public string? Status { get; private set; }

            [Required]
            public string? Price { get; private set; }

            public QuoteMatchedOrders(
                string sourceAmount,
                string fee,
                string status,
                string price)
            {
                SourceAmount = sourceAmount;
                Fee = fee;
                Status = status;
                Price = price;
            }
        }

    }
}
