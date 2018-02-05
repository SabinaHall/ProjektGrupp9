using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLogic.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core.Metadata.Edm;
using static DataLogic.Models.Entries;

namespace Domain.Controllers
{

    [Authorize]
    public class EntryInformativeController : BaseController
    {
        // GET: Entries

        public ActionResult Research()
        {
            return View(db.EntryResearch.ToList());
        }

        public ActionResult Education()
        {
            return View(db.EntryEducation.ToList());
        }


        [HttpPost]
        public ActionResult ResearchSearch(string researchSearch)
        {
            List<EntryResearch> model = new List<EntryResearch>();
            if (!String.IsNullOrEmpty(researchSearch))
            {
                model = db.EntryResearch.Where(s => s.Author.UserName.ToLower().Contains(researchSearch.ToLower()) ||
                s.Heading.ToLower().Contains(researchSearch.ToLower()) || s.text.ToLower().Contains(researchSearch.ToLower())
                  || s.Author.Email.ToLower().Contains(researchSearch.ToLower())).ToList();

            }
            else
            {
                model = db.EntryResearch.ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EducationSearch(string educationSearch)
        {
            List<EntryEducation> model = new List<EntryEducation>();
            if (!String.IsNullOrEmpty(educationSearch))
            {
                model = db.EntryEducation.Where(s => s.Author.UserName.ToLower().Contains(educationSearch.ToLower()) ||
                s.Heading.ToLower().Contains(educationSearch.ToLower()) || s.text.ToLower().Contains(educationSearch.ToLower())
                || s.Author.Email.ToLower().Contains(educationSearch.ToLower())).ToList();
            }
            else
            {
                model = db.EntryEducation.ToList();
            }

            return View(model);
        }

        public ActionResult EditEducation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EntryEducation entryEducation = db.EntryEducation.Find(id);
            if (entryEducation == null)
            {
                return HttpNotFound();
            }
            return View(entryEducation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEducation(EntryEducation entryInformative, HttpPostedFileBase picUpload)
        {

            EntryEducation entryToUpdate = new EntryEducation();

            if (ModelState.IsValid)
            {
                entryToUpdate = db.EntryEducation.Find(entryInformative.Id);
                entryToUpdate.text = entryInformative.text;
                entryToUpdate.Heading = entryInformative.Heading;
                entryToUpdate.Date = entryInformative.Date;
                entryToUpdate.Filename = entryInformative.Filename;
                entryToUpdate.ContentType = entryInformative.ContentType;



                if (picUpload != null && picUpload.ContentLength > 0)
                {
                    entryToUpdate.Filename = picUpload.FileName;
                    entryToUpdate.ContentType = picUpload.ContentType;

                    using (var reader = new BinaryReader(picUpload.InputStream))
                    {
                        entryToUpdate.File = reader.ReadBytes(picUpload.ContentLength);
                    }
                }



                db.SaveChanges();
                return RedirectToAction("Education");
            }
            return View(entryInformative);
        }


        public ActionResult EditResearch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EntryResearch entryResearch = db.EntryResearch.Find(id);
            if (entryResearch == null)
            {
                return HttpNotFound();
            }
            return View(entryResearch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditResearch(EntryResearch entryInformative)
        {

            EntryResearch entryToUpdate = new EntryResearch();

            if (ModelState.IsValid)
            {
                entryToUpdate = db.EntryResearch.Find(entryInformative.Id);
                entryToUpdate.text = entryInformative.text;
                entryToUpdate.Heading = entryInformative.Heading;
                entryToUpdate.Date = entryInformative.Date;
                entryToUpdate.Filename = entryInformative.Filename;
                entryToUpdate.ContentType = entryInformative.ContentType;
                entryToUpdate.File = entryInformative.File;



                db.SaveChanges();
                return RedirectToAction("Education");
            }
            return View(entryInformative);
        }


        //// GET: Entries/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Entries entries = db.Entries.Find(id);
        //    if (entries == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(entries);
        //}

        // GET: Entries/Create
        public ActionResult CreateResearch()
        {
            return View();
        }

        // POST: Entries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResearch([Bind(Include = "Heading,text,EntryType")] EntryResearch entries, HttpPostedFileBase picUpload)
        {
            if (Request.IsAuthenticated && ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId()) as ApplicationUser;
                EntryResearch aEntry = new EntryResearch();

                if (picUpload != null && picUpload.ContentLength > 0)
                {
                    aEntry.Filename = picUpload.FileName;
                    aEntry.ContentType = picUpload.ContentType;

                    using (var reader = new BinaryReader(picUpload.InputStream))
                    {
                        aEntry.File = reader.ReadBytes(picUpload.ContentLength);
                    }
                }

                aEntry.Heading = entries.Heading;
                aEntry.text = entries.text;
                aEntry.Date = DateTime.Now;
                aEntry.Author = user;

                user.ResearchEntries.Add(aEntry);
                db.EntryResearch.Add(aEntry);
                db.SaveChanges();

                return RedirectToAction("Research", "EntryInformative", new { id = user.Id });
            }
            return View(entries);
        }

        public ActionResult CreateEducation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEducation([Bind(Include = "Heading,text,EntryType")] EntryEducation entries, HttpPostedFileBase picUpload)
        {
            if (Request.IsAuthenticated && ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId()) as ApplicationUser;
                EntryEducation aEntry = new EntryEducation();

                if (picUpload != null && picUpload.ContentLength > 0)
                {
                    aEntry.Filename = picUpload.FileName;
                    aEntry.ContentType = picUpload.ContentType;

                    using (var reader = new BinaryReader(picUpload.InputStream))
                    {
                        aEntry.File = reader.ReadBytes(picUpload.ContentLength);
                    }
                }

                aEntry.Heading = entries.Heading;
                aEntry.text = entries.text;
                aEntry.Date = DateTime.Now;
                aEntry.Author = user;

                user.EducationEntries.Add(aEntry);
                db.EntryEducation.Add(aEntry);
                db.SaveChanges();
                return RedirectToAction("Education", "EntryInformative", new { id = user.Id });
            }
            return View(entries);
        }

        public ActionResult EntryFileResearch(int id)
        {
            var be = db.EntryResearch.Single(x => x.Id == id);
            if (be.File != null)
            {
                return File(be.File, be.ContentType);
            }
            return View();
        }

        public ActionResult EntryFileEducation(int id)
        {
            var be = db.EntryEducation.Single(x => x.Id == id);
            if (be.File != null)
            {
                return File(be.File, be.ContentType);
            }
            return View();
        }

        public ActionResult DeleteResearch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryResearch entries = db.EntryResearch.Find(id);
            if (entries == null)
            {
                return HttpNotFound();
            }
            return View(entries);
        }

        public ActionResult DeleteEducation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryEducation entries = db.EntryEducation.Find(id);
            if (entries == null)
            {
                return HttpNotFound();
            }
            return View(entries);
        }

        // POST: Entries/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteResearchConfirmed(int id)
        {
            EntryResearch entries = db.EntryResearch.Find(id);
            db.EntryResearch.Remove(entries);
            db.SaveChanges();
            return RedirectToAction("Research", new { Id = User.Identity.GetUserId() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEducationConfirmed(int id)
        {
            EntryEducation entries = db.EntryEducation.Find(id);
            db.EntryEducation.Remove(entries);
            db.SaveChanges();
            return RedirectToAction("Education", new { Id = User.Identity.GetUserId() });
        }
    }
}
