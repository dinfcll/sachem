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
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Display(Name = "Cours")]
        public string id_Cours;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Autre cours")]
        public string autre_Cours;

        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Display(Name = "Collège")]
        public string id_College;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Autre collège")]
        public string autre_College;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Session")]
        public string id_Sess;

        [Display(Name = "Statut")]
        public string id_Statut;

        [Display(Name = "Résultat (%)")]
        [DisplayFormat(DataFormatString = @"{0:#\%}", ApplyFormatInEditMode = true)]
        [Range(0, 100 , ErrorMessage = Messages.NombreEntreZeroEtCent)]
        public int resultat;
    }
}