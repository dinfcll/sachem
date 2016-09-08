using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;

namespace sachem.Models
{
    public class PersonneMetadata {
        [Display(Name = "Date de naissance")]
        [RegularExpression(@"^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-2][0-9]:[0-5][0-9]:[0-5][0-9]$|^[0-9]{4}\/[0-9]{2}\/[0-9]{2}$"/*, ErrorMessage = Messages.U_007*/)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:yyyy\/MM\/dd}")]
        public global::System.DateTime DateNais;

    }
 
}