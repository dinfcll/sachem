using System;
//using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
//using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Net;
//using System.Web;
using System.Web.Mvc;
using PagedList;
using sachem.Models;

namespace sachem.Controllers
{
    public class GroupesController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();
        List<TypeUsagers> RolesAcces = new List<TypeUsagers>() { TypeUsagers.Enseignant, TypeUsagers.Responsable, TypeUsagers.Super };

        [NonAction]
        private void RegisterViewbags()
        {
            int? idPers = (Session["id_Pers"] == null ? -1 : (int)Session["id_Pers"]);
            bool verif = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
            int sess = db.Session.Max(s => s.id_Sess);
            ViewBag.Sessions = new SelectList(db.Session.OrderByDescending(s => s.id_Sess), "id_Sess", "NomSession", sess);
            var ens = from c in db.Personne where c.id_TypeUsag == 2 && (verif ? true : c.id_Pers == (idPers == -1 ? c.id_Pers : idPers)) && c.Actif == true orderby c.Nom, c.Prenom select c;
            ViewBag.Enseignants = new SelectList(ens, "id_Pers", "NomPrenom");
            ViewBag.Cours = new SelectList(db.Cours.Where(x => x.Actif == true).OrderBy(x => x.Code), "id_Cours", "CodeNom");
        }
        [NonAction]
        private void RegisterViewBagsSessCours(Groupe groupe)
        {
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", groupe.id_Sess);
            ViewBag.id_Cours = new SelectList(db.Cours, "id_Cours", "Code", groupe.id_Cours);
        }
        // GET: Groupes
        public ActionResult Index(int? page, int? id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces,Session)) return RedirectToAction("Error", "Home", null);
            RegisterViewbags();
            var pageNumber = page ?? 1;
            return View(Rechercher(id).ToPagedList(pageNumber, 20));
        }
        // GET: Groupes/Create
        public ActionResult Create()
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session)) return RedirectToAction("Error", "Home", null);
            int? idPers = (Session["id_Pers"] == null ? -1 : (int)Session["id_Pers"]);
            bool verif = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
            ViewBag.id_Cours = new SelectList(db.Cours.Where(x => x.Actif == true).OrderBy(x => x.Code), "id_Cours", "CodeNom");
            var ens = from c in db.Personne where c.id_TypeUsag == 2 && (verif ? true : c.id_Pers == (idPers == -1 ? c.id_Pers : idPers)) && c.Actif == true orderby c.Nom, c.Prenom select c;
            ViewBag.id_Enseignant = new SelectList(ens, "id_Pers", "NomPrenom");
            ViewBag.id_Sess = new SelectList(db.Session.OrderByDescending(s => s.id_Sess), "id_Sess", "NomSession");
            return View();
        }

        // POST: Groupes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            return CreateEdit(groupe, true);
        }

        // GET: Groupes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session)) return RedirectToAction("Error", "Home", null);
            int idPers = (Session["id_Pers"] == null ? -1 : (int)Session["id_Pers"]);
            bool verif = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupe.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Cours = new SelectList(db.Cours.Where(x => x.Actif == true).OrderBy(x => x.Code), "id_Cours", "CodeNom", groupe.id_Cours);
            ViewBag.id_Enseignant = new SelectList(db.Personne.Where(x => x.id_TypeUsag == 2 && x.id_Pers == (idPers == -1 || verif ? x.id_Pers : idPers) && x.Actif == true).OrderBy(x=>x.Prenom).OrderBy(x=>x.Nom), "id_Pers", "NomPrenom", groupe.id_Enseignant);
            ViewBag.id_Sess = new SelectList(db.Session.OrderByDescending(s => s.id_Sess), "id_Sess", "NomSession", groupe.id_Sess);
            return View(groupe);
        }

        // POST: Groupes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            return CreateEdit(groupe);
        }

        [NonAction]
        private ActionResult CreateEdit([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe, bool Ajouter = false)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session)) return RedirectToAction("Error", "Home", null);

            Valider(groupe);

            if (ModelState.IsValid)
            {
                if (!Ajouter)
                {
                    db.Entry(groupe).State = EntityState.Modified;
                }
                else
                {
                    db.Groupe.Add(groupe);
                }
                db.SaveChanges();
                TempData["Questions"] = string.Format(Messages.Q_008(groupe.NoGroupe));
                TempData["idg"] = groupe.id_Groupe;
                return RedirectToAction("Index");
            }
           // if (SessionBag.Current.id_TypeUsag == 2)
            ViewBag.id_Cours = new SelectList(db.Cours, "id_Cours", "Code", groupe.id_Cours);
            ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", groupe.id_Enseignant);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", groupe.id_Sess);
            return View(groupe);
        }

        // GET: Groupes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session)) return RedirectToAction("Error", "Home", null);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupe.Find(id);

            if (db.GroupeEtudiant.Any(ge => ge.id_Groupe == id))
            {
                ViewBag.Error = Messages.Q_007(groupe.NoGroupe);
            }

            if (groupe == null)
            {
                return HttpNotFound();
            }
            return View(groupe);
        }

        // POST: Groupes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session)) return RedirectToAction("Error", "Home", null);

            Groupe groupe = db.Groupe.Find(id);
            GroupeEtudiant ge = db.GroupeEtudiant.Find(groupe.id_Groupe);

            if (ModelState.IsValid)
            {
                foreach (var x in (from c in db.GroupeEtudiant where c.id_Groupe == groupe.id_Groupe select c))
                {
                    db.GroupeEtudiant.Remove(x);
                }
                db.Groupe.Remove(groupe);
                db.SaveChanges();
                TempData["Success"] = string.Format(Messages.I_020(groupe.NoGroupe));
            }

            return RedirectToAction("Index");
        }

        [NonAction]
        private IEnumerable<Groupe> Rechercher(int? id)
        {
            IQueryable<Groupe> groupes;
            int idPers = (Session["id_Pers"] == null ? -1 : (int)Session["id_Pers"]);
            bool verif = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
            int idSess = 0, idEns = (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Enseignant ? idPers : 0), idCours = 0;
            if (id != null)
            {
                groupes = from d in db.Groupe
                          where d.id_Enseignant == id
                          orderby d.Session.p_Saison.Saison, d.Session.Annee, d.Cours.Code, d.Cours.Nom, d.NoGroupe
                          select d;
            }
            else
            {
                if (Request.RequestType == "POST")
                {
                    int.TryParse(Request.Form["Sessions"], out idSess);
                    int.TryParse(Request.Form["Enseignants"], out idEns);
                    int.TryParse(Request["Cours"], out idCours);

                    RegisterViewbags();

                    groupes = from d in db.Groupe
                              where d.id_Cours == (idCours == 0 ? d.id_Cours : idCours) && d.id_Enseignant == (idEns == 0 ? d.id_Enseignant : idEns) && d.id_Sess == (idSess == 0 ? d.id_Sess : idSess)
                              orderby d.Session.p_Saison.Saison, d.Session.Annee, d.Cours.Code, d.Cours.Nom, d.NoGroupe
                              select d;
                }
                else
                {
                    groupes = from d in db.Groupe
                              where d.id_Enseignant == (idPers == -1 || verif ? d.id_Enseignant : idPers)
                              orderby d.Session.p_Saison.Saison, d.Session.Annee, d.Cours.Code, d.Cours.Nom, d.NoGroupe
                              select d;
                }
            }

            
            return groupes.ToList();
        }

        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult AjouterEleve(int idg, int? page)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session)) return RedirectToAction("Error", "Home", null);
            ViewBag.idg = idg;
            Groupe groupe = db.Groupe.Find(idg);
            IEnumerable<Personne> personnes = RechercherEleve();

            db.Configuration.LazyLoadingEnabled = false;

                /*requête LINQ qui va chercher tous les étudiants répondant aux critères de recherche ainsi que leur programme d'étude actuel. */
            var lstEtu = from q in
                         (from p in personnes.Where(x => /*x.Actif == true &&  x.GroupeEtudiant.Any(y => y.id_Groupe == idg) && */ 
                           x.EtuProgEtude.Any(y => y.id_Sess == groupe.id_Sess)).OrderBy(x => x.Nom)
                            select new
                            {
                                Personne = p,
                                ProgEtu = (from pe in db.EtuProgEtude where p.id_Pers == pe.id_Etu && db.ProgrammeEtude.Any(y=> y.id_ProgEtu == pe.id_ProgEtu) orderby pe.id_Sess descending select pe).FirstOrDefault().ProgrammeEtude,
                            }).AsEnumerable()
                            orderby q.Personne.Nom, q.Personne.Prenom
                            // le résultat de la requête sera une liste de PersonneProgEtu (déclaré plus haut),
                            // si l'objet n'est pas déclaré, la vue dynamique n'est pas capable d'évaluer correctement
                            select new PersonneProgEtu { personne = q.Personne, progEtuActif = q.ProgEtu };
            db.Configuration.LazyLoadingEnabled = true;

            var pageNumber = page ?? 1;
            ViewBag.page = pageNumber;
            TempData["idg"] = idg;
            return View(lstEtu.ToPagedList(pageNumber, 20));
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult AjouterEleveGET(int idg, int idp,int noclick = 0)
        {

            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session)) return RedirectToAction("Error", "Home", null);

            Groupe g = db.Groupe.Find(idg);
            Personne p = db.Personne.Find(idp);

            if (g == null || p == null)
            {
                return HttpNotFound();
            }

            if (db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).Where(x => x.id_Groupe == idg).FirstOrDefault() != null)
            {
                TempData["ErrorAjEl"] = Messages.I_023(db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).Where(x => x.id_Groupe == idg).FirstOrDefault().Personne.Matricule7);
                ModelState.AddModelError(string.Empty, Messages.I_023(db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).Where(x => x.id_Groupe == idg).FirstOrDefault().Personne.Matricule7));
            }

            if (ModelState.IsValid)
            {

                if (db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).FirstOrDefault() != null && noclick == 0)
                {
                    TempData["idGe"] = db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).FirstOrDefault().id_GroupeEtudiant;
                    TempData["personne"] = p.id_Pers;
                    TempData["idgcible"] = g.NoGroupe;
                    TempData["ErrorDep"] = Messages.Q_010(p.PrenomNom, g.Session.NomSession, g.NoGroupe, g.Cours.Nom);
                    ModelState.AddModelError(string.Empty, Messages.Q_010(p.PrenomNom, g.Session.NomSession, g.NoGroupe, g.Cours.Nom));
                }

                if (ModelState.IsValid)
                {

                    GroupeEtudiant ge = new GroupeEtudiant();
                    ge.Personne = p;
                    ge.Groupe = g;

                    db.GroupeEtudiant.Add(ge);
                    g.GroupeEtudiant.Add(ge);

                    db.SaveChanges();
                    TempData["Success"] = string.Format(Messages.I_024(p.Matricule7, g.id_Groupe));
                }

            }

            return RedirectToAction("AjouterEleve", new { idg = idg, page = ViewBag.page });
        }

        public ActionResult DeleteEleve(int? id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session)) return RedirectToAction("Error", "Home", null);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GroupeEtudiant ge = db.GroupeEtudiant.Find(id);
            if (ge == null)
            {
                return HttpNotFound();
            }
            return View(ge);
        }

        // POST: Groupes/Delete/5
        [HttpPost, ActionName("DeleteEleve")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEleveConfirmed(int id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session)) return RedirectToAction("Error", "Home", null);


            GroupeEtudiant ge = db.GroupeEtudiant.Find(id);

            TempData["Success"] = string.Format(Messages.I_022(ge.Personne.Matricule7, ge.Groupe.NoGroupe));
            db.GroupeEtudiant.Remove(ge);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Deplacer(int? id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session)) return RedirectToAction("Error", "Home", null);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GroupeEtudiant ge = db.GroupeEtudiant.Find(id);
            if (ge == null)
            {
                return HttpNotFound();
            }

            if ( TempData["idgcible"] == null)
                ViewBag.id_groupedepl = new SelectList(db.Groupe, "id_Groupe", "Cours.CodeNom");
            else
                ViewBag.id_groupedepl = new SelectList(db.Groupe, "id_Groupe", "Cours.CodeNom",TempData["idgcible"]);
            return View(ge);
        }

        [HttpPost, ActionName("Deplacer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeplacerConfirmed(int? id)
        {
            if (!SachemIdentite.ValiderRoleAcces(RolesAcces, Session)) return RedirectToAction("Error", "Home", null);
            int idgretu, idg;
            if (!int.TryParse(Request.Form["idGroupeEtudiant"], out idgretu) || !int.TryParse(Request.Form["id_groupedepl"], out idg))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GroupeEtudiant ge = db.GroupeEtudiant.Find(idgretu);
            Groupe g = db.Groupe.Find(idg);

            if(ge == null || g == null)
            {
                return HttpNotFound();
            }

            GroupeEtudiant ge2 = g.GroupeEtudiant.Where(x => x.id_Etudiant == ge.id_Etudiant).ToList().FirstOrDefault();

            if ( ge2 != null && ge.Groupe.id_Sess == ge2.Groupe.id_Sess && ge.id_Etudiant == ge2.id_Etudiant)
                {
                    TempData["Success"] = Messages.I_041(ge.Personne.Matricule7, g.NoGroupe, g.Cours.Nom);

                }
             else 
            {
                ge.Groupe = g;
                db.SaveChanges();
                TempData["Success"] = Messages.I_040(ge.Personne.Matricule7, g.NoGroupe, g.Cours.Nom);
            }

            return RedirectToAction("Index");
        }

        [NonAction]
        private IEnumerable<Personne> RechercherEleve()
        {
            IEnumerable<Personne> personnes = null;

            string Nom = Request.Form["Nom"];
            string Prenom = Request.Form["Prenom"];
            string Matricule = Request.Form["Matricule"];

            if (String.IsNullOrEmpty(Matricule) && String.IsNullOrEmpty(Nom) && String.IsNullOrEmpty(Prenom))
            {
                personnes = (from c in db.Personne where c.id_TypeUsag == 1 select c).ToList().OrderBy(x => x.NomPrenom).ThenBy(x => x.Matricule7);
            }
            else
            {
                personnes = db.Personne.Where(x => x.id_TypeUsag == 1).Where(c => c.Nom.StartsWith(!String.IsNullOrEmpty(Nom) ? Nom : c.Nom)).Where(c => c.Prenom.StartsWith(!String.IsNullOrEmpty(Prenom) ? Prenom : c.Prenom));
                personnes = personnes.Where(x => x.Matricule7.StartsWith(!String.IsNullOrEmpty(Matricule) ? Matricule : x.Matricule7));
            }
            return personnes;
        }

        public virtual JsonResult ActualiseEnseignant(int session = 0)
        {
            var a = db.Groupe.Where(x => (x.id_Sess == session || session == 0)).ToList();
            List<Personne> ens = new List<Personne>();
            foreach(var x in a)
            {
                ens.Add(x.Personne);
            }
            return Json(ens.AsEnumerable(), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult ActualiseCours(int session = 0, int ens = 0)
        {
            var a = db.Groupe.Where(x => (x.id_Sess == session || session == 0)).Where(x => (x.id_Enseignant == ens || ens == 0));
            List<Cours> cours = new List<Cours>();
            foreach(var x in a)
            {
                cours.Add(x.Cours);
            }
            return Json(cours.AsEnumerable(), JsonRequestBehavior.AllowGet);
        }
 
        [NonAction]
        private void Valider([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            if (db.Groupe.Any(r => r.NoGroupe == groupe.NoGroupe && r.id_Sess == groupe.id_Sess && r.id_Cours == groupe.id_Cours))
                ModelState.AddModelError(string.Empty, Messages.I_021(groupe.NoGroupe));
        }

    }
}
