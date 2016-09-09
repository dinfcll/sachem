using System.ComponentModel.DataAnnotations;

namespace sachem.Models
{
    //instruction pour indiquer que la classe de CoursMetaData est une classe de métadonnée
    [MetadataType(typeof(p_HoraireInscriptionMetadata))]
    //on doit redéfinir la classe partielle même si on ajout rien. Placé immédiatement avant la classe de métadonnée associée
    public partial class p_HoraireInscription
    {


    }

    //la classe de métadonnée doit suivre immédiatement la redéfinition de classe partielle
    //Remarquez que cette classe de métadonnées n’est pas partielle.
    public class p_HoraireInscriptionMetadata
    {
        [Display(Name = "Session")]
        public int id_Sess;

        [RegularExpression("^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$", ErrorMessage = Messages.U_010)]
        [Display(Name = "Heure d'ouverture")]
        public System.TimeSpan HeureDebut;

        [RegularExpression("^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$", ErrorMessage = Messages.U_010)]
        [Display(Name = "Heure de fermeture")]
        public System.TimeSpan HeureFin;

        [RegularExpression(@"^\d{4}[/](0?[1-9]|1[012])[/](0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = Messages.U_007)]
        [Display(Name = "Date de début")]
        public System.DateTime DateDebut;

        [Display(Name = "Date de fin")]
        [Required(ErrorMessage = Messages.U_001)]
        [RegularExpression(@"^\d{4}[/](0?[1-9]|1[012])[/](0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = Messages.U_007)]
        public System.DateTime DateFin;

        public Session Session;
    }

}