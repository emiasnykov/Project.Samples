using System;
using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public sealed class PostTokensBody
    {
        /// <summary>
        /// Either Mint/Burn
        /// </summary>
        [Required]
        public string Action { get; set; }

        /// <summary>
        /// Amount to be minted/burned
        /// </summary>
        [Required]
        public string Amount { get; set; }

        /// <summary>
        /// Address to send the mint/burn amount
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Signature of the address
        /// </summary>
        public string AddressSignature { get; set; }

        /// <summary>
        /// Idem
        /// </summary>
        [Required]
        public string Idem;

        [Obsolete]
        public PostTokensBody() { }

        /// <summary>
        /// For Mint
        /// </summary>
        /// <param name="action"></param>
        /// <param name="amount"></param>
        /// <param name="address"></param>
        /// <param name="addressSignature"></param>
        /// <param name="idem"></param>
        public PostTokensBody(string action,
                              string amount,
                              string address,
                              string addressSignature,
                              string idem)
        {
            Action = action;
            Amount = amount;
            Address = address;
            AddressSignature = addressSignature;
            Idem = idem;
        }

        /// <summary>
        /// For Burn
        /// </summary>
        /// <param name="action"></param>
        /// <param name="amount"></param>
        /// <param name="idem"></param>
        public PostTokensBody(string action,
                             string amount,
                             string idem)
        {
            Action = action;
            Amount = amount;
            Idem = idem;
        }
    }
}
