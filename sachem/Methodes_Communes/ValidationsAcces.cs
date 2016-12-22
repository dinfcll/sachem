using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Methodes_Communes
{
    public abstract class ValidationAcces
    {
        private static string _pathErreurAuth = "/Home/Error";
        private static string _pathErreurDeja = "/Home/Deja";
        private static string _pathLogin = "/Account/Login";
        private static string _pathErreurFerme = "/Home/Ferme";

        private static readonly List<TypeUsager> RolesAccesSuper = new List<TypeUsager> { TypeUsager.Responsable, TypeUsager.Super };
        private static readonly List<TypeUsager> RolesAccesEnseignant = new List<TypeUsager> { TypeUsager.Enseignant, TypeUsager.Responsable, TypeUsager.Super };
        private static readonly List<TypeUsager> RolesAccesEtu = new List<TypeUsager> { TypeUsager.Responsable, TypeUsager.Super, TypeUsager.Enseignant, TypeUsager.Etudiant };
        private static readonly List<TypeUsager> RolesAccesAucun = new List<TypeUsager> { TypeUsager.Aucun };

        private static readonly List<TypeInscription> InscriptionTuteurs = new List<TypeInscription> { TypeInscription.TuteurCours, TypeInscription.TuteurBenevole, TypeInscription.TuteurRemunere };
        private static readonly List<TypeInscription> InscriptionTuteurBenEtRem = new List<TypeInscription> { TypeInscription.TuteurBenevole, TypeInscription.TuteurRemunere };
        private static readonly List<TypeInscription> InscriptionTuteurCours = new List<TypeInscription> { TypeInscription.TuteurCours };
        private static readonly List<TypeInscription> InscriptionEleveAide = new List<TypeInscription> { TypeInscription.EleveAide, TypeInscription.TuteurCours, TypeInscription.TuteurBenevole, TypeInscription.TuteurRemunere  };

        private static void VerifAcces(List<TypeUsager> listeRoles, ActionExecutingContext filterContext, string redirectTo)
        {
            var verif = SachemIdentite.ValiderRoleAcces(listeRoles, filterContext.HttpContext.Session);
            if (!verif)
                filterContext.Result = new RedirectResult(redirectTo);
        }
        private static void VerifEtudiantAcces(List<TypeInscription> listeInscriptions, List<TypeUsager> listeRoles, ActionExecutingContext filterContext, string redirectTo)
        {
            var verif = SachemIdentite.ValiderRoleAccesSuperieurAEtudiant(listeRoles, filterContext.HttpContext.Session);
            if (verif) return;
            verif = SachemIdentite.ValiderRoleAcces(listeRoles, filterContext.HttpContext.Session);
            if (!verif)
                filterContext.Result = new RedirectResult(redirectTo);
            verif = SachemIdentite.ValiderEtudiantTypeAcces(listeInscriptions, filterContext.HttpContext.Session);
            if (!verif)
                filterContext.Result = new RedirectResult(redirectTo);
        }

        public class ValidationAccesSuperEtResp : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                VerifAcces(RolesAccesSuper, filterContext, _pathErreurAuth);
            }
        }

        public class ValidationAccesEnseignant : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                VerifAcces(RolesAccesEnseignant, filterContext, _pathErreurAuth);
            }
        }

        public class ValidationAccesEtudiants : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                VerifAcces(RolesAccesEtu, filterContext, _pathErreurAuth);
            }
        }

        public class ValidationAccesTousTuteurs : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                VerifEtudiantAcces(InscriptionTuteurs, RolesAccesEtu, filterContext, _pathErreurAuth);
            }
        }

        public class ValidationAccesTuteursBenevoleEtRemunere : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                VerifEtudiantAcces(InscriptionTuteurBenEtRem, RolesAccesEtu, filterContext, _pathErreurAuth);
            }
        }

        public class ValidationAccesTuteurCours : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                VerifEtudiantAcces(InscriptionTuteurCours, RolesAccesEtu, filterContext, _pathErreurAuth);
            }
        }

        public class ValidationAccesEleveAide : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                VerifEtudiantAcces(InscriptionEleveAide, RolesAccesEtu, filterContext, _pathErreurAuth);
            }
        }
        

        public class ValidationAccesInscription : ActionFilterAttribute
        {
            private readonly SACHEMEntities _db = new SACHEMEntities();          

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                int id = BrowserSessionBag.Current.id_Pers;
                if (id == 0)
                {
                    filterContext.Result = new RedirectResult(_pathLogin);
                    base.OnActionExecuting(filterContext);
                    VerifAcces(RolesAccesAucun, filterContext, _pathErreurAuth);
                }
                else
                {
                    VerifAcces(RolesAccesEtu, filterContext, _pathErreurFerme);
                }                        

                if (!ValidationDate())
                {
                    filterContext.Result = new RedirectResult(_pathErreurFerme);
                }

                var inscriptionExistante = _db.Inscription.Any(x => x.id_Pers == id);
                if(inscriptionExistante)
                {
                    filterContext.Result = new RedirectResult(_pathErreurDeja);
                }
            }
            private bool ValidationDate()
            {
                var dateActuelle = DateTime.Now;
                var heureActuelle = dateActuelle.TimeOfDay;
                var dbHoraire =_db.p_HoraireInscription.OrderByDescending(x => x.id_Sess).First();
                return (dateActuelle >= dbHoraire.DateDebut && dateActuelle <= dbHoraire.DateFin) ||
                    (dateActuelle == dbHoraire.DateDebut && heureActuelle.Hours > dbHoraire.HeureDebut.Hours) ||
                    (dateActuelle == dbHoraire.DateFin && heureActuelle.Hours < dbHoraire.HeureFin.Hours);
            }

        }

    }
}
