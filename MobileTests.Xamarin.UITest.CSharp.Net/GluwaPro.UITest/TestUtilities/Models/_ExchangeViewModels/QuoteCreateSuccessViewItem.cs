namespace GluwaPro.UITest.TestUtilities.Models.ExchangeViewModels
{
    public class QuoteCreateSuccessViewItem
    {
        public string TextYouSend { get; private set; }
        public string YouSendAmount { get; private set; }
        public string TextFee { get; private set; }
        public string FeeAmount { get; private set; }
        public string TextConverted { get; private set; }
        public string ConvertedAmount { get; private set; }
        public string TextEstimatedExchangeRate { get; private set; }
        public string EstimatedExchangeRateAmount { get; private set; }
        public string TextYouGet { get; private set; }
        public string YouGetAmount { get; private set; }
        public string TextYouGetDetail { get; private set; }
        public string ErrorMessage { get; set; }
        public string QuoteCreatedText { get; set; }
        public bool IsButtonAccept { get; private set; }


        public QuoteCreateSuccessViewItem(
            string textYouSend,
            string youSendAmount,
            string textFee,
            string feeAmount,
            string textConverted,
            string convertedAmount,
            string textEstimatedExchangeRate,
            string estimatedExchangeRateAmount,
            string textYouGet,
            string youGetAmount,
            string textYouGetDetail,
            bool bButtonAccept)
        {
            TextYouSend = textYouSend;
            YouSendAmount = youSendAmount;
            TextFee = textFee;
            FeeAmount = feeAmount;
            TextConverted = textConverted;
            ConvertedAmount = convertedAmount;
            TextEstimatedExchangeRate = textEstimatedExchangeRate;
            EstimatedExchangeRateAmount = estimatedExchangeRateAmount;
            TextYouGet = textYouGet;
            YouGetAmount = youGetAmount;
            TextYouGetDetail = textYouGetDetail;
            IsButtonAccept = bButtonAccept;
        }
    }
}
