namespace ResWebApiTest.TestEngine.QA_Enums
{
    /// <summary>
    /// Serve exceptions mode for QA tools
    /// </summary>
    public enum QA_ServeExceptionMode
    {
        OnStatusCode,
        OnAuthorization, 
        OnAttachedJSONParse, 
        OnUnknownSchemaJSONParsingTemplate,
        OnNullSendRequest, 
        OnNullGetRequest,
        AnyOtherUnknown
    }
}
