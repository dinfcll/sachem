using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using sachem.Models;
using System.Web.Mvc;

//Classe pour les validations, surtout pour l'uniformisation de la sécurité.
//On utilise un Action Filter Attribute au lieu de répéter les mêmes fonctions partout. 
namespace sachem.Classes_Sachem
{
    //Validation pour le controlleur paramètres
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