using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class Stoppage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StoppedAfterSeconds { get; set; }

        public int StoppedForSeconds { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }
        public int EventId { get; set; }
    }
}
