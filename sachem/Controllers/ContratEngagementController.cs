using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;
using sachem.Classes_Sachem;

namespace sachem.Controllers
{
    public class ContratEngagementController : Controller
    {
        private readonly SACHEMEntities _db = new SACHEMEntities();

        [ValidationAcces.ValidationAccesEtu]
        public ActionResult Index()
        {
            int idDeLaPersonneConnectee = SessionBag.Current.id_Pers;
            var inscriptionDeLaPersonneConnectee = _db.Inscription.First(c => c.id_Pers == idDeLaPersonneConnectee);

            return View(inscriptionDeLaPersonneConnectee);
        }

        [HttpPost]
        public ActionResult Index(string motDePasse, bool confirmationSignatureContrat, [Bind(Include = "id_Inscription")] Inscription inscription)
        {
            motDePasse = SachemIdentite.EncrypterChaine(motDePasse);
            int idDeLaPersonneConnectee = SessionBag.Current.id_Pers;
            var personneConnectee = _db.Personne.Find(idDeLaPersonneConnectee);

            var inscriptionDeLaPersonneConnectee = _db.Inscription.Find(inscription.id_Inscription);
            
            
            if (motDePasse != personneConnectee.MP)
            {
                ModelState.AddModelError(string.Empty, Messages.MotsDePasseDoiventEtreIdentiques());
            }

            if (!confirmationSignatureContrat)
            {
                ModelState.AddModelError(string.Empty, Messages.CaseDoitEtreCochee());
            }

            if (ModelState.IsValid)
            {
                inscriptionDeLaPersonneConnectee.ContratEngagement = true;
                _db.Entry(inscriptionDeLaPersonneConnectee).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return View(inscriptionDeLaPersonneConnectee);
        }
    }
}
