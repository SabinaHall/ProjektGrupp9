﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<ResearchTag> ResearchTag { get; set; }
        public DbSet<EducationTag> EducationTag { get; set; }
        public DbSet<Entries> Entries { get; set; }
        public DbSet <EntryInformal> InformalEntries { get; set; } 
        public DbSet<EntryTag> EntryTags { get; set; }
        public DbSet<EntryResearch> EntryResearch { get; set; }
        public DbSet<EntryEducation> EntryEducation { get; set; }
        public DbSet<MeetingInvites> MeetingInvites { get; set; }
        public DbSet<EventParticipants> EventParticipants { get; set; }
        public DbSet<EntryTagEntries> EntryTagEntries { get; set; }
        public DbSet<ResearchTagEntries> ResearchTagEntries { get; set; }
        public DbSet<EducationTagEntries> EducationTagEntries { get; set; }
        public DbSet<Comment> Comments { get; set; } 
        public DbSet<Likes> Likes { get; set; }

        public System.Data.Entity.DbSet<DataLogic.Models.Events> Events { get; set; }

        public System.Data.Entity.DbSet<DataLogic.Models.Email> Emails { get; set; }

        public System.Data.Entity.DbSet<DataLogic.Models.EventViewModel> EventViewModels { get; set; }
    }
}
