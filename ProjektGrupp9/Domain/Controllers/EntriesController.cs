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
        public ActionResult Create([Bind(Include = "Id,Heading,text")] Entries entries, string id, HttpPostedFileBase picUpload)
        {
            if (Request.IsAuthenticated && ModelState.IsValid)
            {
                var user = db.Users.First(x => x.Id == id) as ApplicationUser;
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
