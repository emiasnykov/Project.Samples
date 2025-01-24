using GluwaAPI.TestEngine.CurrencyUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GluwaAPI.TestEngine.Models.RequestBody
{
    public class PostUnpegsBody
    {
#nullable enable

        [Required]
        public string? AllowanceSignature { get; set; }

        [Required]
        public string? Source { get; set; }

        [Required]
        public string? Amount { get; set; }

        [Required]
        public string? Fee { get; set; }

        [Required]
        public string? Currency { get; set; }

        /// <summary>
        /// Idempotent key that must be unique for every request.
        /// </summary>
        [Required]
        public string? Idem { get; set; }
    }
}
