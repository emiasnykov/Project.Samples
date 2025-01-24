using GluwaAPI.TestEngine.EErrorTypeUtils;
using System.ComponentModel.DataAnnotations;

namespace GluwaAPI.TestEngine.Models
{
    public class AuthErrorItem
    {
        [Required]
        public EErrorType Error { get; private set; }

        public string Description { get; private set; }

        public AuthErrorItem(EErrorType error, string description)
        {
            Error = error;
            Description = description;
        }
    }
}
