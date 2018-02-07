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
using Microsoft.AspNet.Identity;

namespace Domain.Controllers
{
    [Authorize]
    public class EntriesController : BaseController
    {
        // GET: Entries 
        public ActionResult IndexFormal()
        {
            Dictionary<Entries, List<string>> model = new Dictionary<Entries, List<string>>();
            var allEntries = db.Entries.ToList();
            foreach (var entrie in allEntries)
            {
                var entrieTags = db.EntryTagEntries.Where(x => x.EntryId == entrie.Id).Select(x => x.TagId).ToList();
                var tagNames = db.EntryTags.Where(x => entrieTags.Contains(x.Id.ToString())).Select(x => x.TagName).ToList();
                model.Add(entrie, tagNames);
            }

            return View(model);
        }

        public ActionResult IndexInformal()
        {
            return View(db.Entries.ToList());
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
        public ActionResult CreateTest()
        {
            var model = new CreateEntryViewModel();
            var tags = new List<SelectListItem>();

            tags = db.EntryTags.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.TagName }).ToList();
            model.TagNameList = tags;

            return View(model);
        }

        // POST: Entries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Heading,text")] Entries entries, HttpPostedFileBase picUpload)
        {
            if (Request.IsAuthenticated && ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId()) as ApplicationUser;
                Entries aEntry = new Entries();

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

                user.Entries.Add(aEntry);
                db.Entries.Add(aEntry);
                db.SaveChanges();

                

                return RedirectToAction("IndexFormal", new { Id = user.Id });
            }
            return View(entries);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTest(CreateEntryViewModel model, HttpPostedFileBase picUpload)
        {
            if(Request.IsAuthenticated && ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId()) as ApplicationUser;
                Entries aEntry = new Entries();

                aEntry.Author = user;
                aEntry.text = model.Entries.text;
                aEntry.Heading = model.Entries.Heading;
                aEntry.Date = DateTime.Now;

                if (picUpload != null && picUpload.ContentLength > 0)
                {
                    aEntry.Filename = picUpload.FileName;
                    aEntry.ContentType = picUpload.ContentType;

                    using (var reader = new BinaryReader(picUpload.InputStream))
                    {
                        aEntry.File = reader.ReadBytes(picUpload.ContentLength);
                    }
                }

                user.Entries.Add(aEntry);
                db.Entries.Add(aEntry);
                db.SaveChanges();
                if (model.SelectedTagIds != null)
                {
                    foreach (var item in model.SelectedTagIds)
                    {
                        var selectedTag = new EntryTagEntries();
                        selectedTag.EntryId = db.Entries.Max(x => x.Id);
                        selectedTag.TagId = item;
                        db.EntryTagEntries.Add(selectedTag);
                    }
                }
                
                db.SaveChanges();

                var emails = db.Users.Select(x => x.Email).ToList();
                var subject = user.Email + " har skrivit ett formellt inlägg.";
                var message = user.Email + " har lagt upp ett inlägg med titeln: " + model.Entries.Heading + ".";

                DataLogic.DbMethods.Methods.SendEmailInvitation(emails, message, subject);
                return RedirectToAction("IndexFormal", new { Id = user.Id });
            }
            return View();
        }


        public ActionResult EntryFile(int id)
        {
            var be = db.Entries.Single(x => x.Id == id);
            if (be.File != null)
            {
                return File(be.File, be.ContentType);
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

            Entries old = db.Entries.Find(id);

            Entries entry = new Entries();
            entry.Id = old.Id;
            entry.Heading = old.Heading;
            entry.text = old.text;
            entry.Date = DateTime.Now;
            entry.Filename = old.Filename;
            entry.ContentType = old.ContentType;
            entry.File = old.File;
            entry.Author = old.Author;


            if (entry == null)
            {
                return HttpNotFound();
            }
            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Entries entry, HttpPostedFileBase picUpload)
        {

            Entries entryToUpdate = new Entries();

            if (ModelState.IsValid)
            {
                entryToUpdate = db.Entries.Find(entry.Id);
                entryToUpdate.text = entry.text;
                entryToUpdate.Heading = entry.Heading;
                entryToUpdate.Date = entry.Date;






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
                return RedirectToAction("IndexFormal");
            }
            return View(entry);
        }

        public ActionResult RemoveFile(int id)
        {

            Entries entry = db.Entries.First(x => x.Id == id);

            entry.File = null;
            entry.ContentType = null;
            entry.Filename = null;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = id });
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
            EntryTagEntries aEntry = db.EntryTagEntries.Find(entries.Id);

            db.EntryTagEntries.Remove(aEntry);
            db.Entries.Remove(entries);
            db.SaveChanges();
            return RedirectToAction("IndexFormal");
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
