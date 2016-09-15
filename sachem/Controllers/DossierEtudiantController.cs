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
    public class DossierEtudiantController : Controller
    {
        //test
        private SACHEMEntities db = new SACHEMEntities();

        // GET: DossierEtudiant
        public ActionResult Index()
        {
            var inscription = db.Inscription.Include(i => i.p_StatutInscription).Include(i => i.p_TypeInscription).Include(i => i.Personne).Include(i => i.Session);
            return View(inscription.ToList());
        }

        // GET: DossierEtudiant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = db.Inscription.Find(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            return View(inscription);
        }

        // GET: DossierEtudiant/Create
        public ActionResult Create()
        {
            ViewBag.id_Statut = new SelectList(db.p_StatutInscription, "id_Statut", "Statut");
            ViewBag.id_TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            ViewBag.id_Pers = new SelectList(db.Personne, "id_Pers", "Nom");
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess");
            return View();
        }

        // POST: DossierEtudiant/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Inscription,id_Sess,id_Pers,id_Statut,id_TypeInscription,TransmettreInfoTuteur,NoteSup,ContratEngagement,BonEchange,DateInscription")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
                db.Inscription.Add(inscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Statut = new SelectList(db.p_StatutInscription, "id_Statut", "Statut", inscription.id_Statut);
            ViewBag.id_TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription", inscription.id_TypeInscription);
            ViewBag.id_Pers = new SelectList(db.Personne, "id_Pers", "Nom", inscription.id_Pers);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", inscription.id_Sess);
            return View(inscription);
        }

        // GET: DossierEtudiant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = db.Inscription.Find(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }

            var vCoursSuivi = from d in db.CoursSuivi
                           where d.id_Pers == inscription.id_Pers
                           select d;
            CoursSuivi dbCoursSuivi = db.CoursSuivi.Find(inscription.id_Pers);

            ViewBag.id_Statut = new SelectList(db.p_StatutInscription, "id_Statut", "Statut", inscription.id_Statut);
            ViewBag.id_TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription", inscription.id_TypeInscription);
            ViewBag.id_Pers = new SelectList(db.Personne, "id_Pers", "Nom", inscription.id_Pers);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", inscription.id_Sess);
            return View(Tuple.Create(inscription, vCoursSuivi.AsEnumerable()));
        }

        // POST: DossierEtudiant/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Inscription,id_Sess,id_Pers,id_Statut,id_TypeInscription,TransmettreInfoTuteur,NoteSup,ContratEngagement,BonEchange,DateInscription")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Statut = new SelectList(db.p_StatutInscription, "id_Statut", "Statut", inscription.id_Statut);
            ViewBag.id_TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription", inscription.id_TypeInscription);
            ViewBag.id_Pers = new SelectList(db.Personne, "id_Pers", "Nom", inscription.id_Pers);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", inscription.id_Sess);
            return View(inscription);
        }

        // GET: DossierEtudiant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = db.Inscription.Find(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            return View(inscription);
        }

        // POST: DossierEtudiant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inscription inscription = db.Inscription.Find(id);
            db.Inscription.Remove(inscription);
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
