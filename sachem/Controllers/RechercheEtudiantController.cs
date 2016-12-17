using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    public class RechercheEtudiantController : Controller
    {
        protected int NoPage = 1;
        protected readonly IDataRepository DataRepository;

        public RechercheEtudiantController(IDataRepository dataRepository)
        {
            DataRepository = dataRepository;
        }

        public RechercheEtudiantController()
        {
            DataRepository = new BdRepository();
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult ActualiseGroupeddl(int cours, int session)
        {
            var a = DataRepository
                .WhereGroupe(g => (g.id_Sess == session || session == 0) && (g.id_Cours == cours || cours == 0))
                .OrderBy(g => g.NoGroupe);
            return Json(a.ToList(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("Get", "Post")]
        public virtual JsonResult ActualiseCoursddl(int session = 0)
        {
            var a = DataRepository.WhereCours(c => c.Groupe.Any(g => g.id_Sess == session || session == 0))
                .OrderBy(c => c.Nom)
                .Select(c => new {c.id_Cours, c.CodeNom});
            return Json(a.ToList(), JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        protected IEnumerable<PersonneProgEtu> Rechercher()
        {
            var matricule = "";
            var session = 0;
            var cours = 0;
            var groupe = 0;
            IEnumerable<PersonneProgEtu> lstEtu = new List<PersonneProgEtu>();

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
                        cours = int.Parse(tanciennerech[2]);
                        ViewBag.Cours = cours;
                    }
                    if (tanciennerech[3].Length != 0)
                    {
                        groupe = int.Parse(tanciennerech[3]);
                        ViewBag.Groupe = groupe;
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
                else
                {
                    if (!string.IsNullOrEmpty(Request.Form["SelectCours"]))
                    {
                        cours = Convert.ToInt32(Request.Form["SelectCours"]);
                        ViewBag.Cours = cours;
                    }
                    else if (!string.IsNullOrEmpty(Request.Params["Cours"]))
                    {
                        cours = Convert.ToInt32(Request.Params["Cours"]);
                        ViewBag.Cours = cours;
                    }
                    if (!string.IsNullOrEmpty(Request.Form["SelectGroupe"]))
                    {
                        groupe = Convert.ToInt32(Request.Form["SelectGroupe"]);
                        ViewBag.Groupe = groupe;
                    }
                    else if (!string.IsNullOrEmpty(Request.Params["Groupe"]))
                    {
                        groupe = Convert.ToInt32(Request.Params["Groupe"]);
                        ViewBag.Groupe = groupe; 
                    }
                    if (!string.IsNullOrEmpty(Request.Form["SelectSession"]))
                    {
                        int.TryParse(Request.Form["SelectSession"], out session);
                    }
                    else if (Request.Form["SelectSession"] == null)
                        session = DataRepository.SessionEnCours();
                }
            }
            ViewBag.SelectSession = DataRepository.ListeSession();
            ViewBag.SelectCours = new SelectList(DataRepository
                .WhereCours(c => c.Groupe.Any(g => g.id_Sess == session || session == 0))
                .OrderBy(c => c.Nom), "id_Cours", "CodeNom", cours);
            ViewBag.SelectGroupe = new SelectList(DataRepository
                .WhereGroupe(g => (g.id_Sess == session || session == 0) && (g.id_Cours == cours || cours == 0))
                .OrderBy(g => g.NoGroupe), "id_Groupe", "NoGroupe", groupe);

            Session["DernRechEtu"] = matricule + ";" + session + ";" + cours + ";" + groupe + ";" + NoPage;
            if (Request.Url != null) Session["DernRechEtuUrl"] = Request.Url.LocalPath;

            if (!ModelState.IsValid) return lstEtu;
            //DataRepository.Configuration.LazyLoadingEnabled = false;
            if (matricule == "")
            {
                lstEtu = DataRepository.WherePersonne(x => x.Actif 
                        && x.GroupeEtudiant.Any(y => y.id_Groupe == groupe || groupe == 0)
                        && x.GroupeEtudiant.Any(z => z.Groupe.id_Cours == cours || cours == 0)
                        && x.EtuProgEtude.Any(y => y.id_Sess == session || session == 0))
                        .OrderBy(x => x.Nom)
                        .Select(p => new
                        {
                            Personne = p,
                            ProgEtu =
                            DataRepository.WhereEtuProgEtude(pe => p.id_Pers == pe.id_Etu).OrderByDescending(pe => pe.id_Sess)
                                .First().ProgrammeEtude
                        }).AsEnumerable()
                        .OrderBy(q => q.Personne.Nom)
                        .ThenBy(q => q.Personne.Prenom)
                        .Select(q => new PersonneProgEtu {personne = q.Personne, progEtuActif = q.ProgEtu});
            }
            else
            {
                lstEtu = DataRepository.WherePersonne(x => x.Matricule.Substring(2).StartsWith(matricule))
                        .OrderBy(x => x.Nom)
                        .ThenBy(x => x.Nom)
                        .Select(p => new
                        {
                            Personne = p,
                            ProgEtu =
                            DataRepository.WhereEtuProgEtude(pe => p.id_Pers == pe.id_Etu).OrderByDescending(pe => pe.id_Sess)
                                .First().ProgrammeEtude
                        }).AsEnumerable()
                        .OrderBy(q => q.Personne.Nom)
                        .ThenBy(q => q.Personne.Prenom)
                        .Select(q => new PersonneProgEtu {personne = q.Personne, progEtuActif = q.ProgEtu});
            }
            //DataRepository.Configuration.LazyLoadingEnabled = true;
            return lstEtu;
        }

        protected IEnumerable<PersonneProgEtu> Rechercher(int? page)
        {
            return Rechercher();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DataRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}