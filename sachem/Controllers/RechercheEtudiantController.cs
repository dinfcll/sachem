using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sachem.Classes_Sachem;
using sachem.Models;

namespace sachem.Controllers
{
    public class RechercheEtudiantController : Controller
    {
        protected readonly SACHEMEntities Db = new SACHEMEntities();

        protected int NoPage = 1;

        [NonAction]
        private void ListeSession()
        {
            ViewBag.SelectSession = Liste.ListeSessionPlusToutesAvecValeur();
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult ActualiseGroupeddl(int cours, int session)
        {
            var a = Liste.ListeGroupeSelonSessionEtCours(cours, session).Select(c => new { c.id_Groupe, c.NoGroupe });
            return Json(a.ToList(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("Get", "Post")]
        public virtual JsonResult ActualiseCoursddl(int session = 0)
        {
            var a = Liste.ListeCoursSelonSession(session).Select(c => new { c.id_Cours, c.CodeNom });
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
                    else if (!String.IsNullOrEmpty(Request.Params["Cours"]))
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
                        session = Db.Session.Max(s => s.id_Sess);
                }
            }
            ListeSession();
            ViewBag.SelectCours = new SelectList(Liste.ListeCoursSelonSession(session).AsQueryable(), "id_Cours", "CodeNom", cours);
            ViewBag.SelectGroupe = new SelectList(Liste.ListeGroupeSelonSessionEtCours(cours, session), "id_Groupe", "NoGroupe", groupe);

            Session["DernRechEtu"] = matricule + ";" + session + ";" + cours + ";" + groupe + ";" + NoPage;
            if (Request.Url != null) Session["DernRechEtuUrl"] = Request.Url.LocalPath;

            if (ModelState.IsValid)
            {
                Db.Configuration.LazyLoadingEnabled = false;
                if (matricule == "")
                {
                    lstEtu = from q in
                            (from p in Db.Personne.Where(x => x.Actif && x.GroupeEtudiant.Any(y => y.id_Groupe == groupe || groupe == 0)
                             && x.GroupeEtudiant.Any(z => z.Groupe.id_Cours == cours || cours == 0)
                             && x.EtuProgEtude.Any(y => y.id_Sess == session || session == 0)).OrderBy(x => x.Nom) 
                             select new
                             {
                                 Personne = p,
                                 ProgEtu = (from pe in Db.EtuProgEtude where p.id_Pers == pe.id_Etu orderby pe.id_Sess descending select pe).FirstOrDefault().ProgrammeEtude
                             }).AsEnumerable()
                             orderby q.Personne.Nom, q.Personne.Prenom
                             select new PersonneProgEtu { personne = q.Personne, progEtuActif = q.ProgEtu };
                }
                else
                {
                    lstEtu = from q in
                            (from p in Db.Personne.Where(x => x.Matricule.Substring(2).StartsWith(matricule)).OrderBy(x => x.Nom).ThenBy(x => x.Nom)
                             select new
                             {
                                 Personne = p,
                                 ProgEtu = (from pe in Db.EtuProgEtude where p.id_Pers == pe.id_Etu orderby pe.id_Sess descending select pe).FirstOrDefault().ProgrammeEtude
                             }).AsEnumerable()
                             orderby q.Personne.Nom, q.Personne.Prenom
                             select new PersonneProgEtu { personne = q.Personne, progEtuActif = q.ProgEtu };
                }
                Db.Configuration.LazyLoadingEnabled = true;
            }
            return lstEtu;
        }

        protected IEnumerable<PersonneProgEtu> Rechercher(int? page)
        {
            return Rechercher();
        }
    }
}