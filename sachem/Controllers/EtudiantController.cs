using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using System.Security.Cryptography;// pour encripter mdp
using System.Text;
using System.Data.Entity;

namespace sachem.Controllers
{
    public class EtudiantController : RechercheEtudiantController
    {    
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
        public static string FormatTelephone(string s)
        {
            var charsToRemove = new string[] { ".", "-", "(", " ", ")" };
            foreach (var c in charsToRemove)
            {
                s = s.Replace(c, string.Empty);
            }
            return s;
        }
        //fonction qui remet le numéro de téléphone dans le bon format
        public static string RemettreTel(string a)

        {
            string modif;
            modif = a.Insert(0, "(");
            modif = modif.Insert(4, ")");
            modif = modif.Insert(5, " ");
            modif = modif.Insert(9,"-");
            return modif;
        }
        // GET: Etudiant/Create
        public ActionResult Create()
        {

            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag");
            return View();
        }

        // POST: Etudiant/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,Matricule,MP,ConfirmPassword,Courriel,Telephone,DateNais")] Personne personne,int? page)
        {
            personne.id_TypeUsag = 1;
            personne.Actif = true;
            personne.Telephone = FormatTelephone(personne.Telephone);

            Valider(personne);
            // Si les données sont valides, faire l'ajout
            if (ModelState.IsValid)
            {
                personne.MP = encrypterChaine(personne.MP); // Encryption du mot de passe
                personne.ConfirmPassword = encrypterChaine(personne.ConfirmPassword); // Encryption du mot de passe 
                
                db.Personne.Add(personne);
                db.SaveChanges();
                personne.Telephone = RemettreTel(personne.Telephone);
                TempData["Success"] = Messages.I_010(personne.Matricule); // Message afficher sur la page d'index confirmant la création
                return RedirectToAction("Index");
            }

            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // GET: Etudiant/Edit/5
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
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            ViewBag.id_Programme = new SelectList(db.ProgrammeEtude, "id_ProgEtu", "nomProg");
            ViewBag.id_Session = new SelectList(db.Session, "id_Sess", "NomSession");
            PersonneEtuProgParent epep = new PersonneEtuProgParent();
            epep.personne = personne;
            epep.epe = Prog.ToList();
            return View(epep);
        }

        public void FillDropDownlist()
        {  
        }
        // POST: Etudiant/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Modification lorsqu'on clique sur le bouton modification / Enregistrement
        public ActionResult Edit([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,Matricule7,MP,Courriel,Telephone,DateNais,Actif")] Personne personne)
        {
            PersonneEtuProgParent pepp = new PersonneEtuProgParent();
            Personne p = db.Personne.Find(personne.id_Pers);
            p.id_TypeUsag = 1;
            var idSexe = (from d in db.Personne
                          where d.id_Pers == p.id_Pers
                          select d).FirstOrDefault();
            p.id_Sexe = idSexe.id_Sexe;
            pepp.personne = p;


            //Mise à jour Viewbag
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", pepp.personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", pepp.personne.id_TypeUsag);
            ViewBag.id_Programme = new SelectList(db.ProgrammeEtude, "id_ProgEtu", "nomProg");
            ViewBag.id_Session = new SelectList(db.Session, "id_Sess", "NomSession");

            var etuprog = new EtuProgEtude();
            //Ajout du programme d'étude (Si l'étudiant rajoute les champs)
            if (Request.Form["id_Programme"] != "" && Request.Form["id_Session"] != "")
            {
                etuprog.id_ProgEtu = Int32.Parse(Request.Form["id_Programme"]);
                etuprog.id_Sess = Int32.Parse(Request.Form["id_Session"]);
                etuprog.id_Etu = personne.id_Pers;
                db.EtuProgEtude.Add(etuprog);
                db.SaveChanges();
            }
            //Aller chercher Programme d'étude(nom)
            var Prog = from d in db.EtuProgEtude
                       where d.id_Etu == pepp.personne.id_Pers
                       orderby d.ProgrammeEtude.Code
                       select d;
            pepp.epe = Prog.ToList();

            if (ModelState.IsValid)
            {
                db.Entry(pepp.personne).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View(pepp);
        }

        // GET: Etudiant/Delete/5
        //exécuté lorsqu'un étudiant est supprimé
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
        public ActionResult DeleteConfirmed(int id,int? page)
        {
            var pageNumber = page ?? 1;
            // Verifie si l'étudiant est relié a un groupe
            /*if (db.Groupe.Any(a => a.Personne == id)) 
            {
                ModelState.AddModelError(string.Empty, Messages.I_014);

            }*/
           
            //if (ModelState.IsValid)
            //{
                //trouve la personne à supprimer
                Personne personne = db.Personne.Find(id);
            //suppression de la personne dans tout les tables qu'on la retrouve
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
        public ActionResult deleteProgEtu(int id, int id2, int Valider = 0)
        {
            Personne personne = db.Personne.Find(id);
            var Prog = from d in db.EtuProgEtude
                       where d.id_Etu == personne.id_Pers
                       orderby d.ProgrammeEtude.Code
                       select d;
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag, "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            ViewBag.id_Programme = new SelectList(db.ProgrammeEtude, "id_ProgEtu", "nomProg");
            ViewBag.id_Session = new SelectList(db.Session, "id_Sess", "NomSession");
            PersonneEtuProgParent epep = new PersonneEtuProgParent();
            epep.personne = personne;
            epep.epe = Prog.ToList();
            TempData["Question"] = Messages.Q_002("");
            var etuProgEtu = db.EtuProgEtude.Where(x => x.id_EtuProgEtude == id);
            if (Valider != 0)
            {
                TempData["Question"] = null;
            }
            if (Valider == 1)
            {
                db.EtuProgEtude.RemoveRange(etuProgEtu);
                db.SaveChanges();
                //faire apparaitre le message
                TempData["Success"] = Messages.I_016("");
                return RedirectToAction("Index");
            }
            TempData["id_Pers"] = id2;
            TempData["id_Prog"] = id;
            return RedirectToAction("Edit", "Etudiant", new { id = id2 });
         }

        //Méthode pour encrypter le de mot de passe.
        public static string encrypterChaine(string mdp)
        {
            byte[] Buffer;
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            Buffer = Encoding.UTF8.GetBytes(mdp);
            return BitConverter.ToString(provider.ComputeHash(Buffer)).Replace("-", "").ToLower();
        }
        //fonction de validation
        private void Valider([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,MP,ConfirmPassword,Courriel,DateNais,Actif")] Personne personne)
        {
            // Verifier si le matricule existe déja dans la BD
            if (db.Personne.Any(x => x.Matricule == personne.Matricule))
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
