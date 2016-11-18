using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Controllers
{
    public class ContratEngagementController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();
        // GET: ContratEngagement
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string motDePasse, bool confirmationSignatureContrat)
        {
            motDePasse = SachemIdentite.encrypterChaine(motDePasse);
            int idDeLaPersonneConnectee = SessionBag.Current.id_Pers;
            Personne personneConnectee = db.Personne.Find(idDeLaPersonneConnectee);

            Inscription inscriptionDeLaPersonneConnectee = db.Inscription.First(c => c.id_Pers == idDeLaPersonneConnectee);
            inscriptionDeLaPersonneConnectee.ContratEngagement = true;

            if (motDePasse != personneConnectee.MP)
            {
                ModelState.AddModelError(string.Empty, "Erreur mot de passe");
            }

            if (!confirmationSignatureContrat)
            {
                ModelState.AddModelError(string.Empty,"Cochez la case pour signer le contrat");
            }

            if (ModelState.IsValid)
            {
                db.Entry(inscriptionDeLaPersonneConnectee).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View();
        }
    }
}