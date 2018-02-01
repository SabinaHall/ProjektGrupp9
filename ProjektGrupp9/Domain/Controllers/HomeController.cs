using DataLogic;
using DataLogic.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Domain.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("IndexFormal", "Entries");
        }

        public ActionResult DeactivateConfirmed (string id)
        {
            
            var user = db.Users.Find(id);
            user.Active = false;
            db.SaveChanges();
            return RedirectToAction("ProfilePage", new { id = id });
        }

        public ActionResult UserFile(string id)
        {
            var be = db.Users.Single(x => x.Id == id);
            if (be.ProfilePicture != null)
            {
                return File(be.ProfilePicture, be.ContentType);
            }
            return View();
        }

        public ActionResult UserSearch()
        {

            List<ApplicationUser> model = new List<ApplicationUser>();

            model = db.Users.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult UserSearch (string UserSearch)
        {
            
            List<ApplicationUser> model = new List<ApplicationUser>();
            if (!String.IsNullOrEmpty(UserSearch))
            {
                model = db.Users.Where(s => s.UserName.ToLower().Contains(UserSearch.ToLower()) ||
                s.FirstName.ToLower().Contains(UserSearch.ToLower()) || s.LastName.ToLower().Contains(UserSearch.ToLower())).ToList();

            }
            else
            {
                model = db.Users.ToList();
            }
            return View(model);
        }
    

        public ActionResult Deactivate (string id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);



           

        }

        public ActionResult Admin()
        {

            return View();
        }

        public ActionResult List()
        {
            var users = new List<ApplicationUser>();
            users = db.Users.ToList();

            return View(users);
        }

        public ActionResult Edit(string id)
        {
            var model = db.Users.Find(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ApplicationUser model)
        {
            var user = db.Users.Find(model.Id);

            user.Email = model.Email;
            user.UserName = model.Email;
            db.SaveChanges();

            return RedirectToAction("List");

        }
        

        public ActionResult ProfilePage(string id)
        {
            string userId = "";
            if (String.IsNullOrEmpty(id))
            {
                userId = User.Identity.GetUserId();
            }
            else
            {
                userId = id;
            }
            
            var user = db.Users.Find(userId);




            return View(user);


        }

        


        public ActionResult EditProfile(string id)
        {
            var user = db.Users.Find(id);

            return View(user);

        }

        [HttpPost]
        public ActionResult EditProfile(ApplicationUser model)
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.Room = model.Room;
            user.Email = model.Email;
           
            db.SaveChanges();

            return RedirectToAction("ProfilePage");

        }


    }
}