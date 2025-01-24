namespace Dashboard.UITests.Enum
{
    /// <summary>
    /// Login user enum types
    /// </summary>
    public enum LoginUserEnum
    {
        UnknownUser,
        ValidUser,
        InvalidUser,
        InvalidEmail,
        ValidUserETH
    };

    /// <summary>
    /// New user enum options
    /// </summary>
    public enum SignUpEnum
    {
        ValidCredentials,
        InvalidUserName,
        InvalidEmail,
        NoPassword,
        PasswordTooShort
    };

    /// <summary>
    /// Browser enum types
    /// </summary>
    public enum BrowsersEnum
    {
        Chrome,
        Firefox
    };

    /// <summary>
    /// Address form inputs
    /// </summary>
    public enum AddressFormEnum
    {
        ValidValues,
        ValidValuesAltAdr,
        ValidAddressNoPrefix,
        InvalidSign,
        InvalidAddress,
        InvalidMsg
    };

    /// <summary>
    /// Webhook url enum options
    /// </summary>
    public enum RegWebhookUrlEnum
    {
        ValidUrl,
        InvalidUrl,
        EmptyUrl
    };

    /// <summary>
    /// Faucet form inputs
    /// </summary>
    public enum FaucetsFormEnum
    {
        ValidAddress,
        InvalidAddress,
        EmptyAddress
    };

    /// <summary>
    /// Transaction form inputs
    /// </summary>
    public enum TransactionsFormEnum
    {
        ValidValues,
        InvalidAddress,
        EmptyAddress
    };
}
