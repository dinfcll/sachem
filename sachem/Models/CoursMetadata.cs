using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace sachem.Models
{
    //instruction pour indiquer que la classe de CoursMetaData est une classe de métadonnée
    [MetadataType(typeof(CoursMetadata))]
    //on doit redéfinir la classe partielle même si on ajout rien. Placé immédiatement avant la classe de métadonnée associée
    public partial class Cours
    {


    }

    //la classe de métadonnée doit suivre immédiatement la redéfinition de classe partielle
    //Remarquez que cette classe de métadonnées n’est pas partielle.
    public class CoursMetadata
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        [StringLength(8, MinimumLength = 8, ErrorMessage = Messages.U_003)]
        public global::System.String Code;


        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        public global::System.String Nom;

        [Display(Name = "Cours")]
        public global::System.String CodeNom;
    }

}