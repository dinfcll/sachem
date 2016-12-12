using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using sachem.Models.DataAccess;
using static sachem.Classes_Sachem.ValidationAcces;
using sachem.Classes_Sachem;

namespace sachem.Controllers
{
    public class EnseignantController : Controller
    {
        private readonly IDataRepository _dataRepository;
        private const int IdEnseignant = 2;
        private const int IdResp = 3;

        public EnseignantController()
        {
            _dataRepository = new BdRepository();
        }

        public EnseignantController(IDataRepository dataRepository)
        {
            this._dataRepository = dataRepository;
        }

        [ValidationAccesSuper]
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;

            return View(Rechercher().ToPagedList(pageNumber, 20));
        }

        [HttpGet]
        [ValidationAccesSuper]
        public ActionResult Create()
        {
            RemplirDropList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,MP,ConfirmPassword,Courriel,DateNais,Actif")] Personne personne)
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
                    SachemIdentite.encrypterMPPersonne(ref personne);
                    _dataRepository.AddEnseignant(personne);

                    TempData["Success"] = Messages.AjouterUnGroupeAUnEnseignant(personne.NomUsager, personne.id_Pers);

                    return RedirectToAction("Index");
                }
            }
            RemplirDropList(personne);

            return View(personne);
        }

        [HttpGet]
        [ValidationAccesSuper]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var personne = _dataRepository.FindEnseignant((int)id);

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
        public ActionResult Edit([Bind(Include = "id_Pers, id_Sexe, id_TypeUsag, Nom, Prenom, NomUsager, MP, ConfirmPassword, Courriel, DateNais, Actif")] Personne personne)
        {
            Valider(personne);

            if (personne.MP != null && personne.ConfirmPassword != null)
            {
                SachemIdentite.encrypterMPPersonne(ref personne);
            }
            else
            {
                var mdp = _dataRepository.FindMdp(personne.id_Pers);
                personne.MP = mdp;

            }
            RemplirDropList(personne);
            if (ModelState.IsValid)
            {
                _dataRepository.DeclareModifiedEns(personne);

                TempData["Success"] = Messages.UsagerModfie(personne.NomUsager);

                return RedirectToAction("Index");
            }
            personne.MP = null;
            personne.ConfirmPassword = null;

            return View(personne);
        }

        [HttpGet]
        [ValidationAccesSuper]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var personne = _dataRepository.FindEnseignant((int)id);

            if(SessionBag.Current.id_pers == id)
            {
                TempData["Error"] = Messages.ResponsableSeSupprimerLuiMeme();
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
            if(_dataRepository.AnyGroupeWhere(g => g.id_Enseignant == id))
            {
                ModelState.AddModelError(string.Empty, Messages.EnseignantNePeutEtreSupprime);
            }
            if(_dataRepository.AnyjumelageWhere(g => g.id_Enseignant == id))
            {
                ModelState.AddModelError(string.Empty, Messages.EnseignantNonSupprimeJumelagePresent());
            }
            if (ModelState.IsValid)
            {
                var personne = _dataRepository.FindEnseignant(id);

                _dataRepository.RemoveEnseignant(id);
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
            ViewBag.Enseignant = Liste.ListeEnseignant();

            return _dataRepository.AllEnseignantResponsable(actif, IdResp, IdEnseignant);
        }

        private void Valider(Personne personne)
        {
            if (_dataRepository.AnyEnseignantWhere(x => x.NomUsager == personne.NomUsager && x.id_Pers != personne.id_Pers,personne))
            {
                ModelState.AddModelError(string.Empty, Messages.NomEnseignantDejaExistant(personne.NomUsager));
            }
            if (personne.MP != personne.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, Messages.MotsDePasseDoiventEtreIdentiques());
            }
        }

        private void RemplirDropList()
        {
            ViewBag.liste_sexe = _dataRepository.liste_sexe();
            ViewBag.id_TypeUsag = _dataRepository.liste_usag(IdResp,IdEnseignant);
        }

        private void RemplirDropList(Personne personne)
        {
            ViewBag.liste_sexe = _dataRepository.liste_sexe(personne);
            ViewBag.id_TypeUsag = _dataRepository.liste_usag(personne,IdResp, IdEnseignant);
        }
    }
}
