namespace GluwaPro.UITest.TestUtilities.Models.ExchangeViewModels
{
    public class QuoteAcceptViewItem
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
        public string ExchangedAmount { get; private set; }
        public string TextQuoteAcceptedText { get; private set; }

        public QuoteAcceptViewItem(
            string textYouSend,
            string youSendAmount,
            string textFee,
            string feeAmount,
            string textConverted,
            string convertedAmount,
            string textEstimatedExchangeRate,
            string estimatedExchangeRateAmount,
            string textYouGet,
            string exchangedAmount,
            string textQuoteAcceptedText
            )
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
            ExchangedAmount = exchangedAmount;
            TextQuoteAcceptedText = textQuoteAcceptedText;
        }
    }
}
