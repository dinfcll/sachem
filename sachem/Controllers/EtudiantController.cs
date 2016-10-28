using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using static sachem.Classes_Sachem.ValidationAcces;
using System.Data.Entity;
using System.Collections.Generic;

namespace sachem.Controllers
{
    public class EtudiantController : RechercheEtudiantController
    {    
        private SACHEMEntities db = new SACHEMEntities();

        public const string constanteAnnee = "20";
        [ValidationAccesEnseignant]
        public ActionResult Index(int? page)
        {
            noPage = (page ?? noPage);
           
            var personne = from c in db.Personne   
                           where c.Actif == true && c.id_TypeUsag == 1
                           select c;

            return View(Rechercher().ToPagedList(noPage, 20));
        }

      
        // GET: Etudiant/Details/5
        //fonction pour formatter le numéro de téléphone avant de mettre dans la bd

        [ValidationAccesEnseignant]
        // GET: Etudiant/Create
        public ActionResult Create()
        {
            //   s.lSexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");

            ViewBag.id_Sexe = db.p_Sexe;
            ViewBag.Selected = 0;
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag");
            ViewBag.id_Programme = new SelectList(db.ProgrammeEtude, "id_ProgEtu", "nomProg");
            ViewBag.id_Session = new SelectList(db.Session, "id_Sess", "NomSession");

            //return View();
            return View();
        }

