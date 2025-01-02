using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public sealed class PostQRCodeBody
    {
        [Required]
        public string Target { get; set; }

        [Required]
        public string Signature { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public string Amount { get; set; }

        public string MerchantOrderID { get; set; }

        public string Expiry { get; set; }

        public string Note { get; set; }

        public PostQRCodeBody(string target,
                          string signature,
                          string currency,
                          string amount)
        {
            Target = target;
            Signature = signature;
            Currency = currency;
            Amount = amount;
            Expiry = "86400";
        }
    }
}

