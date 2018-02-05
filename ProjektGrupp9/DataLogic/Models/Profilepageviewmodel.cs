using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
   public class Profilepageviewmodel
    {
        public ApplicationUser user { get; set; }
        public List<Events> myMeetings { get; set; }
    }
}
