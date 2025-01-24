namespace GluwaAPI.TestEngine.Models.RequestBody
{
    /// <summary>
    /// Gluwa's POST Quote request body
    /// https://docs.gluwa.com/api/order#request-body
    /// </summary>
    public class PostQuoteBody
    {
        /// <summary>
        /// How much do you want to exchange
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Currency conversion according to the quote maker. Format: "SourceTarget"
        /// </summary>
        public string Conversion { get; set; }

        /// <summary>
        /// The address that will fund the source amount
        /// </summary>
        public string SendingAddress { get; set; }

        /// <summary>
        /// The signature of the sending address (X-REQUEST-SIGNATURE)
        /// </summary>
        public string SendingAddressSignature { get; set; }

        /// <summary>
        /// The address that the exchanged currency will be received
        /// </summary>
        public string ReceivingAddress { get; set; }

        /// <summary>
        /// The signature of the receiving address (X-REQUEST-SIGNATURE)
        /// </summary>
        public string ReceivingAddressSignature { get; set; }

        /// <summary>
        /// Only required when source is Btc
        /// </summary>
        [Optional]
        public string BtcPublicKey { get; set; }
    }
}
