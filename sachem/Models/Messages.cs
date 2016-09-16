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
        /// Impossible d'enregistrer cet étudiant. Il existe déja im tudiant ayant le matricule {0}.
        /// </summary>
        /// <param name="matricule"></param>
        /// <returns></returns>
        public static string I_004(string matricule)
        { return $"Impossible d'enregistrer cet étudiant. Il existe déja un étudiant ayant le matricule {matricule}."; }

        /// <summary>
        /// Le cours {0} a été supprimé.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>private string I_009(string cours)
        public static string I_009(string Cours)
        { return $"Le cours {Cours} a été supprimé."; }

        /// <summary>
        /// L'étudiant {0} a été enregistré.
        /// </summary>
        /// <param name="Etudiant"></param>
        /// <returns></returns>
        public static string I_010(string Etudiant)
        { return $"L'étudiant {Etudiant} a été enregistré."; }

        /// <summary>
        /// Impossible de retirer le programme d'étude {0} des programmes suivis par l'étudiant."
        /// </summary>
        /// <param name="Programme"></param>
        /// <returns></returns>
        public static string I_011(string Programme)
        { return $"Impossible de retirer le programme d'étude {Programme} des programmes suivis par l'étudiant."; }
        /// <summary>
        /// l'étudiant ne peut être supprimé
        /// </summary>
        /// <returns></returns>
        public static string I_014()
        { return "l'étudiant ne peut être supprimé"; }

        /// <summary>
        /// Le programme d'étude {0} a été retiré de la liste des programmes suivis par l'étudiant.
        /// </summary>
        /// <param name="Programme"></param>
        /// <returns></returns>
        public static string I_016(string Programme)
        { return $"Le programme d'étude {Programme} a été retiré de la liste des programmes suivis par l'étudiant."; }

        /// <summary>
        /// L'étudiant {0} a été supprimé.
        /// </summary>
        /// <param name="Etudiant"></param>
        /// <returns></returns>
        public static string I_028(string Etudiant)
        { return $"L'étudiant {Etudiant} a été supprimé."; }

        /// <summary>
        /// "Tous les critères de la recherche doivent être précisés."
        /// </summary>
        public static string I_039()
        { return "Tous les critères de la recherche doivent être précisés."; }


        #endregion

        #region MessageContexte
        public const string C_001 = "Le mot de passe et la confirmation du mot de passe doivent être identiques.";
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
        /// 9 caractères
        /// </summary>
        /// <returns></returns>
        public const string U_004 = "Longueur requise : 9 caractères.";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public const string U_007 = "Format de l'année invalide. Format : AAAA/MM/JJ";

        /// <summary>
        /// Format : nom@domaine
        /// </summary>
        /// <returns></returns>
        public const string U_008 = "Format de l'adresse email invalide. Format : nom@domaine";

        /// <summary>
        /// Format : (999)999-9999
        /// </summary>
        /// <returns></returns>
        public const string U_009 = "Format du numéro de téléphone invalide. Format : (999)999-9999";

        #endregion

        #region Question

        /// <summary>
        /// Voulez-vous vraiment supprimer le cours {0} ?
        /// </summary>
        /// <param name="CodeUsager"></param>
        /// <returns></returns>private string Q_001(string Cours)
        public static string Q_001(string Cours)
        { return $"Voulez-vous vraiment supprimer le cours {Cours} ?"; }

        public static string Q_002(string Etudiant)
        { return $"Voulez-vous vraiment supprimer l'étudiant {Etudiant} ?"; }

        public static string Q_011(string Etudiant)
        { return $"Voulez-vous vraiment supprimer le cours {Etudiant} ?"; }
        #endregion

    }
}