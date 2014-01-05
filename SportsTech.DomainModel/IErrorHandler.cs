using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain
{
    public enum ErrorTypeEnum
    {
        Warning,
        Error
    }

    public interface IErrorHandler
    {
        /// <summary>
        /// Adds a key mapping to the errorhandler implementation.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyMapping"></param>
        /// <param name="defaultMessage"></param>
        void AddKeyMapping(string key, string keyMapping, string defaultMessage);

        /// <summary>
        /// Gets the default message based on a key from an errorhandler implementation
        /// </summary>
        /// <param name="key"></param>
        string GetDefaultMessage(string key);

        /// <summary>
        /// Gets the mapping key based on a key mapping from an errorhandler implementation
        /// </summary>
        /// <param name="key"></param>
        string GetKeyMapping(string key);

        /// <summary>
        /// Adds the error to the errorhandler implementation.  When called IsValid will become false.
        /// Keys must be unique
        /// </summary>
        /// <param name="key"></param>
        /// <param name="errorMessage"></param>
        void AddError(string key, string errorMessage, ErrorTypeEnum type);

        /// <summary>
        /// Determines an the error associated with the supplied key is already added to the container
        /// </summary>
        /// <param name="key">The key to check already exists</param>
        /// <returns>True if the key exists otherwise false</returns>
        bool ContainsKey(string key);

        /// <summary>
        /// Determines if the error handler is valid.  Typically true if there are no errors present.
        /// </summary>
        bool IsValid { get; }
    }
}
