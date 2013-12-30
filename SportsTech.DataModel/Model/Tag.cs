using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class Tag
    {
        public Tag()
        {
            EventTags = new HashSet<EventTag>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        [ForeignKey("ClubId")]
        public virtual Club Club { get; set; }
        public int? ClubId { get; set; }

        [ForeignKey("TagTypeId")]
        public virtual TagType TagType { get; set; }
        public int TagTypeId { get; set; }

        public virtual ICollection<EventTag> EventTags { get; set; }

        [ForeignKey("ParentId")]
        public virtual Tag Parent { get; set; }
        public int? ParentId { get; set; }
    }
}
