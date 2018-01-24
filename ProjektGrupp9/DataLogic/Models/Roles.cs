using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string Role { get; set; }

        public virtual ICollection<ApplicationUser> User { get; set; }
    }
}
