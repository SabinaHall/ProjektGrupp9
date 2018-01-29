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

        public ActionResult Research(string id)
        {

            EntryResearch entry = new EntryResearch();
            List<EntryResearch> entryList = db.EntryResearch.Where(x => x.Author.Id == id).ToList();
            return View(entryList);
        }

        public ActionResult Education(string id)
        {
            EntryEducation entry = new EntryEducation();
            List<EntryEducation> entryList = db.EntryEducation.Where(x => x.Author.Id == id).ToList();
            return View(entryList);
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

        public ActionResult CreateEducation()
        {
            return View();
        }

        // POST: Entries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResearch([Bind(Include = "Id,Heading,text,EntryType")] EntryResearch entries, string id, HttpPostedFileBase picUpload)
        {
            if (Request.IsAuthenticated)
            {
                var user = db.Users.First(x => x.Id == id) as ApplicationUser;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEducation([Bind(Include = "Id,Heading,text,EntryType")] EntryEducation entries, string id, HttpPostedFileBase picUpload)
        {
            if (Request.IsAuthenticated)
            {
                var user = db.Users.First(x => x.Id == id) as ApplicationUser;
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

        // GET: Entries/Edit/5
        //public ActionResult Edit(int? id)
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

        // POST: Entries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Heading,text,EntryType,Date")] Entries entries)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(entries).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(entries);
        //}


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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
