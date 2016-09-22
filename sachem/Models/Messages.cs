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

        // Groupes

        /// <summary>
        /// Le groupe {NoGroupe} a été supprimé.
        /// </summary>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>
        public static string I_020(int NoGroupe)
        { return $"Le groupe {NoGroupe} a été supprimé."; }

        /// <summary>
        /// Impossible d'enregistrer ce groupe. Il existe déjà un groupe ayant le numéro {NoGroupe}.
        /// </summary>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>
        public static string I_021(int NoGroupe)
        { return $"Impossible d'enregistrer ce groupe. Il existe déjà un groupe ayant le numéro {NoGroupe}."; }

        /// <summary>
        /// L'étudiant {Matricule} a été retiré du groupe {NoGroupe}.
        /// </summary>
        /// <param name="Matricule"></param>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>
        public static string I_022(string Matricule,int NoGroupe)
        { return $"L'étudiant {Matricule} a été retiré du groupe {NoGroupe}."; }

        /// <summary>
        /// Impossible d'ajouter l'étudiant {Matricule}  au groupe puisqu'il en fait déjà partie.
        /// </summary>
        /// <param name="Matricule"></param>
        /// <returns></returns>
        public static string I_023(string Matricule)
        { return $"Impossible d'ajouter l'étudiant {Matricule} au groupe puisqu'il en fait déjà partie"; }

        /// <summary>
        /// L'étudiant {Matricule} a été ajouté au groupe {IdGroupe}.
        /// </summary>
        /// <param name="Matricule"></param>
        /// <param name="IdGroupe"></param>
        /// <returns></returns>
        public static string I_024(string Matricule, int IdGroupe)
        { return $"L'étudiant {Matricule} a été ajouté au groupe {IdGroupe}."; }

        /// <summary>
        /// L'étudiant {Matricule} a été déplacé au groupe {IdGroupe} du cours {NomCours}.
        /// </summary>
        /// <param name="Matricule"></param>
        /// <param name="IdGroupe"></param>
        /// <returns></returns>
        public static string I_028(string Matricule, int IdGroupe,string NomCours)
        { return $"L'étudiant {Matricule} a été déplacé au groupe {IdGroupe} du cours {NomCours}."; }

        /// <summary>
        /// L'étudiant {Matricule} ne peut pas être déplacé au groupe {IdGroupe} du cours {NomCours},car il y est déjà!
        /// </summary>
        /// <param name="Matricule"></param>
        /// <param name="IdGroupe"></param>
        /// <returns></returns>
        public static string I_029(string Matricule, int IdGroupe, string NomCours)
        { return $"L'étudiant {Matricule} ne peut pas être déplacé au groupe {IdGroupe} du cours {NomCours},car il y est déjà!"; }


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
        /// Message pour longueur requise de 7 caractères
        /// </summary>
        /// <returns></returns>
        public const string U_004 = "Longueur requise: 7 caractères";

        /// <summary>
        /// Message pour longueur requise de 4 caractères
        /// </summary>
        /// <returns></returns>
        public const string U_005 = "Longueur requise : 4 caractères";

        /// <summary>
        /// Message pour Format: AAAA/MM/JJ
        /// </summary>
        /// <returns></returns>
        public const string U_007 = "Format: AAAA/MM/JJ";

        /// <summary>
        /// Message pour Format: nom@nomdomaine.com
        /// </summary>
        /// <returns></returns>
        public const string U_008 = "Format: nom@nomdomaine.com";

        /// <summary>
        /// Message pour Format : (999) 999-9999
        /// </summary>
        /// <returns></returns>
        public const string U_009 = "Format : (999) 999-9999";


        #endregion

        #region Question

        /// <summary>
        /// Voulez-vous vraiment supprimer le cours {0}?
        /// </summary>
        /// <param name="CodeUsager"></param>
        /// <returns></returns>private string Q_001(string Cours)
        public static string Q_001(string Cours)
        { return $"Voulez-vous vraiment supprimer le cours {Cours}?"; }

        /// <summary>
        /// Voulez-vous vraiment supprimer le groupe {0}?
        /// </summary>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>private string Q_006(string NoGroupe)
        public static string Q_006(int NoGroupe)
        { return $"Voulez-vous vraiment supprimer le groupe {NoGroupe}?"; }

        /// <summary>
        /// Des étudiants sont rattachés au groupe {0} Souhaitez-vous le supprimer définitivement?
        /// </summary>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>private string Q_007(string NoGroupe)
        public static string Q_007(int NoGroupe)
        { return $"Des étudiants sont rattachés au groupe {NoGroupe} Souhaitez-vous le supprimer définitivement?"; }

        /// <summary>
        /// Le groupe {0} a été créé. Souhaitez-vous y associer des étudiants?
        /// </summary>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>private string Q_008(string NoGroupe)
        public static string Q_008(int NoGroupe)
        { return $"Le groupe {NoGroupe} a été créé. Souhaitez-vous y associer des étudiants?"; }

        /// <summary>
        /// Voulez-vous vraiment retirer l'étudiant {Matricule} du groupe {NoGroupe}?
        /// </summary>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>private string Q_009(string Matricule,string NoGroupe)
        public static string Q_009(string Matricule,int NoGroupe)
        { return $"Voulez-vous vraiment retirer l'étudiant {Matricule} du groupe {NoGroupe}?"; }

        /// <summary>
        /// L'étudiant {PrenomNom} est déjà inscrit au cours {NomCours} pour la session {NomSaison}. Voulez-vous le déplacer dans le groupe {NoGroupe}? 
        /// </summary>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>private string Q_010(string PrenomNom,string NomSaison,string NoGroupe,string NomCours)
        public static string Q_010(string PrenomNom,string NomSaison,int NoGroupe,string NomCours)
        { return $"L'étudiant {PrenomNom} est déjà inscrit au cours {NomCours} pour la session {NomSaison}. Voulez-vous le déplacer dans le groupe {NoGroupe}?"; }

        #endregion

    }
}