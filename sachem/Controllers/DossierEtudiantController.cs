using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using sachem.Classes_Sachem;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    public class DossierEtudiantController : Controller
    {
        private readonly SACHEMEntities _db = new SACHEMEntities();
        protected int NoPage = 1;
        private readonly IDataRepository _dataRepository;

        public DossierEtudiantController()
        {
            _dataRepository = new BdRepository();
        }

        public DossierEtudiantController(IDataRepository dataRepository)
        {
            this._dataRepository = dataRepository;
        }

        private IEnumerable<Personne> ObtenirListeSuperviseur(int session)
        {
            var lstEnseignant = from p in _db.Personne
                                where (_db.Jumelage.Any(j => (j.id_Sess == session || session == 0) && j.id_Enseignant == p.id_Pers))
                                && p.id_TypeUsag == 2
                                orderby p.Nom, p.Prenom
                                select p;
            return lstEnseignant.ToList();
        }

        private void ListeSuperviseur(int session, int superviseur)
        {
            ViewBag.Superviseur = new SelectList(ObtenirListeSuperviseur(session), "id_Pers", "NomPrenom", superviseur);
        }

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

            var matricule = "";
            var prenom = "";
            var nom = "";
            var session = 0;
            int typeinscription = SessionBag.Current.id_Inscription;
            int superviseur = SessionBag.Current.idSuperviseur;

            if (Request.RequestType == "GET" && Session["DernRechEtu"] != null && (string)Session["DernRechEtuUrl"] == Request.Url?.LocalPath)
            {
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
                        session = int.Parse(tanciennerech[1]);
                        ViewBag.Session = session;
                    }
                    if (tanciennerech[2].Length != 0)
                    {
                        typeinscription = int.Parse(tanciennerech[2]);
                        ViewBag.Inscription = typeinscription;
                    }
                    if (tanciennerech[3].Length != 0)
                    {
                        superviseur = int.Parse(tanciennerech[3]);
                        ViewBag.Superviseur = superviseur;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.Form["Matricule"]))
                {
                    matricule = Request.Form["Matricule"];
                    ViewBag.Matricule = matricule;
                }
                else if (!string.IsNullOrEmpty(Request.Params["Matricule"]))
                {
                    matricule = Request.Params["Matricule"];
                    ViewBag.Matricule = matricule;
                }

                if (!string.IsNullOrEmpty(Request.Form["Prenom"]))
                {
                    prenom = Request.Form["Prenom"];
                    ViewBag.Prenom = prenom;
                }

                else if (!string.IsNullOrEmpty(Request.Params["Prenom"]))
                {
                    prenom = Request.Params["Prenom"];
                    ViewBag.Prenom = prenom;
                }

                if (!string.IsNullOrEmpty(Request.Params["Nom"]))
                {
                    nom = Request.Params["Nom"];
                    ViewBag.Nom = nom;
                }
                else if (!string.IsNullOrEmpty(Request.Form["Nom"]))
                {
                    nom = Request.Form["Nom"];
                    ViewBag.Nom = nom;
                }
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
                if (!string.IsNullOrEmpty(Request.Form["Superviseur"]))
                {
                    superviseur = Convert.ToInt32(Request.Form["Superviseur"]);
                    ViewBag.Superviseur = superviseur;
                }
                else if (!string.IsNullOrEmpty(Request.Params["Superviseur"]))
                {
                    superviseur = Convert.ToInt32(Request.Params["Superviseur"]);
                    ViewBag.Superviseur = superviseur;

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
                    session = Convert.ToInt32(_db.Session.OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison).FirstOrDefault().id_Sess);
            }

            ViewBag.Session = Liste.ListeSession(session);
            ViewBag.Inscription = Liste.ListeTypeInscription(typeinscription);
            ListeSuperviseur(session, superviseur);

            Session["DernRechEtu"] = matricule + ";" + session + ";" + typeinscription + ";" + superviseur + ";" + NoPage;
            Session["DernRechEtuUrl"] = Request.Url.LocalPath;

            var lstEtu = from p in _db.Inscription
                                    where (_db.Jumelage.Any(j => j.id_Enseignant == superviseur || superviseur == 0) &&
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
        protected IEnumerable<Inscription> Rechercher(int? page)
        {
            return Rechercher();
        }

        [ValidationAccesTuteur]
        public ActionResult Index(int? page)
        {
            NoPage = (page ?? NoPage);
            return View(Rechercher().ToPagedList(NoPage, 20));
        }

        [ValidationAcces.ValidationAccesEleve]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var inscription = _dataRepository.FindInscription(id.Value);

            if (inscription == null)
            {
                return HttpNotFound();
            }

            var vCoursSuivi = from d in _db.CoursSuivi
                              where d.id_Pers == inscription.id_Pers
                              select d;

            var vInscription = from d in _db.Inscription
                               where d.id_Pers == inscription.id_Pers
                               select d;            

            ViewBag.idPers = vInscription.First().id_Pers;
            ViewBag.idTypeInsc = vInscription.First().id_TypeInscription;

            return View(Tuple.Create(inscription, vCoursSuivi.AsEnumerable(), vInscription.AsEnumerable()));
        }
        
        [HttpPost]
        [ValidationAcces.ValidationAccesTuteur]
        public void ModifBon(bool bon, string insc)
        {
            var idInscription = Convert.ToInt32(insc);

            var inscription = _db.Inscription.Find(idInscription);

            inscription.BonEchange = bon; 

            _db.Entry(inscription).State = EntityState.Modified;
            _db.SaveChanges();
        }

        [HttpPost]
        [ValidationAcces.ValidationAccesEleve]
        public void ModifEmail(string email, string pers)
        {
            var idPers = Convert.ToInt32(pers);

            var personne = _db.Personne.Find(idPers);

            personne.Courriel = email;

            _db.Entry(personne).State = EntityState.Modified;
            _db.SaveChanges();
        }

        [HttpPost]
        [ValidationAcces.ValidationAccesEleve]
        public void ModifTel(string tel, string pers)
        {
            var idPers = Convert.ToInt32(pers);

            var personne = _db.Personne.Find(idPers);

            personne.Telephone = SachemIdentite.FormatTelephone(tel);

            _db.Entry(personne).State = EntityState.Modified;
            _db.SaveChanges();
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
