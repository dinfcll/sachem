using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using sachem.Models;
using static sachem.Classes_Sachem.ValidationAcces;

namespace sachem.Controllers
{
    public class GroupesController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();

        private IEnumerable<Object> ObtenirListeEnseignant(int session)
        {
                var ens = db.Personne
                    .AsNoTracking()
                    .Join(db.Groupe, p => p.id_Pers, g => g.id_Enseignant, (p, g) => new { Personne = p, Groupe = g })
                    .Where(sel => sel.Personne.id_TypeUsag == 2 && (sel.Groupe.id_Sess == session || session == 0))
                    .OrderBy(x => x.Personne.Prenom).ThenBy(x => x.Personne.Nom)
                    .Select(e => new { e.Personne.id_Pers, NomPrenom = e.Personne.Nom + ", " + e.Personne.Prenom })
                    .Distinct();
            return ens.AsEnumerable();
        }

        private IEnumerable<Object> ObtenirListeCours(int session,int enseignant)
        {
            var cours = db.Cours.Join(db.Groupe, c => c.id_Cours, g => g.id_Cours, (c, g) => new { Cours = c, Groupe = g })
                        .Where(sel => (sel.Groupe.id_Sess == session || session == 0) && (sel.Groupe.id_Enseignant == enseignant || enseignant == 0))
                        .OrderBy(x => x.Cours.Nom).ThenBy(x=>x.Cours.Code).Select(c=> new { c.Cours.id_Cours, CodeNom = c.Cours.Code + "-" + c.Cours.Nom })
                        .Distinct();
            return cours.AsEnumerable();
        }

