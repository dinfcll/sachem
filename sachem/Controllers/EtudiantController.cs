﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using System.Linq;
using System.Web.Services;

namespace sachem.Controllers
{
    public class EtudiantController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();

        // GET: Etudiant
        public ActionResult Index()
        {
            List<object> lpersonne = new List<object>();

            //var test = from d in db.Personne
            //           where 

            //var test = from d in db.EtuProgEtude
            //    where d.id_Etu == d.Personne.id_Pers && d.id_EtuProgEtude == d.ProgrammeEtude.id_ProgEtu
            //    select d;

            var personne = from c in db.Personne
                           //join gr in db.EtuProgEtude on c.id_Pers equals gr.id_Etu
                           //join pr in db.ProgrammeEtude on gr.id_Etu equals pr.id_ProgEtu
                           where c.Actif == true && c.id_TypeUsag == 1
                           select c;
            int nbpers = personne.Count();
            int i = 0;
            foreach (var pers in personne)
            {

                //where d.id_Etu == pers.id_Pers
                //select d;

                //pers.ProgEtu = ProgEtu.ToString();
                //// PROBLEME A REGLER, olivier filion a un programme qui n'existe pas dans BD programma etude


                //var pEtu = (from p in db.EtuProgEtude
                //            where pers.id_Pers = 
                //            orderby p.id_Sess descending//FirstOrDefault
                //            select p).FirstOrDefault();


                //var id = db.EtuProgEtude.Find(pers.id_Pers);
                //    if (id != null)
                //        {
                //            pers.idpEtu = id.id_ProgEtu;
                //            var pEtu = db.ProgrammeEtude.Find(id.id_ProgEtu);
                //            string pe = pEtu.NomProg;
                //            pers.ProgEtu = pe;
                //            i = i + 1;
                //        }
            }
            //i = i + 0;
            //    var pEtu = (from p in db.EtuProgEtude
            //                where
            //                orderby p.id_Sess descending//FirstOrDefault
            //                select p).FirstOrDefault();
            //string pe = pEtu.NomProg;
            //pers.ProgEtu = pe;
            //var pEtu = from d in db.ProgrammeEtude
            //           where d.id_ProgEtu == id.id_ProgEtu
            //           orderby d.Annee descending
            //           select d;
            //var pEtu = db.ProgrammeEtude.Find();
            //string pe = pEtu.FirstOrDefault().ToString();
            //pers.ProgEtu = pe;
            //(From p In context.Persons Select p Order By age Descending).FirstOrDefault
            //}

            ListeSession();
            ListeCours();
            ListeGroupe();


            return View(personne.ToList());
        }

        public string ProgEtude()
        {
          
            return "allo";
        }

        private void ListeSession(int Session = 0)
        {

            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", Session));

            ViewBag.Session = slSession;

        }
        private void ListeCours(int Cours = 0)
        {

            var lCours = db.Cours.AsNoTracking().OrderBy(s => s.Actif).ThenBy(s => s.id_Cours);
            var slCours = new List<SelectListItem>();
            slCours.AddRange(new SelectList(lCours, "id_Cours", "Nom", Cours)); //, "id_Cours", "Nom"));

            ViewBag.Cours = slCours;
        }
        private void ListeGroupe(int Groupe = 0)
        {

            var lGroupe = db.Groupe.AsNoTracking().OrderBy(s => s.NoGroupe);
            var slGroupe = new List<SelectListItem>();
            slGroupe.AddRange(new SelectList(lGroupe, "id_Groupe","NoGroupe", Groupe)); //, "id_Cours", "Nom"));

            ViewBag.Groupe = slGroupe;
        }
        // GET: Etudiant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // GET: Etudiant/Create
        public ActionResult Create()
        {
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag");
            return View();
        }

        // POST: Etudiant/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule,MP,Courriel,Telephone,DateNais,Actif")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                db.Personne.Add(personne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // GET: Etudiant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        public void FillDropDownlist()
        {
            
        }
        // POST: Etudiant/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule,MP,Courriel,Telephone,DateNais,Actif")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // GET: Etudiant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // POST: Etudiant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personne personne = db.Personne.Find(id);
            db.Personne.Remove(personne);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
