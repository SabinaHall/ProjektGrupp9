using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class Entries
    {
        public enum EnumEntryType
        {
            formell = 0,
            informell = 1,
            forskning = 2,
            utbildning = 3
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Rubrik på inlägget")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "För kort eller lång rubrik!")]
        public string Heading { get; set; }

        [Required]
        [Display(Name = "Bloggtext")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "För kort eller lång text!")]
        public string text { get; set; }

        public EnumEntryType EntryType { get; set; }

        public DateTime Date { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
