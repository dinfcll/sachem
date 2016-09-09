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
            int valeur = (int)value;
            if (valeur >= 1967 && valeur <= DateTime.Now.Year + 1)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("");
        }
    }
}