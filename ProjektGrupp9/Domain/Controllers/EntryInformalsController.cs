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

namespace Domain.Controllers
{
    [Authorize]
    public class EntryInformalsController : BaseController
    {

        // GET: EntryInformals
        public ActionResult Index()
        {
            return View(db.InformalEntries.ToList());
        }

        // GET: EntryInformals/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EntryInformal entryInformal = db.InformalEntries.Find(id);
        //    if (entryInformal == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(entryInformal);
        //}

        // GET: EntryInformals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntryInformals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Heading,Text")] EntryInformal entryInformal, HttpPostedFileBase picUpload)
        {
            if (Request.IsAuthenticated && ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId()) as ApplicationUser;
                EntryInformal aEntry = new EntryInformal();

                if (picUpload != null && picUpload.ContentLength > 0)
                {
                    aEntry.Filename = picUpload.FileName;
                    aEntry.ContentType = picUpload.ContentType;

                    using (var reader = new BinaryReader(picUpload.InputStream))
                    {
                        aEntry.File = reader.ReadBytes(picUpload.ContentLength);
                    }
                }

                aEntry.Heading = entryInformal.Heading;
                aEntry.Text = entryInformal.Text;
                aEntry.Date = DateTime.Today;
                aEntry.Author = user;
                aEntry.Likes = entryInformal.Likes;

                user.InformalEntrys.Add(aEntry);
                db.InformalEntries.Add(aEntry);
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = user.Id });
            }
            return View();
        }

        public ActionResult RemoveFile(int id)
        {

            EntryInformal entry = db.InformalEntries.First(x => x.Id == id);

            entry.File = null;
            entry.ContentType = null;
            entry.Filename = null;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = id });
        }

        public ActionResult EntryFile(int id)
        {
            var be = db.InformalEntries.Single(x => x.Id == id);
            if (be.File != null)
            {
                return File(be.File, "Image/png");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EntryInformal old = db.InformalEntries.Find(id);

            EntryInformal entry = new EntryInformal();
            entry.Id = old.Id;
            entry.Heading = old.Heading;
            entry.Text = old.Text;
            entry.Date = DateTime.Today;
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
        public ActionResult Edit(EntryInformal entry, HttpPostedFileBase picUpload)
        {

            EntryInformal entryToUpdate = new EntryInformal();

            if (ModelState.IsValid)
            {
                entryToUpdate = db.InformalEntries.Find(entry.Id);
                entryToUpdate.Text = entry.Text;
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
                return RedirectToAction("Index");
            }
            return View(entry);
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



        public ActionResult Like(int id)
        {   
            var user = db.Users.Find(User.Identity.GetUserId());
            var entry = db.InformalEntries.Find(id);
            var likes = db.Likes.ToList();

            foreach (var item in likes)
            {
                if (item.user.Id == user.Id && item.InformalEntry.Id == entry.Id)
                {
                    db.Likes.Remove(item);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            var like = new Likes();
            like.user = user;
            like.InformalEntry = entry;
            db.Likes.Add(like);

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // POST: EntryInformals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var deletedlikes = db.Likes.Where(x => x.InformalEntry.Id == id).ToList();
            foreach (var item in deletedlikes)
            {
                db.Likes.Remove(item);
            }

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
