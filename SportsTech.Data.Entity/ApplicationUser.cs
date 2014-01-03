using Microsoft.AspNet.Identity.EntityFramework;
using SportsTech.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Entity
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("UserProfileId")]
        public virtual UserProfile UserProfile { get; set; }
        public int UserProfileId { get; set; }
    }
}
