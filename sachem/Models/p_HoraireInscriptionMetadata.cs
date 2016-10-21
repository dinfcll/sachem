using System.ComponentModel.DataAnnotations;

namespace sachem.Models
{
    [MetadataType(typeof(p_HoraireInscriptionMetadata))]
    public partial class p_HoraireInscription
    {


    }

    public class p_HoraireInscriptionMetadata
    {
        [Display(Name = "Session")]
        public int id_Sess;

        [Required(ErrorMessage = Messages.U_001)]
        [RegularExpression("^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$", ErrorMessage = Messages.U_010)]
        [Display(Name = "Heure d'ouverture")]
        public System.TimeSpan HeureDebut;

        [Required(ErrorMessage = Messages.U_001)]
        [RegularExpression("^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$", ErrorMessage = Messages.U_010)]
        [Display(Name = "Heure de fermeture")]
        public System.TimeSpan HeureFin;

        [Required(ErrorMessage = Messages.U_001)]
        [RegularExpression(@"^\d{4}[/](0?[1-9]|1[012])[/](0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = Messages.U_007)]
        [Display(Name = "Date de début")]
        public global::System.String DateDebut;
        
        [Display(Name = "Date de fin")]
        [Required(ErrorMessage = Messages.U_001)]
        [RegularExpression(@"^\d{4}[/](0?[1-9]|1[012])[/](0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = Messages.U_007)]
        public global::System.String DateFin;

        public Session Session;
    }

}