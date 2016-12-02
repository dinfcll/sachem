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

        

        public class ValidationAccesSuper : ActionFilterAttribute
        {
            
            static readonly List<TypeUsagers> rolesAcces = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super };
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var verif = SachemIdentite.ValiderRoleAcces(rolesAcces, filterContext.HttpContext.Session);
                if (!verif)
                    filterContext.Result = new RedirectResult(pathErreurAuth);
            }

        }

        public class ValidationAccesEnseignant : ActionFilterAttribute
        {
            static readonly List<TypeUsagers> rolesAcces = new List<TypeUsagers>() { TypeUsagers.Enseignant, TypeUsagers.Responsable, TypeUsagers.Super };
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var verif = SachemIdentite.ValiderRoleAcces(rolesAcces, filterContext.HttpContext.Session);
                if (!verif)
                    filterContext.Result = new RedirectResult(pathErreurAuth);
            }

        }

        public class ValidationAccesTuteur : ActionFilterAttribute
        {
            static readonly List<TypeUsagers> rolesAcces = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super, TypeUsagers.Enseignant, TypeUsagers.Tuteur };
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var verif = SachemIdentite.ValiderRoleAcces(rolesAcces, filterContext.HttpContext.Session);
                if (!verif)
                    filterContext.Result = new RedirectResult(pathErreurAuth);
            }

        }
        public class ValidationAccesEtu : ActionFilterAttribute
        {
            static readonly List<TypeUsagers> rolesAcces = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super, TypeUsagers.Enseignant, TypeUsagers.Tuteur, TypeUsagers.Eleve };
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var verif = SachemIdentite.ValiderRoleAcces(rolesAcces, filterContext.HttpContext.Session);
                if (!verif)
                    filterContext.Result = new RedirectResult(pathErreurAuth);
            }

        }

        public class ValidationAccesInscription : ActionFilterAttribute
        {
            private readonly SACHEMEntities db = new SACHEMEntities();
            int? id = SessionBag.Current.id_Pers;
            public const string PATH_ERREUR_AUTH = "/Home/Ferme";
            public const string PATH_ERREUR_DEJA = "/Home/Deja";
            static readonly List<TypeUsagers> rolesAcces = new List<TypeUsagers>() { TypeUsagers.Etudiant };
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (id == null)
                    filterContext.Result = new RedirectResult("/Account/Login");
                var verif = SachemIdentite.ValiderRoleAcces(rolesAcces, filterContext.HttpContext.Session);
                if (!verif)
                    filterContext.Result = new RedirectResult(PATH_ERREUR_AUTH);

                DateTime dateActuelle = DateTime.Now.Date;
                if (!ValidationDate(dateActuelle))
                    filterContext.Result = new RedirectResult(PATH_ERREUR_AUTH);

                var inscriptionExistante = db.Inscription.Any(x => x.id_Pers == id);
                if(inscriptionExistante)
                    filterContext.Result = new RedirectResult(PATH_ERREUR_DEJA);

            }
            private bool ValidationDate(DateTime DateActuelle)
            {
                var Session = db.Session.GroupBy(s => s.id_Sess).Select(s => s.OrderByDescending(c => c.id_Sess).First()).Select(c => new { c.id_Sess}); ;
                var HoraireActuel = db.p_HoraireInscription.OrderByDescending(x => x.id_Sess).First();
                if (!(DateActuelle > HoraireActuel.DateDebut && DateActuelle < HoraireActuel.DateFin))
                    return false;
                else
                    return true;
            }

        }

    }
}
