using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using System.Collections.Generic;
using sachem.Methodes_Communes;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    public class EtudiantController : RechercheEtudiantController
    {
        public const string AnneePremiersCaracteres = "20";
        private readonly IDataRepository _dataRepository;

        public EtudiantController()
        {
            _dataRepository = new BdRepository();
        }

        public EtudiantController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Index(int? page)
        {
            NoPage = page ?? NoPage;

            return View(Rechercher().ToPagedList(NoPage, 20));
        }

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Create()
        {
            ViewBag.id_Sexe = DataRepository.ListeSexe();
            ViewBag.id_TypeUsag = DataRepository.ListeTypeUsager();
            ViewBag.id_Programme = DataRepository.ListeProgrammmeCode();
            ViewBag.id_Session = DataRepository.ListeSession();
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Create(Personne personne)//int? page
        {
            var etuProg = new PersonneEtuProgParent();

            personne.id_TypeUsag = 1;
            personne.Actif = true;
            personne.Telephone = SachemIdentite.FormatTelephone(personne.Telephone);
            personne.Matricule = AnneePremiersCaracteres + personne.Matricule;
            etuProg.personne = personne;

            ViewBag.id_Sexe = DataRepository.ListeSexe();
            ViewBag.id_TypeUsag = DataRepository.ListeTypeUsager();
            ViewBag.id_Programme = DataRepository.ListeProgrammmeCode();
            ViewBag.id_Session = DataRepository.ListeSession();

            Valider(etuProg.personne);
            if (etuProg.personne.MP == null)
            {
                ModelState.AddModelError("Mot de passe", "Veuillez entrer un mot de passe");
                TempData["Echec"] = "Veuillez entrer un mot de passe";
            }
            else
            {
                if (ConfirmeMdp(personne.MP, personne.ConfirmPassword) == false)
                {
                return View(etuProg);
                }
            }

            var etuprog = new EtuProgEtude();
            if (Request.Form["id_Programme"] != "" && Request.Form["id_Session"] != "")
            {
                etuprog.id_ProgEtu = int.Parse(Request.Form["id_Programme"]);
                etuprog.id_Sess = int.Parse(Request.Form["id_Session"]);
                etuprog.id_Etu = personne.id_Pers;
                _dataRepository.AddEtuProgEtude(etuprog);
            }

            if (!ModelState.IsValid) return View(etuProg);
            etuProg.personne.MP = SachemIdentite.EncrypterChaine(etuProg.personne.MP); 
            etuProg.personne.ConfirmPassword = SachemIdentite.EncrypterChaine(etuProg.personne.ConfirmPassword);
            _dataRepository.AddPersonne(etuProg.personne);
            personne.Telephone = SachemIdentite.RemettreTel(personne.Telephone);

            TempData["Success"] = Messages.EtudiantEnregistre(personne.Matricule7);
                
            return RedirectToAction("Index");
        }
        // GET: Etudiant/Edit/5
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var personne = _dataRepository.FindPersonne(id.Value);
            if (personne == null)
            {
                return HttpNotFound();
            }
            
            if (personne.Telephone != null)
            {
                personne.Telephone = SachemIdentite.RemettreTel(personne.Telephone);
            }

            var programmes = _dataRepository
                .WhereEtuProgEtude(d => d.id_Etu == personne.id_Pers)
                .OrderBy(d => d.ProgrammeEtude.Code);

            ViewBag.id_Sexe = DataRepository.ListeSexe(personne.id_Sexe);
            ViewBag.id_TypeUsag = DataRepository.ListeTypeUsager(personne.id_TypeUsag);
            ViewBag.id_Programme = DataRepository.ListeProgrammmeCode();
            ViewBag.id_Session = DataRepository.ListeSession();
            var etuProg = new PersonneEtuProgParent
            {
                personne = personne,
                EtuProgEtu = programmes.ToList()
            };
            
            return View(etuProg);
        }

        [HttpPost]
        [AcceptVerbs("Get", "Post")]
        public virtual JsonResult ActualisePEtu(int idProg, int idPers, int valider = 0)
        {
            var personne = _dataRepository.FindPersonne(idPers);
            var etuprog = _dataRepository.FindEtuProgEtude(idProg);
            var programme = _dataRepository.WhereEtuProgEtude(d => d.id_Etu == personne.id_Pers)
                .OrderBy(d => d.ProgrammeEtude.Code);

            var etuProgEtu = _dataRepository.WhereEtuProgEtude(x => x.id_EtuProgEtude == idProg);
            if (!_dataRepository.AnyCoursSuivi(c => c.id_Pers == etuprog.id_Etu && c.id_Sess == etuprog.id_Sess))
            {
                TempData["Success"] = Messages.EtudiantProgrammeSupprimerListeEtudiant(etuprog.ProgrammeEtude.CodeNomProgramme);
                _dataRepository.RemoveRangeEtuProgEtude(etuProgEtu);
            }
            else
            {
                if (programme.Count() > 1)
                {
                    TempData["Success"] = Messages.EtudiantProgrammeSupprimerListeEtudiant(etuprog.ProgrammeEtude.CodeNomProgramme);
                    _dataRepository.RemoveRangeEtuProgEtude(etuProgEtu);
                }
                else
                {
                    TempData["Echec"] = Messages.EtudiantProgrammeSupprimerErreur(etuprog.ProgrammeEtude.CodeNomProgramme);
                }
            }
            var prog = ObtenirProgEtu(idPers);
            return Json(prog.ToList(), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<object> ObtenirProgEtu(int idPers)
        {
           return _dataRepository.WhereEtuProgEtude(sel => sel.id_Etu == idPers)
                .Select(e => new { e.ProgrammeEtude.NomProg, e.id_Etu, e.id_EtuProgEtude, e.ProgrammeEtude.Code })
                .Distinct().AsEnumerable();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Edit(Personne personne)
        {
            var etuProg = new PersonneEtuProgParent();
            personne.id_TypeUsag = 1;
            personne.Telephone = SachemIdentite.FormatTelephone(personne.Telephone);
            etuProg.personne = personne;
            const string message = "Le mot de passe doit contenir 6 caratères";

            var etuprog = new EtuProgEtude();

            etuProg.EtuProgEtu = _dataRepository.WhereEtuProgEtude(d => d.id_Etu == etuProg.personne.id_Pers).OrderBy(d => d.ProgrammeEtude.Code).ToList();
            if (Request.Form["id_Programme"] != "" && Request.Form["id_Session"] != ""&& ConfirmeMdp(personne.MP, personne.ConfirmPassword))
            {
                etuprog.id_ProgEtu = int.Parse(Request.Form["id_Programme"]);
                etuprog.id_Sess = int.Parse(Request.Form["id_Session"]);
                etuprog.id_Etu = personne.id_Pers;
                _dataRepository.AddEtuProgEtude(etuprog);
            }
            if (ConfirmeMdp(personne.MP, personne.ConfirmPassword))
            {
                if (personne.MP != null && personne.MP.Length < 6)
                {
                    ModelState.AddModelError("ConfirmPassword", message);
                    TempData["Echec"] = message;  
                }
                else
                {
                    if (personne.MP != null && personne.ConfirmPassword != null)
                    {
                        etuProg.personne.MP = SachemIdentite.EncrypterChaine(etuProg.personne.MP);
                        etuProg.personne.ConfirmPassword = SachemIdentite.EncrypterChaine(etuProg.personne.ConfirmPassword); 
                    }
                    else
                    {
                        personne.MP = _dataRepository.WherePersonne(c => c.id_Pers == personne.id_Pers).Select(c => c.MP).SingleOrDefault();
                        personne.ConfirmPassword = personne.MP;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                _dataRepository.EditPersonne(etuProg.personne);
                TempData["Success"] = Messages.EtudiantModifie(personne.NomPrenom);
                return RedirectToAction("Index");
            }

            ViewBag.id_Sexe = _dataRepository.ListeSexe(personne.id_Sexe);
            ViewBag.id_TypeUsag = _dataRepository.ListeTypeUsager(etuProg.personne.id_TypeUsag);
            ViewBag.id_Programme = _dataRepository.ListeProgrammmeCode();
            ViewBag.id_Session = _dataRepository.ListeSession();
            return View(etuProg);
        }

        // GET: Etudiant/Delete/5
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var personne = _dataRepository.FindPersonne(id.Value);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // POST: Etudiant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult DeleteConfirmed(int id,int? page)
        {
            var personne = _dataRepository.FindPersonne(id);
            var inscription = _dataRepository.WhereInscription(x => x.id_Pers == personne.id_Pers).FirstOrDefault();

            if (_dataRepository.AnyGroupeEtudiant(x => x.id_Etudiant == personne.id_Pers))
            {
                ModelState.AddModelError(string.Empty, Messages.EtudiantSupprimerErreurLierGroupe);
                TempData["Echec"] = Messages.EtudiantSupprimerErreurLierGroupe;
            }
             
            if (inscription != null)
            {
                if (_dataRepository.AnyJumelage(x => x.id_InscEleve == inscription.id_Inscription))
                {
                    ModelState.AddModelError(string.Empty, Messages.EtudiantSupprimerErreurJumele);
                    TempData["Echec"] = Messages.EtudiantSupprimerErreurJumele;
                }
            }
            if (!ModelState.IsValid) return RedirectToAction("Index");
            {
                var etuProgEtu = _dataRepository.WhereEtuProgEtude(x => x.id_Etu == personne.id_Pers);
                _dataRepository.RemoveRangeEtuProgEtude(etuProgEtu, false);
                var groupeEtu = _dataRepository.WhereGroupeEtudiant(y => y.id_Etudiant == personne.id_Pers);
                _dataRepository.RemoveRangeGroupeEtudiant(groupeEtu, false);
                var jumul = _dataRepository.WhereJumelage(z => z.id_InscEleve == personne.id_Pers);
                _dataRepository.RemoveRangeJumelage(jumul, false);
                var inscri = _dataRepository.WhereInscription(a => a.id_Pers == personne.id_Pers);
                _dataRepository.RemoveRangeInscription(inscri, false);
                var coursSuiv = _dataRepository.WhereCoursSuivi(b => b.id_Pers == personne.id_Pers);
                _dataRepository.RemoveRangeCoursSuivi(coursSuiv, false);
                _dataRepository.RemovePersonne(personne);
                TempData["Success"] = Messages.EtudiantSupprime(personne.NomPrenom);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteProgEtu(int idProg, int idPers, int valider = 0)
        {
            var personne = _dataRepository.FindPersonne(idPers);
            var etuprog = _dataRepository.FindEtuProgEtude(idProg);
            var prog = _dataRepository.WhereEtuProgEtude(d => d.id_Etu == personne.id_Pers).OrderBy(d => d.ProgrammeEtude.Code);
            
            TempData["Question"] = Messages.EtudiantProgrammeEtudeQuestionSupprimerProgramme(etuprog.ProgrammeEtude.CodeNomProgramme);
            var etuProgEtu = _dataRepository.WhereEtuProgEtude(x => x.id_EtuProgEtude == idProg);
            if (valider != 0)
            {
                TempData["Question"] = null;
            }
            if (valider == 1)
            {
                if (!_dataRepository.AnyCoursSuivi(c => c.id_Pers == etuprog.id_Etu && c.id_Sess == etuprog.id_Sess))
                {
                    TempData["Success"] = Messages.EtudiantProgrammeSupprimerListeEtudiant(etuprog.ProgrammeEtude.CodeNomProgramme);
                    _dataRepository.RemoveRangeEtuProgEtude(etuProgEtu);
                }
                else
                {
                    if (prog.Count() > 1)
                    {
                        TempData["Success"] = Messages.EtudiantProgrammeSupprimerListeEtudiant(etuprog.ProgrammeEtude.CodeNomProgramme);
                        _dataRepository.RemoveRangeEtuProgEtude(etuProgEtu);
                    }
                    else
                    {
                        TempData["Echec"] = Messages.EtudiantProgrammeSupprimerErreur(etuprog.ProgrammeEtude.CodeNomProgramme);
                    }   
                }
                return RedirectToAction("Edit", "Etudiant", new { id = idPers });
            }
            TempData["id_Pers"] = idPers;         
            TempData["id_Prog"] = idProg;
            return RedirectToAction("Edit", "Etudiant", new { id = idPers });
         }

        private void Valider(Personne personne)
        {
            if (personne.Matricule7 == null)
            {
                ModelState.AddModelError("Matricule7", Messages.ChampRequis);
            }
            else if (personne.Matricule7.Length != 7 || !personne.Matricule.All(char.IsDigit))
            {
                ModelState.AddModelError("Matricule7", Messages.LongueurDeSeptCaracteres);
            }
            else if (_dataRepository.AnyPersonne(x => x.Matricule == personne.Matricule))
            {
                ModelState.AddModelError(string.Empty, Messages.EtudiantAjouterErreurMatriculeExisteDeja(personne.Matricule));
            }
        }

        private bool ConfirmeMdp(string s1, string s2)
        {
            if (s1 == s2) return true;
            ModelState.AddModelError("ConfirmPassword", Messages.MotsDePasseDoiventEtreIdentiques);
            TempData["Echec"] = Messages.MotsDePasseDoiventEtreIdentiques;
            return false;
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
