﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLogic.Models;
using System.IO;

namespace Domain.Controllers
{
    public class EntryInformalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EntryInformals
        public ActionResult Index()
        {
            return View(db.InformalEntries.ToList());
        }

        // GET: EntryInformals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryInformal entryInformal = db.InformalEntries.Find(id);
            if (entryInformal == null)
            {
                return HttpNotFound();
            }
            return View(entryInformal);
        }

        // GET: EntryInformals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntryInformals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Heading,Text")] EntryInformal entryInformal, string id, HttpPostedFileBase picUpload)
        {
            var user = db.Users.First(x => x.Id == id) as ApplicationUser;
            EntryInformal aEntry = new EntryInformal();

            if (Request.IsAuthenticated)
            {
                if (picUpload != null && picUpload.ContentLength > 0)
                {
                    aEntry.Heading = entryInformal.Heading;
                    aEntry.Text = entryInformal.Text;
                    aEntry.Date = DateTime.Now;
                    aEntry.Author = user;
                    aEntry.Filename = picUpload.FileName;
                    aEntry.ContentType = picUpload.ContentType;

                    using (var reader = new BinaryReader(picUpload.InputStream))
                    {
                        aEntry.File = reader.ReadBytes(picUpload.ContentLength);
                    }

                    db.InformalEntries.Add(entryInformal);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { Id = user.Id });
                }
                else
                {
                    aEntry.Heading = entryInformal.Heading;
                    aEntry.Text = entryInformal.Text;
                    aEntry.Date = DateTime.Now;
                    aEntry.Author = user;

                    user.InformalEntrys.Add(aEntry);
                    db.InformalEntries.Add(aEntry);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { Id = user.Id });
                }
            }
            return View(entryInformal);
        }

        // GET: EntryInformals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryInformal entryInformal = db.InformalEntries.Find(id);
            if (entryInformal == null)
            {
                return HttpNotFound();
            }
            return View(entryInformal);
        }

        // POST: EntryInformals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Heading,Text")] EntryInformal entryInformal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entryInformal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entryInformal);
        }

        // GET: EntryInformals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryInformal entryInformal = db.InformalEntries.Find(id);
            if (entryInformal == null)
            {
                return HttpNotFound();
            }
            return View(entryInformal);
        }

        // POST: EntryInformals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntryInformal entryInformal = db.InformalEntries.Find(id);
            db.InformalEntries.Remove(entryInformal);
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
