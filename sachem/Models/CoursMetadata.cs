using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = Messages.ChampRequis)]
        [StringLength(8, MinimumLength = 8, ErrorMessage = Messages.LongueurDeHuitCaracteres)]
        public string Code;


        [Display(Name = "Nom du cours")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.ChampRequis)]
        public string Nom;

        [Display(Name = "Cours")]
        public string CodeNom;
    }

}