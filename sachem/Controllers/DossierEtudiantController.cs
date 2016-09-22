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
using System.Security.Cryptography;// pour encripter mdp
using System.Text;
using System.Web.Services;

namespace sachem.Controllers
{
    public class DossierEtudiantController : Controller
    {
        //test
        private SACHEMEntities db = new SACHEMEntities();
        protected int noPage = 1;
        private int? pageRecue = null;
        List<TypeUsagers> RolesAcces = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super, TypeUsagers.Enseignant, TypeUsagers.Tuteur };

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
            //int Pers = 0;
            //var ResultReqJum = db.Jumelage.AsNoTracking()
            //    .Where(p => (p.Personne.id_Pers == p.id_Enseignant) && (p.id_Sess == session)).Distinct();
            //var ResultReq = db.Personne.AsNoTracking().Include(ResultReqJum.Where(j => j.id_Enseignant == j.Personne.id_Pers)
            //    .Where(p => ((p.Personne.p_TypeUsag.id_TypeUsag == 2) && (p.id_Enseignant == p.Personne.id_Pers))));
            var lstEnseignant = from p in db.Personne
                                where (db.Jumelage.Any(j => (j.id_Sess == session || session == 0) && j.id_Enseignant == p.id_Pers))
                                && p.id_TypeUsag == 2
                                orderby p.Nom, p.Prenom
                                select p;
            if (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Enseignant)
                return lstEnseignant.ToList().Where(i => i.id_Pers == SessionBag.Current.id_Pers);
            else
                return lstEnseignant.ToList();

        //    return ResultReq.AsEnumerable();
        }


        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListeTypeInscription(int TypeInscription = 0)
        {
            //ordonnee et tous par default, si tuteurs: griser eleve aide.
            int Pers = 0;
            if (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Tuteur)
                TypeInscription = 1;
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
            var id = 0;
            var matricule = "";
            var prenom = "";
            var nom = "";
            var session = 0;
            var typeinscription = 0;
            var superviseur = 0;

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
                                    where (
                                    db.Jumelage.Any(j => j.id_Enseignant == superviseur || superviseur == 0) &&
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
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
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

            return View(Tuple.Create(inscription, vCoursSuivi.AsEnumerable(), vInscription.AsEnumerable()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(FormCollection model)
        {
            var id_Pers = Convert.ToInt32(model["item1.Personne.id_Pers"]);
            var id_Inscription = Convert.ToInt32(model["item1.id_Inscription"]);
            var Courriel = Convert.ToString(model["item1.Personne.Courriel"]);
            var NumTelephone = Convert.ToInt64(model["item1.Personne.NumTelephone"]);
            var BonEchange = model["item1.BonEchange"];

            Personne personne = db.Personne.Find(id_Pers);
            personne.Courriel = Courriel;
            personne.NumTelephone = NumTelephone;

            Inscription inscription = db.Inscription.Find(id_Inscription);

            var vCoursSuivi = from d in db.CoursSuivi
                              where d.id_Pers == inscription.id_Pers
                              select d;

            var vInscription = from d in db.Inscription
                               where d.id_Pers == inscription.id_Pers
                               select d;

            ViewBag.idPers = vInscription.First().id_Pers;

            if (ModelState.IsValid)
            {
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();

            }
            return View(Tuple.Create(inscription, vCoursSuivi.AsEnumerable(), vInscription.AsEnumerable()));
        }

        public static string FormatTelephone(string s)
        {
            var charsToRemove = new string[] { ".", "-", "(", " ", ")" };
            foreach (var c in charsToRemove)
            {
                s = s.Replace(c, string.Empty);
            }
            return s;
        }
        //fonction qui remet le numéro de téléphone dans le bon format
        public static string RemettreTel(string a)

        {
            string modif;
            modif = a.Insert(0, "(");
            modif = modif.Insert(4, ")");
            modif = modif.Insert(5, " ");
            modif = modif.Insert(9, "-");
            return modif;
        }

        // GET: DossierEtudiant/Create
        public ActionResult Create()
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);
            ViewBag.id_Statut = new SelectList(db.p_StatutInscription, "id_Statut", "Statut");
            ViewBag.id_TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            ViewBag.id_Pers = new SelectList(db.Personne, "id_Pers", "Nom");
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess");
            return View();
        }

        // POST: DossierEtudiant/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Inscription,id_Sess,id_Pers,id_Statut,id_TypeInscription,TransmettreInfoTuteur,NoteSup,ContratEngagement,BonEchange,DateInscription")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
                db.Inscription.Add(inscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Statut = new SelectList(db.p_StatutInscription, "id_Statut", "Statut", inscription.id_Statut);
            ViewBag.id_TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription", inscription.id_TypeInscription);
            ViewBag.id_Pers = new SelectList(db.Personne, "id_Pers", "Nom", inscription.id_Pers);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", inscription.id_Sess);
            return View(inscription);
        }

        // GET: DossierEtudiant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
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


            ViewBag.id_Statut = new SelectList(db.p_StatutInscription, "id_Statut", "Statut", inscription.id_Statut);
            ViewBag.id_TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription", inscription.id_TypeInscription);
            ViewBag.id_Pers = new SelectList(db.Personne, "id_Pers", "Nom", inscription.id_Pers);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", inscription.id_Sess);
            return View(inscription);
        }

        // POST: DossierEtudiant/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Inscription,id_Sess,id_Pers,id_Statut,id_TypeInscription,TransmettreInfoTuteur,NoteSup,ContratEngagement,BonEchange,DateInscription")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Statut = new SelectList(db.p_StatutInscription, "id_Statut", "Statut", inscription.id_Statut);
            ViewBag.id_TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription", inscription.id_TypeInscription);
            ViewBag.id_Pers = new SelectList(db.Personne, "id_Pers", "Nom", inscription.id_Pers);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", inscription.id_Sess);
            return View(inscription);
        }

        // GET: DossierEtudiant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
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
            return View(inscription);
        }

        // POST: DossierEtudiant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inscription inscription = db.Inscription.Find(id);
            db.Inscription.Remove(inscription);
            db.SaveChanges();
            return RedirectToAction("Index");
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
