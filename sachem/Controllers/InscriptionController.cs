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
        private const string MSG_ERREUR_CONSECUTIF = "Erreur: vous devez avoir une plage horaire contenant des heures consécutives.";
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
        public ActionResult Index(string typeInscription, string[] values )
        {
            var SessionActuelle = db.Session.AsNoTracking().OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison).FirstOrDefault();
            Inscription inscriptionBD = new Inscription();
            inscriptionBD.id_Pers = SessionBag.Current.id_Pers;
            inscriptionBD.id_Sess = SessionActuelle.id_Sess;
            inscriptionBD.id_Statut = 1;
            //inscriptionBD.id_TypeInscription = La valeur du checkbox;
            inscriptionBD.DateInscription = DateTime.Now;

            ViewBag.TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            if (values != null)
            {
                int longueurTab = values.Length;
                int minute;
                string[] splitValue1, splitValue2, splitValue3;
                string jour;
                Lis­t<DisponibiliteStruct> disponibilites = new List<DisponibiliteStruct>();
                Array.Sort(values, new AlphanumComparatorFast());
                for (int i = 0; i < values.Length - 2; i += 2)
                {
                    //TODO: Valider si les heures se suivent, formatter pour demander confirmation à l'utilisateur.
                    splitValue1 = values[i].Split('-');
                    minute = int.Parse(splitValue1[1]);
                    jour = splitValue1[0];
                    disponibilites.Add(new DisponibiliteStruct(jour, minute));
                }

                Disponibilite dispoBD = new Disponibilite();
                //dispoBD.id_Inscription =  Le ID de l'inscription?
                foreach (DisponibiliteStruct m in disponibilites)
                {
                    //TODO: Mettre dans BD après validation.
                    db.Disponibilite.Add(dispoBD);
                    db.SaveChanges();
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
