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
        [DataType(DataType.Date)]
        public DateTime ? Date { get; set; }

        [Display(Name = "Tid")]
        public string Time { get; set; }

        [Display(Name = "Tidsförslag 1")]
        public string secondaryTime { get; set; }

        [Display(Name = "Tidsförslag 2")]
        public string tertiaryTime { get; set; }

        [Display(Name = "Plats")]
        public string Place { get; set; }

        [StringLength(20, ErrorMessage = "Beskrivningen får inte vara mer än 20 tecken.",  MinimumLength = 3)]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        

        public virtual ApplicationUser Host { get; set; }
      
    }
}
