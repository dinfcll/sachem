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
        /// Le programme a été modifier.
        /// </summary>
        /// <param name="Programme"></param>
        /// <returns></returns>
        public static string I_004(string Programme)
        { return $"Le programme {Programme} a été Modifié."; }

        /// <summary>
        /// Un étudiant est associé à ce programme d'études. Ce programme ne peut être supprimé.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>
        public static string I_005()
        { return $"Un étudiant est associé à ce programme d'études. Ce programme ne peut être supprimé."; }

        /// <summary>
        /// 
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
        { return $"Le programme d'études {nomProgrammeEtude} a été enregistré"; }

        ///<summary>
        /// Le programme d'études {0} a été supprimé
        /// </summary>
        ///<param name="nomProgrammeEtude"></param>
        /// <returns></returns>
        public static  string I_008(string nomProgrammeEtude)
        {  return $"Le programme d'études {nomProgrammeEtude} a été supprimer"; }

        /// <summary>
        /// Le cours {0} a été supprimé.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>
        public static string I_009(string Cours)
        { return $"Le cours {Cours} a été supprimé."; }

        /// <summary>
        /// Le fichier {0} a bien été transféré sur le serveur.
        /// </summary>
        /// <param name="Fichier"></param>
        /// <returns></returns>
        //public static string I_033(string Fichier)
        //{ return $"Le fichier {Fichier} a bien été transféré sur le serveur."; }

        /// <summary>
        /// Erreur lors du transfert de fichier.
        /// </summary>
        /// <returns></returns>
        public static string I_034(string Fichier)
        { return $"Erreur lors du transfert du fichier {Fichier}."; }

        /// <summary>
        /// Un fichier {0} de même nom est déjà présent sur le serveur.
        /// </summary>
        /// <param name="Fichier"></param>
        /// <returns></returns>
        public static string I_035(string Fichier)
        { return $"Un fichier {Fichier} de même nom est déjà présent sur le serveur."; }

        /// <summary>
        /// Le fichier {0} ne doit pas être vide{1}.
        /// </summary>
        /// <param name="Fichier"></param>
        /// <param name="Max"></param>
        /// <returns></returns>
        //public static string I_037()
        //{ return $"Le fichier est trop énorme"; }

        //Comptes/Connexion
        /// <summary>
        /// Connexion échouée.
        /// </summary>
        public static string I_017()
        { return $"Votre tentative de connexion a échoué. Réessayez."; }
        /// <summary>
        /// Mot de passe modifié.
        /// </summary>
        public static string I_018()
        { return $"Mot de passe modifié"; }

        /// <summary>
        /// Mot de passe envoyé au courriel
        /// </summary>
        public static string I_019()
        { return $"Votre mot de passe a été envoyé à votre adresse courriel."; }
        /// <summary>
        /// Compte existant pour cet étudiant!
        /// </summary>
        public static string I_025()
        { return $"Un compte existe déjà pour cet étudiant."; }
        /// <summary>
        /// Compte créé, vous pouvez vous connecter.
        /// </summary>
        public static string I_026()
        { return $"Le compte a été créé. Vous pouvez maintenant vous connecter."; }
        /// <summary>
        /// Aucun étudiant ne correspond aux données saisies.
        /// </summary>
        public static string I_027()
        { return $"Aucun étudiant ne correspond aux données saisies. Vous devez être inscrit à un cours offert par le département de mathématiques."; }


        #endregion

        #region MessageContexte
        //Contexte de comptes
        /// <summary>
        /// MDP doit correspondre a confirmation de mdp.
        /// </summary>
        public const string C_001 = "Le mot de passe et la confirmation du mot de passe doivent être identiques.";
        /// <summary>
        /// L'ancien mdp est invalide!
        /// </summary>
        public const string C_002 = "L'ancien mot de passe est invalide.";
        /// <summary>
        /// Aucune usager associé à cette adresse courriel.
        /// </summary>
        public const string C_003 = "Aucun usager associé à cette adresse courriel.";
        /// <summary>
        /// Usager inexistant ou mdp invalide.
        /// </summary>
        public const string C_004 = "Cet usager n'existe pas ou le mot de passe est invalide.";




            /// <summary>
            /// L'extension du fichier doit être {0} .
            /// </summary>
            /// <param name="Extension"></param>
            /// <returns></returns>
        //public static string C_007(string Extension)
        //{ return $"L'extension du fichier doit être {Extension} ."; }
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
        /// Longueur requise: 7 caractères.
        /// </summary>
        public const string U_004 = "Longueur requise: 7 caractères";

        /// <summary>
        /// Longueur requise: 6 caractères. (MDP)
        /// </summary>
        public const string U_005 = "Longueur requise: 6 caractères";


        /// <summary>
        /// Format: AAAA/MM/JJ
        /// </summary>
        public const string U_007 = "Format: AAAA/MM/JJ";

        /// <summary>
        /// Format: nom@domaine.com
        /// </summary>
        /// <param name="Code"></param>
        public const string U_008 = "Format: nom@nomdomaine.com";

        /// <summary>
        /// Format: (999) 999-9999
        /// </summary>
        public const string U_009 = "Format : (999) 999-9999";


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