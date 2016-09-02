using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sachem.Models
{
    public partial class Personne
    {
        //Pour valider la confirmation de mot de passe dans la vue, on crée une nouvelle valeur dans le modèle avec la balise
        //NotMappedAttribute qui ne sera pas sauvegardée sur la BD mais qui pourra être utilisée pour les validations.

            //Extrait du projet PAM
        [CompareAttribute("MP", ErrorMessage = Messages.C_001)]
        [NotMappedAttribute]
        public string ConfirmPassword { get; set; }

        [NotMappedAttribute]
        public bool SouvenirConnexion { get; set; }

        [NotMappedAttribute]
        public string NomUtilisateur { get; set; }

        [NotMappedAttribute]
        public string AncienMotDePasse { get; set; }


        //Envoi vers sachemModeIDN
    }
}