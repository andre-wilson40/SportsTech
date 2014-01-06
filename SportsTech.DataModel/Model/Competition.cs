using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class Competition : IEntity
    {
        public Competition()
        {
            Seasons = new HashSet<Season>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("ClubId")]
        public Club Club { get; set; }
        public int ClubId { get; set; }

        public virtual ICollection<Season> Seasons { get; set; }
    }
}
