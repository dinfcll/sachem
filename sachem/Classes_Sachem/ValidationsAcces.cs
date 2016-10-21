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
    }
}