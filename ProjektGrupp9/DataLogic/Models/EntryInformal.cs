using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class EntryInformal
    {
        public int Id { get; set; }

        public string Heading { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string Filename { get; set; }

        public string ContentType { get; set; }

        public byte[] File { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
