using System.ComponentModel.DataAnnotations;


namespace sachem.Models
{
    
    [MetadataType(typeof(ProgrammeEtudeMetadata))]
    public partial class ProgrammeEtude
    {


    }


    public class ProgrammeEtudeMetadata
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        [MinLength(5)]
        public string Code { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        [MaxLength(255)]
        [Display(Name = "Nom de programme")]
        public string NomProg { get; set; }

        
        [Required(ErrorMessage = Messages.U_001)]
        [VerificationDate(ErrorMessage = Messages.U_012)]
        [Display(Name = "Année")]
        public int Annee { get; set; }
    }

}