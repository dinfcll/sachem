using sachem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sachem.Controllers
{
    public class RechercheEtudiantController : Controller
    {
        protected readonly SACHEMEntities db = new SACHEMEntities();

        protected int noPage = 1;
        private int? pageRecue = null;

        #region ObtentionRecherche
        //viewbag
        [NonAction]
        //liste des sessions disponibles en ordre d'année
        private void ListeSession(int Session = 0)
        {
            var lSessions = db.Session.AsNoTracking().OrderBy(s => s.Annee).ThenBy(s => s.p_Saison.Saison);
            var slSession = new List<SelectListItem>();
            slSession.AddRange(new SelectList(lSessions, "id_Sess", "NomSession", Session));
            ViewBag.SelectSession = slSession;

        }
        //fonctions permettant d'obtenir la liste des cours. Appelé pour l'initialisation et la maj de la liste déroulante Cours
        [NonAction]
        protected IEnumerable<Cours> ObtenirListeCours(int session)
        {
            int Pers = 0;
            //if (SACHEMIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Enseignant)
            //{
            //    Pers = (int)Session["id_Pers"];
            //}
            var ResultReq = db.Cours.AsNoTracking().Where(c => c.Groupe.Any(g => (g.id_Enseignant == Pers || Pers == 0) && (g.id_Sess == session)));
           
            return ResultReq.AsEnumerable();
        }

        //fonctions permettant d'obtenir la liste des groupe. Appelé pour l'initialisation et la maj de la liste déroulante Groupe
        [NonAction]
        private IEnumerable<Groupe> ObtenirListeGroupe(int cours, int session)
        {

            int Pers = 0;
            //if (SACHEMIdentite.ObtenirTypeUsager(Session) == TypeUsagers.Enseignant)
            //{
            //    Pers = (int)Session["id_Pers"];
            //}

            return db.Groupe.AsNoTracking().Where(p => (p.id_Enseignant == Pers || Pers == 0) && (p.id_Sess == session) && (p.id_Cours == cours));
        }


        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListeCours(int Cours, int session)
        {
            ViewBag.SelectCours = new SelectList(ObtenirListeCours(session).AsQueryable(), "id_Cours", "CodeNom", Cours);
        }



        //fonctions permettant d'initialiser les listes déroulantes
        [NonAction]
        private void ListeGroupe(int Cours, int session, int Groupe)
        {
            ViewBag.SelectGroupe = new SelectList(ObtenirListeGroupe(Cours, session), "id_Groupe", "NoGroupe", Groupe);
        }

        #region Fonctions Ajax

        /// <summary>
        /// Actualise le dropdownlist des groupes selon l'élément sélectionné dans les dropdownlist Session et Cours
        /// </summary>
        [AcceptVerbs("Get", "Post")]
        public JsonResult ActualiseGroupeddl(int cours, int session)
        {
            var a = ObtenirListeGroupe(cours, session).Select(c => new { c.id_Groupe, c.NoGroupe });
            return Json(a.ToList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Actualise le dropdownlist des cours selon l'élément sélectionné dans le dropdownlist Session
        /// </summary>
        [AcceptVerbs("Get", "Post")]
        public virtual JsonResult ActualiseCoursddl(int session = 0)
        {
            var a = ObtenirListeCours(session).Select(c => new { c.id_Cours, c.CodeNom });
            return Json(a.ToList(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        //Fonction pour gérer la recherche, elle est utilisée dans la suppression et dans l'index
        [NonAction]
        protected IEnumerable<PersonneProgEtu> Rechercher()
        {
            var matricule = "";
            var session = 0;
            var cours = 0;
            var groupe = 0;
            var champsRenseignes = 0;
            IEnumerable<PersonneProgEtu> lstEtu = new List<PersonneProgEtu>();

            //Pour accéder à la valeur de cle envoyée en GET dans le formulaire
            //Request.QueryString["cle"]
            //Pour accéder à la valeur cle envoyée en POST dans le formulaire
            //Request.Form["cle"]
            //Cette méthode fonctionnera dans les 2 cas
            //Request["cle"]

            if (Request.RequestType == "GET" && Session["DernRechEtu"] != null && (string)Session["DernRechEtuUrl"] == Request.Url?.LocalPath)
            {
                var anciennerech = (string)Session["DernRechEtu"];
                var tanciennerech = anciennerech.Split(';');

                if (tanciennerech[0] != "")
                {
                    matricule = tanciennerech[0];
                    ViewBag.Matricule = matricule;
                }
                else
                {
                    if (tanciennerech[1] != "")
                    {
                        session = Int32.Parse(tanciennerech[1]);
                        ViewBag.Session = session;
                        champsRenseignes++;
                    }
                    if (tanciennerech[2] != "")
                    {
                        cours = Int32.Parse(tanciennerech[2]);
                        ViewBag.Cours = cours;
                        champsRenseignes++;
                    }
                    if (tanciennerech[3] != "")
                    {
                        groupe = Int32.Parse(tanciennerech[3]);
                        ViewBag.Groupe = groupe;
                        champsRenseignes++;
                    }

                }
                if (tanciennerech[4] != "")
                {
                    noPage = Int32.Parse(tanciennerech[4]);
                }
            }
            else
            {
                //La méthode String.IsNullOrEmpty permet à la fois de vérifier si la chaine est NULL (lors du premier affichage de la page ou vide, lorsque le paramètre n'est pas appliqué 
                if (!String.IsNullOrEmpty(Request.Form["Matricule"]))
                {
                    matricule = Request.Form["Matricule"];
                    ViewBag.Matricule = matricule;
                }
                else if (!String.IsNullOrEmpty(Request.Params["Matricule"]))
                {
                    matricule = Request.Params["Matricule"];
                    ViewBag.Matricule = matricule;
                }
                else
                {  //si la recherche n'est pas effectuée sur le matricule, obtenir les autres champs

                    if (!String.IsNullOrEmpty(Request.Form["SelectCours"]))
                    {
                        cours = Convert.ToInt32(Request.Form["SelectCours"]);
                        ViewBag.Cours = cours;
                        champsRenseignes++;
                    }
                    else if (!String.IsNullOrEmpty(Request.Params["Cours"]))
                    {
                        cours = Convert.ToInt32(Request.Params["Cours"]);
                        ViewBag.Cours = cours;
                        champsRenseignes++;
                    }
                    if (!String.IsNullOrEmpty(Request.Form["SelectGroupe"]))
                    {
                        groupe = Convert.ToInt32(Request.Form["SelectGroupe"]);
                        ViewBag.Groupe = groupe;
                        champsRenseignes++;
                    }
                    else if (!String.IsNullOrEmpty(Request.Params["Groupe"]))
                    {
                        groupe = Convert.ToInt32(Request.Params["Groupe"]);
                        ViewBag.Groupe = groupe;
                        champsRenseignes++;
                    }
                    if (!String.IsNullOrEmpty(Request.Form["SelectSession"]))
                    {
                        session = Convert.ToInt32(Request.Form["SelectSession"]);
                        ViewBag.Session = session;
                        champsRenseignes++;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Request.Params["Session"]))
                        {
                            session = Convert.ToInt32(Request.Params["Session"]);
                            ViewBag.Session = session;
                            champsRenseignes++;
                        }
                        else if (Request.Form["Session"] == null)
                            session = db.Session.Max(s => s.id_Sess);

                    }

                }
            }

            //si un des champs de recherche est absent
            if (champsRenseignes != 3 && champsRenseignes != 0)
            {
                ModelState.AddModelError(string.Empty, Messages.I_039());
            }

            ListeSession(session);
            ListeCours(cours, session);
            ListeGroupe(cours, session, groupe);

            //on enregistre la recherche
            Session["DernRechEtu"] = matricule + ";" + session + ";" + cours + ";" + groupe + ";" + noPage;
            Session["DernRechEtuUrl"] = Request.Url.LocalPath.ToString();

            if (ModelState.IsValid)
            {
                /*désactiver le lazyloading dans le contexte de cette unité de traitement
                    on désactive le lazyloading car on veut charger manuellement les entités enfants (puisqu'elles ne doivent pas toutes être chargées)
                    */
                db.Configuration.LazyLoadingEnabled = false;

                if (matricule == "") //recherche avec les trois champs. Pas besoin de préciser le cours étant donné que le id_Groupe est associé à un id_Cours
                {
                    /*requête LINQ qui va chercher tous les étudiants répondant aux critères de recherche ainsi que leur programme d'étude actuel. */
                    lstEtu = from q in
                            (from p in db.Personne.Where(x => x.Actif == true && x.GroupeEtudiant.Any(y => y.id_Groupe == groupe) && x.EtuProgEtude.Any(y => y.id_Sess == session)).OrderBy(x => x.Nom)
                             select new
                             {
                                 Personne = p,
                                 ProgEtu = (from pe in db.EtuProgEtude where p.id_Pers == pe.id_Etu orderby pe.id_Sess descending select pe).FirstOrDefault().ProgrammeEtude,
                             }).AsEnumerable()
                             orderby q.Personne.Nom, q.Personne.Prenom
                             // le résultat de la requête sera une liste de PersonneProgEtu (déclaré plus haut),
                             // si l'objet n'est pas déclaré, la vue dynamique n'est pas capable d'évaluer correctement
                             select new PersonneProgEtu { personne = q.Personne, progEtuActif = q.ProgEtu };
                }
                else
                {
                    //recherche sur le matricule
                    lstEtu = from q in
                            (from p in db.Personne.Where(x => x.Matricule.Substring(2).StartsWith(matricule)).OrderBy(x => x.Nom).OrderBy(x => x.Nom)
                             select new
                             {
                                 Personne = p,
                                 ProgEtu = (from pe in db.EtuProgEtude where p.id_Pers == pe.id_Etu orderby pe.id_Sess descending select pe).FirstOrDefault().ProgrammeEtude,
                             }).AsEnumerable()
                             orderby q.Personne.Nom, q.Personne.Prenom
                             // le résultat de la requête sera une liste de PersonneProgEtu (déclaré plus haut),
                             // si l'objet n'est pas déclaré, la vue dynamique n'est pas capable d'évaluer correctement
                             select new PersonneProgEtu { personne = q.Personne, progEtuActif = q.ProgEtu };
                }

                db.Configuration.LazyLoadingEnabled = true;
            }

            /*var personne = from c in db.Personne where c.Actif == true && c.id_TypeUsag == 1 select c;
            foreach (var pers in personne)
            {
                var pidEtu = (from p in db.EtuProgEtude where pers.id_Pers == p.id_Etu orderby p.id_Sess descending select p).FirstOrDefault();
                var pEtu = db.ProgrammeEtude.Find(pidEtu.id_ProgEtu);
                pers.ProgEtu = pEtu.NomProg.ToString();

            }*/


            return lstEtu;
        }

        protected IEnumerable<PersonneProgEtu> Rechercher(int? Page)
        {
            pageRecue = Page;
            return Rechercher();
        }

        #endregion
    }
}