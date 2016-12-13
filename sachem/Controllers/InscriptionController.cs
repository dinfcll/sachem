using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using sachem.Classes_Sachem;
using sachem.Models;

namespace sachem.Controllers
{
    public class InscriptionController : Controller
    {
        private readonly SACHEMEntities _db = new SACHEMEntities();
        private const string MsgErreurRemplir = "Veuillez remplir le formulaire de disponibilités.";

        private readonly int _heureDebut = CheckConfigHeure(ConfigurationManager.AppSettings.Get("HeureDebut"), 8);
        private readonly int _heureFin = CheckConfigHeure(ConfigurationManager.AppSettings.Get("HeureFin"), 18);
        private const int DemiHeure = 30;
        private const int DureeRencontreMinutes = 90;

       
        [ValidationAcces.ValidationAccesInscription]
        public ActionResult Index()
        {
            ViewBag.TypeInscription = new SelectList(_db.p_TypeInscription, "id_TypeInscription", "TypeInscription");

            return View();
        }

        [HttpPost]
        [ValidationAcces.ValidationAccesInscription]        
        public ActionResult Index(int typeInscription, string[] jours )
        {
            int idPers = SessionBag.Current.id_Pers;
            ViewBag.TypeInscription = new SelectList(_db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            var sessionActuelle = _db.Session.AsNoTracking().OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison).FirstOrDefault();
            if (sessionActuelle != null)
            {
                var inscriptionBd = new Inscription
                {
                    id_Pers = idPers,
                    id_Sess = sessionActuelle.id_Sess,
                    id_Statut = 1,
                    BonEchange = false,
                    ContratEngagement = false,
                    TransmettreInfoTuteur = false,
                    DateInscription = DateTime.Now,
                    id_TypeInscription = typeInscription
                };
                _db.Inscription.Add(inscriptionBd);
            }
            _db.SaveChanges();
            if (jours != null)
            {
                var dispo = new DisponibiliteStruct();
                var disponibilites = new List<DisponibiliteStruct>();
                Array.Sort(jours, new AlphanumComparatorFast());
                foreach (var jour in jours)
                {
                    //TODO: Valider si les heures se suivent, formatter pour demander confirmation à l'utilisateur.
                    var splitValue1 = jour.Split('-');
                    dispo.Minutes = int.Parse(splitValue1[1]);
                    dispo.Jour = splitValue1[0];
                    disponibilites.Add(dispo);
                }

                var dispoBd = new Disponibilite();
                var inscriptionEtu = _db.Inscription.FirstOrDefault(x => x.id_Pers == idPers);
                foreach (var m in disponibilites)
                {
                    if (inscriptionEtu != null) dispoBd.id_Inscription = inscriptionEtu.id_Inscription;
                    dispoBd.id_Jour = (int)Enum.Parse(typeof(Semaine), m.Jour);
                    dispoBd.minutes = m.Minutes;
                    _db.Disponibilite.Add(dispoBd);
                    _db.SaveChanges();
                }
                SessionBag.Current.id_Inscription = typeInscription;
                switch ((TypeInscription)typeInscription)
                {
                    case TypeInscription.eleveAide:
                        SessionBag.Current.id_TypeUsag = TypeUsagers.Eleve;
                        return RedirectToAction("EleveAide1");
                    case TypeInscription.tuteurDeCours:
                        SessionBag.Current.id_TypeUsag = TypeUsagers.Tuteur;
                        return RedirectToAction("Tuteur");
                    case TypeInscription.tuteurBenevole:
                        SessionBag.Current.id_TypeUsag = TypeUsagers.Tuteur;
                        return RedirectToAction("Benevole");
                    case TypeInscription.tuteurRemunere:
                        SessionBag.Current.id_TypeUsag = TypeUsagers.Tuteur;
                        return RedirectToAction("Benevole");
                    default:
                        return Json(new { success = false, message = MsgErreurRemplir });
                }
            }
            return Json(new { success = false, message = MsgErreurRemplir });
        }

        private static int CheckConfigHeure(string heure, int defaut)
        {
            int result;
            return int.TryParse(heure, out result) ? result : defaut;
        }

        [NonAction]
        public Dictionary<string, List<string>> RetourneTableauDisponibilite()
        {
            var startTime = TimeSpan.FromHours(_heureDebut);
            const int difference = DemiHeure;
            const int rencontre = DureeRencontreMinutes;
            var entriesCount = _heureFin;
            var listeCasesRencontreAu30Min = new Dictionary<TimeSpan, TimeSpan>();
            var sortie = new Dictionary<string, List<string>>();

            for (var i = 0; i < entriesCount; i++)
            {
                listeCasesRencontreAu30Min.Add(startTime.Add(TimeSpan.FromMinutes(difference * i)),
                            startTime.Add(TimeSpan.FromMinutes(difference * i + rencontre)));
            }

            foreach (var case30Min in listeCasesRencontreAu30Min)
            {
                var minutes = case30Min.Key.TotalMinutes;
                var values = new List<string>();
                for (var j = (int)Semaine.Lundi; j <= (int)Semaine.Vendredi; j++)
                {
                    values.Add(((Semaine)j) + "-" + minutes);
                }
                sortie.Add(
                    $"{case30Min.Key.Hours}h{case30Min.Key.Minutes:00}-{case30Min.Value.Hours}h{case30Min.Value.Minutes:00}",
                values);
            }
            return sortie;
        }

