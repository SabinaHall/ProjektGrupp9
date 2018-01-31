using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class TagEntryViewModel
    {
        public IEnumerable<Entries> Entries { get; set; }

        public List<EntryTag> Tags { get; set; }

    }
}
