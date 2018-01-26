using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class EventViewModel
    {
        public Events events { get; set; }
        public List<ApplicationUser> users { get; set; }

    }
}
