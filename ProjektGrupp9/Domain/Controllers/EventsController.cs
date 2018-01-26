using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLogic.Models;

namespace Domain.Controllers
{

    [Authorize]
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            var model = new List<Events>();
            using (var context = new ApplicationDbContext())
            {
                foreach (var e in context.Events.ToList())
                {
                    model.Add(e);
                }
            }
            return View(model);
        }

        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Create(Events model)
        {
            using (var context = new ApplicationDbContext())
            {
                var newEvent = new Events();
              
                newEvent.Date = model.Date;
                newEvent.Time = model.Time;
                newEvent.Place = model.Place;
                newEvent.Description = model.Description;
                  
                context.Events.Add(newEvent);
                context.SaveChanges();
              return RedirectToAction("Index");

            }

        }

        public ActionResult Update(int id)
        {
            var model = new Events();
            using (var context = new ApplicationDbContext())
            {
                model = context.Events.Find(id);
                

                return View(model);
                
            }


        }

        [HttpPost]
        public ActionResult Update(Events model)
        {
            var oldModel = new Events();
            using (var context = new ApplicationDbContext())
            {
                oldModel = context.Events.Find(model.Id);
                oldModel.Date = model.Date;
                oldModel.Time = model.Time;
                oldModel.Place = model.Place;
                oldModel.Description = model.Description;

                context.SaveChanges();
                    
            }
            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {

            var model = new Events();
            using (var context = new ApplicationDbContext())
            {
                
                model = context.Events.Find(id);
                if (model == null)
                {
                    return RedirectToAction("index");
                }
                context.Events.Remove(model);
                context.SaveChanges();
                return View(model);

             

            }


        }
    }
}