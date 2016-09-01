using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Controllers
{
    public class EnseignantController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();

        private void ListeSession(int Enseignant = 0)
        {
            var lEnseignant = db.Personne.AsNoTracking().OrderBy(p => p.Nom ).ThenBy(p => p.Prenom);
            var slEnseignant = new List<SelectListItem>();
            slEnseignant.AddRange(new SelectList(lEnseignant, "id_Pers", "NomSession", Enseignant)); // TO DO "NomSession"

            ViewBag.Enseignant = slEnseignant;
        }


        //Fonction pour gérer la recherche, elle est utilisée dans la suppression et dans l'index
        [NonAction]
        private IEnumerable<Personne> Rechercher()
        {
            var enseignant = 0;
            var actif = true;

            //Pour accéder à la valeur de cle envoyée en GET dans le formulaire
            //Request.QueryString["cle"]
            //Pour accéder à la valeur cle envoyée en POST dans le formulaire
            //Request.Form["cle"]
            //Cette méthode fonctionnera dans les 2 cas
            //Request["cle"]

            if (Request.RequestType == "GET" /*&& Session["DernRechCours"] != null && (string)Session["DernRechCoursUrl"] == Request.Url?.LocalPath*/)
            {
                /*var anciennerech = (string)Session["DernRechCours"];
                var tanciennerech = anciennerech.Split(';');

                if (tanciennerech[0] != "")
                {
                    enseignant = int.Parse(tanciennerech[0]);
                }
                if (tanciennerech[1] != "")
                {
                    actif = bool.Parse(tanciennerech[1]);
                }*/

            }
            else
            {
                /*//La méthode String.IsNullOrEmpty permet à la fois de vérifier si la chaine est NULL (lors du premier affichage de la page ou vide, lorsque le paramètre n'est pas appliquée 
                if (!string.IsNullOrEmpty(Request.Form["Session"]))
                    enseignant = Convert.ToInt32(Request.Form["Session"]);
                //si la variable est null c'est que la page est chargée pour la première fois, donc il faut assigner la session à la session en cours, la plus grande dans la base de données
                else if (Request.Form["Session"] == null)
                    enseignant = db.Session.Max(s => s.id_Sess);*/

                //la méthode Html.checkbox crée automatiquement un champ hidden du même nom que la case à cocher, lorsque la case n'est pas cochée une seule valeur sera soumise, par contre lorsqu'elle est cochée
                //2 valeurs sont soumises, il faut alors vérifier que l'une des valeurs est à true pour vérifier si elle est cochée
                if (!string.IsNullOrEmpty(Request.Form["Actif"]))
                    actif = Request.Form["Actif"].Contains("true");
            }

            ViewBag.Actif = actif;

            ListeSession(enseignant);

            var cours = from c in db.Personne
                        where c.id_TypeUsag == 2
                        && c.Actif == actif
                        orderby c.Nom,c.Prenom
                        select c;

            //on enregistre la recherche
            /*Session["DernRechCours"] = enseignant + ";" + actif;
            Session["DernRechCoursUrl"] = Request.Url?.LocalPath;*/

            return cours.ToList();
        }

        // GET: Enseignant
        public ActionResult Index()
        {
            var personne = db.Personne.Include(p => p.p_Sexe).Include(p => p.p_TypeUsag);
            return View(personne.ToList());
        }

        // GET: Enseignant/Create
        public ActionResult Create()
        {
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag");
            return View();
        }

        // POST: Enseignant/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule,MP,Courriel,Telephone,DateNais,Actif")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                db.Personne.Add(personne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // GET: Enseignant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // POST: Enseignant/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule,MP,Courriel,Telephone,DateNais,Actif")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // GET: Enseignant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // POST: Enseignant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personne personne = db.Personne.Find(id);
            db.Personne.Remove(personne);
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
    }
}
