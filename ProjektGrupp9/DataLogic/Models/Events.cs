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

        [Display(Name = "Datum")]
        [DataType(DataType.Date, ErrorMessage = "Du måste välja ett datum!")] 
        public DateTime ? Date { get; set; }

        [Display(Name = "Tid")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Du måste skriva in tid för mötet!")]
        public string Time { get; set; }

        [Display(Name = "Tidsförslag 1")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Du måste skriva in tid för tidsförslag 1!")]
        public string secondaryTime { get; set; }

        [Display(Name = "Tidsförslag 2")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Du måste skriva in tid för tidsförslag 2!")]
        public string tertiaryTime { get; set; }

        [Display(Name = "Plats")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Du måste skriva in plats för mötet!")]
        public string Place { get; set; }

        [StringLength(20, ErrorMessage = "Beskrivningen får inte vara mer än 20 tecken.",  MinimumLength = 3)]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        

        public virtual ApplicationUser Host { get; set; }
      
    }
}
