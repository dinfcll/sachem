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
        /// Un étudiant est associé à ce programme d'études. Ce programme ne peut être supprimé.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>
        public static string I_005()
        { return $"Un étudiant est associé à ce programme d'études. Ce programme ne peut être supprimé."; }

        /// <summary>
        /// Impossible d'enregistrer ce programme d'études. Il existe déjà un programme ayant le code {0}.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string I_006(string code)
        { return $"Impossible d'enregistrer ce programme d'études. Il existe déjà un programme ayant le code {code}."; }

        ///<summary>
        /// Le programme d'études {0} a été enregistré
        /// </summary>
        ///<param name="nomProgrammeEtude"></param>
        /// <returns></returns>
        public static string I_007(string nomProgrammeEtude)
        { return $"Le programme d'études {nomProgrammeEtude} a été enregistré."; }

        ///<summary>
        /// Le programme d'études {0} a été supprimé
        /// </summary>
        ///<param name="nomProgrammeEtude"></param>
        /// <returns></returns>
        public static  string I_008(string nomProgrammeEtude)
        {  return $"Le programme d'études {nomProgrammeEtude} a été supprimé."; }

        /// <summary>
        /// Le cours {0} a été supprimé.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>
        public static string I_009(string Cours)
        { return $"Le cours {Cours} a été supprimé."; }


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

        /// <summary>
        /// Format : AAAA
        /// </summary>
        /// <returns></returns>
        public const string U_006 = "Format : AAAA";


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
        /// Voulez-vous vraiment supprimer le programme d'études {0} ?
        /// </summary>
        /// <param name="nomProgrammeEtude"></param>
        /// <returns></returns>private string Q_002(string NomProgrammeEtude)
        public static  string Q_002(string nomProgrammeEtude)
        { return $"Voulez-vous vraiment supprimer le programme d'études {nomProgrammeEtude} ?"; }

        #endregion
    }
}