using static sachem.Classes_Sachem.ValidationAcces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;


namespace sachem.Controllers
{
    public class CoursController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();

        

        //fonctions permettant d'initialiser les listes déroulantes

        [NonAction]
        private void ListeSession(int Session = 0)
        {
            
            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", Session));

            ViewBag.Session = slSession;
        }

        //méthode gérant les validations de contexte de l'ajout et de la modification 
        [NonAction]
        private void Valider([Bind(Include = "id_Cours,Code,Nom,Actif")] Cours cours)
        {
            if (db.Cours.Any(r => r.Code == cours.Code && r.id_Cours != cours.id_Cours))
                ModelState.AddModelError(string.Empty, Messages.I_002(cours.Code));
        }

        //Fonction pour gérer la recherche, elle est utilisée dans la suppression et dans l'index
        [NonAction]
        private IEnumerable<Cours> Rechercher()
        {
            var sess = 0;
            var actif = true;

            

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
                    sess = int.Parse(tanciennerech[0]);
                }
                if (tanciennerech[1] != "")
                {
                    actif = bool.Parse(tanciennerech[1]);
                }

            }
            else
            {
                //La méthode String.IsNullOrEmpty permet à la fois de vérifier si la chaine est NULL (lors du premier affichage de la page ou vide, lorsque le paramètre n'est pas appliquée 
                if (!string.IsNullOrEmpty(Request.Form["Session"]))
                    //sess = Convert.ToInt32(Request.Form["Session"]);
                    int.TryParse(Request.Form["Session"], out sess); // MODIF: Loic turgeon et Cristian Zubieta
                //si la variable est null c'est que la page est chargée pour la première fois, donc il faut assigner la session à la session en cours, la plus grande dans la base de données
                else if (Request.Form["Session"] == null)
                    sess = db.Session.Max(s => s.id_Sess);

                //la méthode Html.checkbox crée automatiquement un champ hidden du même nom que la case à cocher, lorsque la case n'est pas cochée une seule valeur sera soumise, par contre lorsqu'elle est cochée
                //2 valeurs sont soumises, il faut alors vérifier que l'une des valeurs est à true pour vérifier si elle est cochée
                if (!string.IsNullOrEmpty(Request.Form["Actif"]))
                    actif = Request.Form["Actif"].Contains("true");
            }

            ViewBag.Actif = actif;

            ListeSession(sess);

            var cours = from c in db.Cours
                        where (db.Groupe.Any(r => r.id_Cours == c.id_Cours && r.id_Sess == sess) || sess == 0)
                        && c.Actif == actif
                        orderby c.Code
                        select c;

            //on enregistre la recherche
            Session["DernRechCours"] = sess + ";" + actif;
            Session["DernRechCoursUrl"] = Request.Url?.LocalPath;

            return cours.ToList();
        }

        // GET: Cours
        [ValidationAccesSuper]
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;

            return View(Rechercher().ToPagedList(pageNumber, 20));
        }


        // GET: Cours/Create
        [ValidationAccesSuper]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cours/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Cours,Code,Nom,Actif")] Cours cours, int? page)
        {
            Valider(cours);

            if (ModelState.IsValid)
            {
                db.Cours.Add(cours);
                db.SaveChanges();

                TempData["Success"] = string.Format(Messages.I_003(cours.Nom));
                return RedirectToAction("Index");
            }


            return View(cours);

        }

        // GET: Cours/Edit/5
        [ValidationAccesSuper]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var cours = db.Cours.Find(id);

            if (cours == null)
                return HttpNotFound();

            if (cours.Groupe.Any())
                ViewBag.DisEns = "True";

            return View(cours);

        }

        // POST: Cours/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Cours,Code,Nom,Actif")] Cours cours, int? page)
        {
            Valider(cours);

            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = string.Format(Messages.I_003(cours.Nom));
                return RedirectToAction("Index");
            }

            return View(cours);
        }

        // GET: Cours/Delete/5
        [ValidationAccesSuper]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var cours = db.Cours.Find(id);

            if (cours == null)
                return HttpNotFound();

            return View(cours);
        }

        // POST: Cours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? page)
        {
            var pageNumber = page ?? 1;
            if (db.Groupe.Any(g => g.id_Cours == id))
            {
                ModelState.AddModelError(string.Empty, Messages.I_001());
            }

            if (ModelState.IsValid)
            {
                var cours = db.Cours.Find(id);
                db.Cours.Remove(cours);
                db.SaveChanges();
                ViewBag.Success = string.Format(Messages.I_009(cours.Nom));
            }

            //plutôt que de faire un RedirectToAction qui aurait comme effet de remmettre à true ModelState.IsValid
            return View("Index", Rechercher().ToPagedList(pageNumber, 20));
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
