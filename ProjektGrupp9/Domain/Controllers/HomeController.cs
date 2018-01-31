using DataLogic.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult ProfilePage()
        {

            var model = new ApplicationUser();
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);


            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Room = user.Room;
            model.PhoneNumber = user.PhoneNumber;

            return View(model);


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