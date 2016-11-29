using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using static sachem.Classes_Sachem.ValidationAcces;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    public struct caseDisponibilite
    {
        string jour;
        int minutes;
        string nomCase;
        bool estDispo;
        bool estDispoMaisJumele;
        int nbreUsagerMemeDispo;
        bool estConsecutiveDonc3hrs;

        public string Jour { get { return jour; } set { jour = value; } }
        public int Minutes { get { return minutes; } set { minutes = value; } }
        public string NomCase { get { return nomCase; } set { nomCase = value; } }
        public bool EstDispo { get { return estDispo; } set { estDispo = value; } }
        public bool EstDispoMaisJumele { get { return estDispoMaisJumele; } set { estDispoMaisJumele = value; } }
        public int NbreUsagerMemeDispo { get { return nbreUsagerMemeDispo; } set { nbreUsagerMemeDispo = value; } }
        public bool EstConsecutiveDonc3hrs { get { return estConsecutiveDonc3hrs; } set { estConsecutiveDonc3hrs = value; } }
    }

    public enum Semaine
    {
        Dimanche = 0,
        Lundi,
        Mardi,
        Mercredi,
        Jeudi,
        Vendredi,
        Samedi
    }

    public class JumelageController : Controller
    {
        private const int HEURE_DEBUT = 8;
        private const int HEURE_FIN = 18;
        private const int DUREE_RENCONTRE = 90;
        private SACHEMEntities db = new SACHEMEntities();
        protected int noPage = 1;
        private int? pageRecue = null;

        private readonly IDataRepository dataRepository;

        public JumelageController()
        {
            dataRepository = new BdRepository();
        }

        public JumelageController(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        [NonAction]
        private void ListeSession(int session = 0)
        {
            var lSessions = db.Session.AsNoTracking().OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", session));
            ViewBag.Session = slSession;
        }

        [NonAction]
        public string RetourneNbreJumelageEtudiant(int count)
        {
            string statut = "";
            switch (count)
            {
                case 0:
                    statut = "Non jumelé";
                    break;
                case 1:
                    statut = "Jumelé";
                    break;
                case 2:
                    statut = "Jumelé (2 fois)";
                    break;
                default:
                    break;
            }
            return statut;
        }

        [NonAction]
        public List<string> RetourneListeJoursSemaine()
        {
            List<string> Jours = new List<string>();
            for (int i = 1; i < 6; i++)
            {
                Jours.Add(((Semaine)i).ToString());
            }
            return Jours.ToList();
        }

        [NonAction]
        public Dictionary<string, List<caseDisponibilite>> RetourneDisponibiliteJumelageUsager(int id, int idTypeInsc)
        {            
            List<caseDisponibilite> jumelageValeurDispo = new List<caseDisponibilite>();
            caseDisponibilite caseDispo = new caseDisponibilite();        
            IQueryable<Disponibilite> dispo = db.Disponibilite.Where(x => x.id_Inscription == id);
            IQueryable<Disponibilite> dispoAutres = db.Disponibilite.Where(x => x.id_Inscription != id);
            IQueryable<Jumelage> dispoJumele; 

            if (idTypeInsc == 1)
            {
                dispoJumele = db.Jumelage.Where(eleve => eleve.id_InscEleve == id);
            }
            else
            {
                dispoJumele = db.Jumelage.Where(tuteur => tuteur.id_InscrTuteur == id);
            }
            foreach (var j in dispoJumele)
            {
                caseDispo.Jour = j.p_Jour.Jour;
                caseDispo.Minutes = j.minutes;
                caseDispo.NomCase = caseDispo.Jour + "-" + caseDispo.Minutes;
                caseDispo.NbreUsagerMemeDispo = 0;
                caseDispo.EstDispo = true;
                caseDispo.EstDispoMaisJumele = true;
                if (Convert.ToBoolean(j.consecutif))
                {
                    caseDispo.EstConsecutiveDonc3hrs = false;
                    jumelageValeurDispo.Add(caseDispo);
                }
                else
                {
                    caseDispo.EstConsecutiveDonc3hrs = false;
                    jumelageValeurDispo.Add(caseDispo);
                    caseDispo.Minutes = j.minutes + DUREE_RENCONTRE;
                    caseDispo.NomCase = caseDispo.Jour + "-" + caseDispo.Minutes;
                    caseDispo.EstConsecutiveDonc3hrs = true;
                    jumelageValeurDispo.Add(caseDispo);
                    caseDispo.EstConsecutiveDonc3hrs = true;
                    jumelageValeurDispo.Add(caseDispo);
                }
            }

            int compteurUsagerAvecMemeDispo = 0;
            foreach (var d in dispo)
            {
                foreach(var a in dispoAutres)
                {
                    if(d.id_Jour==a.id_Jour && d.minutes==a.minutes)
                    {
                        if(!jumelageValeurDispo.Exists(x=>x.Jour==a.p_Jour.Jour&&x.Minutes==a.minutes))
                        {
                            compteurUsagerAvecMemeDispo++;
                        }
                    }
                }
                caseDispo.Jour = d.p_Jour.Jour;
                caseDispo.Minutes = d.minutes;
                caseDispo.NomCase = d.p_Jour.Jour + "-" + d.minutes;
                caseDispo.NbreUsagerMemeDispo = compteurUsagerAvecMemeDispo;
                caseDispo.EstDispo = true;
                caseDispo.EstDispoMaisJumele = false;
                jumelageValeurDispo.Add(caseDispo);            
            }

            TimeSpan startTime = TimeSpan.FromHours(HEURE_DEBUT); // Doit changer pour cette valeur
            int Difference = 30;
            int Rencontre = DUREE_RENCONTRE; // Doit changer pour cette valeur
            int heureMax = HEURE_FIN; // Doit changer pour cette valeur
            Dictionary<TimeSpan, TimeSpan> heures = new Dictionary<TimeSpan, TimeSpan>();
            Dictionary<string, List<caseDisponibilite>> sortie = new Dictionary<string, List<caseDisponibilite>>();

            for (int i = 0; i < heureMax; i++)
            {
                heures.Add(
                    startTime.Add(TimeSpan.FromMinutes(Difference * i)),
                    startTime.Add(TimeSpan.FromMinutes(Difference * i + Rencontre))
                    );                
            }

            foreach (var e in heures)
            {
                int heureCheckbox = (int)e.Key.TotalMinutes; // Doit changer pour cette valeur
                caseDisponibilite caseToutes = new caseDisponibilite();
                List<caseDisponibilite> values = new List<caseDisponibilite>();
                bool dispoHeuresPresent = false;
                dispoHeuresPresent = jumelageValeurDispo.Exists(x => x.Minutes==heureCheckbox);
                for (int j = 1; j < 6; j++)
                {
                    if (dispoHeuresPresent)
                    {
                        if (jumelageValeurDispo.Exists(x => x.Jour == ((Semaine)j).ToString() && x.Minutes == heureCheckbox))
                        {
                            caseToutes = jumelageValeurDispo.Find(x => x.Jour == ((Semaine)j).ToString() && x.Minutes == heureCheckbox);
                        }
                        else
                        {
                            caseToutes.Jour = ((Semaine)j).ToString();
                            caseToutes.Minutes = heureCheckbox;
                            caseToutes.NomCase = caseToutes.Jour + "-" + caseToutes.Minutes;
                            caseToutes.NbreUsagerMemeDispo = 0;
                            caseToutes.EstDispoMaisJumele = false;
                            caseToutes.EstDispo = false;
                        }
                    }
                    else
                    {
                        caseToutes.Jour = ((Semaine)j).ToString();
                        caseToutes.Minutes = heureCheckbox;
                        caseToutes.NomCase = caseToutes.Jour + "-" + caseToutes.Minutes;
                        caseToutes.NbreUsagerMemeDispo = 0;
                        caseToutes.EstDispoMaisJumele = false;
                        caseToutes.EstDispo = false;
                    }
                    values.Add(caseToutes);
                }
                sortie.Add(
                e.Key.Hours + "h" + e.Key.Minutes.ToString("00") + "-" + e.Value.Hours + "h" + e.Value.Minutes.ToString("00"),
                values);
            }

            return sortie;
        }

        [NonAction]
        private void ListeTypeInscription(int TypeInscription = 0)
        {
            var lInscriptions = db.p_TypeInscription.AsNoTracking().OrderBy(i => i.TypeInscription);
            var slInscription = new List<SelectListItem>();
            slInscription.AddRange(new SelectList(lInscriptions, "id_TypeInscription", "TypeInscription", TypeInscription));
            ViewBag.Inscription = slInscription;
        }

        //[ValidationAccesEnseignant]
        public ActionResult Index(int? page)
        {
            noPage = (page ?? noPage);
            return View(Rechercher().ToPagedList(noPage, 20));
        }

        [NonAction]
        private IEnumerable<Inscription> Rechercher()
        {
            int session = 0;
            int typeinscription = 0;
            bool requeteGetSinonPost = Request.RequestType == "GET" && Session["DernRechEtu"] != null && (string)Session["DernRechEtuUrl"] == Request.Url?.LocalPath;
            if (requeteGetSinonPost)
            {
                var tanciennerech = Session["DernRechEtu"].ToString().Split(';');

                if (tanciennerech.Length != 0)
                {
                    session = Int32.Parse(tanciennerech[1]);
                    ViewBag.Session = session;
                }
                if (tanciennerech.Length != 0)
                {
                    typeinscription = Int32.Parse(tanciennerech[2]);
                    ViewBag.Inscription = typeinscription;
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(Request.Form["Inscription"]))
                {
                    typeinscription = Convert.ToInt32(Request.Form["Inscription"]);
                    ViewBag.Inscription = typeinscription;
                }
                else if (!String.IsNullOrEmpty(Request.Params["Inscription"]))
                {
                    typeinscription = Convert.ToInt32(Request.Params["Inscription"]);
                    ViewBag.Inscription = typeinscription;
                }
                if (!String.IsNullOrEmpty(Request.Form["SelectSession"]))
                {
                    session = Convert.ToInt32(Request.Form["SelectSession"]);
                    ViewBag.Session = session;

                }
                else if (!String.IsNullOrEmpty(Request.Params["Session"]))
                {
                    session = Convert.ToInt32(Request.Params["Session"]);
                    ViewBag.Session = session;

                }
                else if (Request.Form["Session"] == null)
                    session = Convert.ToInt32(db.Session.OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison).FirstOrDefault().id_Sess);
            }

            ListeSession(session);
            ListeTypeInscription(typeinscription);

            Session["DernRechEtu"] = session + ";" + typeinscription + ";" + noPage;
            Session["DernRechEtuUrl"] = Request.Url.LocalPath.ToString();

            var lstEtu = from p in db.Inscription
                         where (p.id_Sess == session || session == 0) &&
                         (p.id_TypeInscription == typeinscription || typeinscription == 0)
                         orderby p.Personne.Nom, p.Personne.Prenom
                         select p;

            return lstEtu.ToList();
        }

        [NonAction]
        private IEnumerable<Inscription> Rechercher(int? Page)
        {
            pageRecue = Page;
            return Rechercher();
        }

        //[ValidationAccesEnseignant]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = db.Inscription.Find(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            return View(inscription);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
