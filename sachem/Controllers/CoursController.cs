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
    public class CoursController : Controller
    {
        private readonly IDataRepository _dataRepository;

        public CoursController()
        {
            _dataRepository = new BdRepository();
        }

        public CoursController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        private void Valider([Bind(Include = "id_Cours,Code,Nom,Actif")] Cours cours)
        {
            if (_dataRepository.AnyCoursWhere(r => r.Code == cours.Code && r.id_Cours != cours.id_Cours))
                ModelState.AddModelError(string.Empty, Messages.CoursAjouterErreurCodeExisteDeja(cours.Code));
        }

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
                    sess = _dataRepository.SessionEnCours();

                if (!string.IsNullOrEmpty(Request.Form["Actif"]))
                    actif = Request.Form["Actif"].Contains("true");
            }

            ViewBag.Actif = actif;

            ViewBag.Session = _dataRepository.ListeSession(sess);

            var cours = from c in _dataRepository.AllCours()
                        where (_dataRepository.AnyGroupeWhere(r => r.id_Cours == c.id_Cours && r.id_Sess == sess) || sess == 0)
                        && c.Actif == actif
                        orderby c.Code
                        select c;

            Session["DernRechCours"] = sess + ";" + actif;
            Session["DernRechCoursUrl"] = Request.Url?.LocalPath;

            return cours.ToList();
        }

        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;

            return View(Rechercher().ToPagedList(pageNumber, 20));
        }

        [ValidationAcces.ValidationAccesSuper]
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
                _dataRepository.AddCours(cours);

                TempData["Success"] = string.Format(Messages.CoursEnregistre(cours.Nom));
                return RedirectToAction("Index");
            }

            return View(cours);

        }

        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var cours = _dataRepository.FindCours(id.Value);

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
                _dataRepository.DeclareModified(cours);

                TempData["Success"] = string.Format(Messages.CoursEnregistre(cours.Nom));

                return RedirectToAction("Index");
            }

            return View(cours);
        }

        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var cours = _dataRepository.FindCours(id.Value);

            if (cours == null)
                return HttpNotFound();

            return View("Delete", cours);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? page)
        {
            var pageNumber = page ?? 1;
            if (_dataRepository.AnyGroupeWhere(g => g.id_Cours == id))
            {
                ModelState.AddModelError(string.Empty, Messages.CoursSupprimerErreurGroupeAssocie);
            }

            if (ModelState.IsValid)
            {
                var cours = _dataRepository.FindCours(id);

                _dataRepository.RemoveCours(cours);

                ViewBag.Success = string.Format(Messages.CoursSupprime(cours.Nom));
            }

            return View("Index", Rechercher().ToPagedList(pageNumber, 20));
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
