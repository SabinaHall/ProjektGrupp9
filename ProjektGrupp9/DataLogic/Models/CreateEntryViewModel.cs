using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLogic.Models
{
    public class CreateEntryViewModel
    {
        [Display(Name = "Välj taggar")]
        public List<EntryTag> TagList { get; set; } 

        public IEnumerable<SelectListItem> TagNameList { get; set; }

        [Required]
        public List<string> SelectedTagIds { get; set; }

        public Entries Entries { get; set; }

        public virtual ApplicationUser Author { get; set; }

    }
}
