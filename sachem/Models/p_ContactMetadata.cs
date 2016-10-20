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

        [Display(Name = "Nom")]
        public string Nom;

        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }

        [Display(Name = "Page facebook")]
        public string Facebook { get; set; }

        [Display(Name = "Site web")]
        public string SiteWeb { get; set; }
        
    }

}