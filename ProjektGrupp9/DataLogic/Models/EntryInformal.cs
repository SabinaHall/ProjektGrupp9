using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class EntryInformal
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Rubrik på inlägget")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "För kort eller lång rubrik!")]
        public string Heading { get; set; }

        [Required]
        [Display(Name = "Text till inlägget")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "För kort eller långt inlägg!")]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string Filename { get; set; }

        public string ContentType { get; set; }

        public byte[] File { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
