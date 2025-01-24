using Newtonsoft.Json;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    /// <summary>
    /// Request Body for PATCH v1/ExchangeRequest/ID
    /// </summary>
    public sealed class PatchExchangeRequestBody
    {
        /// <summary>
        /// 
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// Maker Sending Address that made the order
        /// </summary>
        public string SendingAddress { get; private set; }

        /// <summary>
        /// Reserve Txn signature for reserving the exchange
        /// </summary>
        public string ReserveTxnSignature { get; set; }

        /// <summary>
        /// Execute Txn Signature for executing the exchange
        /// </summary>
        public string ExecuteTxnSignature { get; set; }

        /// <summary>
        /// Reclaim Txn Signature for reclaiming exchange
        /// </summary>
        public string ReclaimTxnSignature { get; set; }

        [JsonConstructor]
        public PatchExchangeRequestBody(string sendingAddress)
        {
            SendingAddress = sendingAddress;

        }


    }
}
