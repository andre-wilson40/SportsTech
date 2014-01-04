using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class UserProfile
    {
        public UserProfile()
        {
            Clubs = new HashSet<Club>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string EmailAddress { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string TimeZone { get; set; }

        [Required, MaxLength(20)]
        public string DateFormat { get; set; }

        public virtual ICollection<Club> Clubs { get; set; }

        [NotMapped]
        public string DateTimeFormat
        {
            get { return DateFormat + " HH:mm:ss"; }
        }

        [NotMapped]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
