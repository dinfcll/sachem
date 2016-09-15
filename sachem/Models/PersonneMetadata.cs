using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;

namespace sachem.Models
{
    //instruction pour indiquer que la classe de CoursMetaData est une classe de métadonnée
    [MetadataType(typeof(PersonneMetadata))]
    //on doit redéfinir la classe partielle même si on ajout rien. Placé immédiatement avant la classe de métadonnée associée
    public partial class Personne
    {
        //asdadasda

    }
    public class PersonneMetadata
    {
        //date de naissance
        [Display(Name = "Date de naissance")]
        //regulaExpression pour le format de la datte
        [RegularExpression(@"^\d{4}[/](0?[1-9]|1[012])[/](0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = Messages.U_007)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
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
        [Display(Name = "Numéro de téléphone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?([0-9]{3})-([0-9]{4})$", ErrorMessage = Messages.U_009)]
        public string Telephone;

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

        //[Display(Name = "Confirmation du mot de passe")]
        //[DataType(DataType.Password)]
        //public string ConfMP;
    }

}