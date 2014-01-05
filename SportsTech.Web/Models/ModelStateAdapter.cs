using SportsTech.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Models
{

    /// <summary>
    /// Wrapper class to seperate the ModelStateDictionary from our model objects and provide error reporting facility
    /// </summary>
    internal class ModelStateAdapter : IErrorHandler
    {
        private readonly ModelStateDictionary _modelState;
        private readonly ErrorValueDictionary _keyMappings;

        public ModelStateAdapter(ModelStateDictionary modelState)
        {
            _modelState = modelState;
            _keyMappings = new ErrorValueDictionary();
        }

        #region IModelErrorHandler Members

        public void AddKeyMapping(string key, string keyMapping, string defaultMessage)
        {
            _keyMappings.Add(key, keyMapping, defaultMessage);
        }

        public string GetDefaultMessage(string key)
        {
            return _keyMappings.Get(key).ErrorMessage;
        }

        public string GetKeyMapping(string key)
        {
            return _keyMappings.Get(key).MapKey;
        }

        public void AddError(string key, string errorMessage, ErrorTypeEnum type)
        {
            // Warnings are handled by the navigation link's validation as that is run on the GET of most pages
            // so a warning can be displayed to the user on initial page load.  

            // Also we are not interested in adding multiple errors against a single key, as in the case
            // of the Crop multiple fertiliser logic which uses the same control to add an item over different months.  If we let a key be added then the same error would occur
            // multiple times. andre 2-July

            if (type == ErrorTypeEnum.Error && !ContainsKey(key))
                _modelState.AddModelError(key, errorMessage);
        }

        public bool IsValid
        {
            get
            {
                return _modelState.IsValid;
            }
        }

        public bool ContainsKey(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;

            var index = _modelState.Keys.ToList().IndexOf(key);

            if (index < 0 || index > _modelState.Values.Count) return false;

            return _modelState.Values.ElementAt(index).Errors.Any();
        }

        #endregion


    }

}