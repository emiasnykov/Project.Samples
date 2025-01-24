using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models
{
    public class PostWalletBody
    {
        public string ChainTypeId { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
