using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using static sachem.Classes_Sachem.ValidationAcces;

namespace sachem.Controllers
{
    public class ContratEngagementController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();
        [ValidationAccesEtu]
        public ActionResult Index()
        {
            int idDeLaPersonneConnectee = SessionBag.Current.id_Pers;
            Inscription inscriptionDeLaPersonneConnectee = db.Inscription.First(c => c.id_Pers == idDeLaPersonneConnectee);
            return View(inscriptionDeLaPersonneConnectee);
        }

        [HttpPost]
        public ActionResult Index(string motDePasse, bool confirmationSignatureContrat, [Bind(Include = "id_Inscription")] Inscription inscription)
        {
            motDePasse = SachemIdentite.encrypterChaine(motDePasse);
            int idDeLaPersonneConnectee = SessionBag.Current.id_Pers;
            Personne personneConnectee = db.Personne.Find(idDeLaPersonneConnectee);

            Inscription inscriptionDeLaPersonneConnectee = db.Inscription.Find(inscription.id_Inscription);
            
            
            if (motDePasse != personneConnectee.MP)
            {
                ModelState.AddModelError(string.Empty, Messages.MotsDePasseDoiventEtreIdentiques);
            }

            if (!confirmationSignatureContrat)
            {
                ModelState.AddModelError(string.Empty, "Cochez la case pour signer le contrat");
            }

            if (ModelState.IsValid)
            {
                inscriptionDeLaPersonneConnectee.ContratEngagement = true;
                db.Entry(inscriptionDeLaPersonneConnectee).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View(inscriptionDeLaPersonneConnectee);
        }
    }
}
