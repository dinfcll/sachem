using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using sachem.Models;

namespace sachem.Controllers
{
    public class GroupesController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();

        // GET: Groupes
        public ActionResult Index(int? page, int? id)
        {
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Home");
            }
            RegisterViewbags();
            var pageNumber = page ?? 1;
            return View(Rechercher(id).ToPagedList(pageNumber, 20));
        }

        // GET: Groupes/Details/5
        public ActionResult Details(int? id)
        {
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupe.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            return View(groupe);
        }

        // GET: Groupes/Create
        public ActionResult Create()
        {
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Home");
            }
            int? a = (Session["id_Pers"] == null ? -1 : (int)Session["id_Pers"]);
            bool b = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
            ViewBag.id_Cours = new SelectList(db.Cours, "id_Cours", "CodeNom");
            var ens = from c in db.Personne where c.id_TypeUsag == 2 && (b ? true : c.id_Pers == (a == -1 ? c.id_Pers : a)) select c;
            ViewBag.id_Enseignant = new SelectList(ens, "id_Pers", "NomPrenom");
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "NomSession");
            return View();
        }

        // POST: Groupes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Home");
            }

            Valider(groupe);

            if (ModelState.IsValid)
            {
                db.Groupe.Add(groupe);
                db.SaveChanges();
                TempData["Success"] = string.Format(Messages.Q_008(groupe.NoGroupe));
                return RedirectToAction("Index");
            }

            ViewBag.id_Cours = new SelectList(db.Cours, "id_Cours", "Code", groupe.id_Cours);
            ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", groupe.id_Enseignant);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", groupe.id_Sess);
            
            
            return View(groupe);
        }

        // GET: Groupes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Home");
            }
            int a = (Session["id_Pers"] == null ? -1 : (int)Session["id_Pers"]);
            bool b = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Groupe groupe = db.Groupe.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Cours = new SelectList(db.Cours, "id_Cours", "CodeNom", groupe.id_Cours);
            ViewBag.id_Enseignant = new SelectList(db.Personne.Where(x => x.id_TypeUsag == 2).Where(x => x.id_Pers == (a == -1 || b ? x.id_Pers : a)), "id_Pers", "NomPrenom", groupe.id_Enseignant);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "NomSession", groupe.id_Sess);
            return View(groupe);
        }

        // POST: Groupes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Home");
            }

            Valider(groupe);

            if (ModelState.IsValid)
            {
                db.Entry(groupe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Cours = new SelectList(db.Cours, "id_Cours", "Code", groupe.id_Cours);
            ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", groupe.id_Enseignant);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", groupe.id_Sess);
            return View(groupe);
        }

        // GET: Groupes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Cours");
            }

            Groupe groupe = db.Groupe.Find(id);

            if (ModelState.IsValid)
            {
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
            int a = (Session["id_Pers"] == null ? -1 : (int)Session["id_Pers"]);
            bool b = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
            int idSess = 0, idEns = (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Enseignant ? a : 0), idCours = 0;
            if (id != null)
            {
                groupes = from d in db.Groupe orderby d.Session.p_Saison.Saison, d.Session.Annee, d.Cours.Code, d.Cours.Nom, d.NoGroupe where d.id_Enseignant == id select d;
            }
            else
            {
                if (Request.RequestType == "POST")
                {
                    int.TryParse(Request.Form["Sessions"], out idSess);
                    int.TryParse(Request.Form["Enseignants"], out idEns);
                    int.TryParse(Request["Cours"], out idCours);

                    RegisterViewbags();

                    groupes = from d in db.Groupe where d.id_Cours == (idCours == 0 ? d.id_Cours : idCours) && d.id_Enseignant == (idEns == 0 ? d.id_Enseignant : idEns) && d.id_Sess == (idSess == 0 ? d.id_Sess : idSess) orderby d.Session.p_Saison.Saison, d.Session.Annee, d.Cours.Code, d.Cours.Nom, d.NoGroupe select d;
                }
                else
                {
                    groupes = from d in db.Groupe orderby d.Session.p_Saison.Saison, d.Session.Annee, d.Cours.Code, d.Cours.Nom, d.NoGroupe where d.id_Enseignant == (a == -1 || b ? d.id_Enseignant : a) select d;
                }
            }
            foreach (var n in groupes)
            {
                n.nbPersonne = (from c in db.GroupeEtudiant where c.id_Groupe == n.id_Groupe select c).Count();
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

        [NonAction]
        private void RegisterViewbags()
        {
            int? a = (Session["id_Pers"] == null ? -1 : (int)Session["id_Pers"]);
            bool b = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
            int sess = db.Session.Max(s => s.id_Sess);
            //var last = from b in db.Session orderby b.id_Sess descending select b;
            ViewBag.Sessions = new SelectList(db.Session, "id_Sess", "NomSession", sess);
            var ens = from c in db.Personne where c.id_TypeUsag == 2 && (b ? true : c.id_Pers == (a == -1 ? c.id_Pers : a))  select c;
            ViewBag.Enseignants = new SelectList(ens, "id_Pers", "NomPrenom");
            ViewBag.Cours = new SelectList(db.Cours, "id_Cours", "CodeNom");
        }

        public ActionResult AjouterEleve(int idg, int? page)
        {
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.idg = idg;
            Groupe groupe = db.Groupe.Find(idg);
            //IEnumerable < Personne > personnes = (from c in db.Personne where c.id_TypeUsag == 1 select c).ToList().OrderBy(x => x.NomPrenom).ThenBy(x => x.Matricule7);
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
            return View(lstEtu.ToPagedList(pageNumber, 20));
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult AjouterEleveGET(int idg, int idp)
        {

            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Home");
            }

            Groupe g = db.Groupe.Find(idg);
            Personne p = db.Personne.Find(idp);

            if (g == null || p == null)
            {
                return HttpNotFound();
            }

            if (db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).Where(x => x.id_Groupe == idg).FirstOrDefault() != null)
            {
                //return Content("L'élève est déjà dans ce groupe.");
                ModelState.AddModelError(string.Empty, Messages.I_023(db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).Where(x => x.id_Groupe == idg).FirstOrDefault().Personne.Matricule7));
            }

            if(db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).FirstOrDefault() != null)
            {
                ModelState.AddModelError(string.Empty, Messages.Q_010(p.PrenomNom,g.Session.NomSession,g.NoGroupe, g.Cours.Nom));
                //return Content("L'élève est déjà dans un groupe.");
            }

            if (ModelState.IsValid)
            {
                GroupeEtudiant ge = new GroupeEtudiant();
                ge.Personne = p;
                ge.Groupe = g;

                db.GroupeEtudiant.Add(ge);
                g.GroupeEtudiant.Add(ge);

                db.SaveChanges();
                TempData["Success"]= string.Format(Messages.I_024(p.Matricule7,g.id_Groupe));

            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteEleve(int? id)
        {
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Cours");
            }


            GroupeEtudiant ge = db.GroupeEtudiant.Find(id);

            TempData["Success"] = string.Format(Messages.I_022(ge.Personne.Matricule7, ge.Groupe.NoGroupe));
            db.GroupeEtudiant.Remove(ge);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Deplacer(int? id)
        {
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GroupeEtudiant ge = db.GroupeEtudiant.Find(id);
            if (ge == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_groupedepl = new SelectList(db.Groupe, "id_Groupe", "Cours.CodeNom");
            return View(ge);
        }

        [HttpPost, ActionName("Deplacer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeplacerConfirmed(int? id)
        {
            if (!SachemIdentite.TypeListeAdmin.Contains(SachemIdentite.ObtenirTypeUsager(Session)))
            {
                return RedirectToAction("Index", "Home");
            }
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

            if (!(ge.id_Groupe == g.id_Groupe))
            {

                ge.Groupe = g;
                db.SaveChanges();
                TempData["Success"]= Messages.I_028(ge.Personne.Matricule7, g.NoGroupe, g.Cours.Nom);
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
                /*personnes = (from c in db.Personne where c.id_TypeUsag == 1 && 
                                c.Nom == (!String.IsNullOrEmpty(Nom) ? Nom: c.Nom) &&
                                c.Prenom == (!String.IsNullOrEmpty(Prenom) ? Prenom : c.Prenom)
                                select c).ToList().Where(x => x.Matricule7 == (!String.IsNullOrEmpty(Matricule) ? Matricule : x.Matricule7));*/
                personnes = db.Personne.Where(x => x.id_TypeUsag == 1).Where(c => c.Nom.Contains(!String.IsNullOrEmpty(Nom) ? Nom : c.Nom)).Where(c => c.Prenom.Contains(!String.IsNullOrEmpty(Prenom) ? Prenom : c.Prenom));
                personnes = personnes.Where(x => x.Matricule7.Contains(!String.IsNullOrEmpty(Matricule) ? Matricule : x.Matricule7));
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

            if (groupe.NoGroupe.ToString().Length >4)
                ModelState.AddModelError(string.Empty, Messages.U_005);

            if (db.Groupe.Any(r => r.NoGroupe == groupe.NoGroupe && r.id_Enseignant == groupe.id_Enseignant && r.id_Sess == groupe.id_Sess && r.id_Cours == groupe.id_Cours))
                ModelState.AddModelError(string.Empty, Messages.I_021(groupe.NoGroupe));
        }

    }
}
