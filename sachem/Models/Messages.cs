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
        /// Le nom d'utilisateur est déjà utilisé.
        /// </summary>
        /// <returns></returns>
        public const string I_013 = "Le nom d'utilisateur est déjà utilisé.";


        #endregion

        #region MessageContexte
        /// <summary>
        /// Mot de passe non correspondant
        /// </summary>
        /// <returns></returns>
        public const string C_001 = "les mots de passe ne correspondent pas.";
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
        /// Longeur maximale de 30 caractères
        /// </summary>
        /// <returns></returns>
        public const string U_004 = "Longeur maximale permise : 30 caractères.";
        /// <summary>
        /// Longeur maximale de 256 caractères
        /// </summary>
        /// <returns></returns>
        public const string U_005 = "Longeur maximale permise : 256 caractères.";
        /// <summary>
        /// Longeur maximale de 25 caractères
        /// </summary>
        /// <returns></returns>
        public const string U_006 = "Longeur maximale permise : 25 caractères.";
        /// <summary>
        /// Format : AAAA/MM/JJ
        /// </summary>
        /// <returns></returns>
        public const string U_007 = "Format de date Invalide, format requis : AAAA/MM/JJ.";
        /// <summary>
        /// Format de courriel a respecter
        /// </summary>
        /// <returns></returns>
        public const string U_008 = "Format de courriel a respecter.";
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