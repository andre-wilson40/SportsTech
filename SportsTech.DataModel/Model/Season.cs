﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Model
{
    public class Season
    {
        public Season()
        {
            Teams = new HashSet<Team>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("ClubId")]
        public virtual Club Club { get; set; }
        public int ClubId { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
