using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class Email
    {
        public int Id { get; set; }

        [Required]
        public string FromName { get; set; }


        public string FromEmail { get; set; }

        [Required]
        public string Message { get; set; }

    }
}
