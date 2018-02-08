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
using System.Data.Entity.Core.Metadata.Edm;
using static DataLogic.Models.Entries;

namespace Domain.Controllers
{

    [Authorize]
    public class EntryInformativeController : BaseController
    {
        // GET: Entries

        public ActionResult Research()
        {
            Dictionary<EntryResearch, List<string>> model = new Dictionary<EntryResearch, List<string>>();
            var allEntries = db.EntryResearch.ToList();
            foreach(var entries in allEntries)
            {
                var entrieTags = db.EntryTagEntries.Where(x => x.EntryId == entries.Id).Select(x => x.TagId).ToList();
                var tagNames = db.EntryTags.Where(x => entrieTags.Contains(x.Id.ToString())).Select(x => x.TagName).ToList();
                model.Add(entries, tagNames);
            }
            return View(model);
        }

        public ActionResult Education()
        {

            Dictionary<EntryEducation, List<string>> model = new Dictionary<EntryEducation, List<string>>();
            var allEntries = db.EntryEducation.ToList();
            foreach (var entrie in allEntries)
            {
                var entrieTags = db.EntryTagEntries.Where(x => x.EntryId == entrie.Id).Select(x => x.TagId).ToList();
                var tagNames = db.EntryTags.Where(x => entrieTags.Contains(x.Id.ToString())).Select(x => x.TagName).ToList();
                model.Add(entrie, tagNames);
            }

            return View(model);



            
        }


        [HttpPost]
        public ActionResult ResearchSearch(string researchSearch)
        {
            List<EntryResearch> model = new List<EntryResearch>();
            if (!String.IsNullOrEmpty(researchSearch))
            {
                model = db.EntryResearch.Where(s => s.Author.UserName.ToLower().Contains(researchSearch.ToLower()) ||
                s.Heading.ToLower().Contains(researchSearch.ToLower()) || s.text.ToLower().Contains(researchSearch.ToLower())
                  || s.Author.Email.ToLower().Contains(researchSearch.ToLower())).ToList();

            }
            else
            {
                model = db.EntryResearch.ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EducationSearch(string educationSearch)
        {
            List<EntryEducation> model = new List<EntryEducation>();
            if (!String.IsNullOrEmpty(educationSearch))
            {
                model = db.EntryEducation.Where(s => s.Author.UserName.ToLower().Contains(educationSearch.ToLower()) ||
                s.Heading.ToLower().Contains(educationSearch.ToLower()) || s.text.ToLower().Contains(educationSearch.ToLower())
                || s.Author.Email.ToLower().Contains(educationSearch.ToLower())).ToList();
            }
            else
            {
                model = db.EntryEducation.ToList();
            }

            return View(model);
        }

        public ActionResult EditEducation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EntryEducation old = db.EntryEducation.Find(id);

            EntryEducation entryEducation = new EntryEducation();
            entryEducation.Id = old.Id;
            entryEducation.Heading = old.Heading;
            entryEducation.text = old.text;
            entryEducation.Date = DateTime.Now;
            entryEducation.Filename = old.Filename;
            entryEducation.ContentType = old.ContentType;
            entryEducation.File = old.File;
            entryEducation.Author = old.Author;

            
            if (entryEducation == null)
            {
                return HttpNotFound();
            }
            



            var model = new CreateEducationViewModel();
            var tags = new List<SelectListItem>();

            tags = db.EducationTag.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.TagName }).ToList();
            model.TagNameList = tags;
            model.Entries = entryEducation;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEducation(CreateEducationViewModel model, HttpPostedFileBase picUpload)
        {

            EntryEducation entryToUpdate = new EntryEducation();

            if (ModelState.IsValid)
            {
                entryToUpdate = db.EntryEducation.Find(model.Entries.Id);
                entryToUpdate.text = model.Entries.text;
                entryToUpdate.Heading = model.Entries.Heading;
                entryToUpdate.Date = model.Entries.Date;
                
                




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
                var entryTags = db.EntryTagEntries.Where(x => x.EntryId == model.Entries.Id).ToList();

                if (entryTags != null) //Ändringar sparas ej?
                {
                    db.EntryTagEntries.RemoveRange(entryTags);
                    db.SaveChanges();
                }


                if (model.SelectedTagIds != null)
                {
                    foreach (var item in model.SelectedTagIds)
                    {



                        var selectedTag = new EntryTagEntries();
                        selectedTag.EntryId = db.EntryResearch.Max(x => x.Id);
                        selectedTag.TagId = item;
                        db.EntryTagEntries.Add(selectedTag);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Education");
            }
            return View(model);
        }


        public ActionResult EditResearch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EntryResearch old = db.EntryResearch.Find(id);

            EntryResearch entryResearch = new EntryResearch();
            entryResearch.Id = old.Id;
            entryResearch.Heading = old.Heading;
            entryResearch.text = old.text;
            entryResearch.Date = DateTime.Now;
            entryResearch.Filename = old.Filename;
            entryResearch.ContentType = old.ContentType;
            entryResearch.File = old.File;
            entryResearch.Author = old.Author;


            if (entryResearch == null)
            {
                return HttpNotFound();
            }
            return View(entryResearch);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditResearch(EntryResearch entryInformative, HttpPostedFileBase picUpload)
        {

            EntryResearch entryToUpdate = new EntryResearch();

            if (ModelState.IsValid)
            {
                entryToUpdate = db.EntryResearch.Find(entryInformative.Id);
                entryToUpdate.text = entryInformative.text;
                entryToUpdate.Heading = entryInformative.Heading;
                entryToUpdate.Date = entryInformative.Date;
                entryToUpdate.Filename = entryInformative.Filename;
                entryToUpdate.ContentType = entryInformative.ContentType;
                entryToUpdate.File = entryInformative.File;


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
                return RedirectToAction("Research");
            }
            return View(entryInformative);
        }
        public ActionResult CreateResearch()
        {
            var model = new CreateResearchViewModel();
            var tags = new List<SelectListItem>();

            tags = db.ResearchTag.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.TagName }).ToList();
            model.TagNameList = tags;

            return View(model);
        }

           

