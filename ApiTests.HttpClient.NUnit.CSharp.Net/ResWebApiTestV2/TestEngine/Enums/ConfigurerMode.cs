namespace ResWebApiTest.TestEngine.Enums
{
    /// <summary>
    /// Configuration mode for Configurer:
    ///     Release     - for release version.
    ///     Debug       - for debug/local IIS server version.
    ///     IISExpress  - for debug in IIS express version. Thin one is set as a default configuration mode.
    /// </summary>
    public enum ConfigurerMode
    {
        Release,
        Debug,
        IISExpress
    };
}

