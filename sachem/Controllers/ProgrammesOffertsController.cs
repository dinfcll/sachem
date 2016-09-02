using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using System.Net;
using System.Data.Entity;

namespace sachem.Controllers
{
    public class ProgrammesOffertsController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();
        // GET: ProgrammesOfferts
        public ActionResult Index(string recherche, int? page)
        {
            if (recherche != null)
            {
                page = 1;
            }

            var programmesEtude = from c in db.ProgrammeEtude
                orderby c.Code, c.Annee
                select c;
            if (!String.IsNullOrEmpty(recherche))
            {
                programmesEtude = programmesEtude.Where(c => c.Code.Contains(recherche) || c.NomProg.Contains(recherche)) as IOrderedQueryable<ProgrammeEtude>;
            }

            int numeroPage = (page ?? 1);
            return View(programmesEtude.ToPagedList(numeroPage, 16));
        }
        
        // GET: ProgrammesOfferts/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProgrammesOfferts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProgrammesOfferts/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // POST: ProgrammesOfferts/Edit/5
        
        public ActionResult Edit(int? id)
        {

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var programme = db.ProgrammeEtude.Find(id);

            if (programme == null)
                return HttpNotFound();

            if (programme.NomProg.Any())
                ViewBag.DisEns = "True";
            
            return View(programme);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id_ProgEtu,Code,NomProg,Annee,Actif")] ProgrammeEtude programme, int? page)
        {
            Valider(programme);

            if (ModelState.IsValid)
            {
                db.Entry(programme).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = string.Format(Messages.I_004(programme.NomProg));
                return RedirectToAction("Index");
            }
            return View(programme);
        }

        // GET: ProgrammesOfferts/Delete/5
        public ActionResult Supprimer(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var programme = db.ProgrammeEtude.Find(id);

            if (programme == null)
                return HttpNotFound();

            return View(programme);
        }

        // POST: ProgrammesOfferts/Delete/5
        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? page)
        {
            var pageNumber = page ?? 1;
            if (db.ProgrammeEtude.Any(g => g.id_ProgEtu == id))
            {
                ModelState.AddModelError(string.Empty, Messages.I_001());
            }

            if (ModelState.IsValid)
            {
                var programme = db.ProgrammeEtude.Find(id);
                db.ProgrammeEtude.Remove(programme);
                db.SaveChanges();
                ViewBag.Success = string.Format(Messages.I_008(programme.NomProg));
            }
            return RedirectToAction("Index");
        }
        private void Valider([Bind(Include = "id_ProgEtu,Code,NomProg,Annee,Actif")] ProgrammeEtude programme)
        {
            if (db.Cours.Any(r => r.Code == programme.Code && r.id_Cours != programme.id_ProgEtu))
                ModelState.AddModelError(string.Empty, Messages.I_002(programme.Code));
        }
    }
}
