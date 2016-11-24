﻿using System;
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
            //TODO : envoyer la personne dans la view aussi pour ne pas a avoir a aller le chercher une deuxieme fois
            //TODO envoyer le bon message dans la table Formulaire
            //TODO mettre la question dans la table Question
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
                ModelState.AddModelError(string.Empty, "Erreur mot de passe");
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

            return View();
        }
    }
}