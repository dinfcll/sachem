using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Classes_Sachem;
using sachem.Models;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    public class CoursSuiviController : Controller
    {
        private readonly IDataRepository _dataRepository;
        public CoursSuiviController()
        {
            _dataRepository = new BdRepository();
        }

        public CoursSuiviController(IDataRepository dataRepository)
        {
            this._dataRepository = dataRepository;
        }

        private void Valider([Bind(Include = "id_CoursReussi,id_Sess,id_Pers,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi, bool verif = false)
        {
            if (coursSuivi.id_Cours != null)
            {
                if (_dataRepository.AnyCoursSuiviWhere(r => r.id_Cours == coursSuivi.id_Cours &&
                                                            r.id_Pers == coursSuivi.id_Pers &&
                                                            r.id_Sess == coursSuivi.id_Sess) && verif)
                    ModelState.AddModelError(string.Empty,
                        Messages.ImpossibleEnregistrerCoursCarExisteListeCoursSuivis());
            }
            else
            {
                if(_dataRepository.AnyCoursSuiviWhere(r => r.autre_Cours == coursSuivi.autre_Cours &&
                                                           r.id_Pers == coursSuivi.id_Pers &&
                                                           r.id_Sess == coursSuivi.id_Sess) && verif)
                    ModelState.AddModelError(string.Empty,
                        Messages.ImpossibleEnregistrerCoursCarExisteListeCoursSuivis());
            }

            if (coursSuivi.id_Cours == null &&
                coursSuivi.autre_Cours == string.Empty ||
                coursSuivi.id_Cours != null &&
                coursSuivi.autre_Cours != string.Empty)
                ModelState.AddModelError(string.Empty, Messages.CompleterLesChamps("Cours" , "Autre cours"));

            if (coursSuivi.id_College == null &&
                coursSuivi.autre_College == string.Empty ||
                coursSuivi.id_College != null &&
                coursSuivi.autre_College != string.Empty)
                ModelState.AddModelError(string.Empty, Messages.CompleterLesChamps("Collège", "Autre collège"));

            if ((coursSuivi.id_Statut == null || coursSuivi.id_Statut == 1) && coursSuivi.resultat == null)
                ModelState.AddModelError(string.Empty, Messages.ResultatRequisSiReussi);
        }

        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CoursSuivi cs = _dataRepository.FindCoursSuivi((int)id);

            ViewBag.Personne = id;
            ViewBag.Resultat = "Create";

            ViewBag.Cours = Liste.ListeCours();
            ViewBag.College = Liste.ListeCollege();
            ViewBag.Statut = Liste.ListeStatutCours();
            ViewBag.Session = Liste.ListeSession();

            return View(cs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_CoursReussi,id_Sess,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi, int? id)
        {
            ViewBag.Cours = Liste.ListeCours();
            ViewBag.College = Liste.ListeCollege();
            ViewBag.Statut = Liste.ListeStatutCours();
            ViewBag.Session = Liste.ListeSession();

            if (id != null && _dataRepository.FindPersonne((int) id) == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id != null) coursSuivi.id_Pers = (int)id;

            ViewBag.Personne = coursSuivi.id_Pers;

            Valider(coursSuivi, true);

            if (ModelState.IsValid)
            {
                _dataRepository.AddCoursSuivi(coursSuivi);
                return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.Inscription });
            }

            return View(coursSuivi);
        }

        public ActionResult Edit(int? coursReussi, int? personne)
        {
            if (coursReussi == null || personne == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cs = _dataRepository.FindCoursSuivi((int)coursReussi);

            if (cs == null)
                return HttpNotFound();

            RemplirCour(cs.id_Cours);
            RemplirCollege(cs.id_College);

            ViewBag.Statut = cs.id_Statut == null
                ? Liste.ListeStatutCours()
                : Liste.ListeStatutCours(cs.id_Statut.Value);

            ViewBag.Session = cs.id_Sess == null
                ? Liste.ListeSession()
                : Liste.ListeSession(cs.id_Sess.Value);

            ViewBag.Resultat = "Edit";

            return View(cs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_CoursReussi,id_Sess,id_Pers,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi)
        {
            RemplirCour(coursSuivi.id_Cours);
            RemplirCollege(coursSuivi.id_College);

            if (coursSuivi.id_Statut != null) ViewBag.Statut = Liste.ListeStatutCours(coursSuivi.id_Statut.Value);
            if (coursSuivi.id_Sess != null) ViewBag.Session = Liste.ListeSession(coursSuivi.id_Sess.Value);

            Valider(coursSuivi);

            if (ModelState.IsValid)
            {
                _dataRepository.ModifyCoursSuivi(coursSuivi);
                return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.Inscription });
            }

            return View(coursSuivi);
        }

        public ActionResult Delete(int? coursReussi, int? personne)
        {            
            if (coursReussi == null || personne == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cs = _dataRepository.FindCoursSuivi((int)coursReussi);

            if (cs == null)
            {
                return HttpNotFound();
            }

            var vInscription = _dataRepository.GetSpecificInscription(cs.id_Pers);

            ViewBag.Inscription = vInscription.First();

            return View(cs);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idCoursReussi)
        {
            var coursSuivi = _dataRepository.FindCoursSuivi(idCoursReussi);

            _dataRepository.RemoveCoursSuivi(coursSuivi);
            return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.Inscription });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private void RemplirCour(int? idCours)
        {
            ViewBag.Cours = idCours == null
               ? Liste.ListeCours()
               : Liste.ListeCours(idCours.Value);
        }

        private void RemplirCollege(int? idCollege)
        {
            ViewBag.College = idCollege == null
               ? Liste.ListeCours()
               : Liste.ListeCours(idCollege.Value);
        }
    }
}
