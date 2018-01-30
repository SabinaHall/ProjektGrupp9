using DataLogic.Models;
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
    }
}