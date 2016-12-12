using System;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;
using System.Globalization;

namespace sachem.Controllers
{
    public class HomeController : Controller
    {
        private readonly SACHEMEntities _db = new SACHEMEntities();

        [NonAction]
        public string RetourneMessageInscriptionFermee()
        {
            var dateActuelle = DateTime.Now;
            var horaireActuel = _db.p_HoraireInscription.OrderByDescending(x => x.id_Sess).First();
            if (dateActuelle <= horaireActuel.DateDebut)
            {
                return
                    $"Les inscriptions au SACHEM ouvriront le {horaireActuel.DateFin.ToString("D", CultureInfo.CreateSpecificCulture("fr-CA"))} à {horaireActuel.HeureDebut.Hours}h00";
            }

            return dateActuelle >= horaireActuel.DateFin
                ? $"Les inscriptions au SACHEM sont terminées depuis le {horaireActuel.DateFin.ToString("D", CultureInfo.CreateSpecificCulture("fr-CA"))}. Vous pourrez vous inscrire la session prochaine."
                : "Vous êtes probablement déconnecté, reconnectez-vous.";
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Nous contacter.";
            var contact = _db.p_Contact.First();
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
