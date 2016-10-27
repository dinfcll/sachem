using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using System.Data.Entity;
namespace sachem.Controllers
{
    public class HomeController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Nous contacter.";
            var contact = db.p_Contact.First();
            return View(contact);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}