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
        private static readonly List<TypeUsagers> RolesAccesSuper = new List<TypeUsagers> { TypeUsagers.Responsable, TypeUsagers.Super };
        private static readonly List<TypeUsagers> RolesAccesEnseignant = new List<TypeUsagers> { TypeUsagers.Enseignant, TypeUsagers.Responsable, TypeUsagers.Super };
        private static readonly List<TypeUsagers> RolesAccesTuteur = new List<TypeUsagers> { TypeUsagers.Responsable, TypeUsagers.Super, TypeUsagers.Enseignant, TypeUsagers.Tuteur };
        private static readonly List<TypeUsagers> RolesAccesEleve = new List<TypeUsagers> { TypeUsagers.Responsable, TypeUsagers.Super, TypeUsagers.Enseignant, TypeUsagers.Tuteur, TypeUsagers.Eleve };
        private static readonly List<TypeUsagers> RolesAccesEtu = new List<TypeUsagers> { TypeUsagers.Etudiant };
        private static readonly List<TypeUsagers> RolesAccesAucun = new List<TypeUsagers> { TypeUsagers.Aucun };

        private static void VerifAcces(List<TypeUsagers> listeRoles, ActionExecutingContext filterContext, string redirectTo)
        {
            var verif = SachemIdentite.ValiderRoleAcces(listeRoles, filterContext.HttpContext.Session);
            if (!verif)
                filterContext.Result = new RedirectResult(redirectTo);
        }

        public class ValidationAccesSuper : ActionFilterAttribute
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

        public class ValidationAccesTuteur : ActionFilterAttribute
        {
            
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                VerifAcces(RolesAccesTuteur, filterContext, _pathErreurAuth);
            }

        }

        public class ValidationAccesEleve : ActionFilterAttribute
        {

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                VerifAcces(RolesAccesEleve, filterContext, _pathErreurAuth);
            }

        }
        public class ValidationAccesEtu : ActionFilterAttribute
        {
            
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                VerifAcces(RolesAccesEtu, filterContext, _pathErreurAuth);
            }

        }

        public class ValidationAccesInscription : ActionFilterAttribute
        {
            private readonly SACHEMEntities _db = new SACHEMEntities();          

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                int id = SessionBag.Current.id_Pers;
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
