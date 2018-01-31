using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;

namespace DataLogic.Models
{
    public class ApplicationUser : IdentityUser
    {

        public virtual ICollection<Entries> Entries { get; set; }
        public virtual ICollection<EntryInformal> InformalEntrys { get; set; } 
        public virtual ICollection<EntryEducation> EducationEntries { get; set; }
        public virtual ICollection<EntryResearch> ResearchEntries { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Room { get; set; }
        public bool Active { get; set; } = true;



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}