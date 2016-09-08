using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;

namespace sachem.Models
{
    [MetadataType(typeof(CoursSuiviMetadata))]
    public partial class CoursSuivi
    {

    }

    public class CoursSuiviMetadata
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Cours")]
        [Required(ErrorMessage = Messages.U_001)]
        public string id_Cours;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Autre cours")]
        [Required(ErrorMessage = Messages.U_001)]
        public string autre_Cours;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Collège")]
        [Required(ErrorMessage = Messages.U_001)]
        public string id_College;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Autre collège")]
        [Required(ErrorMessage = Messages.U_001)]
        public string autre_College;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Session")]
        [Required(ErrorMessage = Messages.U_001)]
        public string id_Sess;

        [Display(Name = "Statut")]
        public string id_Statut;

        [Display(Name = "Résultat")]
        [Required(ErrorMessage = Messages.U_001)]
        [Range(0, 100 , ErrorMessage = Messages.U_011)]
        public string resultat;
    }
}