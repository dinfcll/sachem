using System;
using System.Data.Entity;
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

        private int HEURE_DEBUT = CheckConfigHeure(System.Configuration.ConfigurationManager.AppSettings.Get("HeureDebut"), 8);
        private int HEURE_FIN = CheckConfigHeure(System.Configuration.ConfigurationManager.AppSettings.Get("HeureFin"), 18);
        private const int DEMI_HEURE = 30;
        private const int DUREE_RENCONTRE_MINUTES = 90;
        private string[] m_Session = { "Hiver", "Été", "Automne" };
        private string IDREUSSIS = "1";
        private string IDABAN = "2";
        private string IDECHEC = "3";
        // GET: Inscription
        [ValidationAcces.ValidationAccesInscription]
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
                switch ((TypeInscription)typeInscription)
                {
                    case TypeInscription.eleveAide:
                        return RedirectToAction("EleveAide1");
                    case TypeInscription.tuteurDeCours:
                        return RedirectToAction("Index");
                    case TypeInscription.tuteurBenevole:
                    case TypeInscription.tuteurRemunere:
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
            for (int i = 2; i < 7; i++)
            {
                Jours.Add(((Semaine)i).ToString());
            }
            return Jours.ToList();
        }

        private static int CheckConfigHeure(string Heure, int defaut)
        {
            int result;
            return int.TryParse(Heure, out result) ? result : defaut;
        }

        [NonAction]
        public Dictionary<string, List<string>> RetourneTableauDisponibilite()
        {
            TimeSpan StartTime = TimeSpan.FromHours(HEURE_DEBUT);
            int Difference = DEMI_HEURE;
            int Rencontre = DUREE_RENCONTRE_MINUTES;
            int EntriesCount = HEURE_FIN;
            Dictionary<TimeSpan, TimeSpan> listeCasesRencontreAu30min = new Dictionary<TimeSpan, TimeSpan>();
            Dictionary<string, List<string>> Sortie = new Dictionary<string, List<string>>();

            for (int i = 0; i < EntriesCount; i++)
            {
                listeCasesRencontreAu30min.Add(StartTime.Add(TimeSpan.FromMinutes(Difference * i)),
                            StartTime.Add(TimeSpan.FromMinutes(Difference * i + Rencontre)));
            }

            foreach (var case30min in listeCasesRencontreAu30min)
            {
                double minutes = case30min.Key.TotalMinutes;
                List<string> values = new List<string>();
                for (int j = (int)Semaine.Lundi; j <= (int)Semaine.Vendredi; j++)
                {
                    values.Add(((Semaine)j).ToString() + "-" + minutes.ToString());
                }
                Sortie.Add(
                String.Format("{0}h{1}-{2}h{3}", case30min.Key.Hours, case30min.Key.Minutes.ToString("00"), case30min.Value.Hours, case30min.Value.Minutes.ToString("00")),
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
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [ValidationAcces.ValidationAccesEtu]
        public ActionResult EleveAide1()
        {
            listeCours();
            listeStatutCours();
            listeSession();
            return View();
        }
        [ValidationAcces.ValidationAccesEtu]
        [HttpPost]
        public ActionResult EleveAide1(string[][] values)
        {
            TempData["Echec"] = "";
            if (values==null)
            {
                return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.id_Inscription });
            }
            else
            {
                    for (var i = 0; i < values.Length; i++)
                    {
                        if (values[i][0] != "")
                        {
                            var cours = new CoursSuivi();
                            cours.id_Pers = SessionBag.Current.id_Pers;
                            cours.id_Cours = Convert.ToInt32(values[i][0]);
                            cours.id_Statut = Convert.ToInt32(values[i][1]);
                            cours.id_Sess = Convert.ToInt32(values[i][2]);
                            cours.id_College = db.p_College.FirstOrDefault(x => x.College == "Cégep de Lévis-Lauzon").id_College;
                            if (values[i][3] != "")
                            {
                                cours.resultat = Convert.ToInt32(values[i][3]);
                            }
                            db.CoursSuivi.Add(cours);
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.id_Inscription });
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
        [NonAction]
        public void listeCours()
        {
            var lstCrs = from c in db.Cours orderby c.Nom select c;
            var slCrs = new List<SelectListItem>();
            slCrs.AddRange(new SelectList(lstCrs, "id_Cours","CodeNom"));
            ViewBag.lstCours = slCrs;
            ViewBag.lstCours1 = slCrs;
        }
        [NonAction]
        public void listeCollege()
        {
            var lstCol = from c in db.p_College orderby c.College select c;
            var slCol = new List<SelectListItem>();
            slCol.AddRange(new SelectList(lstCol, "id_College", "College"));
            ViewBag.lstCollege = slCol;
        }
        [NonAction]
        public void listeStatutCours()
        {
            var lstStatut = from c in db.p_StatutCours orderby c.id_Statut select c;
            var slStatut = new List<SelectListItem>();
            slStatut.AddRange(new SelectList(lstStatut, "id_Statut", "Statut"));
            ViewBag.lstStatut = slStatut;
        }
        [NonAction]
        public void listeSession()
        {
            var lstSess = from c in db.Session orderby c.id_Sess select c;
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lstSess, "id_Sess", "NomSession", Session));
            ViewBag.slSession = slSession;
        }
        [HttpPost]
        public ActionResult getLigneCours()
        {
            listeCours();
            listeCollege();
            return PartialView("_LigneCoursReussi");
        }
        [HttpPost]
        public ActionResult getLigneCoursEleveAide()
        {
            listeCours();
            listeStatutCours();
            listeSession();
            return PartialView("_LigneCoursReussiEleveAide");
        }
        [HttpPost]
        public string ErreurCours()
        {
            return Messages.I_048();
        }      
    }
}
