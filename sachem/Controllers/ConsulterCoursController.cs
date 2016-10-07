using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using sachem.Models;

namespace sachem.Controllers
{
    public class ConsulterCoursController : Controller
    {

        //int m_IdPers = 2; // 1 = la seule résponsable, 2-9 = enseignants
        //int m_IdTypeUsage = 3; // 2 = enseignant, 3 = responsable

        int m_IdPers = SessionBag.Current.id_Pers;
        int m_IdTypeUsage = SessionBag.Current.id_TypeUsag; // 2 = enseignant, 3 = responsable

        private SACHEMEntities db = new SACHEMEntities();

        // GET: ConsulterCours
        public ActionResult Index()
        {
            if (connexionValide(m_IdTypeUsage))
                return View(AfficherCoursAssignes());
            else
                return View("~/Views/Shared/Error.cshtml");

        }


        //Fonction pour afficher les cours assignés à l'utilisateur connecté
        [NonAction]
        private IEnumerable<Groupe> AfficherCoursAssignes()
        {
            if (!connexionValide(m_IdTypeUsage))
            {
                RedirectToAction("~/Shared/Error.cshtml");
            }


                /*var tidSess = from n in db.Session
                        group n by n.Annee into g
                        select g.OrderByDescending(t => t.Annee).FirstOrDefault();*/

                //int idSess = 0;

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
               /* if (tanciennerech[1] != "")
                {
                    actif = bool.Parse(tanciennerech[1]);
                }*/

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

                /*//la méthode Html.checkbox crée automatiquement un champ hidden du même nom que la case à cocher, lorsque la case n'est pas cochée une seule valeur sera soumise, par contre lorsqu'elle est cochée
                //2 valeurs sont soumises, il faut alors vérifier que l'une des valeurs est à true pour vérifier si elle est cochée
                if (!string.IsNullOrEmpty(Request.Form["Actif"]))
                    actif = Request.Form["Actif"].Contains("true");*/
            }



            //idSess = db.Session.Max(s => s.id_Sess);

            var cours = from c in db.Cours select c;

            if (m_IdTypeUsage == 2) //enseignant
            {
                //Int32.TryParse(Request.Form["Session"], out idSess);
                ListeSession(idSess); //créer liste Session pour le dropdown

                //.DistinctBy(c =>c.id_Cours)
                var ens = from c in db.Groupe
                          where (c.id_Sess == idSess && c.id_Enseignant == m_IdPers) || (idSess == 0 && c.id_Enseignant == m_IdPers)
                          select c;

                ViewBag.IsEnseignant = true;

                return ens.ToList();
            }
            else //responsable
            {
                Int32.TryParse(Request.Form["Personne"], out m_IdPers); //seuls les responsables le voient
                //Int32.TryParse(Request.Form["Session"], out idSess);
                ListeSession(idSess); //créer liste Session pour le dropdown
                ListePersonne(m_IdPers); //créer liste Enseignants pour le dropdown

                var resp = from c in db.Groupe
                           where c.id_Sess == (idSess == 0 ? c.id_Sess : idSess) && c.id_Enseignant == (m_IdPers == 0 ? c.id_Enseignant : m_IdPers)
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
            //var lPersonne = db.Personne.AsNoTracking().OrderBy(p => p.Prenom).ThenBy(p => p.Nom);
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








        // GET: ConsulterCours/Create
        public ActionResult Create()
        {
            ViewBag.id_Sess = new SelectList(db.p_HoraireInscription, "id_Sess", "id_Sess");
            ViewBag.id_Saison = new SelectList(db.p_Saison, "id_Saison", "Saison");
            return View();
        }


        // POST: ConsulterCours/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Sess,id_Saison,Annee")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Session.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Sess = new SelectList(db.p_HoraireInscription, "id_Sess", "id_Sess", session.id_Sess);
            ViewBag.id_Saison = new SelectList(db.p_Saison, "id_Saison", "Saison", session.id_Saison);
            return View(session);
        }

        // GET: ConsulterCours/Edit/5
        /*public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Groupe groupe = db.Groupe.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }


            //return View();

            return View("Edit", "GroupesController", id); //appel de la vue de Loïc pour modifier le groupe sélectionné
        }*/

        // POST: ConsulterCours/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Sess,id_Saison,Annee")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Sess = new SelectList(db.p_HoraireInscription, "id_Sess", "id_Sess", session.id_Sess);
            ViewBag.id_Saison = new SelectList(db.p_Saison, "id_Saison", "Saison", session.id_Saison);
            return View(session);
        }

        // GET: ConsulterCours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: ConsulterCours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Session.Find(id);
            db.Session.Remove(session);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool connexionValide(int idTypeCompte)
        {
            bool valide = true;

            if (idTypeCompte < 2 || idTypeCompte > 4)
            {
                valide = false;
            }

                return valide;

        }
    }
}
