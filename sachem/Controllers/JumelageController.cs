using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using PagedList;
using static sachem.Classes_Sachem.ValidationAcces;
using sachem.Models.DataAccess;

namespace sachem.Controllers
{
    public class JumelageController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();
        protected int noPage = 1;
        private int? pageRecue = null;

        private readonly IDataRepository dataRepository;

        public JumelageController()
        {
            dataRepository = new BdRepository();
        }

        public JumelageController(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        [NonAction]
        //liste des sessions disponibles en ordre d'année
        private void ListeSession(int session = 0)
        {
            var lSessions = db.Session.AsNoTracking().OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", session));
            ViewBag.Session = slSession;
        }

        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListeTypeInscription(int TypeInscription = 0)
        {
            var lInscriptions = db.p_TypeInscription.AsNoTracking().OrderBy(i => i.TypeInscription);
            var slInscription = new List<SelectListItem>();
            slInscription.AddRange(new SelectList(lInscriptions, "id_TypeInscription", "TypeInscription", TypeInscription));
            ViewBag.Inscription = slInscription;
        }

        // GET: Jumelage
        [ValidationAccesEnseignant]
        public ActionResult Index(int? page)
        {
            noPage = (page ?? noPage);

            return View(Rechercher().ToPagedList(noPage, 20));
        }

        [NonAction]
        protected IEnumerable<Inscription> Rechercher()
        {
            int session = 0;
            int typeinscription = 0;

            if (Request.RequestType == "GET" && Session["DernRechEtu"] != null && (string)Session["DernRechEtuUrl"] == Request.Url?.LocalPath)
            {
                var anciennerech = (string)Session["DernRechEtu"];
                var tanciennerech = anciennerech.Split(';');

                if (tanciennerech[1] != "")
                {
                    session = Int32.Parse(tanciennerech[1]);
                    ViewBag.Session = session;
                }
                if (tanciennerech[2] != "")
                {
                    typeinscription = Int32.Parse(tanciennerech[2]);
                    ViewBag.Inscription = typeinscription;
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(Request.Form["Inscription"]))
                {
                    typeinscription = Convert.ToInt32(Request.Form["Inscription"]);
                    ViewBag.Inscription = typeinscription;
                }
                else if (!String.IsNullOrEmpty(Request.Params["Inscription"]))
                {
                    typeinscription = Convert.ToInt32(Request.Params["Inscription"]);
                    ViewBag.Inscription = typeinscription;
                }
                if (!String.IsNullOrEmpty(Request.Form["SelectSession"]))
                {
                    session = Convert.ToInt32(Request.Form["SelectSession"]);
                    ViewBag.Session = session;

                }
                else if (!String.IsNullOrEmpty(Request.Params["Session"]))
                {
                    session = Convert.ToInt32(Request.Params["Session"]);
                    ViewBag.Session = session;

                }
                else if (Request.Form["Session"] == null)
                    session = Convert.ToInt32(db.Session.OrderByDescending(y => y.Annee).ThenByDescending(x => x.id_Saison).FirstOrDefault().id_Sess);
            }

            ListeSession(session);
            ListeTypeInscription(typeinscription);

            Session["DernRechEtu"] = session + ";" + typeinscription + ";" + noPage;
            Session["DernRechEtuUrl"] = Request.Url.LocalPath.ToString();

            var lstEtu = from p in db.Inscription
                         where (p.id_Sess == session || session == 0) &&
                         (p.id_TypeInscription == typeinscription || typeinscription == 0)
                         orderby p.Personne.Nom, p.Personne.Prenom
                         select p;

            return lstEtu.ToList();
        }

        [NonAction]
        protected IEnumerable<Inscription> Rechercher(int? Page)
        {
            pageRecue = Page;
            return Rechercher();
        }

        // GET: Jumelage/Details/5
        [ValidationAccesEnseignant]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = db.Inscription.Find(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            return View(inscription);
        }

        //// GET: Jumelage/Create
        //public ActionResult Create()
        //{
        //    ViewBag.id_InscEleve = new SelectList(db.Inscription, "id_Inscription", "NoteSup");
        //    ViewBag.id_InscrTuteur = new SelectList(db.Inscription, "id_Inscription", "NoteSup");
        //    ViewBag.id_Jour = new SelectList(db.p_Jour, "id_Jour", "Jour");
        //    ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom");
        //    ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess");
        //    return View();
        //}

        //// POST: Jumelage/Create
        //// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        //// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id_Jumelage,id_InscEleve,id_InscrTuteur,id_Sess,id_Enseignant,id_Jour,DateDebut,DateFin,HeureDebut,HeureFin")] Jumelage jumelage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Jumelage.Add(jumelage);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.id_InscEleve = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscEleve);
        //    ViewBag.id_InscrTuteur = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscrTuteur);
        //    ViewBag.id_Jour = new SelectList(db.p_Jour, "id_Jour", "Jour", jumelage.id_Jour);
        //    ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", jumelage.id_Enseignant);
        //    ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", jumelage.id_Sess);
        //    return View(jumelage);
        //}

        //// GET: Jumelage/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Jumelage jumelage = db.Jumelage.Find(id);
        //    if (jumelage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.id_InscEleve = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscEleve);
        //    ViewBag.id_InscrTuteur = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscrTuteur);
        //    ViewBag.id_Jour = new SelectList(db.p_Jour, "id_Jour", "Jour", jumelage.id_Jour);
        //    ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", jumelage.id_Enseignant);
        //    ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", jumelage.id_Sess);
        //    return View(jumelage);
        //}

        //// POST: Jumelage/Edit/5
        //// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        //// plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id_Jumelage,id_InscEleve,id_InscrTuteur,id_Sess,id_Enseignant,id_Jour,DateDebut,DateFin,HeureDebut,HeureFin")] Jumelage jumelage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(jumelage).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.id_InscEleve = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscEleve);
        //    ViewBag.id_InscrTuteur = new SelectList(db.Inscription, "id_Inscription", "NoteSup", jumelage.id_InscrTuteur);
        //    ViewBag.id_Jour = new SelectList(db.p_Jour, "id_Jour", "Jour", jumelage.id_Jour);
        //    ViewBag.id_Enseignant = new SelectList(db.Personne, "id_Pers", "Nom", jumelage.id_Enseignant);
        //    ViewBag.id_Sess = new SelectList(db.Session, "id_Sess", "id_Sess", jumelage.id_Sess);
        //    return View(jumelage);
        //}

        //// GET: Jumelage/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Jumelage jumelage = db.Jumelage.Find(id);
        //    if (jumelage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(jumelage);
        //}

        //// POST: Jumelage/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Jumelage jumelage = db.Jumelage.Find(id);
        //    db.Jumelage.Remove(jumelage);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
