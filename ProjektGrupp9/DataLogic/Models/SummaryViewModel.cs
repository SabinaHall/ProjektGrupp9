using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class SummaryViewModel
    {
        public List<Entries> FormalEntries { get; set; }
        public List<Events> Events { get; set; }
        public Dictionary<MeetingInvites, Events> YourEvents  { get; set; }
    }
}
