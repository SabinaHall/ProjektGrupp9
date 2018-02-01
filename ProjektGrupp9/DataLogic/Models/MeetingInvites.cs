using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
   public class MeetingInvites 
    {
        public int id { get; set; }
        public int EventID { get; set;}
        public string Sender { get; set; }
        public string Receiver { get; set; }

    }
}
