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
    public class JumelageController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();

        // GET: Jumelage
        public ActionResult Index()
        {
            var jumelage = db.Jumelage.Include(j => j.Inscription).Include(j => j.Inscription1).Include(j => j.p_Jour).Include(j => j.Personne).Include(j => j.Session);
            return View(jumelage.ToList());
        }

        // GET: Jumelage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jumelage jumelage = db.Jumelage.Find(id);
            if (jumelage == null)
            {
                return HttpNotFound();
            }
            return View(jumelage);
        }

        // GET: Jumelage/Create
        public ActionResult Create()
        {
            ViewBag.id_InscEleve = new SelectList(db.Inscription, "id_Inscription", "NoteSup");
            ViewBag.id_InscrTuteur = new SelectList(db.Inscription, "id_Inscription", "NoteSup");
            ViewBag.id_Jour = new SelectList(db.p_Jour, "id_Jour", "Jour");
            ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom");
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess");
            return View();
        }

        // POST: Jumelage/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Jumelage,id_InscEleve,id_InscrTuteur,id_Sess,id_Enseignant,id_Jour,DateDebut,DateFin,HeureDebut,HeureFin")] Jumelage jumelage)
        {
            if (ModelState.IsValid)
            {
                db.Jumelage.Add(jumelage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_InscEleve = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscEleve);
            ViewBag.id_InscrTuteur = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscrTuteur);
            ViewBag.id_Jour = new SelectList(db.p_Jour, "id_Jour", "Jour", jumelage.id_Jour);
            ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", jumelage.id_Enseignant);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", jumelage.id_Sess);
            return View(jumelage);
        }

        // GET: Jumelage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jumelage jumelage = db.Jumelage.Find(id);
            if (jumelage == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_InscEleve = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscEleve);
            ViewBag.id_InscrTuteur = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscrTuteur);
            ViewBag.id_Jour = new SelectList(db.p_Jour, "id_Jour", "Jour", jumelage.id_Jour);
            ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", jumelage.id_Enseignant);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", jumelage.id_Sess);
            return View(jumelage);
        }

        // POST: Jumelage/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Jumelage,id_InscEleve,id_InscrTuteur,id_Sess,id_Enseignant,id_Jour,DateDebut,DateFin,HeureDebut,HeureFin")] Jumelage jumelage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jumelage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_InscEleve = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscEleve);
            ViewBag.id_InscrTuteur = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscrTuteur);
            ViewBag.id_Jour = new SelectList(db.p_Jour, "id_Jour", "Jour", jumelage.id_Jour);
            ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", jumelage.id_Enseignant);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", jumelage.id_Sess);
            return View(jumelage);
        }

        // GET: Jumelage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jumelage jumelage = db.Jumelage.Find(id);
            if (jumelage == null)
            {
                return HttpNotFound();
            }
            return View(jumelage);
        }

        // POST: Jumelage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jumelage jumelage = db.Jumelage.Find(id);
            db.Jumelage.Remove(jumelage);
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
