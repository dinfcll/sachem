using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using sachem.Methodes_Communes;
using sachem.Models;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    public class InscriptionController : Controller
    {
        private readonly SACHEMEntities _db = new SACHEMEntities();
        private const string MsgErreurRemplir = Messages.InscriptionRemplirFormulaireDisposErreur;

        private readonly int _heureDebut = CheckConfigHeure(ConfigurationManager.AppSettings.Get("HeureDebut"), 8);
        private readonly int _heureFin = CheckConfigHeure(ConfigurationManager.AppSettings.Get("HeureFin"), 18);
        private const int DemiHeure = 30;
        private const int DureeRencontreMinutes = 90;
        private readonly IDataRepository _dataRepository;

        public InscriptionController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public InscriptionController()
        {
            _dataRepository = new BdRepository();
        }

        [ValidationAcces.ValidationAccesInscription]
        public ActionResult Index()
        {
            ViewBag.TypeInscription = _dataRepository.ListeTypeInscription();

            return View();
        }

        [HttpPost]
        [ValidationAcces.ValidationAccesInscription]        
        public ActionResult Index(int typeInscription, string[] jours )
        {
            int idPers = BrowserSessionBag.Current.id_Pers;
            ViewBag.TypeInscription = _dataRepository.ListeTypeInscription();
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
            if (jours == null) return Json(new {success = false, message = MsgErreurRemplir});
            {
                var dispo = new DisponibiliteStruct();
                var disponibilites = new List<DisponibiliteStruct>();
                Array.Sort(jours, new AlphanumComparatorFast());
                foreach (var jour in jours)
                {
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
                BrowserSessionBag.Current.TypeInscription = typeInscription;
                if (typeInscription < (int)TypeInscription.TuteurRemunere)
                {
                    return RedirectToAction(((TypeInscription)typeInscription).ToString());
                }
                if (typeInscription == (int)TypeInscription.TuteurRemunere)
                {
                    return RedirectToAction(TypeInscription.TuteurBenevole.ToString());
                }
                return Json(new { success = false, message = MsgErreurRemplir });
            }
        }

        [NonAction]
        public List<string> RetourneListeJours()
        {
            return _dataRepository.ListeJours();
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

        [ValidationAcces.ValidationAccesEtudiants]
        public ActionResult EleveAide()
        {
            ViewBag.lstCours = _dataRepository.ListeCours();
            ViewBag.lstCours1 = _dataRepository.ListeCours();
            ViewBag.lstStatut = _dataRepository.ListeStatutCours();
            ViewBag.slSession = _dataRepository.ListeSession();
            return View();
        }

        [HttpGet]
        public ActionResult TuteurCours()
        {
            ViewBag.lstCours = _dataRepository.ListeCours();
            ViewBag.lstCours1 = _dataRepository.ListeCours();
            ViewBag.lstCollege = _dataRepository.ListeCollege();
            return View();
        }

        public ActionResult GetLigneCoursEleveAide()
        {
            ViewBag.lstCours = _dataRepository.ListeCours();
            ViewBag.lstCours1 = _dataRepository.ListeCours();
            ViewBag.lstStatut = _dataRepository.ListeStatutCours();
            ViewBag.slSession = _dataRepository.ListeSession();

            return PartialView("_LigneCoursReussiEleveAide");
        }

        [HttpGet]
        public ActionResult TuteurBenevole()
        {
            ViewBag.lstCours = _dataRepository.ListeCours();
            ViewBag.lstCours1 = _dataRepository.ListeCours();
            ViewBag.lstCollege = _dataRepository.ListeCollege();

            return View();
        }

        [HttpPost]
        public ActionResult GetLigneCours()
        {
            ViewBag.lstCours = _dataRepository.ListeCours();
            ViewBag.lstCours1 = _dataRepository.ListeCours();
            ViewBag.lstCollege = _dataRepository.ListeCollege();

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

            int idPers = BrowserSessionBag.Current.id_Pers;
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
        public string ErreurCours()
        {
            ViewBag.lstCours = _dataRepository.ListeCours();
            return Messages.InscriptionCoursChoisiUneSeuleFois;
        }

        public bool Contient(string value, List<string[]> donneesInscription)
        {
            return donneesInscription.Any(d => d[0] == value || d[2] == value);
        }

        [ValidationAcces.ValidationAccesEleveAide]
        [HttpPost]
        public ActionResult EleveAide1(string[][] values)
        {
            TempData["Echec"] = "";
            if (values==null)
            {
                return RedirectToAction("Details", "DossierEtudiant", new { id = BrowserSessionBag.Current.id_Inscription});
            }
            foreach (var t in values)
            {
                if (t[0] == "") continue;
                var firstOrDefault = _db.p_College.FirstOrDefault(x => x.College == "Cégep de Lévis-Lauzon");

                if (firstOrDefault != null)
                {
                    var cours = new CoursSuivi
                    {
                        id_Pers = BrowserSessionBag.Current.id_Pers,
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
            return RedirectToAction("Details", "DossierEtudiant", new { id = BrowserSessionBag.Current.id_Inscription });
        }

        [HttpPost]
        public ActionResult FormattageDesDisposPourModal(int typeInscription, string[] jours)
        {
            var retour = "";
            var lRetour = new List<string>();
            var lCases = new List<DisponibiliteStruct>();
            var derniereCase = 0;
            foreach (var j in jours)
            {
                var laCase = new DisponibiliteStruct
                {
                    Jour = j.Substring(0, j.IndexOf('-') - 1),
                    Minutes = Convert.ToInt32(j.Substring(j.IndexOf('-') + 1, j.Length-1)),
                    HeureDebut = TimeSpan.FromMinutes(Convert.ToInt32(j.Substring(j.IndexOf('-') + 1, j.Length-1)))
                };
                laCase.HeureFin = laCase.HeureDebut + TimeSpan.FromMinutes(90);
                if (laCase.Minutes.Equals(derniereCase+DemiHeure))
                {
                    var premiereHeureDispoDeJournee = laCase.Minutes;
                    while (!lCases.Exists(x => x.Minutes == premiereHeureDispoDeJournee && x.Jour == laCase.Jour))
                    {
                        premiereHeureDispoDeJournee -= DemiHeure;
                    }
                    var laCaseRetiree = lCases.Find(x => x.Jour == laCase.Jour && x.Minutes == premiereHeureDispoDeJournee);
                    lCases.Remove(laCaseRetiree);
                    laCase.HeureDebut = laCaseRetiree.HeureDebut;
                }
                lCases.Add(laCase);
                derniereCase = laCase.Minutes;
            }
            foreach (var c in lCases.OrderBy(x=>x.Jour).ThenBy(x=>x.HeureDebut))
            {
                lRetour.Add(
                    $"{c.Jour} de {c.HeureDebut.Hours}h{c.HeureDebut.Minutes:00} à {c.HeureFin.Hours}h{c.HeureFin.Minutes:00}");
            }
            foreach (var s in lRetour)
            {
                retour += "<p>" + s + "</p>";
            }
            return Json(new { success = true, data = retour });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
