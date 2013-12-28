using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class Membership
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime ValidFrom { get; set; }

        public int ValidForDays { get; set; }

        public bool IsValid()
        {
            return DateTime.Now.Subtract(ValidFrom).TotalDays <= ValidForDays;
        }
    }
}
