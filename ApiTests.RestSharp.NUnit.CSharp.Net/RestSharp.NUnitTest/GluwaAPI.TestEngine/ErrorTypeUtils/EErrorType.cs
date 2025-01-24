
namespace GluwaAPI.TestEngine.EErrorTypeUtils
{
    /// <summary>
    /// ErrorTypes used in the API
    /// </summary>
    public enum EErrorType
    {
        InvalidUrlParameters,
        MissingBody,
        InvalidBody,
        InvalidValue,
        ValidationError,
        Forbidden,
        WebhookNotFound,
        SignatureMissing,
        SignatureExpired,
        InvalidSignature,
        NotFound,
        Conflict,
        Expired,
        BadRequest,
        AmountTooSmall,
        InvalidAmount,
        Required,
        NotEnoughBalance,
        Positive,
        InvalidAddressSignature,
        InvalidAddress,
        Other,
        InsufficientFunds,
        MissingOtp,
        invalid_grant,
        unsupported_grant_type,
        invalid_client,
        invalid_scope
    }
}
