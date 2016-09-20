using System.ComponentModel.DataAnnotations;

namespace sachem.Models
{
    //instruction pour indiquer que la classe de p_TypeInscriptionMetaData est une classe de métadonnée
    [MetadataType(typeof(p_TypeInscriptionMetadata))]
    //on doit redéfinir la classe partielle même si on ajout rien. Placé immédiatement avant la classe de métadonnée associée
    public partial class p_TypeInscription
    {
        

    }

    //la classe de métadonnée doit suivre immédiatement la redéfinition de classe partielle
    //Remarquez que cette classe de métadonnées n’est pas partielle.
    public class p_TypeInscriptionMetadata
    {
        //TypeInscription
        [Display(Name = "Type d\'inscription")]
        public string TypeInscription;
    }

}