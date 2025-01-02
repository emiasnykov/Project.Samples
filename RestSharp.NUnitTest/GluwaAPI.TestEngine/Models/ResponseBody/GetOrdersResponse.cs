using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models.ResponseBody
{
    public sealed class GetOrdersResponse
    {
#nullable enable
        /// <summary>
        /// Order ID
        /// </summary>
        [Required]
        public string? ID { get; private set; }

        /// <summary>
        /// Conversion of the exchange.
        /// BtcNgng = Btc -> NgNg, NgngBtc = NgNg -> Btc
        /// </summary>
        [Required]
        public string? Conversion { get; set; }

        /// <summary>
        /// The address where the amount in source currency is sent from.
        /// </summary>
        [Required]
        public string SendingAddress { get; private set; }

        /// <summary>
        /// The amount that is currently available for exchange.
        /// </summary>
        [Required]
        public string SourceAmount { get; private set; }

        /// <summary>
        /// Price per source currency.
        /// </summary>
        [Required]
        public string Price { get; private set; }

        /// <summary>
        /// The address where the exchanged currency is received.
        /// </summary>
        [Required]
        public string ReceivingAddress { get; private set; }

        /// <summary>
        /// The status of the order.
        /// Active, Complete, Canceled
        /// </summary>
        [Required]
        public string? Status { get; private set; }

        public GetOrdersResponse(
            string id,
            string conversion,
            string sendingAddress,
            string sourceAmount,
            string price,
            string receivingAddress,
            string status)
        {
            ID = id;
            Conversion = conversion;
            SendingAddress = sendingAddress;
            SourceAmount = sourceAmount;
            Price = price;
            ReceivingAddress = receivingAddress;
            Status = status;
        }
    }



    public struct OrderBookRespose
    {
        [Required]
        public string? Amount { get; set; }

        [Required]
        public string? Price { get; set; }

        public OrderBookRespose(string amount, string price)
        {
            Amount = amount;
            Price = price;
        }
    }


}
