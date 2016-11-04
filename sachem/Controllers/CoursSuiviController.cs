using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Controllers
{
    public class CoursSuiviController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();

        [NonAction]
        private void ListeCours(int cours = 0)
        {
            var lCours = db.Cours.AsNoTracking().OrderBy(c => c.Code);
            var slCours = new List<SelectListItem>();
            slCours.AddRange(new SelectList(lCours, "id_Cours", "CodeNom", cours));
            ViewBag.id_Cours = slCours;
        }

        [NonAction]
        private void ListeCollege(int college = 0)
        {
            var lCollege = db.p_College.AsNoTracking().OrderBy(n => n.College);
            var slCollege = new List<SelectListItem>();
            slCollege.AddRange(new SelectList(lCollege, "id_College", "College", college));

            ViewBag.id_College = slCollege;
        }

        [NonAction]
        private void ListeStatut(int statut = 0)
        {
            var lStatut = db.p_StatutCours.AsNoTracking();
            var slStatut = new List<SelectListItem>();
            slStatut.AddRange(new SelectList(lStatut, "id_Statut", "Statut", statut));

            ViewBag.id_Statut = slStatut;
        }

        [NonAction]
        private void ListeSession(int session = 0)
        {
            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", session));

            ViewBag.id_Sess = slSession;
        }

        //Validation des champs cours et collège
        [NonAction]
        private void Valider([Bind(Include = "id_CoursReussi,id_Sess,id_Pers,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi, bool verif = false)
        {
            //Validation seulement lors de l'ajout
            if (db.CoursSuivi.Any(r => r.id_Cours == coursSuivi.id_Cours && r.id_Pers == coursSuivi.id_Pers && r.id_Sess == coursSuivi.id_Sess && r.id_College == coursSuivi.id_College) && verif)
                ModelState.AddModelError(string.Empty, Messages.I_036());

            if (coursSuivi.id_Cours == null && coursSuivi.autre_Cours == string.Empty || coursSuivi.id_Cours != null && coursSuivi.autre_Cours != string.Empty)
                ModelState.AddModelError(string.Empty, Messages.C_009("Cours" , "Autre cours"));

            if (coursSuivi.id_College == null && coursSuivi.autre_College == string.Empty || coursSuivi.id_College != null && coursSuivi.autre_College != string.Empty)
                ModelState.AddModelError(string.Empty, Messages.C_009("Collège", "Autre collège"));

            //Verif si resultat nécéssaire et présent
            if ((coursSuivi.id_Statut == null || coursSuivi.id_Statut == 1) && coursSuivi.resultat == null)
                ModelState.AddModelError(string.Empty, Messages.C_010);
        }

        // GET: CoursSuivi/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursSuivi cs = db.CoursSuivi.FirstOrDefault(r => r.id_Pers == id);

            var vInscription = from d in db.Inscription
                               where d.id_Pers == id
                               select d.id_Inscription;

            ViewBag.id_insc = vInscription.First();
            ViewBag.idPers = id;
            ViewBag.Resultat = 1;

            ListeCours();
            ListeCollege();
            ListeStatut();
            ListeSession();
            return View(cs);
        }

        // POST: CoursSuivi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_CoursReussi,id_Sess,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi, int? id)
        {
            ListeCours();
            ListeCollege();
            ListeStatut();
            ListeSession();

            coursSuivi.id_Pers = (int)id;
            ViewBag.idPers = coursSuivi.id_Pers;

            Valider(coursSuivi, true);
            
            var vInscription = from d in db.Inscription
                               where d.id_Pers == coursSuivi.id_Pers
                               select d.id_Inscription;
            ViewBag.id_insc = vInscription.First();

            if (ModelState.IsValid)
            {
                db.CoursSuivi.Add(coursSuivi);
                db.SaveChanges();
                return RedirectToAction("Details", "DossierEtudiant", new { id = vInscription.First() });
            }
            return View(coursSuivi);
        }

        // GET: CoursSuivi/Edit/5
        public ActionResult Edit(int? coursReussi, int? personne)
        {
            if (coursReussi == null || personne == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            CoursSuivi cs = db.CoursSuivi.FirstOrDefault(r => r.id_Pers == personne && r.id_CoursReussi == coursReussi);

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

            if (cs.id_Statut == null)
                ListeStatut();
            else
                ListeStatut(cs.id_Statut.Value);

            if (cs.id_Sess == null)
                ListeSession();
            else
                ListeSession(cs.id_Sess.Value);

            ViewBag.Resultat = 0;

            return View(cs);
        }

        // POST: CoursSuivi/Edit/5
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

        // GET: CoursSuivi/Delete/5
        public ActionResult Delete(int? coursReussi, int? personne)
        {            
            if (coursReussi == null || personne == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CoursSuivi cs = db.CoursSuivi.FirstOrDefault(r => r.id_Pers == personne && r.id_CoursReussi == coursReussi);

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
