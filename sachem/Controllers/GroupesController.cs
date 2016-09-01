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
    public class GroupesController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();

        // GET: Groupes
        public ActionResult Index()
        {
            var groupe = db.Groupe.Include(g => g.Cours).Include(g => g.Personne).Include(g => g.Session);
            return View(groupe.ToList());
        }

        // GET: Groupes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupe.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            return View(groupe);
        }

        // GET: Groupes/Create
        public ActionResult Create()
        {
            ViewBag.id_Cours = new SelectList(db.Cours, "id_Cours", "CodeNom");
            ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "NomPrenom");
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "NomSession");
            return View();
        }

        // POST: Groupes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            if (ModelState.IsValid)
            {
                db.Groupe.Add(groupe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Cours = new SelectList(db.Cours, "id_Cours", "Code", groupe.id_Cours);
            ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", groupe.id_Enseignant);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", groupe.id_Sess);
            
            
            return View(groupe);
        }

        // GET: Groupes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupe.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Cours = new SelectList(db.Cours, "id_Cours", "Code", groupe.id_Cours);
            ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", groupe.id_Enseignant);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", groupe.id_Sess);
            return View(groupe);
        }

        // POST: Groupes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Cours = new SelectList(db.Cours, "id_Cours", "Code", groupe.id_Cours);
            ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", groupe.id_Enseignant);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", groupe.id_Sess);
            return View(groupe);
        }

        // GET: Groupes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupe.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            return View(groupe);
        }

        // POST: Groupes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Groupe groupe = db.Groupe.Find(id);
            db.Groupe.Remove(groupe);
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
