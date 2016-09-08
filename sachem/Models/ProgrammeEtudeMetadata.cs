using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace sachem.Models
{
    //instruction pour indiquer que la classe de CoursMetaData est une classe de métadonnée
    [MetadataType(typeof(ProgrammeEtudeMetadata))]
    //on doit redéfinir la classe partielle même si on ajout rien. Placé immédiatement avant la classe de métadonnée associée
    public partial class ProgrammeEtude
    {


    }

    //la classe de métadonnée doit suivre immédiatement la redéfinition de classe partielle
    //Remarquez que cette classe de métadonnées n’est pas partielle.
    public class ProgrammeEtudeMetadata
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        [MinLength(5)]
        public string Code { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        [MaxLength(255)]
        [Display(Name = "Nom de programme")]
        public string NomProg { get; set; }

        
        [Required(ErrorMessage = Messages.U_001)]
        [Display(Name = "Année")]
        public int Annee { get; set; }
    }

}