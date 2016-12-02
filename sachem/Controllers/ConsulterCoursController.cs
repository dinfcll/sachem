using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;

namespace sachem.Controllers
{
    public class ConsulterCoursController : Controller
    {
        int m_IdPers;
        int m_IdTypeUsage;

        private readonly SACHEMEntities db = new SACHEMEntities();

        List<TypeUsagers> RolesAcces = new List<TypeUsagers>() { TypeUsagers.Enseignant, TypeUsagers.Responsable, TypeUsagers.Super };

        public ActionResult Index(int? page)
        {
            db.Configuration.LazyLoadingEnabled = true;

            var noPage = (page ?? 1);

            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);

            m_IdPers = SessionBag.Current.id_Pers;
            m_IdTypeUsage = SessionBag.Current.id_TypeUsag;

            return View(AfficherCoursAssignes().ToPagedList(noPage, 10));
        }
        
        [NonAction]
        private IEnumerable<Groupe> AfficherCoursAssignes()
        {
            var idSess = 0;
            var idPersonne = 0;
            List<Groupe> listeCours = new List<Groupe>();
            string reqType = Request.RequestType;
            string dernRechCoursUrl = (string)Session["DernRechCoursUrl"];
            string localPath = Request.Url?.LocalPath;

            if (reqType == "GET" && Session["DernRechEtu"] != null 
                && (string)Session["DernRechEtuUrl"] == Request.Url?.LocalPath)
            {
                var tanciennerech = Session["DernRechEtu"].ToString().Split(';');

                if (tanciennerech[0].Length != 0)
                {
                    idSess = int.Parse(tanciennerech[0]);
                    ViewBag.Session = idSess;
                }
                if (tanciennerech[1].Length != 0)
                {
                    idPersonne = int.Parse(tanciennerech[1]);
                    ViewBag.Personne = idPersonne;
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(Request.Form["Personne"]))
                {
                    idPersonne = Convert.ToInt32(Request.Form["Personne"]);
                    ViewBag.Superviseur = idPersonne;
                }
                else if (!String.IsNullOrEmpty(Request.Params["Personne"]))
                {
                    idPersonne = Convert.ToInt32(Request.Params["Personne"]);
                    ViewBag.Superviseur = idPersonne;

                }
                if (!String.IsNullOrEmpty(Request.Form["SelectSession"]))
                {
                    idSess = Convert.ToInt32(Request.Form["SelectSession"]);
                    ViewBag.Session = idSess;

                }
                else if (!String.IsNullOrEmpty(Request.Params["Session"]))
                {
                    idSess = Convert.ToInt32(Request.Params["Session"]);
                    ViewBag.Session = idSess;

                }
                else if (Request.Form["Session"] == null)
                    idSess = Convert
                        .ToInt32(db.Session.OrderByDescending(y => y.Annee)
                        .ThenByDescending(x => x.id_Saison)
                        .FirstOrDefault()
                        .id_Sess);
            }

            Session["DernRechCours"] = idSess + ";" + idPersonne;
            Session["DernRechCoursUrl"] = Request.Url?.LocalPath;

            ViewBag.Sessionchoisie = idSess;

            if (m_IdTypeUsage == 2)
            {
                ListeSession(idSess);

                var listeInfoEns = (from c in db.Groupe
                           where (c.id_Sess == idSess && c.id_Enseignant == m_IdPers)
                           || 
                           (idSess == 0 && c.id_Enseignant == m_IdPers)
                           orderby c.NoGroupe
                           select c)
                           .GroupBy(c => c.Cours.Nom)
                           .SelectMany(cours => cours);

                var listeIdUniques = (from c in listeInfoEns select c.id_Cours).Distinct();

                ViewBag.IsEnseignant = true;

                listeCours = trouverCoursUniques(listeInfoEns, listeIdUniques, ViewBag.IsEnseignant);

                return listeCours.ToList();
            }
            else
            {
                ListeSession(idSess);
                ListePersonne(idSess, idPersonne);

                var listeInfoResp = (from c in db.Groupe
                           where c.id_Sess == (idSess == 0 ? c.id_Sess : idSess)
                           && 
                           c.id_Enseignant == (idPersonne == 0 ? c.id_Enseignant : idPersonne)
                           orderby c.NoGroupe
                           select c)
                           .GroupBy(c => c.Cours.Nom)
                           .SelectMany(cours => cours);

                var listeIdUniques = (from c in listeInfoResp select c.id_Cours).Distinct();

                ViewBag.IsEnseignant = false;

                listeCours = trouverCoursUniques(listeInfoResp, listeIdUniques, ViewBag.IsEnseignant);

                return listeCours.ToList();
            }
        }


