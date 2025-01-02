using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models.ResponseBody
{
#nullable enable
    public sealed class GetFeeResponse
    {
        [Required]
        public string? Currency { get; set; }

        [Required]
        public string? MinimumFee { get; set; }


        public GetFeeResponse(string currency, string minimumFee)
        {
            Currency = currency;
            MinimumFee = minimumFee;
        }
    }
}
