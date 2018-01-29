using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
  public class EventParticipants
    {

        [Key]
        public int id { get; set; }
        public int EventID { get; set; }
        public string UserID { get; set; }

    }
}
