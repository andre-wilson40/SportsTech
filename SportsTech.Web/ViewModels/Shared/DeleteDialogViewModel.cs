using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.ViewModels.Shared
{
    public class DeleteDialogViewModel
    {
        public DeleteDialogViewModel()
        {
            DialogId = "dlgDelete";
            HiddenDeleteIdFieldId = "hdnDeleteItemId";
            DeleteIdName = "Id";
        }

        public string DialogId { get; set; }
        public string HiddenDeleteIdFieldId { get; set; }
        public string ControllerName { get; set; }
        public string DeleteIdName { get; set; }
        public string DeleteIdValue { get; set; }
        public string Area { get; set; }
    }
}