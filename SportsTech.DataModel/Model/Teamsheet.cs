using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class Teamsheet
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("EventParticipantId")]
        public virtual EventParticipant EventParticipant { get; set; }
        public int EventParticipantId { get; set; }

        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
        public int PlayerId { get; set; }

        [ForeignKey("PositionId")]
        public virtual Position Position { get; set; }
        public int PositionId { get; set; }
    }
}
