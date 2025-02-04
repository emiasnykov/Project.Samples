using Newtonsoft.Json;

namespace Gluwa.SDK_dotnet.Tests.Models
{
    public sealed class GetExchangeRequestResponse
    {
        /// <summary>
        /// /// Currency conversion according to the order maker. Format: "SourceTarget"
        /// </summary>
        public string Conversion { get; private set; }

        /// <summary>
        /// DestinationAddress
        /// </summary>
        public string DestinationAddress { get; private set; }

        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// SourceAmount
        /// </summary>
        public string SourceAmount { get; private set; }

        /// <summary>
        /// Fee
        /// </summary>
        public string Fee { get; private set; }

        /// <summary>
        /// Executor
        /// </summary>
        public string Executor { get; private set; }

        /// <summary>
        /// ExpiryBlockNumber
        /// </summary>
        public string ExpiryBlockNumber { get; private set; }

        /// <summary>
        /// ReservedFundsAddress
        /// </summary>
        public string ReservedFundsAddress { get; private set; }

        /// <summary>
        /// ReservedFundsRedeemScript
        /// </summary>
        public string ReservedFundsRedeemScript { get; private set; }


        [JsonConstructor]
        public GetExchangeRequestResponse(
            string conversion,
            string destinationAddress,
            string id,
            string sourceAmount,
            string fee,
            string executor,
            string expiryBlockNumber,
            string reservedFundsAddress,
            string reservedFundsRedeemScript)
        {
            Conversion = conversion;
            DestinationAddress = destinationAddress;
            ID = id;
            SourceAmount = sourceAmount;
            Fee = fee;
            Executor = executor;
            ExpiryBlockNumber = expiryBlockNumber;
            ReservedFundsAddress = reservedFundsAddress;
            ReservedFundsRedeemScript = reservedFundsRedeemScript;
        }
    }
}
