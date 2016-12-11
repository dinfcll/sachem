using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using sachem.Models;
using System.Web.Mvc;

namespace sachem.Classes_Sachem
{
    public abstract class ValidationAcces
    {
        public const string pathErreurAuth = "/Home/Error";
        static readonly List<TypeUsagers> rolesAccesSuper = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super };
        static readonly List<TypeUsagers> rolesAccesEnseignant = new List<TypeUsagers>() { TypeUsagers.Enseignant, TypeUsagers.Responsable, TypeUsagers.Super };
        static readonly List<TypeUsagers> rolesAccesTuteur = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super, TypeUsagers.Enseignant, TypeUsagers.Tuteur };
        static readonly List<TypeUsagers> rolesAccesEleve = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super, TypeUsagers.Enseignant, TypeUsagers.Tuteur, TypeUsagers.Eleve };
        static readonly List<TypeUsagers> rolesAccesEtu = new List<TypeUsagers>() { TypeUsagers.Etudiant };
        private static void verifAcces(List<TypeUsagers> listeRoles, ActionExecutingContext filterContext, string redirectTo)
        {
            var verif = SachemIdentite.ValiderRoleAcces(listeRoles, filterContext.HttpContext.Session);
            if (!verif)
                filterContext.Result = new RedirectResult(redirectTo);
        }

        public class ValidationAccesSuper : ActionFilterAttribute
        {
            
            
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                verifAcces(rolesAccesSuper, filterContext, pathErreurAuth);
            }

        }

        public class ValidationAccesEnseignant : ActionFilterAttribute
        {
            
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                verifAcces(rolesAccesEnseignant, filterContext, pathErreurAuth);
            }

        }

        public class ValidationAccesTuteur : ActionFilterAttribute
        {
            
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                verifAcces(rolesAccesTuteur, filterContext, pathErreurAuth);
            }

        }

        public class ValidationAccesEleve : ActionFilterAttribute
        {

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                verifAcces(rolesAccesEleve, filterContext, pathErreurAuth);
            }

        }
        public class ValidationAccesEtu : ActionFilterAttribute
        {
            
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                verifAcces(rolesAccesEtu, filterContext, pathErreurAuth);
            }

        }

        public class ValidationAccesInscription : ActionFilterAttribute
        {
            private readonly SACHEMEntities db = new SACHEMEntities();
            int? id = SessionBag.Current.id_Pers;
            public const string PATH_ERREUR_AUTH = "/Home/Ferme";
            public const string PATH_ERREUR_DEJA = "/Home/Deja";
            
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (id == null)
                {
                    filterContext.Result = new RedirectResult("/Account/Login");
                }

                verifAcces(rolesAccesEtu, filterContext, pathErreurAuth);

                DateTime dateActuelle = DateTime.Now;
                if (!ValidationDate(dateActuelle))
                {
                    filterContext.Result = new RedirectResult(PATH_ERREUR_AUTH);
                }

                var inscriptionExistante = db.Inscription.Any(x => x.id_Pers == id);
                if(inscriptionExistante)
                {
                    filterContext.Result = new RedirectResult(PATH_ERREUR_DEJA);
                }
            }
            private bool ValidationDate(DateTime DateActuelle)
            {
                TimeSpan DateActuelle_Heure = TimeSpan.FromHours(DateActuelle.Hour);
                var Session = db.Session.GroupBy(s => s.id_Sess).Select(s => s.OrderByDescending(c => c.id_Sess).First()).Select(c => new { c.id_Sess});
                var HoraireActuel = db.p_HoraireInscription.OrderByDescending(x => x.id_Sess).First();
                if(DateActuelle < HoraireActuel.DateDebut && DateActuelle > HoraireActuel.DateFin)
                {
                    return false;
                }
                else if (DateActuelle == HoraireActuel.DateDebut && DateActuelle_Heure < HoraireActuel.HeureDebut)
                {
                    return false;
                }
                else if (DateActuelle == HoraireActuel.DateFin && DateActuelle_Heure > HoraireActuel.HeureFin)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

    }
}
