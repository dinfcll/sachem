using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;
using System.Net;
using System.Data.Entity;
using sachem.Classes_Sachem;

namespace sachem.Controllers
{
    public class RechercheInscriptionController : Controller
    {
        private readonly SACHEMEntities _db = new SACHEMEntities();

        private const int Accepte = 3;
        private const int Refuse = 5;

        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Index()
        {
            return View(Rechercher());
        }

        [ValidationAcces.ValidationAccesSuper]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Inscription inscriptionPersonne = _db.Inscription.Find(id);
            if(inscriptionPersonne == null)
            {
                return HttpNotFound();
            }

            var inscription = _db.Inscription.Where(x => x.id_Pers == inscriptionPersonne.id_Pers).ToList();
            RemplirDropList(inscription.First());
            return View(inscription);
        }

        [HttpPut]
        public ActionResult ChangerStatutInscription(int idInscription, int idStatut)
        {
            var inscription = _db.Inscription.FirstOrDefault(x => x.id_Inscription == idInscription);
            if(inscription != null && inscription.id_Sess == _db.Session.Max(s => s.id_Sess))
            {
                inscription.id_Statut = idStatut;
                _db.Entry(inscription).State = EntityState.Modified;
                _db.SaveChanges();

                TempData["Success"] = "Inscription modifiée avec succès!";
                return RedirectToAction("Details","RechercheInscription",new { id = idInscription });
            }
            TempData["Erreur"] = Messages.ErreurModificationInscription();
            return RedirectToAction("Details", "RechercheInscription", new { id = idInscription });
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
                    sess = _db.Session.Max(s => s.id_Sess);

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

            var inscription = from c in _db.Inscription
                              where ((c.id_Sess == sess || sess == 0) && (c.id_Statut == statut || statut == 0) && (c.id_TypeInscription == type || type == 0))
                        select c;

            Session["DernRechInsc"] = sess + ";" + type + ";" + statut;
            Session["DernRechInscUrl"] = Request.Url?.LocalPath;

            return inscription.ToList();
        }

        private void RemplirDropList(Inscription inscription)
        {
            var lStatut = from statut in _db.p_StatutInscription where statut.id_Statut == Accepte || statut.id_Statut == Refuse select statut;
            var vraiStatut = inscription.id_Statut;

            if(vraiStatut != Refuse)
            {
                vraiStatut = Accepte;
            }

            ViewBag.Liste_Statut = new SelectList(lStatut, "id_Statut", "Statut", vraiStatut);
        }
    }
}

