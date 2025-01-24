namespace GluwaAPI.TestEngine.Models
{

    public sealed class CreateSignatureResponse
    {

        public string reserveTxnSignature { get; set; }

        /// <summary>
        /// Transaction signature used to return funds
        /// </summary>

        public string reclaimTxnSignature { get; set; }

        /// <summary>
        /// Transaction signature used to execute exchange
        /// </summary>

        public string executeTxnSignature { get; private set; }

        public CreateSignatureResponse(
            string ReserveTxnSignature,
            string ReclaimTxnSignature,
            string ExecuteTxnSignature)
        {
            reserveTxnSignature = ReserveTxnSignature;
            reclaimTxnSignature = ReclaimTxnSignature;
            executeTxnSignature = ExecuteTxnSignature;
        }

    }

    public sealed class GluwacoinSignature
    {
        public string ReserveTxnSignature { get; set; }

        /// <summary>
        /// Transaction signature used to return funds
        /// </summary>

        public string Nonce { get; set; }

        public GluwacoinSignature(
            string reserveTxnSignature,
            string nonce
           )
        {
            ReserveTxnSignature = reserveTxnSignature;
            Nonce = nonce;
        }

        public GluwacoinSignature()
        {
        }
    }
}
