using Gluwa.SDK_dotnet.Models.Exchange;
using System;
using System.Numerics;

namespace Gluwa.SDK_dotnet.Tests.Models
{
    public sealed class AcceptExchangeRequest
    {
        /// <summary>
        /// Excahnge Request ID
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// Conversion symbol for the exchange.
        /// </summary>
        public EConversion? Conversion { get; set; }

        /// <summary>
        /// The address where the source amount must be sent to.
        /// </summary>
        public string DestinationAddress { get; set; }

        /// <summary>
        /// The amount in source currency.
        /// </summary>
        public string SourceAmount { get; set; }

        /// <summary>
        /// The fee amount that must be used to create ReserveTxnSignature, ExecuteTxnSignature and ReclaimTxnSignature
        /// </summary>
        public string Fee { get; set; }

        /// <summary>
        /// Optional. Included only when the source currency is a Gluwacoin currency.
        /// </summary>
        public string Executor { get; set; }

        /// <summary>
        /// Optional. Included only when the source currency is a Gluwacoin currency.
        /// </summary>
        public BigInteger? ExpiryBlockNumber { get; set; }

        /// <summary>
        /// Optional. Required if the source currency is BTC. 
        /// </summary>
        public string ReservedFundsAddress { get; set; }

        /// <summary>
        /// Optional. Required if the source currency is BTC. 
        /// </summary>
        public string ReservedFundsRedeemScript { get; set; }
    }
}
