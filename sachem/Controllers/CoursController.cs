using static sachem.Classes_Sachem.ValidationAcces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    public class CoursController : Controller
    {
        private readonly IDataRepository dataRepository;

        public CoursController()
        {
            dataRepository = new BdRepository();
        }

        public CoursController(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        [NonAction]
        private void ListeSession(int Session = 0)
        {
            var lSessions = dataRepository.GetSessions();
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", Session));

            ViewBag.Session = slSession;
        }

        [NonAction]
        private void Valider([Bind(Include = "id_Cours,Code,Nom,Actif")] Cours cours)
        {
            if (dataRepository.AnyCoursWhere(r => r.Code == cours.Code && r.id_Cours != cours.id_Cours))
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
                    sess = dataRepository.SessionEnCours();

                if (!string.IsNullOrEmpty(Request.Form["Actif"]))
                    actif = Request.Form["Actif"].Contains("true");
            }

            ViewBag.Actif = actif;

            ListeSession(sess);

            var cours = from c in dataRepository.AllCours()
                        where (dataRepository.AnyGroupeWhere(r => r.id_Cours == c.id_Cours && r.id_Sess == sess) || sess == 0)
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
                dataRepository.AddCours(cours);

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

            var cours = dataRepository.FindCours(id.Value);

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
                dataRepository.DeclareModified(cours);

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

            var cours = dataRepository.FindCours(id.Value);

            if (cours == null)
                return HttpNotFound();

            return View(cours);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? page)
        {
            var pageNumber = page ?? 1;
            if (dataRepository.AnyGroupeWhere(g => g.id_Cours == id))
            {
                ModelState.AddModelError(string.Empty, Messages.I_001());
            }

            if (ModelState.IsValid)
            {
                var cours = dataRepository.FindCours(id);

                dataRepository.RemoveCours(cours);

                ViewBag.Success = string.Format(Messages.I_009(cours.Nom));
            }

            return View("Index", Rechercher().ToPagedList(pageNumber, 20));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dataRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
