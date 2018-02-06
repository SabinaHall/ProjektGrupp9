using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLogic.Models;
using Microsoft.AspNet.Identity;

namespace Domain.Controllers
{
    [Authorize]
    public class CommentsController : BaseController
    {

        // GET: Comments
        public ActionResult Index(int? id)
        {
            //var userId = (from u in db.Comments
            //              where u.Entry.Id == id 
            //              select u.Entry.Author.Id).ToList();

            //var user = db.Users.Find(userId);

            //Session["emailInformal"] = user.Email;
            Session["entryId"] = id;

            var list = (from u in db.Comments
                         where u.TypeOfEntry == 0 && u.Entry.Id == id 
                         select u);

            //var commentList = db.Comments
            //    .Where(x => x.Entry.Id == id).ToList();

            return View(list.ToList());
        }

        public ActionResult IndexFormal(int? id)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            Session["emailFormal"] = user.Email;
            Session["entryIdFormal"] = id;

            var list = (from u in db.Comments
                        where u.TypeOfEntry == 1 && u.EntryFormal.Id == id
                        select u);

            return View(list.ToList());
        }


        public ActionResult IndexEvent(int id)
        {

            var user = db.Users.Find(User.Identity.GetUserId());
            Session["emailEvent"] = user.Email;
            Session["eventId"] = id;

            var list = (from u in db.Comments
                        where u.TypeOfEntry == 2 && u.Meeting.Id == id
                        select u);



            return View(list.ToList());
        }



        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,Date")] Comment comment, int id)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                var aEntry = db.InformalEntries.Find(id);
                Comment aComment = new Comment();
                aComment.TypeOfEntry = 0;
                aComment.Entry = aEntry;
                aComment.Text = comment.Text;
                aComment.Date = DateTime.Now;
                aComment.Writer = user;

                db.Comments.Add(aComment);
                //user.CommentList.Add(aComment);
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = id });
            }

            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult CreateFormal()
        {
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFormal([Bind(Include = "Id,Text,Date")] Comment comment, int id)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                var aEntry = db.Entries.Find(id);
                Comment aComment = new Comment();
                aComment.TypeOfEntry = 1;
                aComment.EntryFormal = aEntry;
                aComment.Text = comment.Text;
                aComment.Date = DateTime.Now;
                aComment.Writer = user;

                db.Comments.Add(aComment);
                //user.CommentList.Add(aComment);
                db.SaveChanges();
                return RedirectToAction("IndexFormal", new { Id = id });
            }

            return View(comment);
        }

        public ActionResult CreateEvent()
        {
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent([Bind(Include = "Id,Text,Date")] Comment comment, int id)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                var meeting = db.Events.Find(id);
                Comment aComment = new Comment();
                aComment.TypeOfEntry = 2;
                aComment.Meeting = meeting;
                aComment.Text = comment.Text;
                aComment.Date = DateTime.Now;
                aComment.Writer = user;

                db.Comments.Add(aComment);
                //user.CommentList.Add(aComment);
                db.SaveChanges();
                return RedirectToAction("IndexEvent", new { Id = id });
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,Date")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
