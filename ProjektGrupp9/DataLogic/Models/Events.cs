using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
   public class Events
    {
     
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Datum")]
        [DataType(DataType.Date, ErrorMessage = "Du måste välja ett datum!")] 
        public DateTime ? Date { get; set; }

        [Required]
        [Display(Name = "Tid")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Du måste skriva in tid för mötet!")]
        public string Time { get; set; }

        [Required]
        [Display(Name = "Tidsförslag 1")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Du måste skriva in tid för tidsförslag 1!")]
        public string secondaryTime { get; set; }

        [Required]
        [Display(Name = "Tidsförslag 2")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Du måste skriva in tid för tidsförslag 2!")]
        public string tertiaryTime { get; set; }

        [Required]
        [Display(Name = "Plats")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Du måste skriva in plats för mötet!")]
        public string Place { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Beskrivningen får inte vara mer än 20 tecken.",  MinimumLength = 3)]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        

        public virtual ApplicationUser Host { get; set; }
      
    }
}
