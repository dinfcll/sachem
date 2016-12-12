using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using System.Data.Entity;
using System.Globalization;

namespace sachem.Controllers
{
    public class HomeController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [NonAction]
        public string RetourneMessageInscriptionFermee()
        {
            DateTime DateActuelle = DateTime.Now;
            TimeSpan DateActuelle_Heure = TimeSpan.FromHours(DateActuelle.Hour);
            var Session = db.Session.GroupBy(s => s.id_Sess).Select(s => s.OrderByDescending(c => c.id_Sess).First()).Select(c => new { c.id_Sess });
            var HoraireActuel = db.p_HoraireInscription.OrderByDescending(x => x.id_Sess).First();
            if (DateActuelle <= HoraireActuel.DateDebut)
            {
                return string.Format("Les inscriptions au SACHEM ouvriront le {0} à {1}h00", HoraireActuel.DateFin.ToString("D", CultureInfo.CreateSpecificCulture("fr-CA")), HoraireActuel.HeureDebut.Hours);
            }
            else if (DateActuelle >= HoraireActuel.DateFin)
            {
                return string.Format("Les inscriptions au SACHEM sont terminées depuis le {0}. Vous pourrez vous inscrire la session prochaine.", HoraireActuel.DateFin.ToString("D", CultureInfo.CreateSpecificCulture("fr-CA")));
            }
            else
            {
                return "Vous êtes probablement déconnecté, reconnectez-vous.";
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Nous contacter.";
            var contact = db.p_Contact.First();
            contact.Telephone = SachemIdentite.RemettreTel(contact.Telephone);
            return View(contact);
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Ferme()
        {
            return View();
        }

        public ActionResult Deja()
        {
            return View();
        }
    }
}
