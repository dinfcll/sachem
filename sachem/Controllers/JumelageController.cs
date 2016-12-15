using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using sachem.Classes_Sachem;

namespace sachem.Controllers
{
    public class JumelageController : Controller
    {
        private const int HeureDebut = 8;
        private const int HeureFin = 18;
        private const int DureeRencontreMinutes = 90;
        private const int DemiHeure = 30;
        private const int IdInscriptionPourEleveAide = 1;
        private readonly SACHEMEntities _db = new SACHEMEntities();
        protected int NoPage = 1;

        [NonAction]
        public string RetourneNbreJumelageEtudiant(int count)
        {
            string statut;

            if (count == 0)
            {
                statut = "Non jumelé";
            }
            else
            {
                statut = "Jumelé";
                if (count > 1)
                {
                    statut += " (" + count + " fois)";
                }
            }
            return statut;
        }

        [NonAction]
        public List<string> RetourneListeJoursSemaine()
        {
            return Liste.ListeJours();
        }

        [NonAction]
        public Dictionary<string, List<DisponibiliteStruct>> RetourneDisponibiliteJumelageUsager(int id, int idTypeInsc, int session, int idCeluiInspecte)
        {
            var listeCasesJumelageEtDisposCeluiInspecte = new List<DisponibiliteStruct>();
            var caseDispoStruct = new DisponibiliteStruct();
            var reqDisposCeluiInspecte = _db.Disponibilite.Where(x => x.id_Inscription == id);
            IQueryable<Disponibilite> reqDisposDeTousLesAutres;
            IQueryable<Jumelage> reqJumelagesCeluiInspecte;
            var listeDisposCeluiInspecte = new List<Disponibilite>();

            if (idTypeInsc == IdInscriptionPourEleveAide)
            {
                reqJumelagesCeluiInspecte = _db.Jumelage.Where(eleve => eleve.id_InscEleve == id && eleve.id_Sess == session);
                reqDisposDeTousLesAutres = _db.Disponibilite.Where(x => x.id_Inscription != id && x.Inscription.id_TypeInscription != IdInscriptionPourEleveAide);
            }
            else
            {
                reqJumelagesCeluiInspecte = _db.Jumelage.Where(tuteur => tuteur.id_InscrTuteur == id && tuteur.id_Sess == session);
                reqDisposDeTousLesAutres = _db.Disponibilite.Where(x => x.id_Inscription != id && x.Inscription.id_TypeInscription == IdInscriptionPourEleveAide);
            }
            foreach (var jumelageEnRouge in reqJumelagesCeluiInspecte)
            {
                caseDispoStruct.Jour = jumelageEnRouge.p_Jour.Jour;
                caseDispoStruct.Minutes = jumelageEnRouge.minutes;
                caseDispoStruct.NomCase = caseDispoStruct.Jour + "-" + caseDispoStruct.Minutes;
                caseDispoStruct.NbreUsagerMemeDispo = 0;
                caseDispoStruct.EstDispo = false;
                caseDispoStruct.EstDispoMaisJumele = true;
                caseDispoStruct.EstDispoEtCompatible = false;
                caseDispoStruct.HeureDebut = TimeSpan.FromMinutes(caseDispoStruct.Minutes);
                caseDispoStruct.HeureFin = caseDispoStruct.HeureDebut + TimeSpan.FromMinutes(DureeRencontreMinutes);
                caseDispoStruct.NomCaseComplete = $"{caseDispoStruct.Jour} de {caseDispoStruct.HeureDebut.Hours}h{caseDispoStruct.HeureDebut.Minutes:00} à {caseDispoStruct.HeureFin.Hours}h{caseDispoStruct.HeureFin.Minutes:00}";
                caseDispoStruct.EstDispoEtCompatibleEtConsecutif = false;
                if (Convert.ToBoolean(!jumelageEnRouge.consecutif))
                {
                    caseDispoStruct.EstConsecutiveDonc3hrs = false;
                    listeCasesJumelageEtDisposCeluiInspecte.Add(caseDispoStruct);
                }
                else
                {
                    caseDispoStruct.EstConsecutiveDonc3hrs = false;
                    listeCasesJumelageEtDisposCeluiInspecte.Add(caseDispoStruct);
                    for (var k = DemiHeure; k <= DureeRencontreMinutes; k += DemiHeure)
                    {
                        caseDispoStruct.Minutes = jumelageEnRouge.minutes + k;
                        caseDispoStruct.NomCase = caseDispoStruct.Jour + "-" + caseDispoStruct.Minutes;
                        caseDispoStruct.EstConsecutiveDonc3hrs = true;
                        listeCasesJumelageEtDisposCeluiInspecte.Add(caseDispoStruct);
                        caseDispoStruct.EstConsecutiveDonc3hrs = true;
                        listeCasesJumelageEtDisposCeluiInspecte.Add(caseDispoStruct);
                    }
                }
            }

            if (idCeluiInspecte != 0)
            {
                listeDisposCeluiInspecte = _db.Disponibilite.Where(x => x.id_Inscription == idCeluiInspecte).ToList();                
            }
            foreach (var disponibiliteEnVert in reqDisposCeluiInspecte)
            {
                var compteurUsagerAvecMemeDispo = 0;
                caseDispoStruct.EstDispoEtCompatible = false;
                foreach (var a in reqDisposDeTousLesAutres)
                {
                    if (disponibiliteEnVert.id_Jour == a.id_Jour && disponibiliteEnVert.minutes == a.minutes)
                    {
                        if (!listeCasesJumelageEtDisposCeluiInspecte.Exists(x => x.Jour == a.p_Jour.Jour && x.Minutes == a.minutes))
                        {
                            compteurUsagerAvecMemeDispo++;
                            if (idCeluiInspecte != 0 && listeDisposCeluiInspecte.Exists(x => x.p_Jour.Jour == a.p_Jour.Jour && x.minutes == a.minutes) && CaseDispoDoitEtreGrisSiJumelageAffecteDispo(listeCasesJumelageEtDisposCeluiInspecte,caseDispoStruct))
                            {
                                caseDispoStruct.EstDispoEtCompatible = true;
                                caseDispoStruct.EstDispoEtCompatibleEtConsecutif = DisponbiliteEstElleConsecutiveDauMoins3Hrs(reqDisposCeluiInspecte.ToList(), listeDisposCeluiInspecte, a.id_Jour, a.minutes);
                            }
                        }
                    }
                }
                caseDispoStruct.Jour = disponibiliteEnVert.p_Jour.Jour;
                caseDispoStruct.Minutes = disponibiliteEnVert.minutes;
                caseDispoStruct.NomCase = disponibiliteEnVert.p_Jour.Jour + "-" + disponibiliteEnVert.minutes;
                caseDispoStruct.NbreUsagerMemeDispo = compteurUsagerAvecMemeDispo;
                caseDispoStruct.EstDispo = true;
                caseDispoStruct.HeureDebut = TimeSpan.FromMinutes(caseDispoStruct.Minutes);
                caseDispoStruct.HeureFin = caseDispoStruct.HeureDebut + TimeSpan.FromMinutes(DureeRencontreMinutes);
                caseDispoStruct.NomCaseComplete = $"{caseDispoStruct.Jour} de {caseDispoStruct.HeureDebut.Hours}h{caseDispoStruct.HeureDebut.Minutes:00} à {caseDispoStruct.HeureFin.Hours}h{caseDispoStruct.HeureFin.Minutes:00}";
                caseDispoStruct.EstDispoMaisJumele = CaseDispoDoitEtreGrisSiJumelageAffecteDispo(listeCasesJumelageEtDisposCeluiInspecte, caseDispoStruct);
                listeCasesJumelageEtDisposCeluiInspecte.Add(caseDispoStruct);
            }

            var startTime = TimeSpan.FromHours(HeureDebut);
            const int difference = DemiHeure;
            const int rencontre = DureeRencontreMinutes;
            const int heureMax = HeureFin;
            var listeCasesRencontreAu30Min = new Dictionary<TimeSpan, TimeSpan>();
            var listeCasesRencontreAfficher = new Dictionary<string, List<DisponibiliteStruct>>();

            for (var k = 0; k < heureMax; k++)
            {
                listeCasesRencontreAu30Min.Add(
                    startTime.Add(TimeSpan.FromMinutes(difference * k)),
                    startTime.Add(TimeSpan.FromMinutes(difference * k + rencontre))
                    );
            }

            foreach (var case30Min in listeCasesRencontreAu30Min)
            {
                var minutes = (int)case30Min.Key.TotalMinutes;
                var caseToutes = new DisponibiliteStruct();
                var values = new List<DisponibiliteStruct>();
                var dispoHeuresPresent = listeCasesJumelageEtDisposCeluiInspecte.Exists(x => x.Minutes == minutes);

                for (var j = (int)Semaine.Lundi; j < (int)Semaine.Samedi; j++)
                {
                    if (dispoHeuresPresent)
                    {
                        if (listeCasesJumelageEtDisposCeluiInspecte.Exists(x => x.Jour == ((Semaine)j).ToString() && x.Minutes == minutes))
                        {
                            caseToutes = listeCasesJumelageEtDisposCeluiInspecte.Find(x => x.Jour == ((Semaine)j).ToString() && x.Minutes == minutes);
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
                    $"{case30Min.Key.Hours}h{case30Min.Key.Minutes:00}-{case30Min.Value.Hours}h{case30Min.Value.Minutes:00}",
                values);
            }
            return listeCasesRencontreAfficher;
        }

        private static bool CaseDispoDoitEtreGrisSiJumelageAffecteDispo(List<DisponibiliteStruct> listeCasesJumelageEtDisposCeluiInspecte, DisponibiliteStruct caseDispoStruct)
        {
            for (var demiHeure = -(DureeRencontreMinutes-30); demiHeure < DureeRencontreMinutes; demiHeure+=30)
            {
                if(listeCasesJumelageEtDisposCeluiInspecte.Exists(
                        x => x.Jour == caseDispoStruct.Jour && 
                        x.EstDispo == false && x.Minutes + demiHeure == caseDispoStruct.Minutes))
                {
                    return true;
                }
            }
            return false;
        }
        private static bool DisponbiliteEstElleConsecutiveDauMoins3Hrs(List<Disponibilite> dispos, List<Disponibilite> disposCeluiInspecte, int idJour, int minutes)
        {
            return ((dispos.Exists(x => x.id_Jour == idJour && x.minutes == minutes) &&
                disposCeluiInspecte.Exists(x => x.id_Jour == idJour && x.minutes == minutes)) &&
                (dispos.Exists(x => x.id_Jour == idJour && x.minutes == minutes + DemiHeure) &&
                disposCeluiInspecte.Exists(x => x.id_Jour == idJour && x.minutes == minutes + (DemiHeure))) &&
                (dispos.Exists(x => x.id_Jour == idJour && x.minutes == minutes + (DemiHeure * 2)) &&
                disposCeluiInspecte.Exists(x => x.id_Jour == idJour && x.minutes == minutes + (DemiHeure * 2))) &&
                (dispos.Exists(x => x.id_Jour == idJour && x.minutes == minutes + (DemiHeure * 3)) &&
                disposCeluiInspecte.Exists(x => x.id_Jour == idJour && x.minutes == minutes + (DemiHeure * 3))));
        }

        [NonAction]
        public List<Inscription> RetourneJumeleursPotentiels(int id, int idTypeInsc, int session)
        {
            IOrderedQueryable<Inscription> listeJumeleurs;
            var eleve = from i in _db.Inscription
                where (_db.Disponibilite.Any(x => x.id_Inscription == id) &&
                       (i.id_Sess == session) &&
                       (i.id_Inscription == id) &&
                       (i.id_TypeInscription == idTypeInsc))
                orderby i.Personne.Nom, i.Personne.Prenom
                select i;

            if (idTypeInsc == IdInscriptionPourEleveAide)
            {
                listeJumeleurs = from i in _db.Inscription
                                 where (_db.Disponibilite.Any(x => x.id_Inscription != id) &&
                                 (i.id_Sess == session) &&
                                 (i.id_TypeInscription != IdInscriptionPourEleveAide))
                                 orderby i.Personne.Nom, i.Personne.Prenom
                                 select i;
            }
            else
            {
                listeJumeleurs = from i in _db.Inscription
                                 where (_db.Disponibilite.Any(x => x.id_Inscription != id) &&
                                 (i.id_Sess == session) &&
                                 (i.id_TypeInscription == IdInscriptionPourEleveAide))
                                 orderby i.Personne.Nom, i.Personne.Prenom
                                 select i;
            }

            var disposJumeleurs = listeJumeleurs.SelectMany(x=>x.Disponibilite).ToList();
            var disposEleve = eleve.SelectMany(x => x.Disponibilite).ToList();

            if(!disposEleve.Any())
            {
                return new List<Inscription>();
            }

            var jumeleursPotentiels = new List<int>();
            foreach(var dispo in disposEleve)
            {
                if ((disposJumeleurs.Exists(x => (x.id_Jour == dispo.id_Jour) && (x.minutes == dispo.minutes) && (!jumeleursPotentiels.Contains(x.id_Inscription)))))
                {
                    jumeleursPotentiels.AddRange(disposJumeleurs.FindAll(y => y.id_Jour == dispo.id_Jour && y.minutes == dispo.minutes).Select(x => x.id_Inscription).ToList());
                }
            }

            var listeJumeleursPotentiels = new List<Inscription>();
            foreach (var jumeleur in listeJumeleurs)
            {
                listeJumeleursPotentiels.AddRange(from t in jumeleursPotentiels where jumeleur.id_Inscription == t select jumeleur);
            }
           
            return listeJumeleursPotentiels;
        }

        [NonAction]
        public IQueryable<Inscription> RetourneJumeleurs(int id, int idTypeInsc, int session)
        {
            IOrderedQueryable<Inscription> listeJumeleurs;
            if (idTypeInsc == IdInscriptionPourEleveAide)
            {
                listeJumeleurs = from p in _db.Inscription
                                 where (_db.Jumelage.Any(x => x.id_InscEleve == id) &&
                                 (p.id_Sess == session) &&
                                 (p.id_TypeInscription != IdInscriptionPourEleveAide))
                                 orderby p.Personne.Nom, p.Personne.Prenom
                                 select p;
            }
            else
            {
                listeJumeleurs = from p in _db.Inscription
                                 where (_db.Jumelage.Any(x => x.id_InscrTuteur == id) &&
                                 (p.id_Sess == session) &&
                                 (p.id_TypeInscription == IdInscriptionPourEleveAide))
                                 orderby p.Personne.Nom, p.Personne.Prenom
                                 select p;
            }

            return listeJumeleurs;
        }

        [NonAction]
        public List<string> RetournePlageHoraireChaqueJumeleur(int idVu, int idTypeInsc, int session, int idJumeleur)
        {
            var plageHoraire = new List<string>();
            var debutJournee = new TimeSpan();
            var jumeleur = idTypeInsc == IdInscriptionPourEleveAide
                ? _db.Jumelage.Where(x => x.id_InscEleve == idVu && x.id_InscrTuteur == idJumeleur && x.id_Sess == session)
                : _db.Jumelage.Where(x => x.id_InscrTuteur == idVu && x.id_InscEleve == idJumeleur && x.id_Sess == session);

            foreach (var j in jumeleur)
            {
                if (!j.consecutif)
                {
                    plageHoraire.Add(
                        j.p_Jour.Jour + " " +
                        (debutJournee.Add(TimeSpan.FromMinutes(j.minutes))).ToString(@"hh\:mm") + "-" +
                        (debutJournee.Add(TimeSpan.FromMinutes(j.minutes + DureeRencontreMinutes))).ToString(@"hh\:mm"));
                }
                else
                {
                    plageHoraire.Add(
                        j.p_Jour.Jour + " " +
                        (debutJournee.Add(TimeSpan.FromMinutes(j.minutes))).ToString(@"hh\:mm") + "-" +
                        (debutJournee.Add(TimeSpan.FromMinutes(j.minutes + (DureeRencontreMinutes * 2)))).ToString(@"hh\:mm"));
                }
            }
            return plageHoraire;
        }

        public void RetirerJumelage(int idVu, int idJumeleA, int vuTypeInsc)
        {
            if(vuTypeInsc==1)
            {
                var jumRetirerId = _db.Jumelage.Where(x => x.id_InscEleve == idVu && x.id_InscrTuteur == idJumeleA).Select(x=>x.id_Jumelage).First();
                var jumRetirer = _db.Jumelage.Find(jumRetirerId);
                _db.Jumelage.Remove(jumRetirer);
                _db.SaveChanges();
            }
            else
            {
                var jumRetirerId = _db.Jumelage.Where(x => x.id_InscEleve == idJumeleA && x.id_InscrTuteur == idVu).Select(x => x.id_Jumelage).First();
                var jumRetirer = _db.Jumelage.Find(jumRetirerId);
                _db.Jumelage.Remove(jumRetirer);
                _db.SaveChanges();
            }

            ViewBag.Success = Messages.JumelageSupprime;
        }

        public void AjoutJumelage(int idVu, int idJumeleA, string jour, int minutes, int vuTypeInsc, int idEnseignant, int estConsecutif)
        {
            int idInscEleveAide;
            int idInscTuteur;

            if (vuTypeInsc == 1)
            {
                idInscEleveAide = idVu;
                idInscTuteur = idJumeleA;
            }
            else
            {
                idInscEleveAide = idJumeleA;
                idInscTuteur = idVu;
            }

            var jumCreation = new Jumelage
            {
                id_Enseignant = idEnseignant,
                id_InscEleve = idInscEleveAide,
                id_InscrTuteur = idInscTuteur,
                id_Jour = (int) Enum.Parse(typeof(Semaine), jour),
                minutes = minutes
            };

            var idSess = _db.Inscription.Where(x => x.id_Inscription == idVu).Select(x => x.id_Sess);

            jumCreation.id_Sess = idSess.FirstOrDefault();
            jumCreation.DateDebut = DateTime.Now;
            jumCreation.DateFin = DateTime.Now;
            jumCreation.consecutif = Convert.ToBoolean(estConsecutif);
            _db.Jumelage.Add(jumCreation);
            _db.SaveChanges();

            ViewBag.Success = Messages.JumelageAjoute;
        }

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Index(int? page)
        {
            NoPage = (page ?? NoPage);
            return View(Rechercher().ToPagedList(NoPage, 20));
        }

        [NonAction]
        private IEnumerable<Inscription> Rechercher()
        {
            var session = 0;
            var typeinscription = 0;
            var requeteGetSinonPost = Request.RequestType == "GET" && Session["DernRechEtu"] != null && (string)Session["DernRechEtuUrl"] == Request.Url?.LocalPath;

            if (requeteGetSinonPost)
            {
                var tanciennerech = Session["DernRechEtu"].ToString().Split(';');

                if (tanciennerech.Length != 0)
                {
                    session = int.Parse(tanciennerech[1]);
                    ViewBag.Session = session;
                }
                if (tanciennerech.Length != 0)
                {
                    typeinscription = int.Parse(tanciennerech[2]);
                    ViewBag.Inscription = typeinscription;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.Form["Inscription"]))
                {
                    typeinscription = Convert.ToInt32(Request.Form["Inscription"]);
                    ViewBag.Inscription = typeinscription;
                }
                else if (!string.IsNullOrEmpty(Request.Params["Inscription"]))
                {
                    typeinscription = Convert.ToInt32(Request.Params["Inscription"]);
                    ViewBag.Inscription = typeinscription;
                }
                if (!string.IsNullOrEmpty(Request.Form["SelectSession"]))
                {
                    session = Convert.ToInt32(Request.Form["SelectSession"]);
                    ViewBag.Session = session;

                }
                else if (!string.IsNullOrEmpty(Request.Params["Session"]))
                {
                    session = Convert.ToInt32(Request.Params["Session"]);
                    ViewBag.Session = session;

                }
                else if (Request.Form["Session"] == null)
                {
                    var firstOrDefault = _db.Session.OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison).FirstOrDefault();

                    if (firstOrDefault != null)
                    {
                        session = Convert.ToInt32(firstOrDefault.id_Sess);
                    }
                }
            }

            ViewBag.Session = Liste.ListeSession(session);
            ViewBag.Inscription = Liste.ListeTypeInscription(typeinscription);

            Session["DernRechEtu"] = session + ";" + typeinscription + ";" + NoPage;
            if (Request.Url != null) Session["DernRechEtuUrl"] = Request.Url.LocalPath;

            var lstEtu = from p in _db.Inscription
                         where (p.id_Sess == session || session == 0) &&
                         (p.id_TypeInscription == typeinscription || typeinscription == 0)
                         orderby p.Personne.Nom, p.Personne.Prenom
                         select p;

            return lstEtu.ToList();
        }

        [NonAction]
        private IEnumerable<Personne> ObtenirListeSuperviseur()
        {
            var lstEnseignant = from p in _db.Personne
                                where p.id_TypeUsag == 2 && p.Actif
                                orderby p.Nom, p.Prenom
                                select p;

            return lstEnseignant.ToList();
        }

        private void ListeSuperviseur(int superviseur)
        {
            ViewBag.Superviseur = new SelectList(ObtenirListeSuperviseur(), "id_Pers", "NomPrenom", superviseur);
        }

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var inscription = _db.Inscription.Find(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            ListeSuperviseur(0);
            return View(inscription);
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
