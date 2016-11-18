using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachem.Classes_Sachem;
using sachem.Models;
using System.Text.RegularExpressions;

namespace sachem.Controllers
{
   
    public class InscriptionController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();
        private const string MSG_ERREUR_CONSECUTIF = "Erreur: vous devez avoir une plage horaire contenant 2 heures consécutives.";
        private const string MSG_ERREUR_LENGTH = "Cochez au moins 2 heures.";
        private const string MSG_ERREUR_REMPLIR = "Veuillez remplir le formulaire de disponibilités.";
        //[ValidationAcces.ValidationAccesInscription]
        // GET: Inscription
        public ActionResult Index()
        {
            ViewBag.TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            return View();
        }

        [HttpPost]
        public ActionResult Index(string[] values)
        {
            ViewBag.TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            if (values != null)
            {
                int longueurTab = values.Length;
                if (longueurTab < 2)
                {
                    return this.Json(new { success = false, message = MSG_ERREUR_LENGTH });
                }
                int[] heures = new int[longueurTab];
                string[] splitValue1, splitValue2;
                Array.Sort(values, new AlphanumComparatorFast());
                for (int i = 0; i < values.Length - 1; i+=2)
                {
                    splitValue1 = values[i].Split('-');
                    splitValue2 = values[i + 1].Split('-');
                    if (!(int.Parse(splitValue1[1]) +1 == int.Parse(splitValue2[1])))
                    {
                        return this.Json(new { success = false, message = MSG_ERREUR_CONSECUTIF });
                    }
                }


                return this.Json(new { success = true, message = values });

            }
            else
            {
                return this.Json(new { success = false, message = MSG_ERREUR_REMPLIR });
            }
        }

        
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

        [NonAction]
        private int? JourANumero(string jour)
        {
            switch (jour)
            {
                case "Lundi":
                    return 2;
                case "Mardi":
                    return 3;
                case "Mercredi":
                    return 4;
                case "Jeudi":
                    return 5;
                case "Vendredi":
                    return 6;
                default:
                    return null;

            }
        }

        [NonAction]
        private string[] triageTableauAlphaNumerique(string[] tableau)
        {
            Array.Sort(tableau, StringComparer.InvariantCulture);
            return tableau;
        }
    }
}
