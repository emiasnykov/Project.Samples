
namespace GluwaAPI.TestEngine.Models
{
    public sealed class ContractAddressItem
    {
        public string Contract { get; set; }
        public string Environment { get; set; }
        public string Address { get; set; }
        public int Decimals { get; set; }

        public ContractAddressItem(string contract,
                                   string environment,
                                   string address,
                                   int decimals)
        {
            Contract = contract;
            Environment = environment;
            Address = address;
            Decimals = decimals;
        }
    }
}
