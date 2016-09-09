using System.ComponentModel.DataAnnotations;

namespace sachem.Models
{
    //instruction pour indiquer que la classe de CoursMetaData est une classe de métadonnée
    [MetadataType(typeof(PersonneMetadata))]
    //on doit redéfinir la classe partielle même si on ajout rien. Placé immédiatement avant la classe de métadonnée associée
    public partial class Personne
    {


    }

    //la classe de métadonnée doit suivre immédiatement la redéfinition de classe partielle
    //Remarquez que cette classe de métadonnées n’est pas partielle.
    public class PersonneMetadata
    {

        [Display(Name = "Nom d'usager")]
        public string NomUsager;

        [Display(Name = "Prénom")]
        [Required(ErrorMessage = Messages.U_001)]
        public string Prenom;

        [Display(Name = "Nom")]
        [Required(ErrorMessage = Messages.U_001)]
        public string Nom;

        [Display(Name = "Sexe")]
        [Required(ErrorMessage = Messages.U_001)]
        public string id_Sexe;

        [Display(Name = "Date de naissance")]
        [Required(ErrorMessage = Messages.U_001)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public string DateNais;

        [Display(Name = "Type d'usager")]
        [Required(ErrorMessage = Messages.U_001)]
        public string id_TypeUsag;

        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string MP;

        [Display(Name = "Confirmation du mot de passe")]
        [DataType(DataType.Password)]
        public string ConfMP;

        [Display(Name = "Courriel")]
        [Required(ErrorMessage = Messages.U_001)]
        [EmailAddress(ErrorMessage = Messages.U_008)]
        public string Courriel;

    }

}