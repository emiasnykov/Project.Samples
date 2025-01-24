
namespace ResWebApiTest.TestEngine.Constants
{
    /// <summary>
    /// Basic string entity constants 
    /// Note: 
    ///     This class is a part of the whole engine string entities and it is therefore static. 
    /// </summary>
    public static class BasicEntity
    {
        // Basic strings
        internal const string NewLine = "\r\n";

        internal const string Slash = "//";
        internal const string BackSlash = "\\";

        internal const string Dot = ".";

        // Indentation strings
        internal const string IndentEmpty = "";
        internal const string IndentLogShort = "    ";        
        internal const string IndentLogLong = "                                        ";
        internal const string IndentTestRegion = "            ";

        // Logger strings
        internal const string LoggerTrail = NewLine + 
                                            NewLine + 
                                            "**************************************************************************************************************************************************************************" + 
                                            NewLine;

        // HTML tag strings
        internal const string HTMLDoctypeTag = "<!DOCTYPE html>";
        internal const string HTMLHtmlTagBeg = "<html>";
        internal const string HTMLHtmlTagEnd = "</html>";
        internal const string HTMLTitleTagBeg = "<title>";
        internal const string HTMLTitleTagEnd = "</title>";
    }
}
