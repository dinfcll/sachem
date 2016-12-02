using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using System.Net;
using static sachem.Classes_Sachem.ValidationAcces;

namespace sachem.Controllers
{
    public class RechercheInscriptionController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();

        const int BROUILLON = 1;
        [ValidationAccesSuper]
        public ActionResult Index()
        {
            var touteInscription = from inscription in db.Inscription
                                   where inscription.id_Statut != BROUILLON
                                   select inscription;
            ListeStatut();
            ListeTypeInscription();

            return View(Rechercher());
        }

        [ValidationAccesSuper]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Inscription inscriptionPersonne = db.Inscription.Find(id);
            if(inscriptionPersonne == null)
            {
                return HttpNotFound();
            }

            List<Inscription> inscription = db.Inscription.Where(x => x.id_Pers == inscriptionPersonne.id_Pers).ToList();
            RemplirDropList(inscription.First());
            return View(inscription);
        }

        // GET: RechercheInscription/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RechercheInscription/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RechercheInscription/Edit/5
        public ActionResult Edit(int? id)
        {
            return View();
        }

        // POST: RechercheInscription/Edit/5
        [HttpPost]
        public ActionResult Edit(List<Inscription> inscription)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RechercheInscription/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RechercheInscription/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        private void ListeSession(int Session = 0)
        {
            var lSessions = from session in db.Session select session;
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", Session));

            ViewBag.Session = slSession;
        }

        [NonAction]
        private void ListeTypeInscription(int TypeInscription = 0)
        {
            var lTypeInscription = from typeinscription in db.p_TypeInscription select typeinscription;
            var slTypeInscription = new List<SelectListItem>();
            slTypeInscription.AddRange(new SelectList(lTypeInscription, "id_TypeInscription", "TypeInscription", TypeInscription));

            ViewBag.TypeInscription = slTypeInscription;
        }

        [NonAction]
        private void ListeStatut(int Statut = 0)
        {
            var lStatut = from statut in db.p_StatutInscription select statut;
            var slStatut = new List<SelectListItem>();
            slStatut.AddRange(new SelectList(lStatut, "id_Statut", "Statut", Statut));

            ViewBag.Statut = slStatut;
        }

        [NonAction]
        private IEnumerable<Inscription> Rechercher()
        {
            var sess = 0;
            var type = 0;
            var statut = 0;

            if (Request.RequestType == "GET" && Session["DernRechCours"] != null && (string)Session["DernRechCoursUrl"] == Request.Url?.LocalPath)
            {
                var anciennerech = (string)Session["DernRechCours"];
                var tanciennerech = anciennerech.Split(';');

                if (tanciennerech[0] != "")
                {
                    sess = int.Parse(tanciennerech[0]);
                }
                if (tanciennerech[1] != "")
                {
                    type = int.Parse(tanciennerech[0]);
                }
                if (tanciennerech[2] != "")
                {
                    statut = int.Parse(tanciennerech[0]);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.Form["Session"]))
                    int.TryParse(Request.Form["Session"], out sess);
                else if (Request.Form["Session"] == null)
                    sess = db.Session.Max(s => s.id_Sess);

                if (!string.IsNullOrEmpty(Request.Form["TypeInscription"]))
                    int.TryParse(Request.Form["TypeInscription"], out type);

                if (!string.IsNullOrEmpty(Request.Form["Statut"]))
                    int.TryParse(Request.Form["Statut"], out statut);
            }


            ListeSession(sess);
            ListeTypeInscription(type);
            ListeStatut(statut);


            var inscription = from c in db.Inscription
                              where ((c.id_Sess == sess || sess == 0) && (c.id_Statut == type || type == 0) && (c.id_TypeInscription == statut || statut == 0))
                        select c;

            Session["DernRechCours"] = sess;
            Session["DernRechCoursUrl"] = Request.Url?.LocalPath;

            return inscription.ToList();
        }

        private void RemplirDropList(Inscription inscription)
        {

            ViewBag.Liste_Statut = new SelectList(db.p_StatutInscription, "id_Statut", "Statut", inscription.id_Inscription);
        }
    }
}
