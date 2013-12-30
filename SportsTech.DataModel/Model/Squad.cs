using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class Squad
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("CompetitionRegistrationId")]
        public virtual CompetitionRegistration CompetitionRegistration { get; set; }
        public int CompetitionRegistrationId { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
