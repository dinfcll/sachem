using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Controllers
{
    public class ConsulterCoursController : Controller
    {
        int m_IdPers = SessionBag.Current.id_Pers;
        int m_IdTypeUsage = SessionBag.Current.id_TypeUsag; // 2 = enseignant, 3 = responsable

        private readonly SACHEMEntities db = new SACHEMEntities();

        List<TypeUsagers> RolesAcces = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super };

        // GET: ConsulterCours
        public ActionResult Index()
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);

            return View(AfficherCoursAssignes());

        }


        //Fonction pour afficher les cours assignés à l'utilisateur connecté
        [NonAction]
        private IEnumerable<Groupe> AfficherCoursAssignes()
        {
            if (!connexionValide(m_IdTypeUsage))
            {
                RedirectToAction("~/Shared/Error.cshtml");
            }
            

            var idSess = 0;

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
                    //sess = Convert.ToInt32(Request.Form["Session"]);
                    int.TryParse(Request.Form["Session"], out idSess); // MODIF: Loic turgeon et Cristian Zubieta
                //si la variable est null c'est que la page est chargée pour la première fois, donc il faut assigner la session à la session en cours, la plus grande dans la base de données
                else if (Request.Form["Session"] == null)
                    idSess = db.Session.Max(s => s.id_Sess);
                
            }

            if (m_IdTypeUsage == 2) //enseignant
            {
                ListeSession(idSess); //créer liste Session pour le dropdown
                
                var ens = from c in db.Groupe
                          where (c.id_Sess == idSess && c.id_Enseignant == m_IdPers) || (idSess == 0 && c.id_Enseignant == m_IdPers)
                          orderby c.NoGroupe
                          select c;

                ViewBag.IsEnseignant = true;

                return ens.ToList();
            }
            else //responsable
            {
                Int32.TryParse(Request.Form["Personne"], out m_IdPers); //seuls les responsables le voient
                ListeSession(idSess); //créer liste Session pour le dropdown
                ListePersonne(m_IdPers); //créer liste Enseignants pour le dropdown

                var resp = from c in db.Groupe
                           where c.id_Sess == (idSess == 0 ? c.id_Sess : idSess) && c.id_Enseignant == (m_IdPers == 0 ? c.id_Enseignant : m_IdPers)
                           orderby c.NoGroupe
                           select c;

                ViewBag.IsEnseignant = false;

                return resp.ToList();
            }
        }

        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListeSession(int _idSess = 0)
        {
            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", _idSess));

            ViewBag.Session = slSession;
        }

        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListePersonne(int idPersonne = 0)
        {
            var lPersonne = from p in db.Personne
                            where p.id_TypeUsag == 2
                            select p;
            var slPersonne = new List<SelectListItem>();
            slPersonne.AddRange(new SelectList(lPersonne, "id_Pers", "PrenomNom", idPersonne));

            ViewBag.Personne = slPersonne;
        }












        // GET: ConsulterCours/Details/5
        public ActionResult Details(int? id)
        {
            if (!connexionValide(m_IdTypeUsage))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var gr = from g in db.Groupe //obtenir les groupes en lien avec le cours trouvé
                         where g.id_Cours == id
                         orderby g.NoGroupe
                         select g;

                if (!gr.Any())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (m_IdTypeUsage == 2) //enseignant
                {
                    ViewBag.IsEnseignant = true;
                }
                else //responsable
                {
                    ViewBag.IsEnseignant = false;
                }

                return View(gr.ToList()); //renvoyer la liste des groupes en lien avec le cours
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
