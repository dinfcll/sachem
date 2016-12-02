using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;

namespace sachem.Models
{
    [MetadataType(typeof(InscriptionMetadata))]
    public partial class Inscription
    {

    }

    public class InscriptionMetadata
    {
        //Bon d'échange
        [Display(Name = "Bon d'échange")]
        public string BonEchange;

        //date d'inscription
        [Display(Name = "Date d'inscription")]
        //regularExpression pour le format de la date
        [RegularExpression(@"^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-2][0-9]:[0-5][0-9]:[0-5][0-9]$|^[0-9]{4}\/[0-9]{2}\/[0-9]{2}$", ErrorMessage = Messages.U_007)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy\\/MM\\/dd}")]
        public global::System.DateTime DateInscription;

        [Display(Name = "Type d\'inscription")]
        [Required(ErrorMessage = Messages.U_001)]
        public string id_TypeInscription;

        [Display(Name = "Statut de l\'inscription")]
        [Required(ErrorMessage = Messages.U_001)]
        public string id_Statut;
    }
}
