using System;
using System.ComponentModel.DataAnnotations;


namespace sachem.Models
{
    public class VerificationDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult(Messages.ChampRequis);
            const int anneeMinimale = 1967;
            var valeur = (int)value;
                
            if (valeur >= anneeMinimale && valeur <= DateTime.Now.Year + 1)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("");
        }
    }
}