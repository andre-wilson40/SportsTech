using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain
{
    public class ErrorKeyValue
    {
        public string SchemaKey { get; private set; }
        public string MapKey { get; private set; }
        public string ErrorMessage { get; private set; }

        private static ErrorKeyValue _nullKey;

        static ErrorKeyValue()
        {
            _nullKey = new ErrorKeyValue("", "", "");
        }

        public ErrorKeyValue(string key, string keyMapping)
            : this(key, keyMapping, "")
        {
        }

        public ErrorKeyValue(string key, string keyMapping, string defaultMessage)
        {
            SchemaKey = key;
            MapKey = keyMapping;
            ErrorMessage = defaultMessage;
        }

        public static ErrorKeyValue Null()
        {
            return _nullKey;
        }
    }

    /// <summary>
    /// Provides key value mappings of errors supplied by the schema on validation
    /// </summary>
    public sealed class ErrorValueDictionary
    {
        private Dictionary<string, ErrorKeyValue> _dictionary = new Dictionary<string, ErrorKeyValue>();

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ErrorValueDictionary() { }

        /// <summary>
        /// Adds a new ErrorKeyValue based on the supplied parameters
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyMapping"></param>
        public ErrorValueDictionary(string key, string keyMapping)
            : this(key, keyMapping, "")
        {
        }

        /// <summary>
        /// Adds a new ErrorKeyValue based on the supplied parameters 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyMapping"></param>
        /// <param name="defaultMessage"></param>
        public ErrorValueDictionary(string key, string keyMapping, string defaultMessage)
        {
            Add(key, keyMapping, defaultMessage);
        }

        #endregion

        /// <summary>
        /// Adds a new ErrorKeyValue entry with the supplied parameters
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyMapping"></param>
        /// <returns></returns>
        public ErrorValueDictionary Add(string key, string keyMapping)
        {
            return this.Add(key, keyMapping, "");
        }

        /// <summary>
        /// Adds a new ErrorKeyValu with the supplied parameters.  If this error is thrown during validation
        /// the defaultMessage will be used if it is not null
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyMapping"></param>
        /// <param name="defaultMessage"></param>
        /// <returns></returns>
        public ErrorValueDictionary Add(string key, string keyMapping, string defaultMessage)
        {
            return Add(new ErrorKeyValue(key, keyMapping, defaultMessage));
        }

        /// <summary>        
        /// Adds supplied object to our internal lookup        
        /// </summary>
        /// <remarks>
        /// throws null reference exception if the Key is null or an empty string
        /// </remarks>
        /// <param name="key"></param>
        /// <exception cref="NullReferenceException" />
        /// <returns></returns>        
        public ErrorValueDictionary Add(ErrorKeyValue key)
        {
            if (string.IsNullOrEmpty(key.SchemaKey))
                throw new NullReferenceException("Key supplied must be not null or empty");

            _dictionary.Add(key.SchemaKey, key);

            return this;
        }

        /// <summary>
        /// Retails a valid ErrorKeyValue based on the supplied key.  If no value is found then an empty object is returned with
        /// empty string values for each object property.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ErrorKeyValue Get(string key)
        {
            if (_dictionary.ContainsKey(key))
                return _dictionary[key];
            else
                return ErrorKeyValue.Null();
        }
    }

}
