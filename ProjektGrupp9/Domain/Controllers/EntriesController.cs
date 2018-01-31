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
            var model = new TagEntryViewModel();
            model.Entries = db.Entries.ToList();
            model.Tags = db.Tags.ToList();
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

            tags = db.Tags.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.TagName }).ToList();
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

                EntryTag et1 = new EntryTag();
                et1.Id = 0;
                et1.TagName = "Möte";

                EntryTag et2 = new EntryTag();
                et2.Id = 0;
                et2.TagName = "Information";

                EntryTag et3 = new EntryTag();
                et3.Id = 0;
                et3.TagName = "Övrigt";

                db.Tags.Add(et1);
                db.Tags.Add(et2);
                db.Tags.Add(et3);

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
            var user = db.Users.Find(User.Identity.GetUserId());
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

            foreach (var item in model.SelectedTagIds)
            {
                var selectedTag = new EntryTagEntries();
                selectedTag.EntryId = db.Entries.Max(x => x.Id);
                selectedTag.TagId = item;
                db.EntryTagEntries.Add(selectedTag);
            }

            db.SaveChanges();
            return RedirectToAction("IndexFormal", new { Id = user.Id });
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
            Entries entries = db.Entries.Find(id);
            if (entries == null)
            {
                return HttpNotFound();
            }
            return View(entries);
        }

        // POST: Entries/Edit/5
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
