using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using sachem.Models;
using System.Net;
using static sachem.Classes_Sachem.ValidationAcces;
using System.Data.Entity;

namespace sachem.Controllers
{
    public class RechercheInscriptionController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();

        const int BROUILLON = 1;
        const int ACCEPTE = 3;
        const int REFUSE = 5;

        [ValidationAccesSuper]
        public ActionResult Index()
        {
            var touteInscription = from inscription in db.Inscription
                                   where inscription.id_Statut != BROUILLON
                                   select inscription;

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

        [HttpPost]
        public ActionResult Edit(int id_Inscription, int id_Statut)
        {
            var inscription = db.Inscription.FirstOrDefault(x => x.id_Inscription == id_Inscription);
            if(inscription != null && inscription.id_Sess == db.Session.Max(s => s.id_Sess))
            {
                inscription.id_Statut = id_Statut;
                db.Entry(inscription).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = "Inscription modifiée avec succès!";
                return RedirectToAction("Details","RechercheInscription",new { id = id_Inscription });
            }
            TempData["Erreur"] = "Erreur lors de la modification de l'inscription. N'oubliez pas qu'il est impossible de modifier l'inscription des anciennes sessions.";
            return RedirectToAction("Details", "RechercheInscription", new { id = id_Inscription });
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
            var lStatut = from statut in db.p_StatutInscription where statut.id_Statut != BROUILLON select statut;
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

            if (Request.RequestType == "GET" && Session["DernRechInsc"] != null && (string)Session["DernRechInscUrl"] == Request.Url?.LocalPath)
            {
                var anciennerech = Session["DernRechInsc"].ToString();
                var tanciennerech = anciennerech.Split(';');

                if (tanciennerech[0] != "")
                {
                    sess = int.Parse(tanciennerech[0]);
                }
                if (tanciennerech[1] != "")
                {
                    type = int.Parse(tanciennerech[1]);
                }
                if (tanciennerech[2] != "")
                {
                    statut = int.Parse(tanciennerech[2]);
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
                              where ((c.id_Sess == sess || sess == 0) && (c.id_Statut == statut || statut == 0) && (c.id_TypeInscription == type || type == 0))
                        select c;

            Session["DernRechInsc"] = sess + ";" + type + ";" + statut;
            Session["DernRechInscUrl"] = Request.Url?.LocalPath;

            return inscription.ToList();
        }

        private void RemplirDropList(Inscription inscription)
        {
            var lStatut = from statut in db.p_StatutInscription where statut.id_Statut == ACCEPTE || statut.id_Statut == REFUSE select statut;
            int vraiStatut = inscription.id_Statut;
            if(vraiStatut != REFUSE)
            {
                vraiStatut = ACCEPTE;
            }
            ViewBag.Liste_Statut = new SelectList(lStatut, "id_Statut", "Statut", vraiStatut);
        }
    }
}
