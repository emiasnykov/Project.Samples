using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models.ResponseBody
{
    /// <summary>
    /// The response generated from GetQuote
    /// </summary>
    public sealed class GetAddressQuoteResponse
    {
#nullable enable
        /// <summary>
        /// Accepted quote ID
        /// </summary>
        [Required]
        public string? ID { get; private set; }

        /// <summary>
        /// The address that funded the source amount
        /// </summary>
        [Required]
        public string? SendingAddress { get; private set; }

        /// <summary>
        /// The total amount
        /// </summary>
        [Required]
        public string? SourceAmount { get; private set; }

        /// <summary>
        /// The total fee of the exchange
        /// </summary>
        [Required]
        public string? Fee { get; private set; }

        /// <summary>
        /// The estimated exchange amount
        /// </summary>
        [Required]
        public string? EstimatedExchangedAmount { get; private set; }

        /// <summary>
        /// The average of all the prices in the list of matched orders
        /// </summary>
        [Required]
        public string? AveragePrice { get; private set; }

        /// <summary>
        /// The best price available in the list of matched orders
        /// </summary>
        [Required]
        public string? BestPrice { get; private set; }

        /// <summary>
        /// The worst price available in the list of matched orders
        /// </summary>
        [Required]
        public string? WorstPrice { get; private set; }

        /// <summary>
        /// the address that the exchanged currency is received
        /// </summary>
        [Required]
        public string? ReceivingAddress { get; private set; }

        /// <summary>
        /// Status of the quote exchange
        /// </summary>
        [Required]
        public string? Status { get; private set; }

        /// <summary>
        /// The conversion of the quote
        /// </summary>
        [Required]
        public string? Conversion { get; private set; }

        public GetAddressQuoteResponse(
            string id,
            string sendingAddress,
            string sourceAmount,
            string fee,
            string estimatedExchangedAmount,
            string averagePrice,
            string bestPrice,
            string worstPrice,
            string receivingAddress,
            string status,
            string conversion)
        {
            ID = id;
            SendingAddress = sendingAddress;
            SourceAmount = sourceAmount;
            Fee = fee;
            EstimatedExchangedAmount = estimatedExchangedAmount;
            AveragePrice = averagePrice;
            BestPrice = bestPrice;
            WorstPrice = worstPrice;
            ReceivingAddress = receivingAddress;
            Status = status;
            Conversion = conversion;
        }
    }

    public sealed class MatchedOrder
    {
        /// <summary>
        /// Order ID
        /// </summary>
        [Required]
        public string? OrderID { get; private set; }

        /// <summary>
        /// The address where the source amount must be sent to
        /// </summary>
        [Required]
        public string? DestinationAddress { get; set; }

        /// <summary>
        /// The amount that is currently available for exchange.
        /// </summary>
        [Required]
        public string SourceAmount { get; private set; }

        /// <summary>
        /// The fee amount used to create ReserveTxnSignature
        /// </summary>
        [Required]
        public string Fee { get; private set; }

        /// <summary>
        /// The amount that is currently available for exchange.
        /// </summary>
        [Required]
        public string ExchangedAmount { get; private set; }

        /// <summary>
        /// Price per source currency.
        /// </summary>
        [Required]
        public string Price { get; private set; }

        /// <summary>
        /// Address which executes the exchange
        /// Gluwacoin only
        /// </summary>
        public string Executor { get; private set; }


        /// <summary>
        /// Block number were the reserve funds expire
        /// Required for Gluwacoin (KRW-G, USD-G,NGN-G)
        /// </summary>
        public string ExpiryBlockNumber { get; private set; }

        /// <summary>
        /// The address where the source amount must be send
        /// to reserve your funds for the exchange.
        /// Required if source =BTC
        /// </summary>
        public string ReservedFundsAddress { get; private set; }


        /// <summary>
        /// BTC Only
        /// </summary>
        public string? ReservedFundsRedeemScript { get; private set; }


        public MatchedOrder(
            string orderId,
            string destinationAddress,
            string sourceAmount,
            string fee,
            string exchangedAmount,
            string price,
            string executor,
            string reservedFundsAddress,
            string reservedFundsRedeemScript,
            string expiryBlockNumber
            )
        {
            OrderID = orderId;
            DestinationAddress = destinationAddress;
            SourceAmount = sourceAmount;
            Fee = fee;
            ExchangedAmount = exchangedAmount;
            Price = price;
            Executor = executor;
            ExpiryBlockNumber = expiryBlockNumber;
            ReservedFundsAddress = reservedFundsAddress;
            ReservedFundsRedeemScript = reservedFundsRedeemScript;
            ExpiryBlockNumber = expiryBlockNumber;


        }


    }


}
