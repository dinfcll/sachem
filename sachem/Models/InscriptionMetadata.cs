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
        [RegularExpression(@"^\d{4}[/](0?[1-9]|1[012])[/](0?[1-9]|[12][0-9]|3[01])$")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public global::System.DateTime DateInscription;
    }
}