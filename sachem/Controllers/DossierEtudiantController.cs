﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using System.Security.Cryptography;// pour encripter mdp
using System.Text;
using System.Web.Services;

namespace sachem.Controllers
{

    public class DossierEtudiantController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();
        protected int noPage = 1;
        private int? pageRecue = null;
        List<TypeUsagers> RolesAcces = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super, TypeUsagers.Enseignant, TypeUsagers.Tuteur };
        List<TypeUsagers> RolesAccesDossier = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super, TypeUsagers.Enseignant, TypeUsagers.Tuteur, TypeUsagers.Eleve };

        #region ObtentionRecherche
        [NonAction]
        //liste des sessions disponibles en ordre d'année
        private void ListeSession(int Session = 0)
        {
            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison).Where(s => s.p_Saison.id_Saison == s.id_Saison);
            var slSession = new List<SelectListItem>();
            
            slSession.AddRange(new SelectList(lSessions.OrderBy(i => i.id_Sess), "id_Sess", "NomSession", Session));
            ViewBag.Session = slSession;

        }

        //fonctions permettant d'obtenir la liste des groupe. Appelé pour l'initialisation et la maj de la liste déroulante Groupe
        [NonAction]
        private IEnumerable<Personne> ObtenirListeSuperviseur(int session)
        {
            var lstEnseignant = from p in db.Personne
                                where (db.Jumelage.Any(j => (j.id_Sess == session || session == 0) && j.id_Enseignant == p.id_Pers))
                                && p.id_TypeUsag == 2
                                orderby p.Nom, p.Prenom
                                select p;
            return lstEnseignant.ToList();
        }


        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListeTypeInscription(int TypeInscription = 0)
        {
            var lInscriptions = db.p_TypeInscription.AsNoTracking().OrderBy(i => i.TypeInscription);
            var slInscription = new List<SelectListItem>();
            slInscription.AddRange(new SelectList(lInscriptions, "id_TypeInscription", "TypeInscription", TypeInscription));
            ViewBag.Inscription = slInscription;
        }

        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListeSuperviseur(int session, int superviseur)
        {
            ViewBag.Superviseur = new SelectList(ObtenirListeSuperviseur(session), "id_Pers", "NomPrenom", superviseur);
        }

        #region Fonction Ajax
        /// <summary>
        /// Actualise le dropdownlist des groupes selon l'élément sélectionné dans les dropdownlist Session et Cours
        /// </summary>
        /// 
        [NonAction]
        [AcceptVerbs("Get", "Post")]
        public JsonResult ActualiseSuperviseurddl(int session)
        {
            var a = ObtenirListeSuperviseur(session).Select(c => new { c.id_Pers, c.NomPrenom });
            return Json(a.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Fonction Recherche IEnumerable<Inscription> List
        //Fonction pour gérer la recherche, elle est utilisée dans la suppression et dans l'index
        [NonAction]
        protected IEnumerable<Inscription> Rechercher()
        {

            string matricule = "";
            string prenom = "";
            string nom = "";
            int session = 0;
            int typeinscription = SessionBag.Current.id_Inscription; //si different de 0, il indiquera a la dropdownlist de type inscription que c'est un tuteur et que la list doit etre grise sur son 'eleve aide'
            int superviseur = SessionBag.Current.idSuperviseur; //si different de 0, il indiquera a la dropdownlist de superviseur de mettre le nom de l'enseignant par defaut, si l'enseignant n'est pas superviseur d'un jumelage = 0 = tous.
            superviseur = 0;
            //region recuperation de donnees en GET pour initialiser les drop down listes
            #region recuperer donnees form
            if (Request.RequestType == "GET" && Session["DernRechEtu"] != null && (string)Session["DernRechEtuUrl"] == Request.Url?.LocalPath)
            {//GET
                var anciennerech = (string)Session["DernRechEtu"];
                var tanciennerech = anciennerech.Split(';');

                if (tanciennerech[0] != "")
                {
                    matricule = tanciennerech[0];
                    ViewBag.Matricule = matricule;
                }
                else
                {
                    if (tanciennerech[1] != "")
                    {
                        session = Int32.Parse(tanciennerech[1]);
                        ViewBag.Session = session;
                    }
                    if (tanciennerech[2] != "")
                    {
                        typeinscription = Int32.Parse(tanciennerech[2]);
                        ViewBag.Inscription = typeinscription;
                    }
                    if (tanciennerech[3] != "")
                    {
                        superviseur = Int32.Parse(tanciennerech[3]);
                        ViewBag.Superviseur = superviseur;
                    }

                }
                if (tanciennerech[4] != "")
                {
                    noPage = Int32.Parse(tanciennerech[4]);
                }
            }
            else//POST
            {
                //La méthode String.IsNullOrEmpty permet à la fois de vérifier si la chaine est NULL (lors du premier affichage de la page ou vide, lorsque le paramètre n'est pas appliqué 
                if (!String.IsNullOrEmpty(Request.Form["Matricule"]))
                {
                    matricule = Request.Form["Matricule"];
                    ViewBag.Matricule = matricule;
                }
                else if (!String.IsNullOrEmpty(Request.Params["Matricule"]))
                {
                    matricule = Request.Params["Matricule"];
                    ViewBag.Matricule = matricule;
                }

                if (!String.IsNullOrEmpty(Request.Form["Prenom"]))
                {
                    prenom = Request.Form["Prenom"];
                    ViewBag.Prenom = prenom;
                }

                else if (!String.IsNullOrEmpty(Request.Params["Prenom"]))
                {
                    prenom = Request.Params["Prenom"];
                    ViewBag.Prenom = prenom;
                }

                if (!String.IsNullOrEmpty(Request.Params["Nom"]))
                {
                    nom = Request.Params["Nom"];
                    ViewBag.Nom = nom;
                }
                else if (!String.IsNullOrEmpty(Request.Form["Nom"]))
                {
                    nom = Request.Form["Nom"];
                    ViewBag.Nom = nom;
                }
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
                if (!String.IsNullOrEmpty(Request.Form["Superviseur"]))
                {
                    superviseur = Convert.ToInt32(Request.Form["Superviseur"]);
                    ViewBag.Superviseur = superviseur;
                }
                else if (!String.IsNullOrEmpty(Request.Params["Superviseur"]))
                {
                    superviseur = Convert.ToInt32(Request.Params["Superviseur"]);
                    ViewBag.Superviseur = superviseur;

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
                    session = db.Session.Max(s => s.id_Sess);

            }



            #endregion
            #region traitement donnees resultatReq
            ListeSession(session);
            ListeTypeInscription(typeinscription);
            ListeSuperviseur(session, superviseur);

            //on enregistre la recherche
            Session["DernRechEtu"] = matricule + ";" + session + ";" + typeinscription + ";" + superviseur + ";" + noPage;
            Session["DernRechEtuUrl"] = Request.Url.LocalPath.ToString();

            var lstEtu = from p in db.Inscription
                                    where (db.Jumelage.Any(j => j.id_Enseignant == superviseur || superviseur == 0) &&
                                    (p.id_Sess == session || session == 0) &&
                                    (p.id_TypeInscription == typeinscription || typeinscription == 0) &&
                                    (p.Personne.Prenom.Contains(prenom) || prenom == "") &&
                                    (p.Personne.Nom.Contains(nom) || nom == "") &&
                                    (p.Personne.Matricule.Substring(2).StartsWith(matricule) || matricule == "")
                                                                  )
                                    orderby p.Personne.Nom, p.Personne.Prenom
                                    select p;
                           
            return lstEtu.ToList();
            #endregion
        }
        #endregion

        [NonAction]
        protected IEnumerable<Inscription> Rechercher(int? Page)
        {
            pageRecue = Page;
            return Rechercher();
        }
        #endregion
        // GET: DossierEtudiant
        public ActionResult Index(int? page)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);
            noPage = (page ?? noPage);

            //return View(Rechercher().ToPagedList(noPage, 20));
            return View(Rechercher());
        }

        // GET: DossierEtudiant/Details/5
        public ActionResult Details(int? id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAccesDossier, Session))
                return RedirectToAction("Error", "Home", null);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

             Inscription inscription = db.Inscription.Find(id);


            if (inscription == null)
            {
                return HttpNotFound();
            }

            var vCoursSuivi = from d in db.CoursSuivi
                              where d.id_Pers == inscription.id_Pers
                              select d;

            var vInscription = from d in db.Inscription
                               where d.id_Pers == inscription.id_Pers
                               select d;            

            ViewBag.idPers = vInscription.First().id_Pers;
            ViewBag.idTypeInsc = vInscription.First().id_TypeInscription;

            return View(Tuple.Create(inscription, vCoursSuivi.AsEnumerable(), vInscription.AsEnumerable()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(FormCollection model)
        {
            //A completer
            if (!SachemIdentite.ValiderRoleAcces(RolesAccesDossier, Session))
                return RedirectToAction("Error", "Home", null);
            var id_Pers = Convert.ToInt32(model["item1.Personne.id_Pers"]);
            var id_Inscription = Convert.ToInt32(model["item1.id_Inscription"]);
            var Courriel = Convert.ToString(model["item1.Personne.Courriel"]);
            var Telephone = Convert.ToString(model["item1.Personne.Telephone"]);
            var BonEchange = model["item1.BonEchange.Value"];

            Personne personne = db.Personne.Find(id_Pers);
            personne.Courriel = Courriel;
            personne.Telephone = SachemIdentite.FormatTelephone(Telephone);

            Inscription inscription = db.Inscription.Find(id_Inscription);

            var vCoursSuivi = from d in db.CoursSuivi
                              where d.id_Pers == inscription.id_Pers
                              select d;

            var vInscription = from d in db.Inscription
                               where d.id_Pers == inscription.id_Pers
                               select d;

            ViewBag.idPers = vInscription.First().id_Pers;
            ViewBag.idTypeInsc = vInscription.First().id_TypeInscription;

            if (ModelState.IsValid)
            {
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();

            }
            return View(Tuple.Create(inscription, vCoursSuivi.AsEnumerable(), vInscription.AsEnumerable()));
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