using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace sachem.Models
{
    //instruction pour indiquer que la classe de CoursMetaData est une classe de métadonnée
    [MetadataType(typeof(PersonneMetadata))]
    //on doit redéfinir la classe partielle même si on ajout rien. Placé immédiatement avant la classe de métadonnée associée
    public partial class Personne
    {
        [System.ComponentModel.DataAnnotations.Compare("MP", ErrorMessage = Messages.C_001)]
        [NotMappedAttribute]
        public string ConfirmPassword { get; set; }


        [System.ComponentModel.DataAnnotations.Compare("AncienMotDePasse", ErrorMessage = Messages.C_001)]
        [NotMappedAttribute]
        public string ConfirmPasswordEdit { get; set; }

        [NotMappedAttribute]
        public bool SouvenirConnexion { get; set; }

        [NotMappedAttribute]
        public string NomUtilisateur { get; set; }

        [NotMappedAttribute]
        public string AncienMotDePasse { get; set; }


    }
    public class PersonneMetadata
    {
        ////date de naissance
        //[Display(Name = "Date de naissance")]
        ////regulaExpression pour le format de la datte
        //[RegularExpression(@"^\d{4}[/](0?[1-9]|1[012])[/](0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = Messages.U_007)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        //[Required(ErrorMessage = Messages.U_001)]
        //public global::System.DateTime DateNais;
        [Display(Name = "Date de naissance")]
        [RegularExpression(@"^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-2][0-9]:[0-5][0-9]:[0-5][0-9]$|^[0-9]{4}\/[0-9]{2}\/[0-9]{2}$", ErrorMessage = Messages.U_007)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:yyyy\/MM\/dd}")]
        [Required(ErrorMessage = Messages.U_001)]
        public global::System.DateTime DateNais;

        //Nom de la personne
        [Display(Name = "Nom")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        [StringLength(30, ErrorMessage = Messages.U_001)]
        public string Nom;

        //Prénom de la personne
        [Display(Name = "Prénom")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        [StringLength(30, ErrorMessage = Messages.U_001)]
        public string Prenom;

        //nom d'usager
        [Display(Name = "Nom usager")]
        public string NomUsager;

        //adresse  courriel de la personne
        [Display(Name = "Courriel")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        [EmailAddress(ErrorMessage = Messages.U_008)]
        public string Courriel;

        //sexe de la personne
        [Display(Name = "Sexe")]
        public string id_Sexe;
        
        //type de l'usager
        [Display(Name = "Type usager")]
        public string id_TypeUsag;

        //numéro de téléphone
        [Display(Name = "Téléphone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = Messages.U_009)] //Vérifie le format du tel
        public global::System.String Telephone; //Ajout pour #Tel dans BD

        //Matricule de l'étudiant
        [Display(Name = "Matricule")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        [StringLength(7, MinimumLength = 7, ErrorMessage = Messages.U_004)]
        public string Matricule;

        //Mot de passe
        [Display(Name = "Mot de passe")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = Messages.U_001)]
        public string MP;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation du mot de passe")]
       public global::System.String ConfirmPassword;

        [DataType(DataType.Password)]
        [Display(Name = "Ancien mot de passe")]
        public global::System.String AncienMotDePasse;

    }

}