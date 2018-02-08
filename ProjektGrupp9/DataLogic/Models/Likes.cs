using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class Likes
    {
        public int Id { get; set; }
        public virtual ApplicationUser user { get; set; }
        public virtual EntryInformal InformalEntry { get; set; }
    }
}
