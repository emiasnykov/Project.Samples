namespace AdminTool.UITests.SharedMethods
{
    /// <summary>
    /// Login user enumeration
    /// </summary>
    public enum LoginUserEnum
    {
        UnknownUser,
        ValidUser,
        InvalidUser,
        NoEmail,
        NoPassword
    };

    /// <summary>
    /// Api Resource enumeration
    /// </summary>
    public enum ApiResourceEnum
    {
        ValidResource,
        NoName,
        NoDisplayName
    };

    /// <summary>
    /// Identity Resource enumeration
    /// </summary>
    public enum IdentityResourceEnum
    {
        ValidResource,
        NoName,
        NoDisplayName
    };
}
