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

        [Required(ErrorMessage = Messages.ChampRequis)]
        [RegularExpression("^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$", ErrorMessage = Messages.FormatHeureMinuteSeconde)]
        [Display(Name = "Heure d'ouverture")]
        public System.TimeSpan HeureDebut;

        [Required(ErrorMessage = Messages.ChampRequis)]
        [RegularExpression("^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$", ErrorMessage = Messages.FormatHeureMinuteSeconde)]
        [Display(Name = "Heure de fermeture")]
        public System.TimeSpan HeureFin;

        [Required(ErrorMessage = Messages.ChampRequis)]
        [RegularExpression(@"^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-2][0-9]:[0-5][0-9]:[0-5][0-9]$|^[0-9]{4}\/[0-9]{2}\/[0-9]{2}$", ErrorMessage = Messages.FormatEnDate)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\/MM\\/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date d'ouverture")]
        public System.DateTime DateDebut;
        
        [Display(Name = "Date de fin")]
        [Required(ErrorMessage = Messages.ChampRequis)]
        [RegularExpression(@"^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-2][0-9]:[0-5][0-9]:[0-5][0-9]$|^[0-9]{4}\/[0-9]{2}\/[0-9]{2}$", ErrorMessage = Messages.FormatEnDate)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\/MM\\/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DateFin;
    }

}