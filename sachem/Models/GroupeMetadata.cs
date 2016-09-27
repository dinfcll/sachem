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
        [Display(Name = "Nombre de personne")]
        public int nbPersonne;
    }

    //la classe de métadonnée doit suivre immédiatement la redéfinition de classe partielle
    //Remarquez que cette classe de métadonnées n’est pas partielle.
    public class GroupeMetadata
    {
        [Display(Name = "ID du groupe")]
        public int id_Groupe;

        [Display(Name = "Numéro de groupe")]
        public int NoGroupe;

        [Display(Name = "Cours")]
        public string id_Cours;
    }
}