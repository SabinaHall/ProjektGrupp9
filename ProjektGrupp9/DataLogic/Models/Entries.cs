using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        //public enum CategoryEnum
        //{
        //    Möte = 0,
        //    Information = 1,
        //    Projekt = 2,
        //    Övrigt = 3
        //}

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

        //public CategoryEnum Category { get; set; }

        public DateTime Date { get; set; }

        public string Filename { get; set; }

        public string ContentType { get; set; }

        public byte[] File { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Category CategoryTest { get; set; }
    }
}
