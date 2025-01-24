
namespace GluwaAPI.TestEngine.Models
{
    public sealed class CurrencyItem
    {
        public string contractAddress { get; set; }
        public int chainId { get; set; }
        public string numberOfDigits { get; set; } 
        public string symbol { get; set; }   
        public string nodeUrl { get; set; }
        public string ftaAbi { get; set; }
        public string currencyAbi { get; set; }
    }
}
