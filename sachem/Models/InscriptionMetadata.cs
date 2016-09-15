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
    }
}