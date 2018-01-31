using DataLogic.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(Domain.Startup))]
namespace Domain
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)

        {
            ConfigureAuth(app);
            DeletePastDates();
            //createRolesandUsers();
        }

        private void DeletePastDates()
        {
            using (var context = new ApplicationDbContext())
            {
                var lista = new List<Events>();
                lista = context.Events.ToList();
                foreach (var item in lista)
                {
                    if (item.Date < DateTime.Now)
                    {
                        context.Events.Remove(item);
                        

                    }
                }

                context.SaveChanges();
            }
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("SuperAdmin"))
            {

                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);
            }


            if (!context.Users.Any(x => x.UserName == "andreas@live.se"))
            {
                PasswordHasher pwdHash = new PasswordHasher();
                string pwd = pwdHash.HashPassword("123");
                var user = new ApplicationUser();


                user.PasswordHash = pwd;
                user.UserName = "andreas@live.se";
                user.Email = "andreas@live.se";
                user.SecurityStamp = Guid.NewGuid().ToString(); //THIS IS WHAT I NEEDED
                context.Users.Add(user);
                context.SaveChanges();

                UserManager.AddToRole(user.Id, "SuperAdmin");
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }
    }
}
