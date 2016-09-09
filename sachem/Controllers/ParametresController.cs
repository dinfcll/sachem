using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Controllers
{
    public class ParametresController : Controller
    {

        private readonly SACHEMEntities db = new SACHEMEntities();
        // GET: Parametres
        public ActionResult Index()
        {
            return View();
        }

        // GET: Parametres/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Parametres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parametres/Create
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

        // GET: Parametres/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Parametres/Edit/5
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

        public ActionResult EditHoraire()
        {
            string[] valeurHeure = new string[5];
            var horaire = db.p_HoraireInscription.First();
            var session = db.Session.Single(r => r.id_Sess == horaire.id_Sess);
            var saison = db.p_Saison.Single(r => r.id_Saison == session.id_Saison);
            valeurHeure[0] = saison.Saison + " " + session.Annee;
            valeurHeure[1] = horaire.DateDebut.Date.ToString("yyyy/MM/dd");
            valeurHeure[2] = horaire.DateFin.Date.ToString("yyyy/MM/dd");
            valeurHeure[3] = horaire.HeureDebut.ToString(@"hh\:mm");
            valeurHeure[4] = horaire.HeureFin.ToString(@"hh\:mm");
            
            return View(Tuple.Create(horaire,valeurHeure));
        }
        // GET: Parametres/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Parametres/Delete/5
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
