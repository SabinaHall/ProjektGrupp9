using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public virtual EntryInformal Entry { get; set; }

        public virtual ApplicationUser Writer { get; set; } 
    }
}
