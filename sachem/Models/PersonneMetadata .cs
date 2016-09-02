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
        public string Prenom;

        [Display(Name = "Sexe")]
        public string id_Sexe;

        [Display(Name = "Date de naissance")]
        public string DateNais;

        [Display(Name = "Type d'usager")]
        public string id_TypeUsag;

        [Display(Name = "Mot de passe")]
        public string MP;

    }

}