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
    public class EntriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Entries
        public ActionResult IndexFormal()
        {
            return View(db.Entries.ToList());
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Heading,text,EntryType,Category")] Entries entries, string id, HttpPostedFileBase picUpload)
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
                    return RedirectToAction("IndexFormal", new { Id = user.Id });
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
                    return RedirectToAction("IndexFormal", new { Id = user.Id });
                }
            }
            return View(entries);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInformalEntry([Bind(Include = "Id,Heading,text,EntryType,Category")] Entries entries, string id, HttpPostedFileBase picUpload)
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
                    aEntry.EntryType = (EnumEntryType)1;
                    aEntry.Filename = picUpload.FileName;
                    aEntry.ContentType = picUpload.ContentType;

                    using (var reader = new BinaryReader(picUpload.InputStream))
                    {
                        aEntry.File = reader.ReadBytes(picUpload.ContentLength);
                    }

                    user.Entries.Add(aEntry);
                    db.Entries.Add(aEntry);
                    db.SaveChanges();
                    return RedirectToAction("IndexFormal", new { Id = user.Id });
                }
                else
                {
                    aEntry.Heading = entries.Heading;
                    aEntry.text = entries.text;
                    aEntry.Date = DateTime.Now;
                    aEntry.Author = user;
                    aEntry.EntryType = (EnumEntryType)1;

                    user.Entries.Add(aEntry);
                    db.Entries.Add(aEntry);
                    db.SaveChanges();
                    return RedirectToAction("IndexFormal", new { Id = user.Id });
                }
            }
            return View(entries);
        }

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
