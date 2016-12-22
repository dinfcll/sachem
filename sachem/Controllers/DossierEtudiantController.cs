using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using sachem.Methodes_Communes;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    public class DossierEtudiantController : Controller
    {
        protected int NoPage = 1;
        private readonly IDataRepository _dataRepository;

        public DossierEtudiantController()
        {
            _dataRepository = new BdRepository();
        }

        public DossierEtudiantController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [NonAction]
        [AcceptVerbs("Get", "Post")]
        public JsonResult ActualiseSuperviseurddl(int session)
        {
            var a = _dataRepository.WherePersonne(
                        p =>
                            //_dataRepository.AnyJumelage(j => (j.id_Sess == session || session == 0) && j.id_Enseignant == p.id_Pers) &&
                            p.id_TypeUsag == (int)TypeUsager.Enseignant).OrderBy(p => p.Nom).ThenBy(p => p.Prenom)
                            .Select(c => new { c.id_Pers, c.NomPrenom });
            return Json(a.ToList(), JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        protected IEnumerable<Inscription> Rechercher()
        {

            var matricule = "";
            var prenom = "";
            var nom = "";
            var session = 0;
            var tuteur = 0;
            var typeinscription = 0;
            int superviseur = BrowserSessionBag.Current.idSuperviseur;
            if ((int)BrowserSessionBag.Current.TypeUsager < (int)TypeUsager.Enseignant)
            {
                tuteur = BrowserSessionBag.Current.id_Pers;
                typeinscription = (int)TypeUsager.Etudiant;
            }

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
                    session = _dataRepository.SessionEnCours();
            }

            ViewBag.Session = _dataRepository.ListeSession(session);
            ViewBag.Inscription = _dataRepository.ListeTypeInscription(typeinscription);
            ViewBag.Superviseur = _dataRepository.ListeSuperviseur(session, superviseur);

            Session["DernRechEtu"] = matricule + ";" + session + ";" + typeinscription + ";" + superviseur + ";" + NoPage;
            Session["DernRechEtuUrl"] = Request.Url.LocalPath;
            return _dataRepository.WhereInscription(
                p => (p.id_Sess == session || session == 0) &&
                     (p.id_TypeInscription == typeinscription || typeinscription == 0) &&
                     (p.Personne.Prenom.Contains(prenom) || prenom == "") &&
                     (p.Personne.Nom.Contains(nom) || nom == "") &&
                     (p.Personne.Matricule.Substring(2).StartsWith(matricule) || matricule == "") //&&
                     //_dataRepository.AnyJumelage(j => (j.id_Enseignant == superviseur || superviseur == 0) &&
                     //j.id_InscrTuteur == tuteur || tuteur == 0)
            ).OrderBy(p => p.Personne.Nom).ThenBy(p => p.Personne.Prenom).ToList();
        }

        [NonAction]
        protected IEnumerable<Inscription> Rechercher(int? page)
        {
            return Rechercher();
        }

        [ValidationAcces.ValidationAccesTousTuteurs]
        public ActionResult Index(int? page)
        {
            NoPage = page ?? NoPage;
            return View(Rechercher().ToPagedList(NoPage, 20));
        }

        [ValidationAcces.ValidationAccesEleveAide]
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

            var vCoursSuivi = _dataRepository.WhereCoursSuivi(d => d.id_Pers == inscription.id_Pers).ToList();

            var vInscription = _dataRepository.WhereInscription(d => d.id_Pers == inscription.id_Pers).ToList();

            return View(Tuple.Create(inscription, vCoursSuivi.AsEnumerable(), vInscription.AsEnumerable()));
        }
        
        [HttpPost]
        [ValidationAcces.ValidationAccesTousTuteurs]
        public void ModifBon(bool bon, string insc)
        {
            var idInscription = Convert.ToInt32(insc);

            var inscription = _dataRepository.FindInscription(idInscription);

            inscription.BonEchange = bon; 

            _dataRepository.EditInscription(inscription);
        }

        [HttpPost]
        [ValidationAcces.ValidationAccesEleveAide]
        public void ModifEmail(string email, string pers)
        {
            var idPers = Convert.ToInt32(pers);

            var personne = _dataRepository.FindPersonne(idPers);

            personne.Courriel = email;

            _dataRepository.EditPersonne(personne);
        }

        [HttpPost]
        [ValidationAcces.ValidationAccesEleveAide]
        public void ModifTel(string tel, string pers)
        {
            var idPers = Convert.ToInt32(pers);

            var personne = _dataRepository.FindPersonne(idPers);

            personne.Telephone = SachemIdentite.FormatTelephone(tel);

            _dataRepository.EditPersonne(personne);
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
