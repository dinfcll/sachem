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
    public class JumelageController : Controller
    {
        private const int HEURE_DEBUT = 8;
        private const int HEURE_FIN = 18;
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
            Jours.Add("Lundi");
            Jours.Add("Mardi");
            Jours.Add("Mercredi");
            Jours.Add("Jeudi");
            Jours.Add("Vendredi");
            return Jours.ToList();
        }

        [NonAction]
        public Dictionary<string, List<string>> RetourneTableauDisponibilite()
        {
            TimeSpan StartTime = TimeSpan.FromHours(8);
            int Difference = 30;
            int Rencontre = db.p_dureeRencontre.FirstOrDefault().duree;
            int EntriesCount = 18;
            string[] jour = new string[5] { "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi" };
            Dictionary<TimeSpan, TimeSpan> Entree = new Dictionary<TimeSpan, TimeSpan>();
            Dictionary<string, List<string>> Sortie = new Dictionary<string, List<string>>();

            for (int i = 0; i < EntriesCount; i++)
            {
                Entree.Add(StartTime.Add(TimeSpan.FromMinutes(Difference * i)),
                            StartTime.Add(TimeSpan.FromMinutes(Difference * i + Rencontre)));
            }

            foreach (var e in Entree)
            {
                double heureCheckbox = e.Key.TotalMinutes - StartTime.TotalMinutes;
                List<string> values = new List<string>();
                for (int j = 0; j < 5; j++)
                {
                    values.Add(jour[j] + "-" + heureCheckbox.ToString());
                }
                Sortie.Add(
                e.Key.Hours + "h" + e.Key.Minutes.ToString("00") + "-" + e.Value.Hours + "h" + e.Value.Minutes.ToString("00"),
                values);
            }
            return Sortie;
        }

        [NonAction]
        Dictionary<string, Dictionary<int, string>> RetourneDisponibiliteJumelageUsager(int id, int idTypeInsc)
        {            
            List<string>  jumelageValeurDispo = new List<string>();            
            IQueryable<Disponibilite> dispo = db.Disponibilite.Where(x => x.id_Inscription == id);
            for (int i = 0; i < dispo.ToList().Count; i++)
            {
                jumelageValeurDispo.Add(
                    dispo
                    .Where(x => x.id_Dispo == i)
                    .Select(y => y.p_Jour.Jour)
                    .ToString() 
                    + "-" +
                    dispo
                    .Where(x => x.id_Dispo == i)
                    .Select(y => y.minutes)
                    .ToString()
                    );
            }

            IQueryable<Jumelage> jumelage;
            if (idTypeInsc == 1)
            {
                jumelage = db.Jumelage.Where(eleve => eleve.id_InscEleve == id);
            }
            else
            {
                jumelage = db.Jumelage.Where(tuteur => tuteur.id_InscrTuteur == id);
            }
            for(int j =0;j< jumelage.ToList().Count; j++)
            {
                string dispoJumele = jumelage
                    .Select(x => x.p_Jour.Jour)
                    .ToString()
                    + "-" +
                    jumelage
                    .Select(y => y.minutes)
                    .ToString();
                int indexDispo = jumelageValeurDispo.IndexOf(dispoJumele);
                if (indexDispo != -1)
                    jumelageValeurDispo[indexDispo] = "*" + dispoJumele;
            }

            string[] jour = new string[5] { "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi" };
            TimeSpan StartTime = TimeSpan.FromHours(HEURE_DEBUT);
            int Difference = 30;
            int Rencontre = db.p_dureeRencontre.FirstOrDefault().duree;
            int heureMax = HEURE_FIN;
            Dictionary<TimeSpan, TimeSpan> heures = new Dictionary<TimeSpan, TimeSpan>();
            Dictionary<string, List<string>> Sortie = new Dictionary<string, List<string>>();

            for (int i = 0; i < heureMax; i++)
            {
                heures.Add(StartTime.Add(TimeSpan.FromMinutes(Difference * i)),
                            StartTime.Add(TimeSpan.FromMinutes(Difference * i + Rencontre)));
            }

            foreach (var e in heures)
            {
                double heureCheckbox = e.Key.TotalMinutes;// - StartTime.TotalMinutes; pas necesaire vu que ca peut faire buger le systeme si il change le debut 8h00 a 7h00 tout decalera.
                List<string> values = new List<string>();
                for (int j = 0; j < 5; j++)
                {
                    values.Add(jour[j] + "-" + heureCheckbox.ToString());
                }
                Sortie.Add(
                e.Key.Hours + "h" + e.Key.Minutes.ToString("00") + "-" + e.Value.Hours + "h" + e.Value.Minutes.ToString("00"),
                values);
            }
            return 1;
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

        //public ActionResult Create()
        //{
        //    ViewBag.id_InscEleve = new SelectList(db.Inscription, "id_Inscription", "NoteSup");
        //    ViewBag.id_InscrTuteur = new SelectList(db.Inscription, "id_Inscription", "NoteSup");
        //    ViewBag.id_Jour = new SelectList(db.p_Jour, "id_Jour", "Jour");
        //    ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom");
        //    ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id_Jumelage,id_InscEleve,id_InscrTuteur,id_Sess,id_Enseignant,id_Jour,DateDebut,DateFin,HeureDebut,HeureFin")] Jumelage jumelage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Jumelage.Add(jumelage);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.id_InscEleve = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscEleve);
        //    ViewBag.id_InscrTuteur = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscrTuteur);
        //    ViewBag.id_Jour = new SelectList(db.p_Jour, "id_Jour", "Jour", jumelage.id_Jour);
        //    ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", jumelage.id_Enseignant);
        //    ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", jumelage.id_Sess);
        //    return View(jumelage);
        //}

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Jumelage jumelage = db.Jumelage.Find(id);
        //    if (jumelage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.id_InscEleve = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscEleve);
        //    ViewBag.id_InscrTuteur = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscrTuteur);
        //    ViewBag.id_Jour = new SelectList(db.p_Jour, "id_Jour", "Jour", jumelage.id_Jour);
        //    ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", jumelage.id_Enseignant);
        //    ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", jumelage.id_Sess);
        //    return View(jumelage);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id_Jumelage,id_InscEleve,id_InscrTuteur,id_Sess,id_Enseignant,id_Jour,DateDebut,DateFin,HeureDebut,HeureFin")] Jumelage jumelage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(jumelage).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.id_InscEleve = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscEleve);
        //    ViewBag.id_InscrTuteur = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscrTuteur);
        //    ViewBag.id_Jour = new SelectList(db.p_Jour, "id_Jour", "Jour", jumelage.id_Jour);
        //    ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", jumelage.id_Enseignant);
        //    ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", jumelage.id_Sess);
        //    return View(jumelage);
        //}

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Jumelage jumelage = db.Jumelage.Find(id);
        //    if (jumelage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(jumelage);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Jumelage jumelage = db.Jumelage.Find(id);
        //    db.Jumelage.Remove(jumelage);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
