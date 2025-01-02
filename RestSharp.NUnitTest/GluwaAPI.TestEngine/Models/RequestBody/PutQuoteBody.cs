using System.Collections.Generic;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    /// <summary>
    /// Request Body for PUT v1/Quote (Accept Quote)
    /// </summary>
    public class PutQuoteBody
    {
        /// <summary>
        /// Checksum
        /// </summary>
        public string Checksum { get; set; }

        /// <summary>
        /// Matched orders
        /// </summary>
        public List<MatchedOrderBody> MatchedOrders { get; set; }
    }

    public class MatchedOrderBody
    {
        /// <summary>
        /// OrderID
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// ReserveTxnSignature
        /// </summary>
        public string ReserveTxnSignature { get; set; }

        [Optional]
        public string Nonce { get; set; }

        [Optional]
        public string ExecuteTxnSignature { get; set; }

        [Optional]
        public string ReclaimTxnSignature { get; set; }
    }
}
