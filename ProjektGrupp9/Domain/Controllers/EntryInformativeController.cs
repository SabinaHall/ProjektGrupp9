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

            Entries entry = new Entries();
            List<Entries> entryList = db.Entries.Where(x => x.Author.Id == id).ToList();
            return View(entryList);
        }

        public ActionResult Education(string id)
        {
            Entries entry = new Entries();
            List<Entries> entryList = db.Entries.Where(x => x.Author.Id == id).ToList();
            return View(entryList);
        }
        


        [HttpPost]
        public ActionResult ResearchSearch (string researchSearch)
        {
            List<Entries> model = new List<Entries>();
            if (!String.IsNullOrEmpty(researchSearch))
            {
                model = db.Entries.Where(s => s.Author.UserName.ToLower().Contains(researchSearch.ToLower())  ||
                s.Heading.ToLower().Contains(researchSearch.ToLower())  || s.text.ToLower().Contains(researchSearch.ToLower())
                  || s.Author.Email.ToLower().Contains(researchSearch.ToLower()) ).ToList();

            }
            else
            {
                model = db.Entries.ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EducationSearch ( string educationSearch)
        {
            List<Entries> model = new List<Entries>();
            if (!String.IsNullOrEmpty(educationSearch))
            {
                model = db.Entries.Where(s => s.Author.UserName.ToLower().Contains(educationSearch.ToLower()) ||
                s.Heading.ToLower().Contains(educationSearch.ToLower()) || s.text.ToLower().Contains(educationSearch.ToLower())
                || s.Author.Email.ToLower().Contains(educationSearch.ToLower())).ToList();
            }
            else
            {
                model = db.Entries.ToList();
            }

            return View(model);
        } 
        
        // GET: Entries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entries entries = db.Entries.Find(id);
            if (entries == null)
            {
                return HttpNotFound();
            }
            return View(entries);
        }

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
        public ActionResult CreateResearch([Bind(Include = "Id,Heading,text,EntryType")] Entries entries, string id, HttpPostedFileBase picUpload)
        {
            if (Request.IsAuthenticated)
            {
                var user = db.Users.First(x => x.Id == id) as ApplicationUser;
                Entries aEntry = new Entries();

                if (picUpload != null && picUpload.ContentLength > 0)
                { 
                    aEntry.Heading = entries.Heading;
                    aEntry.text = entries.text;
                    aEntry.Date = DateTime.Now;
                    aEntry.Author = user;
                    aEntry.EntryType = 0;
                    aEntry.Filename = picUpload.FileName;
                    aEntry.ContentType = picUpload.ContentType;

                    using (var reader = new BinaryReader(picUpload.InputStream))
                    {
                        aEntry.File = reader.ReadBytes(picUpload.ContentLength);
                    }

                    user.Entries.Add(aEntry);
                    db.Entries.Add(aEntry);
                    db.SaveChanges();
                    return RedirectToAction("Research", "EntryInformative", new { id = user.Id });
                }
                else
                {  
                    aEntry.Heading = entries.Heading;
                    aEntry.text = entries.text;
                    aEntry.Date = DateTime.Now;
                    aEntry.Author = user;
                    aEntry.EntryType = 0;

                    user.Entries.Add(aEntry);
                    db.Entries.Add(aEntry);
                    db.SaveChanges();
                    return RedirectToAction("Research", "EntryInformative", new { id = user.Id });
                }
            }
            return View(entries);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEducation([Bind(Include = "Id,Heading,text,EntryType")] Entries entries, string id, HttpPostedFileBase picUpload)
        {
            if (Request.IsAuthenticated)
            {
                var user = db.Users.First(x => x.Id == id) as ApplicationUser;
                Entries aEntry = new Entries();

                if (picUpload != null && picUpload.ContentLength > 0)
                {
                    aEntry.Heading = entries.Heading;
                    aEntry.text = entries.text;
                    aEntry.Date = DateTime.Now;
                    aEntry.Author = user;
                    aEntry.EntryType = 0;
                    aEntry.Filename = picUpload.FileName;
                    aEntry.ContentType = picUpload.ContentType;

                    using (var reader = new BinaryReader(picUpload.InputStream))
                    {
                        aEntry.File = reader.ReadBytes(picUpload.ContentLength);
                    }

                    user.Entries.Add(aEntry);
                    db.Entries.Add(aEntry);
                    db.SaveChanges();
                    return View("Education", "EntryInformative", new { id = user.Id });
                }
                else
                {
                    aEntry.Heading = entries.Heading;
                    aEntry.text = entries.text;
                    aEntry.Date = DateTime.Now;
                    aEntry.Author = user;
                    aEntry.EntryType = 0;

                    user.Entries.Add(aEntry);
                    db.Entries.Add(aEntry);
                    db.SaveChanges();
                    return RedirectToAction("Education", "EntryInformative", new { id = user.Id });
                }
            }
            return View(entries);
        }
        //// GET: Entries/Create 
        //public ActionResult CreateInformalEntry()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateInformalEntry([Bind(Include = "Id,Heading,text,EntryType")] Entries entries, string id, HttpPostedFileBase picUpload)
        //{
        //    if (Request.IsAuthenticated)
        //    {
        //        var user = db.Users.First(x => x.Id == id) as ApplicationUser;
        //        Entries aEntry = new Entries();

        //        if (picUpload != null && picUpload.ContentLength > 0)
        //        {
        //            aEntry.Heading = entries.Heading;
        //            aEntry.text = entries.text;
        //            aEntry.Date = DateTime.Now;
        //            aEntry.Author = user;
        //            aEntry.EntryType = (EnumEntryType)1;
        //            aEntry.Filename = picUpload.FileName;
        //            aEntry.ContentType = picUpload.ContentType;

        //            using (var reader = new BinaryReader(picUpload.InputStream))
        //            {
        //                aEntry.File = reader.ReadBytes(picUpload.ContentLength);
        //            }

        //            user.Entries.Add(aEntry);
        //            db.Entries.Add(aEntry);
        //            db.SaveChanges();
        //            return RedirectToAction("IndexInformal", new { Id = user.Id });
        //        }
        //        else
        //        {
        //            aEntry.Heading = entries.Heading;
        //            aEntry.text = entries.text;
        //            aEntry.Date = DateTime.Now;
        //            aEntry.Author = user;
        //            aEntry.EntryType = (EnumEntryType)1;

        //            user.Entries.Add(aEntry);
        //            db.Entries.Add(aEntry);
        //            db.SaveChanges();
        //            return RedirectToAction("IndexInformal", new { Id = user.Id });
        //        }
        //    }
        //    return View(entries);
        //}

        public ActionResult EntryFile(int id)
        {
            var be = db.Entries.Single(x => x.Id == id);
            if (be.File != null)
            {
                return File(be.File, "Image/png");
            }
            return View();
        }

        // GET: Entries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entries entries = db.Entries.Find(id);
            if (entries == null)
            {
                return HttpNotFound();
            }
            return View(entries);
        }

        // POST: Entries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Heading,text,EntryType,Date")] Entries entries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entries);
        }

        // GET: Entries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entries entries = db.Entries.Find(id);
            if (entries == null)
            {
                return HttpNotFound();
            }
            return View(entries);
        }

        // POST: Entries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entries entries = db.Entries.Find(id);
            db.Entries.Remove(entries);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
