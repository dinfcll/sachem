using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sachem.Models
{
    public class VerificationDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                const int ANNEE_MINIMALE = 1967;
                int valeur = (int)value;
                
                if (valeur >= ANNEE_MINIMALE && valeur <= DateTime.Now.Year + 1)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("");
            }
            return new ValidationResult(Messages.U_001);
            
        }
    }
}