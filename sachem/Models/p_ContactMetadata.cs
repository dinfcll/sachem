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
        [Required(ErrorMessage = Messages.ChampRequis)]
        [Display(Name = "Prénom")]
        public string Prenom;

        [Required(ErrorMessage = Messages.ChampRequis)]
        [Display(Name = "Nom")]
        public string Nom;

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = Messages.FormatTelephone)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = true)]
        [Display(Name = "Téléphone")]
        public string Telephone;

        [Required(ErrorMessage = Messages.ChampRequis)]
        [Display(Name = "Page facebook")]
        public string Facebook;

        [Required(ErrorMessage = Messages.ChampRequis)]
        [Display(Name = "Site web")]
        public string SiteWeb;

        [EmailAddress(ErrorMessage = Messages.FormatDeCourriel)]
        [Required(ErrorMessage = Messages.ChampRequis)]
        public string Courriel;

        [Required(ErrorMessage = Messages.ChampRequis)]
        public string Local; 
    }

}