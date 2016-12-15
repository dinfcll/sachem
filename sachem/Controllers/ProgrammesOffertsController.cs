using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using System.Net;
using System.Data.Entity;
using sachem.Classes_Sachem;

namespace sachem.Controllers
{
    public class ProgrammesOffertsController : Controller
    {
        private readonly SACHEMEntities _db = new SACHEMEntities();

        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Index(string recherche, int? page)
        {
            var numeroPage = (page ?? 1);
            ViewBag.Recherche = recherche;

            return View("Index",Recherche(recherche).ToPagedList(numeroPage, 20));
        }
        
        // GET: ProgrammesOfferts/Create
        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProgrammesOfferts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_ProgEtu,Code,NomProg,Annee,Actif")] ProgrammeEtude programme)
        {
            Valider(programme);
            if (ModelState.IsValid)
            {               
                _db.ProgrammeEtude.Add(programme);
                _db.SaveChanges();

                TempData["Success"] = string.Format(Messages.ProgrammeEnregistre(programme.NomProg));
                return RedirectToAction("Index");
            }
            return View(programme);
        }

        //Méthode qui permet de modifier un programme. on vérifie que le proramme existe bien pour pouvoir rediriger l'usager vers 
        //la bonne vue.
        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var programme = _db.ProgrammeEtude.Find(id);

            if (programme == null)
            {
                return HttpNotFound();
            }

            if (programme.NomProg.Any())
            {
                ViewBag.DisEns = "True";
            }

            return View(programme);
        }

        //POST:modifier un programme
        //permet de modifier un programme et de l'enregistrer. vérification si le programme est bien et on l'enregistre par la suite.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_ProgEtu,Code,NomProg,Annee,Actif")] ProgrammeEtude programme, int? page)
        {
            Valider(programme);

            if (ModelState.IsValid)
            {
                _db.Entry(programme).State = EntityState.Modified;
                _db.SaveChanges();

                TempData["Success"] = string.Format(Messages.ProgrammeEnregistre(programme.NomProg));
                return RedirectToAction("Index");
            }
            return View(programme);
        }

        // GET: ProgrammesOfferts/Delete/5
        //Fonction qui permet de retourner l'utilisateur à la page de suppression avec le bon programme d'étude. On verifie si le 
        //programme existe réellement pour rediriger l'usager vers la bonne action
        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var programme = _db.ProgrammeEtude.Find(id);

            if (programme == null)
            {
                return HttpNotFound();
            }

            return View(programme);
        }

        // POST: ProgrammesOfferts/Delete/5
        /*Fonction qui permet de supprimer un programme. Premièrement, elle regarde s'il y a un étudiant lié au programme d'études.
        Si oui, il est impossible de le supprimer. Sinon, le programme est supprimé*/
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, int? page)
        {
            var pageNumber = page ?? 1;
           
            if (_db.EtuProgEtude.Any(r => r.id_ProgEtu == id))
            {
                ModelState.AddModelError(string.Empty, Messages.ProgrammeSupprimerErreurEtudiantAssocie);
            }

            if (ModelState.IsValid)
            {
                var programme = _db.ProgrammeEtude.Find(id);
                _db.ProgrammeEtude.Remove(programme);
                _db.SaveChanges();
                ViewBag.Success = string.Format(Messages.ProgrammeSupprime(programme.NomProg));
            }
            return View("Index", Recherche(null).ToPagedList(pageNumber, 20));
        }

        //Méthode qui permet de vérifier si le programme d'étude existe réellement, pour que l'on puisse le créer ou le modifier
        [NonAction]
        public void Valider(ProgrammeEtude programme)
        { 
            if (_db.ProgrammeEtude.Any(c => c.Code == programme.Code && c.Actif && programme.Actif && c.id_ProgEtu != programme.id_ProgEtu))
            {
                ModelState.AddModelError(string.Empty, Messages.ProgrammeAjouterErreurExisteDeja(programme.Code));
            }
            if(_db.ProgrammeEtude.Any(c => c.id_ProgEtu == programme.id_ProgEtu && c.Actif) && programme.Actif == false)
            { 
                if (_db.EtuProgEtude.Any(c => c.id_ProgEtu == programme.id_ProgEtu))
                {
                    ModelState.AddModelError(string.Empty, Messages.ProgrammeInactifErreur);
                }
            }
        }
         
        //Méthode qui permet de faire la recherche, soit sur le nom de programme ou sur le code.
        [NonAction]
        private IEnumerable<ProgrammeEtude> Recherche(string recherche)
        {
            var programmesEtude = from c in _db.ProgrammeEtude
                                  orderby c.Code, c.Annee
                                  select c;

            if (!string.IsNullOrEmpty(recherche))
            {
                programmesEtude = programmesEtude.Where(c => c.Code.StartsWith(recherche) || c.NomProg.StartsWith(recherche)) as IOrderedQueryable<ProgrammeEtude>;
            }

            return programmesEtude?.ToList();
        }
    }
}
