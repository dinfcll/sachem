using System.Diagnostics;
using System.Web.Mvc;

namespace sachem.Models
{
    public class Messages
    {
        #region messagesInformatifs

        /// <summary>
        /// Un groupe est associé à ce cours. Le cours ne peut être supprimé.
        /// </summary>
        /// <returns></returns>
        public static string I_001()
        { return "Un groupe est associé à ce cours. Le cours ne peut être supprimé."; }

        /// <summary>
        /// Il existe déjà un cours ayant le code {0}.
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static string I_002(string Code)
        { return $"Il existe déjà un cours ayant le code {Code}."; }

        /// <summary>
        /// Le cours {0} a été enregistré.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>private string I_003(string cours)
        public static string I_003(string Cours)
        { return $"Le cours {Cours} a été enregistré."; }

        /// <summary>
        /// Le cours {0} a été supprimé.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>private string I_009(string cours)
        public static string I_009(string Cours)
        { return $"Le cours {Cours} a été supprimé."; }

        /// <summary>
        /// Enseignant relié à un cour.
        /// </summary>
        /// <returns></returns>
        public const string I_012 = "l'enseignant ne peut etre supprimer car il est relié à un cours";

        //Comptes/Connexion
        public static string I_017()
        { return $"Votre tentative de connexion a échoué. Réessayez."; }
        public static string I_018()
        { return $"Mot de passe modifié"; }
        public static string I_019()
        { return $"Votre mot de passe a été envoyé à votre adresse courriel."; }
        public static string I_025()
        { return $"Un compte existe déjà pour cet étudiant."; }
        public static string I_026()
        { return $"Le compte a été créé. Vous pouvez maintenant vous connecter."; }
        public static string I_027()
        { return $"Aucun étudiant ne correspond aux données saisies. Vous devez être inscrit à un cours offert par le département de mathématiques."; }


        /// <summary>
        /// Le nom d'utilisateur est déjà utilisé.
        /// </summary>
        /// <returns></returns>
        public static string I_013(string NomUsager)
        { return $"Le nom d'utilisateur {NomUsager} est déjà pris."; }

        /// <summary>
        /// Le nom d'utilisateur est déjà utilisé.
        /// </summary>
        /// <returns></returns>
        public static string I_015(string NomUsager)
        { return $"L'usager {NomUsager} à été modifié."; }

        /// <summary>
        /// l'enseignant {0} a été supprimer.
        /// </summary>
        /// <param name="NomUsager"></param>
        /// <returns></returns>private string I_009(string cours)
        public static string I_029(string Enseignant)
        { return $"L'Enseignant {Enseignant} a été supprimé."; }

        /// <summary>
        /// Enseignant présent dans un jumelage
        /// </summary>
        /// <returns></returns>
        public const string I_033 = "l'enseignant ne peut être supprimer car il est encore présent dans un jumelage";
        #endregion

        #region MessageContexte
        //Contexte de comptes
        public const string C_001 = "Le mot de passe et la confirmation du mot de passe doivent être identiques.";
        public const string C_002 = "L'ancien mot de passe est invalide.";
        public const string C_003 = "Aucun usager associé à cette adresse courriel.";
        public const string C_004 = "Cet usager n'existe pas ou le mot de passe est invalide.";



        #endregion

        #region MessageUnitaire

        /// <summary>
        /// Requis
        /// </summary>
        /// <returns></returns>
        public const string U_001 = "Requis";

        /// <summary>
        /// Format : nom@nomdomaine
        /// </summary>
        /// <returns></returns>
        public const string U_003 = "Longueur requise : 8 caractères.";

        /// <summary>
        /// Format : AAAA/MM/JJ
        /// </summary>
        /// <returns></returns>
        public const string U_007 = "Format de date Invalide, format requis : AAAA/MM/JJ.";

        /// <summary>
        /// Format de courriel a respecter
        /// </summary>
        /// <returns></returns>
        public const string U_008 = "Format de courriel a respecter : exemple@gmail.com";


        public const string U_004 = "Longueur requise: 7 caractères";


        #endregion

        #region Question

        /// <summary>
        /// Voulez-vous vraiment supprimer le cours {0} ?
        /// </summary>
        /// <param name="CodeUsager"></param>
        /// <returns></returns>private string Q_001(string Cours)
        public static string Q_001(string Cours)
        { return $"Voulez-vous vraiment supprimer le cours {Cours} ?"; }

        /// <summary>
        /// Voulez-vous vraiment supprimer l'enseignant {0} ?
        /// </summary>
        /// <param name="NomUsager"></param>
        /// <returns></returns>private string Q_003(string Enseignant)
        public static string Q_003(string Enseignant)
        { return $"Voulez-vous vraiment supprimer l'enseignant {Enseignant} ?"; }
        #endregion

        public static MvcHtmlString Q_004(string NomUsager, int id_Enseignant)
        {

            return MvcHtmlString.Create($"L'enseignant {NomUsager} a été créé. Souhaitez-vous <a href=\"Sachem/Groupes/{id_Enseignant}\">y associer un groupe?</a>"); // Note: Changé vers quel page le lien pointe.
        }

    }
}