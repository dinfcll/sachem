using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using PagedList;

namespace sachem.Controllers
{
    public class ProgrammesOffertsController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();
        // GET: ProgrammesOfferts
        public ActionResult Index(string filtreOrdre, string recherche, int? page)
        {
            

            if (recherche != null)
            {
                page = 1;
            }
            else
            {
                recherche = filtreOrdre;
            }

            ViewBag.Filtre = recherche;
            var programmesEtude = from c in db.ProgrammeEtude
                    orderby c.Code, c.Annee
                    select c;
            if (!String.IsNullOrEmpty(recherche))
            {
                programmesEtude = programmesEtude.Where(c => c.Code.Contains(recherche) || c.NomProg.Contains(recherche)) as IOrderedQueryable<ProgrammeEtude>;
            }

            int numeroPage = (page ?? 1);
            return View(programmesEtude.ToPagedList(numeroPage, 12));
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

        // GET: ProgrammesOfferts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProgrammesOfferts/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProgrammesOfferts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProgrammesOfferts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
    }
}
