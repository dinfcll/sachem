using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using sachem.Models.DataAccess;


namespace sachem.Controllers
{
    public class CoursSuiviController : Controller
    {
        private readonly IDataRepository dataRepository;
        public CoursSuiviController()
        {
            dataRepository = new BdRepository();
        }

        public CoursSuiviController(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        [NonAction]
        private void ListeCours(int cours = 0)
        {
            var lCours = dataRepository.GetCours();
            var slCours = new List<SelectListItem>();
            slCours.AddRange(new SelectList(lCours, "id_Cours", "CodeNom", cours));
            ViewBag.id_Cours = slCours;
        }

        //Validation des champs cours et collège
        [NonAction]
        private void Valider([Bind(Include = "id_CoursReussi,id_Sess,id_Pers,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi, bool verif = false)
        {
            //Validation seulement lors de l'ajout
            if (coursSuivi.id_Cours != null)
            {
                if (dataRepository.AnyCoursSuiviWhere(r => r.id_Cours == coursSuivi.id_Cours && r.id_Pers == coursSuivi.id_Pers && r.id_Sess == coursSuivi.id_Sess) && verif)
                    ModelState.AddModelError(string.Empty, Messages.I_036());
            }
            else
            {
                if(dataRepository.AnyCoursSuiviWhere(r => r.autre_Cours == coursSuivi.autre_Cours && r.id_Pers == coursSuivi.id_Pers && r.id_Sess == coursSuivi.id_Sess) && verif)
                    ModelState.AddModelError(string.Empty, Messages.I_036());
            }

            if (coursSuivi.id_Cours == null && coursSuivi.autre_Cours == string.Empty || coursSuivi.id_Cours != null && coursSuivi.autre_Cours != string.Empty)
                ModelState.AddModelError(string.Empty, Messages.C_009("Cours" , "Autre cours"));

            if (coursSuivi.id_College == null && coursSuivi.autre_College == string.Empty || coursSuivi.id_College != null && coursSuivi.autre_College != string.Empty)
                ModelState.AddModelError(string.Empty, Messages.C_009("Collège", "Autre collège"));

            //Verif si resultat nécéssaire et présent
            if ((coursSuivi.id_Statut == null || coursSuivi.id_Statut == 1) && coursSuivi.resultat == null)
                ModelState.AddModelError(string.Empty, Messages.C_010);
        }

        // GET: CoursSuivi/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursSuivi cs = dataRepository.FindCoursSuivi((int)id);

            ViewBag.idPers = id;
            ViewBag.Resultat = "Create";

            ListeCours();
            ViewBag.id_College = Liste.ListeCollege();
            ViewBag.id_Statut = Liste.ListeStatutCours();
            ViewBag.id_Sess = Liste.ListeSession();
            return View(cs);
        }

        // POST: CoursSuivi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_CoursReussi,id_Sess,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi, int? id)
        {
            ListeCours();
            ViewBag.id_College = Liste.ListeCollege();
            ViewBag.id_Statut = Liste.ListeStatutCours();
            ViewBag.id_Sess = Liste.ListeSession();

            if (dataRepository.FindPersonne((int) id) == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            coursSuivi.id_Pers = (int)id;
            ViewBag.idPers = coursSuivi.id_Pers;

            Valider(coursSuivi, true);

            if (ModelState.IsValid)
            {
                dataRepository.AddCoursSuivi(coursSuivi);
                return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.id_Inscription });
            }
            return View(coursSuivi);
        }

        // GET: CoursSuivi/Edit/5
        public ActionResult Edit(int? coursReussi, int? personne)
        {
            if (coursReussi == null || personne == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            CoursSuivi cs = dataRepository.FindCoursSuivi((int)coursReussi);

            if (cs == null)
                return HttpNotFound();

            if (cs.id_Cours == null)
                ListeCours();
            else
                ListeCours(cs.id_Cours.Value);

            if (cs.id_College == null)
                ViewBag.id_College = Liste.ListeCollege();
            else
                ViewBag.id_College = Liste.ListeCollege(cs.id_College.Value);

            if (cs.id_Statut == null)
                ViewBag.id_Statut = Liste.ListeStatutCours();
            else
                ViewBag.id_Statut = Liste.ListeStatutCours(cs.id_Statut.Value);

            if (cs.id_Sess == null)
                ViewBag.id_Sess = Liste.ListeSession();
            else
                ViewBag.id_Sess = Liste.ListeSession(cs.id_Sess.Value);

            ViewBag.Resultat = "Edit";

            return View(cs);
        }

        // POST: CoursSuivi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_CoursReussi,id_Sess,id_Pers,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi)
        {
            if (coursSuivi.id_Cours == null)
                ListeCours();
            else
                ListeCours(coursSuivi.id_Cours.Value);

            if (coursSuivi.id_College == null)
                ViewBag.id_College = Liste.ListeCollege();
            else
                ViewBag.id_College = Liste.ListeCollege(coursSuivi.id_College.Value);

            ViewBag.id_Statut = Liste.ListeStatutCours(coursSuivi.id_Statut.Value);
            ViewBag.id_Sess = Liste.ListeSession(coursSuivi.id_Sess.Value);

            Valider(coursSuivi);

            if (ModelState.IsValid)
            {
                dataRepository.ModifyCoursSuivi(coursSuivi);
                return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.id_Inscription });
            }
            return View(coursSuivi);
        }

        // GET: CoursSuivi/Delete/5
        public ActionResult Delete(int? coursReussi, int? personne)
        {            
            if (coursReussi == null || personne == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CoursSuivi cs = dataRepository.FindCoursSuivi((int)coursReussi);

            if (cs == null)
            {
                return HttpNotFound();
            }

            var vInscription = dataRepository.GetSpecificInscription(cs.id_Pers);

            ViewBag.id_insc = vInscription.First();

            return View(cs);
        }

        // POST: CoursSuivi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id_CoursReussi)
        {
            CoursSuivi coursSuivi = dataRepository.FindCoursSuivi(id_CoursReussi);

            dataRepository.RemoveCoursSuivi(coursSuivi);
            return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.id_Inscription });
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
