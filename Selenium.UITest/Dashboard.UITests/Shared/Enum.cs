using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.UITests.Enum
{
    /// <summary>
    /// Login user enumeration
    /// </summary>
    public enum LoginUserEnum
    {
        UnknownUser,
        ValidUser,
        InvalidUser,
        InvalidEmail,
        ValidUserETH
    }
    /// <summary>
    /// SignUp new user enumeration
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
    /// Browser enumeration
    /// </summary>
    public enum BrowsersEnum
    {
        Chrome,
        Firefox
    };

    /// <summary>
    /// Fill in address form enumeration
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
    /// Webhook url enumeration
    /// </summary>
    public enum RegWebhookUrlEnum
    {
        ValidUrl,
        InvalidUrl,
        EmptyUrl
    };

    /// <summary>
    /// Fill in FaucetsForm enumeration
    /// </summary>
    public enum FaucetsFormEnum
    {
        ValidAddress,
        InvalidAddress,
        EmptyAddress
    };
    public enum TransactionsFormEnum
    {
        ValidValues,
        InvalidAddress,
        EmptyAddress
    };
}
