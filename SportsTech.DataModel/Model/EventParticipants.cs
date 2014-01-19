using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class EventParticipant
    {
        public EventParticipant()
        {
            Teamsheet = new HashSet<Teamsheet>();
            EventTags = new HashSet<EventTag>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual ICollection<Teamsheet> Teamsheet { get; set; }
        public virtual ICollection<EventTag> EventTags { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
        public int TeamId { get; set; }

        [ForeignKey("OppositionId")]
        public virtual Opposition Opposition { get; set; }
        public int OppositionId { get; set; }

        public bool IsHomeGame { get; set; }

        public int ScoreFor { get; set; }

        public int ScoreAgainst { get; set; }
    }
}
