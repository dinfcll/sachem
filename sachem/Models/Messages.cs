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
        /// L’horaire d’inscription au SACHEM a été mis à jour.
        /// </summary>
        /// <returns></returns>
        public static string I_030()
        { return $"L’horaire d’inscription au SACHEM a été mis à jour."; }
        #endregion

        #region MessageContexte

        ///<summary>
        /// La dat de début doit être antérieure à la date de fin
        /// </summary>
        /// <returns></returns>
        public const string C_005 = "La date de début doit être antérieure à la date de fin.";

        ///<summary>
        /// La dat de début doit être antérieure à la date de fin
        /// </summary>
        /// <returns></returns>
        public const string C_006 = "Les dates de début et de fin doivent faire partie de la session sélectionnée.";
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

        ///<summary>
        /// Format: AAAA/MM/JJ
        /// </summary>
        /// <returns></returns>
        public const string U_007 = "Format : AAAA/MM/JJ";

        ///<summary>
        /// Format : HH:MM
        /// </summary>
        /// <returns></returns>
        public const string U_010 = "Format : HH:MM";

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