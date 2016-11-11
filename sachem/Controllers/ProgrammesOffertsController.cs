using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using System.Net;
using System.Data.Entity;
using static sachem.Classes_Sachem.ValidationAcces;

namespace sachem.Controllers
{
    public class ProgrammesOffertsController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();

        [ValidationAccesSuper]
        public ActionResult Index(string recherche, int? page)
        {
            int numeroPage = (page ?? 1);
            ViewBag.Recherche = recherche;
            return View("Index",Recherche(recherche).ToPagedList(numeroPage, 20));
        }
        
        // GET: ProgrammesOfferts/Create
        [ValidationAccesSuper]
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
                db.ProgrammeEtude.Add(programme);
                db.SaveChanges();

                TempData["Success"] = string.Format(Messages.I_007(programme.NomProg));
                return RedirectToAction("Index");
            }
            return View(programme);
        }

        //Méthode qui permet de modifier un programme. on vérifie que le proramme existe bien pour pouvoir rediriger l'usager vers 
        //la bonne vue.
        [ValidationAccesSuper]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var programme = db.ProgrammeEtude.Find(id);

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
                db.Entry(programme).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = string.Format(Messages.I_007(programme.NomProg));
                return RedirectToAction("Index");
            }
            return View(programme);
        }

        // GET: ProgrammesOfferts/Delete/5
        //Fonction qui permet de retourner l'utilisateur à la page de suppression avec le bon programme d'étude. On verifie si le 
        //programme existe réellement pour rediriger l'usager vers la bonne action
        [ValidationAccesSuper]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var programme = db.ProgrammeEtude.Find(id);

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
           
            if (db.EtuProgEtude.Any(r => r.id_ProgEtu == id))
            {
                ModelState.AddModelError(string.Empty, Messages.I_005());
            }

            if (ModelState.IsValid)
            {
                var programme = db.ProgrammeEtude.Find(id);
                db.ProgrammeEtude.Remove(programme);
                db.SaveChanges();
                ViewBag.Success = string.Format(Messages.I_008(programme.NomProg));
            }
            return View("Index", Recherche(null).ToPagedList(pageNumber, 20));
        }

        //Méthode qui permet de vérifier si le programme d'étude existe réellement, pour que l'on puisse le créer ou le modifier
        [NonAction]
        public void Valider([Bind(Include = "id_ProgEtu,Code,NomProg,Annee,Actif")]ProgrammeEtude programme)
        { 
            if (db.ProgrammeEtude.Any(c => c.Code == programme.Code && c.Actif && c.Annee == programme.Annee && c.id_ProgEtu != programme.id_ProgEtu))
            {
                ModelState.AddModelError(String.Empty, Messages.I_006(programme.Code));
            }
            var programmeBd = db.ProgrammeEtude.Find(programme.id_ProgEtu);
            if (programmeBd.Actif == true && programme.Actif == false)
            {
                if (db.EtuProgEtude.Any(c => c.id_ProgEtu == programme.id_ProgEtu))
                {
                    ModelState.AddModelError(String.Empty, "Impossible de mettre le programme inactif si il est encore relié à des étudiants");
                }
            }

        }

        //Méthode qui permet de faire la recherche, soit sur le nom de programme ou sur le code.
        [NonAction]
        private IEnumerable<ProgrammeEtude> Recherche(string recherche)
        {
            var programmesEtude = from c in db.ProgrammeEtude
                                  orderby c.Code, c.Annee
                                  select c;

            if (!String.IsNullOrEmpty(recherche))
            {
                programmesEtude = programmesEtude.Where(c => c.Code.StartsWith(recherche) || c.NomProg.StartsWith(recherche)) as IOrderedQueryable<ProgrammeEtude>;
            }
            return programmesEtude.ToList();
        }
    }
}