        [NonAction]
        private List<Groupe> trouverCoursUniques(IQueryable<Groupe> listeTout, IQueryable<int> _listeIdUniques, bool isEnseignant)
        {
            List<Groupe> listeCours = new List<Groupe>();
            int i = 0;
            int compteurPos = 0;
            int idPrec = 0;
            var tlid = _listeIdUniques.ToList();
          
            foreach(Groupe t in listeTout)
            {
                foreach (var j in tlid)
                {
                    if (t.id_Cours == tlid[i] && t.id_Cours != idPrec)
                    {
                        idPrec = t.id_Cours;
                        if (!isEnseignant)
                            t.nomsConcatenesProfs = trouverNomsProfs(listeTout, t.id_Cours);
                        listeCours.Add(t);
                        compteurPos = i;
                    }
                    i++;
                }
                i = 0;
            }
            return listeCours;
        }

        [NonAction]
        private string trouverNomsProfs(IQueryable<Groupe> _listeTout, int idCours)
        {
            string nomsProfs = "";
            List<string> listeNomsTemp = new List<string>();

            var lNomsProfs = from c in _listeTout where c.id_Cours == idCours select c;

            foreach (var n in lNomsProfs)
            {
                if (!listeNomsTemp.Contains(n.Personne.PrenomNom))
                {
                    nomsProfs += n.Personne.PrenomNom + ", ";
                    listeNomsTemp.Add(n.Personne.PrenomNom);
                }
            }

            nomsProfs = nomsProfs.Remove(nomsProfs.Length - 2, 2);

            return nomsProfs;
        }

        [NonAction]
        private bool connexionValide(int idTypeUsager)
        {
            bool valide = false;

            if (idTypeUsager == 2 || idTypeUsager == 3)
                valide = true;

            return valide;
        }
        
        [NonAction]
        private void ListeSession(int _idSess = 0)
        {
            var lSessions = db.Session
                .AsNoTracking()
                .OrderBy(s => s.Annee)
                .ThenBy(s => s.p_Saison.Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", _idSess));

            ViewBag.Session = slSession;
        }

        [HttpPost]
        public void ListePersonne(int idSession, int idPers)
        {
            var lPersonne = (from p in db.Personne
                            join c in db.Groupe on p.id_Pers equals c.id_Enseignant
                            where (p.id_TypeUsag == (int)TypeUsagers.Enseignant 
                            || 
                            p.id_TypeUsag == (int)TypeUsagers.Responsable) 
                            && 
                            p.Actif == true 
                            && 
                            c.id_Sess == (idSession == 0 ? c.id_Sess : idSession)
                            orderby p.Nom,p.Prenom
                            select p).Distinct();

            var slPersonne = new List<SelectListItem>();
            slPersonne.AddRange(new SelectList(lPersonne, "id_Pers", "NomPrenom", idPers));

            ViewBag.Personne = slPersonne;
        }

        public ActionResult Details(int? idCours, int? idSess)
        {
            m_IdPers = SessionBag.Current.id_Pers;
            m_IdTypeUsage = SessionBag.Current.id_TypeUsag;
            IOrderedQueryable<Groupe> gr;

            if (idSess == 0)
                ViewBag.IsSessToutes = true;
            else
                ViewBag.IsSessToutes = false;

            if (!connexionValide(m_IdTypeUsage))
                return View("~/Views/Shared/Error.cshtml");
            else
            {
                if (idCours == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                if (m_IdTypeUsage == 2)
                {
                    ViewBag.IsEnseignant = true;

                    gr = from g in db.Groupe
                         where g.id_Cours == idCours 
                         &&
                         g.id_Enseignant == m_IdPers
                         orderby g.NoGroupe
                         select g;
                }
                else
                {
                    ViewBag.IsEnseignant = false;

                    gr = from g in db.Groupe
                         where g.id_Cours == idCours
                         orderby g.NoGroupe
                         select g;
                }

                if (!gr.Any())
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                return View(gr.ToList());
            }
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