        // POST: Etudiant/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationAccesEnseignant]
        public ActionResult Create([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,Matricule,MP,ConfirmPassword,Courriel,Telephone,DateNais")] Personne personne,int? page)
        {

            PersonneEtuProgParent pepp = new PersonneEtuProgParent();
            //personne.id_Sexe = Int32.Parse(Request.Form["id_Sexe"]);

            personne.id_TypeUsag = 1;
            personne.Actif = true;
            personne.Telephone = SachemIdentite.FormatTelephone(personne.Telephone);
            personne.Matricule = constanteAnnee + personne.Matricule;
            pepp.personne = personne;
            

            var etuprog = new EtuProgEtude();


            if (Request.Form["id_Programme"] != "" && Request.Form["id_Session"] != "")
            {
                etuprog.id_ProgEtu = Int32.Parse(Request.Form["id_Programme"]);
                etuprog.id_Sess = Int32.Parse(Request.Form["id_Session"]);
                var idEtu = db.Personne.AsNoTracking().OrderByDescending(p => p.id_Pers).FirstOrDefault();
                etuprog.id_Etu = idEtu.id_Pers;
            }

            Valider(pepp.personne);
            // Si les données sont valides, faire l'ajout
            if (ModelState.IsValid)
            {
                pepp.personne.MP = SachemIdentite.encrypterChaine(pepp.personne.MP); // Encryption du mot de passe
                pepp.personne.ConfirmPassword = SachemIdentite.encrypterChaine(pepp.personne.ConfirmPassword); // Encryption du mot de passe   
                db.Personne.Add(pepp.personne);
                db.SaveChanges();
                db.EtuProgEtude.Add(etuprog);
                db.SaveChanges();
                personne.Telephone = SachemIdentite.RemettreTel(personne.Telephone);
                TempData["Success"] = Messages.I_010(personne.Matricule7); // Message afficher sur la page d'index confirmant la création
                return RedirectToAction("Index");
            }
            ViewBag.id_Sexe = db.p_Sexe;
            ViewBag.Selected = 0;
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag");
            ViewBag.id_Programme = new SelectList(db.ProgrammeEtude, "id_ProgEtu", "nomProg");
            ViewBag.id_Session = new SelectList(db.Session, "id_Sess", "NomSession");


            return View(pepp);
        }

        // GET: Etudiant/Edit/5
        [ValidationAccesEnseignant]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            //retroune la liste de programme qui relié à l'élève
            var Prog = from d in db.EtuProgEtude
                       where d.id_Etu == personne.id_Pers
                       orderby d.ProgrammeEtude.Code
                       select d;

            ViewBag.id_Sexe = db.p_Sexe;
            ViewBag.Selected = personne.id_Sexe;
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            ViewBag.id_Programme = new SelectList(db.ProgrammeEtude, "id_ProgEtu", "nomProg");
            ViewBag.id_Session = new SelectList(db.Session, "id_Sess", "NomSession");
            PersonneEtuProgParent epep = new PersonneEtuProgParent();
            epep.personne = personne;
            epep.epe = Prog.ToList();
            return View(epep);
        }

        // POST: Etudiant/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationAccesEnseignant]
        //Modification lorsqu'on clique sur le bouton modification / Enregistrement
        public ActionResult Edit([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule7,MP,Courriel,Telephone,DateNais,Actif")] Personne personne)
        {
            PersonneEtuProgParent pepp = new PersonneEtuProgParent();
            personne.id_TypeUsag = 1;
            pepp.personne = personne;

            var etuprog = new EtuProgEtude();
            //Aller chercher Programme d'étude(nom)
            var Prog = from d in db.EtuProgEtude
                       where d.id_Etu == pepp.personne.id_Pers
                       orderby d.ProgrammeEtude.Code
                       select d;
            pepp.epe = Prog.ToList();

            //Ajout du programme d'étude (Si l'étudiant rajoute les champs)
            if (Request.Form["id_Programme"] != "" && Request.Form["id_Session"] != "")
            {
                etuprog.id_ProgEtu = Int32.Parse(Request.Form["id_Programme"]);
                etuprog.id_Sess = Int32.Parse(Request.Form["id_Session"]);
                etuprog.id_Etu = personne.id_Pers;
                db.EtuProgEtude.Add(etuprog);
                db.SaveChanges();
            }
            if (ModelState.IsValid)
            {
                db.Entry(pepp.personne).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = Messages.I_045(personne.NomPrenom);
                //redirection à l'index après la suppression
                return RedirectToAction("Index");
            }
            //Mise à jour Viewbag

            ViewBag.id_Sexe = db.p_Sexe;
            ViewBag.Selected = pepp.personne.id_Sexe;
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", pepp.personne.id_TypeUsag);
            ViewBag.id_Programme = new SelectList(db.ProgrammeEtude, "id_ProgEtu", "nomProg");
            ViewBag.id_Session = new SelectList(db.Session, "id_Sess", "NomSession");


            return View(pepp);
        }

        // GET: Etudiant/Delete/5
        //exécuté lorsqu'un étudiant est supprimé
        [ValidationAccesEnseignant]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            
            return View(personne);
        }

        // POST: Etudiant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ValidationAccesEnseignant]
        public ActionResult DeleteConfirmed(int id,int? page)
        {
            var pageNumber = page ?? 1;           
            Personne personne = db.Personne.Find(id);

            var etuProgEtu = db.EtuProgEtude.Where(x => x.id_Etu == personne.id_Pers);
            db.EtuProgEtude.RemoveRange(etuProgEtu);
            var groupeEtu = db.GroupeEtudiant.Where(y => y.id_Etudiant == personne.id_Pers);
            db.GroupeEtudiant.RemoveRange(groupeEtu);
            var Jumul = db.Jumelage.Where(z => z.id_InscEleve == personne.id_Pers);
            db.Jumelage.RemoveRange(Jumul);
            var Inscri = db.Inscription.Where(a => a.id_Pers == personne.id_Pers);
            db.Inscription.RemoveRange(Inscri);
            var CoursSuiv = db.CoursSuivi.Where(b => b.id_Pers == personne.id_Pers);
            db.CoursSuivi.RemoveRange(CoursSuiv);

            //suppresion et sauvegarde dans la bd
            db.Personne.Remove(personne);
            db.SaveChanges();
            TempData["Success"] = Messages.I_028(personne.NomPrenom);
            //redirection à l'index après la suppression
            return RedirectToAction("Index");
        }
        //fonction qui supprime un programme d'étude à oartir de la page modifier
        public ActionResult deleteProgEtu(int idProg, int idPers, int Valider = 0)
        {
            Personne personne = db.Personne.Find(idPers);
            EtuProgEtude etuprog = db.EtuProgEtude.Find(idProg);
            var Prog = from d in db.EtuProgEtude
                       where d.id_Etu == personne.id_Pers
                       orderby d.ProgrammeEtude.Code
                       select d;
            
            TempData["Question"] = Messages.Q_002(etuprog.ProgrammeEtude.CodeNomProgramme);
            var etuProgEtu = db.EtuProgEtude.Where(x => x.id_EtuProgEtude == idProg);
            if (Valider != 0)
            {
                TempData["Question"] = null;
            }
            if (Valider == 1)
            {
                if (!db.CoursSuivi.Any(c => c.id_Pers == etuprog.id_Etu && c.id_Sess == etuprog.id_Sess))
                {
                    TempData["Success"] = Messages.I_016(etuprog.ProgrammeEtude.CodeNomProgramme);
                    db.EtuProgEtude.RemoveRange(etuProgEtu);
                    db.SaveChanges();
                }
                else
                {
                    if (Prog.Count() > 1)
                    {
                        TempData["Success"] = Messages.I_016(etuprog.ProgrammeEtude.CodeNomProgramme);
                        db.EtuProgEtude.RemoveRange(etuProgEtu);
                        db.SaveChanges();
                    }
                    else
                        TempData["Echec"] = Messages.I_011(etuprog.ProgrammeEtude.CodeNomProgramme);
                }
                //faire apparaitre le message
                return RedirectToAction("Edit", "Etudiant", new { id = idPers });
            }
            TempData["id_Pers"] = idPers;         
            TempData["id_Prog"] = idProg;
            return RedirectToAction("Edit", "Etudiant", new { id = idPers });
         }

        //fonction de validation
        private void Valider([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,MP,ConfirmPassword,Courriel,DateNais,Actif")] Personne personne)
        {
            
            if (personne.Matricule7 == null)
                ModelState.AddModelError("Matricule7", Messages.U_001); //requis
            else if (personne.Matricule7.Length != 7 || !personne.Matricule.All(char.IsDigit)) //vérifie le matricule
                ModelState.AddModelError("Matricule7", Messages.U_004); //longueur
            else if (db.Personne.Any(x => x.Matricule == personne.Matricule))// Verifier si le matricule existe déja dans la BD
                ModelState.AddModelError(string.Empty, Messages.I_004(personne.Matricule));
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
