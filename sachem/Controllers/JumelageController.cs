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
        private const int DUREE_RENCONTRE_MINUTES = 90;
        private const int DEMI_HEURE = 30;
        private const int ID_INSCRIPTION_POUR_ELEVE_AIDE = 1;
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

        public void AjoutJumelage(int idTuteur,int idEleveAide, string jour, int minutes)
        {
            int idJour = (int)Enum.Parse(typeof(Semaine), jour);
            Jumelage ajoutJumelage = new Jumelage();
            ajoutJumelage.consecutif = false;
            ajoutJumelage.DateDebut = new DateTime();
            ajoutJumelage.DateFin = null;
            ajoutJumelage.id_Enseignant = 0;
            ajoutJumelage.id_InscEleve = idEleveAide;
            ajoutJumelage.id_InscrTuteur = idTuteur;
            ajoutJumelage.id_Jour = idJour;
            ajoutJumelage.id_Sess = sachem.Models.SessionBag.Current.id_Sess;
            ajoutJumelage.minutes = minutes;
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
        public Dictionary<string, List<DisponibiliteStruct>> RetourneDisponibiliteJumelageUsager(int id, int idTypeInsc, int session, int idCeluiInspecte)
        {
            List<DisponibiliteStruct> jumelageValeurDispo = new List<DisponibiliteStruct>();
            DisponibiliteStruct caseDispo = new DisponibiliteStruct();
            IQueryable<Disponibilite> dispo = db.Disponibilite.Where(x => x.id_Inscription == id);
            IQueryable<Disponibilite> dispoAutres;
            IQueryable<Jumelage> dispoJumele;
            List<Disponibilite> dispoCeluiInspecte = new List<Disponibilite>();

            if (idTypeInsc == ID_INSCRIPTION_POUR_ELEVE_AIDE)
            {
                dispoJumele = db.Jumelage.Where(eleve => eleve.id_InscEleve == id && eleve.id_Sess == session);
                dispoAutres = db.Disponibilite.Where(x => x.id_Inscription != id && x.Inscription.id_TypeInscription != ID_INSCRIPTION_POUR_ELEVE_AIDE);
            }
            else
            {
                dispoJumele = db.Jumelage.Where(tuteur => tuteur.id_InscrTuteur == id && tuteur.id_Sess == session);
                dispoAutres = db.Disponibilite.Where(x => x.id_Inscription != id && x.Inscription.id_TypeInscription == ID_INSCRIPTION_POUR_ELEVE_AIDE);
            }
            foreach (var j in dispoJumele)
            {
                caseDispo.Jour = j.p_Jour.Jour;
                caseDispo.Minutes = j.minutes;
                caseDispo.NomCase = caseDispo.Jour + "-" + caseDispo.Minutes;
                caseDispo.NbreUsagerMemeDispo = 0;
                caseDispo.EstDispo = false;
                caseDispo.EstDispoMaisJumele = true;
                caseDispo.EstDispoEtCompatible = false;
                caseDispo.EstDispoEtCompatibleEtConsecutif = false;
                if (Convert.ToBoolean(!j.consecutif))
                {
                    caseDispo.EstConsecutiveDonc3hrs = false;
                    jumelageValeurDispo.Add(caseDispo);
                }
                else
                {
                    caseDispo.EstConsecutiveDonc3hrs = false;
                    jumelageValeurDispo.Add(caseDispo);
                    for (int k = DEMI_HEURE; k <= DUREE_RENCONTRE_MINUTES; k += DEMI_HEURE)
                    {
                        caseDispo.Minutes = j.minutes + k;
                        caseDispo.NomCase = caseDispo.Jour + "-" + caseDispo.Minutes;
                        caseDispo.EstConsecutiveDonc3hrs = true;
                        jumelageValeurDispo.Add(caseDispo);
                        caseDispo.EstConsecutiveDonc3hrs = true;
                        jumelageValeurDispo.Add(caseDispo);
                    }
                }
            }

            int compteurUsagerAvecMemeDispo = 0;
            
            if (idCeluiInspecte != 0)
            {
                dispoCeluiInspecte = db.Disponibilite.Where(x => x.id_Inscription == idCeluiInspecte).ToList();
            }
            foreach (var d in dispo)
            {
                compteurUsagerAvecMemeDispo = 0;
                caseDispo.EstDispoEtCompatible = false;
                foreach (var a in dispoAutres)
                {
                    if (d.id_Jour == a.id_Jour && d.minutes == a.minutes)
                    {
                        if (!jumelageValeurDispo.Exists(x => x.Jour == a.p_Jour.Jour && x.Minutes == a.minutes))
                        {
                            compteurUsagerAvecMemeDispo++;
                            if (idCeluiInspecte != 0 && dispoCeluiInspecte.Exists(x => x.p_Jour.Jour == a.p_Jour.Jour && x.minutes == a.minutes))
                            {
                                caseDispo.EstDispoEtCompatible = true;
                                caseDispo.EstDispoEtCompatibleEtConsecutif = disponbiliteEstElleConsecutiveDauMoins3hrs(dispo.ToList(), dispoCeluiInspecte, a.id_Jour, a.minutes);
                            }
                        }
                    }
                }
                caseDispo.Jour = d.p_Jour.Jour;
                caseDispo.Minutes = d.minutes;
                caseDispo.NomCase = d.p_Jour.Jour + "-" + d.minutes;
                caseDispo.NbreUsagerMemeDispo = compteurUsagerAvecMemeDispo;
                caseDispo.EstDispo = true;
                caseDispo.EstDispoMaisJumele = jumelageValeurDispo.Exists(
                        x => x.Jour == caseDispo.Jour &&
                        x.EstDispoMaisJumele == true &&
                        x.EstConsecutiveDonc3hrs == false &&
                        ((caseDispo.Minutes > x.Minutes && caseDispo.Minutes < x.Minutes + DUREE_RENCONTRE_MINUTES) ||
                        ((caseDispo.Minutes + DUREE_RENCONTRE_MINUTES) > x.Minutes && (caseDispo.Minutes + DUREE_RENCONTRE_MINUTES) < x.Minutes + DUREE_RENCONTRE_MINUTES))
                        ) || jumelageValeurDispo.Exists(
                            x => x.Jour == caseDispo.Jour &&
                            x.EstDispoMaisJumele == true &&
                            x.EstConsecutiveDonc3hrs == true &&
                            ((caseDispo.Minutes > x.Minutes && caseDispo.Minutes < x.Minutes + (DUREE_RENCONTRE_MINUTES * 2)) ||
                            ((caseDispo.Minutes + DUREE_RENCONTRE_MINUTES) > x.Minutes && (caseDispo.Minutes + DUREE_RENCONTRE_MINUTES) < x.Minutes + (DUREE_RENCONTRE_MINUTES * 2))));   
                jumelageValeurDispo.Add(caseDispo);     
            }

            TimeSpan startTime = TimeSpan.FromHours(HEURE_DEBUT);
            int Difference = DEMI_HEURE;
            int Rencontre = DUREE_RENCONTRE_MINUTES;
            int heureMax = HEURE_FIN;
            Dictionary<TimeSpan, TimeSpan> listeCasesRencontreAu30min = new Dictionary<TimeSpan, TimeSpan>();
            Dictionary<string, List<DisponibiliteStruct>> listeCasesRencontreAfficher = new Dictionary<string, List<DisponibiliteStruct>>();

            for (int i = 0; i < heureMax; i++)
            {
                listeCasesRencontreAu30min.Add(
                    startTime.Add(TimeSpan.FromMinutes(Difference * i)),
                    startTime.Add(TimeSpan.FromMinutes(Difference * i + Rencontre))
                    );                
            }

            foreach (var case30min in listeCasesRencontreAu30min)
            {
                int minutes = (int)case30min.Key.TotalMinutes;
                DisponibiliteStruct caseToutes = new DisponibiliteStruct();
                List<DisponibiliteStruct> values = new List<DisponibiliteStruct>();
                bool dispoHeuresPresent = false;
                dispoHeuresPresent = jumelageValeurDispo.Exists(x => x.Minutes==minutes);
                for (int j = 1; j < 6; j++)
                {
                    if (dispoHeuresPresent)
                    {
                        if (jumelageValeurDispo.Exists(x => x.Jour == ((Semaine)j).ToString() && x.Minutes == minutes))
                        {
                            caseToutes = jumelageValeurDispo.Find(x => x.Jour == ((Semaine)j).ToString() && x.Minutes == minutes);
                        }
                        else
                        {
                            caseToutes.Jour = ((Semaine)j).ToString();
                            caseToutes.Minutes = minutes;
                            caseToutes.NomCase = caseToutes.Jour + "-" + caseToutes.Minutes;
                            caseToutes.NbreUsagerMemeDispo = 0;
                            caseToutes.EstDispoMaisJumele = false;
                            caseToutes.EstDispo = false;
                        }
                    }
                    else
                    {
                        caseToutes.Jour = ((Semaine)j).ToString();
                        caseToutes.Minutes = minutes;
                        caseToutes.NomCase = caseToutes.Jour + "-" + caseToutes.Minutes;
                        caseToutes.NbreUsagerMemeDispo = 0;
                        caseToutes.EstDispoMaisJumele = false;
                        caseToutes.EstDispo = false;
                    }
                    values.Add(caseToutes);
                }
                listeCasesRencontreAfficher.Add(
                String.Format("{0}h{1}-{2}h{3}", case30min.Key.Hours, case30min.Key.Minutes.ToString("00"), case30min.Value.Hours, case30min.Value.Minutes.ToString("00")),
                values);
            }

            return listeCasesRencontreAfficher;
        }

        private bool disponbiliteEstElleConsecutiveDauMoins3hrs(List<Disponibilite> dispos, List<Disponibilite> disposCeluiInspecte, int idJour, int minutes)
        {
            return ((dispos.Exists(x => x.id_Jour == idJour && x.minutes == minutes) &&
                disposCeluiInspecte.Exists(x => x.id_Jour == idJour && x.minutes == minutes)) &&
                (dispos.Exists(x => x.id_Jour == idJour && x.minutes == minutes + DEMI_HEURE) &&
                disposCeluiInspecte.Exists(x => x.id_Jour == idJour && x.minutes == minutes + (DEMI_HEURE))) &&
                (dispos.Exists(x => x.id_Jour == idJour && x.minutes == minutes + (DEMI_HEURE * 2)) &&
                disposCeluiInspecte.Exists(x => x.id_Jour == idJour && x.minutes == minutes + (DEMI_HEURE * 2))) &&
                (dispos.Exists(x => x.id_Jour == idJour && x.minutes == minutes + (DEMI_HEURE * 3)) &&
                disposCeluiInspecte.Exists(x => x.id_Jour == idJour && x.minutes == minutes + (DEMI_HEURE * 3))));
        }

        [NonAction]
        public List<Inscription> RetourneJumeleursPotentiels(int id, int idTypeInsc, int session)
        {
            IOrderedQueryable<Inscription> listeJumeleurs, eleve;
            eleve = from i in db.Inscription
                    where (db.Disponibilite.Any(x => x.id_Inscription == id) &&
                    (i.id_Sess == session) &&
                    (i.id_Inscription == id) &&
                    (i.id_TypeInscription == idTypeInsc))
                    orderby i.Personne.Nom, i.Personne.Prenom
                    select i;

            if (idTypeInsc == ID_INSCRIPTION_POUR_ELEVE_AIDE)
            {
                listeJumeleurs = from i in db.Inscription
                                 where (db.Disponibilite.Any(x => x.id_Inscription != id) &&
                                 (i.id_Sess == session) &&
                                 (i.id_TypeInscription != ID_INSCRIPTION_POUR_ELEVE_AIDE))
                                 orderby i.Personne.Nom, i.Personne.Prenom
                                 select i;
            }
            else
            {
                listeJumeleurs = from i in db.Inscription
                                 where (db.Disponibilite.Any(x => x.id_Inscription != id) &&
                                 (i.id_Sess == session) &&
                                 (i.id_TypeInscription == ID_INSCRIPTION_POUR_ELEVE_AIDE))
                                 orderby i.Personne.Nom, i.Personne.Prenom
                                 select i;
            }

            List<Disponibilite> disposJumeleurs = listeJumeleurs.SelectMany(x=>x.Disponibilite).ToList();
            List<Disponibilite> disposEleve = eleve.SelectMany(x => x.Disponibilite).ToList();
            List<int> jumeleursPotentiels = new List<int>();
            foreach(var dispo in disposEleve)
            {
                if ((disposJumeleurs.Exists(x => (x.id_Jour == dispo.id_Jour) && (x.minutes == dispo.minutes) && (!jumeleursPotentiels.Contains(x.id_Inscription)))))
                {
                    jumeleursPotentiels.AddRange(disposJumeleurs.FindAll(y => y.id_Jour == dispo.id_Jour && y.minutes == dispo.minutes).Select(x => x.id_Inscription).ToList());
                }
            }

            List<Inscription> listeJumeleursPotentiels = new List<Inscription>();            
            foreach (var jumeleur in listeJumeleurs)
            {
                for (int k = 0; k < jumeleursPotentiels.Count(); k++)
                {
                    if (jumeleur.id_Inscription == jumeleursPotentiels[k])
                    {
                        listeJumeleursPotentiels.Add(jumeleur);
                    }
                }
            }
           
            return listeJumeleursPotentiels;
        }

        [NonAction]
        public IQueryable<Inscription> RetourneJumeleurs(int id, int idTypeInsc, int session)
        {
            IOrderedQueryable<Inscription> listeJumeleurs;
            if (idTypeInsc == ID_INSCRIPTION_POUR_ELEVE_AIDE)
            {
                listeJumeleurs = from p in db.Inscription
                                 where (db.Jumelage.Any(x => x.id_InscEleve == id) &&
                                 (p.id_Sess == session) &&
                                 (p.id_TypeInscription != ID_INSCRIPTION_POUR_ELEVE_AIDE))
                                 orderby p.Personne.Nom, p.Personne.Prenom
                                 select p;
            }
            else
            {
                listeJumeleurs = from p in db.Inscription
                                 where (db.Jumelage.Any(x => x.id_InscrTuteur == id) &&
                                 (p.id_Sess == session) &&
                                 (p.id_TypeInscription == ID_INSCRIPTION_POUR_ELEVE_AIDE))
                                 orderby p.Personne.Nom, p.Personne.Prenom
                                 select p;
            }

            return listeJumeleurs;
        }

        [NonAction]
        public List<string> RetournePlageHoraireChaqueJumeleur(int idVu, int idTypeInsc, int session, int idJumeleur)
        {
            List<string> plageHoraire = new List<string>();
            TimeSpan DebutJournee = new TimeSpan();
            IQueryable<Jumelage> jumeleur;
            if (idTypeInsc == ID_INSCRIPTION_POUR_ELEVE_AIDE)
                jumeleur = db.Jumelage.Where(x => x.id_InscEleve == idVu && x.id_InscrTuteur == idJumeleur && x.id_Sess == session);
            else
                jumeleur = db.Jumelage.Where(x => x.id_InscrTuteur == idVu && x.id_InscEleve == idJumeleur && x.id_Sess == session);
            foreach (var j in jumeleur)
            {
                if (!j.consecutif)
                {                    
                    plageHoraire.Add(
                        j.p_Jour.Jour + " " + 
                        (DebutJournee.Add(TimeSpan.FromMinutes(j.minutes))).ToString(@"hh\:mm") + "-" + 
                        (DebutJournee.Add(TimeSpan.FromMinutes(j.minutes + DUREE_RENCONTRE_MINUTES))).ToString(@"hh\:mm"));
                }
                else
                {
                    plageHoraire.Add(
                        j.p_Jour.Jour + " " + 
                        (DebutJournee.Add(TimeSpan.FromMinutes(j.minutes))).ToString(@"hh\:mm") + "-" + 
                        (DebutJournee.Add(TimeSpan.FromMinutes(j.minutes + (DUREE_RENCONTRE_MINUTES * 2)))).ToString(@"hh\:mm"));
                }
            }
            return plageHoraire;
        }

        public void RetirerJumelage(int idVu, int idJumeleA, int vuTypeInsc)
        {
            if(vuTypeInsc==1)
            {
                int jumRetirerId = db.Jumelage.Where(x => x.id_InscEleve == idVu && x.id_InscrTuteur == idJumeleA).Select(x=>x.id_Jumelage).First();
                var jumRetirer = db.Jumelage.Find(jumRetirerId);
                db.Jumelage.Remove(jumRetirer);
                db.SaveChanges();
            }
            else
            {
                var jumRetirerId = db.Jumelage.Where(x => x.id_InscEleve == idJumeleA && x.id_InscrTuteur == idVu).Select(x => x.id_Jumelage).First();
                var jumRetirer = db.Jumelage.Find(jumRetirerId);
                db.Jumelage.Remove(jumRetirer);
                db.SaveChanges();
            }
            
            ViewBag.Success = "Le jumelage a été retiré.";
        }

        public void AjoutJumelage(int idVu, int idJumeleA, string jour, int minutes, int vuTypeInsc, bool estConsecutif)
        {
            if (vuTypeInsc == 1)
            {
                Jumelage jumCreation = new Jumelage();
                jumCreation.id_Enseignant = 6;
                jumCreation.id_InscEleve = idVu;
                jumCreation.id_InscrTuteur = idJumeleA;
                jumCreation.id_Jour = (int)Enum.Parse(typeof(Semaine), jour);
                jumCreation.minutes = minutes;
                jumCreation.id_Sess = 1;
                jumCreation.DateDebut = DateTime.Now;
                jumCreation.DateFin = DateTime.Now;
                jumCreation.consecutif = false;
                db.Jumelage.Add(jumCreation);
                db.SaveChanges();
            }
            else
            {
                var jumRetirerId = db.Jumelage.Where(x => x.id_InscEleve == idJumeleA && x.id_InscrTuteur == idVu).Select(x => x.id_Jumelage).First();
                var jumRetirer = db.Jumelage.Find(jumRetirerId);
                db.Jumelage.Remove(jumRetirer);
                db.SaveChanges();
            }

            ViewBag.Success = "Le jumelage a été retiré.";
        }

        [NonAction]
        private void ListeTypeInscription(int TypeInscription = 0)
        {
            var lInscriptions = db.p_TypeInscription.AsNoTracking().OrderBy(i => i.TypeInscription);
            var slInscription = new List<SelectListItem>();
            slInscription.AddRange(new SelectList(lInscriptions, "id_TypeInscription", "TypeInscription", TypeInscription));
            ViewBag.Inscription = slInscription;
        }

        [ValidationAccesEnseignant]
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
