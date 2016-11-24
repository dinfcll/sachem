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
        private const string MSG_ERREUR_REMPLIR = "Veuillez remplir le formulaire de disponibilités.";
        //[ValidationAcces.ValidationAccesInscription]
        // GET: Inscription
        public ActionResult Index()
        {
            ViewBag.TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            return View();
        }

        [HttpPost]
        public ActionResult Index(int typeInscription, string[] values )
        {
            int id_Pers = SessionBag.Current.id_Pers;
            ViewBag.TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            var SessionActuelle = db.Session.AsNoTracking().OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison).FirstOrDefault();
            Inscription inscriptionBD = new Inscription();
            inscriptionBD.id_Pers = id_Pers;
            inscriptionBD.id_Sess = SessionActuelle.id_Sess;
            inscriptionBD.id_Statut = 1;
            inscriptionBD.BonEchange = false;
            inscriptionBD.ContratEngagement = false;
            inscriptionBD.TransmettreInfoTuteur = false;
            inscriptionBD.DateInscription = DateTime.Now;
            inscriptionBD.id_TypeInscription = typeInscription;
            db.Inscription.Add(inscriptionBD);
            db.SaveChanges();
            if (values != null)
            {
                int longueurTab = values.Length;
                int minute;
                string[] splitValue1;
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
                var InscriptionEtu = db.Inscription.Where(x => x.id_Pers == id_Pers).FirstOrDefault();
                foreach (DisponibiliteStruct m in disponibilites)
                {
                    dispoBD.id_Inscription = InscriptionEtu.id_Inscription;
                    dispoBD.id_Jour = m.dictionary[m.Jour];
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
        
    }
}
