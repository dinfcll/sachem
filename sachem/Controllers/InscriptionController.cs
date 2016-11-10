using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachem.Classes_Sachem;
using sachem.Models;

namespace sachem.Controllers
{
   
    public class InscriptionController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();

        //[ValidationAcces.ValidationAccesInscription]
        // GET: Inscription
        public ActionResult Index()
        {
            ViewBag.TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            /*var inscription = from c in db.Inscription
                              select c;
            var liste = Tuple.Create(inscription, "string");*/
            //return View(liste);
            return View();
        }

        [HttpPost]
        public ActionResult Index(Horaire horaire)
        {
            ViewBag.TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            var test = horaire;
            if (horaire != null)
            {
                return Json("Success");
            }
            return View();
        }

        /*private string NoAJour(int id)
        {
            switch (id % 5)
            {
                case 0:
                    return "Vendredi";
                    break;
                case 1:

                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:

                    break;

            }
        }*/
        // GET: Inscription/Delete/5
        //NOTE: Penser à Wiper les inscriptions à chaque fin de session. Constante?
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inscription/Delete/5
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
