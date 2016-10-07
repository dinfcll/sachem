using System.ComponentModel.DataAnnotations;

namespace sachem.Models
{
   
    [MetadataType(typeof(p_ContactMetadata))]
    
    public partial class p_Contact
    {


    }

    
    public class p_ContactMetadata
    {

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        public string Prenom;

        [Display(Name = "Nom")]
        public string Nom;
    }

}