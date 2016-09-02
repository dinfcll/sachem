using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Controllers
{
    public class ConsulterCoursController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();

        // GET: ConsulterCours
        public ActionResult Index()
        {
            return View(AfficherCoursAssignes());
            //db.Cours.ToList()
        }


        //Fonction pour afficher lescours assignés à l'utilisateur connecté
        [NonAction]
        private IEnumerable<Groupe> AfficherCoursAssignes()
        {
            var idSess = 0;
            var idPers = 5; // 1 = la seule résponsable, 2-9 = enseignants
            var idTypeUsage = 3; // 2 = enseignant, 3 = responsable
            var cours = from c in db.Cours select c;

            if (idTypeUsage == 2) //enseignant
            {
                Int32.TryParse(Request.Form["Session"], out idSess);
                ListeSession(idSess); //créer liste Session pour le dropdown

                var ens = from c in db.Groupe
                        where (c.id_Sess == idSess && c.id_Enseignant == idPers) || (idSess == 0 && c.id_Enseignant == idPers)
                        select c;

                ViewBag.IsEnseignant = true;

                return ens.ToList();
            }
            else //responsable
            {
                Int32.TryParse(Request.Form["Personne"], out idPers); //seuls les responsables le voient
                Int32.TryParse(Request.Form["Session"], out idSess);
                ListeSession(idSess); //créer liste Session pour le dropdown
                ListePersonne(idPers); //créer liste Enseignants pour le dropdown

                var resp = from c in db.Groupe
                    where c.id_Sess == (idSess == 0 ? c.id_Sess : idSess) && c.id_Enseignant == (idPers == 0 ? c.id_Enseignant : idPers)
                    select c;

                ViewBag.IsEnseignant = false;

                return resp.ToList();
            }
        }

        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListeSession(int Session = 0)
        {
            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", Session));

            ViewBag.Session = slSession;
        }

        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListePersonne(int idPersonne = 0)
        {
            //var lPersonne = db.Personne.AsNoTracking().OrderBy(p => p.Prenom).ThenBy(p => p.Nom);
            var lPersonne = from p in db.Personne
                            where p.id_TypeUsag == 2
                            select p;
            var slPersonne = new List<SelectListItem>();
            slPersonne.AddRange(new SelectList(lPersonne, "id_Pers", "PrenomNom", idPersonne));

            ViewBag.Personne = slPersonne;
        }
























        // GET: ConsulterCours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // GET: ConsulterCours/Create
        public ActionResult Create()
        {
            ViewBag.id_Sess = new SelectList(db.p_HoraireInscription, "id_Sess", "id_Sess");
            ViewBag.id_Saison = new SelectList(db.p_Saison, "id_Saison", "Saison");
            return View();
        }

        // POST: ConsulterCours/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Sess,id_Saison,Annee")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Session.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Sess = new SelectList(db.p_HoraireInscription, "id_Sess", "id_Sess", session.id_Sess);
            ViewBag.id_Saison = new SelectList(db.p_Saison, "id_Saison", "Saison", session.id_Saison);
            return View(session);
        }

        // GET: ConsulterCours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Sess = new SelectList(db.p_HoraireInscription, "id_Sess", "id_Sess", session.id_Sess);
            ViewBag.id_Saison = new SelectList(db.p_Saison, "id_Saison", "Saison", session.id_Saison);
            return View(session);
        }

        // POST: ConsulterCours/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Sess,id_Saison,Annee")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Sess = new SelectList(db.p_HoraireInscription, "id_Sess", "id_Sess", session.id_Sess);
            ViewBag.id_Saison = new SelectList(db.p_Saison, "id_Saison", "Saison", session.id_Saison);
            return View(session);
        }

        // GET: ConsulterCours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: ConsulterCours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Session.Find(id);
            db.Session.Remove(session);
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
