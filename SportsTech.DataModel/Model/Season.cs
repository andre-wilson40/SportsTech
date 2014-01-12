using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class Season : IEntity
    {
        public Season()
        {
            CompetitionRegistrations = new HashSet<CompetitionRegistration>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("CompetitionId")]
        public virtual Competition Competition { get; set; }
        public int CompetitionId { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public virtual ICollection<CompetitionRegistration> CompetitionRegistrations { get; set; }
    }
}
