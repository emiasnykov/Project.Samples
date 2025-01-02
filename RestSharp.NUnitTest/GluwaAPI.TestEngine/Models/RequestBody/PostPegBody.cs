using GluwaAPI.TestEngine.CurrencyUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostPegsBody
    {
#nullable enable

        [Required]
        public string? Source { get; set; }

        [Required]
        public string? Amount { get; set; }

        [Required]
        public string? Fee { get; set; }

        [Required]
        public string? Currency { get; set; }

        [Required]
        public string? Signature { get; set; }


        [Required]
        public string? Idem { get; set; }

        [Required]
        public string? Nonce { get; set; }
    }
}
