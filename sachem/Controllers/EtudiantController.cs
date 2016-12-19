using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using sachem.Methodes_Communes;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    public class EtudiantController : Controller
    {
        public const string AnneePremiersCaracteres = "20";
        private readonly IDataRepository _dataRepository;
        protected int NoPage = 1;

        public EtudiantController()
        {
            _dataRepository = new BdRepository();
        }

        public EtudiantController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [AcceptVerbs("Get", "Post")]
        public JsonResult ActualiseGroupeddl(int cours, int session)
        {
            var a = _dataRepository
                .WhereGroupe(g => (g.id_Sess == session || session == 0) && (g.id_Cours == cours || cours == 0))
                .OrderBy(g => g.NoGroupe);
            return Json(a.ToList(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("Get", "Post")]
        public virtual JsonResult ActualiseCoursddl(int session = 0)
        {
            var a = _dataRepository.WhereCours(c => c.Groupe.Any(g => g.id_Sess == session || session == 0))
                .OrderBy(c => c.Nom)
                .Select(c => new { c.id_Cours, c.CodeNom });
            return Json(a.ToList(), JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        protected IEnumerable<PersonneProgEtu> Rechercher()
        {
            var matricule = "";
            var session = 0;
            var cours = 0;
            var groupe = 0;
            IEnumerable<PersonneProgEtu> lstEtu = new List<PersonneProgEtu>();

            if (Request.RequestType == "GET" && Session["DernRechEtu"] != null && (string)Session["DernRechEtuUrl"] == Request.Url?.LocalPath)
            {
                var tanciennerech = Session["DernRechEtu"].ToString().Split(';');

                if (tanciennerech[0].Length != 0)
                {
                    matricule = tanciennerech[0];
                    ViewBag.Matricule = matricule;
                }
                else
                {
                    if (tanciennerech[1].Length != 0)
                    {
                        session = int.Parse(tanciennerech[1]);
                        ViewBag.Session = session;
                    }
                    if (tanciennerech[2].Length != 0)
                    {
                        cours = int.Parse(tanciennerech[2]);
                        ViewBag.Cours = cours;
                    }
                    if (tanciennerech[3].Length != 0)
                    {
                        groupe = int.Parse(tanciennerech[3]);
                        ViewBag.Groupe = groupe;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.Form["Matricule"]))
                {
                    matricule = Request.Form["Matricule"];
                    ViewBag.Matricule = matricule;
                }
                else if (!string.IsNullOrEmpty(Request.Params["Matricule"]))
                {
                    matricule = Request.Params["Matricule"];
                    ViewBag.Matricule = matricule;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request.Form["SelectCours"]))
                    {
                        cours = Convert.ToInt32(Request.Form["SelectCours"]);
                        ViewBag.Cours = cours;
                    }
                    else if (!string.IsNullOrEmpty(Request.Params["Cours"]))
                    {
                        cours = Convert.ToInt32(Request.Params["Cours"]);
                        ViewBag.Cours = cours;
                    }
                    if (!string.IsNullOrEmpty(Request.Form["SelectGroupe"]))
                    {
                        groupe = Convert.ToInt32(Request.Form["SelectGroupe"]);
                        ViewBag.Groupe = groupe;
                    }
                    else if (!string.IsNullOrEmpty(Request.Params["Groupe"]))
                    {
                        groupe = Convert.ToInt32(Request.Params["Groupe"]);
                        ViewBag.Groupe = groupe;
                    }
                    if (!string.IsNullOrEmpty(Request.Form["SelectSession"]))
                    {
                        int.TryParse(Request.Form["SelectSession"], out session);
                    }
                    else if (Request.Form["SelectSession"] == null)
                        session = _dataRepository.SessionEnCours();
                }
            }
            ViewBag.SelectSession = _dataRepository.ListeSession(session);
            ViewBag.SelectCours = new SelectList(_dataRepository
                .WhereCours(c => c.Groupe.Any(g => g.id_Sess == session || session == 0))
                .OrderBy(c => c.Nom), "id_Cours", "CodeNom", cours);
            ViewBag.SelectGroupe = new SelectList(_dataRepository
                .WhereGroupe(g => (g.id_Sess == session || session == 0) && (g.id_Cours == cours || cours == 0))
                .OrderBy(g => g.NoGroupe), "id_Groupe", "NoGroupe", groupe);

            Session["DernRechEtu"] = matricule + ";" + session + ";" + cours + ";" + groupe + ";" + NoPage;
            if (Request.Url != null) Session["DernRechEtuUrl"] = Request.Url.LocalPath;

            if (!ModelState.IsValid) return lstEtu;
            _dataRepository.BeLazy(false);
            if (matricule == "")
            {
                lstEtu = _dataRepository.WherePersonne(x => x.Actif
                        && x.GroupeEtudiant.Any(y => y.id_Groupe == groupe || groupe == 0)
                        && x.GroupeEtudiant.Any(z => z.Groupe.id_Cours == cours || cours == 0)
                        && x.EtuProgEtude.Any(y => y.id_Sess == session || session == 0))
                        .OrderBy(x => x.Nom)
                        .Select(p => new
                        {
                            Personne = p,
                            ProgEtu =
                            _dataRepository.WhereEtuProgEtude(pe => p.id_Pers == pe.id_Etu).OrderByDescending(pe => pe.id_Sess)
                                .First().ProgrammeEtude
                        }).AsEnumerable()
                        .OrderBy(q => q.Personne.Nom)
                        .ThenBy(q => q.Personne.Prenom)
                        .Select(q => new PersonneProgEtu { personne = q.Personne, progEtuActif = q.ProgEtu });
            }
            else
            {
                lstEtu = _dataRepository.WherePersonne(x => x.Matricule.Substring(2).StartsWith(matricule))
                        .OrderBy(x => x.Nom)
                        .ThenBy(x => x.Nom)
                        .Select(p => new
                        {
                            Personne = p,
                            ProgEtu =
                            _dataRepository.WhereEtuProgEtude(pe => p.id_Pers == pe.id_Etu).OrderByDescending(pe => pe.id_Sess)
                                .First().ProgrammeEtude
                        }).AsEnumerable()
                        .OrderBy(q => q.Personne.Nom)
                        .ThenBy(q => q.Personne.Prenom)
                        .Select(q => new PersonneProgEtu { personne = q.Personne, progEtuActif = q.ProgEtu });
            }
            _dataRepository.BeLazy();
            return lstEtu;
        }

        protected IEnumerable<PersonneProgEtu> Rechercher(int? page)
        {
            return Rechercher();
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
            ViewBag.id_Sexe = _dataRepository.ListeSexe();
            ViewBag.id_TypeUsag = _dataRepository.ListeTypeUsager();
            ViewBag.id_ProgEtu = _dataRepository.ListeProgrammmeEtude();
            ViewBag.id_Session = _dataRepository.ListeSession();
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Create([Bind(Include = "id_Pers, id_Sexe, id_TypeUsag, Nom, Prenom, NomUsager, Matricule7, MP, ConfirmPassword, Courriel, Telephone, DateNais, Actif")] Personne personne, int? page)
        {
            var etuProg = new PersonneEtuProgParent();

            personne.id_TypeUsag = 1;
            personne.Actif = true;
            personne.Telephone = SachemIdentite.FormatTelephone(personne.Telephone);
            personne.Matricule = AnneePremiersCaracteres + personne.Matricule7;
            etuProg.personne = personne;

            Valider(etuProg.personne);

            ViewBag.id_Sexe = _dataRepository.ListeSexe();
            ViewBag.id_TypeUsag = _dataRepository.ListeTypeUsager();
            ViewBag.id_ProgEtu = _dataRepository.ListeProgrammmeEtude();
            ViewBag.id_Session = _dataRepository.ListeSession();

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

            if (!ModelState.IsValid) return View(etuProg);
            etuProg.personne.MP = SachemIdentite.EncrypterChaine(etuProg.personne.MP);
            etuProg.personne.ConfirmPassword = SachemIdentite.EncrypterChaine(etuProg.personne.ConfirmPassword);
            _dataRepository.AddPersonne(etuProg.personne);
            personne.Telephone = SachemIdentite.RemettreTel(personne.Telephone);
            var etuprog = new EtuProgEtude();
            if (Request.Form["id_ProgEtu"] != "" && Request.Form["id_Session"] != "")
            {
                etuprog.id_ProgEtu = int.Parse(Request.Form["id_ProgEtu"]);
                etuprog.id_Sess = int.Parse(Request.Form["id_Session"]);
                etuprog.id_Etu = personne.id_Pers;
                try
                {
                    _dataRepository.AddEtuProgEtude(etuprog);
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                
            }
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

            ViewBag.id_Sexe = _dataRepository.ListeSexe(personne.id_Sexe);
            ViewBag.id_TypeUsag = _dataRepository.ListeTypeUsager(personne.id_TypeUsag);
            ViewBag.id_ProgEtu = _dataRepository.ListeProgrammmeEtude();
            ViewBag.id_Session = _dataRepository.ListeSession();
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
        public ActionResult Edit([Bind(Include = "id_Pers, id_Sexe, id_TypeUsag, Nom, Prenom, NomUsager, Matricule7, MP, ConfirmPassword, Courriel, Telephone, DateNais, Actif")] Personne personne, int? page)
        {
            Valider(personne);
            var etuProg = new PersonneEtuProgParent();
            personne.Telephone = SachemIdentite.FormatTelephone(personne.Telephone);
            etuProg.personne = personne;
            const string message = "Le mot de passe doit contenir 6 caratères";

            var etuprog = new EtuProgEtude();

            etuProg.EtuProgEtu = _dataRepository.WhereEtuProgEtude(d => d.id_Etu == etuProg.personne.id_Pers).OrderBy(d => d.ProgrammeEtude.Code).ToList();
            if (Request.Form["id_Programme"] != "" && Request.Form["id_Session"] != "" && ConfirmeMdp(personne.MP, personne.ConfirmPassword))
            {
                etuprog.id_ProgEtu = int.Parse(Request.Form["id_ProgEtu"]);
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
                        personne.MP =
                            _dataRepository.WherePersonne(x => x.id_Pers == personne.id_Pers, true)
                                .Select(x => x.MP)
                                .FirstOrDefault();
                        personne.ConfirmPassword = personne.MP;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                _dataRepository.EditPersonne(personne);
                foreach (var prog in etuProg.EtuProgEtu)
                {
                    _dataRepository.EditEtuProgEtude(prog);
                }
                TempData["Success"] = Messages.EtudiantModifie(personne.NomPrenom);
                return RedirectToAction("Index");
            }

            ViewBag.id_Sexe = _dataRepository.ListeSexe(personne.id_Sexe);
            ViewBag.id_TypeUsag = _dataRepository.ListeTypeUsager(etuProg.personne.id_TypeUsag);
            ViewBag.id_ProgEtu = _dataRepository.ListeProgrammmeEtude();
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

        private void Valider([Bind(Include = "id_Pers, id_Sexe, id_TypeUsag, Nom, Prenom, NomUsager, Matricule7, MP, Courriel, Telephone, DateNais, Actif")] Personne personne)
        {
            if (personne.Matricule7 == null)
            {
                ModelState.AddModelError("Matricule7", Messages.ChampRequis);
            }
            else if (personne.Matricule7.Length != 7 || !personne.Matricule7.All(char.IsDigit))
            {
                ModelState.AddModelError("Matricule7", Messages.LongueurDeSeptCaracteres);
            }
            else if (_dataRepository.AnyPersonne(x => x.Matricule == personne.Matricule && x.id_Pers != personne.id_Pers))
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
