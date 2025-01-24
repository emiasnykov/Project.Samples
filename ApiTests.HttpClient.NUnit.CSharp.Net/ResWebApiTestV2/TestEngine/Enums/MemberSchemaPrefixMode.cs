using System.Runtime.Serialization;

namespace ResWebApiTest.TestEngine.Enums
{
    /// <summary>
    /// Node schema item template prefix mode:
    ///     Neutral     - Definitive 
    ///     Dictionary  - Non-Definitive
    ///     Iteration   - Non-Definitive
    ///     Array       - Non-Definitive
    /// Note:
    ///     This is parametrized enum using values, just to make sure this is NOT seen as a standard text, to be NOT easily changed in the code.
    /// </summary>
    public enum SchemaItemTemplateMode
    {
        [EnumMember(Value = "N")]
        Neutral,
        [EnumMember(Value = "D")]
        Dictionary,
        [EnumMember(Value = "I")]
        Iteration,
        [EnumMember(Value = "A")]
        Array
    }
}
