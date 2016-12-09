using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachem.Classes_Sachem;
using sachem.Models;
using System.Text.RegularExpressions;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace sachem.Controllers
{
   
    public class InscriptionController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();
        private const string MSG_ERREUR_REMPLIR = "Veuillez remplir le formulaire de disponibilités.";

        private const int HEURE_DEBUT = 8;
        private const int HEURE_FIN = 18;
        private const int DEMI_HEURE = 30;
        private const int DUREE_RENCONTRE_MINUTES = 90;

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
                    //db.SaveChanges();
                }
                SessionBag.Current.id_Inscription = typeInscription;
                switch (typeInscription)
                    {
                    case 1: // élève aidé
                        return this.Json(new { url = "EleveAide1" });
                    case 2: // Tuteur de cours
                        return this.Json(new { url = "Tuteur" });
                    case 3: //Tuteur bénévole
                    case 4: //Tuteur rémunéré
                        return this.Json(new {url = "TBenevole" });
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
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Tuteur()
        {
            listeCours();
            listeCollege();
            return View();
        }
        [HttpGet]
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
            slCrs.AddRange(new SelectList(lstCrs, "id_Cours", "CodeNom"));
            ViewBag.lstCours = slCrs;
            ViewBag.lstCours1 = slCrs;
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
        public string Poursuivre(string[][] values, string[] coursInteret)
        {
            int i = 0;
            int resultat;
            string[] temp = new string[3];
            List<string[]> donneesInscription = new List<string[]>();
            bool erreur = false;

            while (i < values.Length && !erreur)
            {
                temp = new string[3];

                if (values[i][0] == "")
                {
                    if (values[i][2] == "")
                    {
                        erreur = true;
                    }
                    else
                    {
                        if (!Contient(values[i][0], donneesInscription))
                        {
                            temp[0] = values[i][2];
                        }
                        else
                        {
                            erreur = true;    
                        }
                    }                    
                }
                else
                {
                    if(!Contient(values[i][0], donneesInscription))
                    {
                        temp[0] = values[i][0];
                    }
                    else
                    {
                        erreur = true;
                    }

                }

                if (Int32.TryParse(values[i][1], out resultat) && (resultat >= 0 && resultat <= 100))
                {
                    temp[1] = values[i][1];
                }
                else
                {
                    erreur = true;
                }
                if (values[i][3] == "")
                {
                    if (values[i][4] == "")
                    {
                        erreur = true;
                    }
                    else
                    {
                        temp[2] = values[i][4];
                    }
                }
                else
                {
                    temp[2] = values[i][3];
                }

                donneesInscription.Add(temp);
                i++;
            }
            if (erreur)
            {
                return "non!";
            }   

            CoursSuivi cs = new CoursSuivi();
            int idPers = SessionBag.Current.id_Pers;
            int sess = SessionBag.Current.id_Sess;

            foreach (string[] d in donneesInscription)
            {
                if (d[0] == "")
                {
                    cs.autre_Cours = d[0];
                }
                else
                {
                    cs.id_Cours = int.Parse(d[0]);
                }
                cs.resultat = int.Parse(d[1]);
                if (d[2] == "")
                {
                    cs.autre_College = d[2];
                }
                else
                {
                    cs.id_College = int.Parse(d[2]);
                }
                cs.id_Pers = idPers;
                cs.id_Sess = sess;
                db.CoursSuivi.Add(cs);
                db.SaveChanges();
            }

            int ptype = SessionBag.Current.id_Inscription;            
            var InscriptionInteret = db.Inscription.Where(x => x.id_Pers == idPers).FirstOrDefault();

            for (i=0;i<3;i++)
            {   
                CoursInteret ci = new CoursInteret();
                ci.id_Inscription = InscriptionInteret.id_Inscription;
                ci.id_Cours = Int32.Parse(coursInteret[i]);

                //ci.Cours = db.Cours.Where(x => x.id_Cours == ci.id_Cours).FirstOrDefault();
                //ci.Inscription = InscriptionInteret;

                ci.Priorite = i + 1;
                db.CoursInteret.Add(ci);
                try
                {
                    db.SaveChanges();
                }
                catch(DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            return "oui!";
        }


        public bool Contient(string value, List<string[]> donneesInscription)
        {
            foreach (string[] d in donneesInscription)
            {
                if (d[0] == value || d[2] == value)
                {
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        public string ErreurCours()
        {
            return Messages.I_048();
        }
    }
}
