using System.ComponentModel.DataAnnotations;

namespace sachem.Models
{
    //instruction pour indiquer que la classe de CoursMetaData est une classe de métadonnée
    [MetadataType(typeof(PersonneMetadata))]
    //on doit redéfinir la classe partielle même si on ajout rien. Placé immédiatement avant la classe de métadonnée associée
    public partial class Personne
    {


    }

    public class PersonneMetadata
    {
        [Display(Name = "Enseignant")]
        public string NomPrenom;

        [Display(Name = "Matricule")]
        public string Matricule7;

        [Display(Name = "Nom du programme d'étude")]
        public string ProgEtu;
    }
}