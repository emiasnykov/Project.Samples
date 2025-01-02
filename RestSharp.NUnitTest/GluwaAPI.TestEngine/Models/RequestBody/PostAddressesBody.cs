using GluwaAPI.TestEngine.CurrencyUtils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public sealed class PostAddressesBody
    {
        /// <summary>
        /// User's Ethereum address.
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Currencies to register. Only NGN-G and sNGN-G are supported.
        /// </summary>
        [Required]
        public List<string> Currencies { get; set; }

        /// <summary>
        /// Message used to create signature
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Signature that was created using an address value and a message.
        /// </summary>
        [Required]
        public string Signature { get; set; }

        public PostAddressesBody(
            string address,
            List<ECurrency> currencies,
            string message,
            string signature)
        {
            Address = address;
            Currencies = currencies.Select(c => c.ToString()).ToList();
            Message = message;
            Signature = signature;
        }

    }
}
