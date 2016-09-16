using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sachem.Models
{
    [MetadataType(typeof(PersonneMetadata))]
    public partial class Personne
    {
        //Pour valider la confirmation de mot de passe dans la vue, on crée une nouvelle valeur dans le modèle avec la balise
        //NotMappedAttribute qui ne sera pas sauvegardée sur la BD mais qui pourra être utilisée pour les validations.

        [System.ComponentModel.DataAnnotations.Compare("MP", ErrorMessage = Messages.C_001)]
        [NotMappedAttribute]
        public string ConfirmPassword { get; set; }

        [NotMappedAttribute]
        public bool SouvenirConnexion { get; set; }

        [NotMappedAttribute]
        public string NomUtilisateur { get; set; }

        [NotMappedAttribute]
        public string AncienMotDePasse { get; set; }


        public class PersonneMetadata //Grandement tirée du PAM... adapté pour le Sachem
        {
            [Display(Name = "Prénom")]
            [StringLength(30)]
            [Required(ErrorMessage = Messages.U_001)]
            public global::System.String Prenom;

            [Display(Name = "Nom")]
            [StringLength(30)]
            [Required(ErrorMessage = Messages.U_001)]
            public global::System.String Nom;

            //Expression régulière qui permet 2 formats de dates, celui exigé dans l'application YYYY/MM/DD et celui formaté par le 
            //système en format datetime YYYY/MM/DD hh:mm:ss. Il faut que les deux expressions soient utilisables pour que le modèle
            //ne tombe pas en erreur lors de la validation.
            [Display(Name = "Date de naissance")]
            [RegularExpression(@"^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-2][0-9]:[0-5][0-9]:[0-5][0-9]$|^[0-9]{4}\/[0-9]{2}\/[0-9]{2}$", ErrorMessage = Messages.U_007)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:yyyy\/MM\/dd}")]
            public global::System.DateTime DateNais;


            [Display(Name = "Courriel")]
            [EmailAddress(ErrorMessage = Messages.U_008)]
            [StringLength(256)]
            public global::System.String Courriel;

            [Display(Name = "Se souvenir de moi")]
            public global::System.String SouvenirConnexion;

            [Display(Name = "Téléphone")]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = Messages.U_009)] //Vérifie le format du tel
            public global::System.String Telephone; //Ajout pour #Tel dans BD

            [Display(Name = "Nom d'usager")]
            [StringLength(25)]
            public global::System.String NomUsager;

            [Display(Name = "Matricule")]
            [StringLength(9)]
            public global::System.String Matricule;

            //Extrait du PAM partiellement
            [Display(Name = "Matricule")]
            [StringLength(7)]
            public global::System.String Matricule7;

            [DataType(DataType.Password)]
            [Display(Name = "Mot de passe")]
            public global::System.String MP;

            [DataType(DataType.Password)]
            [Display(Name = "Confirmation du mot de passe")]
            public global::System.String ConfirmPassword;

            [DataType(DataType.Password)]
            [Display(Name = "Ancien mot de passe")]
            public global::System.String AncienMotDePasse;

            [Display(Name = "Type d'usager")]
            public global::System.Int32 id_TypeUsag;

            [Display(Name = "Sexe")]
            [Required(ErrorMessage = Messages.U_001)]
            public global::System.Int32 id_Sexe;

            [Display(Name = "Nom d'utilisateur")]
            public global::System.String NomUtilisateur;

            [Display(Name = "Nom du programme d'étude")]
            public string ProgEtu;

            [Display(Name = "Enseignant")]
            public string NomPrenom;
        }
    }
}