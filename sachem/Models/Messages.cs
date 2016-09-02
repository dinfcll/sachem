using System.Diagnostics;

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
        /// Le fichier {0} a bien été enregistré.
        /// </summary>
        /// <param name="Fichier"></param>
        /// <returns></returns>
        public static string I_033(string Fichier)
        { return $"Le fichier {Fichier} a bien été enregistré."; }

        /// <summary>
        /// Une erreur c'est produite lors du transfert de fichier.
        /// </summary>
        /// <returns></returns>
        public static string I_034()
        { return "Une erreur c'est produite lors du transfert de fichier."; }

        /// <summary>
        /// Le fichier {0} existe déjà dans le répertoire de la base de donnée.
        /// </summary>
        /// <param name="Fichier"></param>
        /// <returns></returns>
        public static string I_035(string Fichier)
        { return $"Le fichier {Fichier} existe déjà dans le répertoire de la base de donnée."; }
    }


        #endregion

        #region MessageContexte
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


        #endregion

        #region Question

        /// <summary>
        /// Voulez-vous vraiment supprimer le cours {0} ?
        /// </summary>
        /// <param name="CodeUsager"></param>
        /// <returns></returns>private string Q_001(string Cours)
        public static string Q_001(string Cours)
        { return $"Voulez-vous vraiment supprimer le cours {Cours} ?"; }


        #endregion

    }
}