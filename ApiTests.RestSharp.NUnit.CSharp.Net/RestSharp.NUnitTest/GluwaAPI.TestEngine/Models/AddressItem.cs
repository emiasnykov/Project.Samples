using GluwaAPI.TestEngine.CurrencyUtils;
using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models
{
    /// <summary>
    /// Address Item uses to hold the test address used in the Exchange
    /// </summary>
    public class AddressItem
    {
        /// <summary>
        /// Test Address
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Private Key
        /// </summary>
        [Required]
        public string PrivateKey { get; set; }

        /// <summary>
        /// For Btc
        /// </summary>
        [Optional]
        public string PublicKey { get; set; }

        public AddressItem() { }

    }
}
