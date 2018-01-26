using Microsoft.AspNet.Identity.EntityFramework;
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

        public DbSet<Entries> Entries { get; set; }
        public DbSet <EntryInformal> InformalEntries { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<EntryResearch> EntryResearch { get; set; }
        public DbSet<EntryEducation> EntryEducation { get; set; }

        public System.Data.Entity.DbSet<DataLogic.Models.Events> Events { get; set; }
    }
}
