using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sachem.Models
{
    [MetadataType(typeof(GroupeMetadata))]
    //on doit redéfinir la classe partielle même si on ajout rien. Placé immédiatement avant la classe de métadonnée associée
    public partial class Groupe
    {

    }

    //la classe de métadonnée doit suivre immédiatement la redéfinition de classe partielle
    //Remarquez que cette classe de métadonnées n’est pas partielle.
    public class GroupeMetadata
    {
        [Display(Name = "ID du groupe")]
        public int id_Groupe;

        [Display(Name = "Numéro de groupe")]
        [Required(ErrorMessage = Messages.U_001)]
        public int NoGroupe;

        [Display(Name = "Cours")]
        [Required(ErrorMessage = Messages.U_001)]
        public string id_Cours;

        //[Display(Name = "Nombre de personne")]
        //public int nbPersonne;
        
    }
}