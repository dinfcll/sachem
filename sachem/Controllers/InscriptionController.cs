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

        private const int HEURE_DEBUT = 8;
        private const int HEURE_FIN = 18;
        private const int DUREE_RENCONTRE = 90;

        [ValidationAcces.ValidationAccesInscription]
        // GET: Inscription
        public ActionResult Index()
        {
            ViewBag.TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            return View();
        }

        [ValidationAcces.ValidationAccesInscription]
        [HttpPost]
        public ActionResult Index(int typeInscription, string[] jours )
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
            if (jours != null)
            {
                int longueurTab = jours.Length;
                string[] splitValue1;
                DisponibiliteStruct dispo = new DisponibiliteStruct();
                Lis­t<DisponibiliteStruct> disponibilites = new List<DisponibiliteStruct>();
                Array.Sort(jours, new AlphanumComparatorFast());
                for (int i = 0; i < jours.Length; i++)
                {
                    //TODO: Valider si les heures se suivent, formatter pour demander confirmation à l'utilisateur.
                    splitValue1 = jours[i].Split('-');
                    dispo.Minutes = int.Parse(splitValue1[1]);
                    dispo.Jour = splitValue1[0];
                    disponibilites.Add(dispo);
                }

                Disponibilite dispoBD = new Disponibilite();
                var InscriptionEtu = db.Inscription.Where(x => x.id_Pers == id_Pers).FirstOrDefault();
                foreach (DisponibiliteStruct m in disponibilites)
                {
                    dispoBD.id_Inscription = InscriptionEtu.id_Inscription;
                    dispoBD.id_Jour = (int)Enum.Parse(typeof(Semaine), m.Jour);
                    dispoBD.minutes = m.Minutes;
                    db.Disponibilite.Add(dispoBD);
                    db.SaveChanges();
                }
                switch (typeInscription)
                {
                    case 1: // élève aidé
                        return RedirectToAction("EleveAide1");
                    case 2: // Tuteur de cours
                        return RedirectToAction("Index");
                    case 3: //Tuteur bénévole
                    case 4: //Tuteur rémunéré
                        return RedirectToAction("Index");
                    default:
                        return this.Json(new { success = false, message = MSG_ERREUR_REMPLIR });
                }
            }
            else
            {
                return this.Json(new { success = false, message = MSG_ERREUR_REMPLIR });
            }
        }

        [NonAction]
        public List<string> RetourneListeJours()
        {
            List<string> Jours = new List<string>();
            for (int i = 1; i < 6; i++)
            {
                Jours.Add(((Semaine)i).ToString());
            }
            return Jours.ToList();
        }

        [NonAction]
        public Dictionary<string, List<string>> RetourneTableauDisponibilite()
        {
            TimeSpan StartTime = TimeSpan.FromHours(HEURE_DEBUT);
            int Difference = 30;
            int Rencontre = DUREE_RENCONTRE;
            int EntriesCount = HEURE_FIN;
            Dictionary<TimeSpan, TimeSpan> Entree = new Dictionary<TimeSpan, TimeSpan>();
            Dictionary<string, List<string>> Sortie = new Dictionary<string, List<string>>();

            for (int i = 0; i < EntriesCount; i++)
            {
                Entree.Add(StartTime.Add(TimeSpan.FromMinutes(Difference * i)),
                            StartTime.Add(TimeSpan.FromMinutes(Difference * i + Rencontre)));
            }

            foreach (var e in Entree)
            {
                double heureCheckbox = e.Key.TotalMinutes;
                List<string> values = new List<string>();
                for (int j = (int)Semaine.Lundi; j <= (int)Semaine.Vendredi; j++)
                {
                    values.Add(((Semaine)j).ToString() + "-" + heureCheckbox.ToString());
                }
                Sortie.Add(
                e.Key.Hours + "h" + e.Key.Minutes.ToString("00") + "-" + e.Value.Hours + "h" + e.Value.Minutes.ToString("00"),
                values);
            }
            return Sortie;
        }

        // GET: Inscription/Delete/5
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
