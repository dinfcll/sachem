﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Controllers
{
    public class RechercheInscriptionController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();
        
        public ActionResult Index()
        {
            var touteInscription = from nom in db.Inscription
                select nom;
            ListeSession();
            ListeStatut();
            ListeTypeInscription();

            return View(touteInscription.ToList());
        }

        // GET: RechercheInscription/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RechercheInscription/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RechercheInscription/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RechercheInscription/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RechercheInscription/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RechercheInscription/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RechercheInscription/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        private void ListeSession(int Session = 0)
        {
            var lSessions = from session in db.Session select session;
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", Session));

            ViewBag.Session = slSession;
        }

        [NonAction]
        private void ListeTypeInscription(int TypeInscription = 0)
        {
            var lTypeInscription = from typeinscription in db.p_TypeInscription select typeinscription;
            var slTypeInscription = new List<SelectListItem>();
            slTypeInscription.AddRange(new SelectList(lTypeInscription, "id_TypeInscription", "TypeInscription", TypeInscription));

            ViewBag.TypeInscription = slTypeInscription;
        }

        [NonAction]
        private void ListeStatut(int Statut = 0)
        {
            var lStatut = from statut in db.p_StatutCours select statut;
            var slStatut = new List<SelectListItem>();
            slStatut.AddRange(new SelectList(lStatut, "id_Statut", "Statut", Statut));

            ViewBag.Statut = slStatut;
        }
    }
}
