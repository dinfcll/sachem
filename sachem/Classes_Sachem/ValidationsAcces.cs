using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using sachem.Models;
using System.Web.Mvc;

namespace sachem.Classes_Sachem
{
    public class ValidationAccesParametres : ActionFilterAttribute
    {
        static readonly List<TypeUsagers> rolesAcces = new List<TypeUsagers>() { TypeUsagers.Responsable, TypeUsagers.Super };
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var verif = SachemIdentite.ValiderRoleAcces(rolesAcces, filterContext.HttpContext.Session);
            if (!verif)
                filterContext.Result = new RedirectResult("/Home/Error");
        }

    }
}