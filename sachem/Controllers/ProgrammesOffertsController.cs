using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Controllers
{
    public class ProgrammesOffertsController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();
        // GET: ProgrammesOfferts
        public ActionResult Index()
        {
            var m = from c in db.ProgrammeEtude
                    orderby c.Code, c.Annee
                    select c;
            return View(m.ToList());
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
