using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sachem.Models
{
    
    [MetadataType(typeof(CourrielMetadata))]
    
    public partial class Courriel
    {


    }

   
    public class CourrielMetadata
    {
        [Display(Name = "Type de courriel")]
        [Required(ErrorMessage = Messages.ChampRequis)]
        public int id_TypeCourriel { get; set; }

        [Display(Name = "Titre")]
        [Required(ErrorMessage = Messages.ChampRequis)]
        public string Titre { get; set; }

        [AllowHtml]
        [Display(Name = "Contenu du courriel")]
        [Required(ErrorMessage = Messages.ChampRequis)]
        public string Courriel1 { get; set; }

       
        [Display(Name = "Date de début")]
        [RegularExpression(@"^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-2][0-9]:[0-5][0-9]:[0-5][0-9]$|^[0-9]{4}\/[0-9]{2}\/[0-9]{2}$", ErrorMessage = Messages.FormatEnDate)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\/MM\\/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DateDebut { get; set; }

        
        [Display(Name = "Date de fin")]
        [RegularExpression(@"^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-2][0-9]:[0-5][0-9]:[0-5][0-9]$|^[0-9]{4}\/[0-9]{2}\/[0-9]{2}$", ErrorMessage = Messages.FormatEnDate)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\/MM\\/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateFin { get; set; }

        [Display(Name = "Type de courriel")]
        public virtual int p_TypeCourriel { get; set; }
    }

}