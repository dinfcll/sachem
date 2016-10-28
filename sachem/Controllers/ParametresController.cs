using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using static sachem.Classes_Sachem.ValidationAcces;

namespace sachem.Controllers
{
    public class ParametresController : Controller
    {
        private const int idCourriel = 1;
        private readonly SACHEMEntities db = new SACHEMEntities();

        [HttpGet]
        [ValidationAccesSuper]
        public ActionResult EditCourrier()
        {
            var courrier = db.Courriel.First();
            ViewBag.id_TypeCourriel = new SelectList(db.p_TypeCourriel, "id_TypeCourriel", "TypeCourriel");
            return View(courrier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourrier(Courriel courriel)
        {
            courriel.id_TypeCourriel = idCourriel;
            if (courriel.DateFin != null)
            {
                if((courriel.DateDebut - courriel.DateFin.Value).TotalDays > 0)
                    ModelState.AddModelError(string.Empty, Messages.C_005);
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
        [ValidationAccesSuper]
        public ActionResult EditContact()
        {
            var contact = db.p_Contact.First();
            contact.Telephone = SachemIdentite.RemettreTel(contact.Telephone);
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationAccesSuper]
        public ActionResult EditContact([Bind(Include = "id_Contact,Nom,Prenom,Courriel,Telephone,Poste,Facebook,SiteWeb,Local")] p_Contact contact)
        {
            ValiderContact(contact);

            if (ModelState.IsValid)
            {
                contact.Telephone = SachemIdentite.FormatTelephone(contact.Telephone);
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = string.Format(Messages.I_031());
                return View(contact);
            }
            return View(contact);
        }

        [NonAction]
        [ValidationAccesSuper]
        private void ListeSession(int session = 0)
        {
            var lSessions = db.Session.AsNoTracking().OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", session));
            ViewBag.id_Sess = slSession;
        }

        //Méthode qui envoie a la view Edit horaire la liste de toutes les horaires d'inscription ainsi que l'horaire de la session courrante
        [ValidationAccesSuper]
        public ActionResult EditHoraire(int session = 0)
        {
            var idZero = db.Session.OrderByDescending(y => y.id_Sess).FirstOrDefault();
            if (session == 0)
                session = idZero.id_Sess;
            ViewBag.idSess = session;
            var lhoraire = db.p_HoraireInscription.OrderByDescending(y => y.id_Sess).Where(x => x.id_Sess ==session).FirstOrDefault();
            ListeSession(session);
            ViewBag.idSessStable = idZero.id_Sess;         
            return View(lhoraire);
        }


        
        [HttpPost]
        [ValidationAccesSuper]
        public ActionResult EditHoraire([Bind(Include = "id_Sess, DateDebut, DateFin, HeureDebut, HeureFin")] p_HoraireInscription HI)
        {
            var id_Session = db.Session.AsNoTracking().OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison).FirstOrDefault();
            Session session = db.Session.Find(HI.id_Sess);
            if (id_Session.id_Sess == session.id_Sess)
            {
                //regarde l'année
                if (session.Annee != HI.DateDebut.Year || session.Annee != HI.DateFin.Year)
                {
                    ModelState.AddModelError(string.Empty, Messages.C_006);
                }

                //regarde si les dates sont bonnes
                if ((HI.DateFin - HI.DateDebut).TotalDays < 1)
                {
                    ModelState.AddModelError(string.Empty, Messages.C_005);
                }
                //Regarder si cest les bon id (ps : ca lest pas)
                switch (session.p_Saison.id_Saison)
                {
                    //Si hiver : de janvier inclus jusqua mai inclus (mois fin <= 5) pas besoin de verif la date de début
                    //car on est sur que c'est la bonne année et qu'elle est avant la date de fin
                    case 1:
                        if (HI.DateFin.Month > new DateTime(1, 5, 1).Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.C_006);
                        }
                        break;
                    //Si ete : de juin inclus jusqua aout inclus (si mois du début >= 6 et mois fin <= 8)
                    case 2:
                        if (new DateTime(1, 6, 1).Month > HI.DateDebut.Month || HI.DateFin.Month > new DateTime(1, 8, 1).Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.C_006);
                        }
                        break;
                    //si automne: de aout inclus jusqua decembre inclus (si mois du début >= 8 et mois fin <= 12)
                    //pas besoin de verif la date de fin car on est sur que c'est la bonne année et qu'elle est apres la date de début
                    case 3:
                        if (new DateTime(1, 8, 1).Month > HI.DateDebut.Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.C_006);
                        }
                        break;
                }

                if (ModelState.IsValid)
                {
                    var SessionSurHI = db.p_HoraireInscription.AsNoTracking().OrderBy(x => x.id_Sess).FirstOrDefault();
                    if (SessionSurHI.id_Sess != session.id_Sess)
                    {
                        db.Entry(HI).State = EntityState.Added;
                    }
                    else
                    {
                        db.Entry(HI).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                }
            }
            return RedirectToAction("EditHoraire");
        }

        [HttpGet]
        [ValidationAccesSuper]
        public ActionResult EditCollege(int? page)
        {
            var college = from c in db.p_College orderby c.College select c;

            var pageNumber = page ?? 1;

            return View(college.ToPagedList(pageNumber, 20));
        }

        [HttpPost]
        [ValidationAccesSuper]
        public ActionResult EditCollege(string nomCollege, int? id)
        {
            
            if (db.p_College.Any(r => r.id_College == id))
            {
                var college = db.p_College.Find(id);
                college.College = nomCollege;
                db.Entry(college).State = EntityState.Modified;
                db.SaveChanges();
            }
            var collegeretour = from c in db.p_College orderby c.College select c;
            return RedirectToAction("EditCollege");
        }

        [HttpPost]
        [ValidationAccesSuper]
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
            return RedirectToAction("EditCollege");
        }

        [HttpPost]
        [ValidationAccesSuper]
        public void DeleteCollege(int? id)
        {
            var college = db.p_College.Find(id);
            if(college != null)
            {
                db.p_College.Remove(college);
                db.SaveChanges();
            }
        }

        [NonAction]
        private void ValiderContact([Bind(Include = "id_Contact,Nom,Prenom,Courriel,Telephone,Poste,Facebook,SiteWeb,Local")]p_Contact contact)
        {
            if (!db.p_Contact.Any(r => r.id_Contact == contact.id_Contact))
                ModelState.AddModelError(string.Empty," ");
        }
    }
}
