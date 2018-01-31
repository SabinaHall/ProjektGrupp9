using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLogic.Models;

namespace Domain.Controllers
{
    public class EntryTagsController : BaseController
    {

        // GET: EntryTags
        public ActionResult Index()
        {
            return View(db.Tags.ToList());
        }

        // GET: EntryTags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryTag entryTag = db.Tags.Find(id);
            if (entryTag == null)
            {
                return HttpNotFound();
            }
            return View(entryTag);
        }

        // GET: EntryTags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntryTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] EntryTag entryTag)
        {
            if (ModelState.IsValid)
            {
                db.Tags.Add(entryTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(entryTag);
        }

        // GET: EntryTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryTag entryTag = db.Tags.Find(id);
            if (entryTag == null)
            {
                return HttpNotFound();
            }
            return View(entryTag);
        }

        // POST: EntryTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] EntryTag entryTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entryTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entryTag);
        }

        // GET: EntryTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryTag entryTag = db.Tags.Find(id);
            if (entryTag == null)
            {
                return HttpNotFound();
            }
            return View(entryTag);
        }

        // POST: EntryTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntryTag entryTag = db.Tags.Find(id);
            db.Tags.Remove(entryTag);
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
