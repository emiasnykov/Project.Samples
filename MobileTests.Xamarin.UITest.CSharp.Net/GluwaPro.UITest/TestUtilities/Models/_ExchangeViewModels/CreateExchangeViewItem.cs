namespace GluwaPro.UITest.TestUtilities.Models.ExchangeViewModels
{
    /// <summary>
    /// Use this class to get info from Exchange view
    /// </summary>
    public class CreateExchangeViewItem
    {
        public string ExchangeSendCurrency { get; private set; }
        public string ExchangeReceiveCurrency { get; private set; }
        public string ExchangeAmount { get; private set; }
        public string Balance { get; private set; }
        public string Currency { get; private set; }
        public bool IsButtonNext { get; private set; }

        public CreateExchangeViewItem(
            string exchangeSendCurrency,
            string exchangeReceiveCurrency,
            string exchangeAmount,
            string balance,
            string currency,
            bool bButtonNext)
        {
            ExchangeSendCurrency = exchangeSendCurrency;
            ExchangeReceiveCurrency = exchangeReceiveCurrency;
            ExchangeAmount = exchangeAmount;
            Balance = balance;
            Currency = currency;
            IsButtonNext = bButtonNext;
        }
    }
}
