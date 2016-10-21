﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Controllers
{
    public class ParametresController : Controller
    {
        List<TypeUsagers> RolesAcces = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super };
        private readonly SACHEMEntities db = new SACHEMEntities();

        public ActionResult IndexModifier(int? id)
        {
            return View("Edit");
        }

        
        public ActionResult Edit()
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);

            var contact = db.p_Contact.First();
            return View(contact);
        }

        [HttpGet]
        public ActionResult EditCourrier()
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);
            var courrier = db.Courriel.First();
            ViewBag.id_TypeCourriel = new SelectList(db.p_TypeCourriel, "id_TypeCourriel", "TypeCourriel");
            return View(courrier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourrier(Courriel courriel, p_TypeCourriel typeCourriel)
        {
            ViewBag.id_TypeCourriel = new SelectList(db.p_TypeCourriel, "id_TypeCourriel", "TypeCourriel");
            courriel.p_TypeCourriel = typeCourriel;
            if (courriel.DateFin != null)
            {
                if((courriel.DateDebut - courriel.DateFin.Value).TotalDays > 0)
                {
                    ModelState.AddModelError(string.Empty, Messages.C_005);
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(courriel).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = Messages.I_032();
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditContact()
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);
            var contact = db.p_Contact.First();
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContact([Bind(Include = "id_Contact,Nom,Prenom,Courriel,Telephone,Poste,Facebook,SiteWeb,Local")] p_Contact contact)
        {
            Valider(contact);

            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = string.Format(Messages.I_003(contact.Nom));
                return View(contact);
            }
            return View(contact);
        }

        [NonAction]
        private IEnumerable<Session> ObtenirSession(int session = 0)
        {
            var lstSession = from s in db.Session
                                where (db.p_HoraireInscription.Any(j => (j.id_Sess == s.id_Sess || session == 0)))
                                orderby s.NomSession
                                select s;
            return lstSession.ToList();
        }

        [NonAction]
        private void ListeSession(int session)
        {
            ViewBag.Session = new SelectList(ObtenirSession(session), "id_Sess", "NomSession", session);
        }

        //trouver un moyen de faire fonctionner la vue pour qu'elle affiche bien [Été 2016] en dropdownlist
        // MODULO ???!?!?
        public ActionResult EditHoraire()
        {
            //var horaire = db.p_HoraireInscription.First();
            //List<SelectListItem> horaireList = new List<SelectListItem>();
            //foreach (var item in db.p_HoraireInscription)
            //{
            //    var sess = db.Session.Find(item.id_Sess);
            //    horaireList.Add
            //    (
            //        new SelectListItem {Text = item.id_Sess.ToString(), Value = sess.NomSession}
            //    );
            //}
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);

            var horaire = db.p_HoraireInscription.First();

            List<SelectListItem> horaireList = new List<SelectListItem>();

            ListeSession(0);
            // return View(Tuple.Create(horaireList,horaire));
            return View();
        }


        
        [HttpPost]
        public ActionResult EditHoraire([Bind(Prefix = "Item2")] p_HoraireInscription nouvelHoraire)
        {
            
            var session = db.Session.Find(nouvelHoraire.id_Sess);
            var saison = db.p_Saison.Find(session.id_Saison);
            //regarde l'année
            if (session.Annee != nouvelHoraire.DateFin.Year || session.Annee != nouvelHoraire.DateDebut.Year)
            {
                ModelState.AddModelError(string.Empty, Messages.C_006);
        }

            //regarde si les dates sont bonnes
            if((nouvelHoraire.DateFin - nouvelHoraire.DateDebut).TotalDays < 1)
            {
                ModelState.AddModelError(string.Empty, Messages.C_005);
            }
            //Regarder si cest les bon id (ps : ca lest pas)
            switch (saison.id_Saison)
                {
                    //Si hiver : de janvier inclus jusqua mai inclus (mois fin <= 5) pas besoin de verif la date de début
                    //car on est sur que c'est la bonne année et qu'elle est avant la date de fin
                    case 1:
                        if (nouvelHoraire.DateFin.Month > new DateTime(1,5,1).Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.C_006);
                        }
                        break;
                    //Si ete : de juin inclus jusqua aout inclus (si mois du début >= 6 et mois fin <= 8)
                    case 2:
                        if (new DateTime(1,6,1).Month > nouvelHoraire.DateDebut.Month || nouvelHoraire.DateFin.Month > new DateTime(1,8,1).Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.C_006);
        }
                        break;
                    //si automne: de aout inclus jusqua decembre inclus (si mois du début >= 8 et mois fin <= 12)
                    //pas besoin de verif la date de fin car on est sur que c'est la bonne année et qu'elle est apres la date de début
                    case 3:
                        if (new DateTime(1, 8, 1).Month > nouvelHoraire.DateDebut.Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.C_006);
                        }
                    break;
                }

            
            if (ModelState.IsValid)
            {
                db.Entry(nouvelHoraire).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("EditHoraire");
            }
            return RedirectToAction("EditHoraire");
        }


        [NonAction]
        private void Valider([Bind(Include = "id_Contact,Nom,Prenom,Courriel,Telephone,Poste,Facebook,SiteWeb,Local")]p_Contact contact)
        {
            if (db.p_Contact.Any(r => r.id_Contact == contact.id_Contact && r.Prenom != contact.Prenom && r.Nom != contact.Nom))
                ModelState.AddModelError(string.Empty, Messages.I_002(contact.id_Contact.ToString()));
        }

        public ActionResult IndexCollege()
        {
            var college = from tout in db.p_College
                          orderby tout.College
                          select tout;
            return View(college);
        }
        [HttpGet]
        public ActionResult EditCollege()
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);
            var college = from c in db.p_College select c;
            return View(college);
        }

        [HttpPost]
        public void EditCollege(string nomCollege, int? id)
        {
            
            if (db.p_College.Any(r => r.id_College == id))
            {
                var college = db.p_College.Find(id);
                college.College = nomCollege;
                db.Entry(college).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public ActionResult AddCollege(string nomCollege)
        {
            if (!db.p_College.Any(p => p.College == nomCollege))
            {
                var college = new p_College
                {
                    College = nomCollege
                };
                db.p_College.Add(college);
                db.SaveChanges();
                TempData["Success"] = string.Format(Messages.I_044(nomCollege));
            }
            return RedirectToAction("IndexCollege");
        }

        [HttpPost]
        public void DeleteCollege(int? id)
        {
            var college = db.p_College.Find(id);
            if(college != null)
            {
                db.p_College.Remove(college);
                db.SaveChanges();
            }
        }
    }
}
