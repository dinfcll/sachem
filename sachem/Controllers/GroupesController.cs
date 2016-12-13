using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using sachem.Models;
using sachem.Classes_Sachem;

namespace sachem.Controllers
{
    public class GroupesController : Controller
    {
        private readonly SACHEMEntities _db = new SACHEMEntities();

        private IEnumerable<object> ObtenirListeEnseignant(int session)
        {
            return _db.Personne
                .AsNoTracking()
                .Join(_db.Groupe, p => p.id_Pers, g => g.id_Enseignant, (p, g) => new { Personne = p, Groupe = g })
                .Where(sel => sel.Personne.id_TypeUsag == 2 && (sel.Groupe.id_Sess == session || session == 0))
                .OrderBy(x => x.Personne.Prenom).ThenBy(x => x.Personne.Nom)
                .Select(e => new { e.Personne.id_Pers, NomPrenom = e.Personne.Nom + ", " + e.Personne.Prenom })
                .Distinct().AsEnumerable();
        }

        private IEnumerable<object> ObtenirListeCours(int session,int enseignant)
        {
            return _db.Cours
                .Join(_db.Groupe, c => c.id_Cours, g => g.id_Cours, (c, g) => new { Cours = c, Groupe = g })
                .Where(sel => (sel.Groupe.id_Sess == session || session == 0) && (sel.Groupe.id_Enseignant == enseignant || enseignant == 0))
                .OrderBy(x => x.Cours.Nom).ThenBy(x=>x.Cours.Code).Select(c=> new { c.Cours.id_Cours, CodeNom = c.Cours.Code + "-" + c.Cours.Nom })
                .Distinct().AsEnumerable();
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

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Index(int? id,int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.Disabled = SDisabled();

            return View(Rechercher(id).ToPagedList(pageNumber, 20));
        }

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Create(int? idEns)
        {
            int? idPers = (int?) Session["id_Pers"] ?? -1;
            var verif = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
            ViewBag.id_Cours = new SelectList(_db.Cours.Where(x => x.Actif).OrderBy(x => x.Code), "id_Cours", "CodeNom");
            var ens = from c in _db.Personne where c.id_TypeUsag == 2 && (verif || c.id_Pers == (idPers == -1 ? c.id_Pers : idPers)) && c.Actif orderby c.Nom, c.Prenom select c;
            ViewBag.id_Enseignant = new SelectList(ens, "id_Pers", "NomPrenom", idEns);
            ViewBag.id_Sess = new SelectList(_db.Session.OrderByDescending(s => s.id_Sess), "id_Sess", "NomSession");
            ViewBag.Disabled = SDisabled();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            ViewBag.Disabled = SDisabled();
            return CreateEdit(groupe, true);
        }

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Edit(int? id)
        {
            var idPers = (int?) Session["id_Pers"] ?? -1;
            var verif = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var groupe = _db.Groupe.Find(id);
            if (groupe == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Cours = new SelectList(_db.Cours.Where(x => x.Actif).OrderBy(x => x.Code), "id_Cours", "CodeNom", groupe.id_Cours);
            ViewBag.id_Enseignant = new SelectList(_db.Personne.Where(x => x.id_TypeUsag == 2 && x.id_Pers == (idPers == -1 || verif ? x.id_Pers : idPers) && x.Actif).OrderBy(x=>x.Prenom).ThenBy(x=>x.Nom), "id_Pers", "NomPrenom", groupe.id_Enseignant);
            ViewBag.id_Sess = new SelectList(_db.Session.OrderByDescending(s => s.id_Sess), "id_Sess", "NomSession", groupe.id_Sess);
            ViewBag.Disabled = SDisabled();

            return View(groupe);
        }

        private string SDisabled()
        {
            return (SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Enseignant ? "disabled" : "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            ViewBag.Disabled = SDisabled();
            return CreateEdit(groupe);
        }

        [ValidationAcces.ValidationAccesEnseignant]
        private ActionResult CreateEdit([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe, bool ajouter = false)
        {
            groupe.id_Enseignant = groupe.id_Enseignant ?? SessionBag.Current.id_Pers;
            Valider(groupe);

            if (ModelState.IsValid)
            {
                if (!ajouter)
                {
                    _db.Entry(groupe).State = EntityState.Modified;
                }
                else
                {
                    _db.Groupe.Add(groupe);
                }
                _db.SaveChanges();
                TempData["Questions"] = string.Format(Messages.GroupeCreeAssocierEtudiant(groupe.NoGroupe));
                TempData["idg"] = groupe.id_Groupe;
                return RedirectToAction("Index");
            }
            ViewBag.id_Cours = new SelectList(_db.Cours, "id_Cours", "CodeNom", groupe.id_Cours);
            ViewBag.id_Enseignant = new SelectList(_db.Personne, "id_Pers", "NomPrenom", groupe.id_Enseignant);
            ViewBag.id_Sess = new SelectList(_db.Session, "id_Sess", "NomSession", groupe.id_Sess);
            ViewBag.Disabled = SDisabled();
            return View(groupe);
        }

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var groupe = _db.Groupe.Find(id);

            if (_db.GroupeEtudiant.Any(ge => ge.id_Groupe == id))
            {
                ViewBag.Error = Messages.VraimentSupprimerGroupeAvecEtudiantsRattaches(groupe.NoGroupe);
            }

            if (groupe == null)
            {
                return HttpNotFound();
            }
            return View(groupe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult DeleteConfirmed(int id)
        {
            var groupe = _db.Groupe.Find(id);

            if (ModelState.IsValid)
            {
                foreach (var x in (from c in _db.GroupeEtudiant where c.id_Groupe == groupe.id_Groupe select c))
                {
                    _db.GroupeEtudiant.Remove(x);
                }
                _db.Groupe.Remove(groupe);
                _db.SaveChanges();
                TempData["Success"] = string.Format(Messages.GroupeSupprime(groupe.NoGroupe));
            }

            return RedirectToAction("Index");
        }

        private IEnumerable<Groupe> Rechercher(int? id)
        {
            var idPers = (int?) Session["id_Pers"] ?? -1;
            var verif = SachemIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Responsable;
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
                    idSess = _db.Session.Max(s => s.id_Sess);
                }
                if (Request.UrlReferrer != null)
                {
                    if (Request.UrlReferrer.AbsolutePath.Contains("/Enseignant/Edit/"))
                    {
                        if (id != null) idEns = (int)id;
                    }
                    else if (Request.UrlReferrer.AbsolutePath.Contains("/Groupes/Index"))
                    {
                        if (SDisabled() != "disabled")
                        {
                            int.TryParse(Request.Form["Enseignants"], out idEns);
                        }
                    }
                }
                int.TryParse(Request["Cours"], out idCours);
            }
            IQueryable<Groupe> groupes = from d in _db.Groupe
                where d.id_Cours == (idCours == 0 ? d.id_Cours : idCours) && d.id_Enseignant == (idEns == 0 ? d.id_Enseignant : idEns) && d.id_Sess == (idSess == 0 ? d.id_Sess : idSess)
                orderby d.Session.p_Saison.Saison, d.Session.Annee, d.Cours.Code, d.Cours.Nom, d.NoGroupe
                select d;

            Session["DernRechCours"] = idSess + ";" + idEns + ";" + idCours;
            Session["DernRechCoursUrl"] = Request.Url?.LocalPath;

            ViewBag.Sessions = new SelectList(_db.Session.OrderByDescending(s => s.id_Sess), "id_Sess", "NomSession", idSess);
            ViewBag.Enseignants = new SelectList((verif ? ObtenirListeEnseignant(idSess): _db.Personne.AsNoTracking().Where(e => e.id_TypeUsag == 2 && e.id_Pers == idPers)
                                                    .Select(e => new { e.id_Pers, NomPrenom = e.Nom + ", " + e.Prenom }).AsEnumerable()), "id_Pers", "NomPrenom", idEns);
            ViewBag.Cours = new SelectList(ObtenirListeCours(idSess,idEns), "id_Cours", "CodeNom", idCours);


            return groupes.ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult AjouterEleve(int idg, int? page)
        {
            ViewBag.idg = idg;
            var personnes = RechercherEleve();
            var lstEtu = personnes.Join(_db.EtuProgEtude, p => p.id_Pers, epe => epe.id_Etu, (p, epe) => new PersEtuProg(p,epe)).OrderBy(x=>x.p.Nom);
            var pageNumber = page ?? 1;
            ViewBag.page = pageNumber;
            TempData["idg"] = idg;

            return View(lstEtu.ToPagedList(pageNumber, 20));
        }

        [HttpGet]
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult AjouterEleveGet(int idg, int idp,int noclick = 0)
        {
            var g = _db.Groupe.Find(idg);
            var p = _db.Personne.Find(idp);

            if (g == null || p == null)
            {
                return HttpNotFound();
            }

            if (_db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).FirstOrDefault(x => x.id_Groupe == idg) != null)
            {
                var firstOrDefault = _db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).FirstOrDefault(x => x.id_Groupe == idg);

                if (firstOrDefault != null)
                    TempData["ErrorAjoutEleve"] = Messages.EtudiantAjouteDeuxFoisAuGroupe(firstOrDefault.Personne.Matricule7);

                var groupeEtudiant = _db.GroupeEtudiant.Where(x => x.id_Etudiant == idp).FirstOrDefault(x => x.id_Groupe == idg);

                if (groupeEtudiant != null)
                    ModelState.AddModelError(string.Empty, Messages.EtudiantAjouteDeuxFoisAuGroupe(groupeEtudiant.Personne.Matricule7));
            }

            if (ModelState.IsValid)
            {

                if (_db.GroupeEtudiant.FirstOrDefault(x => x.id_Etudiant == idp) != null && noclick == 0)
                {
                    var firstOrDefault = _db.GroupeEtudiant.FirstOrDefault(x => x.id_Etudiant == idp);
                    if (firstOrDefault != null)
                        TempData["idGe"] = firstOrDefault.id_GroupeEtudiant;
                    TempData["personne"] = p.id_Pers;
                    TempData["idgcible"] = g.NoGroupe;
                    TempData["ErrorDep"] = Messages.VraimentDeplacerEtudiant(p.PrenomNom);
                    ModelState.AddModelError(string.Empty, Messages.VraimentDeplacerEtudiant(p.PrenomNom));
                }

                if (ModelState.IsValid)
                {
                    var ge = new GroupeEtudiant
                    {
                        Personne = p,
                        Groupe = g
                    };

                    _db.GroupeEtudiant.Add(ge);
                    g.GroupeEtudiant.Add(ge);

                    _db.SaveChanges();
                    TempData["Success"] = string.Format(Messages.EtudiantAjouteAuGroupe(p.Matricule7, g.id_Groupe));
                }
            }
            return RedirectToAction("AjouterEleve", new { idg, ViewBag.page });
        }

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult DeleteEleve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GroupeEtudiant ge = _db.GroupeEtudiant.Find(id);
            if (ge == null)
            {
                return HttpNotFound();
            }
            return View(ge);
        }

        // POST: Groupes/Delete/5
        [HttpPost, ActionName("DeleteEleve")]
        [ValidateAntiForgeryToken]
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult DeleteEleveConfirmed(int id)
        {
            GroupeEtudiant ge = _db.GroupeEtudiant.Find(id);

            TempData["Success"] = string.Format(Messages.EudiantRetireDuGroupe(ge.Personne.Matricule7, ge.Groupe.NoGroupe));
            _db.GroupeEtudiant.Remove(ge);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult Deplacer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ge = _db.GroupeEtudiant.Find(id);

            if (ge == null)
            {
                return HttpNotFound();
            }

            ViewBag.id_groupedepl = TempData["idgcible"] == null
                ? new SelectList(_db.Groupe, "id_Groupe", "CodeNomGroupe")
                : new SelectList(_db.Groupe, "id_Groupe", "CodeNomGroupe", TempData["idgcible"]);

            return View(ge);
        }

        [HttpPost, ActionName("Deplacer")]
        [ValidateAntiForgeryToken]
        [ValidationAcces.ValidationAccesEnseignant]
        public ActionResult DeplacerConfirmed(int? id)
        {
            int idgretu, idg;
            if (!int.TryParse(Request.Form["idGroupeEtudiant"], out idgretu) || !int.TryParse(Request.Form["id_groupedepl"], out idg))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ge = _db.GroupeEtudiant.Find(idgretu);
            var g = _db.Groupe.Find(idg);

            if(ge == null || g == null)
            {
                return HttpNotFound();
            }

            var ge2 = g.GroupeEtudiant.Where(x => x.id_Etudiant == ge.id_Etudiant).ToList().FirstOrDefault();

            if ( ge2 != null && ge.Groupe.id_Sess == ge2.Groupe.id_Sess && ge.id_Etudiant == ge2.id_Etudiant)
            {
                TempData["Success"] = Messages.EtudiantDeplacementDeCoursImpossible(ge.Personne.Matricule7, g.NoGroupe, g.Cours.Nom);
            }
            else 
            {
                ge.Groupe = g;
                _db.SaveChanges();
                TempData["Success"] = Messages.EtudiantDeplacementDeGroupe(ge.Personne.Matricule7, g.NoGroupe, g.Cours.Nom);
            }

            return RedirectToAction("Index");
        }

        private IEnumerable<Personne> RechercherEleve()
        {
            IEnumerable<Personne> personnes;

            var nom = Request.Form["Nom"];
            var prenom = Request.Form["Prenom"];
            var matricule = Request.Form["Matricule"];

            if (string.IsNullOrEmpty(matricule) && string.IsNullOrEmpty(nom) && string.IsNullOrEmpty(prenom))
            {
                personnes = (from c in _db.Personne where c.id_TypeUsag == 1 select c).ToList().OrderBy(x => x.NomPrenom).ThenBy(x => x.Matricule7);
            }
            else
            {
                personnes = _db.Personne.Where(x => x.id_TypeUsag == 1).Where(c => c.Nom.StartsWith(!string.IsNullOrEmpty(nom) ? nom : c.Nom)).Where(c => c.Prenom.StartsWith(!string.IsNullOrEmpty(prenom) ? prenom : c.Prenom));
                personnes = personnes.Where(x => x.Matricule7.StartsWith(!string.IsNullOrEmpty(matricule) ? matricule : x.Matricule7));
            }
            return personnes;
        }
 
        private void Valider([Bind(Include = "id_Groupe,id_Cours,id_Sess,id_Enseignant,NoGroupe")] Groupe groupe)
        {
            if (_db.Groupe.Any(r => r.NoGroupe == groupe.NoGroupe && r.id_Sess == groupe.id_Sess && r.id_Cours == groupe.id_Cours))
            {
                ModelState.AddModelError(string.Empty, Messages.GroupeAyantLeMemeNumero(groupe.NoGroupe)); 
            }
        }

    }
}
