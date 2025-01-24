using GluwaAPI.TestEngine.EErrorTypeUtils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Supporting type class for response body in create order
/// </summary>
namespace GluwaAPI.TestEngine.Models
{
    public class InnerError
    {
        public EErrorType Code { get; private set; }
        public string Path { get; private set; }
        public string Message { get; private set; }
        public InnerError(EErrorType code, string path, string message)
        {
            Code = code;
            Path = path;
            Message = message;
        }
    }

    public sealed class ValidationErrorItem
    {
        /// <summary>
        /// Error Code
        /// </summary>
        [Required]
        public EErrorType Code { get; private set; }

        /// <summary>
        /// Error Message
        /// </summary>
        [Required]
        public string Message { get; private set; }

        public List<InnerError> InnerErrors { get; private set; }

        public ValidationErrorItem(EErrorType code, string message, List<InnerError> innerErrors)
        {
            Code = code;
            Message = message;
            InnerErrors = innerErrors;
        }
    }
}
