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
        [Display(Name = "Prénom")]
        public string Prenom;

        [Required(ErrorMessage = Messages.U_001)]
        [Display(Name = "Nom")]
        public string Nom;

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = Messages.U_009)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = true)]
        [Display(Name = "Téléphone")]
        public string Telephone;

        [Required(ErrorMessage = Messages.U_001)]
        [Display(Name = "Page facebook")]
        public string Facebook;

        [Required(ErrorMessage = Messages.U_001)]
        [Display(Name = "Site web")]
        public string SiteWeb;

        [EmailAddress(ErrorMessage = Messages.U_008)]
        [Required(ErrorMessage = Messages.U_001)]
        public string Courriel;

        [Required(ErrorMessage = Messages.U_001)]
        public string Local; 
    }

}