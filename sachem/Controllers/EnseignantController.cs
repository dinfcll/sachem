using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using System.Security.Cryptography;
using System.Text;

namespace sachem.Controllers
{
    public class EnseignantController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();

        private void ListeEnseignant(int Enseignant = 0)
        {
            var lEnseignant = db.Personne.AsNoTracking().OrderBy(p => p.Nom ).ThenBy(p => p.Prenom);
            var slEnseignant = new List<SelectListItem>();
            slEnseignant.AddRange(new SelectList(lEnseignant, "id_Pers", "Nom", Enseignant));

            ViewBag.Enseignant = slEnseignant;
        }


        //Fonction pour gérer la recherche, elle est utilisée dans la suppression et dans l'index
        [NonAction]
        private IEnumerable<Personne> Rechercher()
        {
            var enseignant = 0;
            var actif = true;

            // Verifier si la case a cocher est coché ou non
            if (!string.IsNullOrEmpty(Request.Form["Actif"]))
                actif = Request.Form["Actif"].Contains("true");


            ViewBag.Actif = actif;
            // liste et pagination
            ListeEnseignant(enseignant);

            // Requete linq pour aller chercher les enseignants et responsables dans la BD
            var Enseignant = from c in db.Personne
                        where (c.id_TypeUsag == 2 || c.id_TypeUsag == 3)
                        && c.Actif == actif
                        orderby c.Nom,c.Prenom
                        select c;

            return Enseignant.ToList();
        }


        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;

            return View(Rechercher().ToPagedList(pageNumber, 20));
        }

        private void Valider([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,MP,ConfMP,Courriel,DateNais,Actif")] Personne personne)
        {
            if (db.Personne.Any(x => x.NomUsager == personne.NomUsager && x.id_Pers != personne.id_Pers))// Verifier si le nom d'usager existe ou s'il a entré son ancien nom
                ModelState.AddModelError(string.Empty, Messages.I_013(personne.NomUsager));

            if (personne.MP != personne.ConfMP) // Verifier la correspondance des mots de passe
                ModelState.AddModelError(string.Empty, Messages.C_001);
        }

        // GET: Enseignant/Create
        public ActionResult Create()
        {
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");
            // Permet d'afficher seulement Enseignant et Responsable du Sachem dans les valeurs possibles de la list déroulante.
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag.Where(x => x.id_TypeUsag == 2 || x.id_TypeUsag == 3), "id_TypeUsag", "TypeUsag"); 
            return View();
        }

        // POST: Enseignant/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,MPDemander,ConfMP,Courriel,DateNais,Actif")] Personne personne)
        {           
            personne.MP = personne.MPDemander;
            Valider(personne);
            if (ModelState.IsValid)
            {
                personne.MP = encrypterChaine(personne.MP); // Encryption du mot de passe
                db.Personne.Add(personne);
                db.SaveChanges();
                TempData["Success"] = Messages.Q_004(personne.NomUsager); // Message afficher sur la page d'index confirmant la création
                return RedirectToAction("Index");
            }
            // afficher les listes déroulantes contenant le type d'usager et le sexe
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag.Where(x => x.id_TypeUsag == 2 || x.id_TypeUsag == 3), "id_TypeUsag", "TypeUsag");

            return View(personne);

        }
        //Méthode d'encryption de mot de passe.
        public static string encrypterChaine(string Chaine)
        {
            byte[] buffer;
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            buffer = Encoding.UTF8.GetBytes(Chaine);
            return BitConverter.ToString(provider.ComputeHash(buffer)).Replace("-", "").ToLower();
        }
        // GET: Enseignant/Edit/5
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
            // afficher les listes déroulantes contenant le type d'usager et le sexe
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag.Where(x => x.id_TypeUsag == 2 || x.id_TypeUsag == 3), "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
            return View(personne);
        }

        // POST: Enseignant/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Pers, id_Sexe, id_TypeUsag, Nom, Prenom, NomUsager, MPDemander, ConfMP, Courriel, DateNais, Actif")] Personne personne)
        {
            if(personne.MPDemander != null)
            {
                personne.MP = personne.MPDemander;
                personne.MP = encrypterChaine(personne.MPDemander); // Appel de la méthode qui encrypte le mot de passe
                personne.ConfMP = encrypterChaine(personne.ConfMP); // Appel de la méthode qui encrypte la confirmation du mot de passe
            }
            else
            {         
                var Enseignant = from c in db.Personne
                               where (c.id_Pers == personne.id_Pers)
                                 select c.MP;
                personne.MP = Enseignant.SingleOrDefault();
                personne.ConfMP = personne.MP;
            }
            Valider(personne);
            if (ModelState.IsValid)
            {
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = Messages.I_015(personne.NomUsager); // Message afficher sur la page d'index confirmant la modification
                return RedirectToAction("Index");

            }
            // afficher les listes déroulantes contenant le type d'usager et le sexe
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag.Where(x => x.id_TypeUsag == 2|| x.id_TypeUsag == 3), "id_TypeUsag", "TypeUsag", personne.id_TypeUsag); 
            return View(personne);
        }

        // GET: Enseignant/Delete/5
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

        // POST: Enseignant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,int? page)
        {
            var pageNumber = page ?? 1;
            if (db.Groupe.Any(g => g.id_Enseignant == id)) // Verifier si l'enseignant est relié a un groupe
            {
                ModelState.AddModelError(string.Empty, Messages.I_012);
            }
            if (db.Jumelage.Any(g => g.id_Enseignant == id)) // Vérifier si l'enseignant est relié a un jumelage
            {
                ModelState.AddModelError(string.Empty, Messages.I_033);
            }
            if (ModelState.IsValid)
            {
                Personne personne = db.Personne.Find(id);
                var SuppPersonne = db.Personne.Where(x => x.id_Pers == id); // rechercher l'enseignant
                db.Personne.RemoveRange(SuppPersonne); // retirer toute les occurences de l'enseignant
                db.SaveChanges();
                ViewBag.Success = string.Format(Messages.I_029(personne.NomUsager));
            }
            return View("Index", Rechercher().ToPagedList(pageNumber, 20)); // retour à index avec les divisions par page
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
