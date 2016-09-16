using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Controllers
{
    public class CoursSuiviController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();

        [NonAction]
        private void ListeCours(int Cours=0)
        {
            var lCours = db.Cours.AsNoTracking().OrderBy(c => c.Code);
            var slCours = new List<SelectListItem>();
            slCours.AddRange(new SelectList(lCours, "id_Cours", "CodeNom", Cours));
            ViewBag.id_Cours = slCours;
        }

        [NonAction]
        private void ListeCollege(int College=0)
        {
            var lCollege = db.p_College.AsNoTracking().OrderBy(n => n.College);
            var slCollege = new List<SelectListItem>();
            slCollege.AddRange(new SelectList(lCollege, "id_College", "College", College));

            ViewBag.id_College = slCollege;
        }

        [NonAction]
        private void ListeStatut(int Statut = 0)
        {
            var lStatut = db.p_StatutCours.AsNoTracking();
            var slStatut = new List<SelectListItem>();
            slStatut.AddRange(new SelectList(lStatut, "id_Statut", "Statut", Statut));

            ViewBag.id_Statut = slStatut;
        }

        [NonAction]
        private void ListeSession(int Session=0)
        {
            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", Session));

            ViewBag.id_Sess = slSession;
        }

        //Validation des champs cours et collège
        [NonAction]
        private void Valider([Bind(Include = "id_CoursReussi,id_Sess,id_Pers,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi, int i = 0)
        {
            //Validation seulement lors de l'ajout
            if (i == 1)
            {
                if (db.CoursSuivi.Any(r => r.id_Cours == coursSuivi.id_Cours && r.id_Pers == coursSuivi.id_Pers && r.id_Sess == coursSuivi.id_Sess && r.id_College == coursSuivi.id_College))
                    ModelState.AddModelError(string.Empty, Messages.I_036());
            }

            if (coursSuivi.id_Cours == null && coursSuivi.autre_Cours == string.Empty || coursSuivi.id_Cours != null && coursSuivi.autre_Cours != string.Empty)
                ModelState.AddModelError(string.Empty, Messages.C_009("Cours" , "Autre cours"));

            if (coursSuivi.id_College == null && coursSuivi.autre_College == string.Empty || coursSuivi.id_College != null && coursSuivi.autre_College != string.Empty)
                ModelState.AddModelError(string.Empty, Messages.C_009("Collège", "Autre collège"));
        }

        // GET: CoursSuivi
        public ActionResult Index()
        {
            return View(db.CoursSuivi.ToList());
        }

        // GET: CoursSuivi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursSuivi coursSuivi = db.CoursSuivi.Find(id);
            if (coursSuivi == null)
            {
                return HttpNotFound();
            }
            return View(coursSuivi);
        }

 

        // GET: CoursSuivi/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursSuivi cs = db.CoursSuivi.Where(r => r.id_Pers == id).FirstOrDefault();

            var vInscription = from d in db.Inscription
                               where d.id_Pers == cs.id_Pers
                               select d.id_Inscription;

            ViewBag.id_insc = vInscription.First();

            if (cs == null)
            {
                return HttpNotFound();
            }

            ListeCours();
            ListeCollege();
            ListeStatut();
            ListeSession();
            return View(cs);
        }

        // POST: CoursSuivi/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_CoursReussi,id_Sess,id_Pers,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi)
        //id_CoursReussi,id_Sess,id_Pers,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College
        //Cours,autre_Cours,College,autre_College,Session,Statut,resultat
        {
            ListeCours();
            ListeCollege();
            ListeStatut();
            ListeSession();


            Valider(coursSuivi, 1);

            var vInscription = from d in db.Inscription
                               where d.id_Pers == coursSuivi.id_Pers
                               select d.id_Inscription;

            if (ModelState.IsValid)
            {
                db.CoursSuivi.Add(coursSuivi);
                db.SaveChanges();
                return RedirectToAction("Details", "DossierEtudiant", new { id = vInscription.First() });
            }
            return View(coursSuivi);
        }

        // GET: CoursSuivi/Edit/5
        public ActionResult Edit(int? id, int? id2)
        {
            if (id == null || id2 == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            CoursSuivi cs = db.CoursSuivi.Where(r => r.id_Pers == id2 && r.id_CoursReussi == id).FirstOrDefault();

            var vInscription = from d in db.Inscription
                               where d.id_Pers == cs.id_Pers
                               select d.id_Inscription;

            ViewBag.id_insc = vInscription.First();


            if (cs == null)
                return HttpNotFound();

            if (cs.id_Cours == null)
                ListeCours();
            else
                ListeCours(cs.id_Cours.Value);

            if (cs.id_College == null)
                ListeCollege();
            else
                ListeCollege(cs.id_College.Value);
            ListeStatut(cs.id_Statut.Value);
            ListeSession(cs.id_Sess.Value);
            return View(cs);
        }

        // POST: CoursSuivi/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_CoursReussi,id_Sess,id_Pers,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi)
        {
            if (coursSuivi.id_Cours == null)
                ListeCours();
            else
                ListeCours(coursSuivi.id_Cours.Value);

            if (coursSuivi.id_College == null)
                ListeCollege();
            else
                ListeCollege(coursSuivi.id_College.Value);

            ListeStatut(coursSuivi.id_Statut.Value);
            ListeSession(coursSuivi.id_Sess.Value);

            Valider(coursSuivi);

            var vInscription = from d in db.Inscription
                              where d.id_Pers == coursSuivi.id_Pers
                              select d.id_Inscription;

            if (ModelState.IsValid)
            {
                db.Entry(coursSuivi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "DossierEtudiant", new { id = vInscription.First() });
            }
            return View(coursSuivi);
        }

        //id étant id_CoursReussi et id2 étant id_Pers
        // GET: CoursSuivi/Delete/5
        public ActionResult Delete(int? id, int? id2)
        {            
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CoursSuivi cs = db.CoursSuivi.Where(r => r.id_Pers == id2 && r.id_CoursReussi == id).FirstOrDefault();

            if (cs == null)
            {
                return HttpNotFound();
            }

            var vInscription = from d in db.Inscription
                               where d.id_Pers == cs.id_Pers
                               select d.id_Inscription;

            ViewBag.id_insc = vInscription.First();

            return View(cs);
        }

        // POST: CoursSuivi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoursSuivi coursSuivi = db.CoursSuivi.Find(id);

            var vInscription = from d in db.Inscription
                               where d.id_Pers == coursSuivi.id_Pers
                               select d.id_Inscription;

            db.CoursSuivi.Remove(coursSuivi);
            db.SaveChanges();
            return RedirectToAction("Details", "DossierEtudiant", new { id = vInscription.First() });
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
