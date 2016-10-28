using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
namespace sachem.Controllers
{
    public class ConsulterCoursController : Controller
    {
        int m_IdPers;
        int m_IdTypeUsage;

        private readonly SACHEMEntities db = new SACHEMEntities();

        readonly List<TypeUsagers> RolesAcces = new List<TypeUsagers>() { TypeUsagers.Enseignant, TypeUsagers.Responsable, TypeUsagers.Super };

        // GET: ConsulterCours
        public ActionResult Index()
        {
            ViewBag.IsSessToutes = true;

            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);

            m_IdPers = SessionBag.Current.id_Pers;
            m_IdTypeUsage = SessionBag.Current.id_TypeUsag; // 2 = enseignant, 3 = responsable

            return View(AfficherCoursAssignes());

        }
        
        //Fonction pour afficher les cours assignés à l'utilisateur connecté
        [NonAction]
        private IEnumerable<Groupe> AfficherCoursAssignes()
        {
            var idSess = 0;
            var listeCours = new List<Groupe>();

            //Pour accéder à la valeur de cle envoyée en GET dans le formulaire
            //Request.QueryString["cle"]
            //Pour accéder à la valeur cle envoyée en POST dans le formulaire
            //Request.Form["cle"]
            //Cette méthode fonctionnera dans les 2 cas
            //Request["cle"]
            if (Request.RequestType == "GET" && Session["DernRechCours"] != null && (string)Session["DernRechCoursUrl"] == Request.Url?.LocalPath)
            {
                var anciennerech = (string)Session["DernRechCours"];
                var tanciennerech = anciennerech.Split(';');

                if (tanciennerech[0] != "")
                {
                    idSess = int.Parse(tanciennerech[0]);
                }
            }
            else
            {
                //La méthode String.IsNullOrEmpty permet à la fois de vérifier si la chaine est NULL (lors du premier affichage de la page ou vide, lorsque le paramètre n'est pas appliquée 
                if (!string.IsNullOrEmpty(Request.Form["Session"]))
                    int.TryParse(Request.Form["Session"], out idSess);
                //si la variable est null c'est que la page est chargée pour la première fois, donc il faut assigner la session à la session en cours, la plus grande dans la base de données
                else if (Request.Form["Session"] == null)
                    idSess = db.Session.Max(s => s.id_Sess);
            }
            ViewBag.Sessionchoisie = idSess;
            if (m_IdTypeUsage == 2) //enseignant
            {
                ListeSession(idSess); //créer liste Session pour le dropdown

                var listeInfoEns = (from c in db.Groupe
                           where (c.id_Sess == idSess && c.id_Enseignant == m_IdPers) || (idSess == 0 && c.id_Enseignant == m_IdPers)
                           orderby c.Cours.Code ascending
                           select c).GroupBy(c => c.Cours.Nom).SelectMany(cours => cours);

                var listeIdUniques = (from c in listeInfoEns select c.id_Cours).Distinct();

                ViewBag.IsEnseignant = true;

                listeCours = TrouverCoursUniques(listeInfoEns, listeIdUniques, ViewBag.IsEnseignant);

                return listeCours.ToList(); //retourne tous les cours à un enseignant
            }
            else //responsable
            {
                ListeSession(idSess); //créer liste Session pour le dropdown
                ListePersonne(m_IdPers); //créer liste Enseignants pour le dropdown

                var listeInfoResp = (from c in db.Groupe
                           where c.id_Sess == (idSess == 0 ? c.id_Sess : idSess) && c.id_Enseignant == (m_IdPers == 0 ? c.id_Enseignant : m_IdPers)
                           orderby c.Cours.Code ascending
                           select c).GroupBy(c => c.Cours.Nom).SelectMany(cours => cours);

                var listeIdUniques = (from c in listeInfoResp select c.id_Cours).Distinct();

                ViewBag.IsEnseignant = false;

                listeCours = TrouverCoursUniques(listeInfoResp, listeIdUniques, ViewBag.IsEnseignant);

                return listeCours.OrderBy(x => x.Cours.Code).ToList(); //retourne tous les cours
            }
        }

