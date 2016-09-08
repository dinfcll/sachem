using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using sachem.Models;

namespace sachem.Controllers
{
    public class GroupesController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();

        // GET: Groupes
        public ActionResult Index(int? page)
        {
            RegisterViewbags();
            var pageNumber = page ?? 1;
            return View(Rechercher().ToPagedList(pageNumber, 20));
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
            var ens = from c in db.Personne where c.id_TypeUsag == 2 select c;
            ViewBag.id_Enseignant = new SelectList(ens, "id_Pers", "NomPrenom");
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

        [NonAction]
        private IEnumerable<Groupe> Rechercher()
        {
            IQueryable<Groupe> groupes;
            int idSess = 0, idEns = 0, idCours = 0;
            if (Request.RequestType == "POST")
            {
                int.TryParse(Request.Form["Sessions"], out idSess);
                int.TryParse(Request.Form["Enseignants"], out idEns);
                int.TryParse(Request["Cours"], out idCours);
                
                RegisterViewbags();

                groupes = from d in db.Groupe where d.id_Cours == (idCours == 0 ? d.id_Cours : idCours) && d.id_Enseignant == (idEns == 0 ? d.id_Enseignant : idEns) && d.id_Sess == (idSess == 0 ? d.id_Sess : idSess) orderby d.Session.p_Saison.Saison, d.Session.Annee, d.Cours.Code, d.Cours.Nom, d.NoGroupe select d;
            }
            else
            {
                groupes = from d in db.Groupe orderby d.Session.p_Saison.Saison, d.Session.Annee, d.Cours.Code, d.Cours.Nom, d.NoGroupe select d;
            }
            foreach (var n in groupes)
            {
                n.nbPersonne = (from c in db.GroupeEtudiant where c.id_Groupe == n.id_Groupe select c).Count();
            }
            return groupes.ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [NonAction]
        private void RegisterViewbags()
        {
            int sess = db.Session.Max(s => s.id_Sess);
            //var last = from b in db.Session orderby b.id_Sess descending select b;
            ViewBag.Sessions = new SelectList(db.Session, "id_Sess", "NomSession", sess);
            var ens = from c in db.Personne where c.id_TypeUsag == 2 select c;
            ViewBag.Enseignants = new SelectList(ens, "id_Pers", "NomPrenom");
            ViewBag.Cours = new SelectList(db.Cours, "id_Cours", "CodeNom");
        }
    }
}
