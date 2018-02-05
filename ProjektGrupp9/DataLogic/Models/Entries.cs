using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLogic.Models
{
    public class Entries
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Rubrik på inlägget")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "För kort eller lång rubrik!")]
        public string Heading { get; set; }

        [Required]
        [Display(Name = "Bloggtext")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "För kort eller lång text!")]
        public string text { get; set; }

        public DateTime Date { get; set; }

        public string Filename { get; set; }

        public string ContentType { get; set; }

        public byte[] File { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Comment> CommentsFormal { get; set; } 

        //public virtual ICollection<EntryTag> EntryTags { get; set; }
    }
}
