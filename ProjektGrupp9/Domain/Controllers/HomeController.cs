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

        public ActionResult ProfilePage()
        {

            
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);


           

            return View(user);


        }

            


    }
}