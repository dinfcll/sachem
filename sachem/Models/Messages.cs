using System;
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

        //Comptes/Connexion
        /// <summary>
        /// Votre tentative de connexion a échoué. Réessayez.
        /// </summary>
        /// <returns></returns>
        public static string I_017()
        { return $"Votre tentative de connexion a échoué. Réessayez."; }

        /// <summary>
        /// Mot de passe modifié
        /// </summary>
        /// <returns></returns>
        public static string I_018()
        { return $"Mot de passe modifié"; }

        /// <summary>
        /// Votre mot de passe a été envoyé à votre adresse courriel.
        /// </summary>
        /// <returns></returns>
        public static string I_019()
        { return $"Votre mot de passe a été envoyé à votre adresse courriel."; }

        /// <summary>
        /// Un compte existe déjà pour cet étudiant.
        /// </summary>
        /// <returns></returns>
        public static string I_025()
        { return $"Un compte existe déjà pour cet étudiant."; }

        /// <summary>
        /// Le compte a été créé. Vous pouvez maintenant vous connecter.
        /// </summary>
        /// <returns></returns>
        public static string I_026()
        { return $"Le compte a été créé. Vous pouvez maintenant vous connecter."; }

        /// <summary>
        /// Aucun étudiant ne correspond aux données saisies. Vous devez être inscrit à un cours offert par le département de mathématiques.
        /// </summary>
        /// <returns></returns>
        public static string I_027()
        { return $"Aucun étudiant ne correspond aux données saisies. Vous devez être inscrit à un cours offert par le département de mathématiques."; }

        /// <summary>
        /// L’horaire d’inscription au SACHEM a été mis à jour.
        /// </summary>
        /// <returns></returns>
        public static string I_030()
        { return $"L’horaire d’inscription au SACHEM a été mis à jour."; }

        /// <summary>
        /// Le courriel a été mis à jour
        /// </summary>
        /// <returns></returns>
        public static string I_032()
        { return $"Le courriel a été mis à jour.";}
        #endregion

        #region MessageContexte
        //Contexte de comptes

        ///<summary>
        /// Le mot de passe et la confirmation du mot de passe doivent être identiques.
        /// </summary>
        /// <returns></returns>
        public const string C_001 = "Le mot de passe et la confirmation du mot de passe doivent être identiques.";

        ///<summary>
        /// L'ancien mot de passe est invalide.
        /// </summary>
        /// <returns></returns>
        public const string C_002 = "L'ancien mot de passe est invalide.";

        ///<summary>
        /// "Aucun usager associé à cette adresse courriel.
        /// </summary>
        /// <returns></returns>
        public const string C_003 = "Aucun usager associé à cette adresse courriel.";

        ///<summary>
        /// Cet usager n'existe pas ou le mot de passe est invalide.
        /// </summary>
        /// <returns></returns>
        public const string C_004 = "Cet usager n'existe pas ou le mot de passe est invalide.";

        ///<summary>
        /// La date de début doit être antérieure à la date de fin
        /// </summary>
        /// <returns></returns>
        public const string C_005 = "La date de début doit être antérieure à la date de fin.";

        ///<summary>
        /// Les dates de début et de fin doivent faire partie de la session sélectionnée.
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

        /// <summary>
        /// Longueur requise: 7 caractères
        /// </summary>
        /// <returns></returns>
        public const string U_004 = "Longueur requise: 7 caractères";

        ///<summary>
        /// Format: AAAA/MM/JJ
        /// </summary>
        /// <returns></returns>
        public const string U_007 = "Format : AAAA/MM/JJ";

        ///<summary>
        /// Format: nom@nomdomaine.com
        /// </summary>
        /// <returns></returns>
        public const string U_008 = "Format: nom@nomdomaine.com";

        ///<summary>
        /// Format : (999) 999-9999
        /// </summary>
        /// <returns></returns>
        public const string U_009 = "Format : (999) 999-9999";

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

        /// <summary>
        /// Un collège est en cours d'ajout ou de modification. Souhaitez-vous annuler cette opération?
        /// </summary>
        /// <returns></returns>
        public static string Q_014()
        { return $"Un collège est en cours d'ajout ou de modification. Souhaitez-vous annuler cette opération?"; }

        /// <summary>
        /// Voulez-vous vraiment supprimer le collège {0}?
        /// </summary>
        /// <param name="College"></param>
        /// <returns></returns>private string Q_015(string College)
        public static string Q_015(string College)
        { return $"Voulez-vous vraiment supprimer le collège {College} ?"; }

        #endregion

    }
}