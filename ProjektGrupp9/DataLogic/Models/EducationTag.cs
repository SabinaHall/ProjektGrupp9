using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class EducationTag
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Taggnamn")]
        public string TagName { get; set; }

        //public List<EntryTag> DefaultTagList { get; set; } 

        //public virtual ICollection<Entries> Entries { get; set; }

    }
}
