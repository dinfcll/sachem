using System;
using System.Diagnostics;
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
        /// <param name="Code"></param>
        /// <returns></returns>
        public static string CoursADejaCeCode(string Code)
        { return $"Il existe déjà un cours ayant le code {Code}."; }

        /// <summary>
        /// Le cours {0} a été enregistré.
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>private string I_003(string cours)
        public static string CoursEnregistre(string Cours)
        { return $"Le cours {Cours} a été enregistré."; }

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
        /// <param name="Cours"></param>
        /// <returns></returns>
        public static string CoursSupprime(string Cours)
        { return $"Le cours {Cours} a été supprimé."; }

        /// <summary>
        /// L'étudiant {0} a été enregistré.
        /// </summary>
        /// <param name="Etudiant"></param>
        /// <returns></returns>
        public static string EtudiantEnregistre(string Etudiant)
        { return $"L'étudiant {Etudiant} a été enregistré."; }

        /// <summary>
        /// Impossible de retirer le programme d'étude {0} des programmes suivis par l'étudiant.
        /// </summary>
        /// <param name="Programme"></param>
        /// <returns></returns>
        public static string ImpossibleDeRetirerProgrammeDUnEtudiant(string Programme)
        { return $"Impossible de retirer le programme d'étude {Programme} des programmes suivis par l'étudiant."; }

        /// <summary>
        /// L'enseignant ne peut être supprimé car il est relié à un cours.
        /// </summary>
        /// <returns></returns>
        public const string EnseignantNePeutEtreSupprime = "L'enseignant ne peut être supprimé car il est relié à un cours.";

        /// <summary>
        /// Impossible d’enregistrer cet enseignant. Il existe déjà un enseignant ayant le nom d'usager {0}.
        /// </summary>
        /// <param name="NomUsager"></param>
        /// <returns></returns>
        public static string NomEnseignantDejaExistant(string NomUsager)
        { return $"Impossible d’enregistrer cet enseignant. Il existe déjà un enseignant ayant le nom d'usager {NomUsager}."; }

        /// <summary>
        /// L'étudiant ne peut être supprimé, car il est associé à un groupe.
        /// </summary>
        /// <returns></returns>
        public static string EtudiantNePeutEtreSupprimeCarLieAUnGroupe()
        { return "L'étudiant ne peut être supprimé, car il est associé à un groupe."; }

        /// <summary>
        /// L'usager {0} a été modifié.
        /// </summary>
        /// <param name="NomUsager"></param>
        /// <returns></returns>
        public static string UsagerModfie(string NomUsager)
        { return $"L'usager {NomUsager} a été modifié."; }

        /// <summary>
        /// Le programme d'étude {0} a été retiré de la liste des programmes suivis par l'étudiant.
        /// </summary>
        /// <param name="Programme"></param>
        /// <returns></returns>
        public static string ProgrammeRetireDelaListeEtudiant(string Programme)
        { return $"Le programme d'étude {Programme} a été retiré de la liste des programmes suivis par l'étudiant."; }

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
        /// <param name="NoGroupe"></param>
        /// <returns></returns>
        public static string GroupeSupprime(int NoGroupe)
        { return $"Le groupe {NoGroupe} a été supprimé."; }

        /// <summary>
        /// Impossible d'enregistrer ce groupe. Il existe déjà un groupe ayant le numéro {0}.
        /// </summary>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>
        public static string GroupeAyantLeMemeNumero(int NoGroupe)
        { return $"Impossible d'enregistrer ce groupe. Il existe déjà un groupe ayant le numéro {NoGroupe}."; }

        /// <summary>
        /// L'étudiant {0} a été retiré du groupe {1}.
        /// </summary>
        /// <param name="Matricule"></param>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>
        public static string EudiantRetireDuGroupe(string Matricule, int NoGroupe)
        { return $"L'étudiant {Matricule} a été retiré du groupe {NoGroupe}."; }

        /// <summary>
        /// Impossible d'ajouter l'étudiant {0} au groupe puisqu'il en fait déjà partie.
        /// </summary>
        /// <param name="Matricule"></param>
        /// <returns></returns>
        public static string EtudiantAjouteDeuxFoisAuGroupe(string Matricule)
        { return $"Impossible d'ajouter l'étudiant {Matricule} au groupe puisqu'il en fait déjà partie."; }

        /// <summary>
        /// L'étudiant {0} a été ajouté au groupe {1}.
        /// </summary>
        /// <param name="Matricule"></param>
        /// <param name="IdGroupe"></param>
        /// <returns></returns>
        public static string EtudiantAjouteAuGroupe(string Matricule, int IdGroupe)
        { return $"L'étudiant {Matricule} a été ajouté au groupe {IdGroupe}."; }

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
        /// <param name="Etudiant"></param>
        /// <returns></returns>
        public static string EtudiantSupprime(string Etudiant)
        { return $"L'étudiant {Etudiant} a été supprimé."; }

        /// <summary>
        /// L'enseignant {0} a été supprimer.
        /// </summary>
        /// <param name="Enseignant"></param>
        /// <returns></returns>
        public static string EnseignantSupprime(string Enseignant)
        { return $"L'enseignant {Enseignant} a été supprimé."; }

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
        /// <param name="Fichier"></param>
        /// <returns></returns>
        public static string ErreurTransfertFichier(string Fichier)
        { return $"Erreur lors du transfert du fichier {Fichier}."; }

        /// <summary>
        /// Un fichier {0} de même nom est déjà présent sur le serveur.
        /// </summary>
        /// <param name="Fichier"></param>
        /// <returns></returns>
        public static string FichierAvecLeMemeNomExisteDeja(string Fichier)
        { return $"Un fichier {Fichier} de même nom est déjà présent sur le serveur."; }

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
        /// <param name="Matricule"></param> 
        /// <param name="IdGroupe"></param>
        /// <param name="NomCours"></param>
        /// <returns></returns>
        public static string EtudiantDeplacementDeGroupe(string Matricule, int IdGroupe, string NomCours)
        { return $"L'étudiant {Matricule} a été déplacé au groupe {IdGroupe} du cours {NomCours}."; }

        /// <summary>
        /// L'étudiant {0} ne peut pas être déplacé au groupe {1} du cours {2},car il y est déjà!
        /// </summary>
        /// <param name="Matricule"></param>
        /// <param name="IdGroupe"></param>
        /// <param name="NomCours"></param>
        /// <returns></returns>
        public static string EtudiantDeplacementDeCoursImpossible(string Matricule, int IdGroupe, string NomCours)
        { return $"L'étudiant {Matricule} ne peut pas être déplacé au groupe {IdGroupe} du cours {NomCours}, car il y est déjà."; }

        /// <summary>
        /// L'étudiant ne peut être supprimé s'il est jumelé
        /// </summary>
        /// <returns></returns>
        public static string EtudiantNonSupprimeCarJumele()
        { return $"L'étudiant ne peut être supprimé s'il est jumelé."; }

        /// <summary>
        /// Le collège {0} a été ajouté
        /// </summary>
        /// <param name="NomCollege"></param>
        /// <returns></returns>
        public static string CollegeAjoute(string NomCollege)
        { return $"Le collège {NomCollege} a été ajouté."; }

        /// <summary>
        /// L'étudiant {0} a été modifié.
        /// </summary>
        /// <param name="NomEtudiant"></param>
        /// <returns></returns>
        public static string EtudiantModifie(string NomEtudiant)
        { return $"L'étudiant {NomEtudiant} a été modifié."; }

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
        /// <param name="NomCollege"></param>
        /// <returns></returns>
        public static string CollegeSupprime(string NomCollege)
        { return $"Le collège {NomCollege} a bien été supprimé."; }
        
        /// <summary>
        /// Problème lors de l'envoi du courriel, le port {0} est bloqué.
        /// </summary>
        /// <param name="NumeroPort></param>
        /// <returns></returns>
        public static string EnvoiCourrielImpossiblePortBloque(string NumeroPort)
        { return $"Problème lors de l'envoi du courriel, le port {NumeroPort} est bloqué."; }

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
        /// <param name="DateDebut"></param>
        /// <param name="DateFin"></param>
        /// <returns></returns>
        public static string DatesDansLaSession (string DateDebut, string DateFin)
        { return $"Les dates de début et de fin doivent faire partie de la session. {DateDebut} et {DateFin}."; }

        /// <summary>
        /// Un des deux champs {0}, {1} doit être complété.
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        public static string CompleterLesChamps(string param1, string param2)
        { return $"Un des deux champs {param1}, {param2} doit être complété."; }

        /// <summary>
        /// Le résultat est requis si le statut du cours est réussi.
        /// </summary>
        /// <returns></returns>
        public static string ResultatRequisSiReussi()
        { return $"Le résultat est requis si le statut du cours est réussi."; }
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
        /// <returns></returns>
        public const string LongueurDeQuatreCaracteres = "Longueur requise: 4 caractères.";

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

        #endregion

        #region Question

        /// <summary>
        /// Voulez-vous vraiment supprimer le cours {0}?
        /// </summary>
        /// <param name="Cours"></param>
        /// <returns></returns>
        public static string VraimentSupprimerCours(string Cours)
        { return $"Voulez-vous vraiment supprimer le cours {Cours}?"; }

        /// <summary>
        /// Voulez-vous vraiment supprimer le programme d'études {0} ?
        /// </summary>
        /// <param name="nomProgrammeEtude"></param>
        /// <returns></returns>
        public static string VraimentSupprimerProgrammeEtude(string nomProgrammeEtude)
        { return $"Voulez-vous vraiment supprimer le programme d'études {nomProgrammeEtude} ?"; }

        /// <summary>
        /// Voulez-vous vraiment supprimer l'enseignant {0} ?
        /// </summary>
        /// <param name="Enseignant"></param>
        /// <returns></returns>
        public static string VraimentSupprimerEnseignant(string Enseignant)
        { return $"Voulez-vous vraiment supprimer l'enseignant {Enseignant} ?"; }

        /// <summary>
        /// L'enseignant {0} a été créé. Souhaitez-vous <a href=\"Sachem/Groupes/{1}\">y associer un groupe?</a>
        /// </summary>
        /// <param name="NomUsager"></param>
        /// <param name="id_Enseignant"></param>
        /// <returns></returns>
        public static MvcHtmlString AjouterUnGroupeAUnEnseignant(string NomUsager, int id_Enseignant)
        { return MvcHtmlString.Create($"L'enseignant {NomUsager} a été créé. Souhaitez-vous <a href=\"/Groupes/Create?idEns={id_Enseignant}\">y associer un groupe?</a>");}

        /// <summary>
        /// Voulez-vous vraiment supprimer le groupe {0}?
        /// </summary>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>
        public static string VraimentSupprimerGroupe(int NoGroupe)
        { return $"Voulez-vous vraiment supprimer le groupe {NoGroupe}?"; }

        /// <summary>
        /// Des étudiants sont rattachés au groupe {0} Souhaitez-vous le supprimer définitivement?
        /// </summary>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>
        public static string VraimentSupprimerGroupeAvecEtudiantsRattaches(int NoGroupe)
        { return $"Des étudiants sont rattachés au groupe {NoGroupe}. Souhaitez-vous le supprimer définitivement?"; }

        /// <summary>
        /// Le groupe {0} a été créé. Souhaitez-vous y associer des étudiants?
        /// </summary>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>
        public static string GroupeCreeAssocierEtudiant(int NoGroupe)
        { return $"Le groupe {NoGroupe} a été créé. Souhaitez-vous y associer des étudiants?"; }

        /// <summary>
        /// Voulez-vous vraiment retirer l'étudiant {0} du groupe {1}?
        /// </summary>
        /// <param name="Matricule"></param>
        /// <param name="NoGroupe"></param>
        /// <returns></returns>
        public static string RetirerEtudiantDUnGroupe(string Matricule,int NoGroupe)
        { return $"Voulez-vous vraiment retirer l'étudiant {Matricule} du groupe {NoGroupe}?"; }

        /// <summary>
        /// L'étudiant {0} est déjà inscrit à un cours. Voulez-vous le déplacer? 
        /// </summary>
        /// <param name="PrenomNom"></param>
        /// <returns></returns>
        public static string VraimentDeplacerEtudiant(string PrenomNom)
        { return $"L'étudiant {PrenomNom} est déjà inscrit à un cours. Voulez-vous le déplacer?"; }
        
        /// <summary>
        /// Voulez-vous vraiment supprimer le collège {0}?
        /// </summary>
        /// <param name="College"></param>
        /// <returns></returns>
        public static string VraimentSupprimerCollege(string College)
        { return $"Voulez-vous vraiment supprimer le collège {College}?"; }

        /// <summary>
        /// Voulez-vous vraiment mettre fin à ce jumelage?
        /// </summary>
        /// <returns></returns>
        public static string VraimentMettreFinJumelage()
        { return $"Voulez-vous vraiment mettre fin à ce jumelage?"; }

        /// <summary>
        /// Voulez-vous vraiment supprimer l'étudiant {0} ?
        /// </summary>
        /// <param name="NomEtudiant"></param>
        /// <returns></returns>
        public static string VraimentSupprimerEtudiant(string NomEtudiant)
        { return $"Voulez-vous vraiment supprimer l'étudiant {NomEtudiant}?"; }

        #endregion

        }
}