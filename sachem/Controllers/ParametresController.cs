using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;
using sachem.Classes_Sachem;

namespace sachem.Controllers
{
    public class ParametresController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();

        [ValidationAccesParametres]
        public ActionResult IndexModifier(int? id)
        {
            return View("Edit");
        }

        [ValidationAccesParametres]
        public ActionResult Edit()
        {
            var contact = db.p_Contact.First();
            return View(contact);
        }

        
        [HttpGet]
        [ValidationAccesParametres]
        public ActionResult EditCourrier()
        {
            var courrier = db.Courriel.First();
            ViewBag.id_TypeCourriel = new SelectList(db.p_TypeCourriel, "id_TypeCourriel", "TypeCourriel");
            return View(courrier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationAccesParametres]
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
        [ValidationAccesParametres]
        public ActionResult EditContact()
        {
            var contact = db.p_Contact.First();
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationAccesParametres]
        public ActionResult Edit([Bind(Include = "id_Contact,Nom,Prenom,Courriel,Telephone,Poste,Facebook,SiteWeb,Local")] p_Contact contact)
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

        
        //Méthode qui envoie a la view Edit horaire la liste de toutes les horaires d'inscription ainsi que l'horaire de la session courrante
        [ValidationAccesParametres]
        public ActionResult EditHoraire()
        {
            var horaire = db.p_HoraireInscription.First();

            List<SelectListItem> horaireList = new List<SelectListItem>();

            foreach (var item in db.p_HoraireInscription)
            {
                var sess = db.Session.Find(item.id_Sess);
                horaireList.Add
                (
                    new SelectListItem {Text = item.id_Sess.ToString(), Value = sess.NomSession}
                );
            }
            return View(Tuple.Create(horaireList,horaire));
        }


        [HttpPost]
        [ValidationAccesParametres]
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


        [ValidationAccesParametres]
        public ActionResult IndexCollege()
        {
            var college = from tout in db.p_College
                          orderby tout.College
                          select tout;
            return View(college);
        }

        [HttpGet]
        [ValidationAccesParametres]
        public ActionResult EditCollege()
        {
            var college = from c in db.p_College select c;
            return View(college);
        }

        
        [HttpPost]
        [ValidationAccesParametres]
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

        [ValidationAccesParametres]
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
        [ValidationAccesParametres]
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
