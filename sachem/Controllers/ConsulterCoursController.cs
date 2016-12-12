using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using sachem.Classes_Sachem;

namespace sachem.Controllers
{
    public class ConsulterCoursController : Controller
    {
        private int _idPers;
        private int _idTypeUsage;
        private readonly SACHEMEntities _db = new SACHEMEntities();

        private readonly List<TypeUsagers> _rolesAcces = new List<TypeUsagers>() { TypeUsagers.Enseignant, TypeUsagers.Responsable, TypeUsagers.Super };

        public ActionResult Index(int? page)
        {
            _db.Configuration.LazyLoadingEnabled = true;

            var noPage = (page ?? 1);

            if (!SachemIdentite.ValiderRoleAcces(_rolesAcces, Session))
            {
                return RedirectToAction("Error", "Home", null);
            }

            _idPers = SessionBag.Current.id_Pers;
            _idTypeUsage = SessionBag.Current.id_TypeUsag;

            return View(AfficherCoursAssignes().ToPagedList(noPage, 10));
        }

        private IEnumerable<Groupe> AfficherCoursAssignes()
        {
            var idSess = 0;
            var idPersonne = 0;
            List<Groupe> listeCours;
            var reqType = Request.RequestType;

            if (reqType == "GET" && Session["DernRechCours"] != null 
                && (string)Session["DernRechCoursUrl"] == Request.Url?.LocalPath)
            {
                var tanciennerech = Session["DernRechCours"].ToString().Split(';');

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
                if (!string.IsNullOrEmpty(Request.Form["Personne"]))
                {
                    idPersonne = Convert.ToInt32(Request.Form["Personne"]);
                    ViewBag.Superviseur = idPersonne;
                }
                else if (!string.IsNullOrEmpty(Request.Params["Personne"]))
                {
                    idPersonne = Convert.ToInt32(Request.Params["Personne"]);
                    ViewBag.Superviseur = idPersonne;
                }
                if (!string.IsNullOrEmpty(Request.Form["SelectSession"]))
                {
                    idSess = Convert.ToInt32(Request.Form["SelectSession"]);
                    ViewBag.Session = idSess;
                }
                else if (!string.IsNullOrEmpty(Request.Params["Session"]))
                {
                    idSess = Convert.ToInt32(Request.Params["Session"]);
                    ViewBag.Session = idSess;
                }
                else if (Request.Form["Session"] == null)
                {
                    var firstOrDefault = _db.Session.OrderByDescending(y => y.Annee)
                        .ThenByDescending(x => x.id_Saison)
                        .FirstOrDefault();

                    if (firstOrDefault != null)
                        idSess = Convert
                            .ToInt32(firstOrDefault
                                .id_Sess);
                }
            }

            Session["DernRechCours"] = idSess + ";" + idPersonne;
            Session["DernRechCoursUrl"] = Request.Url?.LocalPath;

            ViewBag.Sessionchoisie = idSess;

            if (_idTypeUsage == 2)
            {
                ViewBag.Session = Liste.ListeSession(idSess);

                var listeInfoEns = (from c in _db.Groupe
                           where (c.id_Sess == idSess && c.id_Enseignant == _idPers) ||
                           (idSess == 0 && c.id_Enseignant == _idPers)
                           orderby c.NoGroupe
                           select c)
                           .GroupBy(c => c.Cours.Nom)
                           .SelectMany(cours => cours);

                var listeIdUniques = (from c in listeInfoEns select c.id_Cours).Distinct();

                ViewBag.IsEnseignant = true;

                listeCours = TrouverCoursUniques(listeInfoEns, listeIdUniques, ViewBag.IsEnseignant);

                return listeCours.ToList();
            }
            else
            {
                ViewBag.Session = Liste.ListeSession(idSess);
                ViewBag.Personne = Liste.ListePersonne(idSess, idPersonne);

                var listeInfoResp = (from c in _db.Groupe
                           where c.id_Sess == (idSess == 0 ? c.id_Sess : idSess) && 
                           c.id_Enseignant == (idPersonne == 0 ? c.id_Enseignant : idPersonne)
                           orderby c.NoGroupe
                           select c)
                           .GroupBy(c => c.Cours.Nom)
                           .SelectMany(cours => cours);

                var listeIdUniques = (from c in listeInfoResp select c.id_Cours).Distinct();

                ViewBag.IsEnseignant = false;

                listeCours = TrouverCoursUniques(listeInfoResp, listeIdUniques, ViewBag.IsEnseignant);

                return listeCours.ToList();
            }
        }

        private static List<Groupe> TrouverCoursUniques(IQueryable<Groupe> listeTout, IEnumerable<int> listeIdUniques, bool isEnseignant)
        {
            var listeCours = new List<Groupe>();
            var idPrec = 0;
            var tlid = listeIdUniques.ToList();
          
            foreach(var t in listeTout)
            {
                foreach (var id in tlid)
                {
                    if (t.id_Cours == id && t.id_Cours != idPrec)
                    {
                        idPrec = t.id_Cours;
                        if (!isEnseignant)
                        {
                            t.nomsConcatenesProfs = TrouverNomsProfs(listeTout, t.id_Cours);
                        }
                        listeCours.Add(t);
                    }
                }
            }
            return listeCours;
        }

        private static string TrouverNomsProfs(IQueryable<Groupe> listeTout, int idCours)
        {
            var nomsProfs = "";
            var listeNomsTemp = new List<string>();

            var lNomsProfs = from c in listeTout where c.id_Cours == idCours select c;

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

        private static bool ConnexionValide(int idTypeUsager)
        {
            return idTypeUsager == 2 || idTypeUsager == 3;
        }
        
        [HttpPost]
        public ActionResult Details(int? idCours, int? idSess)
        {
            _idPers = SessionBag.Current.id_Pers;
            _idTypeUsage = SessionBag.Current.id_TypeUsag;

            ViewBag.IsSessToutes = idSess == 0;

            if (!ConnexionValide(_idTypeUsage))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                if (idCours == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                IOrderedQueryable<Groupe> gr;
                if (_idTypeUsage == 2)
                {
                    ViewBag.IsEnseignant = true;

                    gr = from g in _db.Groupe
                         where g.id_Cours == idCours &&
                         g.id_Enseignant == _idPers
                         orderby g.NoGroupe
                         select g;
                }
                else
                {
                    ViewBag.IsEnseignant = false;
                    gr = from g in _db.Groupe
                         where g.id_Cours == idCours
                         orderby g.NoGroupe
                         select g;
                }

                if (!gr.Any())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return View(gr.ToList());
            }
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