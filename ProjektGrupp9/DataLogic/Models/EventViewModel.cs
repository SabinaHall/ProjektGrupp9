using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLogic.Models
{
    
    public class EventViewModel
    {
        [Key]
        public int id { get; set; }

        public Events events { get; set; }
        public List<ApplicationUser> users { get; set; }
        [Required]
        [Display(Name = "Mötesdeltagare")]
        public string Meetingparticipants { get; set; }
        public IEnumerable<SelectListItem> Participants { get; set; }

    }
}
