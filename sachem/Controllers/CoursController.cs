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

        [NonAction]
        private void ListeSession(int Session = 0)
        {
            
            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", Session));

            ViewBag.Session = slSession;
        }

        [NonAction]
        private void Valider([Bind(Include = "id_Cours,Code,Nom,Actif")] Cours cours)
        {
            if (db.Cours.Any(r => r.Code == cours.Code && r.id_Cours != cours.id_Cours))
                ModelState.AddModelError(string.Empty, Messages.I_002(cours.Code));
        }

        [NonAction]
        private IEnumerable<Cours> Rechercher()
        {
            var sess = 0;
            var actif = true;

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
                if (!string.IsNullOrEmpty(Request.Form["Session"]))
                    int.TryParse(Request.Form["Session"], out sess);
                else if (Request.Form["Session"] == null)
                    sess = db.Session.Max(s => s.id_Sess);

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

            Session["DernRechCours"] = sess + ";" + actif;
            Session["DernRechCoursUrl"] = Request.Url?.LocalPath;

            return cours.ToList();
        }

        [ValidationAccesSuper]
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;

            return View(Rechercher().ToPagedList(pageNumber, 20));
        }

        [ValidationAccesSuper]
        public ActionResult Create()
        {
            return View();
        }

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
