using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using static sachem.Classes_Sachem.ValidationAcces;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    public class DossierEtudiantController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();
        protected int noPage = 1;
        private int? pageRecue = null;

        private readonly IDataRepository dataRepository;

        public DossierEtudiantController()
        {
            dataRepository = new BdRepository();
        }

        public DossierEtudiantController(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        [NonAction]
        //liste des sessions disponibles en ordre d'année
        private void ListeSession(int session = 0)
        {
            var lSessions = db.Session.AsNoTracking().OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", session));
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

            if (Request.RequestType == "GET" && Session["DernRechEtu"] != null && (string)Session["DernRechEtuUrl"] == Request.Url?.LocalPath)
            {//GET
                var tanciennerech = Session["DernRechEtu"].ToString().Split(';');

                if (tanciennerech[0].Length != 0)
                {
                    matricule = tanciennerech[0];
                    ViewBag.Matricule = matricule;
                }
                else
                {
                    if (tanciennerech[1].Length != 0)
                    {
                        session = Int32.Parse(tanciennerech[1]);
                        ViewBag.Session = session;
                    }
                    if (tanciennerech[2].Length != 0)
                    {
                        typeinscription = Int32.Parse(tanciennerech[2]);
                        ViewBag.Inscription = typeinscription;
                    }
                    if (tanciennerech[3].Length != 0)
                    {
                        superviseur = Int32.Parse(tanciennerech[3]);
                        ViewBag.Superviseur = superviseur;
                    }
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
                    session = Convert.ToInt32(db.Session.OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison).FirstOrDefault().id_Sess);
            }

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
        }

        [NonAction]
        protected IEnumerable<Inscription> Rechercher(int? Page)
        {
            pageRecue = Page;
            return Rechercher();
        }

        // GET: DossierEtudiant
        [ValidationAccesEleve]
        public ActionResult Index(int? page)
        {
            noPage = (page ?? noPage);
            return View(Rechercher().ToPagedList(noPage, 20));
        }

        // GET: DossierEtudiant/Details/5
        [ValidationAccesEleve]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Inscription inscription = db.Inscription.Find(id);
            var inscription = dataRepository.FindInscription(id.Value);

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
        [ValidationAccesTuteur]
        public void ModifBon(bool bon, string insc)
        {
            var id_Inscription = Convert.ToInt32(insc);

            Inscription inscription = db.Inscription.Find(id_Inscription);

            inscription.BonEchange = bon; 

            db.Entry(inscription).State = EntityState.Modified;
            db.SaveChanges();
        }

        [HttpPost]
        [ValidationAccesEleve]
        public void ModifEmail(string email, string pers)
        {
            var id_Pers = Convert.ToInt32(pers);

            Personne personne = db.Personne.Find(id_Pers);

            personne.Courriel = email;

            db.Entry(personne).State = EntityState.Modified;
            db.SaveChanges();
        }

        [HttpPost]
        [ValidationAccesEleve]
        public void ModifTel(string tel, string pers)
        {
            var id_Pers = Convert.ToInt32(pers);

            Personne personne = db.Personne.Find(id_Pers);

            personne.Telephone = SachemIdentite.FormatTelephone(tel);

            db.Entry(personne).State = EntityState.Modified;
            db.SaveChanges();
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
