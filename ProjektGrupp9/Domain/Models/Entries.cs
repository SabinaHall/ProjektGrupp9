using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Models
{
    public class Entries
    {
        public enum EnumEntries
        {
            formell = 0,
            informell = 1,
            utbildning = 2,
            forskning = 3
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Rubrik på inlägg")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "För kort eller lång rubrik!")]
        public string Heading { get; set; }

        [Required]
        [Display(Name = "Blogtext")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "För kort eller lång text!")]
        public string Text { get; set; }

        public EnumEntries Type { get; set; } 

        public DateTime Date { get; set; }

        public virtual ApplicationUser Author { get; set; }


    }
}