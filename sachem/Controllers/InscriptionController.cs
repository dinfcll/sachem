using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sachem.Classes_Sachem;
using sachem.Models;
using System.Text.RegularExpressions;

namespace sachem.Controllers
{
   
    public class InscriptionController : Controller
    {
        private readonly SACHEMEntities db = new SACHEMEntities();
        private readonly string msg_Erreur_Consecutif = "Erreur: vous devez avoir une plage horaire contenant 2 heures consécutives.";
        private string[] m_Session = { "Hiver", "Printemps", "Été", "Automne" };
        //[ValidationAcces.ValidationAccesInscription]
        // GET: Inscription
        public ActionResult Index()
        {
            ViewBag.TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            return View();
        }

        [HttpPost]
        public ActionResult Index(string[] values)
        {
            ViewBag.TypeInscription = new SelectList(db.p_TypeInscription, "id_TypeInscription", "TypeInscription");
            if (values != null)
            {
                int longueurTab = values.Length;
                if (longueurTab % 2 != 0)
                {
                    return this.Json(new { success = false, message = "Utilisez un nombre pair d'heures." });
        }
                int[] heures = new int[longueurTab];
                string[] splitValue1, splitValue2;
                Array.Sort(values, new AlphanumComparatorFast());
                for (int i = 0; i < values.Length - 1; i+=2)
        {
                    splitValue1 = values[i].Split('-');
                    splitValue2 = values[i + 1].Split('-');
                    if (!(int.Parse(splitValue1[1]) +1 == int.Parse(splitValue2[1])))
            {
                        return this.Json(new { success = false, message = "Utilisez au moins deux heures consécutives!" });
                    }
                }
                return this.Json(new { success = true, message = values });

            }
            else
            {
                return this.Json(new { success = false, message = "Veuillez remplir le formulaire de disponibilités." });
            }
        }


        // GET: Inscription/Delete/5
        //NOTE: Penser à Wiper les inscriptions à chaque fin de session. Constante?
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inscription/Delete/5
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
        public ActionResult EleveAide1()
        {
            listeCours();
            listeStatutCours();
            listeSession();
            return View();
            //Ajouter session + etat du cours dropdown, enlever liste collège, terminer la première et la deuxième page en priorité
        }
        [NonAction]
        private int? JourANumero(string jour)
        {
            switch (jour)
            {
                case "Lundi":
                    return 2;
                case "Mardi":
                    return 3;
                case "Mercredi":
                    return 4;
                case "Jeudi":
                    return 5;
                case "Vendredi":
                    return 6;
                default:
                    return null;

            }
        }

        [NonAction]
        private string[] triageTableauAlphaNumerique(string[] tableau)
        {
            Array.Sort(tableau, StringComparer.InvariantCulture);
            return tableau;
        }
        public ActionResult Tuteur()
        {
            listeCours();
            listeCollege();
            return View();
        }
        public ActionResult TBenevole()
        {
            listeCours();
            listeCollege();
            return View();
        }
        [NonAction]
        public void listeCours()
        {
            var lstCrs = from c in db.Cours orderby c.Nom select c;
            var slCrs = new List<SelectListItem>();
            slCrs.AddRange(new SelectList(lstCrs, "id_Cours","CodeNom"));
            ViewBag.lstCours = slCrs;
            ViewBag.lstCours1 = slCrs;
        }
        [NonAction]
        public void listeCollege()
        {
            var lstCol = from c in db.p_College orderby c.College select c;
            var slCol = new List<SelectListItem>();
            slCol.AddRange(new SelectList(lstCol, "id_College", "College"));
            ViewBag.lstCollege = slCol;
        }
        [NonAction]
        public void listeStatutCours()
        {
            var lstStatut = from c in db.p_StatutCours orderby c.id_Statut select c;
            var slStatut = new List<SelectListItem>();
            slStatut.AddRange(new SelectList(lstStatut, "id_Statut", "Statut"));
            ViewBag.lstStatut = slStatut;
        }
        [NonAction]
        public void listeSession()
        {
            List<string> listeSession = new List<string>();
            var lstSess = from c in db.Session orderby c.id_Sess select c;
            foreach( var session in lstSess)
            {
                string NomSession = m_Session[session.id_Saison]+" "+ session.Annee.ToString();
                listeSession.Add(NomSession);             
            }
            var slSess = new List<SelectListItem>();
            slSess.AddRange(new SelectList(listeSession));
            ViewBag.lstSess = slSess;
        }
        [HttpPost]
        public ActionResult getLigneCours()
        {
            listeCours();
            listeCollege();
            return PartialView("_LigneCoursReussi");
        }
        [HttpPost]
        public ActionResult getLigneCoursEleveAide()
        {
            listeCours();
            listeStatutCours();
            listeSession();
            return PartialView("_LigneCoursReussiEleveAide");
        }
        [HttpPost]
        public void Poursuivre()
        {
        }
        [HttpPost]
        public string ErreurCours()
        {
            return Messages.I_048();
        }
    }
}
