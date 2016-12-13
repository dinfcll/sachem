using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using sachem.Classes_Sachem;
using System.Data.Entity;
using System.Collections.Generic;

namespace sachem.Controllers
{
    public class EtudiantController : RechercheEtudiantController
    {
        public const string AnneePremiersCaracteres = "20";

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Index(int? page)
        {
            NoPage = page ?? NoPage;

            return View(Rechercher().ToPagedList(NoPage, 20));
        }

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Create()
        {
            ViewBag.id_Sexe = Db.p_Sexe;
            ViewBag.Selected = 0;
            ViewBag.id_TypeUsag = new SelectList(Db.p_TypeUsag, "id_TypeUsag", "TypeUsag");
            ViewBag.id_Programme = new SelectList(Db.ProgrammeEtude.Where(x => x.Actif), "id_ProgEtu", "CodeNomProgramme");
            ViewBag.id_Session = new SelectList(Db.Session, "id_Sess", "NomSession");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Create([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,Matricule,MP,ConfirmPassword,Courriel,Telephone,DateNais")] Personne personne,int? page)
        {
            var etuProg = new PersonneEtuProgParent();

            personne.id_TypeUsag = 1;
            personne.Actif = true;
            personne.Telephone = SachemIdentite.FormatTelephone(personne.Telephone);
            personne.Matricule = AnneePremiersCaracteres + personne.Matricule;
            etuProg.personne = personne;

                ViewBag.id_Sexe = Db.p_Sexe;
                ViewBag.Selected = 0;
                ViewBag.id_TypeUsag = new SelectList(Db.p_TypeUsag, "id_TypeUsag", "TypeUsag");
                ViewBag.id_Programme = new SelectList(Db.ProgrammeEtude.Where(x => x.Actif), "id_ProgEtu", "CodeNomProgramme");
                ViewBag.id_Session = new SelectList(Db.Session, "id_Sess", "NomSession");

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
                Db.EtuProgEtude.Add(etuprog);
            }

            if (ModelState.IsValid)
            {
                etuProg.personne.MP = SachemIdentite.encrypterChaine(etuProg.personne.MP); 
                etuProg.personne.ConfirmPassword = SachemIdentite.encrypterChaine(etuProg.personne.ConfirmPassword);
                Db.Personne.Add(etuProg.personne);
                Db.SaveChanges();
                personne.Telephone = SachemIdentite.RemettreTel(personne.Telephone);

                TempData["Success"] = Messages.EtudiantEnregistre(personne.Matricule7);
                
                return RedirectToAction("Index");
            }
             return View(etuProg);
        }
        // GET: Etudiant/Edit/5
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = Db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            
            if (personne.Telephone != null)
            {
                personne.Telephone = SachemIdentite.RemettreTel(personne.Telephone);
            }

            var programmes = from d in Db.EtuProgEtude
                       where d.id_Etu == personne.id_Pers
                       orderby d.ProgrammeEtude.Code
                       select d;

            ViewBag.id_Sexe = Db.p_Sexe;
            ViewBag.Selected = personne.id_Sexe;
            ViewBag.id_TypeUsag = new SelectList(Db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            ViewBag.id_Programme = new SelectList(Db.ProgrammeEtude.Where(x=> x.Actif), "id_ProgEtu", "CodeNomProgramme");
            ViewBag.id_Session = new SelectList(Db.Session, "id_Sess", "NomSession");
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
            var personne = Db.Personne.Find(idPers);
            var etuprog = Db.EtuProgEtude.Find(idProg);
            var programme = from d in Db.EtuProgEtude
                       where d.id_Etu == personne.id_Pers
                       orderby d.ProgrammeEtude.Code
                       select d;

            var etuProgEtu = Db.EtuProgEtude.Where(x => x.id_EtuProgEtude == idProg);
            if (!Db.CoursSuivi.Any(c => c.id_Pers == etuprog.id_Etu && c.id_Sess == etuprog.id_Sess))
            {
                TempData["Success"] = Messages.ProgrammeRetireDelaListeEtudiant(etuprog.ProgrammeEtude.CodeNomProgramme);
                Db.EtuProgEtude.RemoveRange(etuProgEtu);
                Db.SaveChanges();
            }
            else
            {
                if (programme.Count() > 1)
                {
                    TempData["Success"] = Messages.ProgrammeRetireDelaListeEtudiant(etuprog.ProgrammeEtude.CodeNomProgramme);
                    Db.EtuProgEtude.RemoveRange(etuProgEtu);
                    Db.SaveChanges();
                }
                else
                {
                    TempData["Echec"] = Messages.ImpossibleDeRetirerProgrammeDUnEtudiant(etuprog.ProgrammeEtude.CodeNomProgramme);
                }
            }
            var prog = ObtenirProgEtu(idPers);
            return Json(prog.ToList(), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<object> ObtenirProgEtu(int idPers)
        {
            var ens = Db.EtuProgEtude
                .AsNoTracking()
                .Where(sel => sel.id_Etu == idPers)
                .Select(e => new { e.ProgrammeEtude.NomProg, e.id_Etu, e.id_EtuProgEtude, e.ProgrammeEtude.Code })
                .Distinct();
            
            return ens.AsEnumerable();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Edit([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule7,MP,ConfirmPassword,Courriel,Telephone,DateNais,Actif")] Personne personne)
        {
            var etuProg = new PersonneEtuProgParent();
            personne.id_TypeUsag = 1;
            personne.Telephone = SachemIdentite.FormatTelephone(personne.Telephone);
            etuProg.personne = personne;
            const string message = "Le mot de passe doit contenir 6 caratères";

            var etuprog = new EtuProgEtude();

            var prog = from d in Db.EtuProgEtude
                       where d.id_Etu == etuProg.personne.id_Pers
                       orderby d.ProgrammeEtude.Code
                       select d;
            etuProg.EtuProgEtu = prog.ToList();
            if (Request.Form["id_Programme"] != "" && Request.Form["id_Session"] != ""&& ConfirmeMdp(personne.MP, personne.ConfirmPassword))
            {
                etuprog.id_ProgEtu = int.Parse(Request.Form["id_Programme"]);
                etuprog.id_Sess = int.Parse(Request.Form["id_Session"]);
                etuprog.id_Etu = personne.id_Pers;
                Db.EtuProgEtude.Add(etuprog);
                Db.SaveChanges();
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
                        etuProg.personne.MP = SachemIdentite.encrypterChaine(etuProg.personne.MP);
                        etuProg.personne.ConfirmPassword = SachemIdentite.encrypterChaine(etuProg.personne.ConfirmPassword); 
                    }
                    else
                    {
                        var mdp = from c in Db.Personne
                                  where (c.id_Pers == personne.id_Pers)
                                  select c.MP;
                        personne.MP = mdp.SingleOrDefault();
                        personne.ConfirmPassword = personne.MP;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                Db.Entry(etuProg.personne).State = EntityState.Modified;
                Db.SaveChanges();
                TempData["Success"] = Messages.EtudiantModifie(personne.NomPrenom);
                return RedirectToAction("Index");
            }

            ViewBag.id_Sexe = Db.p_Sexe;
            ViewBag.Selected = etuProg.personne.id_Sexe;
            ViewBag.id_TypeUsag = new SelectList(Db.p_TypeUsag, "id_TypeUsag", "TypeUsag", etuProg.personne.id_TypeUsag);
            ViewBag.id_Programme = new SelectList(Db.ProgrammeEtude.Where(x => x.Actif), "id_ProgEtu", "CodeNomProgramme");
            ViewBag.id_Session = new SelectList(Db.Session, "id_Sess", "NomSession");
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
            Personne personne = Db.Personne.Find(id);
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
            var personne = Db.Personne.Find(id);
            var inscription = Db.Inscription.FirstOrDefault(x => x.id_Pers == personne.id_Pers);

            if (Db.GroupeEtudiant.Any(x => x.id_Etudiant == personne.id_Pers))
            {
                ModelState.AddModelError(string.Empty, Messages.EtudiantNePeutEtreSupprimeCarLieAUnGroupe());
                TempData["Echec"] = Messages.EtudiantNePeutEtreSupprimeCarLieAUnGroupe();
            }
             
            if (inscription != null)
            {
                if (Db.Jumelage.Any(x => x.id_InscEleve == inscription.id_Inscription))
                {
                    ModelState.AddModelError(string.Empty, Messages.EtudiantNonSupprimeCarJumele());
                    TempData["Echec"] = Messages.EtudiantNonSupprimeCarJumele();
                }
            }
            if (ModelState.IsValid)
            {
                var etuProgEtu = Db.EtuProgEtude.Where(x => x.id_Etu == personne.id_Pers);
                Db.EtuProgEtude.RemoveRange(etuProgEtu);
                var groupeEtu = Db.GroupeEtudiant.Where(y => y.id_Etudiant == personne.id_Pers);
                Db.GroupeEtudiant.RemoveRange(groupeEtu);
                var jumul = Db.Jumelage.Where(z => z.id_InscEleve == personne.id_Pers);
                Db.Jumelage.RemoveRange(jumul);
                var inscri = Db.Inscription.Where(a => a.id_Pers == personne.id_Pers);
                Db.Inscription.RemoveRange(inscri);
                var coursSuiv = Db.CoursSuivi.Where(b => b.id_Pers == personne.id_Pers);
                Db.CoursSuivi.RemoveRange(coursSuiv);
                Db.Personne.Remove(personne);
                Db.SaveChanges();
                TempData["Success"] = Messages.EtudiantSupprime(personne.NomPrenom);
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProgEtu(int idProg, int idPers, int valider = 0)
        {
            var personne = Db.Personne.Find(idPers);
            var etuprog = Db.EtuProgEtude.Find(idProg);
            var prog = from d in Db.EtuProgEtude
                       where d.id_Etu == personne.id_Pers
                       orderby d.ProgrammeEtude.Code
                       select d;
            
            TempData["Question"] = Messages.VraimentSupprimerProgrammeEtude(etuprog.ProgrammeEtude.CodeNomProgramme);
            var etuProgEtu = Db.EtuProgEtude.Where(x => x.id_EtuProgEtude == idProg);
            if (valider != 0)
            {
                TempData["Question"] = null;
            }
            if (valider == 1)
            {
                if (!Db.CoursSuivi.Any(c => c.id_Pers == etuprog.id_Etu && c.id_Sess == etuprog.id_Sess))
                {
                    TempData["Success"] = Messages.ProgrammeRetireDelaListeEtudiant(etuprog.ProgrammeEtude.CodeNomProgramme);
                    Db.EtuProgEtude.RemoveRange(etuProgEtu);
                    Db.SaveChanges();
                }
                else
                {
                    if (prog.Count() > 1)
                    {
                        TempData["Success"] = Messages.ProgrammeRetireDelaListeEtudiant(etuprog.ProgrammeEtude.CodeNomProgramme);
                        Db.EtuProgEtude.RemoveRange(etuProgEtu);
                        Db.SaveChanges();
                    }
                    else
                    {
                        TempData["Echec"] = Messages.ImpossibleDeRetirerProgrammeDUnEtudiant(etuprog.ProgrammeEtude.CodeNomProgramme);
                    }   
                }
                return RedirectToAction("Edit", "Etudiant", new { id = idPers });
            }
            TempData["id_Pers"] = idPers;         
            TempData["id_Prog"] = idProg;
            return RedirectToAction("Edit", "Etudiant", new { id = idPers });
         }

        private void Valider([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,MP,ConfirmPassword,Courriel,DateNais,Actif")] Personne personne)
        {
            if (personne.Matricule7 == null)
            {
                ModelState.AddModelError("Matricule7", Messages.ChampRequis);
            }
            else if (personne.Matricule7.Length != 7 || !personne.Matricule.All(char.IsDigit))
            {
                ModelState.AddModelError("Matricule7", Messages.LongueurDeSeptCaracteres);
            }
            else if (Db.Personne.Any(x => x.Matricule == personne.Matricule))
            {
                ModelState.AddModelError(string.Empty, Messages.MatriculeDejaExistant(personne.Matricule));
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool ConfirmeMdp(string s1, string s2)
        {
            if (s1 != s2)
            {
                ModelState.AddModelError("ConfirmPassword", Messages.MotsDePasseDoiventEtreIdentiques());
                TempData["Echec"] = Messages.MotsDePasseDoiventEtreIdentiques();
                return false;
            }
            return true;
        }
    }
}
