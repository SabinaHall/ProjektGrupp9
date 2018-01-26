using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Domain.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Vår 'about'-sida.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Vår kontaktsida.";

            return View();
        }
    }
}