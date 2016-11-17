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
        [ValidateAntiForgeryToken]
        public ActionResult Index(int pa)
        {
            ViewBag.TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");

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
        public ActionResult Tuteur()
        {
            listeCours();
            listeCollege();
            return View();
        }
        public ActionResult TBenevole()
        {
            listeCours();
            listeCollege();
            return View();
        }
        [HttpPost]
        public void listeCours()
        {
            var lstCrs = from c in db.Cours orderby c.Nom select c;
            var slCrs = new List<SelectListItem>();
            slCrs.AddRange(new SelectList(lstCrs, "id_Cours","CodeNom"));
            var sltemp = new List<SelectListItem>(slCrs);
            ViewBag.lstCours1 = sltemp;
            ViewBag.lstCours4 = sltemp;
            slCrs.RemoveAt(0);
            sltemp = new List<SelectListItem>(slCrs);
            ViewBag.lstCours2 = sltemp;
            slCrs.RemoveAt(0);
            sltemp = new List<SelectListItem>(slCrs);
            ViewBag.lstCours3 = sltemp;
        }
        [HttpPost]
        public void listeCollege()
        {
            var lstCol = from c in db.p_College orderby c.College select c;
            var slCol = new List<SelectListItem>();
            slCol.AddRange(new SelectList(lstCol, "id_College", "College"));
            ViewBag.lstCollege = slCol;
        }

        [HttpPost]
        public ActionResult getLigneCours()
        {
            listeCours();
            listeCollege();
            return PartialView("_LigneCoursReussi");
        }
        [HttpPost]
        public void Poursuivre()
        {



        }
    }
}
