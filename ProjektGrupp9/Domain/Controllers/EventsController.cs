using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLogic.Models;
using DataLogic.DbMethods;
using Microsoft.AspNet.Identity;
using System.Web.UI.WebControls;

namespace Domain.Controllers
{

    [Authorize]
    public class EventsController : BaseController
    {
        // GET: Events
        public ActionResult Index()
        {
            var model = new EventViewModel(); 

            using (var context = new ApplicationDbContext())
            {

                model.allEvents = context.Events.OrderBy(x => x.Date).ToList();
            }
            return View(model);
        }

        public ActionResult Create()
        {

            var model = new EventViewModel();
            var users = new List<SelectListItem>();

            users = db.Users.Select(x => new SelectListItem { Value = x.Id, Text = x.UserName }).ToList();
            model.Participants = users;

            //model.users = db.Users.Where(x => x.Id != User.Identity.GetUserId()).ToList();
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(EventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Participants = db.Users.Select(x => new SelectListItem { Value = x.Id, Text = x.UserName }).ToList();
                return View(model);
            }
            using (var context = new ApplicationDbContext())
            {
                var newEvent = new Events();
              
                newEvent.Date = model.events.Date;
                newEvent.Time = model.events.Time;
                newEvent.secondaryTime = model.events.secondaryTime;
                newEvent.tertiaryTime = model.events.tertiaryTime;
                newEvent.Place = model.events.Place;
                newEvent.Description = model.events.Description;
                newEvent.Host = context.Users.Find(User.Identity.GetUserId());
                context.Events.Add(newEvent);
                context.SaveChanges();

                var sender = db.Users.Find(User.Identity.GetUserId());

                var emails = new List<string>();


                foreach (var item in model.ListId)
                {

                    var invite = new MeetingInvites()
                    {
                        EventID = db.Events.Max(x => x.Id),
                        Sender = sender.UserName,
                        Receiver = db.Users.Where(x => x.UserName == item).SingleOrDefault().Id
                        
                        
                    };
                    //var e = db.Users.Where(x => x.UserName == item).SingleOrDefault().Email;
                    //var e = db.Users.Find(item).Email;
                    emails.Add(item);

                    context.MeetingInvites.Add(invite);
                  
                }
                var message = "Du har blivit inbjuden till ett möte, gå in och se under händelser för mer detaljer!";
                var subject = "Ny inbjudan till möte";

                DataLogic.DbMethods.Methods.SendEmailInvitation(emails, message, subject);
                  
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

        

        public ActionResult EventDetails(int id)
        {
            var model = new EventViewModel();
            model.events = db.Events.Find(id);
            var l = new List<string>();
            foreach (var item in db.EventParticipants)
            {
                if (item.EventID == id)
                {
                    l.Add(item.UserID);
                }
            }
            var lu = new List<ApplicationUser>();
            foreach (var item in l)
            {
                var user = new ApplicationUser();
                user = db.Users.Find(item);
                lu.Add(user);
            }
            model.users = lu;
            return View(model);
        }

        [Authorize]
        public ActionResult MeetingInvites(List<ApplicationUser> user)
        {
            var SenderID = User.Identity.GetUserId();

            var ReceiverIDList = new List<string>();
            foreach (var item in user)
            {
                ReceiverIDList.Add(item.Id);
            }

            foreach (var item in ReceiverIDList)
            {
                var toAdd = new MeetingInvites
                {
                    Sender = SenderID,
                    Receiver = item
                };
                db.MeetingInvites.Add(toAdd);
            }
            db.SaveChanges();




            return RedirectToAction("Index");
        }

        public ActionResult Events(string id)
        {

            
            var model = new SummaryViewModel();
            
                var allInvites = db.MeetingInvites.Where(x => x.Receiver == id).ToList();

            if (allInvites.Count > 0 && db.Events.Count() > 0)
            {

                Dictionary<MeetingInvites, Events> ev = new Dictionary<MeetingInvites, Events>();
                foreach (var invite in allInvites)
                {
                    var e = db.Events.Where(x => x.Id == invite.EventID).SingleOrDefault();
                    ev.Add(invite, e);
                }
                model.YourEvents = ev;
            }
            model.Events = db.Events.Where(x => x.Date == DateTime.Today).ToList();
            model.FormalEntries = db.Entries.Where(x => x.Date == DateTime.Today).ToList();
            return View(model);

        }

        [HttpPost]
        public ActionResult Accept(AcceptViewModel model)
        {



            
            var eventID = db.MeetingInvites.Where(x => x.id == model.InviteID).Select(x => x.EventID).SingleOrDefault();

            var eventParticipants = new EventParticipants()
            {
                EventID = eventID,
                UserID = User.Identity.GetUserId()
            };
            db.EventParticipants.Add(eventParticipants);

            var m = db.MeetingInvites.Find(model.InviteID);
            db.MeetingInvites.Remove(m);

            db.SaveChanges();
            var Event = db.Events.Find(eventID);
            var user = db.Users.Find(User.Identity.GetUserId());
            var email = new List<string>();
            email.Add(user.Email);
            
            var subject = "Tillagd i ett möte";
            var message = $"Du har blivit tillagd i ett möte av: {Event.Host.FirstName} {Event.Host.LastName} <br> Datum: {Event.Date} <br> Tid: {Event.Time} <br> Plats: {Event.Place}";

            DataLogic.DbMethods.Methods.SendEmailInvitation(email, message, subject);
            ICalMessage.SendMessage(Event, user.Email);

            var emailHost = new List<string>();
            emailHost.Add(Event.Host.Email);

            var subjectHost = "Accepterad inbjudan";
            var messageHost = user.FirstName + " " + user.LastName + $" har accepterat din mötesinbjudan och valde tidsförslaget: " +
                $"{model.SelectedTime}  <br> Datum: {Event.Date} <br> Tid: {Event.Time} <br> Plats: {Event.Place}";

            DataLogic.DbMethods.Methods.SendEmailInvitation(emailHost, messageHost , subjectHost);

            return RedirectToAction("Index");

        }

        public ActionResult Decline(int id)
        {

            var m = db.MeetingInvites.Find(id);
            db.MeetingInvites.Remove(m);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

       
    }
    


}
