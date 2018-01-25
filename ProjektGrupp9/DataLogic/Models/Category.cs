using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Namn { get; set; }

        public virtual ICollection<Entries> Entries { get; set; } 
    } 
}
