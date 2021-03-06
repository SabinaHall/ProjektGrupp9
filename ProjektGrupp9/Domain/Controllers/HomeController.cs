﻿
using DataLogic.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult ActivateConfirmed(string id)
        {

            var user = db.Users.Find(id);
            user.Active = true;
            db.SaveChanges();
            return RedirectToAction("ProfilePage", new { id = id });
        }

        public ActionResult UserFile(string id)
        {
            var be = db.Users.Find(id);
            if (be.ProfilePicture != null)
            {
                return File(be.ProfilePicture, be.ContentType);
            }
            else
            {
                byte[] imageData = null;
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/994628-200.png");
                imageData = System.IO.File.ReadAllBytes(path);
                return File(imageData, "images/png");
            }
            
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

        public ActionResult Activate(string id)
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

            Profilepageviewmodel model = new Profilepageviewmodel();
             model.user = db.Users.Find(userId);

            var superAdmin = (from r in db.Roles where r.Name.Contains("SuperAdmin") select r).FirstOrDefault();
            var superAdminUsers = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(superAdmin.Id)).ToList();

            var admin = (from r in db.Roles where r.Name.Contains("Admin") select r).FirstOrDefault();
            var adminUsers = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(admin.Id)).ToList();


            if (superAdminUsers.Find(x => x.Id == userId) != null)
            {
                TempData["role"] = "SuperAdmin";
            }
           

            if (adminUsers.Find(x => x.Id == userId) != null)
            {
                TempData["role"] = "Admin";
            }
            
            if (TempData["role"] == null)
            {
                TempData["role"] = "";
            }

            var e = db.EventParticipants.Where(x => x.UserID == model.user.Id).Select(x => x.EventID).ToList();

            model.myMeetings = db.Events.Where(x => e.Contains(x.Id)).ToList();

            return View(model);


        }

        


        public ActionResult EditProfile(string id)
        {
            var user = db.Users.Find(id);

            return View(user);

        }

        [HttpPost]
        public ActionResult EditProfile(ApplicationUser model, HttpPostedFileBase picUpload)
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNmbr = model.PhoneNmbr;
            user.Room = model.Room;
            user.Email = model.Email;
            user.GetMail = model.GetMail;
            if (picUpload != null && picUpload.ContentLength > 0)
            {

                user.ContentType = picUpload.ContentType;

                using (var reader = new BinaryReader(picUpload.InputStream))
                {
                    user.ProfilePicture = reader.ReadBytes(picUpload.ContentLength);
                }
            }

            db.SaveChanges();

            return RedirectToAction("ProfilePage");

        }


    }
}