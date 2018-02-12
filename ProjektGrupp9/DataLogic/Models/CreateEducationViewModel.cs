﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLogic.Models
{
    public class CreateEducationViewModel
    {
        [Display(Name = "Välj taggar")]
        public List<EntryTag> TagList { get; set; }

        public IEnumerable<SelectListItem> TagNameList { get; set; }
        public IEnumerable<SelectListItem> ExistingTagNameList { get; set; }

        public List<string> SelectedTagIds { get; set; }
        public List<string> SelectedTags { get; set; }
        public EntryEducation Entries { get; set; }

        public virtual ApplicationUser Author { get; set; }

    }
}
