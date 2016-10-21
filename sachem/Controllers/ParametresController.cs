using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        [NonAction]
        private IEnumerable<Session> ObtenirSession(int session = 0)
        {
            var lstSession = from s in db.Session
                                where (db.p_HoraireInscription.Any(j => (j.id_Sess == s.id_Sess || session == 0)))
                                orderby s.NomSession
                                select s;
            return lstSession.ToList();
        }

        [NonAction]
        private void ListeSession(int session)
        {
            ViewBag.Session = new SelectList(ObtenirSession(session), "id_Sess", "NomSession", session);
        }

        //trouver un moyen de faire fonctionner la vue pour qu'elle affiche bien [Été 2016] en dropdownlist
        // MODULO ???!?!?
        public ActionResult EditHoraire()
        {
            //var horaire = db.p_HoraireInscription.First();
            //List<SelectListItem> horaireList = new List<SelectListItem>();
            //foreach (var item in db.p_HoraireInscription)
            //{
            //    var sess = db.Session.Find(item.id_Sess);
            //    horaireList.Add
            //    (
            //        new SelectListItem {Text = item.id_Sess.ToString(), Value = sess.NomSession}
            //    );
            //}

            ListeSession(0);
            // return View(Tuple.Create(horaireList,horaire));
            return View();
        }


        //A VERIF
        [HttpPost]
        public ActionResult EditHoraire([Bind(Prefix = "Item2")] p_HoraireInscription nouvelHoraire)
        {
            
            var session = db.Session.Find(nouvelHoraire.id_Sess);
            var saison = db.p_Saison.Find(nouvelHoraire.Session.id_Saison);
            ModelState.Clear();
            if (session.Annee != nouvelHoraire.DateFin.Year || session.Annee != nouvelHoraire.DateDebut.Year)
            {
                ModelState.AddModelError(string.Empty, Messages.C_006);
            }

            if((nouvelHoraire.DateFin - nouvelHoraire.DateDebut).TotalDays < 1)
            {
                ModelState.AddModelError(string.Empty, Messages.C_005);
            }
            //Regarder si cest les bon id (ps : ca lest pas)
            switch (saison.id_Saison)
                {
                    //Si hiver : de janvier inclus jusqua mai inclus (mois fin <= 5) pas besoin de verif la date de début
                    //car on est sur que c'est la bonne année et qu'elle est avant la date de fin
                    case 1:
                        if (nouvelHoraire.DateFin.Month > new DateTime(1,5,1).Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.C_006);
                        }
                        break;
                    //Si ete : de juin inclus jusqua aout inclus (si mois du début >= 6 et mois fin <= 8)
                    case 2:
                        if (new DateTime(1,6,1).Month > nouvelHoraire.DateDebut.Month || nouvelHoraire.DateFin.Month > new DateTime(1,8,1).Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.C_006);
                        }   
                        break;
                    //si automne: de aout inclus jusqua decembre inclus (si mois du début >= 8 et mois fin <= 12)
                    //pas besoin de verif la date de fin car on est sur que c'est la bonne année et qu'elle est apres la date de début
                    case 3:
                        if (new DateTime(1, 8, 1).Month > nouvelHoraire.DateDebut.Month)
                        {
                            ModelState.AddModelError(string.Empty, Messages.C_006);
                        }
                    break;
                }

            
            if (ModelState.IsValid)
            {
                db.Entry(nouvelHoraire).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("EditHoraire");
            }
            return RedirectToAction("EditHoraire");
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
