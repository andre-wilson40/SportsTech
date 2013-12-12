using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SportsTech.Web.ViewModels.Event
{
    public class CreateViewModel
    {
        [DisplayName("Against")]
        [Required]
        public string Against { get; set; }

        [DisplayName("At home")]
        public bool IsHomeGame { get; set; }

        [DisplayName("Referee")]
        public string Referee { get; set; }

        [DisplayName("Game day")]
        [Required]
        public DateTime EventDate { get; set; }
    }
}