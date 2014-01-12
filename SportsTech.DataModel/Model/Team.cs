using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class Team : IEntity
    {
        public Team()
        {
           // Seasons = new HashSet<Season>();
            CompetitionRegistrations = new HashSet<CompetitionRegistration>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ClubId")]        
        public virtual Club Club { get; set; }
        public int ClubId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        // public virtual ICollection<Season> Seasons { get; set; }
        public virtual ICollection<CompetitionRegistration> CompetitionRegistrations { get; set; }
    }
}
