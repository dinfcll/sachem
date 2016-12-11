using System;
using System.Collections.Generic;
using System.Linq;
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

            var inscription = db.Inscription.Where(x => x.id_Pers == inscriptionPersonne.id_Pers).ToList();
            RemplirDropList(inscription.First());
            return View(inscription);
        }

        [HttpPut]
        public ActionResult ChangerStatutInscription(int id_Inscription, int id_Statut)
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

        private IEnumerable<Inscription> Rechercher()
        {
            var sess = 0;
            var type = 0;
            var statut = 0;

            if (Request.RequestType == "GET" && Session["DernRechInsc"] != null && (string)Session["DernRechInscUrl"] == Request.Url?.LocalPath)
            {
                var ancienneRech = Session["DernRechInsc"].ToString();
                var tancienneRech = ancienneRech.Split(';');

                if (tancienneRech[0] != "")
                {
                    sess = int.Parse(tancienneRech[0]);
                }
                if (tancienneRech[1] != "")
                {
                    type = int.Parse(tancienneRech[1]);
                }
                if (tancienneRech[2] != "")
                {
                    statut = int.Parse(tancienneRech[2]);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.Form["Session"]))
                {
                    int.TryParse(Request.Form["Session"], out sess);
                }
                else if (Request.Form["Session"] == null)
                    sess = db.Session.Max(s => s.id_Sess);

                if (!string.IsNullOrEmpty(Request.Form["TypeInscription"]))
                {
                    int.TryParse(Request.Form["TypeInscription"], out type);
                }

                if (!string.IsNullOrEmpty(Request.Form["Statut"]))
                {
                    int.TryParse(Request.Form["Statut"], out statut);
                }
            }

            ViewBag.Session = Liste.ListeSession(sess);
            ViewBag.TypeInscription = Liste.ListeTypeInscription(type);
            ViewBag.Statut = Liste.ListeStatutInscriptionSansBrouillon(statut);

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

