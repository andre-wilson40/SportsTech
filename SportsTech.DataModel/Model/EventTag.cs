using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class EventTag
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("EventParticipantId")]
        public virtual EventParticipant EventParticipant { get; set; }
        public int EventParticipantId { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
        public int TagId { get; set; }

        public int Value { get; set; }

        public DateTime RecordedAt { get; set; }
    }
}
