using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ResWebApiTest.TestEngine.Extensions
{
    /// <summary>
    /// Enum extension to read attributes
    /// Note: 
    ///     This class is an extention class of enum and is therefore static.
    /// </summary>
    public static class EnumExt
    {
        #region Public methods
        /// **************************************
        
        /// <summary>
        /// Get enum proper property by T (Template)
        /// </summary>
        /// <typeparam name="T">Enum template</typeparam>
        /// <param name="_Value">Enum param</param>
        /// <returns>The enum value of an attribute</returns>
        public static T ToEnumByAttributes<T>(this string _Value) where T : Enum
        {
            // Get enum type by T
            var enumType = typeof(T);

            // Loop through all the enums
            foreach (var name in Enum.GetNames(enumType))
            {
                // Get field name
                var field = enumType.GetField(name);

                // If filed is empty simply comtinue
                if (field == null) continue;

                // Get enum member attributes of the field
                var enumMemberAttribute = GetEnumMemberAttribute(field);

                // If the enum member attribute is correct - parse it as T (template) and return
                if (enumMemberAttribute != null && enumMemberAttribute.Value == _Value)
                    return (T)Enum.Parse(enumType, name);

                // Get enum description of the attribute of the field
                var descriptionAttribute = GetDescriptionAttribute(field);

                // If the enum description attribute is correct - parse it as T (template) and return
                if (descriptionAttribute != null && descriptionAttribute.Description == _Value)
                    return (T)Enum.Parse(enumType, name);

                // This is the last check - the value - parse it as T (template) and return
                if (name == _Value)
                    return (T)Enum.Parse(enumType, name);
            }

            // If anything happens with enum - rise the exception
            throw new ArgumentOutOfRangeException(nameof(_Value), _Value, $"The enum value could not be mapped to a type: {enumType.FullName}");
        }

        /// <summary>
        /// Get attribute of enum as string
        /// </summary>
        /// <param name="_Value">Enum value</param>
        /// <returns>The enum value of an attribute</returns>
        public static string Prefix(this Enum _Value)
        {
            // Get enum field value as string
            var field = _Value
                .GetType()
                .GetField(_Value.ToString());

            // If it is empty  - simple is that
            if (field == null) return string.Empty;

            // Get enum member attributes of the field
            var enumMemberAttribute = GetEnumMemberAttribute(field);

            // If the enum member attribute is correct - parse it as and return
            if (enumMemberAttribute != null)
                return enumMemberAttribute.Value ?? string.Empty;

            // Get enum description of the attribute of the field
            var descriptionAttribute = GetDescriptionAttribute(field);

            // If the enum description attribute is correct - parse it and return
            if (descriptionAttribute != null)
                return descriptionAttribute.Description;

            // Whatever left - return
            return _Value.ToString();
        }

        #endregion Public methods

        #region Private methods
        /// **************************************

        // Get enum description
        private static DescriptionAttribute GetDescriptionAttribute(FieldInfo _Field)
        {
            return _Field
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .OfType<DescriptionAttribute>()
                .SingleOrDefault();
        }

        // Get enum member
        private static EnumMemberAttribute GetEnumMemberAttribute(FieldInfo _Field)
        {
            return _Field
                .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                .OfType<EnumMemberAttribute>()
                .SingleOrDefault();
        }

        #endregion Private methods
    }
}
