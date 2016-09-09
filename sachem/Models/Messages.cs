﻿using System.Diagnostics;

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
        
        /// <summary>
        /// Le nom d'utilisateur est déjà utilisé.
        /// </summary>
        /// <returns></returns>
        public static string I_013(string NomUsager)
        { return $"Le nom d'utilisateur {NomUsager} est déjà pris."; }

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
        /// <summary>
        /// Les mot de passes non correspondant
        /// </summary>
        /// <returns></returns>
        public const string C_001 = "les mots de passes ne correspondent pas.";
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

        public static string Q_004(string NomUsager)
        { return $"L'enseignant {NomUsager} a été créé. Souhaitez-vous y associer un groupe?"; }

    }
}