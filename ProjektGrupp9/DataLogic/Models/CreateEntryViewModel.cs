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
        [Key]
        public int Id { get; set; }

        //public string Heading { get; set; }

        //public string Text { get; set; }

        //public DateTime Date { get; set; }

        //public string Filename { get; set; }

        //public string ContentType { get; set; }

        //public byte[] File { get; set; }

        public List<EntryTag> TagList { get; set; } 

        public IEnumerable<SelectListItem> TagNameList { get; set; }

        public List<string> SelectedTagIds { get; set; }

        public Entries Entries { get; set; }

        public virtual ApplicationUser Author { get; set; }

    }
}
