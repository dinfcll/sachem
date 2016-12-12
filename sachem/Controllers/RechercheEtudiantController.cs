using sachem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sachem.Classes_Sachem;

namespace sachem.Controllers
{
    public class RechercheEtudiantController : Controller
    {
        protected readonly SACHEMEntities db = new SACHEMEntities();

        protected int noPage = 1;
        private int? pageRecue = null;

        [NonAction]
        private void ListeSession(int Session = 0)
        {
            SelectListItem toute = new SelectListItem();
            toute.Text = "Toutes";
            toute.Value = "0";
            var slSession = Liste.ListeSession();
            slSession.Insert(0,toute);
            ViewBag.SelectSession = slSession;
        }

        /// <summary>
        /// Actualise le dropdownlist des groupes selon l'élément sélectionné dans les dropdownlist Session et Cours
        /// </summary>
        [AcceptVerbs("Get", "Post")]
        public JsonResult ActualiseGroupeddl(int cours, int session)
        {
            var a = Liste.ListeGroupeSelonSessionEtCours(cours, session).Select(c => new { c.id_Groupe, c.NoGroupe });
            return Json(a.ToList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Actualise le dropdownlist des cours selon l'élément sélectionné dans le dropdownlist Session
        /// </summary>
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
            var champsRenseignes = 0;
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
                        session = Int32.Parse(tanciennerech[1]);
                        ViewBag.Session = session;
                        champsRenseignes++;
                    }
                    if (tanciennerech[2].Length != 0)
                    {
                        cours = Int32.Parse(tanciennerech[2]);
                        ViewBag.Cours = cours;
                        champsRenseignes++;
                    }
                    if (tanciennerech[3].Length != 0)
                    {
                        groupe = Int32.Parse(tanciennerech[3]);
                        ViewBag.Groupe = groupe;
                        champsRenseignes++;
                    }
                }
            }
            else
            {
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
                {
                    if (!String.IsNullOrEmpty(Request.Form["SelectCours"]))
                    {
                        cours = Convert.ToInt32(Request.Form["SelectCours"]);
                        ViewBag.Cours = cours;
                        champsRenseignes++;
                    }
                    else if (!String.IsNullOrEmpty(Request.Params["Cours"]))
                    {
                        cours = Convert.ToInt32(Request.Params["Cours"]);
                        ViewBag.Cours = cours;
                        champsRenseignes++;
                    }
                    if (!String.IsNullOrEmpty(Request.Form["SelectGroupe"]))
                    {
                        groupe = Convert.ToInt32(Request.Form["SelectGroupe"]);
                        ViewBag.Groupe = groupe;
                        champsRenseignes++;
                    }
                    else if (!String.IsNullOrEmpty(Request.Params["Groupe"]))
                    {
                        groupe = Convert.ToInt32(Request.Params["Groupe"]);
                        ViewBag.Groupe = groupe; 
                        champsRenseignes++;
                    }
                    if (!string.IsNullOrEmpty(Request.Form["SelectSession"]))
                    {
                        int.TryParse(Request.Form["SelectSession"], out session);
                    }
                    else if (Request.Form["SelectSession"] == null)
                        session = db.Session.Max(s => s.id_Sess);
                }
            }
            ListeSession(session);
            ViewBag.SelectCours = new SelectList(Liste.ListeCoursSelonSession(session).AsQueryable(), "id_Cours", "CodeNom", cours);
            ViewBag.SelectGroupe = new SelectList(Liste.ListeGroupeSelonSessionEtCours(cours, session), "id_Groupe", "NoGroupe", groupe);

            Session["DernRechEtu"] = matricule + ";" + session + ";" + cours + ";" + groupe + ";" + noPage;
            Session["DernRechEtuUrl"] = Request.Url.LocalPath.ToString();

            if (ModelState.IsValid)
            {
                db.Configuration.LazyLoadingEnabled = false;
                if (matricule == "")
                {
                    lstEtu = from q in
                            (from p in db.Personne.Where(x => x.Actif == true && x.GroupeEtudiant.Any(y => y.id_Groupe == groupe || groupe == 0) 
                             && x.GroupeEtudiant.Any(z => z.Groupe.id_Cours == cours || cours == 0)
                             && x.EtuProgEtude.Any(y => y.id_Sess == session || session == 0)).OrderBy(x => x.Nom) 
                             select new
                             {
                                 Personne = p,
                                 ProgEtu = (from pe in db.EtuProgEtude where p.id_Pers == pe.id_Etu orderby pe.id_Sess descending select pe).FirstOrDefault().ProgrammeEtude,
                             }).AsEnumerable()
                             orderby q.Personne.Nom, q.Personne.Prenom
                             select new PersonneProgEtu { personne = q.Personne, progEtuActif = q.ProgEtu };
                }
                else
                {
                    lstEtu = from q in
                            (from p in db.Personne.Where(x => x.Matricule.Substring(2).StartsWith(matricule)).OrderBy(x => x.Nom).OrderBy(x => x.Nom)
                             select new
                             {
                                 Personne = p,
                                 ProgEtu = (from pe in db.EtuProgEtude where p.id_Pers == pe.id_Etu orderby pe.id_Sess descending select pe).FirstOrDefault().ProgrammeEtude,
                             }).AsEnumerable()
                             orderby q.Personne.Nom, q.Personne.Prenom
                             select new PersonneProgEtu { personne = q.Personne, progEtuActif = q.ProgEtu };
                }
                db.Configuration.LazyLoadingEnabled = true;
            }
            return lstEtu;
        }

        protected IEnumerable<PersonneProgEtu> Rechercher(int? Page)
        {
            pageRecue = Page;
            return Rechercher();
        }
    }
}