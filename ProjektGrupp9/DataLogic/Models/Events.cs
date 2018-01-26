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
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Place { get; set; }
        [StringLength(20, ErrorMessage = "Beskrivningen får inte vara mer än 20 tecken.",  MinimumLength = 3)]
        public string Description { get; set; }
      
    }
}
