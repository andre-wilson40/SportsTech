using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class Event : IEntity
    {
        public Event()
        {
            Participants = new HashSet<EventParticipant>();
            Stoppages = new HashSet<Stoppage>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey("SeasonId")]
        public virtual Season Season { get; set; }
        public int SeasonId { get; set; }

        [ForeignKey("SeasonRoundId")]
        public virtual SeasonRound Round { get; set; }
        public int SeasonRoundId { get; set; }

        public virtual ICollection<EventParticipant> Participants { get; set; }
        public virtual ICollection<Stoppage> Stoppages { get; set; }

        public DateTime EventDate { get; set; }
    }
}
