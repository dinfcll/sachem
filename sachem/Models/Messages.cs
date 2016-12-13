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
        public static string GroupeAssocieAUnCoursNePeutEtreSupprime()
        { return "Un groupe est associé à ce cours. Le cours ne peut être supprimé."; }

        /// <summary>
        /// Il existe déjà un cours ayant le code {0}.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string CoursADejaCeCode(string code)
        { return $"Il existe déjà un cours ayant le code {code}."; }

        /// <summary>
        /// Le cours {0} a été enregistré.
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>private string I_003(string cours)
        public static string CoursEnregistre(string cours)
        { return $"Le cours {cours} a été enregistré."; }

        /// <summary>
        /// Impossible d'enregistrer cet étudiant. Il existe déja im tudiant ayant le matricule {0}.
        /// </summary>
        /// <param name="matricule"></param>
        /// <returns></returns>
        public static string MatriculeDejaExistant(string matricule)
        { return $"Impossible d'enregistrer cet étudiant. Il existe déja un étudiant ayant le matricule {matricule}."; }


        /// <summary>
        /// Un étudiant est associé à ce programme d'études. Ce programme ne peut être supprimé.
        /// </summary>
        /// <returns></returns>
        public static string ProgrammeNonSupprimeCarEtudiantYEstAsoocie()
        { return $"Un étudiant est associé à ce programme d'études. Ce programme ne peut être supprimé."; }

        /// <summary>
        /// Impossible d'enregistrer ce programme d'études. Il existe déjà un programme ayant le code {0}.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ProgrammeAvecCodeDejaExistant(string code)
        { return $"Impossible d'enregistrer ce programme d'études. Il existe déjà un programme ayant le code {code}."; }

        ///<summary>
        /// Le programme d'études {0} a été enregistré
        /// </summary>
        ///<param name="nomProgrammeEtude"></param>
        /// <returns></returns>
        public static string ProgrammeAvecMemeNom(string nomProgrammeEtude)
        { return $"Le programme d'études {nomProgrammeEtude} a été enregistré."; }

        ///<summary>
        /// Le programme d'études {0} a été supprimé
        /// </summary>
        /// <param name="nomProgrammeEtude"></param>
        /// <returns></returns>
        public static  string ProgrammeSupprime(string nomProgrammeEtude)
        {  return $"Le programme d'études {nomProgrammeEtude} a été supprimé."; }


        /// <summary>
        /// Le cours {0} a été supprimé.
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>
        public static string CoursSupprime(string cours)
        { return $"Le cours {cours} a été supprimé."; }

        /// <summary>
        /// L'étudiant {0} a été enregistré.
        /// </summary>
        /// <param name="etudiant"></param>
        /// <returns></returns>
        public static string EtudiantEnregistre(string etudiant)
        { return $"L'étudiant {etudiant} a été enregistré."; }

        /// <summary>
        /// Impossible de retirer le programme d'étude {0} des programmes suivis par l'étudiant.
        /// </summary>
        /// <param name="programme"></param>
        /// <returns></returns>
        public static string ImpossibleDeRetirerProgrammeDUnEtudiant(string programme)
        { return $"Impossible de retirer le programme d'étude {programme} des programmes suivis par l'étudiant."; }

        /// <summary>
        /// L'enseignant ne peut être supprimé car il est relié à un cours.
        /// </summary>
        /// <returns></returns>
        public const string EnseignantNePeutEtreSupprime = "L'enseignant ne peut être supprimé car il est relié à un cours.";

        /// <summary>
        /// Impossible d’enregistrer cet enseignant. Il existe déjà un enseignant ayant le nom d'usager {0}.
        /// </summary>
        /// <param name="nomUsager"></param>
        /// <returns></returns>
        public static string NomEnseignantDejaExistant(string nomUsager)
        { return $"Impossible d’enregistrer cet enseignant. Il existe déjà un enseignant ayant le nom d'usager {nomUsager}."; }

        /// <summary>
        /// L'étudiant ne peut être supprimé, car il est associé à un groupe.
        /// </summary>
        /// <returns></returns>
        public static string EtudiantNePeutEtreSupprimeCarLieAUnGroupe()
        { return "L'étudiant ne peut être supprimé, car il est associé à un groupe."; }

        /// <summary>
        /// L'usager {0} a été modifié.
        /// </summary>
        /// <param name="nomUsager"></param>
        /// <returns></returns>
        public static string UsagerModfie(string nomUsager)
        { return $"L'usager {nomUsager} a été modifié."; }

        /// <summary>
        /// Le programme d'étude {0} a été retiré de la liste des programmes suivis par l'étudiant.
        /// </summary>
        /// <param name="programme"></param>
        /// <returns></returns>
        public static string ProgrammeRetireDelaListeEtudiant(string programme)
        { return $"Le programme d'étude {programme} a été retiré de la liste des programmes suivis par l'étudiant."; }

        //Comptes/Connexion
        /// <summary>
        /// Votre tentative de connexion a échoué. Réessayez.
        /// </summary>
        /// <returns></returns>
        public static string ConnexionEchouee()
        { return $"Votre tentative de connexion a échouée. Réessayez."; }

        /// <summary>
        /// Mot de passe modifié
        /// </summary>
        /// <returns></returns>
        public static string MotDePasseModifie()
        { return $"Mot de passe modifié"; }

        /// <summary>
        /// Votre mot de passe a été envoyé à votre adresse courriel.
        /// </summary>
        /// <returns></returns>
        public static string MotDePasseCourriel()
        { return $"Votre mot de passe a été envoyé à votre adresse courriel."; }

        // Groupes

        /// <summary>
        /// Le groupe {0} a été supprimé.
        /// </summary>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string GroupeSupprime(int noGroupe)
        { return $"Le groupe {noGroupe} a été supprimé."; }

        /// <summary>
        /// Impossible d'enregistrer ce groupe. Il existe déjà un groupe ayant le numéro {0}.
        /// </summary>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string GroupeAyantLeMemeNumero(int noGroupe)
        { return $"Impossible d'enregistrer ce groupe. Il existe déjà un groupe ayant le numéro {noGroupe}."; }

        /// <summary>
        /// L'étudiant {0} a été retiré du groupe {1}.
        /// </summary>
        /// <param name="matricule"></param>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string EudiantRetireDuGroupe(string matricule, int noGroupe)
        { return $"L'étudiant {matricule} a été retiré du groupe {noGroupe}."; }

        /// <summary>
        /// Impossible d'ajouter l'étudiant {0} au groupe puisqu'il en fait déjà partie.
        /// </summary>
        /// <param name="matricule"></param>
        /// <returns></returns>
        public static string EtudiantAjouteDeuxFoisAuGroupe(string matricule)
        { return $"Impossible d'ajouter l'étudiant {matricule} au groupe puisqu'il en fait déjà partie."; }

        /// <summary>
        /// L'étudiant {0} a été ajouté au groupe {1}.
        /// </summary>
        /// <param name="matricule"></param>
        /// <param name="idGroupe"></param>
        /// <returns></returns>
        public static string EtudiantAjouteAuGroupe(string matricule, int idGroupe)
        { return $"L'étudiant {matricule} a été ajouté au groupe {idGroupe}."; }

        /// <summary>
        /// Un compte existe déjà pour cet étudiant.
        /// </summary>
        /// <returns></returns>
        public static string CompteExisteDeja()
        { return $"Un compte existe déjà pour cet étudiant."; }

        /// <summary>
        /// Le compte a été créé. Vous pouvez maintenant vous connecter.
        /// </summary>
        /// <returns></returns>
        public static string CompteCree()
        { return $"Le compte a été créé. Vous pouvez maintenant vous inscrire au Sachem."; }

        /// <summary>
        /// Aucun étudiant ne correspond aux données saisies. Vous devez être inscrit à un cours offert par le département de mathématiques.
        /// </summary>
        /// <returns></returns>
        public static string EtudiantNonInscrit()
        { return $"Aucun étudiant ne correspond aux données saisies. Vous devez être inscrit à un cours offert par le département de mathématiques."; }
       
        /// <summary>
        /// L'étudiant {0} a été supprimé.
        /// </summary>
        /// <param name="etudiant"></param>
        /// <returns></returns>
        public static string EtudiantSupprime(string etudiant)
        { return $"L'étudiant {etudiant} a été supprimé."; }

        /// <summary>
        /// L'enseignant {0} a été supprimer.
        /// </summary>
        /// <param name="enseignant"></param>
        /// <returns></returns>
        public static string EnseignantSupprime(string enseignant)
        { return $"L'enseignant {enseignant} a été supprimé."; }

        /// <summary>
        /// L’horaire d’inscription au SACHEM a été mis à jour.
        /// </summary>
        /// <returns></returns>
        public static string HoraireMisAJour()
        { return $"L’horaire d’inscription au SACHEM a été mis à jour."; }


        ///<summary>
        /// L’information de la section « Nous contacter » a été mise à jour.
        /// </summary>
        /// <returns></returns>
        public static string NousContaterMisAJour()
        { return $"L’information de la section « Nous contacter » a été mise à jour."; }

        /// <summary>
        /// Le courriel a été mis à jour
        /// </summary>
        /// <returns></returns>
        public static string CourrielMisAJour()
        { return $"Le courriel a été mis à jour."; }

        /// <summary>
        /// Enseignant présent dans un jumelage
        /// </summary>
        /// <returns></returns>
        public static string EnseignantNonSupprimeJumelagePresent()
        { return $"L'enseignant ne peut être supprimé car il est encore présent dans un jumelage."; }

        /// <summary>
        /// Erreur lors du transfert de fichier {0}.
        /// </summary>
        /// <param name="fichier"></param>
        /// <returns></returns>
        public static string ErreurTransfertFichier(string fichier)
        { return $"Erreur lors du transfert du fichier {fichier}."; }

        /// <summary>
        /// Un fichier {0} de même nom est déjà présent sur le serveur.
        /// </summary>
        /// <param name="fichier"></param>
        /// <returns></returns>
        public static string FichierAvecLeMemeNomExisteDeja(string fichier)
        { return $"Un fichier {fichier} de même nom est déjà présent sur le serveur."; }

        /// <summary>
        /// Impossible d’enregistrer ce cours. Il existe déjà dans votre liste de cours suivis antérieurement.
        /// </summary>
        /// <returns></returns>
        public static string ImpossibleEnregistrerCoursCarExisteListeCoursSuivis()
        { return "Impossible d’enregistrer ce cours. Il existe déjà dans votre liste de cours suivis antérieurement."; }

        /// <summary>
        /// Un responsable ne peut pas se supprimer lui-même
        /// </summary>
        /// <returns></returns>
        public static string ResponsableSeSupprimerLuiMeme()
        { return $"Un responsable ne peut pas se supprimer lui-même."; }

        /// <summary>
        /// L'étudiant {Matricule} a été déplacé au groupe {IdGroupe} du cours {NomCours}.
        /// </summary>
        /// <param name="matricule"></param> 
        /// <param name="idGroupe"></param>
        /// <param name="nomCours"></param>
        /// <returns></returns>
        public static string EtudiantDeplacementDeGroupe(string matricule, int idGroupe, string nomCours)
        { return $"L'étudiant {matricule} a été déplacé au groupe {idGroupe} du cours {nomCours}."; }

        /// <summary>
        /// L'étudiant {0} ne peut pas être déplacé au groupe {1} du cours {2},car il y est déjà!
        /// </summary>
        /// <param name="matricule"></param>
        /// <param name="idGroupe"></param>
        /// <param name="nomCours"></param>
        /// <returns></returns>
        public static string EtudiantDeplacementDeCoursImpossible(string matricule, int idGroupe, string nomCours)
        { return $"L'étudiant {matricule} ne peut pas être déplacé au groupe {idGroupe} du cours {nomCours}, car il y est déjà."; }

        /// <summary>
        /// L'étudiant ne peut être supprimé s'il est jumelé
        /// </summary>
        /// <returns></returns>
        public static string EtudiantNonSupprimeCarJumele()
        { return $"L'étudiant ne peut être supprimé s'il est jumelé."; }

        /// <summary>
        /// Le collège {0} a été ajouté
        /// </summary>
        /// <param name="nomCollege"></param>
        /// <returns></returns>
        public static string CollegeAjoute(string nomCollege)
        { return $"Le collège {nomCollege} a été ajouté."; }

        /// <summary>
        /// L'étudiant {0} a été modifié.
        /// </summary>
        /// <param name="nomEtudiant"></param>
        /// <returns></returns>
        public static string EtudiantModifie(string nomEtudiant)
        { return $"L'étudiant {nomEtudiant} a été modifié."; }

        /// <summary>
        /// Un cours ne peut être choisi qu'une seule fois.
        /// </summary>
        /// <returns></returns>
        public static string CoursChoisiUneSeuleFois()
        { return $"Un cours ne peut être choisi qu'une seule fois."; }

        /// <summary>
        /// Le nom du collège a bien été modifié
        /// </summary>
        /// <returns></returns>
        public static string CollegeModifie()
        { return "Le nom du collège a bien été modifié."; }

        /// <summary>
        /// Le collège {0} a bien été supprimé
        /// </summary>
        /// <param name="nomCollege"></param>
        /// <returns></returns>
        public static string CollegeSupprime(string nomCollege)
        { return $"Le collège {nomCollege} a bien été supprimé."; }
        
        /// <summary>
        /// Problème lors de l'envoi du courriel, le port {0} est bloqué.
        /// </summary>
        /// <param name="numeroPort"></param>
        /// <returns></returns>
        public static string EnvoiCourrielImpossiblePortBloque(string numeroPort)
        { return $"Problème lors de l'envoi du courriel, le port {numeroPort} est bloqué."; }

        /// <summary>
        /// Cochez la case pour signer le contrat.
        /// </summary>
        /// <returns></returns>
        public static string CaseDoitEtreCochee()
        { return $"Cochez la case pour signer le contrat."; }

        /// <summary>
        /// Le numéro de téléphone a été modifié.
        /// </summary>
        /// <returns></returns>
        public static string TelephoneModifie()
        { return $"Le numéro de téléphone a été modifié."; }

        /// <summary>
        /// Le numéro de téléphone saisi est invalide.
        /// </summary>
        /// <returns></returns>
        public static string TelephoneInvalide()
        { return $"Le numéro de téléphone saisi est invalide."; }

        /// <summary>
        /// L'adresse courriel a été modifiée.
        /// </summary>
        /// <returns></returns>
        public static string CourrielModifie()
        { return $"L'adresse courriel a été modifiée."; }

        /// <summary>
        /// L'adresse courriel saisie est invalide.
        /// </summary>
        /// <returns></returns>
        public static string CourrielInvalide()
        { return $"L'adresse courriel saisie est invalide."; }

        /// <summary>
        /// Le bon d'achat a été acheté.
        /// </summary>
        /// <returns></returns>
        public static string BonAchatAchete()
        { return $"Le bon d'achat a été acheté."; }

        /// <summary>
        /// Le bon d'achat n'a pas été acheté.
        /// </summary>
        /// <returns></returns>
        public static string BonAchatPasAchete()
        { return $"Le bon d'achat n'a pas été acheté."; }

        /// <summary>
        /// Ce collège d'enseignement existe déjà.
        /// </summary>
        /// <returns></returns>
        public static string CollegeDejaExistant()
        { return $"Ce collège d'enseignement existe déjà."; }

        /// <summary>
        /// Impossible de mettre le programme inactif s'il est encore relié à des étudiants.
        /// </summary>
        /// <returns></returns>
        public static string ImpossibleMettreProgrammeInactif()
        { return $"Impossible de mettre le programme inactif s'il est encore relié à des étudiants."; }

        /// <summary>
        /// Erreur lors de la modification de l'inscription. N'oubliez pas qu'il est impossible de modifier l'inscription des anciennes sessions.
        /// </summary>
        /// <returns></returns>
        public static string ErreurModificationInscription()
        { return $"Erreur lors de la modification de l'inscription. N'oubliez pas qu'il est impossible de modifier l'inscription des anciennes sessions."; }
        #endregion

        #region MessageContexte
        //Contexte de comptes

        ///<summary>
        /// Le mot de passe et la confirmation du mot de passe doivent être identiques.
        /// </summary>
        /// <returns></returns>
        public static string MotsDePasseDoiventEtreIdentiques()
        { return $"Le mot de passe et la confirmation du mot de passe doivent être identiques."; }

        ///<summary>
        /// L'ancien mot de passe est invalide.
        /// </summary>
        /// <returns></returns>
        public static string MauvaisAncienMotDePasse()
        { return $"L'ancien mot de passe est invalide.";}

        ///<summary>
        /// Aucun usager associé à cette adresse courriel.
        /// </summary>
        /// <returns></returns>
        public static string AucunUsagerAvecCeCourriel()
        { return $"Aucun usager associé à cette adresse courriel."; }

        ///<summary>
        /// La date de début doit être antérieure à la date de fin
        /// </summary>
        /// <returns></returns>
        public static string ValidationDate()
        { return $"La date de début doit être antérieure à la date de fin."; } 

        ///<summary>
        /// Les dates de début et de fin doivent faire partie de la session sélectionnée.
        /// </summary>
        /// <param name="dateDebut"></param>
        /// <param name="dateFin"></param>
        /// <returns></returns>
        public static string DatesDansLaSession (string dateDebut, string dateFin)
        { return $"Les dates de début et de fin doivent faire partie de la session. {dateDebut} et {dateFin}."; }

        /// <summary>
        /// Un des deux champs {0}, {1} doit être complété.
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        public static string CompleterLesChamps(string param1, string param2)
        { return $"Un des deux champs {param1}, {param2} doit être complété."; }


        /// <summary>
        /// Acheté
        /// </summary>
        /// <returns></returns>
        public static string BonEchangeAchete = "Acheté";

        /// <summary>
        /// Pas acheté
        /// </summary>
        /// <returns></returns>
        public static string BonEchangePasAchete = "Pas acheté";
        #endregion

        #region MessageUnitaire

        /// <summary>
        /// Requis.
        /// </summary>
        /// <returns></returns>
        public const string ChampRequis = "Requis.";

        /// <summary>
        /// Longueur requise : 8 caractères.
        /// </summary>
        /// <returns></returns>
        public const string LongueurDeHuitCaracteres = "Longueur requise : 8 caractères.";

        /// <summary>
        /// Longueur requise: 7 caractères.
        /// </summary>
        /// <returns></returns>
        public const string LongueurDeSeptCaracteres = "Longueur requise: 7 caractères.";

        /// <summary>
        /// Longueur requise: 4 caractères.
        /// </summary>
        public const string LongueurDeQuatreCaracteres = "Longueur requise: 4 caractères";

        /// <summary>
        /// Résultat requis si réussi.
        /// </summary>
        public const string ResultatRequisSiReussi = "Le résultat est requis si le statut du cours est réussi.";

        /// <summary>
        /// Format : AAAA
        /// </summary>
        /// <returns></returns>
        public const string FormatAnnee = "Format : AAAA";

        /// <summary>
        /// Format: AAAA/MM/JJ
        /// </summary>
        /// <returns></returns>
        public const string FormatEnDate = "Format: AAAA/MM/JJ";

        /// <summary>
        /// Format: nom@domaine.com
        /// </summary>
        /// <returns></returns>
        public const string FormatDeCourriel = "Format: nom@nomdomaine.com";

        ///<summary>
        /// Format : (999) 999-9999
        /// </summary>
        /// <returns></returns>
        public const string FormatTelephone = "Format : (999) 999-9999";

        ///<summary>
        /// Format : HH:MM:SS
        /// </summary>
        /// <returns></returns>
        public const string FormatHeureMinuteSeconde = "Format : HH:MM:SS";

        /// <summary>
        /// "La date indiquée doit être entre l'année 1967 et celle en cours"
        /// </summary>
        /// <returns></returns>
        public const string DatePlusHauteQueLAnneeDeFondationDuCepgep = "La date indiquée doit être entre l'année 1967 et celle en cours.";

        /// <summary>
        /// "Longueur minimale: 6 caractères!"
        /// </summary>
        /// <returns></returns>
        public const string LongueurDeSixCarateres = "Longueur minimale de 6 caractères.";
        /// <summary>
        /// "Veuillez entrer un nombre en 0 et 1000"
        /// </summary>
        /// <returns></returns>
        public const string NombreEntreZeroEtCent = "Veuillez entrer un nombre en 0 et 1000.";

        /// <summary>
        /// "Votre nom d'utilisateur est votre numéro de DA (7 chiffres)."
        /// </summary>
        /// <returns></returns>
        public const string ConnexionEtudiantRestriction = "Votre nom d'utilisateur est votre numéro de DA (7 chiffres).";
        /// <summary>
        /// "Votre nom d'utilisateur est votre nom, suivi de la première lettre de votre prénom."
        /// </summary>
        /// <returns></returns>
        public const string ConnexionEnseignantRestriction = "Votre nom d'utilisateur est votre nom, suivi de la première lettre de votre prénom.";
        /// <summary>
        /// "Cours de mathématiques antérieurs"
        /// </summary>
        /// <returns></returns>
        public const string CoursAnterieur = "Cours de mathématiques antérieurs";

        #endregion

        #region Question

        /// <summary>
        /// Voulez-vous vraiment supprimer le cours {0}?
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>
        public static string VraimentSupprimerCours(string cours)
        { return $"Voulez-vous vraiment supprimer le cours {cours}?"; }

        /// <summary>
        /// Voulez-vous vraiment supprimer le programme d'études {0} ?
        /// </summary>
        /// <param name="nomProgrammeEtude"></param>
        /// <returns></returns>
        public static string VraimentSupprimerProgrammeEtude(string nomProgrammeEtude)
        { return $"Voulez-vous vraiment supprimer le programme d'études {nomProgrammeEtude} ?"; }

        /// <summary>
        /// Voulez-vous vraiment supprimer le programme d'études:
        /// </summary>
        /// <returns></returns>
        public static string SupprimerCeProgrammeEtude()
        { return $"Voulez-vous vraiment supprimer le programme d'études:"; }

        /// <summary>
        /// Voulez-vous vraiment supprimer l'enseignant {0} ?
        /// </summary>
        /// <param name="enseignant"></param>
        /// <returns></returns>
        public static string VraimentSupprimerEnseignant(string enseignant)
        { return $"Voulez-vous vraiment supprimer l'enseignant {enseignant} ?"; }

        /// <summary>
        /// L'enseignant {0} a été créé. Souhaitez-vous y associer un groupe?
        /// </summary>
        /// <param name="nomUsager"></param>
        /// <param name="idEnseignant"></param>
        /// <returns></returns>
        public static MvcHtmlString AjouterUnGroupeAUnEnseignant(string nomUsager, int idEnseignant)
        { return MvcHtmlString.Create($"L'enseignant {nomUsager} a été créé. Souhaitez-vous <a href=\"/Groupes/Create?idEns={idEnseignant}\">y associer un groupe?</a>");}

        /// <summary>
        /// Voulez-vous vraiment supprimer le groupe {0}?
        /// </summary>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string VraimentSupprimerGroupe(int noGroupe)
        { return $"Voulez-vous vraiment supprimer le groupe {noGroupe}?"; }

        /// <summary>
        /// Des étudiants sont rattachés au groupe {0} Souhaitez-vous le supprimer définitivement?
        /// </summary>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string VraimentSupprimerGroupeAvecEtudiantsRattaches(int noGroupe)
        { return $"Des étudiants sont rattachés au groupe {noGroupe}. Souhaitez-vous le supprimer définitivement?"; }

        /// <summary>
        /// Le groupe {0} a été créé. Souhaitez-vous y associer des étudiants?
        /// </summary>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string GroupeCreeAssocierEtudiant(int noGroupe)
        { return $"Le groupe {noGroupe} a été créé. Souhaitez-vous y associer des étudiants?"; }

        /// <summary>
        /// Voulez-vous vraiment retirer l'étudiant {0} du groupe {1}?
        /// </summary>
        /// <param name="matricule"></param>
        /// <param name="noGroupe"></param>
        /// <returns></returns>
        public static string RetirerEtudiantDUnGroupe(string matricule,int noGroupe)
        { return $"Voulez-vous vraiment retirer l'étudiant {matricule} du groupe {noGroupe}?"; }

        /// <summary>
        /// L'étudiant {0} est déjà inscrit à un cours. Voulez-vous le déplacer? 
        /// </summary>
        /// <param name="prenomNom"></param>
        /// <returns></returns>
        public static string VraimentDeplacerEtudiant(string prenomNom)
        { return $"L'étudiant {prenomNom} est déjà inscrit à un cours. Voulez-vous le déplacer?"; }
        
        /// <summary>
        /// Voulez-vous vraiment supprimer le cours {0} de votre liste de cours suivis?
        /// </summary>
        /// <param name="cours"></param>
        /// <returns></returns>
        public static string SupprimerCoursListeSuivis(string cours)
        { return $"Voulez-vous vraiment supprimer le cours {cours} de votre liste de cours suivis?"; }

        /// <summary>
        /// Un collège est en cours d'ajout ou de modification. Souhaitez-vous annuler cette opération?
        /// </summary>
        /// <returns></returns>
        public static string AnnulerOperationCollege()
        { return $"Un collège est en cours d'ajout ou de modification. Souhaitez-vous annuler cette opération?"; }


        /// <summary>
        /// Voulez-vous vraiment supprimer le collège {0}?
        /// </summary>
        /// <param name="college"></param>
        /// <returns></returns>
        public static string VraimentSupprimerCollege(string college)
        { return $"Voulez-vous vraiment supprimer le collège {college}?"; }

        /// <summary>
        /// Voulez-vous vraiment mettre fin à ce jumelage?
        /// </summary>
        /// <returns></returns>
        public static string VraimentMettreFinJumelage()
        { return $"Voulez-vous vraiment mettre fin à ce jumelage?"; }

        /// <summary>
        /// Voulez-vous vraiment supprimer l'étudiant {0} ?
        /// </summary>
        /// <param name="nomEtudiant"></param>
        /// <returns></returns>
        public static string VraimentSupprimerEtudiant(string nomEtudiant)
        { return $"Voulez-vous vraiment supprimer l'étudiant {nomEtudiant}?"; }

        /// <summary>
        /// Combien d'heures êtes vous pret à donner à chaque semaine (1h30/élève aidé)?.
        /// </summary>
        /// <returns></returns>
        public static string InscriptionCombienHeuresPretADonner()
        { return $"Combien d'heures êtes vous pret à donner à chaque semaine (1h30/élève aidé)?"; }


        /// <summary>
        /// "Etes-vous certain d'annuler le téléversement?"
        /// </summary>
        /// <returns></returns>
        public static string ImporterAnnulerTeleversementConfirmation()
        { return $"Etes-vous certain d'annuler le téléversement?"; }

        /// <summary>
        /// Êtes-vous prêt à faire deux rencontres consécutives (3h)
        /// </summary>
        public const string InscriptionRencontresConsecutives = "Êtes-vous prêt à faire deux rencontres consécutives (3h) ?";

        #endregion

        #region Inscription
        /// <summary>
        /// Un resultat et un statut d'un des cours ne concorde pas. Un cours réussi doit avoir une note supérieur ou égal à 60 et un échec inférieur à 60.
        /// </summary>
        /// <returns></returns>
        public static string InscriptionCoursReussiErreurLorsDuChargement()
        { return $"Un resultat et un statut d'un des cours ne concorde pas. Un cours réussi doit avoir une note supérieur ou égal à 60 et un échec inférieur à 60."; }

        /// <summary>
        /// Indiquez tous les cours de mathématiques(collégial) que vous avez réussis, abandonnés ou échoués au cégep. Pour ceux réussis, indiquez vos résultats :
        /// </summary>
        /// <returns></returns>
        public static string InscriptionCoursReussiIndication()
        { return $"Indiquez tous les cours de mathématiques(collégial) que vous avez réussis, abandonnés ou échoués au cégep. Pour ceux réussis, indiquez vos résultats :"; }

        /// <summary>
        /// Indiquez vos heures de disponibilité en cliquant dans les cases appropriées.
        /// </summary>
        /// <returns></returns>
        public static string InscriptionDirectivesTableauDisponibilite1()
        { return $"Indiquez vos heures de disponibilité en cliquant dans les cases appropriées."; }

        /// <summary>
        /// Chaque ✓ indique que vous êtes disponible pour une rencontre de 1h30 dans cette plage horaire
        /// </summary>
        /// <returns></returns>
        public static string InscriptionDirectivesTableauDisponibilite2()
        { return $"Chaque ✓ indique que vous êtes disponible pour une rencontre de 1h30 dans cette plage horaire"; }

        /// <summary>
        /// Il y aura au minimum une rencontre par semaine, soit 1h30/semaine
        /// </summary>
        /// <returns></returns>
        public static string InscriptionDirectivesTableauDisponibilite3()
        { return "Il y aura au minimum une rencontre par semaine, soit 1h30/semaine"; }

        /// <summary>
        /// Inscrivez plusieurs choix pour faciliter le jumelage.
        /// </summary>
        /// <returns></returns>
        public static string InscriptionDirectivesTableauDisponibilite4()
        { return "Inscrivez plusieurs choix pour faciliter le jumelage."; }

        /// <summary>
        /// Confirmation de vos disponibilités
        /// </summary>
        public const string InscriptionConfirmationDispoTitre = "Confirmation de vos disponibilités";

        /// <summary>
        /// Type d'inscription choisi:
        /// </summary>
        public const string InscriptionTypeInscriptionChoisi = "Type d'inscription choisi:";

        /// <summary>
        /// Type d'inscription choisi:
        /// </summary>
        public const string InscriptionTypeInscriptionChoisiAlert = "Veuillez choisir un type d'inscription";

        /// <summary>
        /// Vos plages de disponibilités sélectionnées sont:
        /// </summary>
        public const string InscriptionDisposChoisis = "Vos plages de disponibilités sélectionnées sont:";

        /// <summary>
        /// Veuillez remplir le formulaire de disponibilités.
        /// </summary>
        public const string InscriptionRemplirFormulaireDisposErreur =
            "Veuillez remplir le formulaire de disponibilités.";

        /// <summary>
        /// Les cours dans lesquels vous aimeriez donner de l'aide sont:
        /// </summary>
        public const string InscriptionCoursQueJaide = "Les cours dans lesquels vous aimeriez donner de l'aide sont:";

        /// <summary>
        /// Indiquez tous les cours de mathématiques (collégial) que vous avez suivis et réussis ainsi que le cégep où ces cours ont eu lieu :
        /// </summary>
        public const string InscriptionCoursSuivis = "Indiquez tous les cours de mathématiques (collégial) que vous avez suivis et réussis ainsi que le cégep où ces cours ont eu lieu :";

        #endregion

        #region Importer données
        /// <summary>
        /// Les fichers seront traités dans les prochaines dix minutes.
        /// </summary>
        /// <returns></returns>
        public static string ImporterTempsDeTraitementsEnMinutes()
        { return $"Les fichers seront traités dans les prochaines dix minutes."; }



        /// <summary>
        /// "Le fichier est trop énorme."
        /// </summary>
        /// <returns></returns>
        public static string ImporterFichierTropEnorme(string filesize, string maxFilesize)
        { return $"Le fichier est trop énorme: ({filesize}MiB). Taille maximale: {maxFilesize}MiB."; }

        /// <summary>
        /// "Les fichiers CSV seulement."
        /// </summary>
        /// <returns></returns>
        public static string ImporterFichierCsvSeulement(string extension)
        { return $"Seulement les fichiers de type {extension} sont acceptés."; }

        /// <summary>
        /// "Le serveur a répondu le code d'erreur."
        /// </summary>
        /// <returns></returns>
        public static string ImporterCodeErreurServeur(string statusCode)
        { return $"Le serveur a répondu le code d'erreur: {statusCode}."; }

        /// <summary>
        /// "Annuler le téléversement."
        /// </summary>
        /// <returns></returns>
        public static string ImporterAnnulerTeleversement()
        { return "Annuler le téléversement."; }

        /// <summary>
        /// "Effacer le fichier."
        /// </summary>
        /// <returns></returns>
        public static string ImporterEffacerFichier()
        { return "Effacer le fichier."; }

        /// <summary>
        /// "Vous ne pouvez pas téléverser plus de fichiers."
        /// </summary>
        /// <returns></returns>
        public static string ImporterMaxAtteint()
        { return "Vous ne pouvez pas téléverser plus de fichiers."; }

        /// <summary>
        /// "SVP utiliser le formulaire ci-dessous pour le téléversement de fichiers."
        /// </summary>
        /// <returns></returns>
        public static string ImporterDirectivesDropZone(string extension)
        { return $"Utiliser cette page pour téléverser le ficher de données ({extension}) reçu de l'administration du CLL."; }

        /// <summary>
        /// "Glisser le fichier ici pour le téléverser au serveur."
        /// </summary>
        /// <returns></returns>
        public static string ImporterDirectivesDropZoneFichier()
        { return "Glisser les fichiers ici ou cliquez ici pour téléverser."; }

        /// <summary>
        /// "Votre navigateur ne supporte pas la fonction glisser-déposer de fichiers."
        /// </summary>
        /// <returns></returns>
        public static string ImporterNavigateurNeSupportePasDropZone()
        { return "Votre navigateur ne supporte pas la fonction glisser-déposer de fichiers."; }
        #endregion

        #region Contrat Engagement
        /// <summary>
        /// "Dans le but d'assurer l'efficacité du service de tutorat offert au Sachem, vous vous engagez à:"
        /// </summary>
        /// <returns></returns>
        public const string ContratJeMengageA = "Dans le but d'assurer l'efficacité du service de tutorat offert au Sachem, vous vous engagez à:";

        /// <summary>
        /// "Respecter l'horaire prévu et à aviser votre partenaire dans le cas d'un retard prévisible ou d'une absence."
        /// </summary>
        /// <returns></returns>
        public const string ContratClose1 = "Respecter l'horaire prévu et à aviser votre partenaire dans le cas d'un retard prévisible ou d'une absence. Le tuteur a la responsabilité de compléter un billet d'absence.";

        /// <summary>
        /// "Reprendre dans les plus bref délais toute rencontre annulée (au plus tard lors des 2 semaines qui suivront la rencontre)."
        /// </summary>
        /// <returns></returns>
        public const string ContratClose2 = "Reprendre dans les plus bref délais toute rencontre annulée (au plus tard lors des 2 semaines qui suivront la rencontre).";

        /// <summary>
        /// "Préparer vos rencontres de façon à utiliser adéquatement les périodes de tutorat."
        /// </summary>
        /// <returns></returns>
        public const string ContratClose3 = "Préparer vos rencontres de façon à utiliser adéquatement les périodes de tutorat. Chacun doit fournir une certaine somme de travail entre les rencontres afin que celles-ci atteignent leur objectif. À cette fin, l'élève aidé accepte que son tuteur vérifie au début de chaque rencontre le travail effectué (exercices, résumés, etc.) depuis la rencontre précédente.";

        /// <summary>
        /// "Accepter dans les plus bref délais toute rencontre annulée"
        /// </summary>
        /// <returns></returns>
        public const string ContratClose4 = "Accepter dans les plus bref délais toute rencontre annulée (au plus tard lors des 2 semaines qui suivront la rencontre).";

        /// <summary>
        /// "Aviser la sécretaire du Sachem si vous décidez d'abandonner votre cours ou le cégep, et ce, le plus tôt possible."
        /// </summary>
        /// <returns></returns>
        public const string ContratClose5 = "Aviser la sécretaire du Sachem si vous décidez d'abandonner votre cours ou le cégep, et ce, le plus tôt possible.";

        /// <summary>
        /// "Aviser la sécretaire du Sachem si vous décidez d'interrompre vos rencontres de tutorat"
        /// </summary>
        /// <returns></returns>
        public const string ContratClose6 = "Aviser la sécretaire du Sachem si vous décidez d'interrompre vos rencontres de tutorat, et ce, le plus tôt possible.";

        /// <summary>
        /// "Je m'engage à travailler sérieusement au Sachem, à être ponctuel et respectueux."
        /// </summary>
        /// <returns></returns>
        public const string ContratClose7 = "Je m'engage à travailler sérieusement au Sachem, à être ponctuel et respectueux. De plus, je m'engage à fournir des questions à mon tuteur 48 heures à l'avance afin que celui-ci puisse se préparer adéquatement.";

        /// <summary>
        /// "Je m'engage à bien préparer mes rencontres, à être ponctuel et respectueux."
        /// </summary>
        /// <returns></returns>
        public const string ContratClose8 = "Je m'engage à bien préparer mes rencontres, à être ponctuel et respectueux.";

        /// <summary>
        /// "Votre contrat d'engagement a déjà été signé."
        /// </summary>
        /// <returns></returns>
        public const string ContratConfirmation = "Votre contrat d'engagement a déjà été signé.";

        #endregion

        #region Jumelage
        /// <summary>
        /// Le jumelage a bien été retiré.
        /// </summary>
        /// <returns></returns>
        public static string JumelageRetire()
        { return $"Le jumelage a bien été retiré."; }

        /// <summary>
        /// Le jumelage a bien été crée.
        /// </summary>
        /// <returns></returns>
        public static string JumelageCree()
        { return $"Le jumelage a bien été crée."; }

        /// <summary>
        /// Veuillez sélectionner un enseignant.
        /// </summary>
        /// <returns></returns>
        public static string JumelageCreationDoitSelectionnerEnseignant()
        { return $"Veuillez sélectionner un enseignant."; }

        /// <summary>
        /// Mettre fin à un jumelage
        /// </summary>
        /// <returns></returns>
        public const string JumelageFinJumelage = "Mettre fin à un jumelage";

        /// <summary>
        /// Êtes-vous certain de mettre fin au jumelage entre:
        /// </summary>
        /// <returns></returns>
        public static string JumelageFinJumelageEntre()
        { return "Êtes-vous certain de mettre fin au jumelage entre:"; }

        /// <summary>
        /// Êtes-vous certain de créer au jumelage entre:
        /// </summary>
        /// <returns></returns>
        public static string JumelageCreerJumelageEntre()
        { return "Êtes-vous certain de créer un jumelage entre:"; }

        /// <summary>
        /// Pour la plage horaire de:
        /// </summary>
        /// <returns></returns>
        public const string JumelagePlageHoraireSelectionne = "Pour la plage horaire de:";

        /// <summary>
        /// Avec comme superviseur enseignant:
        /// </summary>
        /// <returns></returns>
        public const string JumelageEnseignantSelectionne = "Avec comme superviseur enseignant:";

        /// <summary>
        /// Désirez-vous étendre la durée de cette rencontre sur 3h pour créer un jumelage avec deux rencontres consécutives ?
        /// </summary>
        /// <returns></returns>
        public const string JumelageCreerConsecutif = "Désirez-vous étendre la durée de cette rencontre sur 3h pour créer un jumelage avec deux rencontres consécutives ?";

        #endregion

        #region College
        /// <summary>
        /// "Ajouter un collège d'enseignement"
        /// </summary>
        /// <returns></returns>
        public const string CollegeAjouter = "Ajouter un collège d'enseignement";
        /// <summary>
        /// "Modification du nom du collège d'enseignement."
        /// </summary>
        /// <returns></returns>
        public const string CollegeModification = "Modification du nom du collège d'enseignement.";

        /// <summary>
        /// "Nouveau nom du collège d'enseignement :"
        /// </summary>
        /// <returns></returns>
        public const string CollegeNouveauNom = "Nouveau nom du collège d'enseignement :";

        /// <summary>
        /// "Suppression d'un collège d'enseignement."
        /// </summary>
        /// <returns></returns>
        public const string CollegeSuppression = "Suppression d'un collège d'enseignement.";

        /// <summary>
        /// "Voulez-vous vraiment supprimer ce collège d'enseignement ?"
        /// </summary>
        /// <returns></returns>
        public const string CollegeSuppressionConfirmation = "Voulez-vous vraiment supprimer ce collège d'enseignement ?";

        /// <summary>
        /// "Ajouter un collège d'enseignement"
        /// </summary>
        /// <returns></returns>
        public const string CollegeAjout = "Ajout d'un collège d'enseignement.";

        /// <summary>
        /// "Ajouter un collège d'enseignement"
        /// </summary>
        /// <returns></returns>
        public const string CollegeAjoutNom = "Nom du collège d'enseignement à ajouter : ";
        #endregion

        #region Pages Erreur
        #endregion

        #region Recherche = Aucun
        /// <summary>
        /// "Aucun cours"
        /// </summary>
        /// <returns></returns>
        public const string AucunCoursSansRecherche = "Aucun cours.";

        /// <summary>
        /// "Aucun demande d'inscription"
        /// </summary>
        /// <returns></returns>
        public const string AucunDemandeInscriptionSansRecherche = "Aucun demande d'inscription.";

        /// <summary>
        /// "Aucun groupe ne répond à ces critères"
        /// </summary>
        /// <returns></returns>
        public const string AucunGroupe = "Aucun groupe ne répond à ces critères.";

        /// <summary>
        /// "Aucun collège ne répond à ces critères"
        /// </summary>
        /// <returns></returns>
        public const string AucunCollege = "Aucun collège ne répond à ces critères.";

        /// <summary>
        /// "Aucun cours ne répond à ces critères"
        /// </summary>
        /// <returns></returns>
        public const string AucunCours = "Aucun cours ne répond à ces critères.";

        /// <summary>
        /// "Aucun dossier d'étudiant ne répond à ces critères"
        /// </summary>
        /// <returns></returns>
        public const string AucunDossierEtudiant = "Aucun dossier d'étudiant ne répond à ces critères.";

        /// <summary>
        /// "Aucun enseignant ne répond à ces critères"
        /// </summary>
        /// <returns></returns>
        public const string AucunEnseignant = "Aucun enseignant ne répond à ces critères.";

        /// <summary>
        /// "Aucun étudiant ne répond à ces critères"
        /// </summary>
        /// <returns></returns>
        public const string AucunEtudiant = "Aucun étudiant ne répond à ces critères.";

        /// <summary>
        /// "Aucun jumelage ne répond à ces critères"
        /// </summary>
        /// <returns></returns>
        public const string AucunJumelage = "Aucun jumelage ne répond à ces critères.";

        /// <summary>
        /// "Aucun programme d'études ne répond à ces critères"
        /// </summary>
        /// <returns></returns>
        public const string AucunProgramme = "Aucun programme d'études ne répond à ces critères.";

        /// <summary>
        /// "Aucune inscription ne répond à ces critères"
        /// </summary>
        /// <returns></returns>
        public const string AucunInscription = "Aucune inscription ne répond à ces critères.";
        #endregion
    }
}