        [NonAction]
        private List<Groupe> TrouverCoursUniques(IQueryable<Groupe> listeTout, IQueryable<int> listeIdUniques, bool isEnseignant)
        {
            List<Groupe> listeCours = new List<Groupe>();
            int idPrec = 0;
            var idUniques = listeIdUniques.ToList();

            foreach(Groupe groupe in listeTout)
            {
                foreach (var id in idUniques)
                {
                    if (groupe.id_Cours == id && groupe.id_Cours != idPrec) //si id unique et pas encore traité
                    {
                        idPrec = groupe.id_Cours;
                        if (!isEnseignant)
                        {
                            groupe.nomsConcatenesProfs = trouverNomsProfs(listeTout, groupe.id_Cours);
                        }
                        listeCours.Add(groupe);
                    }
                }
            }
            return listeCours;
        }

        [NonAction]
        private string trouverNomsProfs(IQueryable<Groupe> listeTout, int idCours)
        {
            string nomsProfs = "";
            List<string> listeNomsTemp = new List<string>();

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

        [NonAction]
        private bool connexionValide(int idTypeUsager)
        {
            if (idTypeUsager == 2 || idTypeUsager == 3)
            {
                return true;
            }
        return false;
        }
        
        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListeSession(int idSess = 0)
        {
            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", idSess));

            ViewBag.Session = slSession;
        }

        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListePersonne(int idPersonne)
        {
            var lPersonne = from p in db.Personne
                            where (p.id_TypeUsag == (int)TypeUsagers.Enseignant || p.id_TypeUsag == (int)TypeUsagers.Responsable) && p.Actif == true
                            orderby p.Nom,p.Prenom
                            select p;
            var slPersonne = new List<SelectListItem>();
            slPersonne.AddRange(new SelectList(lPersonne, "id_Pers", "NomPrenom", idPersonne));

            ViewBag.Personne = slPersonne;
        }

        // GET: ConsulterCours/Details/5
        public ActionResult Details(int? idCours, int? idSess)
        {
            m_IdPers = SessionBag.Current.id_Pers;
            m_IdTypeUsage = SessionBag.Current.id_TypeUsag; // 2 = enseignant, 3 = responsable
            IOrderedQueryable<Groupe> gr;

            if (!connexionValide(m_IdTypeUsage))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                if (idCours == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (m_IdTypeUsage == 2) //enseignant
                {
                    ViewBag.IsEnseignant = true;

                    if (idSess == 0)
                    {
                        ViewBag.IsSessToutes = true;

                        gr = obtenirGroupesEnseignant(idCours, m_IdPers);
                    }
                    else
                    {
                        ViewBag.IsSessToutes = false;

                        gr = obtenirGroupesEnseignant(idCours, m_IdPers, idSess);
                    }
                }
                else //responsable
                {
                    ViewBag.IsEnseignant = false;

                    if (idSess == 0)
                    {
                        ViewBag.IsSessToutes = true;

                        gr = obtenirGroupesEnseignant(idCours);
                    }
                    else
                    {
                        ViewBag.IsSessToutes = false;

                        gr = obtenirGroupesEnseignant(idCours, idSess);
                    }
                }

                if (!gr.Any())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return View(gr.ToList()); //renvoyer la liste des groupes en lien avec le cours
            }
        }


        private IOrderedQueryable<Groupe> obtenirGroupesEnseignant(int? _idCours, int _m_IdPers)
        {
            IOrderedQueryable<Groupe>  gr = 
                from g in db.Groupe 
                where g.id_Cours == _idCours && g.id_Enseignant == _m_IdPers
                orderby g.NoGroupe
                select g;

            return gr;
        }

        private IOrderedQueryable<Groupe> obtenirGroupesEnseignant(int? _idCours, int _idEnseignant, int? _idSess)
        {
            IOrderedQueryable<Groupe> gr =
                from g in db.Groupe 
                where g.id_Cours == _idCours && g.id_Enseignant == _idEnseignant && g.id_Sess == _idSess
                orderby g.NoGroupe
                select g;

            return gr;
        }
        private IOrderedQueryable<Groupe> obtenirGroupesEnseignant(int? _idCours)
        {
            IOrderedQueryable<Groupe> gr =
                from g in db.Groupe
                where g.id_Cours == _idCours
                orderby g.NoGroupe
                select g;

            return gr;
        }
        private IOrderedQueryable<Groupe> obtenirGroupesEnseignant(int? _idCours, int? _idSess)
        {
            IOrderedQueryable<Groupe> gr =
                from g in db.Groupe
                where g.id_Cours == _idCours && g.id_Sess == _idSess
                orderby g.NoGroupe
                select g;

            return gr;
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
