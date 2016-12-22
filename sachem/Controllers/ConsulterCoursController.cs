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
    public class ConsulterCoursController : Controller
    {
        private int _idPers;
        private int _idTypeUsage;
        private readonly IDataRepository _dataRepository;

        public ConsulterCoursController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public ConsulterCoursController()
        {
            _dataRepository = new BdRepository();
        }

        private readonly List<TypeUsager> _rolesAcces = new List<TypeUsager> { TypeUsager.Enseignant, TypeUsager.Responsable, TypeUsager.Super };

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Index(int? page)
        {
            _dataRepository.BeLazy(true);

            var noPage = page ?? 1;

            if (!SachemIdentite.ValiderRoleAcces(_rolesAcces, Session))
            {
                return RedirectToAction("Error", "Home", null);
            }

            _idPers = BrowserSessionBag.Current.id_Pers;
            _idTypeUsage = BrowserSessionBag.Current.TypeUsager;

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
                    var firstOrDefault = _dataRepository.AllSession().OrderByDescending(y => y.Annee)
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
                ViewBag.Session = _dataRepository.ListeSession(idSess);

                var listeInfoEns = _dataRepository.WhereGroupe(c => (c.id_Sess == idSess && c.id_Enseignant == _idPers) ||
                            (idSess == 0 && c.id_Enseignant == _idPers))
                            .OrderBy(c => c.NoGroupe)
                            .GroupBy(c => c.Cours.Nom)
                            .SelectMany(cours => cours)
                            .AsQueryable();

                var listeIdUniques = listeInfoEns.Select(c => c.id_Cours).Distinct();

                ViewBag.IsEnseignant = true;

                listeCours = TrouverCoursUniques(listeInfoEns, listeIdUniques, ViewBag.IsEnseignant);

                return listeCours.ToList();
            }
            else
            {
                ViewBag.Session = _dataRepository.ListeSession(idSess);
                ViewBag.Personne = _dataRepository.ListeEnseignantEtResponsable(idPersonne);//id_Sess?

                var listeInfoResp = _dataRepository.WhereGroupe(c => c.id_Sess == (idSess == 0 ? c.id_Sess : idSess) &&
                            c.id_Enseignant == (idPersonne == 0 ? c.id_Enseignant : idPersonne))
                            .OrderBy(c => c.NoGroupe)
                            .GroupBy(c => c.Cours.Nom)
                            .SelectMany(cours => cours)
                            .AsQueryable();

                var listeIdUniques = (listeInfoResp.Select(c => c.id_Cours)).Distinct();

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
                    if (t.id_Cours != id || t.id_Cours == idPrec) continue;
                    idPrec = t.id_Cours;
                    if (!isEnseignant)
                    {
                        t.nomsConcatenesProfs = TrouverNomsProfs(listeTout, t.id_Cours);
                    }
                    listeCours.Add(t);
                }
            }
            return listeCours;
        }

        private static string TrouverNomsProfs(IQueryable<Groupe> listeTout, int idCours)
        {
            var nomsProfs = "";
            var listeNomsTemp = new List<string>();

            var lNomsProfs = listeTout.Where(c => c.id_Cours == idCours);

            foreach (var n in lNomsProfs)
            {
                if (listeNomsTemp.Contains(n.Personne.PrenomNom)) continue;
                nomsProfs += n.Personne.PrenomNom + ", ";
                listeNomsTemp.Add(n.Personne.PrenomNom);
            }
            nomsProfs = nomsProfs.Remove(nomsProfs.Length - 2, 2);

            return nomsProfs;
        }

        private static bool ConnexionValide(int idTypeUsager)
        {
            return idTypeUsager == 2 || idTypeUsager == 3;
        }
        
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Details(int? idCours, int? idSess)
        {
            _idPers = BrowserSessionBag.Current.id_Pers;
            _idTypeUsage = BrowserSessionBag.Current.TypeUsager;

            ViewBag.IsSessToutes = idSess == 0;

            if (!ConnexionValide(_idTypeUsage))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            if (idCours == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IQueryable<Groupe> gr;
            if (_idTypeUsage == 2)
            {
                ViewBag.IsEnseignant = true;

                gr = _dataRepository.WhereGroupe(g => g.id_Cours == idCours &&
                                           g.id_Enseignant == _idPers).OrderBy(g => g.NoGroupe).AsQueryable();
            }
            else
            {
                ViewBag.IsEnseignant = false;
                gr = _dataRepository.WhereGroupe(g => g.id_Cours == idCours).OrderBy(g => g.NoGroupe).AsQueryable();
            }

            if (!gr.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(gr.ToList());
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
