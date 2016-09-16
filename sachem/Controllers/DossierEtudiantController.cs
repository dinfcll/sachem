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

        #region ObtentionRecherche
        [NonAction]
        //liste des sessions disponibles en ordre d'année
        private void ListeSession(int Session = 0)
        {
            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison).Where(s => s.p_Saison.id_Saison == s.id_Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", Session));
            ViewBag.Session = slSession;

        }

        //fonctions permettant d'obtenir la liste des groupe. Appelé pour l'initialisation et la maj de la liste déroulante Groupe
        [NonAction]
        private IEnumerable<Jumelage> ObtenirListeSuperviseur(int session)
        {
            int Pers = 0;
            var ResultReq = db.Jumelage.AsNoTracking().Where(p => (p.Personne.id_Pers== p.id_Enseignant) && (p.id_Sess == session));
            return ResultReq.AsEnumerable();
        }


        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListeTypeInscription(int TypeInscription = 0)
        {
            //ordonnee et tous par default, si tuteurs: griser eleve aide.
            int Pers = 0;
            var lInscriptions = db.p_TypeInscription.AsNoTracking().OrderBy(i => i.TypeInscription);
            var slInscription = new List<SelectListItem>();
            slInscription.AddRange(new SelectList(lInscriptions, "id_TypeInscription", "TypeInscription", TypeInscription));
            ViewBag.Inscription = slInscription;
        }

        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListeSuperviseur(int session, int superviseur)
        {
            ViewBag.Superviseur = new SelectList(ObtenirListeSuperviseur(session), "id_Superviseur", "Superviseur", superviseur);
        }

        #region Fonctions Ajax
        /// <summary>
        /// Actualise le dropdownlist des groupes selon l'élément sélectionné dans les dropdownlist Session et Cours
        /// </summary>
        [AcceptVerbs("Get", "Post")]
        public JsonResult ActualiseSuperviseurddl(int session)
        {
            var a = ObtenirListeSuperviseur(session).Select(c => new { c.Personne.id_Pers, c.Personne.NomPrenom });
            return Json(a.ToList(), JsonRequestBehavior.AllowGet);
        }


        //Fonction pour gérer la recherche, elle est utilisée dans la suppression et dans l'index
        [NonAction]
        protected IEnumerable<Inscription> Rechercher()
        {
            var matricule = "";
            var prenom = "";
            var nom = "";
            var session = 0;
            var typeinscription = 0;
            var superviseur = 0;
            var champsRenseignes = 0;
            IEnumerable<Inscription> lstEtu = new List<Inscription>();

            //Pour accéder à la valeur de cle envoyée en GET dans le formulaire
            //Request.QueryString["cle"]
            //Pour accéder à la valeur cle envoyée en POST dans le formulaire
            //Request.Form["cle"]
            //Cette méthode fonctionnera dans les 2 cas
            //Request["cle"]

            if (Request.RequestType == "GET" && Session["DernRechEtu"] != null && (string)Session["DernRechEtuUrl"] == Request.Url?.LocalPath)
            {
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
                        champsRenseignes++;
                    }
                    if (tanciennerech[2] != "")
                    {
                        typeinscription = Int32.Parse(tanciennerech[2]);
                        ViewBag.Inscription = typeinscription;
                        champsRenseignes++;
                    }
                    if (tanciennerech[3] != "")
                    {
                        superviseur = Int32.Parse(tanciennerech[3]);
                        ViewBag.Superviseur = superviseur;
                        champsRenseignes++;
                    }

                }
                if (tanciennerech[4] != "")
                {
                    noPage = Int32.Parse(tanciennerech[4]);
                }
            }
            else
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
                else
                {  //si la recherche n'est pas effectuée sur le matricule, obtenir les autres champs

                    if (!String.IsNullOrEmpty(Request.Form["SelectTypeInscription"]))
                    {
                        typeinscription = Convert.ToInt32(Request.Form["SelectTypeInscription"]);
                        ViewBag.Inscription = typeinscription;
                        champsRenseignes++;
                    }
                    else if (!String.IsNullOrEmpty(Request.Params["typeinscription"]))
                    {
                        typeinscription = Convert.ToInt32(Request.Params["typeinscription"]);
                        ViewBag.Inscription = typeinscription;
                        champsRenseignes++;
                    }
                    if (!String.IsNullOrEmpty(Request.Form["SelectSuperviseur"]))
                    {
                        superviseur = Convert.ToInt32(Request.Form["SelectSuperviseur"]);
                        ViewBag.Superviseur = superviseur;
                        champsRenseignes++;
                    }
                    else if (!String.IsNullOrEmpty(Request.Params["Superviseur"]))
                    {
                        superviseur = Convert.ToInt32(Request.Params["Superviseur"]);
                        ViewBag.Superviseur = superviseur;
                        champsRenseignes++;
                    }
                    if (!String.IsNullOrEmpty(Request.Form["SelectSession"]))
                    {
                        session = Convert.ToInt32(Request.Form["SelectSession"]);
                        ViewBag.Session = session;
                        champsRenseignes++;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Request.Params["Session"]))
                        {
                            session = Convert.ToInt32(Request.Params["Session"]);
                            ViewBag.Session = session;
                            champsRenseignes++;
                        }
                        else if (Request.Form["Session"] == null)
                            session = db.Session.Max(s => s.id_Sess);

                    }

                }
            }

            //si un des champs de recherche est absent
            if (champsRenseignes != 3 && champsRenseignes != 0)
            {
                ModelState.AddModelError(string.Empty, Messages.I_039());
            }

            ListeSession(session);
            ListeTypeInscription(typeinscription);
            ListeSuperviseur(session, superviseur);

            //on enregistre la recherche
            Session["DernRechEtu"] = matricule + ";" + session + ";" + typeinscription + ";" + superviseur + ";" + noPage;
            Session["DernRechEtuUrl"] = Request.Url.LocalPath.ToString();

            if (ModelState.IsValid)
            {
                /*désactiver le lazyloading dans le contexte de cette unité de traitement
                    on désactive le lazyloading car on veut charger manuellement les entités enfants (puisqu'elles ne doivent pas toutes être chargées)
                    */
                db.Configuration.LazyLoadingEnabled = false;

                if (matricule == "" && prenom == "" && nom == "") //recherche avec les trois champs
                {
                    /*requête LINQ qui va chercher tous les étudiants répondant aux critères de recherche ainsi que leur programme d'étude actuel. */
                    lstEtu = db.Inscription.Include(i => i.p_StatutInscription).Include(i => i.p_TypeInscription).Include(i => i.Personne).Include(i => i.Session)
                        .Where(i => i.Session.id_Sess==session && i.id_TypeInscription==typeinscription && i.Personne.id_Pers==superviseur);
                }
                else if (matricule == "" && (prenom != "" || nom != ""))
                {
                    //recherche sur le nom ou le prenom
                    if (prenom != "" && nom == "")
                    {
                        lstEtu = db.Inscription.Include(i => i.p_StatutInscription).Include(i => i.p_TypeInscription).Include(i => i.Personne).Include(i => i.Session)
                            .Where(i => i.Personne.Prenom == prenom);
                    }
                    else if(prenom=="" && nom != "")
                    {
                        lstEtu = db.Inscription.Include(i => i.p_StatutInscription).Include(i => i.p_TypeInscription).Include(i => i.Personne).Include(i => i.Session)
                            .Where(i => i.Personne.Nom == nom);
                    }
                    else
                    {
                        lstEtu = db.Inscription.Include(i => i.p_StatutInscription).Include(i => i.p_TypeInscription).Include(i => i.Personne).Include(i => i.Session)
                            .Where(i => i.Personne.Nom == nom && i.Personne.Prenom == prenom);
                    }
                }
                else
                {
                    //recherche sur le matricule
                    lstEtu = db.Inscription.Include(i => i.p_StatutInscription).Include(i => i.p_TypeInscription).Include(i => i.Personne).Include(i => i.Session)
                            .Where(i => i.Personne.Matricule7==matricule);
                }

                db.Configuration.LazyLoadingEnabled = true;
            }

            /*var personne = from c in db.Personne where c.Actif == true && c.id_TypeUsag == 1 select c;
            foreach (var pers in personne)
            {
                var pidEtu = (from p in db.EtuProgEtude where pers.id_Pers == p.id_Etu orderby p.id_Sess descending select p).FirstOrDefault();
                var pEtu = db.ProgrammeEtude.Find(pidEtu.id_ProgEtu);
                pers.ProgEtu = pEtu.NomProg.ToString();

            }*/


            return lstEtu;
        }




        #endregion
        [NonAction]
        protected IEnumerable<Inscription> Rechercher(int? Page)
        {
            pageRecue = Page;
            return Rechercher();
        }

        // GET: DossierEtudiant
        public ActionResult Index(int? page)
        {
            //ListeSession();
            //ListeTypeInscription(0,0);


            //return View(Rechercher().ToPagedList(pageNumber, 20));
            //var inscription = db.Inscription.Include(i => i.p_StatutInscription).Include(i => i.p_TypeInscription).Include(i => i.Personne).Include(i => i.Session);
            //return View(inscription().ToPagedList(pageNumber, 20));

            noPage = (page ?? noPage);

            //var inscription = db.Inscription.Include(i => i.p_StatutInscription).Include(i => i.p_TypeInscription).Include(i => i.Personne).Include(i => i.Session);
            return View(Rechercher().ToPagedList(noPage, 20));
        }

        // GET: DossierEtudiant/Details/5
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

            var vCoursSuivi = from d in db.CoursSuivi
                              where d.id_Pers == inscription.id_Pers
                              select d;

            var vInscription = from d in db.Inscription
                               where d.id_Pers == inscription.id_Pers
                             select d;
            
            return View(Tuple.Create(inscription, vCoursSuivi.AsEnumerable(), vInscription.AsEnumerable()));
        }

        [NonAction]
        [HttpPost, ActionName("Modifier")]
        public ActionResult Modifier()
        {
            return View();
        }

        // GET: DossierEtudiant/Create
        public ActionResult Create()
        {
            ViewBag.id_Statut = new SelectList(db.p_StatutInscription, "id_Statut", "Statut");
            ViewBag.id_TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            ViewBag.id_Pers = new SelectList(db.Personne, "id_Pers", "Nom");
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess");
            return View();
        }

        // POST: DossierEtudiant/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
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
        #endregion
    }
}
