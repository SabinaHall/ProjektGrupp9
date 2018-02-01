using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Models
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            for (int i = 1; i < 4; i++)
            {
                PasswordHasher pwdHash = new PasswordHasher();
                string pwd = pwdHash.HashPassword("123");
                var user = new ApplicationUser();


                user.PasswordHash = pwd;
                user.UserName = $"User{i}@live.se";
                user.Email = $"User{i}@live.se";
                user.FirstName = $"User{i}";
                user.LastName = $"User{i}";
                user.Room = $"N204{i}";
                user.PhoneNmbr = $"0738675869";
                user.SecurityStamp = Guid.NewGuid().ToString();
                context.Users.Add(user);
                context.SaveChanges();
            }


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("SuperAdmin"))
            {

                // first we create SuperAdmin rool   
                var role = new IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Admin"))
            {

                // create Admin rool   
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("User"))
            {

                // create User rool   
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }

            for (int i = 1; i < 3; i++)
            {
                PasswordHasher pwdHash = new PasswordHasher();
                string pwd = pwdHash.HashPassword("123");
                var user = new ApplicationUser();


                user.PasswordHash = pwd;
                user.UserName = $"admin{i}@live.se";
                user.Email = $"admin{i}@live.se";
                user.FirstName = $"Admin{i}";
                user.LastName = $"Admin{i}";
                user.Room = $"N204{i}";
                user.PhoneNmbr = $"0738675869";
                user.SecurityStamp = Guid.NewGuid().ToString();
                context.Users.Add(user);
                context.SaveChanges();

                UserManager.AddToRole(user.Id, "Admin");
            }



            if (!context.Users.Any(x => x.UserName == "andreas@live.se"))
            {
                PasswordHasher pwdHash = new PasswordHasher();
                string pwd = pwdHash.HashPassword("123");
                var user = new ApplicationUser();


                user.PasswordHash = pwd;
                user.UserName = "andreas@live.se";
                user.Email = "andreas@live.se";
                user.FirstName = "Andreas";
                user.LastName = "Johansson";
                user.Room = "N2020";
                user.PhoneNmbr = "hemligt";
                user.SecurityStamp = Guid.NewGuid().ToString(); 
                context.Users.Add(user);
                context.SaveChanges();

                UserManager.AddToRole(user.Id, "SuperAdmin");
            }

            if (!context.Users.Any(x => x.UserName == "superadmin@live.se"))
            {
                PasswordHasher pwdHash = new PasswordHasher();
                string pwd = pwdHash.HashPassword("123");
                var user = new ApplicationUser();


                user.PasswordHash = pwd;
                user.UserName = "superadmin@live.se";
                user.Email = "superadmin@live.se";
                user.SecurityStamp = Guid.NewGuid().ToString();
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


            EntryTag et1 = new EntryTag();
            et1.Id = 0;
            et1.TagName = "Möte";

            EntryTag et2 = new EntryTag();
            et2.Id = 0;
            et2.TagName = "Information";

            EntryTag et3 = new EntryTag();
            et3.Id = 0;
            et3.TagName = "Övrigt";

            context.Tags.Add(et1);
            context.Tags.Add(et2);
            context.Tags.Add(et3);

            base.Seed(context);
        }
    }
}
