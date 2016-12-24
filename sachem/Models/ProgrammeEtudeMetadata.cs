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
        [Required(ErrorMessage = Messages.ChampRequis)]
        [MinLength(5)]
        public string Code { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.ChampRequis)]
        [MaxLength(255)]
        [Display(Name = "Nom de programme")]
        public string NomProg { get; set; }

        
        [Required(ErrorMessage = Messages.ChampRequis)]
        [VerificationDate(ErrorMessage = Messages.DatePlusHauteQueLAnneeDeFondationDuCegep)]
        [Display(Name = "Année")]
        public int Annee { get; set; }

        [Display(Name = "Nom du programme d'étude")]
        public string CodeNomProgramme;

    }

}