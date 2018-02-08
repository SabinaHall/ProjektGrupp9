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
            return View(db.EntryTags.ToList());
        }

        // GET: EntryTags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryTag entryTag = db.EntryTags.Find(id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TagName")] EntryTag entryTag)
        {
            EntryTag aTag = new EntryTag();
            aTag.TagName = entryTag.TagName;

            db.EntryTags.Add(aTag);
            db.SaveChanges();
            return RedirectToAction("CreateTest", "Entries");

            //return View(entryTag);
        }

        public ActionResult CreateEducation()
        {
            return View();
        }

        // POST: EntryTags/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEducation([Bind(Include = "TagName")] EntryTag entryTag)
        {
            EducationTag aTag = new EducationTag();
            aTag.TagName = entryTag.TagName;

            db.EducationTag.Add(aTag);
            db.SaveChanges();
            return RedirectToAction("CreateEducation", "EntryInformative");

            //return View(entryTag);
        }

        public ActionResult CreateResearch()
        {
            return View();
        }

        // POST: EntryTags/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResearch([Bind(Include = "TagName")] EntryTag entryTag)
        {
            ResearchTag aTag = new ResearchTag();
            aTag.TagName = entryTag.TagName;

            db.ResearchTag.Add(aTag);
            db.SaveChanges();
            return RedirectToAction("CreateResearch", "EntryInformative");

            //return View(entryTag);
        }

        // GET: EntryTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryTag entryTag = db.EntryTags.Find(id);
            if (entryTag == null)
            {
                return HttpNotFound();
            }
            return View(entryTag);
        }

        // POST: EntryTags/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TagName")] EntryTag entryTag)
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
            EntryTag entryTag = db.EntryTags.Find(id);
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
            EntryTag entryTag = db.EntryTags.Find(id);
            db.EntryTags.Remove(entryTag);
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
