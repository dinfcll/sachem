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
                programmesEtude =
                    programmesEtude.Where(c => c.Code.Contains(recherche) || c.NomProg.Contains(recherche)) as
                        IOrderedQueryable<ProgrammeEtude>;
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
        public ActionResult Create([Bind(Include = "id_ProgEtu,Code,NomProg,Annee,Actif")] ProgrammeEtude programme)
        {
          
                Valider(programme);
            if (ModelState.IsValid)
            {
                
                db.ProgrammeEtude.Add(programme);
                db.SaveChanges();

                TempData["Success"] = string.Format(Messages.I_006(programme.NomProg));
                return RedirectToAction("Index");

            }
            return View(programme);


        }

        // GET: ProgrammesOfferts/Edit/5
        // POST: ProgrammesOfferts/Edit/5
       
        public ActionResult Edit(int? id)
        {

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var programme = db.ProgrammeEtude.Find(id);

            if (programme == null)
                return HttpNotFound();

            if (programme.Code.Any())
                ViewBag.DisEns = "True";
            
            return View(programme);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id_ProgEtu,Code,NomProg,Annee,Actif")] ProgrammeEtude programme)
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
        public ActionResult Supprimer(int id)
        {
            var programme = db.ProgrammeEtude.Single(r => r.id_ProgEtu == id);
            return View(programme);
        }

        // POST: ProgrammesOfferts/Delete/5
        [HttpPost]
        public ActionResult Supprimer(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// methode pour valider le programme
        /// </summary>
        /// <param name="programme"></param>
        private void Valider([Bind(Include = "id_ProgEtu,Code,NomProg,Annee,Actif")] ProgrammeEtude programme)
        {
            if (db.ProgrammeEtude.Any(r => r.Code == programme.Code && r.id_ProgEtu != programme.id_ProgEtu))
                ModelState.AddModelError(string.Empty, Messages.I_002(programme.Code));
        }
    }
}