        [ValidationAcces.ValidationAccesEtu]
        public ActionResult EleveAide1()
        {
            ListeCours();
            ListeStatutCours();
            ListeSession();
            return View();
        }

        [HttpGet]
        public ActionResult Tuteur()
        {
            ListeCours();
            ListeCollege();
            return View();
        }

        [NonAction]
        public void ListeStatutCours()
        {
            var lstStatut = from c in _db.p_StatutCours orderby c.id_Statut select c;
            var slStatut = new List<SelectListItem>();
            slStatut.AddRange(new SelectList(lstStatut, "id_Statut", "Statut"));
            ViewBag.lstStatut = slStatut;
        }

        public ActionResult GetLigneCoursEleveAide()
        {
            ListeCours();
            ListeStatutCours();
            ListeSession();

            return PartialView("_LigneCoursReussiEleveAide");
        }

        [HttpGet]
        public ActionResult Benevole()
        {
            ListeCours();
            ListeCollege();

            return View();
        }

        [HttpPost]
        public ActionResult GetLigneCours()
        {
            ListeCours();
            ListeCollege();

            return PartialView("_LigneCoursReussi");
        }

        [HttpPost]
        public string Poursuivre(string[][] values, string[] coursInteret)
        {
            var i = 0;
            var donneesInscription = new List<string[]>();
            var erreur = false;

            while (i < values.Length && !erreur)
            {
                var temp = new string[3];

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
                    if (!Contient(values[i][0], donneesInscription))
                    {
                        temp[0] = values[i][0];
                    }
                    else
                    {
                        erreur = true;
                    }

                }

                int resultat;
                if (int.TryParse(values[i][1], out resultat) && (resultat >= 0 && resultat <= 100))
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

            int idPers = SessionBag.Current.id_Pers;
            const int sess = 2;

            foreach (var d in donneesInscription)
            {
                var cs = new CoursSuivi();

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
                _db.CoursSuivi.Add(cs);
                _db.SaveChanges();
            }
            return "DossierEtudiant/Index";
        }

        [HttpPost]
        public void ListeCours()
        {
            var lstCrs = from c in _db.Cours orderby c.Nom select c;
            var slCrs = new List<SelectListItem>();
            slCrs.AddRange(new SelectList(lstCrs, "id_Cours", "CodeNom"));
            ViewBag.lstCours = slCrs;
            ViewBag.lstCours1 = slCrs;
        }

        [HttpPost]
        public string ErreurCours()
        {
            var lstCrs = from c in _db.Cours orderby c.Nom select c;
            var slCrs = new List<SelectListItem>();
            slCrs.AddRange(new SelectList(lstCrs, "id_Cours", "CodeNom"));
            ViewBag.lstCours = slCrs;
            ViewBag.lstCours1 = slCrs;
            return Messages.CoursChoisiUneSeuleFois();
        }

        [HttpPost]
        public void ListeCollege()
        {
            var lstCol = from c in _db.p_College orderby c.College select c;
            var slCol = new List<SelectListItem>();
            slCol.AddRange(new SelectList(lstCol, "id_College", "College"));
            ViewBag.lstCollege = slCol;
        }

        [NonAction]
        public void ListeSession()
        {
            var lstSess = from c in _db.Session orderby c.id_Sess select c;
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lstSess, "id_Sess", "NomSession", Session));
            ViewBag.slSession = slSession;
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

        [ValidationAcces.ValidationAccesEleve]
        [HttpPost]
        public ActionResult EleveAide1(string[][] values)
        {
            TempData["Echec"] = "";
            if (values==null)
            {
                return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.id_Inscription});
            }
            foreach (var t in values)
            {
                if (t[0] != "")
                {
                    var firstOrDefault = _db.p_College.FirstOrDefault(x => x.College == "Cégep de Lévis-Lauzon");

                    if (firstOrDefault != null)
                    {
                        var cours = new CoursSuivi
                        {
                            id_Pers = SessionBag.Current.id_Pers,
                            id_Cours = Convert.ToInt32(t[0]),
                            id_Statut = Convert.ToInt32(t[1]),
                            id_Sess = Convert.ToInt32(t[2]),
                            id_College = firstOrDefault.id_College
                        };
                        if (t[3] != "")
                        {
                            cours.resultat = Convert.ToInt32(t[3]);
                        }
                        _db.CoursSuivi.Add(cours);
                    }
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.id_Inscription });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
