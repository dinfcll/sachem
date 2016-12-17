using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using sachem.Models.DataAccess;
using sachem.Methodes_Communes;

namespace sachem.Controllers
{
    public class EnseignantController : Controller
    {
        private readonly IDataRepository _dataRepository;

        public EnseignantController()
        {
            _dataRepository = new BdRepository();
        }

        public EnseignantController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;

            return View(Rechercher().ToPagedList(pageNumber, 20));
        }

        [HttpGet]
        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Create()
        {
            RemplirDropList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Personne personne)
        {
            Valider(personne);
            
            if (ModelState.IsValid)
            {
                if (personne.MP == null)
                {
                    ModelState.AddModelError("MP", Messages.ChampRequis);
                }
                else
                {
                    SachemIdentite.EncrypterMpPersonne(ref personne);
                    _dataRepository.AddPersonne(personne);

                    TempData["Success"] = Messages.EnseignantAjouterUnGroupeAEnseignant(personne.NomUsager, personne.id_Pers);

                    return RedirectToAction("Index");
                }
            }
            RemplirDropList(personne);

            return View(personne);
        }

        [HttpGet]
        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var personne = _dataRepository.FindPersonne((int)id);

            if (personne == null)
            {
                return HttpNotFound();
            }
            RemplirDropList(personne);
            personne.MP = "";

            return View(personne);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Personne personne)
        {
            Valider(personne);
            if (personne.MP != null && personne.ConfirmPassword != null)
            {
                SachemIdentite.EncrypterMpPersonne(ref personne);
            }
            else
            {
                var mdp = _dataRepository.FindMdp(personne.id_Pers);
                personne.MP = mdp;
            }
            
            if (ModelState.IsValid)
            {
                _dataRepository.EditPersonne(personne);

                TempData["Success"] = Messages.EnseignantModifierUsagerModfie(personne.NomUsager);

                return RedirectToAction("Index");
            }
            RemplirDropList(personne);
            personne.MP = "";
            personne.ConfirmPassword = "";

            return View(personne);
        }

        [HttpGet]
        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var personne = _dataRepository.FindPersonne((int)id);

            if(SessionBag.Current.id_pers == id)
            {
                TempData["Error"] = Messages.EnseignantSupprimerErreurLuiMeme;
                return RedirectToAction("Index", "Enseignant", null);
            }
            if (personne == null)
            {
                return HttpNotFound();
            }

            return View(personne);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,int? page)
        {
            var pageNumber = page ?? 1;
            if(_dataRepository.AnyGroupe(g => g.id_Enseignant == id))
            {
                ModelState.AddModelError(string.Empty, Messages.EnseignantSupprimerErreurLierCours);
            }
            if(_dataRepository.AnyJumelage(g => g.id_Enseignant == id))
            {
                ModelState.AddModelError(string.Empty, Messages.EnseignantSupprimerErreurJumelagePresent);
            }
            if (ModelState.IsValid)
            {
                var personne = _dataRepository.FindPersonne(id);

                _dataRepository.RemovePersonne(personne);
                ViewBag.Success = string.Format(Messages.EnseignantSupprime(personne.NomUsager));

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

        private bool SiEstCochee()
        {
            return !string.IsNullOrEmpty(Request.Form["Actif"]);
        }

        private IEnumerable<Personne> Rechercher()
        {
            var actif = true;

            if (SiEstCochee())
            {
                actif = Request.Form["Actif"].StartsWith("true");
            }
            ViewBag.Actif = actif;
            ViewBag.Enseignant = _dataRepository.ListeEnseignant();

            return _dataRepository.WherePersonne(x=>x.id_TypeUsag==(int)TypeUsagers.Responsable 
                                                && x.id_TypeUsag==(int)TypeUsagers.Enseignant 
                                                && x.Actif == actif);
        }

        private void Valider(Personne personne)
        {
            if (_dataRepository.AnyPersonne(x => x.NomUsager == personne.NomUsager && x.id_Pers != personne.id_Pers))
            {
                ModelState.AddModelError(string.Empty, Messages.EnseignantAjouterErreurExisteDeja(personne.NomUsager));
            }
            if (personne.MP != personne.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, Messages.MotsDePasseDoiventEtreIdentiques);
            }
        }

        private void RemplirDropList()
        {
            ViewBag.id_Sexe = _dataRepository.ListeSexe();
            ViewBag.id_TypeUsag = _dataRepository.ListeTypeUsager();
        }

        private void RemplirDropList(Personne personne)
        {
            ViewBag.id_Sexe = _dataRepository.ListeSexe(personne.id_Sexe);
            ViewBag.id_TypeUsag = _dataRepository.ListeTypeUsager(personne.id_TypeUsag);
        }
    }
}
