using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class Club : IEntity
    {
        public Club()
        {
            Members = new HashSet<Member>();
            UserProfiles = new HashSet<UserProfile>();
            Competitions = new HashSet<Competition>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        public virtual ICollection<Member> Members { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }

        public virtual ICollection<Competition> Competitions { get; set; }
    }
}
