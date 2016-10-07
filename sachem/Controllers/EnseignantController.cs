using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using sachem.Models;
using PagedList;


namespace sachem.Controllers
{
    public class EnseignantController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();

        private const int ID_ENSEIGNANT = 2;
        private const int ID_RESP = 3;

        List<TypeUsagers> RolesAcces = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super };

        [NonAction]
        private void ListeEnseignant(int Enseignant = 0)
        {
            var lEnseignant = db.Personne.AsNoTracking().OrderBy(p => p.Nom ).ThenBy(p => p.Prenom);
            var slEnseignant = new List<SelectListItem>();
            slEnseignant.AddRange(new SelectList(lEnseignant, "id_Pers", "Nom", Enseignant));

            ViewBag.Enseignant = slEnseignant;
        }

        private bool siCaseEstCochee()
        {
            //Vérifier si la case est cocher ou non
            bool CaseEstCochee = !string.IsNullOrEmpty(Request.Form["Actif"]);

            return CaseEstCochee;
        }

        //Fonction pour gérer la recherche, elle est utilisée dans la suppression et dans l'index
        [NonAction]
        private IEnumerable<Personne> Rechercher()
        {
            var enseignant = 0;
            var actif = true;

            // Verifier si la case a cocher est coché ou non
            if (siCaseEstCochee())
                actif = Request.Form["Actif"].Contains("true");


            ViewBag.Actif = actif;
            // liste et pagination
            ListeEnseignant(enseignant);

            // Requete linq pour aller chercher les enseignants et responsables dans la BD
            var Enseignant = from c in db.Personne
                        where (c.id_TypeUsag == ID_ENSEIGNANT || c.id_TypeUsag == 3)
                        && c.Actif == actif
                        orderby c.Nom,c.Prenom
                        select c;

            return Enseignant.ToList();
        }

     
        public ActionResult Index(int? page)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);

            var pageNumber = page ?? 1;

            return View(Rechercher().ToPagedList(pageNumber, 20));
        }

        [NonAction]
        private void Valider([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,MP,ConfirmPasswordEdit,Courriel,DateNais,Actif")] Personne personne)
        {
            if (db.Personne.Any(x => x.NomUsager == personne.NomUsager && x.id_Pers != personne.id_Pers))// Verifier si le nom d'usager existe ou s'il a entré son ancien nom
                ModelState.AddModelError(string.Empty, Messages.I_013(personne.NomUsager));
        }

        // GET: Enseignant/Create
        public ActionResult Create()
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);

            RemplirDropList();
            return View();
        }

        // POST: Enseignant/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Pers,id_Sexe,id_TypeUsag,Nom,Prenom,NomUsager,MP,ConfirmPassword,Courriel,DateNais,Actif")] Personne personne)
        {
            Valider(personne);
            
            if (ModelState.IsValid)                
            {
                if (personne.MP == null)
                    ModelState.AddModelError("MP",Messages.U_001);
                else
                {
                    SachemIdentite.encrypterMPPersonne(ref personne); // Encryption du mot de passe
                    db.Personne.Add(personne);
                    db.SaveChanges();
                    TempData["Success"] = Messages.Q_004(personne.NomUsager, personne.id_Pers); // Message afficher sur la page d'index confirmant la création
                    return RedirectToAction("Index");
                }
            }

            RemplirDropList(personne);
            ViewBag.id_person = personne.id_Pers;
            return View(personne);

        }
        // GET: Enseignant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            RemplirDropList(personne);
            ViewBag.id_person = personne.id_Pers;
            personne.MP = "";
            return View(personne);
        }

        // POST: Enseignant/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Pers, id_Sexe, id_TypeUsag, Nom, Prenom, NomUsager, MP, ConfirmPassword, Courriel, DateNais, Actif")] Personne personne)
        {
            if(personne.AncienMotDePasse != null)
            {
                SachemIdentite.encrypterMPPersonne(ref personne); // Appel de la méthode qui encrypte le mot de passe
            }
            else
            {         
                var Enseignant = from c in db.Personne
                               where (c.id_Pers == personne.id_Pers)
                                 select c.MP;
                personne.MP = Enseignant.SingleOrDefault();
            }
            Valider(personne);
            if (ModelState.IsValid)
            {
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = Messages.I_015(personne.NomUsager); // Message afficher sur la page d'index confirmant la modification
                return RedirectToAction("Index");

            }

            return View(personne);
        }

        // GET: Enseignant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session))
                return RedirectToAction("Error", "Home", null);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personne.Find(id);
            if(SessionBag.Current.id_pers == id)
            {
                TempData["Error"] = Messages.I_037;
                return RedirectToAction("Index", "Enseignant", null);
            }
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

        [NonAction]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void RemplirDropList()
        {
            // afficher les listes déroulantes contenant le type d'usager et le sexe
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe");
            // Permet d'afficher seulement Enseignant et Responsable du Sachem dans les valeurs possibles de la list déroulante.
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag.Where(x => x.id_TypeUsag == ID_ENSEIGNANT || x.id_TypeUsag == 3), "id_TypeUsag", "TypeUsag");
        }
        private void RemplirDropList(Personne personne)
        {
            // afficher les listes déroulantes contenant le type d'usager, le sexe et sélectionne la valeur du Sexe de la personne dans la liste déroulante.
            ViewBag.id_Sexe = new SelectList(db.p_Sexe, "id_Sexe", "Sexe", personne.id_Sexe);
            // Permet d'afficher seulement Enseignant et Responsable du Sachem dans les valeurs possibles de la list déroulante et sélectionne la valeur du type d'usager de la personne dans la liste déroulante.
            ViewBag.id_TypeUsag = new SelectList(db.p_TypeUsag.Where(x => x.id_TypeUsag == ID_ENSEIGNANT || x.id_TypeUsag == 3), "id_TypeUsag", "TypeUsag", personne.id_TypeUsag);
        }
    }
}
