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
        [NotMappedAttribute]
        public string ConfirmPassword { get; set; }

        [NotMappedAttribute]
        public bool SouvenirConnexion { get; set; }

        [NotMappedAttribute]
        public string NomUtilisateur { get; set; }

        [NotMappedAttribute]
        public string AncienMotDePasse { get; set; }
    }
    public class PersonneMetadata
    {
        [Display(Name = "Prénom")]
        [StringLength(30)]
        [Required(ErrorMessage = Messages.U_001)]
        public string Prenom;

        [Display(Name = "Nom")]
        [StringLength(30)]
        [Required(ErrorMessage = Messages.U_001)]
        public string Nom;

        [Display(Name = "Nom")]
        public string NomPrenom;

        //Expression régulière qui permet 2 formats de dates, celui exigé dans l'application YYYY/MM/DD et celui formaté par le 
        //système en format datetime YYYY/MM/DD hh:mm:ss. Il faut que les deux expressions soient utilisables pour que le modèle
        //ne tombe pas en erreur lors de la validation.
        [Display(Name = "Date de naissance")]
        //la mise en commentaire le l'expression reguliere me permet de creer des comptes
        [RegularExpression(@"^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-2][0-9]:[0-5][0-9]:[0-5][0-9]$|^[0-9]{4}\/[0-9]{2}\/[0-9]{2}$", ErrorMessage = Messages.U_007)]
        [DisplayFormat(DataFormatString = "{0:yyyy\\/MM\\/dd}", ApplyFormatInEditMode = true)]
        public global::System.DateTime DateNais;


        [Display(Name = "Courriel")]
        [EmailAddress(ErrorMessage = Messages.U_008)]
        [StringLength(256)]
        public string Courriel;

        [Display(Name = "Se souvenir de moi")]
        public string SouvenirConnexion;

        [Display(Name = "Téléphone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = Messages.U_009)] //Vérifie le format du tel
        public string Telephone; //Ajout pour #Tel dans BD

        [Display(Name = "Nom d'utilisateur")]
        [StringLength(25)]
        public string NomUsager;

        [Display(Name = "No de DA")]
        [StringLength(9)]
        public global::System.String Matricule;

        //Extrait du PAM partiellement
        [Display(Name = "No de DA")]
        [StringLength(7, ErrorMessage = Messages.U_004)]
        public string Matricule7;

        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string MP;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation du mot de passe")]
        public string ConfirmPassword;

        [DataType(DataType.Password)]
        [Display(Name = "Ancien mot de passe")]
        public string AncienMotDePasse;

        [Display(Name = "Type d'usager")]
        public int id_TypeUsag;

        [Display(Name = "Sexe")]
        [Required(ErrorMessage = Messages.U_001)]
        public int id_Sexe;

        [Display(Name = "Nom d'utilisateur")]
        public global::System.String NomUtilisateur;

        [Display(Name = "Âge")]
        public string Age;

        [Display(Name = "Programme d'étude")]
        public string ProgEtu;
    }

}