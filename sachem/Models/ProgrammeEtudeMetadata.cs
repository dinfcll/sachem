using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Nom de programme")]
        public string NomProg { get; set; }

        [Display(Name = "Année")]
        public int Annee { get; set; }
    }

}