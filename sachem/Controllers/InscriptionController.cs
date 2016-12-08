using System;
using System.Data.Entity;
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
        private string[] m_Session = { "Hiver", "Été", "Automne" };
        private string IDREUSSIS = "1";
        private string IDABAN = "2";
        private string IDECHEC = "3";
        private readonly string MESSAGESTATUT = "Un resultat et un statut d'un des cours ne concorde pas. Un cours réussi doit avoir une note supérieur ou égal à 60 et un échec inférieur à 60.";
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

        [ValidationAcces.ValidationAccesEtu]
        public ActionResult EleveAide1()
        {
            listeCours();
            listeStatutCours();
            listeSession();
            return View();
        }
        [ValidationAcces.ValidationAccesEtu]
        [HttpPost]
        public ActionResult EleveAide1(string[][] values)
        {
            TempData["Echec"] = "";
            if (values==null)
            {
                return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.id_Inscription });
            }
            else
            {
                if (coherenceStatutResultat(values))
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        if (values[i][0] != "")
                        {
                            var cours = new CoursSuivi();
                            cours.id_Pers = SessionBag.Current.id_Pers;
                            cours.id_Cours = Convert.ToInt32(values[i][0]);
                            cours.id_Statut = Convert.ToInt32(values[i][1]);
                            cours.id_Sess = trouverSession(values[i][2]);
                            cours.id_College = db.p_College.FirstOrDefault(x => x.College == "Cégep de Lévis-Lauzon").id_College;
                            if (values[i][3] != "")
                            {
                                cours.resultat = Convert.ToInt32(values[i][3]);
                            }
                            db.CoursSuivi.Add(cours);
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Details", "DossierEtudiant", new { id = SessionBag.Current.id_Inscription });
                }
                else
                {
                    TempData["Echec"]= MESSAGESTATUT;
                    return View("EleveAide1");
                }
            }
            
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
        private int trouverIdSaison(string saison)
        {
            switch (saison)
            {
                case "Été":
                    return 2;
                case "Automne":
                    return 3;
                default:
                    return 1;
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
            var lstSess = from c in db.Session orderby c.id_Sess select c;
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lstSess, "id_Sess", "NomSession", Session));
            ViewBag.Session = slSession;
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
        public string ErreurCours()
        {
            return Messages.I_048();
        }
        [NonAction]
        public bool coherenceStatutResultat(string[][] values)
        {
            var resultat = 0;
            var retour = true;
            for (var i = 0; i < values.Length; i++)
            {
                if (values[i][0] != "")
                {
                    if (!int.TryParse(values[i][3], out resultat))
                    {
                        if (values[i][1] != IDABAN)
                        {
                            retour = false;
                        }
                    }
                    if (values[i][1] == IDREUSSIS)
                    {
                        if (resultat < 60)
                        {
                            retour = false;
                        }
                    }
                    else
                    {
                        if (values[i][1] == IDECHEC)
                        {
                            if (resultat >= 60)
                            {
                                retour = false;
                            }
                        }
                        else
                        {
                            if (resultat != 0)
                            {
                                retour = false;
                            }
                        }
                    }
                }
            }
            return retour;
        }
        [NonAction]
        public int trouverSession(string session)
        {
            var annee = Convert.ToInt32(session.Substring(session.Length - 5));
            var saison = session.Substring(0, session.Length - 5);
            var idSaison = trouverIdSaison(saison);
            return db.Session.FirstOrDefault(x=>x.Annee==annee && x.id_Saison==idSaison).id_Sess;
        }
    }
}
