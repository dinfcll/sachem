using System;
using System.Collections.Generic;
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
            var idDeLaPersonneConnectee = SessionBag.Current.id_Pers;
            Personne personneConnectee = db.Personne.Find(idDeLaPersonneConnectee);
            return View();
        }
    }
}