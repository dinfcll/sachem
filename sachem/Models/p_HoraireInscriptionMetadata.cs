using System.ComponentModel.DataAnnotations;

namespace sachem.Models
{
    //instruction pour indiquer que la classe de CoursMetaData est une classe de métadonnée
    [MetadataType(typeof(p_HoraireInscriptionMetadata))]
    //on doit redéfinir la classe partielle même si on ajout rien. Placé immédiatement avant la classe de métadonnée associée
    public partial class p_HoraireInscription
    {


    }

    //la classe de métadonnée doit suivre immédiatement la redéfinition de classe partielle
    //Remarquez que cette classe de métadonnées n’est pas partielle.
    public class p_HoraireInscriptionMetadata
    {
        [Display(Name = "Session")]
        public int id_Sess;

        [Display(Name = "Heure d'ouverture")]
        public System.TimeSpan HeureDebut;

        [Display(Name = "Heure de fermeture")]
        public System.TimeSpan HeureFin;

        [Display(Name = "Date de début")]
        public System.DateTime DateDebut;

        [Display(Name = "Date de fin")]
        public System.DateTime DateFin;

        public Session Session;
    }

}