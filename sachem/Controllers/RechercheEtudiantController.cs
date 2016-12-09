using sachem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", Session));
            slSession.Insert(0,toute);
            ViewBag.SelectSession = slSession;

        }

        [NonAction]
        protected IEnumerable<Cours> ObtenirListeCours(int session)
        {
            return db.Cours.AsNoTracking()
                .Where(c => c.Groupe.Any(g => (g.id_Sess == session || session == 0)))
                .OrderBy(c => c.Nom)
                .AsEnumerable();
        }

        [NonAction]
        private IEnumerable<Groupe> ObtenirListeGroupe(int cours, int session)
        {
            return db.Groupe.AsNoTracking()
                .Where(p => (p.id_Sess == session || session == 0) && (p.id_Cours == cours || cours == 0))
                .OrderBy(p => p.NoGroupe);
        }

        [NonAction]
        private void ListeCours(int Cours, int session)
        {
            ViewBag.SelectCours = new SelectList(ObtenirListeCours(session).AsQueryable(), "id_Cours", "CodeNom", Cours);
        }

        [NonAction]
        private void ListeGroupe(int Cours, int session, int Groupe)
        {
            ViewBag.SelectGroupe = new SelectList(ObtenirListeGroupe(Cours, session), "id_Groupe", "NoGroupe", Groupe);
        }

        /// <summary>
        /// Actualise le dropdownlist des groupes selon l'élément sélectionné dans les dropdownlist Session et Cours
        /// </summary>
        [AcceptVerbs("Get", "Post")]
        public JsonResult ActualiseGroupeddl(int cours, int session)
        {
            var a = ObtenirListeGroupe(cours, session).Select(c => new { c.id_Groupe, c.NoGroupe });
            return Json(a.ToList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Actualise le dropdownlist des cours selon l'élément sélectionné dans le dropdownlist Session
        /// </summary>
        [AcceptVerbs("Get", "Post")]
        public virtual JsonResult ActualiseCoursddl(int session = 0)
        {
            var a = ObtenirListeCours(session).Select(c => new { c.id_Cours, c.CodeNom });
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
            ViewBag.SelectSession = Liste.ListeSession(session);
            ListeCours(cours, session);
            ListeGroupe(cours, session, groupe);

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