using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public enum TagTypeEnum
    {
        System,
        UserDefined
    }

    public class TagType
    {
        public TagType()
        {
            Tags = new HashSet<Tag>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public TagTypeEnum Type { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