        [AcceptVerbs("Get", "Post")]
        public virtual JsonResult ActualiseCours(int enseignant = 0 ,int session = 0)
        {
            var actucours = ObtenirListeCours(session, enseignant);
            return Json(actucours.ToList(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("Get", "Post")]
        public virtual JsonResult ActualiseEnseignant(int session = 0)
        {
            var actuens = ObtenirListeEnseignant(session);
            return Json(actuens.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Groupes
        [ValidationAccesEnseignant]
        public ActionResult Index(int? id,int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.Disabled = sDisabled();
            return View(Rechercher(id).ToPagedList(pageNumber, 20));
        }

        // GET: Groupes/Create
        [ValidationAccesEnseignant]
        public ActionResult Create(int? idEns)
        {
            int? idPers = (Session["id_Pers"] == null ? -1 : (int)Session["id_Pers"]);
            bool verif = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
            ViewBag.id_Cours = new SelectList(db.Cours.Where(x => x.Actif == true).OrderBy(x => x.Code), "id_Cours", "CodeNom");
            var ens = from c in db.Personne where c.id_TypeUsag == 2 && (verif ? true : c.id_Pers == (idPers == -1 ? c.id_Pers : idPers)) && c.Actif == true orderby c.Nom, c.Prenom select c;
            ViewBag.id_Enseignant = new SelectList(ens, "id_Pers", "NomPrenom", (idEns != null ? idEns : null));
            ViewBag.id_Sess = new SelectList(db.Session.OrderByDescending(s => s.id_Sess), "id_Sess", "NomSession");
            ViewBag.Disabled = sDisabled();
            return View();
        }

        // POST: Groupes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            ViewBag.Disabled = sDisabled();
            return CreateEdit(groupe, true);
        }

        // GET: Groupes/Edit/5
        [ValidationAccesEnseignant]
        public ActionResult Edit(int? id)
        {
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
            ViewBag.Disabled = sDisabled();
            return View(groupe);
        }

        private string sDisabled()
        {
            return (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Enseignant ? "disabled" : "");
        }

        // POST: Groupes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            ViewBag.Disabled = sDisabled();
            return CreateEdit(groupe);
        }

        [ValidationAccesEnseignant]
        private ActionResult CreateEdit([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe, bool Ajouter = false)
        {
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
            ViewBag.id_Cours = new SelectList(db.Cours, "id_Cours", "CodeNom", groupe.id_Cours);
            ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "NomPrenom", groupe.id_Enseignant);
            ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "NomSession", groupe.id_Sess);
            ViewBag.Disabled = sDisabled();
            return View(groupe);
        }

        // GET: Groupes/Delete/5
        [ValidationAccesEnseignant]
        public ActionResult Delete(int? id)
        {
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
        [ValidationAccesEnseignant]
        public ActionResult DeleteConfirmed(int id)
        {
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

        private IEnumerable<Groupe> Rechercher(int? id)
        {
            IQueryable<Groupe> groupes;
            int idPers = (Session["id_Pers"] == null ? -1 : (int)Session["id_Pers"]);
            bool verif = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
            int idSess = 0, idEns = (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Enseignant ? idPers : 0), idCours = 0;

            if (Request.RequestType == "GET" && Session["DernRechCours"] != null && Session["DernRechCoursUrl"].ToString() == Request.Url?.LocalPath)
            {
                var anciennerech = Session["DernRechCours"].ToString();
                var tanciennerech = anciennerech.Split(';');

                if (tanciennerech[0] != "")
                {
                    idSess = int.Parse(tanciennerech[0]);
                }
                if (tanciennerech[1] != "")
                {
                    idEns = int.Parse(tanciennerech[1]);
                }
                if (tanciennerech[2] != "")
                {
                    idCours = int.Parse(tanciennerech[2]);
                }

            }

            else
            {
                if (!string.IsNullOrEmpty(Request.Form["Sessions"]))
                {
                    int.TryParse(Request.Form["Sessions"], out idSess);
                }
                else if (Request.Form["Sessions"] == null)
                {
                    idSess = db.Session.Max(s => s.id_Sess);
                }
                if (Request.UrlReferrer != null)
                {
                    if (Request.UrlReferrer.AbsolutePath.Contains("/Enseignant/Edit/"))
                    {
                        idEns = (int)id;
                    }
                    else if (Request.UrlReferrer.AbsolutePath.Contains("/Groupes/Index"))
                    {
                        if (sDisabled() != "disabled")
                        {
                            int.TryParse(Request.Form["Enseignants"], out idEns);
                        }
                    }
                }
                int.TryParse(Request["Cours"], out idCours);
            }
            groupes = from d in db.Groupe
                        where d.id_Cours == (idCours == 0 ? d.id_Cours : idCours) && d.id_Enseignant == (idEns == 0 ? d.id_Enseignant : idEns) && d.id_Sess == (idSess == 0 ? d.id_Sess : idSess)
                        orderby d.Session.p_Saison.Saison, d.Session.Annee, d.Cours.Code, d.Cours.Nom, d.NoGroupe
                        select d;
            //on enregistre la recherche
            Session["DernRechCours"] = idSess + ";" + idEns + ";" + idCours;
            Session["DernRechCoursUrl"] = Request.Url?.LocalPath;

            ViewBag.Sessions = new SelectList(db.Session.OrderByDescending(s => s.id_Sess), "id_Sess", "NomSession", idSess);
            ViewBag.Enseignants = new SelectList((verif ? ObtenirListeEnseignant(idSess): db.Personne.AsNoTracking().Where(e => e.id_TypeUsag == 2 && e.id_Pers == idPers)
                                                    .Select(e => new { e.id_Pers, NomPrenom = e.Nom + ", " + e.Prenom }).AsEnumerable()), "id_Pers", "NomPrenom", idEns);
            ViewBag.Cours = new SelectList(ObtenirListeCours(idSess,idEns), "id_Cours", "CodeNom", idCours);


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

        [ValidationAccesEnseignant]
        public ActionResult AjouterEleve(int idg, int? page)
        {
            ViewBag.idg = idg;
            Groupe groupe = db.Groupe.Find(idg);
            IEnumerable<Personne> personnes = RechercherEleve();
            var lstEtu = personnes.Join(db.EtuProgEtude, p => p.id_Pers, epe => epe.id_Etu, (p, epe) => new PersEtuProg(p,epe)).OrderBy(x=>x.p.Nom);
            var pageNumber = page ?? 1;
            ViewBag.page = pageNumber;
            TempData["idg"] = idg;
            return View(lstEtu.ToPagedList(pageNumber, 20));
        }

        [HttpGet]
        [ValidationAccesEnseignant]
        public ActionResult AjouterEleveGET(int idg, int idp,int noclick = 0)
        {
            Groupe g = db.Groupe.Find(idg);
            Personne p = db.Personne.Find(idp);

            if (g == null || p == null)
            {
                return HttpNotFound();
            }

            if (db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).Where(x => x.id_Groupe == idg).FirstOrDefault() != null)
            {
                TempData["ErrorAjoutEleve"] = Messages.I_023(db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).Where(x => x.id_Groupe == idg).FirstOrDefault().Personne.Matricule7);
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

        [ValidationAccesEnseignant]
        public ActionResult DeleteEleve(int? id)
        {
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
        [ValidationAccesEnseignant]
        public ActionResult DeleteEleveConfirmed(int id)
        {
            GroupeEtudiant ge = db.GroupeEtudiant.Find(id);

            TempData["Success"] = string.Format(Messages.I_022(ge.Personne.Matricule7, ge.Groupe.NoGroupe));
            db.GroupeEtudiant.Remove(ge);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [ValidationAccesEnseignant]
        public ActionResult Deplacer(int? id)
        {
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
                ViewBag.id_groupedepl = new SelectList(db.Groupe, "id_Groupe", "CodeNomGroupe");
            else
                ViewBag.id_groupedepl = new SelectList(db.Groupe, "id_Groupe", "CodeNomGroupe", TempData["idgcible"]);
            return View(ge);
        }

        [HttpPost, ActionName("Deplacer")]
        [ValidateAntiForgeryToken]
        [ValidationAccesEnseignant]
        public ActionResult DeplacerConfirmed(int? id)
        {
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
 
        private void Valider([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            if (db.Groupe.Any(r => r.NoGroupe == groupe.NoGroupe && r.id_Sess == groupe.id_Sess && r.id_Cours == groupe.id_Cours))
            {
                ModelState.AddModelError(string.Empty, Messages.I_021(groupe.NoGroupe)); 
            }
        }

    }
}
