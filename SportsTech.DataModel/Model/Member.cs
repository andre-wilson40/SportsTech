using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class Member
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " "  + LastName;
            }
        }

        [ForeignKey("ClubId")]
        public Club Club { get; set;}
        public int ClubId { get; set;}

        [ForeignKey("MembershipId")]
        public Membership Membership { get; set; }
        public int MembershipId { get; set; }
    }
}
