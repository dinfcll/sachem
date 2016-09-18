using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sachem.Models
{
    //instruction pour indiquer que la classe de CoursMetaData est une classe de métadonnée
    [MetadataType(typeof(CourrielMetadata))]
    //on doit redéfinir la classe partielle même si on ajout rien. Placé immédiatement avant la classe de métadonnée associée
    public partial class Courriel
    {


    }

    //la classe de métadonnée doit suivre immédiatement la redéfinition de classe partielle
    //Remarquez que cette classe de métadonnées n’est pas partielle.
    public class CourrielMetadata
    {
        [Display(Name = "Type de courriel")]
        [Required(ErrorMessage = Messages.U_001)]
        public int id_TypeCourriel { get; set; }

        [Display(Name = "Titre")]
        [Required(ErrorMessage = Messages.U_001)]
        public string Titre { get; set; }

        [AllowHtml]
        [Display(Name = "Contenu du courriel")]
        [Required(ErrorMessage = Messages.U_001)]
        public string Courriel1 { get; set; }

        [RegularExpression(@"^\d{4}[-/](0?[1-9]|1[012])[-/](0?[1-9]|[12][0-9]|3[01])$",ErrorMessage = Messages.U_007)]
        [Display(Name = "Date de début")]
        public System.DateTime DateDebut { get; set; }

        [RegularExpression(@"^\d{4}[/](0?[1-9]|1[012])[/](0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = Messages.U_007)]
        [Display(Name = "Date de fin")]
        public Nullable<System.DateTime> DateFin { get; set; }

        [Display(Name = "Type de courriel")]
        public virtual p_TypeCourriel p_TypeCourriel { get; set; }
    }

}