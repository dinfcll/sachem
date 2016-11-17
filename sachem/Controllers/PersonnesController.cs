using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    public class PersonnesController : Controller
    {
        private readonly IDataRepository dataRepository;

        public PersonnesController()
        {
            dataRepository = new BdRepository();
        }

        public PersonnesController(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        // GET: Personnes
        public ActionResult Index()
        {
            var personne = dataRepository.IndexPersonne();
            return View(personne.ToList());
        }

        // GET: Personnes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = dataRepository.FindPersonne(id.Value);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // GET: Personnes/Create
        public ActionResult Create()
        {
            ViewBag.id_Sexe = new SelectList(dataRepository.AllSexe(), "id_Sexe", "Sexe");
            ViewBag.id_TypeUsag = new SelectList(dataRepository.AllTypeUsag(), "id_TypeUsag", "TypeUsag");
            return View();
        }

        // POST: Personnes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule,MP,Courriel,Telephone,DateNais,Actif")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                dataRepository.AddPersonne(personne);
                return RedirectToAction("Index");
            }

            ViewBag.id_Sexe = new SelectList(dataRepository.AllSexe(), "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(dataRepository.AllTypeUsag(), "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // GET: Personnes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = dataRepository.FindPersonne(id.Value);
            if (personne == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Sexe = new SelectList(dataRepository.AllSexe(), "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(dataRepository.AllTypeUsag(), "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // POST: Personnes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule,MP,Courriel,Telephone,DateNais,Actif")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                dataRepository.DeclareModifiedPers(personne);
                return RedirectToAction("Index");
            }
            ViewBag.id_Sexe = new SelectList(dataRepository.AllSexe(), "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(dataRepository.AllTypeUsag(), "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // GET: Personnes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = dataRepository.FindPersonne(id.Value);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // POST: Personnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personne personne = dataRepository.FindPersonne(id);
            dataRepository.RemovePersonne(personne);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dataRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
