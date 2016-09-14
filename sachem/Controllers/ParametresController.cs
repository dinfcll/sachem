using System;
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

        //trouver un moyen de faire fonctionner la vue pour qu'elle affiche bien [Été 2016] en dropdownlist

        public ActionResult EditHoraire()
        {
            //peut-etre trouver un moyen de tout faire le formatage dans la vue ?
            string[] valeurHeure = new string[5];
            var horaire = db.p_HoraireInscription.First();
            var session = db.Session.Single(r => r.id_Sess == horaire.id_Sess);
            var saison = db.p_Saison.Single(r => r.id_Saison == session.id_Saison);
            valeurHeure[1] = horaire.DateDebut.Date.ToString("yyyy/MM/dd");
            valeurHeure[2] = horaire.DateFin.Date.ToString("yyyy/MM/dd");
            valeurHeure[3] = horaire.HeureDebut.ToString(@"hh\:mm");
            valeurHeure[4] = horaire.HeureFin.ToString(@"hh\:mm");
            
            return View(Tuple.Create(horaire,session,saison,valeurHeure));
        }
        //A VERIF
        [HttpPost]
        public ActionResult EditHoraire([Bind(Include = "id_Sess,DateDebut,DateFin,HeureDebut,HeureFin")] p_HoraireInscription horaire, Session session, p_Saison saison)
        {
            if (!(session.Annee == horaire.DateFin.Year) || !(session.Annee == horaire.DateDebut.Year))
            {
                //ajouter le message d'erreur comme quoi il faut que les années des dates entrées 
                //soit les mêmes que l'année de la session en cours
                ModelState.AddModelError(string.Empty, "A ajouter");
            }

            if((horaire.DateFin - horaire.DateDebut).TotalDays >= 1)
            {
                //ajouter le messsage d'erreur signalant qu'il est impossible d'avoir la date de fin avec la date de début
                ModelState.AddModelError(string.Empty,"A ajouter");
            }
            
            switch (saison.id_Saison)
                {
                    //Si hiver : de janvier inclus jusqua mai inclus (mois fin <= 5) pas besoin de verif la date de début
                    //car on est sur que c'est la bonne année et qu'elle est avant la date de fin
                    case 0:
                        if (horaire.DateFin.Month > new DateTime(1,5,1).Month)
                        {
                            //Erreur comme quoi les dates indiquées ne sont pas dans la bonne session.
                        }
                        break;
                    //Si ete : de juin inclus jusqua aout inclus (si mois du début >= 6 et mois fin <= 8)
                    case 1:
                        if (new DateTime(1,6,1).Month > horaire.DateDebut.Month || horaire.DateFin.Month > new DateTime(1,8,1).Month)
                        {
                                //Erreur comme quoi les dates indiquées ne sont pas dans la bonne session.
                        }
                        break;
                    //si automne: de aout inclus jusqua decembre inclus (si mois du début >= 8 et mois fin <= 12)
                    //pas besoin de verif la date de fin car on est sur que c'est la bonne année et qu'elle est apres la date de début
                    case 2:
                        if (new DateTime(1, 8, 1).Month > horaire.DateDebut.Month)
                        {
                            //Erreur comme quoi les dates indiquées ne sont pas dans la bonne session.
                        }
                    break;
                }
            if(horaire.HeureFin < horaire.HeureDebut)
            {
                //ajouter le messsage d'erreur signalant qu'il est impossible d'avoir l'heure de fin avant l'heure de début
                ModelState.AddModelError(string.Empty, "A ajouter");
            }

            //si on rentre ici, cela veut dire que tout est en ordre et que l'horaire peut être modifier
            if (ModelState.IsValid)
            {
                db.Entry(horaire).State = EntityState.Modified;
                db.SaveChanges();
                //ajouter les messages comme quoi tout a fonctionner, sinon je pleures
            }
            return View();
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