        // POST: Entries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResearch( CreateResearchViewModel model, HttpPostedFileBase picUpload)
        {
            if (Request.IsAuthenticated && ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId()) as ApplicationUser;
                EntryResearch aEntry = new EntryResearch();

                if (picUpload != null && picUpload.ContentLength > 0)
                {
                    aEntry.Filename = picUpload.FileName;
                    aEntry.ContentType = picUpload.ContentType;

                    using (var reader = new BinaryReader(picUpload.InputStream))
                    {
                        aEntry.File = reader.ReadBytes(picUpload.ContentLength);
                    }
                }

                aEntry.Heading = model.Entries.Heading;
                aEntry.text = model.Entries.text;
                aEntry.Date = DateTime.Now;
                aEntry.Author = user;

                user.ResearchEntries.Add(aEntry);
                db.EntryResearch.Add(aEntry);

                db.SaveChanges();
                if (model.SelectedTagIds != null)
                {
                    foreach (var item in model.SelectedTagIds)
                    {
                        var selectedTag = new EntryTagEntries();
                        selectedTag.EntryId = db.EntryResearch.Max(x => x.Id);
                        selectedTag.TagId = item;
                        db.EntryTagEntries.Add(selectedTag);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Research", "EntryInformative", new { id = user.Id });
            }
            return View(model);
        }

        public ActionResult CreateEducation()
        {

            var model = new CreateEducationViewModel();
            var tags = new List<SelectListItem>();

            tags = db.EducationTag.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.TagName }).ToList();
            model.TagNameList = tags;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEducation(CreateEducationViewModel model, HttpPostedFileBase picUpload)
        {
            if (Request.IsAuthenticated && ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId()) as ApplicationUser;
                EntryEducation aEntry = new EntryEducation();

                if (picUpload != null && picUpload.ContentLength > 0)
                {
                    aEntry.Filename = picUpload.FileName;
                    aEntry.ContentType = picUpload.ContentType;

                    using (var reader = new BinaryReader(picUpload.InputStream))
                    {
                        aEntry.File = reader.ReadBytes(picUpload.ContentLength);
                    }
                }

                aEntry.Heading = model.Entries.Heading;
                aEntry.text = model.Entries.text;
                aEntry.Date = DateTime.Now;
                aEntry.Author = user;

                user.EducationEntries.Add(aEntry);
                db.EntryEducation.Add(aEntry);
                db.SaveChanges();


                if (model.SelectedTagIds != null)
                {
                    foreach (var item in model.SelectedTagIds)
                    {
                        var selectedTag = new EducationTagEntries();
                        selectedTag.EntryId = db.EntryEducation.Max(x => x.Id);
                        selectedTag.TagId = db.EducationTag.Where(x => x.TagName == item).SingleOrDefault().Id.ToString();
                        db.EducationTagEntries.Add(selectedTag);
                    }
                }

                db.SaveChanges();



                return RedirectToAction("Education", "EntryInformative", new { id = user.Id });
            }
            return View(model);
        }

        public ActionResult EntryFileResearch(int id)
        {
            var be = db.EntryResearch.Single(x => x.Id == id);
            if (be.File != null)
            {
                return File(be.File, be.ContentType);
            }
            return View();
        }

        public ActionResult RemoveFileEducation(int id)
        {

            EntryEducation entry = db.EntryEducation.First(x => x.Id == id);

            entry.File = null;
            entry.ContentType = null;
            entry.Filename = null;
            db.SaveChanges();

            return RedirectToAction("EditEducation", new { id = id});
        }

        public ActionResult RemoveFileResearch(int id)
        {

            EntryResearch entry = db.EntryResearch.First(x => x.Id == id);

            entry.File = null;
            entry.ContentType = null;
            entry.Filename = null;
            db.SaveChanges();
            return RedirectToAction("EditResearch", new { id = id });
        }

        public ActionResult EntryFileEducation(int id)
        {
            var be = db.EntryEducation.Single(x => x.Id == id);
            if (be.File != null)
            {
                return File(be.File, be.ContentType);
            }
            return View();
        }

        public ActionResult DeleteResearch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryResearch entries = db.EntryResearch.Find(id);
            if (entries == null)
            {
                return HttpNotFound();
            }
            return View(entries);
        }

        public ActionResult DeleteEducation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryEducation entries = db.EntryEducation.Find(id);
            if (entries == null)
            {
                return HttpNotFound();
            }
            return View(entries);
        }

        // POST: Entries/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteResearchConfirmed(int id)
        {
            EntryResearch entries = db.EntryResearch.Find(id);
            db.EntryResearch.Remove(entries);
            db.SaveChanges();
            return RedirectToAction("Research", new { Id = User.Identity.GetUserId() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEducationConfirmed(int id)
        {
            EntryEducation entries = db.EntryEducation.Find(id);
            db.EntryEducation.Remove(entries);
            db.SaveChanges();
            return RedirectToAction("Education", new { Id = User.Identity.GetUserId() });
        }
    }
}

