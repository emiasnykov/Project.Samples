using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ResWebApiTest.TestEngine.Extensions
{
    /// <summary>
    /// Object converter to any type
    /// Note: 
    ///     This class is an extention class of converting object and is therefore static.
    /// </summary>
    public static class ConvertObjectExt
    {
        #region Public methods
        /// **************************************

        /// <summary>
        /// Object to dictionary (Tkey,TVal) converter.
        /// Enhanced version of method with the validation of assignement type and fully creation a dictionary
        /// </summary>
        /// <typeparam name="T">TKey type</typeparam>
        /// <typeparam name="V">TVal type</typeparam>
        /// <param name="_ObjToConvert">Object to be converted to IDictionary</param>
        /// <returns>Converted dictionary</returns>
        public static IDictionary<T, V> ToDictionary<T, V>(object _ObjToConvert)
        {
            var dicCurrent = new Dictionary<T, V>();

            // Check the type of assign
            if (typeof(IDictionary).IsAssignableFrom(_ObjToConvert.GetType()))
            {
                // Do the loop for any assigned objects entries
                foreach (DictionaryEntry dicData in _ObjToConvert as IDictionary)
                {
                    try
                    {
                        // Add to the result
                        dicCurrent.Add((T)dicData.Key, (V)dicData.Value);
                    }
                    catch
                    {
                        //Simply ignore adding another nested dictionary
                        continue;
                    }
                }
            }
            else
            {
                // Convert my object to dictionary if it is not a dictionary
                dicCurrent = (Dictionary<T, V>)ToDictionary<V>(_ObjToConvert);
            }

            return dicCurrent;
        }

        /// <summary>
        /// Object to dictionary (string, object) converter.
        /// </summary>
        /// <param name="_Source">Object source</param>
        /// <returns>Converted dictionary</returns>
        public static IDictionary<string, object> ToDictionarySimple(object _Source) => _Source.ToDictionary<object>();

        /// <summary>
        /// Object to list (TVal) converter.
        /// Enhanced version by adding empty object to skip ThrowExceptionOnFailure in WebApiTest engine
        /// </summary>
        /// <typeparam name="T">TVal type</typeparam>
        /// <param name="_ObjToConvert">Object to be converted to IList</param>
        /// <param name="_AddAnyObject">Add empty object on true. It is needed for ThrowExceptionOnFailure</param>
        /// <returns>Converted list</returns>
        public static IList<T> ToList<T>(object _ObjToConvert, bool _AddAnyObject = false)
        {
            bool bProperLst = true;
            var lstCurrent = new List<T>();

            // Serve object to converted
            if (!(_ObjToConvert == null))
            {
                try
                {
                    // Enumerable converted object
                    lstCurrent = ((IEnumerable<T>)_ObjToConvert).ToList();
                }
                catch (InvalidCastException)
                {
                    // Skip all non-enumerable objects 
                    bProperLst = false;

                    // Add empty object, so it's to be skipped in ThrowExceptionOnFailure
                    if (_AddAnyObject)
                        lstCurrent.Add((T)new object());
                }

                // If this is a proper list return the value
                if (bProperLst)
                    return lstCurrent;
            }

            return lstCurrent;
        }

        #endregion Public methods

        #region Private methods
        /// **************************************

        // Convert to dictionary<T>
        private static IDictionary<string, T> ToDictionary<T>(this object _Source)
        {
            // If the source is null throw exception
            if (_Source == null)
                // TODO refactor throw, also create param and antity
                throw new ArgumentNullException("Source", "Unable to convert object to a dictionary. The source object is null.");

            var dictionary = new Dictionary<string, T>();

            // Loop for the properties and add the proper ones
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(_Source))
                AddPropertyToDictionary<T>(property, _Source, dictionary);
            
            return dictionary;
        }

        // Add property do dictionary<T>
        private static void AddPropertyToDictionary<T>(PropertyDescriptor _Property, object _Source, Dictionary<string, T> _Dictionary)
        {
            object value = _Property.GetValue(_Source);

            // Check the type and add to dictionary when pass
            if (IsOfType<T>(value))
                _Dictionary.Add(_Property.Name, (T)value);
        }

        // Check object type
        private static bool IsOfType<T>(object value) => value is T;        

        #endregion Private methods
    }
}
